using FBS.XF.Toolkit.Images;
using Xamarin.Forms.Internals;

namespace FBS.XF.Toolkit.WPF.Images
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
			Registrar.Registered.Register(typeof(SvgImageSource), typeof(SvgImageSourceHandler));
		}
		#endregion
	}
}