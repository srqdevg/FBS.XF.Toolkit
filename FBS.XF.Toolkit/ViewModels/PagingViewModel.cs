using System.Collections.Generic;
using PropertyChanged;

namespace FBS.XF.Toolkit.ViewModels
{
	/// <summary>
	/// Paging View Model.
	/// </summary>
	[AddINotifyPropertyChangedInterface]
	public class PagingViewModel
	{
		#region Private methods
		/// <summary>
		/// Gets the page list.
		/// </summary>
		/// <returns>List&lt;System.Int32&gt;.</returns>
		private List<string> GetPageList()
		{
			////// Calculate mid point
			////var midPoint = ShowPages / 2;
			////var startPage = 1;
			////var totalPages = TotalItems / ItemsPerPage;

			////if (CurrentPage > midPoint)
			////{
			////	if (CurrentPage + midPoint + 1 > totalPages)
			////	{
			////		if (totalPages - ShowPages > 1)
			////		{
			////			startPage = totalPages - ShowPages;
			////		}
			////	}
			////	else if (CurrentPage - midPoint > 0)
			////	{
			////		startPage = CurrentPage - midPoint;
			////	}
			////}

			////var maxPage = totalPages > ShowPages ? ShowPages : totalPages;

			////Pages.Clear();

			////for (var p = 0; p < maxPage; p++)
			////{
			////	Pages.Add((startPage + p).ToString());
			////}

			Pages.Clear();
			Pages.Add("1");
			Pages.Add("2");
			Pages.Add("3");

			// Total pages
			return Pages;
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
		public int ItemsPerPage { get; set; } = 10;

		/// <summary>
		/// Gets or sets the pages.
		/// </summary>
		/// <value>The pages.</value>
		public List<string> Pages => GetPageList();

		/// <summary>
		/// Gets or sets the current page.
		/// </summary>
		/// <value>The current page.</value>
		public int ShowPages { get; set; } = 5;

		/// <summary>
		/// Gets or sets the total items.
		/// </summary>
		/// <value>The total items.</value>
		public int TotalItems { get; set; }
		#endregion
	}
}
