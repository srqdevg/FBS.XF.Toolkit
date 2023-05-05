using System;
using System.Collections.Generic;
using System.Linq;
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
		/// <remarks>
		/// Some code from https://github.com/cornflourblue/JW.Pager/blob/master/Pager.cs
		/// </remarks>
		public void GeneratePageList()
		{
			// Calculate total pages
			var totalPages = (int) Math.Ceiling((decimal) TotalItems / ItemsPerPage);

			// Show all pages
			var startPage = 1;
			var endPage = totalPages;

			if (totalPages > ShowPages)
			{
				// Total pages more than max so calculate start and end pages
				var maxPagesBeforeCurrentPage = (int) Math.Floor((decimal) ShowPages / 2);
				var maxPagesAfterCurrentPage = (int) Math.Ceiling((decimal) ShowPages / 2) - 1;

				if (CurrentPage <= maxPagesBeforeCurrentPage)
				{
					// Current page near the start
					startPage = 1;
					endPage = ShowPages;
				}
				else if (CurrentPage + maxPagesAfterCurrentPage >= totalPages)
				{
					// Current page near the end
					startPage = totalPages - ShowPages + 1;
					endPage = totalPages;
				}
				else
				{
					// Current page somewhere in the middle
					startPage = CurrentPage - maxPagesBeforeCurrentPage;
					endPage = CurrentPage + maxPagesAfterCurrentPage;
				}
			}

			// create an array of pages that can be looped over
			var pages = Enumerable.Range(startPage, (endPage + 1) - startPage)
				.Select(p => new IdText {Id = p.ToString(), IsSelected = CurrentPage.Equals(p), Text = p.ToString()})
				.ToList();

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
