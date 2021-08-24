using System.Threading.Tasks;
using FBS.XF.Toolkit.Interfaces;
using FBS.XF.Toolkit.iOS.Controls;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ThreeButtonDialog))]
namespace FBS.XF.Toolkit.iOS.Controls
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
			var alerController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

			alerController.AddAction(UIAlertAction.Create(firstButton, UIAlertActionStyle.Default, alert =>
			{
				taskCompletionSource.SetResult(firstButton);
			}));

			alerController.AddAction(UIAlertAction.Create(secondButton, UIAlertActionStyle.Default, alert =>
			{
				taskCompletionSource.SetResult(secondButton);
			}));

			alerController.AddAction(UIAlertAction.Create(cancel, UIAlertActionStyle.Cancel, alert =>
			{
				taskCompletionSource.SetResult(cancel);
			}));

			UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alerController, animated: true, completionHandler: null);

			return taskCompletionSource.Task;
		}
		#endregion
	}
}