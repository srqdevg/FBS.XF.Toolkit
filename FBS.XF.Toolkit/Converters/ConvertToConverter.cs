using System;
using System.Globalization;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Converters
{
	/// <summary>
	/// Convert to Converter.
	/// </summary>
	/// <seealso cref="Xamarin.Forms.IValueConverter" />
	public class ConvertToConverter : IValueConverter
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
				if (!(parameter is string dataType) || !(value is string inputValue))
				{
					return value;
				}

				// Boolean
				if (dataType.Equals("bool", StringComparison.OrdinalIgnoreCase) ||
				    dataType.Equals("boolean", StringComparison.OrdinalIgnoreCase))
				{
					return bool.TryParse(inputValue, out var result) && result;
				}

				// Date
				if (dataType.Equals("date", StringComparison.OrdinalIgnoreCase) ||
				    dataType.Equals("datetime", StringComparison.OrdinalIgnoreCase))
				{
					if (DateTime.TryParse(inputValue, out var result))
					{
						return DateTime.MinValue;
					}
				}       
				
				// Integer
				if (dataType.Equals("int", StringComparison.OrdinalIgnoreCase) ||
				    dataType.Equals("integer", StringComparison.OrdinalIgnoreCase))
				{
					if (bool.TryParse(inputValue, out var result))
					{
						return 0;
					}
				}

				return inputValue;
			}
			catch
			{

				return value;
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
			return value;
		}
		#endregion
	}
}
