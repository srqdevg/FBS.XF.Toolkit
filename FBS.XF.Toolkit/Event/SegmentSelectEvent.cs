using System;

namespace FBS.XF.Toolkit.Event
{
	/// <summary>
	/// SegmentSelectEventArgs.
	/// Implements the <see cref="System.EventArgs" />
	/// </summary>
	/// <seealso cref="System.EventArgs" />
	[Preserve(AllMembers = true)]
	public class SegmentSelectEventArgs : EventArgs
	{
		#region Properties
		/// <summary>
		/// Creates new value.
		/// </summary>
		/// <value>The new value.</value>
		public int NewValue { get; set; }
		#endregion
	}
}
