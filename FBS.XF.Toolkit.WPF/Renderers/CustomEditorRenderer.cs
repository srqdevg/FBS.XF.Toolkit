using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;
using FBS.XF.Toolkit.Controls;
using FBS.XF.Toolkit.WPF.Renderers;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]
namespace FBS.XF.Toolkit.WPF.Renderers
{
	/// <summary>
	/// Custom Editor Renderer.
	/// Implements the <see cref="Xamarin.Forms.Platform.WPF.EditorRenderer" />
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Platform.WPF.EditorRenderer" />
	public class CustomEditorRenderer : EditorRenderer
	{
		#region Overrides
		/// <summary>
		/// Gets the size of the desired.
		/// </summary>
		/// <param name="widthConstraint">The width constraint.</param>
		/// <param name="heightConstraint">The height constraint.</param>
		/// <returns>SizeRequest.</returns>
		public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
		{
			var size = base.GetDesiredSize(widthConstraint, heightConstraint);

			if (size.Request.Height < Element.MinimumHeightRequest)
			{
				size = base.GetDesiredSize(widthConstraint, Element.MinimumHeightRequest);
				var newSize = new SizeRequest(
					new Size(size.Request.Width, Element.MinimumHeightRequest),
					new Size(200, Element.MinimumHeightRequest - 2));
				return newSize;
			}
			else
			{
				var newSize = new SizeRequest(size.Request, new Size(200, size.Request.Height - 2));
				return newSize;
			}
		}

		/// <summary>
		/// Called when [element changed].
		/// </summary>
		/// <param name="e">The e.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
		{
			var firstTime = Control == null;

			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				if (Control != null && firstTime)
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
			if (e.PropertyName.Equals("IsReadOnly") || e.PropertyName.Equals("IsVisibile"))
			{
				var customEditor = (CustomEditor) sender;

				Control.BorderBrush = customEditor.IsReadOnly && customEditor.IsVisible 
					? System.Windows.Media.Brushes.Transparent 
					: System.Windows.Media.Brushes.DarkGray;
			}

			base.OnElementPropertyChanged(sender, e);
		}
		#endregion
	}
}