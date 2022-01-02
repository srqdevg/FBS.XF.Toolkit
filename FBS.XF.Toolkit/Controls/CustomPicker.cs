using Xamarin.Forms;

namespace FBS.XF.Toolkit.Controls
{
	/// <summary>
	/// Custom Picker.
	/// Implements the <see cref="Picker" />
	/// </summary>
	/// <seealso cref="Picker" />
	/// <remarks>Used to fix issues in MS version of controls</remarks>
	public class CustomPicker : Picker
	{
		public static readonly BindableProperty SizeToFitProperty =
			BindableProperty.Create(nameof(SizeToFit), typeof(bool), typeof(CustomButton), default(bool));

		public bool SizeToFit
		{
			get => (bool) GetValue(SizeToFitProperty);
			set => SetValue(SizeToFitProperty, value);
		}
	}
}
