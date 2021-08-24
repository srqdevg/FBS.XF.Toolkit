using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Converters
{
	/// <summary>
	/// Value to Brush Converter.
	/// </summary>
	/// <seealso cref="Xamarin.Forms.IValueConverter" />
	public class ValueToBrushConverter : IValueConverter
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
			var converter = new ColorTypeConverter();

			if (value is string stringValue)
			{
				var index = valueList.IndexOf(stringValue);
				var color = index > -1 ? (Color) converter.ConvertFromInvariantString(colorList[index]) : Color.Default;
				return new SolidColorBrush(color);
			}

			return new SolidColorBrush(Color.Default);
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

		#region Properties
		/// <summary>
		/// Gets or sets the colors.
		/// </summary>
		/// <value>The colors.</value>
		public string Colors { set => colorList = value.Split(',').ToList(); }

		/// <summary>
		/// Gets or sets the values.
		/// </summary>
		/// <value>The values.</value>
		public string Values { set => valueList = value.Split(',').ToList(); }
		#endregion

		#region Fields
		private List<string> colorList;
		private List<string> valueList;
		#endregion
	}
}
