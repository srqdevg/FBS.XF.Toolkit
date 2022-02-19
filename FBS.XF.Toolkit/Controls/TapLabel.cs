using System;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Controls
{
	/// <summary>
	/// TapLabel.
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Label" />
	public class TapLabel : Label, IDisposable
	{
		#region Events/Delegates
		/// <summary>
		/// Occurs when [tapped].
		/// </summary>
		public event EventHandler<ItemTappedEventArgs> Tapped;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="LinkLabel"/> class.
		/// </summary>
		public TapLabel()
		{
			CreateControl();
		}
		#endregion

		#region IDisposable
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			tapRecognizer.Tapped -= Label_Tapped;
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Creates the control.
		/// </summary>
		private void CreateControl()
		{
			// Create a tap recognizer
			tapRecognizer = new TapGestureRecognizer();
			tapRecognizer.Tapped += Label_Tapped;
			GestureRecognizers.Add(tapRecognizer);
		}
		#endregion
		
		#region UI methods
		/// <summary>
		/// Handles the <see cref="E:ItemTapped" /> event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void Label_Tapped(object sender, EventArgs e)
		{
			Tapped?.Invoke(this, new ItemTappedEventArgs(Text, Id, 0));
		}
		#endregion

		#region Fields
		private TapGestureRecognizer tapRecognizer;
		#endregion
	}
}
