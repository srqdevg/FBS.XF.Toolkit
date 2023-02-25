using FBS.XF.Toolkit.Controls;
using FBS.XF.Toolkit.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace FBS.XF.Toolkit.iOS.Renderers
{
	/// <summary>
	/// Custom Picker Renderer.
	/// Implements the <see cref="PickerRenderer" />
	/// </summary>
	/// <seealso cref="PickerRenderer" />
	public class CustomPickerRenderer : PickerRenderer
	{
		#region Override methods
		/// <summary>
		/// Called when [element changed].
		/// </summary>
		/// <param name="e">The e.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
		{
			base.OnElementChanged(e);

			var element = (CustomPicker) Element;

			if (Control != null && Element != null && !string.IsNullOrEmpty(element.Image))
			{
				var downarrow = UIImage.FromBundle(element.Image);
				Control.RightViewMode = UITextFieldViewMode.Always;
				Control.RightView = new UIImageView(downarrow);
			}
		}
		#endregion`
	}
}