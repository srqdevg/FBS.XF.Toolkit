using System;
using System.IO;
using System.Reflection;
using Android.Accounts;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.Telephony;
using Android.Util;
using FBS.XF.Toolkit.Android.Services;
using FBS.XF.Toolkit.Interfaces;
using Xamarin.Forms;
using AndroidOSApplication = Android.App.Application;
using AndroidOSEnvironment = Android.OS.Environment;

[assembly: Dependency(typeof(NativeServices))]
namespace FBS.XF.Toolkit.Android.Services
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
			var manager = (TelephonyManager) AndroidOSApplication.Context.GetSystemService(Context.TelephonyService);
			return manager?.SimCountryIso;
		}

		/// <summary>
		/// Gets the name of the carrier.
		/// </summary>
		/// <returns>System.String.</returns>
		public string GetCarrierName()
		{
			var manager = (TelephonyManager) AndroidOSApplication.Context.GetSystemService(Context.TelephonyService);
			return manager?.SimCarrierIdName;
		}

		/// <summary>
		/// Gets the download folder.
		/// </summary>
		/// <returns>System.String.</returns>
		public string GetDownloadFolder()
		{
			// Download folder
#pragma warning disable 618
			var folder = AndroidOSEnvironment.GetExternalStoragePublicDirectory(AndroidOSEnvironment.DirectoryDownloads)?.AbsolutePath;
#pragma warning restore 618
			return folder;
		}

		/// <summary>
		/// Gets the image rotation.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <returns>System.Int32.</returns>
		public int GetImageRotation(string filePath)
		{
			var exif = new ExifInterface(filePath);

			try
			{
				var orientation = (TagOrientation) exif.GetAttributeInt(ExifInterface.TagOrientation, -1);

				switch (orientation)
				{
					case TagOrientation.OrientationRotate90:
						return 90;
					case TagOrientation.OrientationRotate180:
						return 180;
					case TagOrientation.OrientationRotate270:
						return 270;
					default:
						return 0;
				}
			}
			finally
			{
				exif.Dispose();
			}
		}

		/// <summary>
		/// Gets the telephone number.
		/// </summary>
		/// <returns>System.String.</returns>
		public string GetPhoneNumber()
		{
			var manager = (TelephonyManager) AndroidOSApplication.Context.GetSystemService(Context.TelephonyService);
			return manager?.Line1Number;
		}
		
		/// <summary>
		/// Gets the photo directory.
		/// </summary>
		/// <param name="sdStorage">if set to <c>true</c> [sd storage].</param>
		/// <returns>System.String.</returns>
		public string GetPhotoDirectory(bool sdStorage = false)
		{
			// If looking for the SD card?
			if (sdStorage)
			{
				// Return photo location
				return $"{GetRootDirectory(true)}DCIM";
			}

			// Normal internal storage
#pragma warning disable 618
			return AndroidOSEnvironment.GetExternalStoragePublicDirectory(AndroidOSEnvironment.DirectoryDcim)?.AbsolutePath;
#pragma warning restore 618
		}
		
		/// <summary>
		/// Gets the primary email.
		/// </summary>
		/// <returns>System.String.</returns>
		public string GetPrimaryEmail()
		{
			var emailPattern = Patterns.EmailAddress;
			var accounts = AccountManager.Get(AndroidOSApplication.Context)?.GetAccounts();

			if (accounts != null && emailPattern != null)
			{
				foreach (var account in accounts)
				{
					if (emailPattern.Matcher(account.Name ?? string.Empty).Matches())
					{
						return account.Name;
					}
				}
			}

			return string.Empty;
		}

		/// <summary>
		/// Gets the root direcvtory.
		/// </summary>
		/// <param name="sdStorage">if set to <c>true</c> [sd storage].</param>
		/// <returns>System.String.</returns>
		public string GetRootDirectory(bool sdStorage = false)
		{
			return GetBaseFolderPath(sdStorage);
		}

		/// <summary>
		/// Gets the width of the text.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns>System.Double.</returns>
		public double GetTextWidth(string text)
		{
			throw new NotImplementedException();
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
				var originalImage = BitmapFactory.DecodeByteArray(image, 0,image.Length);
				var matrix = new Matrix();
				matrix.PostRotate(rotationDegrees);

				// ReSharper disable once AssignNullToNotNullAttribute
				var rotatedBitmap = Bitmap.CreateBitmap(originalImage, 0, 0, originalImage!.Width, originalImage!.Height, matrix, true);

				using (var memoryStream = new MemoryStream())
				{
					// ReSharper disable once PossibleNullReferenceException
					rotatedBitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, memoryStream);
					return memoryStream.ToArray();
				}
			}
			catch
			{
				return null;
			}
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Gets the base folder path.
		/// </summary>
		/// <param name="sdStorage">The sd storage.</param>
		/// <returns>System.String.</returns>
		private static string GetBaseFolderPath(bool sdStorage)
		{
			var baseFolderPath = "";

			try
			{
				var context = AndroidOSApplication.Context;
				var dirs = context.GetExternalFilesDirs(null);

				if (dirs != null)
				{
					foreach (var folder in dirs)
					{
						var isRemovable = AndroidOSEnvironment.InvokeIsExternalStorageRemovable(folder);
						var isEmulated = AndroidOSEnvironment.InvokeIsExternalStorageEmulated(folder);

						if (sdStorage ? isRemovable && !isEmulated : !isRemovable && isEmulated)
						{
							baseFolderPath = folder.Path;
						}
					}
				}
			}
			catch (Exception)
			{
				return null;
			}

			// This is a full path for the application, now get the top (global) level
			return baseFolderPath.Substring(0, baseFolderPath.IndexOf("android/data", StringComparison.OrdinalIgnoreCase));
		}
		#endregion
	}
}