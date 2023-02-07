// ReSharper disable once CheckNamespace
namespace System
{
	/// <summary>
	/// Object Extension.
	/// </summary>
	public static class ObjectExtension
	{
		#region Public methods
		/// <summary>
		/// Converts to int32.
		/// </summary>
		/// <returns>System.Nullable&lt;System.Int32&gt;.</returns>
		public static decimal? ToDecimal(this object value)
		{
			if (value != null)
			{
				if (value is decimal value1)
				{
					return value1;
				}

				if (value is string stringValue && decimal.TryParse(stringValue, out var value2))
				{
					return value2;
				}
			}

			return null;
		}
		#endregion
	}
}
