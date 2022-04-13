using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;
using FBS.XF.Toolkit.Controls;
using FBS.XF.Toolkit.WPF.Renderers;
using MenuItem = System.Windows.Controls.MenuItem;
using Size = System.Windows.Size;

[assembly: ExportRenderer(typeof(CustomLabel), typeof(CustomLabelRenderer))]
namespace FBS.XF.Toolkit.WPF.Renderers
{
	/// <summary>
	/// Custom Label Renderer.
	/// Implements the <see cref="Xamarin.Forms.Platform.WPF.LabelRenderer" />
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Platform.WPF.LabelRenderer" />
	public class CustomLabelRenderer : LabelRenderer
	{
		#region Override methods
		/// <summary>
		/// Gets the size of the desired.
		/// </summary>
		/// <param name="widthConstraint">The width constraint.</param>
		/// <param name="heightConstraint">The height constraint.</param>
		/// <returns>SizeRequest.</returns>
		public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
		{
			var size = base.GetDesiredSize(widthConstraint, heightConstraint);

			//if (Element.Text.StartsWith("The Stone House Hotel is a "))
			//{
			//	var stackLayout = (StackLayout) Element.Parent;

			//	if (stackLayout.Width > 0)
			//	{
			//		var comboBox = new ComboBox();
			//		var formattedText = new FormattedText(Element.Text, CultureInfo.GetCultureInfo("en-us"),
			//			System.Windows.FlowDirection.LeftToRight, new Typeface("OpenSans"), 16, Brushes.Black,
			//			VisualTreeHelper.GetDpi(comboBox).PixelsPerDip);

			//		var width = Math.Round(formattedText.WidthIncludingTrailingWhitespace + 1);
			//		var height = Math.Round(formattedText.Height + 1);

			//		var newValue = Math.Round(width / size.Request.Width + 0.5) * height;
			//		var newSize = new SizeRequest(
			//			new Xamarin.Forms.Size(size.Request.Width, newValue),
			//			new Xamarin.Forms.Size(size.Request.Width, newValue));
			//		return newSize;
			//	}
			//}

			return size;
		}

		/// <summary>
		/// Called when [element changed].
		/// </summary>
		/// <param name="e">The e.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Label> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				if (Control != null)
				{
					Control.TextTrimming = TextTrimming.None;
					Control.TextWrapping = TextWrapping.Wrap;

					// Create menu helper
					//var uiHelper = new UIHelper();

					// Add context menu
					var contextMenu = new ContextMenu();
					contextMenu.Items.Add(UIHelper.CreateMenuItem("Copy", "copyCommand", "copy", CopyMenuItemOnClick));

					Control.ContextMenu = contextMenu;
				}
			}
		}

		/// <summary>
		/// Copies the menu item on click.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void CopyMenuItemOnClick(object sender, RoutedEventArgs e)
		{
			var menuItem = (MenuItem) e.Source;
			var menu = (ContextMenu) menuItem.Parent;
			var textblock = (TextBlock) menu.PlacementTarget;

			Clipboard.SetText(textblock.Text);
		}
		#endregion
	}
}
