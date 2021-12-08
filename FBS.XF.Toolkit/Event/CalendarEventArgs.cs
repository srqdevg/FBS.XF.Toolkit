using System;

namespace FBS.XF.Toolkit.Event
{
	/// <summary>
	/// Calendar Event Args.
	/// </summary>
	/// <seealso cref="System.EventArgs" />
	public class CalendarEventArgs : EventArgs
	{
		#region Properties
		/// <summary>
		/// Creates new date.
		/// </summary>
		public DateTime? NewDate;
		#endregion
	}
}
