﻿using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FBS.XF.Toolkit.Interfaces;
using FBS.XF.Toolkit.WPF.Controls;
using Xamarin.Forms;

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
		private void DatePicker_SelectedDatesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			SelectedDate = datePicker.SelectedDate;
			datePickerWindow.DialogResult = true;
		}

		/// <summary>
		/// Handles the KeyUp event of the Window control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
		private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == System.Windows.Input.Key.Escape)
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
			// Create dialog window
			var window = new System.Windows.Window
			{
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
		private System.Windows.Controls.Calendar datePicker;
		private Window datePickerWindow;
		#endregion
	}
}