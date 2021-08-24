using Xamarin.Forms;

namespace FBS.XF.Toolkit.Controls
{
	/// <summary>
	/// Valid Text Entry.
	/// </summary>
	public class ValidTextEntry : ValidEntry
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ValidTextEntry"/> class.
		/// </summary>
		public ValidTextEntry() : base(Keyboard.Text)
		{
			// All the work should be done by the base class
		}
		#endregion
	}
}