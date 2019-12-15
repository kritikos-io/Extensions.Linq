#nullable disable
namespace Kritikos.Extensions.LinqTests
{
	using System;
	using System.Linq.Expressions;

	using Kritikos.Extensions.Linq;

	using Xunit;

	public class ExpressionDeconstructorTests
	{
		[Fact]
		public void DeconstructBinary()
		{
			const int initial = 1;
			const int addInstead = 5;
			const int addTo = 3;

			Expression<Func<int, int>> expr = null;
			Assert.Throws<ArgumentNullException>(() => expr.Deconstruct(out var param, out var body));

			expr = x => x + initial;

			var restructured = expr switch
			{
				LambdaExpression(var param, BinaryExpression(ExpressionType.Add, var left, ConstantExpression(
				ExpressionType.Constant, _, initial)))
				=> Expression.Lambda(Expression.Add(left, Expression.Constant(addInstead)), param.ToArray()),

				_
				=> throw new NotSupportedException()
			};

			var output = restructured.Compile().DynamicInvoke(addTo);
			Assert.Equal(addInstead + addTo, output);
		}
	}
}
