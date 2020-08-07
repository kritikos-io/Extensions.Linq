namespace Kritikos.Extensions.Linq
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.CompilerServices;

	public static class EnumerableExtensions
	{
		/// <summary>
		/// Returns all distinct elements of the given source, where "distinctness"
		/// is determined via a projection and the default equality comparer for the projected type.
		/// </summary>
		/// <typeparam name="TSource">Type of the source sequence.</typeparam>
		/// <typeparam name="TKey">Type of the projected element.</typeparam>
		/// <param name="source">Source sequence.</param>
		/// <param name="selector">Projection for determining "distinctness".</param>
		/// <returns>
		/// A sequence consisting of distinct elements from the source sequence,
		/// comparing them by the specified key projection.
		/// </returns>
		public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
			this IEnumerable<TSource> source,
			Func<TSource, TKey> selector)
			=> source.DistinctBy(selector, null);

		/// <summary>
		/// Returns all distinct elements of the given source, where "distinctness"
		/// is determined via a projection and the specified comparer for the projected type.
		/// </summary>
		/// <typeparam name="TSource">Type of the source sequence.</typeparam>
		/// <typeparam name="TKey">Type of the projected element.</typeparam>
		/// <param name="source">Source sequence.</param>
		/// <param name="selector">Projection for determining "distinctness".</param>
		/// <param name="comparer">The equality comparer to use to determine whether or not keys are equal.
		/// If null, the default equality comparer for <c>TSource</c> is used.</param>
		/// <returns>
		/// A sequence consisting of distinct elements from the source sequence,
		/// comparing them by the specified key projection.
		/// </returns>
		public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
			this IEnumerable<TSource> source,
			Func<TSource, TKey> selector,
			IEqualityComparer<TKey>? comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			if (selector == null)
			{
				throw new ArgumentNullException(nameof(selector));
			}

			var knownKeys = new HashSet<TKey>(comparer);
			foreach (var element in source)
			{
				if (knownKeys.Add(selector(element)))
				{
					yield return element;
				}
			}
		}

		/// <summary>
		/// Returns the set of elements in the first sequence which aren't
		/// in the second sequence, according to a given key selector.
		/// </summary>
		/// <remarks>
		/// This is a set operation; if multiple elements in <paramref name="first"/> have
		/// equal keys, only the first such element is returned.
		/// This operator uses deferred execution and streams the results, although
		/// a set of keys from <paramref name="second"/> is immediately selected and retained.
		/// </remarks>
		/// <typeparam name="TSource">The type of the elements in the input sequences.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="selector"/>.</typeparam>
		/// <param name="first">The sequence of potentially included elements.</param>
		/// <param name="second">The sequence of elements whose keys may prevent elements in
		/// <paramref name="first"/> from being returned.</param>
		/// <param name="selector">The mapping from source element to key.</param>
		/// <returns>A sequence of elements from <paramref name="first"/> whose key was not also a key for
		/// any element in <paramref name="second"/>.</returns>
		public static IEnumerable<TSource> ExceptBy<TSource, TKey>(
			this IEnumerable<TSource> first,
			IEnumerable<TSource> second,
			Func<TSource, TKey> selector)
			=> ExceptBy(first, second, selector, null);

		/// <summary>
		/// Returns the set of elements in the first sequence which aren't
		/// in the second sequence, according to a given key selector.
		/// </summary>
		/// <remarks>
		/// This is a set operation; if multiple elements in <paramref name="first"/> have
		/// equal keys, only the first such element is returned.
		/// </remarks>
		/// <typeparam name="TSource">The type of the elements in the input sequences.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="selector"/>.</typeparam>
		/// <param name="first">The sequence of potentially included elements.</param>
		/// <param name="second">The sequence of elements whose keys may prevent elements in
		/// <paramref name="first"/> from being returned.</param>
		/// <param name="selector">The mapping from source element to key.</param>
		/// <param name="comparer">The equality comparer to use to determine whether or not keys are equal.
		/// If null, the default equality comparer for <c>TSource</c> is used.</param>
		/// <returns>A sequence of elements from <paramref name="first"/> whose key was not also a key for
		/// any element in <paramref name="second"/>.</returns>
		public static IEnumerable<TSource> ExceptBy<TSource, TKey>(
			this IEnumerable<TSource> first,
			IEnumerable<TSource> second,
			Func<TSource, TKey> selector,
			IEqualityComparer<TKey>? comparer)
		{
			if (first == null)
			{
				throw new ArgumentNullException(nameof(first));
			}

			if (second == null)
			{
				throw new ArgumentNullException(nameof(second));
			}

			if (selector == null)
			{
				throw new ArgumentNullException(nameof(selector));
			}

			var keys = new HashSet<TKey>(second.Select(selector), comparer);
			foreach (var element in first)
			{
				var key = selector(element);
				if (keys.Contains(key))
				{
					continue;
				}

				yield return element;
				keys.Add(key);
			}
		}

		/// <summary>
		/// Flattens a sequence containing arbitrarily-nested sequences. An additional parameter specifies a function that projects an inner sequence via a property of an object.
		/// </summary>
		/// <typeparam name="T">The <see cref="Type"/> of elements in <paramref name="source"/>.</typeparam>
		/// <param name="source">The given <see cref="IEnumerable{T}"/> instance.</param>
		/// <param name="filter">Optional predicate to exclude items.</param>
		/// <param name="selector">A function that receives each element of the sequence as an object
		/// and projects an inner sequence to be flattened. If the function
		/// returns <c>null</c> then the object argument is considered a leaf
		/// of the flattening process.</param>
		/// <returns>
		/// A sequence that contains the elements of <paramref name="source"/> and all nested sequences projected via the <paramref name="selector"/>
		/// function that satisfy the <paramref name="filter"/>.
		/// </returns>
		public static IEnumerable<T> Flatten<T>(
			this IEnumerable<T> source,
			Func<T, bool>? filter = null,
			Func<T, IEnumerable<T>>? selector = null)
		{
			if (source == null)
			{
				yield break;
			}

			if (filter != null)
			{
				source = source.Where(filter);
			}

			foreach (var node in source)
			{
				yield return node;
				var children = (selector == null)
					? node as IEnumerable<T>
					: selector(node);

#pragma warning disable CS8604, 8601 // Possible null reference argument.
				foreach (var child in children.Flatten(filter, selector))
#pragma warning restore CS8604, 8601 // Possible null reference argument.
				{
					yield return child;
				}
			}
		}

		/// <summary>
		/// Executes the given action on each element in the source sequence.
		/// </summary>
		/// <typeparam name="T">The type of the elements in the sequence.</typeparam>
		/// <param name="source">The sequence of elements.</param>
		/// <param name="action">The action to execute on each element.</param>
		/// <returns>The original sequence after the action is applied.</returns>
		public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			foreach (var element in source)
			{
				action(element);
				yield return element;
			}
		}

		/// <summary>
		/// Executes the given action on each element in the source sequence. Each element's index is used in the logic of the action.
		/// </summary>
		/// <typeparam name="T">The type of the elements in the sequence.</typeparam>
		/// <param name="source">The sequence of elements.</param>
		/// <param name="action">The action to execute on each element.</param>
		/// <returns>The original sequence after the action is applied.</returns>
		public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			var index = 0;
			foreach (var element in source)
			{
				action(element, index++);
				yield return element;
			}
		}

		public static bool HasDuplicates<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
			=> source?.GroupBy(selector).Any(x => x.Count() > 1)
				?? throw new ArgumentNullException(nameof(source));

		public static bool HasDuplicates<TSource>(this IEnumerable<TSource> source)
			=> source?.HasDuplicates(x => x)
				?? throw new ArgumentNullException(nameof(source));

		public static IEnumerable<T> AsEnumerable<T>(this IEnumerator<T> enumerator)
		{
			if (enumerator == null)
			{
				yield break;
			}

			enumerator.Reset();

			while (enumerator.MoveNext())
			{
				yield return enumerator.Current;
			}
		}
	}
}
