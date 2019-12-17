namespace Kritikos.Extensions.LinqTests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Kritikos.Extensions.Linq;
	using Kritikos.Extensions.LinqTests.AssertData;

	using Xunit;

	public class QueryableOrderingTests
	{
		private static IQueryable<Animal> Animals { get; } = new List<Animal>
		{
			new Animal { Id = 0, Name = "Sir Cat-a-lot" },
			new Animal { Id = 1, Name = "Bark-a-lot" },
			new Animal { Id = 2, Name = "Tweet-a-lot" },
		}.AsQueryable();

		[Fact]
		public void OrderByName()
		{
			var sortByPropertyName = Animals.OrderByProperty("Id");
			var sortBySelector = Animals.OrderBy(x => x.Id);
			Assert.Equal(sortBySelector, sortByPropertyName);

			Assert.Throws<ArgumentException>(()=>Animals.OrderByProperty("Ib"));

			var sortByPropertyNameDescending = Animals.OrderByPropertyDescending("Id");
			var sortBySelectorDescending = Animals.OrderByDescending(x => x.Id);
			Assert.Equal(sortBySelectorDescending,sortByPropertyNameDescending);

			Assert.Throws<ArgumentException>(() => Animals.OrderByPropertyDescending("Is"));
		}

		[Fact]
		public void OrderByNameOrDefault()
		{
			var sortByPropertyName = Animals.OrderByPropertyOrDefault("Id", x => x.Name);
			var sortByInvalidProperty = Animals.OrderByPropertyOrDefault("ib", x => x.Name);
			var orderByName = Animals.OrderBy(x => x.Name);
			var orderById = Animals.OrderBy(x => x.Id);

			Assert.Equal(orderById,sortByPropertyName);
			Assert.Equal(orderByName,sortByInvalidProperty);

			var sortByPropertyNameDesc = Animals.OrderByPropertyOrDefaultDescending("Id", x => x.Name);
			var sortByInvalidPropertyNameDesc = Animals.OrderByPropertyOrDefaultDescending("Ib", x => x.Name);
			var orderByNameDesc = Animals.OrderByDescending(x => x.Name);
			var orderByIdDesc = Animals.OrderByDescending(x => x.Id);

			Assert.Equal(orderByIdDesc,sortByPropertyNameDesc);
			Assert.Equal(orderByNameDesc,sortByInvalidPropertyNameDesc);
		}
	}
}
