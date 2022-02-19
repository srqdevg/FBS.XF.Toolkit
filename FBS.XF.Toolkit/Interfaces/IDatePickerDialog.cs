using System;
using System.Threading.Tasks;

namespace FBS.XF.Toolkit.Interfaces
{
	/// <summary>
	/// IDate Picker Dialog
	/// </summary>
	public interface IDatePickerDialog
	{
		#region Methods
		/// <summary>
		/// Picks the date.
		/// </summary>
		/// <returns>Task&lt;System.Nullable&lt;DateTime&gt;&gt;.</returns>
		Task<bool> PickDate(DateTime? startDate);
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the visit date.
		/// </summary>
		/// <value>The visit date.</value>
		DateTime? SelectedDate { get; set; }
		#endregion
	}
}