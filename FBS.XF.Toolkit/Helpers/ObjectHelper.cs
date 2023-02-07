using System;
using System.Collections.Generic;

namespace FBS.XF.Toolkit.Helpers
{
	/// <summary>
	/// ObjectHelper.
	/// </summary>
	public static class ObjectHelper
	{
		#region Public methods
		/// <summary>
		/// Gets the value as date time.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="outValue">The out value.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		public static bool GetValueAsDateTime(object value, out DateTime? outValue)
		{
			// ReSharper disable once RedundantNullableTypeMark
			if (value is DateTime?)
			{
				outValue = (DateTime?) value;
				return true;
			}

			if (value is string stringValue)
			{
				if (!string.IsNullOrWhiteSpace(stringValue) && DateTime.TryParse(stringValue, out var fieldValue))
				{
					outValue = fieldValue;
					return true;
				}
			}

			outValue = null;
			return false;
		}

		/// <summary>
		/// Gets the value as decimal.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="outValue">The out value.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		public static bool GetValueAsDecimal(object value, out decimal? outValue)
		{
			// ReSharper disable once RedundantNullableTypeMark
			if (value is decimal?)
			{
				outValue = (decimal?) value;
				return true;
			}

			if (value is int? && ((int?) value).HasValue)
			{
				outValue = Convert.ToDecimal((int?) value);
				return true;
			}

			if (value is string stringValue)
			{
				if (!string.IsNullOrWhiteSpace(stringValue) && decimal.TryParse(stringValue, out var fieldValue))
				{
					outValue = fieldValue;
					return true;
				}
			}

			outValue = null;
			return false;
		}

		/// <summary>
		/// Gets the value as int.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="outValue">The out value.</param>
		/// <returns><c>true</c> if success, <c>false</c> otherwise.</returns>
		public static bool GetValueAsInt(object value, out int? outValue)
		{
			// ReSharper disable once RedundantNullableTypeMark
			if (value is int?)
			{
				outValue = (int?) value;
				return true;
			}

			if (value is string stringValue)
			{
				if (!string.IsNullOrWhiteSpace(stringValue) && int.TryParse(stringValue, out var fieldValue))
				{
					outValue = fieldValue;
					return true;
				}
			}

			outValue = null;
			return false;
		}

		/// <summary>
		/// Gets the value as int.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="outValue">The out value.</param>
		/// <returns><c>true</c> if success, <c>false</c> otherwise.</returns>
		public static bool GetValueAsString(object value, out string outValue)
		{
			// ReSharper disable once RedundantNullableTypeMark
			if (value is int?)
			{
				outValue = ((int?) value).ToString();
				return true;
			}

			if (value is string stringValue)
			{
				if (!string.IsNullOrWhiteSpace(stringValue))
				{
					outValue = stringValue;
					return true;
				}
			}

			outValue = null;
			return false;
		}

		/// <summary>
		/// Determines whether [is generic list] [the specified object].
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <returns><c>true</c> if [is generic list] [the specified object]; otherwise, <c>false</c>.</returns>
		public static bool IsGenericList(object obj)
		{
			var objectType = obj.GetType();
			return objectType.IsGenericType && (objectType.GetGenericTypeDefinition() == typeof(List<>));
		}   
		
		/// <summary>
		/// Determines whether [is instance of generic type] [the specified object].
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <param name="genericType">Type of the generic.</param>
		/// <returns><c>true</c> if [is instance of generic type] [the specified object]; otherwise, <c>false</c>.</returns>
		public static bool IsInstanceOfGenericType(object obj, Type genericType)
		{
			if (obj == null)
			{
				return false;
			}

			var type = obj.GetType();

			while (type != null)
			{
				if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType)
				{
					return true;
				}

				type = type.BaseType;
			}
			return false;
		}
		#endregion
	}
}
