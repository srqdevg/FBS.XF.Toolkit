using System;
using System.Reflection;
using Android.Accounts;
using Android.Content;
using Android.Media;
using Android.Telephony;
using Android.Util;
using FBS.XF.Toolkit.Android.Services;
using FBS.XF.Toolkit.Interfaces;
using Xamarin.Forms;
using Application = Android.App.Application;
using Environment = Android.OS.Environment;

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
			var manager = (TelephonyManager) Application.Context.GetSystemService(Context.TelephonyService);
			return manager?.SimCountryIso;
		}

		/// <summary>
		/// Gets the name of the carrier.
		/// </summary>
		/// <returns>System.String.</returns>
		public string GetCarrierName()
		{
			var manager = (TelephonyManager) Application.Context.GetSystemService(Context.TelephonyService);
			return manager?.SimCarrierIdName;
		}

		/// <summary>
		/// Gets the download folder.
		/// </summary>
		/// <returns>System.String.</returns>
		public string GetDownloadFolder()
		{
			// Download folder
			var folder = Application.Context.GetExternalFilesDir(Environment.DirectoryDownloads);
			return folder?.Path;
		}
		
		/// <summary>
		/// Gets the telephone number.
		/// </summary>
		/// <returns>System.String.</returns>
		public string GetPhoneNumber()
		{
			var manager = (TelephonyManager) Application.Context.GetSystemService(Context.TelephonyService);
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
			return Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDcim)?.AbsolutePath;
#pragma warning restore 618
		}
		
		/// <summary>
		/// Gets the primary email.
		/// </summary>
		/// <returns>System.String.</returns>
		public string GetPrimaryEmail()
		{
			var emailPattern = Patterns.EmailAddress;
			var accounts = AccountManager.Get(Application.Context)?.GetAccounts();

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
		/// Gets the rotation.
		/// </summary>
		/// <param name="photofilePath">The photofile path.</param>
		/// <returns>System.Int32.</returns>
		public int GetRotation(string photofilePath)
		{
			var exif = new ExifInterface(photofilePath);

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
				var context = Application.Context;
				var dirs = context.GetExternalFilesDirs(null);

				if (dirs != null)
				{
					foreach (var folder in dirs)
					{
						var isRemovable = Environment.InvokeIsExternalStorageRemovable(folder);
						var isEmulated = Environment.InvokeIsExternalStorageEmulated(folder);

						if (sdStorage ? isRemovable && !isEmulated : !isRemovable && isEmulated)
						{
							baseFolderPath = folder.Path;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("GetBaseFolderPath caused the follwing exception: {0}", ex);
			}

			// This is a full path for the application, now get the top (global) level
			return baseFolderPath.Substring(0, baseFolderPath.IndexOf("android/data", StringComparison.OrdinalIgnoreCase));
		}
		#endregion
	}
}