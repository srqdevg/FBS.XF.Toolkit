using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FBS.XF.Toolkit.Images;
using Xamarin.Forms.Platform.WPF;

namespace FBS.XF.Toolkit.WPF.Images
{
	/// <summary>
	/// Svg image source handler.
	/// </summary>
	/// <seealso cref="IImageSourceHandler" />
	public class SvgImageSourceHandler : IImageSourceHandler
	{
		#region Public methods
		/// <summary>
		/// Loads the image async.
		/// </summary>
		/// <param name="imagesource">Imagesource.</param>
		/// <param name="cancelationToken">Cancelation token.</param>
		/// <returns>The image async.</returns>
		public async Task<ImageSource> LoadImageAsync(Xamarin.Forms.ImageSource imagesource, CancellationToken cancelationToken = default(CancellationToken))
		{
			var svgImageSource = imagesource as SvgImageSource;

			// ReSharper disable once PossibleNullReferenceException
			await using (var stream = await svgImageSource.GetImageStreamAsync(cancelationToken).ConfigureAwait(false))
			{
				if (stream == null)
				{
					return null;
				}

				// Create bitmap 
				var bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();

				// Populate with the stream
				bitmapImage.StreamSource = stream;
				bitmapImage.EndInit();

				return bitmapImage;
			}
		}
		#endregion
	}
}