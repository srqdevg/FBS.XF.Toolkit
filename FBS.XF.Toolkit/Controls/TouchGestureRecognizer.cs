using System;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Controls
{
	/// <summary>
	/// Touch Gesture Recognizer.
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Element" />
	/// <seealso cref="Xamarin.Forms.IGestureRecognizer" />
	public class TouchGestureRecognizer : Element, IGestureRecognizer
	{
		#region Fields
		/// <summary>
		/// The touch down
		/// </summary>
		public Action TouchDown;

		/// <summary>
		/// The touch up
		/// </summary>
		public Action TouchUp;
		#endregion
	}
}
