using FBS.XF.Toolkit.Controls;
using FBS.XF.Toolkit.WPF.Renderers;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace FBS.XF.Toolkit.WPF.Renderers
{
	/// <summary>
	/// Custom Entry Renderer.
	/// Implements the <see cref="Xamarin.Forms.Platform.WPF.EntryRenderer" />
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Platform.WPF.EntryRenderer" />
	public class CustomEntryRenderer : EntryRenderer
	{
		#region Override methods
		/// <summary>
		/// Called when [element changed].
		/// </summary>
		/// <param name="e">The e.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				if (Control != null)
				{
					Control.SpellCheck.IsEnabled = true;

					Control.BorderBrush = e.NewElement.IsReadOnly && e.NewElement.IsVisible
						? System.Windows.Media.Brushes.Transparent
						: System.Windows.Media.Brushes.DarkGray;
				}
			}
		}

		/// <summary>
		/// Handles the <see cref="E:ElementPropertyChanged" /> event.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName.Equals("IsReadOnly"))
			{
				var customEntry = (CustomEntry) sender;

				Control.BorderBrush = customEntry.IsReadOnly && customEntry.IsVisible
					? System.Windows.Media.Brushes.Transparent
					: System.Windows.Media.Brushes.DarkGray;
			}

			base.OnElementPropertyChanged(sender, e);
		}
		#endregion
	}
}
