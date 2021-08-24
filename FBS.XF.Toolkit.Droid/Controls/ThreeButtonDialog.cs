using System.Threading.Tasks;
using Android.App;
using FBS.XF.Toolkit.Android.Controls;
using FBS.XF.Toolkit.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(ThreeButtonDialog))]
namespace FBS.XF.Toolkit.Android.Controls
{
	/// <summary>
	/// Three Button Alert.
	/// Implements the <see cref="IThreeButtonDialog" />
	/// </summary>
	/// <seealso cref="IThreeButtonDialog" />
	/// <remarks>
	/// https://github.com/DamianAntonowicz/XamarinForms.ThreeButtonAlert
	/// </remarks>
	public class ThreeButtonDialog : IThreeButtonDialog
	{
		#region Public methods
		/// <summary>
		/// Displays the specified title.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="message">The message.</param>
		/// <param name="firstButton">The first button.</param>
		/// <param name="secondButton">The second button.</param>
		/// <param name="cancel">The cancel.</param>
		/// <returns>Task&lt;System.String&gt;.</returns>
		public Task<string> DisplayAlert(string title, string message, string firstButton, string secondButton, string cancel)
		{
			var taskCompletionSource = new TaskCompletionSource<string>();
			var alertBuilder = new AlertDialog.Builder(Platform.CurrentActivity);

			alertBuilder.SetTitle(title);
			alertBuilder.SetMessage(message);

			alertBuilder.SetPositiveButton(firstButton, (senderAlert, args) => { taskCompletionSource.SetResult(firstButton); });

			alertBuilder.SetNegativeButton(secondButton, (senderAlert, args) => { taskCompletionSource.SetResult(secondButton); });

			alertBuilder.SetNeutralButton(cancel, (senderAlery, args) => { taskCompletionSource.SetResult(cancel); });

			var alertDialog = alertBuilder.Create();
			alertDialog.Show();

			return taskCompletionSource.Task;
		}
		#endregion
	}
}