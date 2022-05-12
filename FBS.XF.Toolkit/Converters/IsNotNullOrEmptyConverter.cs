using System;
using System.Globalization;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Converters
{
	/// <summary>
	/// Is Not Null Or Empty Converter.
	/// </summary>
	/// <seealso cref="Xamarin.Forms.IValueConverter" />
	public class IsNotNullOrEmptyConverter : IValueConverter
	{
		#region Public methods
		/// <summary>
		/// Converts a value.
		/// </summary>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				if (value != null)
				{
					if (value is string stringValue)
					{
						return !string.IsNullOrWhiteSpace(stringValue);
					}

					if (value is DateTime)
					{
						var dateTimeValue = (DateTime?) value;
						return dateTimeValue.HasValue;
					}

					if (value is int)
					{
						var intValue = (int?) value;
						return intValue.HasValue;
					}

					if (value != null)
					{
						return true;
					}
				}

				return false;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Converts a value.
		/// </summary>
		/// <param name="value">The value that is produced by the binding target.</param>
		/// <param name="targetType">The type to convert to.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			// Do nothing
			return null;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets a value indicating whether [invert return value].
		/// </summary>
		/// <value><c>true</c> if [invert return value]; otherwise, <c>false</c>.</value>
		public bool InvertReturnValue { get; set; }
		#endregion
	}
}