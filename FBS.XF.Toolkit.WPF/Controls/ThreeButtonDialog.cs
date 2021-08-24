using System.Threading.Tasks;
using FBS.XF.Toolkit.Interfaces;
using FBS.XF.Toolkit.WPF.Controls;
using Xamarin.Forms;
using System.Windows;
using System.Windows.Controls;

[assembly: Dependency(typeof(ThreeButtonDialog))]
namespace FBS.XF.Toolkit.WPF.Controls
{
	/// <summary>
	/// Three Button Alert.
	/// Implements the <see cref="IThreeButtonDialog" />
	/// </summary>
	/// <seealso cref="IThreeButtonDialog" />
	/// <remarks>https://github.com/DamianAntonowicz/XamarinForms.ThreeButtonAlert</remarks>
	public class ThreeButtonDialog : IThreeButtonDialog
	{
		#region UI methods
		/// <summary>
		/// Handles the Click event of the Button control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			SelectedButton = ((System.Windows.Controls.Button) sender).Content.ToString();
			AlertWindow.DialogResult = true;
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Displays the specified title.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="message">The message.</param>
		/// <param name="firstButtonText">The first button text.</param>
		/// <param name="secondButtonText">The second button text.</param>
		/// <param name="cancelButtonText">The cancel button text.</param>
		/// <returns>System.Threading.Tasks.Task&lt;string&gt;.</returns>
		public Task<string> DisplayAlert(string title, string message, string firstButtonText, string secondButtonText, string cancelButtonText)
		{
			// Create dialog window
			var window = new Window
			{
				ResizeMode = ResizeMode.NoResize,
				ShowInTaskbar = false,
				SizeToContent = SizeToContent.WidthAndHeight,
				Title = title,
				WindowStartupLocation = WindowStartupLocation.CenterOwner
			};

			// Stack panel to hold message and button stack
			var stackPanel = new StackPanel {Margin = new System.Windows.Thickness(20, 10, 20, 10)};
			window.Content = stackPanel;

			// Message label
			var label = new System.Windows.Controls.Label
			{
				Content = message,
				Padding = new System.Windows.Thickness(0, 10, 0, 10)
			};

			stackPanel.Children.Add(label);

			// Buttons stack panel
			var buttonStackPanel = new StackPanel
			{
				HorizontalAlignment = HorizontalAlignment.Right,
				Orientation = Orientation.Horizontal
			};

			stackPanel.Children.Add(buttonStackPanel);

			// First button
			var firstButton = new System.Windows.Controls.Button
			{
				Content = firstButtonText,
				Height = 25,
				IsDefault = true,
				Margin = new System.Windows.Thickness(0, 0, 10, 0),
				Width = 75
			};

			firstButton.Click += Button_Click;
			buttonStackPanel.Children.Add(firstButton);

			// Second button
			var secondButton = new System.Windows.Controls.Button
			{
				Content = secondButtonText,
				Height = 25,
				Margin = new System.Windows.Thickness(0,0,10,0),
				Width = 75
			};

			secondButton.Click += Button_Click;
			buttonStackPanel.Children.Add(secondButton);

			// Cancel button
			var cancelButton = new System.Windows.Controls.Button
			{
				Content = cancelButtonText,
				Height = 25,
				IsCancel = true,
				Margin = new System.Windows.Thickness(0, 0, 10, 0),
				Width = 70
			};
			
			cancelButton.Click += Button_Click;
			buttonStackPanel.Children.Add(cancelButton);

			AlertWindow = window;

			window.ShowDialog();
			
			var taskCompletionSource = new TaskCompletionSource<string>();
			taskCompletionSource.SetResult(SelectedButton);
			return taskCompletionSource.Task;
		}
		#endregion

		#region Properties
		public string SelectedButton { get; set; }
		#endregion

		#region Fields
		public Window AlertWindow { get; set; }
		#endregion
	}
}