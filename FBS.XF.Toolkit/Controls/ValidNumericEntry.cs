using System;
using System.Collections.Generic;
using System.Linq;
using PropertyChanged;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Controls
{
	/// <summary>
	/// Valid Numeric Entry.
	/// </summary>
	[AddINotifyPropertyChangedInterface]
	public class ValidNumericEntry : ValidEntry, IDisposable
	{
		#region Bindable properties
		/// <summary>
		/// The allow decimals property
		/// </summary>
		public static readonly BindableProperty AllowDecimalsProperty =
			BindableProperty.Create(nameof(AllowDecimals), typeof(bool), typeof(CustomButton), default(bool), BindingMode.TwoWay);

		/// <summary>
		/// The in error property
		/// </summary>
		public static readonly BindableProperty InErrorProperty =
			BindableProperty.Create(nameof(InError), typeof(bool), typeof(CustomButton), default(bool), BindingMode.TwoWay);

		/// <summary>
		/// The maximum value property
		/// </summary>
		public static readonly BindableProperty MaxValueProperty =
			BindableProperty.Create(nameof(MaxValue), typeof(int), typeof(CustomButton), default(int));

		/// <summary>
		/// The valid values property
		/// </summary>
		public static readonly BindableProperty ValidValuesProperty =
			BindableProperty.Create(nameof(ValidValues), typeof(List<int>), typeof(CustomButton));
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ValidNumericEntry"/> class.
		/// </summary>
		public ValidNumericEntry() : base(Keyboard.Numeric)
		{
			// Add Max Value validation
			TextChanged += ValidNumericEntry_TextChanged;
		}
		#endregion

		#region IDisposable
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public new void Dispose()
		{
			TextChanged -= ValidNumericEntry_TextChanged;
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
			if (!string.IsNullOrWhiteSpace(e.NewTextValue) )
			{
				if (AllowDecimals)
				{
					// Parse decimal text
					if (decimal.TryParse(e.NewTextValue, out var decimalValue))
					{
						if (MaxValue > 0)
						{
							InError = decimalValue > MaxValue;
						}

						return;
					}
				}
				
				// Parse integer text
				if (int.TryParse(e.NewTextValue, out var intValue))
				{
					if (MaxValue > 0)
					{
						InError = intValue > MaxValue;
						return;
					}

					if (ValidValues != null && ValidValues.Any())
					{
						InError = !ValidValues.Contains(intValue);
						return;
					}

					return;
				}
				
				// Reset value
				Text = e.OldTextValue;
			}

			InError = false;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the allow decimals.
		/// </summary>
		/// <value>The allow decimals.</value>
		public bool AllowDecimals
		{
			get => (bool)GetValue(AllowDecimalsProperty);
			set => SetValue(AllowDecimalsProperty, value);
		}

		/// <summary>
		/// Returns true if ... is valid.
		/// </summary>
		/// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
		public bool InError
		{
			get => (bool) GetValue(InErrorProperty);
			set => SetValue(InErrorProperty, value);
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

		/// <summary>
		/// Gets or sets the valid values.
		/// </summary>
		/// <value>The valid values.</value>
		public List<int> ValidValues
		{
			get => (List<int>) GetValue(ValidValuesProperty);
			set => SetValue(ValidValuesProperty, value);
		}
		#endregion
	}
}