using System;
using System.Globalization;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Converters
{
	/// <summary>
	/// Trim Text Converter
	/// </summary>
	/// <seealso cref="IValueConverter" />
	public class TrimTextConverter : IValueConverter
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="TrimTextConverter"/> class.
		/// </summary>
		public TrimTextConverter()
		{
			// Set default limit to 255 characters
			MaximumCharacters = 255;
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
			var temp = value as string;

			if (string.IsNullOrWhiteSpace(temp))
			{
				return string.Empty;
			}

			if (parameter != null)
			{
				MaximumCharacters = System.Convert.ToInt32(parameter);
			}

			return temp.Length > MaximumCharacters ? $"{temp.Left(MaximumCharacters - 3)}..." : temp;
		}

		/// <summary>
		/// Converts a value.
		/// </summary>
		/// <param name="value">The value that is produced by the binding target.</param>
		/// <param name="targetType">The type to convert to.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
		/// <exception cref="NotImplementedException"></exception>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the maximum characters.
		/// </summary>
		/// <value>The maximum characters.</value>
		public int MaximumCharacters { get; set; }
		#endregion
	}
}