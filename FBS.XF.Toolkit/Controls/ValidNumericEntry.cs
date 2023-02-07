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
		/// The decimal places property
		/// </summary>
		public static readonly BindableProperty DecimalPlacesProperty =
			BindableProperty.Create(nameof(DecimalPlaces), typeof(int), typeof(CustomButton), default(int), BindingMode.OneWay,
				propertyChanged: (bd, ov, nv) => ((ValidNumericEntry) bd).DecimalPlacesPropertyChanged(ov, nv));

		/// <summary>
		/// The decimal types property
		/// </summary>
		public static readonly BindableProperty DecimalTypeProperty =
			BindableProperty.Create(nameof(DecimalType), typeof(DecimalTypes), typeof(CustomButton), default, BindingMode.OneWay,
			propertyChanged: (bd, ov, nv) => ((ValidNumericEntry) bd).DecimalTypePropertyChanged(ov, nv));

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
		/// Decimals the places property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void DecimalPlacesPropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue && newValue != null)
			{
				decimalPlacesFormat = $"{0}.{new string('0', (int) newValue)}";
			}
		}

		/// <summary>
		/// Decimals the type property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void DecimalTypePropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue && newValue != null)
			{
				switch ((DecimalTypes) newValue)
				{
					case DecimalTypes.Halves:
					case DecimalTypes.Tenths:
						DecimalPlaces = 1;
						break;
					case DecimalTypes.Quarters:
						DecimalPlaces = 2;
						break;
					default:
						DecimalPlaces = 0;
						break;
				}
			}
		}

		/// <summary>
		/// Valids the numeric entry text changed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="Xamarin.Forms.TextChangedEventArgs"/> instance containing the event data.</param>
		private void ValidNumericEntry_TextChanged(object sender, TextChangedEventArgs e)
		{
			// Nothing?
			if (e.NewTextValue == null)
			{
				return;
			}

			// Spaces?
			if (e.NewTextValue.Contains(' '))
			{
				Text = e.NewTextValue.Trim();
			}

			// Do we have a max value and do we check it
			if (!string.IsNullOrWhiteSpace(e.NewTextValue))
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

						if (DecimalPlaces > 0 && e.NewTextValue.Contains("."))
						{
							var places = e.NewTextValue.Split('.');

							switch (DecimalType)
							{
								case DecimalTypes.Halves:
									// Must be at most one character after the decimal and character must be 5
									if (places[^1].Length > 1 || 
									    (places[^1].Length == 1 && !"05".Contains(places[^1])))
									{
										// Reset value
										Text = e.OldTextValue;
									}

									break;
								case DecimalTypes.Tenths:
									// Must be at most one character after the decimal, any value allowed
									if (places[^1].Length > 1)
									{
										// Reset value
										Text = e.OldTextValue;
									}

									break;
								case DecimalTypes.Quarters:
									// Must be at most twos character after the decimal, only 25,5,50 and 75 allowed
									if (places[^1].Length > 2 ||
									    (places[^1].Length == 1 && places[^1] != "0") ||
									    (places[^1].Length == 1 && places[^1] != "5") ||
									    (places[^1].Length == 2 && places[^1] != "25" || places[^1] != "50" || places[^1] != "75"))
									{
										// Reset value
										Text = e.OldTextValue;
									}

									break;
								default:
									if (places[^1].Length > DecimalPlaces)
									{
										Text = Math.Round(decimalValue, DecimalPlaces).ToString(decimalPlacesFormat);
									}

									break;
							}
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

						if (InError)
						{
							return;
						}
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
		/// Gets or sets the decimal places.
		/// </summary>
		/// <value>The decimal places.</value>
		public int DecimalPlaces
		{
			get => (int) GetValue(DecimalPlacesProperty);
			set => SetValue(DecimalPlacesProperty, value);
		}

		/// <summary>
		/// Gets or sets the type of the decimal.
		/// </summary>
		/// <value>The type of the decimal.</value>
		public DecimalTypes DecimalType
		{
			get => (DecimalTypes) GetValue(DecimalTypeProperty);
			set => SetValue(DecimalTypeProperty, value);
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

		#region Fields
		private string decimalPlacesFormat;
		#endregion
	}
}