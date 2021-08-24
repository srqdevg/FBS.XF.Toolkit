using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Converters
{
	/// <summary>
	/// Value to Name Converter.
	/// </summary>
	/// <seealso cref="Xamarin.Forms.IValueConverter" />
	public class ValueToNameConverter : IValueConverter
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
			if (value != null)
			{
				if (value is string stringValue)
				{
					var index = valueList.IndexOf(stringValue);
					return index == -1 ? string.Empty : namesList[index];
				}

				if (value is int intValue)
				{
					var index = valueList.IndexOf(intValue.ToString());
					return index == -1 ? string.Empty : namesList[index];
				}
			}

			return null;
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
		/// Sets the names.
		/// </summary>
		/// <value>The names.</value>
		public string Names { set => namesList = value.Split(',').ToList(); }

		/// <summary>
		/// Gets or sets the values.
		/// </summary>
		/// <value>The values.</value>
		public string Values { set => valueList = value.Split(',').ToList(); }
		#endregion

		#region Fields
		private List<string> namesList;
		private List<string> valueList;
		#endregion
	}
}
