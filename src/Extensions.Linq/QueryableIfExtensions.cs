namespace Kritikos.Extensions.Linq
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;

	public static class QueryableIfExtensions
	{
		/// <summary>
		/// Filters a sequence of values based on a predicate, only if <paramref name="condition"/> is true.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of the <paramref name="source"/>.</typeparam>
		/// <param name="source">The <see cref="IEnumerable{T}"/> to filter.</param>
		/// <param name="condition">The condition to decide whether to filter or not.</param>
		/// <param name="predicate">A function to test each source element for a condition; the second parameter of the function represents the index of the source element.</param>
		/// <returns>An <see cref="IEnumerable{T}"/> that contains elements from the input sequence that satisfy the predicate if the <paramref name="condition"/> was true.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> was <see langword="null"/>.</exception>
		public static IQueryable<TSource> WhereIf<TSource>(
			this IQueryable<TSource> source,
			bool condition,
			Expression<Func<TSource, bool>> predicate)
			=> condition
				? source.Where(predicate)
				: source;

		/// <summary>
		/// Returns a specified number of contiguous elements from the start of a sequence, only if <paramref name="condition"/> is true.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of the <paramref name="source"/>.</typeparam>
		/// <param name="source">The <see cref="IEnumerable{T}"/> to return elements from.</param>
		/// <param name="condition">The condition to decide whether to filter or not.</param>
		/// <param name="take">The number of elements to return.</param>
		/// <returns>An <see cref="IEnumerable{T}"/>  that contains the specified number of elements from the start of the input sequence if the <paramref name="condition"/> was true.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> was <see langword="null"/>.</exception>
		public static IQueryable<TSource> TakeIf<TSource>(this IQueryable<TSource> source, bool condition, int take)
			=> condition
				? source.Take(take)
				: source;

		/// <summary>
		/// Bypasses a specified number of elements in a sequence and then returns the remaining elements, only if <paramref name="condition"/> is true.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of the <paramref name="source"/>.</typeparam>
		/// <param name="source">The <see cref="IEnumerable{T}"/> to return elements from.</param>
		/// <param name="condition">The condition to decide whether to filter or not.</param>
		/// <param name="skip">The number of elements to skip before returning the remaining elements.</param>
		/// <returns>An <see cref="IEnumerable{T}"/>  that contains the elements that occur after the specified index in the input sequence if the <paramref name="condition"/> was true.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> was <see langword="null"/>.</exception>
		public static IQueryable<TSource> SkipIf<TSource>(this IQueryable<TSource> source, bool condition, int skip)
			=> condition
				? source.Skip(skip)
				: source;
	}
}
