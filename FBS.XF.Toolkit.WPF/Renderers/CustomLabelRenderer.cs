﻿using System.Windows;
using FBS.XF.Toolkit.Controls;
using FBS.XF.Toolkit.WPF.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;

[assembly: ExportRenderer(typeof(CustomLabel), typeof(CustomLabelRenderer))]
namespace FBS.XF.Toolkit.WPF.Renderers
{
	public class CustomLabelRenderer : LabelRenderer
	{
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

		public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
		{
			var size = base.GetDesiredSize(widthConstraint, heightConstraint);
			var newSize = new SizeRequest(size.Request,new Xamarin.Forms.Size(200, size.Request.Height -2));
			return newSize;
		}
	}
}