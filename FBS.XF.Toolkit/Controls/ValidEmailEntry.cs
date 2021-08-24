using Xamarin.Forms;

namespace FBS.XF.Toolkit.Controls
{
	/// <summary>
	/// Valid Email Entry.
	/// </summary>
	/// <remarks>
	/// So I can't get the email keyboard to appear unless it is set immediately,
	/// so lets just extend the ValidEmailEntry and set the keyboard
	/// </remarks>
	public class ValidEmailEntry : ValidEntry
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ValidEmailEntry"/> class.
		/// </summary>
		public ValidEmailEntry() : base(Keyboard.Email)
		{
			// All the work should be done by the base class
		}
		#endregion
	}
}