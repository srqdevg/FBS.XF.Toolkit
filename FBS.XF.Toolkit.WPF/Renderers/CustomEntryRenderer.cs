using System;
using FBS.XF.Toolkit.Controls;
using FBS.XF.Toolkit.WPF.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace FBS.XF.Toolkit.WPF.Renderers
{
	/// <summary>
	/// CustomEntryRenderer.
	/// Implements the <see cref="Xamarin.Forms.Platform.WPF.EntryRenderer" />
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Platform.WPF.EntryRenderer" />
	public class CustomEntryRenderer : EntryRenderer
	{
		#region Override methods
		public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
		{
			var size = base.GetDesiredSize(widthConstraint, heightConstraint);
			//var newSize = new SizeRequest(size.Request, new Size(200, size.Request.Height - 2));

			if (Element.WidthRequest == 40)
			{
				Console.Write(":");
			}

			return size;
		}   
		
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
				}
			}
		}
		#endregion
	}
}
