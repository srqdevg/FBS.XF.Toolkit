using System.Collections.Generic;
using PropertyChanged;

namespace FBS.XF.Toolkit.Models
{
	/// <summary>
	/// NamedCollection.
	/// Implements the <see cref="System.Collections.Generic.List{T}" />
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <seealso cref="System.Collections.Generic.List{T}" />
	[AddINotifyPropertyChangedInterface]
	public class NamedCollection<T> : List<T>
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="NamedCollection{T}"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="items">The items.</param>
		public NamedCollection(string name, List<T> items) : base(items)
		{
			Name = name;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; }
		#endregion
	}
}