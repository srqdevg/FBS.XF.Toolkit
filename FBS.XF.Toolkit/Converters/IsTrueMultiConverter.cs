using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Converters
{
	/// <summary>
	/// Is True MultiConverter
	/// </summary>
	/// <seealso cref="Xamarin.Forms.IMultiValueConverter" />
	/// <remarks>
	/// Implements the <see cref="Xamarin.Forms.IMultiValueConverter" />
	/// https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/data-binding/multibinding
	/// </remarks>
	public class IsTrueMultiConverter : IMultiValueConverter
	{
		#region Public methods
		/// <summary>
		/// To be added.
		/// </summary>
		/// <param name="values">To be added.</param>
		/// <param name="targetType">To be added.</param>
		/// <param name="parameter">To be added.</param>
		/// <param name="culture">To be added.</param>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values == null || !targetType.IsAssignableFrom(typeof(bool)))
			{
				return false; // Alternatively, return BindableProperty.UnsetValue to use the binding FallbackValue
			}

			if (AnyValueTrue)
			{
				foreach (var value in values)
				{
					if (!(value is bool b))
					{
						continue;
					}

					if (b)
					{
						return true;
					}
				}

				return false;
			}

			foreach (var value in values)
			{
				if (!(value is bool b))
				{
					return false; // Alternatively, return BindableProperty.UnsetValue to use the binding FallbackValue
				}

				if (!b)
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// To be added.
		/// </summary>
		/// <param name="value">To be added.</param>
		/// <param name="targetTypes">To be added.</param>
		/// <param name="parameter">To be added.</param>
		/// <param name="culture">To be added.</param>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			if (!(value is bool b) || targetTypes.Any(t => !t.IsAssignableFrom(typeof(bool))))
			{
				// Return null to indicate conversion back is not possible
				return null;
			}

			if (b)
			{
				return targetTypes.Select(t => (object) true).ToArray();
			}
			
			// Can't convert back from false because of ambiguity
			return null;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets a value indicating whether [any value true].
		/// </summary>
		/// <value><c>true</c> if [any value true]; otherwise, <c>false</c>.</value>
		public bool AnyValueTrue { get; set; }
		#endregion
	}
}
