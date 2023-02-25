using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Views;
using AndroidX.Core.Content;
using FBS.XF.Toolkit.Android.Renderers;
using FBS.XF.Toolkit.Controls;
using Microsoft.EntityFrameworkCore.Internal;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using static Java.Util.ResourceBundle;
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

			element = (CustomPicker) this.Element;

			////if (Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
			////{
			////	Control.Background = AddPickerStyles(element.Image);
			////}
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Adds the picker styles.
		/// </summary>
		/// <param name="imagePath">The image path.</param>
		/// <returns>LayerDrawable.</returns>
		public LayerDrawable AddPickerStyles(string imagePath)
		{
			ShapeDrawable border = new ShapeDrawable();
			border.Paint.Color = Color.Gray;
			border.SetPadding(10, 10, 10, 10);
			border.Paint.SetStyle(Paint.Style.Stroke);

			Drawable[] layers = { border, GetDrawable(imagePath) };
			LayerDrawable layerDrawable = new LayerDrawable(layers);
			layerDrawable.SetLayerInset(0, 0, 0, 0, 0);

			return layerDrawable;
		}

		/// <summary>
		/// Gets the drawable.
		/// </summary>
		/// <param name="imagePath">The image path.</param>
		/// <returns>BitmapDrawable.</returns>
		private BitmapDrawable GetDrawable(string imagePath)
		{
			var resId = Resources.GetIdentifier(imagePath, "drawable", Context.PackageName);
			var drawable = ContextCompat.GetDrawable(Context, resId);
			var bitmap = ((BitmapDrawable) drawable).Bitmap;

			var result = new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, 70, 70, true));
			result.Gravity = GravityFlags.Right;

			return result;
		}
		#endregion

		#region Fields
		private CustomPicker element;
		#endregion
	}
}