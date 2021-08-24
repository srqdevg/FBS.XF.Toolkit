using System;

namespace FBS.XF.Toolkit
{
	/// <summary>
	/// Calendar Result.
	/// </summary>
	public class CalendarResult
	{
		#region Properties
		/// <summary>
		/// Gets or sets a value indicating whether this instance is success.
		/// </summary>
		/// <value><c>true</c> if this instance is success; otherwise, <c>false</c>.</value>
		public bool IsSuccess { get; set; }

		/// <summary>
		/// Gets or sets the selected date.
		/// </summary>
		/// <value>The selected date.</value>
		public DateTime? SelectedDate { get; set; }
		#endregion
	}
}
