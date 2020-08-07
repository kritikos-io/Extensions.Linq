namespace Kritikos.Extensions.Linq
{
	using System;
	using System.ComponentModel;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;

	public static class QueryableOrderingExtensions
	{
		/// <summary>
		/// Sorts the elements of a sequence by <paramref name="property"/> in ascending order.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="property">The name of the property to use in ordering.</param>
		/// <returns>An <see cref="IOrderedQueryable{T}"/> whose elements are sorted according to <paramref name="property"/>in ascending order.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="property"/> does not exist on <typeparamref name="TSource"/> or is empty.</exception>
		public static IOrderedQueryable<TSource> OrderByProperty<TSource>(
			this IQueryable<TSource> source,
			string property)
			=> source.OrderByPropertyNameInDirection(ListSortDirection.Ascending, property);

		/// <summary>
		/// Sorts the elements of a sequence by <paramref name="property"/> in descending order.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="property">The name of the property to use in ordering.</param>
		/// <returns>An <see cref="IOrderedQueryable{T}"/> whose elements are sorted according to <paramref name="property"/>in descending order.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="property"/> does not exist on <typeparamref name="TSource"/> or is empty.</exception>
		public static IOrderedQueryable<TSource> OrderByPropertyDescending<TSource>(
			this IQueryable<TSource> source,
			string property)
			=> source.OrderByPropertyNameInDirection(ListSortDirection.Descending, property);

		/// <summary>
		/// Sorts the elements of a sequence by <paramref name="propertyName"/> in the selected order.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="direction">The direction to sort in.</param>
		/// <param name="propertyName">The name of the property to use in ordering.</param>
		/// <returns>An <see cref="IOrderedQueryable{T}"/> whose elements are sorted according to <paramref name="propertyName"/>in the selected order.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="propertyName"/> does not exist on <typeparamref name="TSource"/> or is empty.</exception>
		public static IOrderedQueryable<TSource> OrderByPropertyNameInDirection<TSource>(
			this IQueryable<TSource> source,
			ListSortDirection direction,
			string propertyName)
		{
			var entity = typeof(TSource);
			var property = entity.GetProperty(propertyName);

			if (property == null)
			{
				throw new ArgumentException($"Type of {entity.FullName} does not contain property {propertyName}!");
			}

			var arg = Expression.Parameter(entity, "x");
			var body = Expression.Property(arg, propertyName);

			dynamic selector = Expression.Lambda(body, arg);

			return direction == ListSortDirection.Ascending
				? Queryable.OrderBy(source, selector)
				: Queryable.OrderByDescending(source, selector);
		}

		/// <summary>
		/// Sorts the elements of a sequence in the selected order.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">The type of the property to sort by.</typeparam>
		/// <param name="source">A sequence of values to order.</param>
		/// /// <param name="selector">A key selector function.</param>
		/// <param name="direction">The direction to sort in.</param>
		/// <returns>An <see cref="IOrderedQueryable{T}"/> whose elements are sorted according to <paramref name="selector"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
		[ExcludeFromCodeCoverage]
		public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(
			IQueryable<TSource> source,
			Expression<Func<TSource, TKey>> selector,
			ListSortDirection direction)
			=> direction == ListSortDirection.Ascending
				? source.OrderBy(selector)
				: source.OrderByDescending(selector);
	}
}
