using System.Threading;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using FBS.XF.Toolkit.Images;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace FBS.XF.Toolkit.Android.Images
{
	/// <summary>
	/// Svg image source handler.
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Platform.Android.IImageSourceHandler" />
	/// <remarks>All rights are associated to https://github.com/muak/SvgImageSource
	/// MIT License</remarks>
	public class SvgImageSourceHandler : IImageSourceHandler
	{
		#region Public methods
		/// <summary>
		/// Loads the image async.
		/// </summary>
		/// <returns>The image async.</returns>
		/// <param name="imagesource">Imagesource.</param>
		/// <param name="context">Context.</param>
		/// <param name="cancelationToken">Cancelation token.</param>
		public async Task<Bitmap> LoadImageAsync(ImageSource imagesource, Context context, CancellationToken cancelationToken = default(CancellationToken))
		{
			var svgImageSource = imagesource as SvgImageSource;

			using (var stream = await svgImageSource.GetImageStreamAsync(cancelationToken).ConfigureAwait(false))
			{
				if (stream == null)
				{
					return null;
				}

				return await BitmapFactory.DecodeStreamAsync(stream);
			}
		}
		#endregion
	}
}
