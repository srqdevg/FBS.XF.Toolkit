using System.Linq;
using Android.Content;
using Android.Views;
using FBS.XF.Toolkit.Android.Renderers;
using FBS.XF.Toolkit.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using FrameRenderer = Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer;

[assembly: ExportRenderer(typeof(CustomButton), typeof(ExtendedButtonRenderer))]
namespace FBS.XF.Toolkit.Android.Renderers
{
	/// <summary>
	/// GestureFrameRenderer.
	/// </summary>
	/// <seealso cref="FrameRenderer" />
	public class ExtendedButtonRenderer : FrameRenderer
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ExtendedButtonRenderer"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public ExtendedButtonRenderer(Context context) : base(context)
		{
		}
		#endregion

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
				{
					return;
				}

				if (e.NewElement.GestureRecognizers.All(x => x.GetType() != typeof(TouchGestureRecognizer)))
				{
					return;
				}

				Control.Touch += (sender, args) =>
				{
					switch (args.Event.Action)
					{
						case MotionEventActions.Down:
							foreach (var recognizer in Element.GestureRecognizers.Where(x => x.GetType() == typeof(TouchGestureRecognizer)))
							{
								if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
								{
									touchGestureRecognizer.TouchDown();
								}
							}

							break;
						case MotionEventActions.Up:
						case MotionEventActions.Cancel:
							foreach (var recognizer in Element.GestureRecognizers.Where(x => x.GetType() == typeof(TouchGestureRecognizer)))
							{
								if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
								{
									touchGestureRecognizer.TouchUp();
								}
							}

							break;
					}
				};
			}
		}
		#endregion
	}
}