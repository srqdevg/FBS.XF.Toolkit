using System.Reflection;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Extensions
{
	/// <summary>
	/// Binding Extensions.
	/// </summary>
	/// <remarks>
	/// All rights reserved to 
	/// https://stackoverflow.com/questions/45340019/xamarin-forms-getbindingexpression
	/// </remarks>
	public static class BindingExtensions
	{
		#region Public methods
		/// <summary>
		/// Gets the binding.
		/// </summary>
		/// <param name="self">The self.</param>
		/// <param name="property">The property.</param>
		/// <returns>Binding.</returns>
		public static Binding GetBinding(this BindableObject self, BindableProperty property)
		{
			var methodInfo = typeof(BindableObject).GetTypeInfo().GetDeclaredMethod("GetContext");
			var context = methodInfo?.Invoke(self, new object[] { property });
			var propertyInfo = context?.GetType().GetTypeInfo().GetDeclaredField("Binding");
			return propertyInfo?.GetValue(context) as Binding;
		}
		#endregion
	}
}
