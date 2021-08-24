using System.Reflection;

namespace FBS.XF.Toolkit.Interfaces
{
	/// <summary>
	/// Interface INativeServices
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
		/// Gets the rotation.
		/// </summary>
		/// <param name="photofilePath">The photofile path.</param>
		/// <returns>System.Int32.</returns>
		int GetRotation(string photofilePath);
		#endregion
	}
}
