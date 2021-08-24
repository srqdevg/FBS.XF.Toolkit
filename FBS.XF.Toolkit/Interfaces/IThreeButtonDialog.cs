using System.Threading.Tasks;

namespace FBS.XF.Toolkit.Interfaces
{
	/// <summary>
	/// Interface IThreeButtonDialog
	/// </summary>
	public interface IThreeButtonDialog
	{
		#region Methods
		/// <summary>
		/// Displays the alert.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="message">The message.</param>
		/// <param name="firstButton">The first button.</param>
		/// <param name="secondButton">The second button.</param>
		/// <param name="cancel">The cancel.</param>
		/// <returns>Task&lt;System.String&gt;.</returns>
		Task<string> DisplayAlert(string title, string message, string firstButton, string secondButton, string cancel);
		#endregion
	}
}