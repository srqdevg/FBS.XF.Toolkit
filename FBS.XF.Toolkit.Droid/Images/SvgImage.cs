using Android.Content;
using FBS.XF.Toolkit.Images;
using Xamarin.Forms.Internals;

namespace FBS.XF.Toolkit.Android.Images
{
	/// <summary>
	/// Svg image.
	/// </summary>
	/// <remarks>
	/// All rights are associated to https://github.com/muak/SvgImageSource
	/// MIT License
	/// </remarks>
	public static class SvgImage
	{
		#region Public methods
		/// <summary>
		/// Init this instance.
		/// </summary>
		public static void Init(Context context)
		{
			Registrar.Registered.Register(typeof(SvgImageSource), typeof(SvgImageSourceHandler));

			using var display = context.Resources?.DisplayMetrics;

			if (display != null)
			{
				SvgImageSource.ScreenScale = display.Density;
			}
		}
		#endregion
	}
}