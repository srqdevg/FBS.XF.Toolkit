using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Timers;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Helpers
{
	/// <summary>
	/// Utility Helper.
	/// </summary>
	public class UtilityHelper
	{
		#region Public methods
		/// <summary>
		/// Delays the action.
		/// </summary>
		/// <param name="millisecond">The millisecond.</param>
		/// <param name="action">The action.</param>
		public static void DelayAction(int millisecond, Action action)
		{
			Device.StartTimer(new TimeSpan(0, 0, 0, millisecond), () =>
			{
				// Do something when time elapses
				Device.BeginInvokeOnMainThread(action.Invoke);
				return false;
			});
		}
		#endregion
	}
}
