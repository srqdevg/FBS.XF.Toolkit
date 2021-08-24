using System;
using System.Collections.Generic;
using System.Linq;
using FBS.XF.Toolkit.Models;

namespace FBS.XF.Toolkit.Helpers
{
	/// <summary>
	/// Enum Helper.
	/// </summary>
	public class EnumHelper
	{
		#region Public methods
		/// <summary>
		/// Gets the enum descriptions.
		/// </summary>
		/// <param name="enumType">Type of the enum.</param>
		/// <param name="value">The value.</param>
		/// <param name="splitCamelCase">if set to <c>true</c> [split camel case].</param>
		/// <returns>System.String.</returns>
		public static string GetEnumDescription(Type enumType, object value, bool splitCamelCase = true)
		{
			// ReSharper disable once PossibleNullReferenceException
			return GetEnumDescriptions(enumType, splitCamelCase).FirstOrDefault(e => e.Id.Equals(((int) value).ToString())).Text;
		}

		/// <summary>
		/// Gets the enum descriptions.
		/// </summary>
		/// <param name="enumType">Type of the enum.</param>
		/// <param name="splitCamelCase">if set to <c>true</c> [split camel case].</param>
		/// <returns>List&lt;IdText&gt;.</returns>
		public static List<IdText> GetEnumDescriptions(Type enumType, bool splitCamelCase = true)
		{
			return splitCamelCase 
				? Enum.GetValues(enumType).Cast<object>().Select(v => new IdText(((int) v).ToString(), Enum.GetName(enumType, v).SplitCamelCase())).ToList() 
				: Enum.GetValues(enumType).Cast<object>().Select(v => new IdText(((int) v).ToString(), Enum.GetName(enumType, v))).ToList();
		}
		#endregion
	}
}
