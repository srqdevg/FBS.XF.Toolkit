using System.Windows;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;
using FBS.XF.Toolkit.Controls;
using FBS.XF.Toolkit.WPF.Renderers;

[assembly: ExportRenderer(typeof(CustomLabel), typeof(CustomLabelRenderer))]
namespace FBS.XF.Toolkit.WPF.Renderers
{
	/// <summary>
	/// CustomLabelRenderer.
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
				}
			}
		}
		#endregion
	}
}
