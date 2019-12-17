namespace Kritikos.Extensions.LinqTests
{
	using System;
	using System.Linq;

	using Kritikos.Extensions.Linq;

	using Xunit;

	public class OrderedQueryableTests
	{
		[Fact]
		public void PaginationTests()
		{
			const int arrSize = 5;
			const int pageSize = 2;
			IOrderedQueryable<int> arr = null;

			Assert.Throws<ArgumentNullException>(() => arr.Slice(1, pageSize));

			arr = QueryableIfTests.GetRandomQueryable(arrSize).OrderBy(x => x);

			Assert.Throws<ArgumentException>(() => arr.Slice(0, pageSize));
			Assert.Throws<ArgumentException>(() => arr.Slice(1, -1));

			Assert.Throws<ArgumentException>(() => arr.Slice(2, 0));

			var page = arr.Slice(1, pageSize).ToList();
			Assert.Equal(pageSize,page.Count);

			page = arr.Slice(2, pageSize).ToList();
			Assert.Equal(pageSize, page.Count);

			page = arr.Slice(3, pageSize).ToList();
			Assert.Single(page);

			page = arr.Slice(4, pageSize).ToList();
			Assert.Empty(page);

			page = arr.Slice(1, arrSize * 2).ToList();
			Assert.Equal(arrSize, page.Count);

			page = arr.Slice(1, 0).ToList();
			Assert.Equal(arrSize, page.Count);
		}

		[Fact]
		public void ThenByTests()
		{
			var animals = QueryableOrderingTests.Animals.OrderByProperty("Id");

			Assert.Throws<ArgumentException>(() => animals.ThenByProperty("Ib"));
			Assert.Throws<ArgumentException>(() => animals.ThenByPropertyDescending("Ib"));

			var withSelector = animals.ThenBy(x => x.Name);
			var withProperty = animals.ThenByProperty("Name");

			Assert.Equal(withSelector,withProperty);

			var withSelectorDesc = animals.ThenBy(x => x.Name);
			var withPropertyDesc = animals.ThenByProperty("Name");

			Assert.Equal(withSelectorDesc, withPropertyDesc);
		}
	}
}
