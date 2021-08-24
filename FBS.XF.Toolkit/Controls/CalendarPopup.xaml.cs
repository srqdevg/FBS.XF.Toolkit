using System;
using PropertyChanged;
using Rg.Plugins.Popup.Services;
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
		public event Action<EventArgs> DataChanged;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CalendarPopup"/> class.
		/// </summary>
		public CalendarPopup()
		{
			InitializeComponent();

			MonthYear = DateTime.Now;
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
			DataChanged?.Invoke(new EventArgs());
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
		/// Gets or sets the month year.
		/// </summary>
		/// <value>The month year.</value>
		public DateTime? MonthYear { get; set; }

		/// <summary>
		/// Gets or sets the selected date.
		/// </summary>
		/// <value>The selected date.</value>
		public DateTime? SelectedDate { get; set; }
		#endregion
	}
}