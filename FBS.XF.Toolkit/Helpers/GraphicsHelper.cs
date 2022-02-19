using System;
using System.IO;
using SkiaSharp;

namespace FBS.XF.Toolkit.Helpers
{
	/// <summary>
	/// Graphics Helper.
	/// </summary>
	public static class GraphicsHelper
	{
		#region Public methods
		/// <summary>
		/// Resizes the image.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="height">The height.</param>
		/// <param name="width">The width.</param>
		/// <param name="rotate">The rotate.</param>
		/// <returns>Stream.</returns>
		public static Stream ResizeImage(string fileName, double height, double width, int rotate = 0)
		{
			// Load file into stream
			using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
			{
				// Load into SKBitmap
				var sourceImage = SKBitmap.Decode(stream);

				if (rotate > 0)
				{
					sourceImage = Rotate(sourceImage, rotate);
				}

				// Crop it to the correct ratio
				sourceImage = CropBitmap(sourceImage, width, height);

				// Figure out resize
				var resizedWidth = (int) width;
				var resizedHeight = (int) height;
				var imageInfo = new SKImageInfo(resizedWidth, resizedHeight, SKImageInfo.PlatformColorType,
					SKAlphaType.Premul);

				// Resize 
				using (var surface = SKSurface.Create(imageInfo))
				{
					using (var paint = new SKPaint())
					{
						// High quality with antialiasing
						paint.IsAntialias = true;
						paint.FilterQuality = SKFilterQuality.High;

						// draw the bitmap to fill the surface
						surface.Canvas.DrawBitmap(sourceImage, new SKRectI(0, 0, resizedWidth, resizedHeight), paint);
						surface.Canvas.Flush();

						// Save
						using (var data = surface.Snapshot().Encode(SKEncodedImageFormat.Png, 100))
						{
							var memoryStream = new MemoryStream();
							data.SaveTo(memoryStream);
							memoryStream.Position = 0;
							return memoryStream;
						}
					}
				}
			}
		}

		/// <summary>
		/// Rotates the specified bitmap.
		/// </summary>
		/// <param name="bitmap">The bitmap.</param>
		/// <param name="angle">The angle.</param>
		/// <returns>SKBitmap.</returns>
		public static SKBitmap Rotate(SKBitmap bitmap, double angle)
		{
			var radians = Math.PI * angle / 180;
			var sine = (float)Math.Abs(Math.Sin(radians));
			var cosine = (float)Math.Abs(Math.Cos(radians));
			var originalWidth = bitmap.Width;
			var originalHeight = bitmap.Height;
			var rotatedWidth = (int)(cosine * originalWidth + sine * originalHeight);
			var rotatedHeight = (int)(cosine * originalHeight + sine * originalWidth);
			var rotatedBitmap = new SKBitmap(rotatedWidth, rotatedHeight);

			using (var surface = new SKCanvas(rotatedBitmap))
			{
				// ReSharper disable PossibleLossOfFraction
				surface.Translate(rotatedWidth / 2, rotatedHeight / 2);
				surface.RotateDegrees((float)angle);
				surface.Translate(-originalWidth / 2, -originalHeight / 2);
				surface.DrawBitmap(bitmap, new SKPoint());
				// ReSharper restore PossibleLossOfFraction
			}

			return rotatedBitmap;
		}
	
		/// <summary>
		/// Saves the bitmap to stream.
		/// </summary>
		/// <param name="bitmap">The bitmap.</param>
		/// <returns>Stream.</returns>
		public static Stream SaveBitmapToStream(SKBitmap bitmap)
		{
			// Create image from bitmap and save to stream
			using (var data = SKImage.FromBitmap(bitmap).Encode(SKEncodedImageFormat.Png, 100))
			{
				var memoryStream = new MemoryStream();
				data.SaveTo(memoryStream);
				memoryStream.Position = 0;
				return memoryStream;
			}
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Crops the bitmap.
		/// </summary>
		/// <param name="bitmap">The bitmap.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <returns>SKBitmap.</returns>
		private static SKBitmap CropBitmap(SKBitmap bitmap, double width, double height)
		{
			var ratio = bitmap.Width / width;
			var cropHeight = (float) (height * ratio);
			var cropTop = (bitmap.Height - cropHeight) / 2;
			cropHeight += cropTop * 2;

			ratio = bitmap.Height / height;
			var cropWidth = (float) (height * ratio);
			var cropLeft =  (bitmap.Width - cropWidth) / 2;
			cropWidth += cropLeft * 2;

			if (cropTop < 0)
			{
				cropTop = 0;
				cropHeight = bitmap.Height;
			}

			if (cropLeft < 0)
			{
				cropLeft = 0;
				cropWidth = bitmap.Width;
			}

			var croppedBitmap = new SKBitmap((int) cropWidth, (int) cropHeight);
			var destination = new SKRect(0, 0, cropWidth, cropHeight);
			var source = new SKRect(cropLeft, cropTop, cropWidth, cropHeight);

			using (var canvas = new SKCanvas(croppedBitmap))
			{
				canvas.DrawBitmap(bitmap, source, destination);
			}

			return croppedBitmap;
		}
		#endregion
	}
}