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
		internal static IQueryable<Animal> Animals { get; } = new List<Animal>
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
	}
}
