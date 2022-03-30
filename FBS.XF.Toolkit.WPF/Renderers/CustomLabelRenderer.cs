using System.Windows;
using System.Windows.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;
using FBS.XF.Toolkit.Controls;
using FBS.XF.Toolkit.WPF.Renderers;
using MenuItem = System.Windows.Controls.MenuItem;

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
			var newSize = new SizeRequest(size.Request, new Xamarin.Forms.Size(200, size.Request.Height - 2));
			return newSize;
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
