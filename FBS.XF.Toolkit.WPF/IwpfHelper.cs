using System.Diagnostics;
using System.Windows;
using FBS.XF.Toolkit.Interfaces;
using FBS.XF.Toolkit.WPF;
using Xamarin.Forms;

[assembly: Dependency(typeof(WPFHelper))]
namespace FBS.XF.Toolkit.WPF
{
	/// <summary>
	/// WPF Helper.
	/// </summary>
	public class WPFHelper : IWPFHelper
	{
		#region Public methods
		/// <summary>
		/// Launches the browser.
		/// </summary>
		/// <param name="address">The address.</param>
		public void LaunchBrowser(string address)
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = address,
				UseShellExecute = true
			});
		}

		/// <summary>
		/// Refreshes the layout.
		/// </summary>
		/// <remarks>
		/// This dumb code actually fixes the MS issues of poor display in Xamarin Forms, by forcing an unseen layout update
		/// And yes, both lines are required even though the second one looks stupid.
		/// </remarks>
		public void RefreshLayout()
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				HostWindow.UpdateLayout();
				HostWindow.WindowState = HostWindow.WindowState;
			});
		}
		#endregion

		#region
		public static double ScreenHeight { get; set; }
		public static double ScreenWidth { get; set; }
		public static Window HostWindow { get; set; }
		#endregion
	}
}
