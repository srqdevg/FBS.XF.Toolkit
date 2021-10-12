using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using FBS.XF.Toolkit.Interfaces;
using FBS.XF.Toolkit.WPF.Controls;
using Xamarin.Forms;
using Application = Xamarin.Forms.Application;
using Color = Xamarin.Forms.Color;
using SelectionChangedEventArgs = System.Windows.Controls.SelectionChangedEventArgs;
using SolidColorBrush = System.Windows.Media.SolidColorBrush;
using Thickness = System.Windows.Thickness;

[assembly: Dependency(typeof(DatePickerDialog))]

namespace FBS.XF.Toolkit.WPF.Controls
{
	/// <summary>
	/// DatePickerDialog.
	/// Implements the <see cref="DatePickerDialog" />
	/// </summary>
	/// <seealso cref="DatePickerDialog" />
	public class DatePickerDialog : IDatePickerDialog
	{
		#region UI methods
		/// <summary>
		/// Handles the Click event of the Button control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void DatePicker_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
		{
			SelectedDate = datePicker.SelectedDate;
			datePickerWindow.DialogResult = true;
		}

		/// <summary>
		/// Handles the KeyUp event of the Window control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
		private void Window_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				datePickerWindow.DialogResult = false;
			}
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Picks the date.
		/// </summary>
		/// <param name="startDateTime">The start date time.</param>
		/// <returns>Task&lt;System.Nullable&lt;DateTime&gt;&gt;.</returns>
		public Task<bool> PickDate(DateTime? startDateTime)
		{
			var backgroundColor = (Color) (Application.Current.RequestedTheme == OSAppTheme.Dark
				? Application.Current.Resources["PopupBackgroundDark"]
				: Application.Current.Resources["PopupBackgroundLight"]);

			var backgroundBrush = (SolidColorBrush) new BrushConverter().ConvertFrom(backgroundColor.ToHex());

			// Create dialog window
			var window = new Window
			{
				BorderBrush = backgroundBrush,
				BorderThickness = new Thickness(2, 2, 2, 2),
				ResizeMode = ResizeMode.NoResize,
				ShowInTaskbar = false,
				SizeToContent = SizeToContent.WidthAndHeight,
				Title = "Select Date",
				WindowStartupLocation = WindowStartupLocation.CenterOwner,
				WindowStyle = WindowStyle.None
			};

			window.KeyUp += Window_KeyUp;

			// Date picker 
			datePicker = new Calendar
			{
				IsTodayHighlighted = true,
				DisplayDate = startDateTime ?? DateTime.Now,
				SelectedDate = SelectedDate,
				VerticalContentAlignment = VerticalAlignment.Center
			};

			datePicker.SelectedDatesChanged += DatePicker_SelectedDatesChanged;

			// Add panel to window
			window.Content = datePicker;
			datePickerWindow = window;
			var result = window.ShowDialog();

			var taskCompletionSource = new TaskCompletionSource<bool>();

			if (result.HasValue && result.Value)
			{
				taskCompletionSource.SetResult(result.Value);
			}
			else
			{
				taskCompletionSource.SetResult(false);
			}

			return taskCompletionSource.Task;
		}
		#endregion

		#region Properties
		public DateTime? SelectedDate { get; set; }
		#endregion

		#region Fields
		private Calendar datePicker;
		private Window datePickerWindow;
		#endregion
	}
}