using System;
using System.Globalization;
using FBS.XF.Toolkit.Helpers;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Converters
{
	/// <summary>
	/// Enum To Text Converter.
	/// </summary>
	/// <seealso cref="Xamarin.Forms.IValueConverter" />
	public class EnumToTextConverter : IValueConverter
	{
		#region Public methods
		/// <summary>
		/// Implement this method to convert <paramref name="value" /> to <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="targetType">The type to which to convert the value.</param>
		/// <param name="parameter">A parameter to use during the conversion.</param>
		/// <param name="culture">The culture to use during the conversion.</param>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (parameter != null && bool.TryParse(parameter.ToString(), out var result))
			{
				return EnumHelper.GetEnumDescription(value.GetType(), value, result);
			}

			return EnumHelper.GetEnumDescription(value.GetType(), value);
		}

		/// <summary>
		/// Implement this method to convert <paramref name="value" /> back from <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="targetType">The type to which to convert the value.</param>
		/// <param name="parameter">A parameter to use during the conversion.</param>
		/// <param name="culture">The culture to use during the conversion.</param>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}	
		#endregion
	}
}
