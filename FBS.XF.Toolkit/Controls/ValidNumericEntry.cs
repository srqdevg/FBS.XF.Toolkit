using PropertyChanged;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Controls
{
	/// <summary>
	/// Valid Numeric Entry.
	/// </summary>
	[AddINotifyPropertyChangedInterface]
	public class ValidNumericEntry : ValidEntry
	{
		#region Bindable properties
		/// <summary>
		/// The is valid property
		/// </summary>
		public static readonly BindableProperty IsValidProperty =
			BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(CustomButton), default(bool), BindingMode.TwoWay);

		/// <summary>
		/// The maximum value property
		/// </summary>
		public static readonly BindableProperty MaxValueProperty =
			BindableProperty.Create(nameof(MaxValue), typeof(int), typeof(CustomButton), default(int));
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ValidNumericEntry"/> class.
		/// </summary>
		public ValidNumericEntry() : base(Keyboard.Numeric)
		{
			// Add Max Value validation
			// Add NLOG see IPad or other logging!!!

			TextChanged += ValidNumericEntry_TextChanged;
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Valids the numeric entry text changed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="Xamarin.Forms.TextChangedEventArgs"/> instance containing the event data.</param>
		private void ValidNumericEntry_TextChanged(object sender, TextChangedEventArgs e)
		{
			// Do we have a max value and do we check it
			if (MaxValue > 0 & !string.IsNullOrWhiteSpace(e.NewTextValue) )
			{
				// Parse text
				if (int.TryParse(e.NewTextValue, out var value))
				{
					if (value > MaxValue)
					{
						IsValid = true;
						return;
					}
				}
				else
				{
					// Reset value
					Text = e.OldTextValue;
				}
			}

			IsValid = false;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Returns true if ... is valid.
		/// </summary>
		/// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
		public bool IsValid
		{
			get => (bool) GetValue(IsValidProperty);
			set => SetValue(IsValidProperty, value);
		}

		/// <summary>
		/// Gets or sets the maximum value.
		/// </summary>
		/// <value>The maximum value.</value>
		public int MaxValue
		{
			get => (int) GetValue(MaxValueProperty);
			set => SetValue(MaxValueProperty, value);
		}
		#endregion
	}
}