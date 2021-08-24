using System;
using System.Globalization;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Converters
{
	/// <summary>
	/// Color To Brush Converter.
	/// Implements the <see cref="Xamarin.Forms.IValueConverter" />
	/// </summary>
	/// <seealso cref="Xamarin.Forms.IValueConverter" />
	public class ColorToBrushConverter : IValueConverter
	{
		#region IValueConverter methods
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
			return value is Color color ? new SolidColorBrush(color) : null;
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
			return value is SolidColorBrush brush ? brush.Color : (object) null;
		}
		#endregion
	}
}
