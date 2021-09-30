using System;
using System.Reflection;
using FBS.XF.Toolkit.Interfaces;
using FBS.XF.Toolkit.WPF.Services;
using Xamarin.Forms;

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
		/// Gets the rotation.
		/// </summary>
		/// <param name="photofilePath">The photofile path.</param>
		/// <returns>System.Int32.</returns>
		public int GetRotation(string photofilePath)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}