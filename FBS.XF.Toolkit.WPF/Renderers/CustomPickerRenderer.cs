using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;
using FBS.XF.Toolkit.Controls;
using FBS.XF.Toolkit.WPF.Renderers;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace FBS.XF.Toolkit.WPF.Renderers
{
	/// <summary>
	/// CustomPickerRenderer.
	/// Implements the <see cref="Xamarin.Forms.Platform.WPF.PickerRenderer" />
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Platform.WPF.PickerRenderer" />
	public class CustomPickerRenderer : PickerRenderer
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
			var newSize = new SizeRequest(size.Request, new Size(200, size.Request.Height - 2));
			return newSize;
		}

		/// <summary>
		/// Called when [element changed].
		/// </summary>
		/// <param name="e">The e.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				if (Control != null)
				{
					Control.IsEditable = false;
				}
			}
		}
		#endregion
	}
}