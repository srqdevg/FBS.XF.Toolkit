using System;
using FBS.XF.Toolkit.Event;
using PropertyChanged;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FBS.XF.Toolkit.Controls
{
	/// <summary>
	/// Calendar Popup.
	/// Implements the <see cref="Xamarin.Forms.ContentPage" />
	/// </summary>
	/// <seealso cref="Xamarin.Forms.ContentPage" />
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[AddINotifyPropertyChangedInterface]
	public partial class CalendarPopup 
	{
		#region Events/Delegates
		public event Action<CalendarEventArgs> DataChanged;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CalendarPopup"/> class.
		/// </summary>
		public CalendarPopup()
		{
			InitializeComponent();

			Month = DateTime.Now.Month;
			Year = DateTime.Now.Year;
			TitleBackgroundColor = Application.Current.RequestedTheme == OSAppTheme.Light ? Color.DarkGray : Color.DarkSlateGray;
			BindingContext = this;
		}
		#endregion

		#region UI methods
		/// <summary>
		/// Handles the Clicked event of the CancelButton control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private async void CancelButton_Clicked(object sender, EventArgs e)
		{
			// Cancel
			await PopupNavigation.Instance.PopAsync();
		}

		/// <summary>
		/// Handles the Clicked event of the OKButton control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private async void OKButton_Clicked(object sender, EventArgs e)
		{
			DataChanged?.Invoke(new CalendarEventArgs { NewDate = SelectedDate });
			await PopupNavigation.Instance.PopAsync();
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the maximum date.
		/// </summary>
		/// <value>The maximum date.</value>
		public DateTime? MaximumDate { get; set; }

		/// <summary>
		/// Gets or sets the minimum date.
		/// </summary>
		/// <value>The minimum date.</value>
		public DateTime? MinimumDate { get; set; }

		/// <summary>
		/// Gets or sets the color of the month label.
		/// </summary>
		/// <value>The color of the month label.</value>
		public Color MonthLabelColor { get; set; }

		/// <summary>
		/// Gets the display date.
		/// </summary>
		/// <value>The display date.</value>
		public DateTime DisplayDate => Year == 0 || Month == 0 ? new DateTime(2021, 1, 1) : new DateTime(Year, Month, 1);

		/// <summary>
		/// Gets or sets the month.
		/// </summary>
		/// <value>The month.</value>
		[AlsoNotifyFor(nameof(DisplayDate))]
		public int Month { get; set; }

		/// <summary>
		/// Gets or sets the selected date.
		/// </summary>
		/// <value>The selected date.</value>
		public DateTime? SelectedDate { get; set; }

		/// <summary>
		/// Gets or sets the title text.
		/// </summary>
		/// <value>The title text.</value>
		public string TitleText { get; set; }

		/// <summary>
		/// Gets or sets the color of the title background.
		/// </summary>
		/// <value>The color of the title background.</value>
		public Color TitleBackgroundColor { get; set; }

		/// <summary>
		/// Gets or sets the year.
		/// </summary>
		/// <value>The year.</value>
		[AlsoNotifyFor(nameof(DisplayDate))]
		public int Year { get; set; }
		#endregion
	}
}