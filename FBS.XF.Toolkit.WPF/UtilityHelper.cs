using System;
using System.Diagnostics;
using System.Windows;
using FBS.XF.Toolkit.Interfaces;
using FBS.XF.Toolkit.WPF;
using Xamarin.Forms;

[assembly: Dependency(typeof(UtilityHelper))]
namespace FBS.XF.Toolkit.WPF
{
	/// <summary>
	/// Utility Helper.
	/// </summary>
	public class UtilityHelper : IUtilityHelper
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

				//var windowHeight = HostWindow.Height;
				//var windowWidth = HostWindow.Width;
				//var windowState = HostWindow.WindowState;

				//if (windowState == WindowState.Maximized)
				//{
				//	HostWindow.Height = ScreenHeight;
				//	HostWindow.Width = ScreenWidth;
				//	HostWindow.WindowState = WindowState.Normal;
				//	HostWindow.UpdateLayout();

				//	HostWindow.WindowState = WindowState.Maximized;
				//	HostWindow.Height = windowHeight;
				//	HostWindow.Width = windowWidth;
				//}
				//else if (windowState == WindowState.Normal)
				//{
				//	HostWindow.UpdateLayout();
				//	HostWindow.Height = HostWindow.Height + 1;
				//	HostWindow.Width = HostWindow.Width + 1;
				//	HostWindow.UpdateLayout();

				//	HostWindow.Height = windowHeight;
				//	HostWindow.Width = windowWidth;
				//}
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
