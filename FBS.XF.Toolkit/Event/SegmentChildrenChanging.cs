using System;
using System.Collections.Generic;
using FBS.XF.Toolkit.Controls;

namespace FBS.XF.Toolkit.Event
{
	[Preserve(AllMembers = true)]
	public class SegmentChildrenChanging : EventArgs
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="SegmentChildrenChanging"/> class.
		/// </summary>
		/// <param name="oldValues">The old values.</param>
		/// <param name="newValues">The new values.</param>
		public SegmentChildrenChanging(IList<SegmentedControlOption> oldValues, IList<SegmentedControlOption> newValues)
		{
			OldValues = oldValues;
			NewValues = newValues;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Creates new values.
		/// </summary>
		/// <value>The new values.</value>
		public IList<SegmentedControlOption> NewValues { get; }

		/// <summary>
		/// Gets the old values.
		/// </summary>
		/// <value>The old values.</value>
		public IList<SegmentedControlOption> OldValues { get; }
		#endregion
	}
}
