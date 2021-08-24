using System;

namespace FBS.XF.Toolkit
{
	public class RepeaterEventArgs : EventArgs
	{
		public object PreviousSelection { get; }
		public object CurrentSelection { get; }

		public RepeaterEventArgs(object previousSelection, object currentSelection)
		{
			PreviousSelection = previousSelection ;
			CurrentSelection = currentSelection;
		}
	}
}
