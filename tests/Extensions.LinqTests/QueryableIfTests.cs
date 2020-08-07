namespace Kritikos.Extensions.LinqTests
{
	using System;
	using System.Linq;

	using Kritikos.Extensions.Linq;

	using Xunit;

	public class QueryableIfTests
	{
		private const int QueryableSize = 50;
		private const int GreaterThan = 30;
		private const int Take = 2;
		private const int Skip = 5;
		private static readonly Random Random = new Random();

		internal static IQueryable<int> GetRandomQueryable(int size, int min = 0, int max = 100)
		{
			var arr = new int[size];
			for (var i = 0; i < size; i++)
			{
				arr[i] = Random.Next(min, max);
			}

			return arr.AsQueryable();
		}

		[Fact]
		public void WhereIfTests()
		{
			var arr = GetRandomQueryable(QueryableSize);

			var greaterThan = arr.WhereIf(true, x => x > GreaterThan);
			Assert.Equal(arr.Count(x => x > GreaterThan), greaterThan.Count());

			var invalidCondition = arr.WhereIf(false, x => x < 0);
			Assert.Equal(arr.Count(), invalidCondition.Count());
		}

		[Fact]
		public void TakeIfTests()
		{
			var arr = GetRandomQueryable(QueryableSize);

			var taken = arr.TakeIf(true, Take);
			Assert.Equal(Take, taken.Count());

			var invalidCondition = arr.TakeIf(false, Take);
			Assert.Equal(arr.Count(), invalidCondition.Count());
		}

		[Fact]
		public void SkipIfTests()
		{
			var arr = GetRandomQueryable(QueryableSize);

			var skipped = arr.SkipIf(true, Skip);
			Assert.Equal(QueryableSize - Skip, skipped.Count());

			var invalidCondition = arr.SkipIf(false, Skip);
			Assert.Equal(arr.Count(), invalidCondition.Count());
		}
	}
}
