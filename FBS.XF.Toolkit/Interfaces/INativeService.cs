﻿using System.Reflection;

namespace FBS.XF.Toolkit.Interfaces
{
	/// <summary>
	/// INative Services
	/// </summary>
	/// <remarks>
	/// Define various helper methods to get information from the native device
	/// </remarks>
	public interface INativeService
	{
		#region Methods
		/// <summary>
		/// Gets the assembly.
		/// </summary>
		/// <returns>Assembly.</returns>
		Assembly GetAssembly();

		/// <summary>
		/// Gets the carrier country.
		/// </summary>
		/// <returns>System.String.</returns>
		string GetCarrierCountry();

		/// <summary>
		/// Gets the name of the carrier.
		/// </summary>
		/// <returns>System.String.</returns>
		string GetCarrierName();

		/// <summary>
		/// Gets the download folder.
		/// </summary>
		/// <returns>System.String.</returns>
		string GetDownloadFolder();

		/// <summary>
		/// Gets the image rotation.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <returns>System.Int32.</returns>
		int GetImageRotation(string filePath);

		/// <summary>
		/// Gets the photo directory.
		/// </summary>
		/// <param name="sdStorage">if set to <c>true</c> [sd storage].</param>
		/// <returns>System.String.</returns>
		string GetPhotoDirectory(bool sdStorage);

		/// <summary>
		/// Gets the phone number.
		/// </summary>
		/// <returns>System.String.</returns>
		string GetPhoneNumber();

		/// <summary>
		/// Gets the primary email.
		/// </summary>
		/// <returns>System.String.</returns>
		string GetPrimaryEmail();

		/// <summary>
		/// Gets the root directory.
		/// </summary>
		/// <param name="sdStorage">if set to <c>true</c> [sd storage].</param>
		/// <returns>System.String.</returns>
		string GetRootDirectory(bool sdStorage);

		/// <summary>
		/// Gets the width of the text.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns>System.Double.</returns>
		double GetTextWidth(string text);

		/// <summary>
		/// Rotates the image.
		/// </summary>
		/// <param name="image">The image.</param>
		/// <param name="rotationDegrees">The rotation degrees.</param>
		/// <returns>System.Byte[].</returns>
		byte[] RotateImage(byte[] image, int rotationDegrees);
		#endregion
	}
}
