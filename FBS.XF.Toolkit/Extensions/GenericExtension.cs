using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace FBS.XF.Toolkit.Extensions
{
	/// <summary>
	/// Generic Extension.
	/// </summary>
	public static class GenericExtension
	{
		#region Public methods
		/// <summary>
		/// Compares the specified object1.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="object1">The object1.</param>
		/// <param name="object2">The object2.</param>
		/// <returns><c>true</c> if equal then true, <c>false</c> otherwise.</returns>
		public static bool Compare<T>(this T object1, T object2)
		{
			// Get the type of the object
			var type = typeof(T);

			// Return false if any of the object is false
			if (object1 == null || object2 == null)
			{
				return false;
			}

			// Loop through each properties inside class and get values for the property from both the objects and compare
			foreach (var property in type.GetProperties())
			{
				if (property.Name != "ExtensionData")
				{
					// Check if the property is in System.***
					if (!property.PropertyType.FullName!.StartsWith("System."))
					{
						continue;
					}

					if (Attribute.IsDefined(property, typeof(NotMappedAttribute)))
					{
						continue;
					}

					var object1Value = string.Empty;
					var object2Value = string.Empty;

					// ReSharper disable PossibleNullReferenceException
					if (property.GetValue(object1, null) != null)
					{
						object1Value = property.GetValue(object1, null).ToString();
					}

					if (property.GetValue(object2, null) != null)
					{
						object2Value = property.GetValue(object2, null).ToString();
					}

					// ReSharper restore PossibleNullReferenceException
					if (object1Value.Trim() != object2Value.Trim())
					{
						return false;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// Copies to.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="object1">The object1.</param>
		/// <param name="object2">The object2.</param>
		public static void CopyTo<T>(this T object1, T object2)
		{
			// Get the type of the object
			var type = typeof(T);

			// Return false if any of the object is false
			if (object1 != null && object2 != null)
			{
				// Loop through each properties inside class and get values for the property from both the objects and compare
				foreach (var property in type.GetProperties())
				{
					property.SetValue(object2, property.GetValue(object1, null));
				}
			}
		}

		/// <summary>
		/// Updates the data.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dbset">The dbset.</param>
		/// <param name="currentList">The current list.</param>
		/// <param name="keyFields">The key fields.</param>
		public static void UpdateData<T>(this DbSet<T> dbset, List<T> currentList, params string[] keyFields) where T : class
		{
			var type = typeof(T);

			// Indentify key fields
			var propertyFields = keyFields.Select(k => type.GetProperty(k)).ToList();

			// Iterate list
			foreach (var item in dbset)
			{
				// Get key field values
				var keyValues = propertyFields.Select(p => p!.GetValue(item, null)).ToList();
				T toCompareItem = null;

				foreach (var compareItem in currentList)
				{
					var compareKeyValues = propertyFields.Select(p => p!.GetValue(compareItem, null)).ToList();

					if (compareKeyValues.OrderBy(t => t).SequenceEqual(keyValues.OrderBy(t => t)))
					{
						toCompareItem = compareItem;
						break;
					}
				}

				if (toCompareItem == null)
				{
					// It's deleted, so.....
					dbset.Remove(item);
					continue;
				}

				// Any differences
				if (!item.Compare(toCompareItem))
				{
					// Different, so copy properties
					toCompareItem.CopyTo(item);
				}
				
				// Remove from list
				currentList.Remove(toCompareItem);
			}

			// Whats left is new
			dbset.AddRange(currentList);
		}

		/// <summary>
		/// Updates the database set.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dbset">The dbset.</param>
		/// <param name="entity">The entity.</param>
		/// <param name="currentList">The current list.</param>
		/// <param name="expression">The expression.</param>
		/// <returns>T.</returns>
		public static T UpdateDbSet<T>(this DbSet<T> dbset, T entity, List<T> currentList, Func<T, bool> expression)
			where T : class
		{
			var item = currentList.FirstOrDefault(expression);

			if (item == null)
			{
				dbset.Add(entity);
				currentList.Add(entity);
			}

			return null;
		}
		#endregion
	}
}
