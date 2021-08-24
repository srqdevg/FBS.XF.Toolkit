using System;
using System.Globalization;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Converters
{
	/// <summary>
	/// Boolean Inverter Converter
	/// </summary>
	public class BooleanInverterConverter : IValueConverter
	{
		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="BooleanInverterConverter"/> class.
		/// </summary>
		public BooleanInverterConverter()
		{
			NullValue = null;
		}
		#endregion

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
			if (value == null && NullValue != null)
			{
				return System.Convert.ToBoolean(NullValue);
			}

			if (value is bool temp)
			{
				return !temp;
			}

			return value;
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
			if (value is bool temp)
			{
				return !temp;
			}

			return value;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the mask.
		/// </summary>
		/// <value>The mask.</value>
		public string NullValue { get; set; }
		#endregion
	}
}