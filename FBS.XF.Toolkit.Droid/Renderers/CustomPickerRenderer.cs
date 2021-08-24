using System.ComponentModel;
using Android.Content;
using FBS.XF.Toolkit.Android.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(Picker), typeof(CustomPickerRenderer))]

namespace FBS.XF.Toolkit.Android.Renderers
{
	/// <summary>
	/// Custom Picker Renderer.
	/// Implements the <see cref="Xamarin.Forms.Platform.Android.AppCompat.PickerRenderer" />
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Platform.Android.AppCompat.PickerRenderer" />
	/// <remarks>
	/// There are two renderers for Android.
	/// One under --> Xamarin.Forms.Platform.Android
	/// and under --> Xamarin.Forms.Platform.Android.AppCompat
	/// </remarks>
	public class CustomPickerRenderer : PickerRenderer
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CustomPickerRenderer"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public CustomPickerRenderer(Context context) : base(context)
		{
		}
		#endregion

		#region Override methods
		/// <summary>
		/// Handles the <see cref="E:ElementPropertyChanged" /> event.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			var color = Application.Current.RequestedTheme == OSAppTheme.Dark ? Color.White : Color.Black;
			Control.SetTextColor(color);
		}
		#endregion
	}
}