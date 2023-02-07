using System.Linq;
using Xamarin.Forms.Internals;

// ReSharper disable once CheckNamespace
namespace System
{
	/// <summary>
	/// Class Extension.
	/// </summary>
	public static class ClassExtension
	{
		#region Public methods
		/// <summary>
		/// Copies the properties.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="copyFrom">The copy from.</param>
		/// <returns>T.</returns>
		/// <exception cref="System.ArgumentNullException">copyFrom</exception>
		public static T CopyProperties<T>(this T copyFrom)
		{
			// Check args
			if (copyFrom == null)
			{
				throw new ArgumentNullException(nameof(copyFrom));
			}

			// Create new instance (must have default constructor)
			var copyTo = (T)Activator.CreateInstance(typeof(T));

			// Iterate all properties and copy values
			copyFrom.GetType()
				.GetProperties()
				.Where(prop => prop.CanRead && prop.CanWrite)
				.ForEach(p => p.SetValue(copyTo, p.GetValue(copyFrom)));

			return copyTo;
		}

		/// <summary>
		/// Copies the properties.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="copyFrom">The copy from.</param>
		/// <param name="copyTo">The copy to.</param>
		/// <returns>T.</returns>
		/// <exception cref="ArgumentNullException">copyFrom</exception>
		public static T CopyProperties<T>(this T copyFrom, T copyTo)
		{
			// Check args
			if (copyFrom == null)
			{
				throw new ArgumentNullException(nameof(copyFrom));
			}

			// Iterate all properties and copy values
			copyFrom.GetType()
				.GetProperties()
				.Where(prop => prop.CanRead && prop.CanWrite && !prop.GetGetMethod().IsVirtual)
				.ForEach(p => p.SetValue(copyTo, p.GetValue(copyFrom)));

			return copyTo;
		}

		/// <summary>
		/// Sets the named property.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="updateObject">The update object.</param>
		/// <param name="name">The name.</param>
		/// <param name="value">The value.</param>
		/// <exception cref="ArgumentNullException">
		/// updateObject
		/// or
		/// name
		/// </exception>
		public static void SetNamedProperty<T>(this T updateObject, string name, string value)
		{
			// Check args
			if (updateObject == null)
			{
				throw new ArgumentNullException(nameof(updateObject));
			}

			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentNullException(nameof(name));
			}

			// Get type, property
			var type = typeof(T);
			var property = type.GetProperty(name);

			// Set value
			if (property != null)
			{
				property.SetValue(type, value, null);
			}
		}
		#endregion
	}
}
