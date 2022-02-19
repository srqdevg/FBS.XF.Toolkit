namespace FBS.XF.Toolkit.Interfaces
{
	/// <summary>
	/// IUtility Helper
	/// </summary>
	public interface IUtilityHelper
	{
		#region Methods
		/// <summary>
		/// Launches the browser.
		/// </summary>
		/// <param name="address">The address.</param>
		void LaunchBrowser(string address);

		/// <summary>
		/// Refreshes the layout.
		/// </summary>
		void RefreshLayout();
		#endregion
	}
}
