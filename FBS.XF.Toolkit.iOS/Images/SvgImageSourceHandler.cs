using System.Threading;
using System.Threading.Tasks;
using FBS.XF.Toolkit.Images;
using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.iOS.Images
{
	/// <summary>
	/// Svg image source handler.
	/// </summary>
	/// <remarks>
	/// All rights are associated to https://github.com/muak/SvgImageSource
	/// MIT License
	/// </remarks>
	public class SvgImageSourceHandler : IImageSourceHandler
	{
		#region Public methods
		/// <summary>
		/// Loads the image async.
		/// </summary>
		/// <returns>The image async.</returns>
		/// <param name="imagesource">Imagesource.</param>
		/// <param name="cancelationToken">Cancelation token.</param>
		/// <param name="scale">Scale.</param>
		public async Task<UIImage> LoadImageAsync(ImageSource imagesource, CancellationToken cancelationToken = default(CancellationToken), float scale = 1)
		{
			var svgImageSource = (SvgImageSource) imagesource;

			if (svgImageSource != null)
			{
				using (var stream = await svgImageSource.GetImageStreamAsync(cancelationToken).ConfigureAwait(false))
				{
					return stream == null ? null : UIImage.LoadFromData(NSData.FromStream(stream), SvgImageSource.ScreenScale);
				}
			}

			return null;
		}
		#endregion
	}
}