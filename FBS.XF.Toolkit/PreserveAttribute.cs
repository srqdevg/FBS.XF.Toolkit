using System;

namespace FBS.XF.Toolkit
{
	/// <summary>
	/// PreserveAttribute. This class cannot be inherited.
	/// Implements the <see cref="System.Attribute" />
	/// </summary>
	/// <seealso cref="System.Attribute" />
	public sealed class PreserveAttribute : Attribute
	{
		#region Fields
		/// <summary>
		/// Gets or sets a value indicating whether [all members].
		/// </summary>
		/// <value><c>true</c> if [all members]; otherwise, <c>false</c>.</value>
		public bool AllMembers { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="PreserveAttribute"/> is conditional.
		/// </summary>
		/// <value><c>true</c> if conditional; otherwise, <c>false</c>.</value>
		public bool Conditional { get; set; }
		#endregion
	}
}
