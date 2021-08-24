using FBS.XF.Toolkit.Images;
using UIKit;

namespace FBS.XF.Toolkit.iOS.Images
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
		public static void Init()
		{
			Xamarin.Forms.Internals.Registrar.Registered.Register(typeof(SvgImageSource), typeof(SvgImageSourceHandler));

			// gets screen's scale here. can't use MainScreen.Scale in LoadImageAsync because of not being main thread.
			SvgImageSource.ScreenScale = (float) UIScreen.MainScreen.Scale;
		}
		#endregion
	}
}
