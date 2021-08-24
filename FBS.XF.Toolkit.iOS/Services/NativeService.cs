using System;
using System.Reflection;
using CoreTelephony;
using FBS.XF.Toolkit.iOS.Services;
using FBS.XF.Toolkit.Interfaces;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(NativeServices))]
// ReSharper disable once CheckNamespace
namespace FBS.XF.Toolkit.iOS.Services
{
	/// <summary>
	/// NativeService.
	/// </summary>
	public class NativeServices : INativeService
	{
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
			using (var info = new CTTelephonyNetworkInfo ())
			{
				return info.SubscriberCellularProvider?.MobileCountryCode; 
			} 
		}

		/// <summary>
		/// Gets the name of the carrier.
		/// </summary>
		/// <returns>System.String.</returns>
		public string GetCarrierName()
		{
			using (var info = new CTTelephonyNetworkInfo ())
			{ 
				return info.SubscriberCellularProvider?.CarrierName; 
			} 
		}

		/// <summary>
		/// Gets the download folder.
		/// </summary>
		/// <returns>System.String.</returns>
		public string GetDownloadFolder()
		{
			return $"{GetRootDirectory(true)}DOWNLOADS";
		}

		/// <summary>
		/// Gets the primary email.
		/// </summary>
		/// <returns>System.String.</returns>
		public string GetPrimaryEmail()
		{
			// iOS uses an Apple ID, so what is the actual primary email?
			// Let's stick with an empty string for the moment. MORE RESEARCH

			return string.Empty;
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
			return null; //Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDcim).AbsolutePath;
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
			////PHAsset asset = new PHAsset(photofilePath);

			////PHImageManager.DefaultManager.RequestImageData(asset, null, (data, dataUti, orientation, info) =>
			////{
			////	Console.WriteLine(data);
			////	Console.WriteLine(info);
			////});


			////var exif = new ExifInterface(photofilePath);

			////try
			////{
			////	var orientation = (TagOrientation) exif.GetAttributeInt(ExifInterface.TagOrientation, -1);

			////	switch (orientation)
			////	{
			////		case TagOrientation.OrientationRotate90:
			////			return 90;
			////		case TagOrientation.OrientationRotate180:
			////			return 180;
			////		case TagOrientation.OrientationRotate270:
			////			return 270;
			////		default:
			////			return 0;
			////	}
			////}
			////finally
			////{
			////	exif.Dispose();
			////}
			
			return 0;
		}

		/// <summary>
		/// Hides the keyboard.
		/// </summary>
		public void HideKeyboard()
		{
			// Close keyboard
			UIApplication.SharedApplication.KeyWindow.EndEditing(true);
		}

		/// <summary>
		/// Gets the telephone number.
		/// </summary>
		/// <returns>System.String.</returns>
		public string GetPhoneNumber()
		{
			////var manager = (TelephonyManager) Application.Context.GetSystemService(Context.TelephonyService);
			////return manager.Line1Number;
			return null;
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

			////try
			////{
			////	var context = Application.Context;
			////	var dirs = context.GetExternalFilesDirs(null);

			////	foreach (var folder in dirs)
			////	{
			////		var isRemovable = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) ../InvokeIsExternalStorageRemovable(folder);
			////		var isEmulated = Environment.InvokeIsExternalStorageEmulated(folder);

			////		if (sdStorage ? isRemovable && !isEmulated : !isRemovable && isEmulated)
			////		{
			////			baseFolderPath = folder.Path;
			////		}
			////	}
			////}
			////catch (Exception ex)
			////{
			////	Console.WriteLine("GetBaseFolderPath caused the follwing exception: {0}", ex);
			////}

			// This is a full path for the application, now get the top (global) level
			return baseFolderPath.Substring(0, baseFolderPath.IndexOf("android/data", StringComparison.OrdinalIgnoreCase));
		}
		#endregion
	}
}