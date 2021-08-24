using Xamarin.Forms;

namespace FBS.XF.Toolkit.Controls
{
	/// <summary>
	/// Valid Telephone Entry.
	/// </summary>
	public class ValidPhoneEntry : ValidEntry
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ValidPhoneEntry"/> class.
		/// </summary>
		public ValidPhoneEntry() : base(Keyboard.Telephone)
		{
		}
		#endregion
	}
}