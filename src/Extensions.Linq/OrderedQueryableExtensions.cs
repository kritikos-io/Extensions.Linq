namespace Kritikos.Extensions.Linq
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public static class OrderedQueryableExtensions
	{
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

			if (size == 0 && page > 1)
			{
				throw new ArgumentException("Size 0 should only be used with page number 1, otherwise data parity can not be guaranteed!");
			}

			return source.Skip((page - 1) * size)
				.TakeIf(size > 0, size);
		}
	}
}
