namespace Kritikos.Extensions.Linq;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

[ExcludeFromCodeCoverage]
public static class ExpressionDeconstructor
{
  public static void Deconstruct(
    this BinaryExpression expr,
    out ExpressionType operation,
    out Expression left,
    out Expression right)
  {
    ArgumentNullException.ThrowIfNull(expr);

    operation = expr.NodeType;
    left = expr.Left;
    right = expr.Right;
  }

  public static void Deconstruct(
    this BlockExpression expr,
    out ExpressionType operation,
    out List<Expression> expressions,
    out Expression result,
    out List<ParameterExpression> parameters)
  {
    ArgumentNullException.ThrowIfNull(expr);

    operation = expr.NodeType;
    result = expr.Result;
    expressions = expr.Expressions.ToList();
    parameters = expr.Variables.ToList();
  }

  public static void Deconstruct(
    this CatchBlock expr,
    out Expression body,
    out Expression? filter,
    out Type exceptionType,
    out ParameterExpression? exception)
  {
    ArgumentNullException.ThrowIfNull(expr);

    body = expr.Body;
    filter = expr.Filter;
    exceptionType = expr.Test;
    exception = expr.Variable;
  }

  public static void Deconstruct(
    this ConditionalExpression expr,
    out Expression condition,
    out ExpressionType operation,
    out Expression onTrue,
    out Expression onFalse)
  {
    ArgumentNullException.ThrowIfNull(expr);

    condition = expr.Test;
    operation = expr.NodeType;
    onTrue = expr.IfTrue;
    onFalse = expr.IfFalse;
  }

  public static void Deconstruct(
    this ConstantExpression expr,
    out ExpressionType operation,
    out Type constantType,
    out object? val)
  {
    ArgumentNullException.ThrowIfNull(expr);

    operation = expr.NodeType;
    val = expr.Value;
    constantType = expr.Type;
  }

  public static void Deconstruct(
    this DefaultExpression expr,
    out ExpressionType operation,
    out Type type)
  {
    ArgumentNullException.ThrowIfNull(expr);

    type = expr.Type;
    operation = expr.NodeType;
  }

  public static void Deconstruct(
    this DynamicExpression expr,
    out ExpressionType operation,
    out List<Expression> args)
  {
    ArgumentNullException.ThrowIfNull(expr);

    operation = expr.NodeType;
    args = expr.Arguments.ToList();
  }

  public static void Deconstruct(
    this ElementInit expr,
    out List<Expression> args,
    out MethodInfo method)
  {
    ArgumentNullException.ThrowIfNull(expr);

    args = expr.Arguments.ToList();
    method = expr.AddMethod;
  }

  public static void Deconstruct(
    this GotoExpression expr,
    out ExpressionType operation,
    out GotoExpressionKind kind,
    out LabelTarget target)
  {
    ArgumentNullException.ThrowIfNull(expr);

    kind = expr.Kind;
    operation = expr.NodeType;
    target = expr.Target;
  }

  public static void Deconstruct(
    this IndexExpression expr,
    out ExpressionType operation,
    out Expression? body,
    out List<Expression> arguments)
  {
    ArgumentNullException.ThrowIfNull(expr);

    body = expr.Object;
    arguments = expr.Arguments.ToList();
    operation = expr.NodeType;
  }

  public static void Deconstruct(
    this InvocationExpression expr,
    out ExpressionType operation,
    out Expression body,
    out List<Expression> args)
  {
    ArgumentNullException.ThrowIfNull(expr);

    body = expr.Expression;
    args = expr.Arguments.ToList();
    operation = expr.NodeType;
  }

  public static void Deconstruct(
    this LabelExpression expr,
    out ExpressionType operation,
    out Expression? defaultValue,
    out LabelTarget target)
  {
    ArgumentNullException.ThrowIfNull(expr);

    operation = expr.NodeType;
    defaultValue = expr.DefaultValue;
    target = expr.Target;
  }

  public static void Deconstruct(this LabelTarget expr, out string? name)
  {
    ArgumentNullException.ThrowIfNull(expr);
    name = expr.Name;
  }

  public static void Deconstruct(
    this LambdaExpression expr,
    out List<ParameterExpression> parameters,
    out Expression body)
  {
    ArgumentNullException.ThrowIfNull(expr);

    parameters = expr.Parameters.ToList();
    body = expr.Body;
  }

  public static void Deconstruct(
    this ListInitExpression expr,
    out ExpressionType operation,
    out NewExpression newExpression,
    out List<ElementInit> initializers)
  {
    ArgumentNullException.ThrowIfNull(expr);

    operation = expr.NodeType;
    initializers = expr.Initializers.ToList();
    newExpression = expr.NewExpression;
  }

  public static void Deconstruct(
    this LoopExpression expr,
    out ExpressionType operation,
    out Expression body,
    out LabelTarget? continueLabel,
    out LabelTarget? breakLabel)
  {
    ArgumentNullException.ThrowIfNull(expr);

    operation = expr.NodeType;
    body = expr.Body;
    continueLabel = expr.ContinueLabel;
    breakLabel = expr.BreakLabel;
  }

  public static void Deconstruct(
    this MemberAssignment expr,
    out Expression body,
    out MemberInfo member,
    out MemberBindingType binding)
  {
    ArgumentNullException.ThrowIfNull(expr);

    body = expr.Expression;
    member = expr.Member;
    binding = expr.BindingType;
  }

  public static void Deconstruct(
    this MemberBinding expr,
    out MemberInfo member,
    out MemberBindingType memberType)
  {
    ArgumentNullException.ThrowIfNull(expr);

    memberType = expr.BindingType;
    member = expr.Member;
  }

  public static void Deconstruct(
    this MemberExpression expr,
    out ExpressionType operation,
    out Expression? body,
    out MemberInfo member)
  {
    ArgumentNullException.ThrowIfNull(expr);

    operation = expr.NodeType;
    body = expr.Expression;
    member = expr.Member;
  }

  public static void Deconstruct(
    this MemberInitExpression expr,
    out ExpressionType operation,
    out NewExpression newExpression,
    out List<MemberBinding> bindings)
  {
    ArgumentNullException.ThrowIfNull(expr);

    operation = expr.NodeType;
    newExpression = expr.NewExpression;
    bindings = expr.Bindings.ToList();
  }

  public static void Deconstruct(
    this MemberListBinding expr,
    out MemberInfo member,
    out MemberBindingType bindingType,
    out List<ElementInit> initializers)
  {
    ArgumentNullException.ThrowIfNull(expr);

    member = expr.Member;
    initializers = expr.Initializers.ToList();
    bindingType = expr.BindingType;
  }

  public static void Deconstruct(
    this MemberMemberBinding expr,
    out MemberInfo member,
    out List<MemberBinding> bindings,
    out MemberBindingType bindingType)
  {
    ArgumentNullException.ThrowIfNull(expr);

    bindings = expr.Bindings.ToList();
    member = expr.Member;
    bindingType = expr.BindingType;
  }

  public static void Deconstruct(
    this MethodCallExpression expr,
    out ExpressionType operation,
    out Expression? body,
    out MethodInfo methodInfo,
    out List<Expression> args)
  {
    ArgumentNullException.ThrowIfNull(expr);

    operation = expr.NodeType;
    body = expr.Object;
    args = expr.Arguments.ToList();
    methodInfo = expr.Method;
  }

  public static void Deconstruct(
    this NewArrayExpression expr,
    out ExpressionType operation,
    out List<Expression> expressions)
  {
    ArgumentNullException.ThrowIfNull(expr);

    operation = expr.NodeType;
    expressions = expr.Expressions.ToList();
  }

  public static void Deconstruct(
    this NewExpression expr,
    out ExpressionType operation,
    out List<Expression> expressions,
    out List<MemberInfo> members,
    out List<Expression> args,
    out ConstructorInfo? constructor)
  {
    ArgumentNullException.ThrowIfNull(expr);

    operation = expr.NodeType;
    expressions = expr.Arguments.ToList();
    members = expr.Members?.ToList() ?? Array.Empty<MemberInfo>().ToList();
    args = expr.Arguments.ToList();
    constructor = expr.Constructor;
  }

  public static void Deconstruct(
    this ParameterExpression expr,
    out ExpressionType operation,
    out Type type,
    out string? name,
    out bool byRef)
  {
    ArgumentNullException.ThrowIfNull(expr);

    operation = expr.NodeType;
    type = expr.Type;
    name = expr.Name;
    byRef = expr.IsByRef;
  }

  public static void Deconstruct(
    this RuntimeVariablesExpression expr,
    out ExpressionType operation,
    out List<ParameterExpression> parameters)
  {
    ArgumentNullException.ThrowIfNull(expr);

    operation = expr.NodeType;
    parameters = expr.Variables.ToList();
  }

  public static void Deconstruct(
    this SwitchCase expr,
    out Expression body,
    out List<Expression> expressions)
  {
    ArgumentNullException.ThrowIfNull(expr);

    body = expr.Body;
    expressions = expr.TestValues.ToList();
  }

  public static void Deconstruct(
    this SwitchExpression expr,
    out Expression body,
    out Expression value,
    out List<SwitchCase> cases,
    out Expression? defaultBody)
  {
    ArgumentNullException.ThrowIfNull(expr);

    body = expr.SwitchValue;
    cases = expr.Cases.ToList();
    value = expr.SwitchValue;
    defaultBody = expr.DefaultBody;
  }

  public static void Deconstruct(
    this TryExpression expr,
    out ExpressionType operation,
    out Expression body,
    out Expression? fault,
    out List<CatchBlock> catches)
  {
    ArgumentNullException.ThrowIfNull(expr);

    body = expr.Body;
    fault = expr.Fault;
    catches = expr.Handlers.ToList();
    operation = expr.NodeType;
  }

  public static void Deconstruct(
    this TypeBinaryExpression expr,
    out ExpressionType operation,
    out Expression body,
    out Type type,
    out Type operand)
  {
    ArgumentNullException.ThrowIfNull(expr);

    operation = expr.NodeType;
    body = expr.Expression;
    type = expr.Type;
    operand = expr.TypeOperand;
  }

  public static void Deconstruct(
    this UnaryExpression expr,
    out ExpressionType operation,
    out Expression body)
  {
    ArgumentNullException.ThrowIfNull(expr);

    operation = expr.NodeType;
    body = expr.Operand;
  }
}
