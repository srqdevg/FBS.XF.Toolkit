using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;
using FBS.XF.Toolkit.Interfaces;
using FBS.XF.Toolkit.WPF.Services;
using Xamarin.Forms;
using System.Net.Mail;

[assembly: Dependency(typeof(NativeServices))]
namespace FBS.XF.Toolkit.WPF.Services
{
	/// <summary>
	/// Native Service.
	/// </summary>
	public class NativeServices : INativeService
	{
		#region Constants/Enumerations
		/// <summary>
		/// TagOrientation
		/// </summary>
		public enum TagOrientation
		{
			OrientationUndefined = 0,
			OrientationNormal = 1,
			OrientationFlipHorizontal = 2,
			OrientationRotate180 = 3,
			OrientationFlipVertical = 4,
			OrientationTranspose = 5,
			OrientationRotate90 = 6,
			OrientationTransverse = 7,
			OrientationRotate270 = 8
		}
		#endregion

		#region NativeServices methods
		/// <summary>
		/// Gets the assembly.
		/// </summary>
		/// <returns>Assembly.</returns>
		public Assembly GetAssembly()
		{
			return GetType().Assembly;
		}

		/// <summary>
		/// Gets the carrier country.
		/// </summary>
		/// <returns>System.String.</returns>
		public string GetCarrierCountry()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the name of the carrier.
		/// </summary>
		/// <returns>System.String.</returns>
		public string GetCarrierName()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the download folder.
		/// </summary>
		/// <returns>System.String.</returns>
		public string GetDownloadFolder()
		{
			// Download folder
			var folder = Environment.GetEnvironmentVariable("USERPROFILE") + @"\" + "Downloads";
			return folder;
		}

		/// <summary>
		/// Gets the image rotation.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <returns>System.Int32.</returns>
		public int GetImageRotation(string filePath)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the telephone number.
		/// </summary>
		/// <returns>System.String.</returns>
		public string GetPhoneNumber()
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
		/// Gets the photo directory.
		/// </summary>
		/// <param name="sdStorage">if set to <c>true</c> [sd storage].</param>
		/// <returns>System.String.</returns>
		public string GetPhotoDirectory(bool sdStorage = false)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
		/// Gets the primary email.
		/// </summary>
		/// <returns>System.String.</returns>
		public string GetPrimaryEmail()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the root direcvtory.
		/// </summary>
		/// <param name="sdStorage">if set to <c>true</c> [sd storage].</param>
		/// <returns>System.String.</returns>
		public string GetRootDirectory(bool sdStorage = false)
		{
			return Environment.GetEnvironmentVariable("USERPROFILE");
		}

		/// <summary>
		/// Gets the width of the text.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns>System.Double.</returns>
		public double GetTextWidth(string text)
		{
			var comboBox = new ComboBox();

			var formattedText = new FormattedText(text, CultureInfo.GetCultureInfo("en-us"),
				System.Windows.FlowDirection.LeftToRight, new Typeface("Verdana"), 16, System.Windows.Media.Brushes.Black,
				VisualTreeHelper.GetDpi(comboBox).PixelsPerDip);

			return Math.Round(formattedText.WidthIncludingTrailingWhitespace + 1);
		}

		/// <summary>
		/// Rotates the image.
		/// </summary>
		/// <param name="image">The image.</param>
		/// <param name="rotationDegrees">The rotation degrees.</param>
		/// <returns>System.Byte[].</returns>
		public byte[] RotateImage(byte[] image, int rotationDegrees)
		{
			try
			{
				// Load  image
				var stream = new MemoryStream();
				stream.Write(image, 0, Convert.ToInt32(image.Length));

				// Rotate image
				var bitmap = new Bitmap(stream);
				bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);

				// Save image
				stream.Position = 0;
				bitmap.Save(stream, ImageFormat.Jpeg);

				return stream.ToArray();
			}
			catch
			{
				return null;
			}
		}
		#endregion
	}
}