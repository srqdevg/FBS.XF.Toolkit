using System;
using System.Globalization;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Converters
{
	/// <summary>
	/// Boolean To Object Converter.
	/// Implements the <see cref="Xamarin.Forms.IValueConverter" />
	/// </summary>
	/// <seealso cref="Xamarin.Forms.IValueConverter" />
	public class BoolToObjectConverter : IValueConverter
	{
		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="BoolToObjectConverter"/> class.
		/// </summary>
		public BoolToObjectConverter()
		{
			FalseObject = null;
			TrueObject = null;
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
			if (value == null)
			{
				return null;
			}

			if (value is bool temp)
			{
				return temp ? TrueObject : FalseObject;
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
			if (value is { } result)
				return result.Equals(TrueObject);

			if (value == null && TrueObject == null)
				return true;

			return value;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the false object.
		/// </summary>
		/// <value>The false object.</value>
		public object FalseObject { get; set; }

		/// <summary>
		/// Gets or sets the true object.
		/// </summary>
		/// <value>The true object.</value>
		public object TrueObject { get; set; }
		#endregion
	}
}