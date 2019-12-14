namespace Kritikos.Extensions.Linq
{
	using System;
	using System.ComponentModel;
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
			=> source.OrderByPropertyOrSelector<TSource, int>(property, ListSortDirection.Ascending);

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
			=> source.OrderByPropertyOrSelector<TSource, int>(property, ListSortDirection.Descending);

		/// <summary>
		/// Sorts the elements of a sequence by <paramref name="property"/> or a default selector in ascending order.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="selector"/>.</typeparam>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="property">The name of the property to use in ordering. If this is null or does not exist on <typeparamref name="TSource"/>, the <paramref name="selector"/> will be used.</param>
		/// /// <param name="selector">A function to extract a key from an element.</param>
		/// <returns>An <see cref="IOrderedQueryable{T}"/> whose elements are sorted according to <paramref name="property"/> or <paramref name="selector"/> in ascending order.</returns>
		/// <exception cref="ArgumentException"><paramref name="property"/> does not exist on <typeparamref name="TSource"/> or is empty, and <paramref name="selector"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> (when no valid <paramref name="property"/> is provided) is <see langword="null"/>.</exception>
		public static IOrderedQueryable<TSource> OrderByPropertyOrDefault<TSource, TKey>(
			this IQueryable<TSource> source,
			string property,
			Expression<Func<TSource, TKey>> selector)
			=> source.OrderByPropertyOrSelector(property, ListSortDirection.Ascending, selector);

		/// <summary>
		/// Sorts the elements of a sequence by <paramref name="property"/> or a default selector in descending order.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="selector"/>.</typeparam>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="property">The name of the property to use in ordering. If this is null or does not exist on <typeparamref name="TSource"/>, the <paramref name="selector"/> will be used.</param>
		/// /// <param name="selector">A function to extract a key from an element.</param>
		/// <returns>An <see cref="IOrderedQueryable{T}"/> whose elements are sorted according to <paramref name="property"/> or <paramref name="selector"/> in descending order.</returns>
		/// <exception cref="ArgumentException"><paramref name="property"/> does not exist on <typeparamref name="TSource"/> or is empty, and <paramref name="selector"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> (when no valid <paramref name="property"/> is provided) is <see langword="null"/>.</exception>
		public static IOrderedQueryable<TSource> OrderByPropertyOrDefaultDescending<TSource, TKey>(
			this IQueryable<TSource> source,
			string property,
			Expression<Func<TSource, TKey>> selector)
			=> source.OrderByPropertyOrSelector(property, ListSortDirection.Descending, selector);

		/// <summary>
		/// Sorts the elements of a sequence in the order specified by <paramref name="direction"/>.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="direction">The direction to sort to.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <returns>An <see cref="IOrderedQueryable{T}"/> whose elements are sorted according to a key, in <paramref name="direction"/> order.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is <see langword="null"/>.</exception>
		public static IOrderedQueryable<TSource> OrderByDirection<TSource, TKey>(
			this IQueryable<TSource> source,
			ListSortDirection direction,
			Expression<Func<TSource, TKey>> keySelector)
			=> direction == ListSortDirection.Ascending
				? source.OrderBy(keySelector)
				: source.OrderByDescending(keySelector);

		private static IOrderedQueryable<TSource> OrderByPropertyOrSelector<TSource, TKey>(
			this IQueryable<TSource> source,
			string propertyName,
			ListSortDirection direction,
			Expression<Func<TSource, TKey>>? keySelector = null)
		{
			var entity = typeof(TSource);
			var property = entity.GetProperty(propertyName);

			if (property == null && keySelector == null)
			{
				throw new ArgumentException($"Type of {entity.FullName} does not contain property {propertyName}!");
			}

			return property != null
				? source.OrderByPropertyNameInDirection(direction, entity, property)
				: source.OrderByDirection(direction, keySelector);
		}

		private static IOrderedQueryable<TSource> OrderByPropertyNameInDirection<TSource>(
			this IQueryable<TSource> source,
			ListSortDirection direction,
			Type type,
			MemberInfo member)
		{
			var arg = Expression.Parameter(type, "x");
			var body = Expression.Property(arg, member.Name);

			dynamic selector = Expression.Lambda(body, arg);

			return direction == ListSortDirection.Ascending
				? Queryable.OrderBy(source, selector)
				: Queryable.OrderByDescending(source, selector);
		}
	}
}
