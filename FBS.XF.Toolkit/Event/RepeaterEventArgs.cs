using System;

namespace FBS.XF.Toolkit.Event
{
	/// <summary>
	/// Repeater EventArgs.
	/// Implements the <see cref="System.EventArgs" />
	/// </summary>
	/// <seealso cref="System.EventArgs" />
	public class RepeaterEventArgs : EventArgs
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="RepeaterEventArgs"/> class.
		/// </summary>
		/// <param name="previousSelection">The previous selection.</param>
		/// <param name="currentSelection">The current selection.</param>
		public RepeaterEventArgs(object previousSelection, object currentSelection)
		{
			PreviousSelection = previousSelection;
			CurrentSelection = currentSelection;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the current selection.
		/// </summary>
		/// <value>The current selection.</value>
		public object CurrentSelection { get; }

		/// <summary>
		/// Gets the previous selection.
		/// </summary>
		/// <value>The previous selection.</value>
		public object PreviousSelection { get; }
		#endregion
	}
}
