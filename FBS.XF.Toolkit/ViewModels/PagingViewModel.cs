using System;
using System.Collections.Generic;
using FBS.XF.Toolkit.Models;
using PropertyChanged;

namespace FBS.XF.Toolkit.ViewModels
{
	/// <summary>
	/// Paging View Model.
	/// </summary>
	[AddINotifyPropertyChangedInterface]
	public class PagingViewModel
	{
		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="PagingViewModel"/> class.
		/// </summary>
		public PagingViewModel()
		{
			Pages = new List<IdText>();
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Gets the page list.
		/// </summary>
		/// <returns>List&lt;System.Int32&gt;.</returns>
		public void GeneratePageList()
		{
			// Calculate mid point
			var midPoint = ShowPages / 2;
			var startPage = 1;
			var totalPages = Math.DivRem(TotalItems, ItemsPerPage, out var remainder);

			if (remainder > 0)
			{
				totalPages++;
			}

			// Make sure current page is within range
			if (totalPages < CurrentPage)
			{
				CurrentPage = totalPages;
			}

			if (CurrentPage > midPoint)
			{
				if (CurrentPage + midPoint + 1 > totalPages)
				{
					if (totalPages - ShowPages > 1)
					{
						startPage = (totalPages +1 )- ShowPages;
					}
				}
				else if (CurrentPage - midPoint > 0)
				{
					startPage = CurrentPage - midPoint;
				}
			}

			var maxPage = totalPages > ShowPages ? ShowPages : totalPages;

			Pages = null;

			var pages = new List<IdText>();

			for (var p = 0; p < maxPage; p++)
			{
				var pageNumber = startPage + p;
				pages.Add(new IdText { Id = pageNumber.ToString(), IsSelected = CurrentPage.Equals(pageNumber), Text = pageNumber.ToString() });
			}

			Pages = pages;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the current page.
		/// </summary>
		/// <value>The current page.</value>
		public int CurrentPage { get; set; } = 1;

		/// <summary>
		/// Gets or sets the current page.
		/// </summary>
		/// <value>The current page.</value>
		public int ItemsPerPage { get; set; } = 25;

		/// <summary>
		/// Gets or sets the pages.
		/// </summary>
		/// <value>The pages.</value>
		public List<IdText> Pages { get; set; }

		/// <summary>
		/// Gets or sets the show pages.
		/// </summary>
		/// <value>The show pages.</value>
		public int ShowPages { get; set; } = 5;

		/// <summary>
		/// Gets or sets the total items.
		/// </summary>
		/// <value>The total items.</value>
		public int TotalItems { get; set; }
		#endregion
	}
}
