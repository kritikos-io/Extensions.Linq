namespace Kritikos.Extensions.Linq
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Linq.Expressions;

	public static class OrderedQueryableExtensions
	{
		/// <summary>
		/// Performs a subsequent ordering of the elements in a sequence in ascending order.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <param name="source">An <see cref="IOrderedQueryable{T}"/> that contains elements to sort.</param>
		/// <param name="property">The name of the property to use in ordering.</param>
		/// <returns>An <see cref="IOrderedQueryable{T}"/> whose elements are sorted according to <paramref name="property"/>in ascending order.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="property"/> does not exist on <typeparamref name="TSource"/> or is empty.</exception>
		public static IOrderedQueryable<TSource> ThenByProperty<TSource>(
			this IOrderedQueryable<TSource> source,
			string property)
			=> source.ThenByPropertyNameInDirection(ListSortDirection.Ascending, property);

		/// <summary>
		/// Performs a subsequent ordering of the elements in a sequence in descending order.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <param name="source">An <see cref="IOrderedQueryable{T}"/> that contains elements to sort.</param>
		/// <param name="property">The name of the property to use in ordering.</param>
		/// <returns>An <see cref="IOrderedQueryable{T}"/> whose elements are sorted according to <paramref name="property"/>in descending order.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="property"/> does not exist on <typeparamref name="TSource"/> or is empty.</exception>
		public static IOrderedQueryable<TSource> ThenByPropertyDescending<TSource>(
			this IOrderedQueryable<TSource> source,
			string property)
			=> source.ThenByPropertyNameInDirection(ListSortDirection.Descending, property);

		/// <summary>
		/// Returns <paramref name="size"/> elements, bypassing those on previous <paramref name="page"/> to facilitate paging.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of the <paramref name="source"/>.</typeparam>
		/// <param name="source">The <see cref="IEnumerable{T}"/> to return elements from.</param>
		/// <param name="page">The number of the page to extract.</param>
		/// <param name="size">The number of elements per page.</param>
		/// <returns>The page requested containing <paramref name="size"/> elements.</returns>
		/// <remarks><paramref name="page"/> is one-based index, <paramref name="size"/> 0 brings all elements (should be used with <paramref name="page"/> number 1).</remarks>
		/// <exception cref="ArgumentException"><paramref name="page"/> or <paramref name="size"/> is not greater than zero.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
		public static IQueryable<TSource> Slice<TSource>(
			this IOrderedQueryable<TSource> source,
			int page,
			int size)
		{
			if (page <= 0)
			{
				throw new ArgumentException("Page number should be strictly greater than zero!", nameof(page));
			}

			if (size < 0)
			{
				throw new ArgumentException("Page size should be greater than zero!", nameof(size));
			}

#pragma warning disable IDE0046 // Convert to conditional expression - Ternary operator would reduce readability
			if (size == 0 && page > 1)
#pragma warning restore IDE0046 // Convert to conditional expression
			{
				throw new ArgumentException(
					"Size 0 should only be used with page number 1, otherwise data parity can not be guaranteed!");
			}

			return source.Skip((page - 1) * size)
				.TakeIf(size > 0, size);
		}

		/// <summary>
		/// Performs a subsequent ordering of the elements in a sequence in the selected order.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <param name="source">An <see cref="IOrderedQueryable{T}"/> that contains elements to sort.</param>
		/// <param name="direction">The direction to sort in.</param>
		/// <param name="propertyName">The name of the property to use in ordering.</param>
		/// <returns>An <see cref="IOrderedQueryable{T}"/> whose elements are sorted according to <paramref name="propertyName"/>in the selected order.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="propertyName"/> does not exist on <typeparamref name="TSource"/> or is empty.</exception>
		public static IOrderedQueryable<TSource> ThenByPropertyNameInDirection<TSource>(
			this IOrderedQueryable<TSource> source,
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
				? Queryable.ThenBy(source, selector)
				: Queryable.ThenByDescending(source, selector);
		}

		/// <summary>
		/// Performs a subsequent ordering of the elements in a sequence in the selected order.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">The type of the property to sort by.</typeparam>
		/// <param name="source">An <see cref="IOrderedQueryable{T}"/> that contains elements to sort.</param>
		/// <param name="selector">A key selector function.</param>
		/// <param name="direction">The direction to sort in.</param>
		/// <returns>An <see cref="IOrderedQueryable{T}"/> whose elements are sorted according to <paramref name="selector"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
		[ExcludeFromCodeCoverage]
		public static IOrderedQueryable<TSource> ThenBy<TSource, TKey>(
			IOrderedQueryable<TSource> source,
			Expression<Func<TSource, TKey>> selector,
			ListSortDirection direction)
			=> direction == ListSortDirection.Ascending
				? source.ThenBy(selector)
				: source.ThenByDescending(selector);
	}
}
