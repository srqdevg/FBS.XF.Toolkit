using System;
using System.Globalization;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Converters
{
	/// <summary>
	/// Greater Than Value Converter.
	/// </summary>
	/// <seealso cref="Xamarin.Forms.IValueConverter" />
	public class GreaterThanValueConverter : IValueConverter
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
			if (value is int v)
			{
				// Is parameter an int or string
				if (parameter is int ip)
				{
					var isGreaterThan = OrEqualTo ? v >= ip : v > ip;

					if (isGreaterThan && ExcludeValue != null && ip == ExcludeValue)
					{
						isGreaterThan = false;
					}

					return InvertReturnValue ? !isGreaterThan : isGreaterThan;
				}

				if (parameter is string sp)
				{
					if (int.TryParse(sp, out var isp))
					{
						var isGreaterThan = OrEqualTo ? v >= isp : v > isp;

						if (isGreaterThan && ExcludeValue != null && isp == ExcludeValue)
						{
							isGreaterThan = false;
						}


						return InvertReturnValue ? !isGreaterThan : isGreaterThan;
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
		/// Gets or sets a value indicating whether [or equal to].
		/// </summary>
		/// <value><c>true</c> if [or equal to]; otherwise, <c>false</c>.</value>
		public bool OrEqualTo { get; set; }

		/// <summary>
		/// Gets or sets the exclude value.
		/// </summary>
		/// <value>The exclude value.</value>
		public int? ExcludeValue { get; set; }
		#endregion
	}
}
