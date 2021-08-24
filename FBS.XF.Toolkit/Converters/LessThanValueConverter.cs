using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Converters
{
	/// <summary>
	/// Less Than Value Converter.
	/// </summary>
	/// <seealso cref="Xamarin.Forms.IValueConverter" />
	public class LessThanValueConverter : IValueConverter
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
			if (value != null && value is int v)
			{
				// Is parameter an int or string
				if (parameter is int ip)
				{
					var isEqual = OrEquals ? v <= ip : v < ip;
					return InvertReturnValue ? !isEqual : isEqual;
				}

				if (parameter is string sp)
				{
					if (int.TryParse(sp, out var isp))
					{
						var isEqual = OrEquals ? v <= isp : v < isp;
						return InvertReturnValue ? !isEqual : isEqual;
					}
				}
			}

			return false;
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

		#region Properties
		/// <summary>
		/// Gets or sets a value indicating whether [invert return value].
		/// </summary>
		/// <value><c>true</c> if [invert return value]; otherwise, <c>false</c>.</value>
		public bool InvertReturnValue { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [or equals].
		/// </summary>
		/// <value><c>true</c> if [or equals]; otherwise, <c>false</c>.</value>
		public bool OrEquals { get; set; }
		#endregion
	}
}
