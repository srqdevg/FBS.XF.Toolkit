using System;
using System.Windows;
using FBS.XF.Toolkit.Controls;
using FBS.XF.Toolkit.WPF.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;
using Xamarin.Forms.PlatformConfiguration;
using Size = Xamarin.Forms.Size;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]

namespace FBS.XF.Toolkit.WPF.Renderers
{
	public class CustomEditorRenderer : EditorRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
		{
			var firstTime = Control == null;

			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				if (Control != null && firstTime)
				{
					Control.SpellCheck.IsEnabled = true;
				}
			}
		}

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
	}
}