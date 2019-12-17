# Extensions.Linq

[![Build Status](https://dev.azure.com/kritikos/DotNet%20Libaries/_apis/build/status/Extensions.Linq?branchName=master)](https://dev.azure.com/kritikos/DotNet%20Libaries/_build/latest?definitionId=12&branchName=master)
[![Coverage Status](https://coveralls.io/repos/github/kritikos-io/Extensions.Linq/badge.svg?branch=master)](https://coveralls.io/github/kritikos-io/Extensions.Linq?branch=master)
[![codecov](https://codecov.io/gh/kritikos-io/Extensions.Linq/branch/master/graph/badge.svg)](https://codecov.io/gh/kritikos-io/Extensions.Linq)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=kritikos-io_Extensions.Linq&metric=alert_status)](https://sonarcloud.io/dashboard?id=kritikos-io_Extensions.Linq)
[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
![GitHub language count](https://img.shields.io/github/languages/count/kritikos-io/Extensions.Linq)
![GitHub top language](https://img.shields.io/github/languages/top/kritikos-io/Extensions.Linq)

Common System.Linq extensions.

## -If type extensions

* WhereIf
* TakeIf
* SkipIf

Allows queueing of multiple linq queries naturally instead of breaking the flow to provide condition checking.
Typical example in an API trying to implement queries:

```csharp
var query = DbContext.EntityName.Where(x=>true);

if (!string.IsNullOrEmpty(parameters.NameFilter)) {
  query = query.Where(x=> x.Name.Contains(parametersNameFilter))
}

    [...]
```

Apart from the cognitive issues added by such a syntax, developers new to Entity Framework or hasty code writing might end up in something resembling

```csharp
var query = DbContext.EntityName.AsQueryable()
```

This is a major issue, as the extension method ```AsQueryable``` is not available in ```DbSet``` or ```IQueryable```, but in ```IEnumerable```, to which both DbSet and Queryable can be converted **implicitly**. This would force loading data for the entire table, and even worse, leaves an open database cursor, eventually leading to availability issues.

Compare this to the safe and natural Linq syntax provided by these methods

```csharp
var query = DbContext.EntityName
  .WhereIf(
    !string.IsNullOrEmpty(parameters.NameFilter),
    x=> x.Name.Contains(parametersNameFilter))

    [...]
```

## Ordering Extensions

* OrderByProperty
* OrderByPropertyDescending
* ThenByProperty
* ThenByPropertyDescending

Provides a set of methods allowing ordering a queryable by a property name (passed in as string). Also, an overload accepting a fallback selector is provided, in case the property does not exist on the specified type, or the property string is not populated.

**Important information**: As it should be apparent, this method of ordering relies on reflection. However, leveraging the dynamic keyword, polymorphic inline caching is used, so the reflection cost is paid only for the first time any such extension method is called for each type.

*Initial implementation was a collaboration with [palladin](#contributors)*

## Pagination Extensions

* Slice

Simple and safe method to extract pages from Queryable datasets. Only available on ```IOrderedQueryable``` since this is the only way data parity can be enforced.

## Deconstruction Extensions

Provides deconstruct extension methods for all expressions under the System.Linq.Expressions namespace, for easy pattern matching, especially when leveraging the swich expression new to C# 8.0. Requested, and guided, by [palladin](#contributors).

Feature was developed to faciliate the following kind of syntax, in order to provide the base for expression splicing, collaborated under [nessos/Splicer](splicer):

```csharp
expr = x => x + initial;

var restructured = expr switch
{
  LambdaExpression(var param, BinaryExpression(ExpressionType.Add, var left, ConstantExpression(ExpressionType.Constant, _, initial)))
    => Expression.Lambda(Expression.Add(left, Expression.Constant(addInstead)), param.ToArray()),

  _
    => throw new NotSupportedException()
};
```

## Contributors

* [palladin](https://github.com/palladin), aka [@NickPalladinos](https://twitter.com/NickPalladinos), high priest of the Old Ones, providing tips and guidance in return for blood sacrifice.

[splicer]: https://github.com/nessos/Splicer
