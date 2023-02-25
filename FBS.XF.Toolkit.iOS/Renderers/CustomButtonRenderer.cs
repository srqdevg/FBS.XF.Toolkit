using System.Linq;
using FBS.XF.Toolkit.Controls;
using FBS.XF.Toolkit.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]
namespace FBS.XF.Toolkit.iOS.Renderers
{
	/// <summary>
	/// Custom Button Renderer.
	/// </summary>
	/// <seealso cref="FrameRenderer" />
	public class CustomButtonRenderer : FrameRenderer
	{
		#region Override methods
		/// <summary>
		/// Called when [element changed].
		/// </summary>
		/// <param name="e">The e.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement == null)
			{
				if (!e.NewElement.GestureRecognizers.Any())
					return;

				if (e.NewElement.GestureRecognizers.All(x => x.GetType() != typeof(TouchGestureRecognizer)))
					return;

				// ReSharper disable once UseObjectOrCollectionInitializer
				pressGestureRecognizer = new UILongPressGestureRecognizer(() =>
				{
					if (pressGestureRecognizer.State == UIGestureRecognizerState.Began)
					{
						foreach (var recognizer in Element.GestureRecognizers.Where(x => x.GetType() == typeof(TouchGestureRecognizer)))
						{
							if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
							{
								touchGestureRecognizer.TouchDown();
							}
						}
					}
					else if (pressGestureRecognizer.State == UIGestureRecognizerState.Ended)
					{
						foreach (var recognizer in Element.GestureRecognizers.Where(x => x.GetType() == typeof(TouchGestureRecognizer)))
						{
							if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
							{
								touchGestureRecognizer.TouchUp();
							}
						}
					}
				});

				pressGestureRecognizer.MinimumPressDuration = 0.0;
				AddGestureRecognizer(pressGestureRecognizer);
			}
		}
		#endregion

		#region Fields
		private UILongPressGestureRecognizer pressGestureRecognizer;
		#endregion
	}
}