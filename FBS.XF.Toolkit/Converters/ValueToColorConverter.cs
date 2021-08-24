using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Converters
{
	/// <summary>
	/// Value to Color Converter.
	/// </summary>
	/// <seealso cref="Xamarin.Forms.IValueConverter" />
	public class ValueToColorConverter : IValueConverter
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
			if (value is string stringValue)
			{
				var index = valueList.IndexOf(stringValue);

				if (index > -1)
				{
					var color = colorList[index];
					return color.StartsWith("#") ? Color.FromHex(color.Substring(1)) : new ColorTypeConverter().ConvertFromInvariantString(color);
				}
			}

			if (colorList.Count > valueList.Count)
			{
				var color = colorList[^1];
				return color.StartsWith("#") ? Color.FromHex(color.Substring(1)) : new ColorTypeConverter().ConvertFromInvariantString(color);
			}

			return Color.Default;
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
