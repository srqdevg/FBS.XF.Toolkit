using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Converters
{
	/// <summary>
	/// Equal To Value Converter.
	/// </summary>
	/// <seealso cref="Xamarin.Forms.IValueConverter" />
	public class EqualToValueConverter  : IValueConverter
	{
		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="EqualToValueConverter" /> class.
		/// </summary>
		public EqualToValueConverter()
		{
			FalseValue = false;
			TrueValue = true;
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
			try
			{
				if (value != null)
				{
					// Is parameter an int or a string or ....
					if (value is int v && parameter is int p)
					{
						var isEqual = v.Equals(p);
						isEqual = InvertReturnValue ? !isEqual : isEqual;
						return isEqual ? TrueValue : FalseValue;
					}

					if (value is int va && parameter is string pvs)
					{
						var isEqual = pvs.Split(',').ToList().Any(pv => pv.Trim().Equals(va.ToString().Trim(), StringComparison.OrdinalIgnoreCase));
						isEqual = InvertReturnValue ? !isEqual : isEqual;
						return isEqual ? TrueValue : FalseValue;
					}

					if (value is string sv && parameter is int sp)
					{
						var isEqual = !sv.Equals(sp.ToString().Trim(), StringComparison.OrdinalIgnoreCase);
						isEqual = InvertReturnValue ? !isEqual : isEqual;
						return isEqual ? TrueValue : FalseValue;
					}

					if (value is string sva && parameter is string spvs)
					{
						var isEqual = spvs.Split(',').ToList().Any(spv => spv.Trim().Equals(sva.Trim(), StringComparison.OrdinalIgnoreCase));
						isEqual = InvertReturnValue ? !isEqual : isEqual;
						return isEqual ? TrueValue : FalseValue;
					}

					return value.ToString().Equals(parameter.ToString(), StringComparison.OrdinalIgnoreCase);
				}

				return FalseValue;
			}
			catch
			{

				return FalseValue;
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

		#region Properties
		/// <summary>
		/// Gets or sets the false value.
		/// </summary>
		/// <value>The false value.</value>
		public object FalseValue { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [invert return value].
		/// </summary>
		/// <value><c>true</c> if [invert return value]; otherwise, <c>false</c>.</value>
		public bool InvertReturnValue { get; set; }

		/// <summary>
		/// Gets or sets the true value.
		/// </summary>
		/// <value>The true value.</value>
		public object TrueValue { get; set; }
		#endregion
	}
}
