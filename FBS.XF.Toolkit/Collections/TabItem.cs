using Xamarin.Forms;

namespace FBS.XF.Toolkit.Collections
{
	/// <summary>
	/// TabItem.
	/// </summary>
	public class TabItem : BindableObject
	{
		#region Bindable properties
		/// <summary>
		/// The items property
		/// </summary>
		public static readonly BindableProperty IdProperty =
			BindableProperty.Create(nameof(Id), typeof(string), typeof(TabItem));

		/// <summary>
		/// The items property
		/// </summary>
		public static readonly BindableProperty IsVisibleProperty =
			BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(TabItem), true);

		/// <summary>
		/// The items property
		/// </summary>
		public static readonly BindableProperty TextProperty =
			BindableProperty.Create(nameof(Text), typeof(string), typeof(TabItem));
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="TabItem"/> class.
		/// </summary>
		public TabItem()
		{
			IsVisible = true;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TabItem"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="text">The text.</param>
		/// <param name="isVisible">if set to <c>true</c> [is visible].</param>
		public TabItem(string id, string text, bool isVisible = true)
		{
			Id = id;
			Text = text;
			IsVisible = isVisible;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public string Id 
		{
			get => (string) GetValue(IdProperty) ;
			set => SetValue(IdProperty, value);
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is visible.
		/// </summary>
		/// <value><c>true</c> if this instance is visible; otherwise, <c>false</c>.</value>
		public bool IsVisible
		{
			get => (bool) GetValue(IsVisibleProperty);
			set => SetValue(IsVisibleProperty, value);
		}

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text 
		{
			get => (string) GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}
		#endregion
	}
}
