using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FBS.XF.Toolkit.Collections
{
	/// <summary>
	/// Sortable Observable Collection.
	/// Implements the <see cref="System.Collections.ObjectModel.ObservableCollection{T}" />
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <seealso cref="System.Collections.ObjectModel.ObservableCollection{T}" />
	/// <remarks>
	/// All copyrigth remains with
	/// https://brianlagunas.com/write-a-sortable-observablecollection-for-wpf/
	/// </remarks>
	public class SortableObservableCollection<T> : ObservableCollection<T>
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="SortableObservableCollection{T}"/> class.
		/// </summary>
		public SortableObservableCollection()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SortableObservableCollection{T}"/> class.
		/// </summary>
		/// <param name="list">The list from which the elements are copied.</param>
		public SortableObservableCollection(List<T> list)
			: base(list)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SortableObservableCollection{T}"/> class.
		/// </summary>
		/// <param name="collection">The collection from which the elements are copied.</param>
		public SortableObservableCollection(IEnumerable<T> collection)
			: base(collection)
		{
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Sorts the specified key selector.
		/// </summary>
		/// <typeparam name="TKey">The type of the t key.</typeparam>
		/// <param name="keySelector">The key selector.</param>
		/// <param name="comparer">The comparer.</param>
		public void Sort<TKey>(Func<T, TKey> keySelector, IComparer<TKey> comparer)
		{
			ApplySort(Items.OrderBy(keySelector, comparer));
		}

		/// <summary>
		/// Sorts the specified key selector.
		/// </summary>
		/// <typeparam name="TKey">The type of the t key.</typeparam>
		/// <param name="keySelector">The key selector.</param>
		/// <param name="direction">The direction.</param>
		public void Sort<TKey>(Func<T, TKey> keySelector, System.ComponentModel.ListSortDirection direction)
		{
			switch (direction)
			{
				case System.ComponentModel.ListSortDirection.Ascending:
				{
					ApplySort(Items.OrderBy(keySelector));
					break;
				}
				case System.ComponentModel.ListSortDirection.Descending:
				{
					ApplySort(Items.OrderByDescending(keySelector));
					break;
				}
			}
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Applies the sort.
		/// </summary>
		/// <param name="sortedItems">The sorted items.</param>
		private void ApplySort(IEnumerable<T> sortedItems)
		{
			var sortedItemsList = sortedItems.ToList();

			foreach (var item in sortedItemsList)
			{
				Move(IndexOf(item), sortedItemsList.IndexOf(item));
			}
		}
		#endregion
	}
}
