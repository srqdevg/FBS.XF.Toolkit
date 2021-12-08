using System.ComponentModel;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Controls
{
	/// <summary>
	/// SegmentedControlOption.
	/// Implements the <see cref="Xamarin.Forms.View" />
	/// </summary>
	/// <seealso cref="Xamarin.Forms.View" />
	/// <remarks>
	/// All rights belong to https://github.com/1iveowl/Plugin.SegmentedControl
	/// MIT License
	/// I modified this control to include svg images in the 'segments'
	/// </remarks>
	[Preserve(AllMembers = true)]
	public class SegmentedControlOption : View
	{
		#region Bindable properties
		/// <summary>
		/// The item property
		/// </summary>
		public static readonly BindableProperty ItemProperty =
			BindableProperty.Create(nameof(Item), typeof(object), typeof(SegmentedControlOption),
				propertyChanged: (bindable, value, newValue) => ((SegmentedControlOption) bindable).OnItemChanged(value, newValue));

		/// <summary>
		/// The text property
		/// </summary>
		public static readonly BindableProperty TextProperty =
			BindableProperty.Create(nameof(Text), typeof(string), typeof(SegmentedControlOption), string.Empty);

		/// <summary>
		/// The text property name property
		/// </summary>
		public static readonly BindableProperty TextPropertyNameProperty =
			BindableProperty.Create(nameof(TextPropertyName), typeof(string), typeof(SegmentedControlOption));
		#endregion

		#region Override methods
		/// <summary>
		/// Method that is called when a bound property is changed.
		/// </summary>
		/// <param name="propertyName">The name of the bound property that changed.</param>
		/// <remarks>To be added.</remarks>
		protected override void OnPropertyChanged(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);

			if (propertyName == nameof(Item) || propertyName == nameof(TextPropertyName))
			{
				SetTextFromItemProperty();
			}
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Called when [item changed].
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="newValue">The new value.</param>
		private void OnItemChanged(object value, object newValue)
		{
			if (value is INotifyPropertyChanged mutableItem)
			{
				mutableItem.PropertyChanged -= OnItemPropertyChanged;
			}

			if (newValue is INotifyPropertyChanged newMutableItem)
			{
				newMutableItem.PropertyChanged += OnItemPropertyChanged;
			}
		}

		/// <summary>
		/// Handles the <see cref="E:ItemPropertyChanged" /> event.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
		private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == TextPropertyName)
			{
				SetTextFromItemProperty();
			}
		}

		/// <summary>
		/// Sets the text from item property.
		/// </summary>
		private void SetTextFromItemProperty()
		{
			if (Item != null && TextPropertyName != null)
				Text = Item.GetType().GetProperty(TextPropertyName)?.GetValue(Item)?.ToString();
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the item.
		/// </summary>
		/// <value>The item.</value>
		public object Item
		{
			get => GetValue(ItemProperty);
			set => SetValue(ItemProperty, value);
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

		/// <summary>
		/// Gets or sets the name of the text property.
		/// </summary>
		/// <value>The name of the text property.</value>
		public string TextPropertyName
		{
			get => (string) GetValue(TextPropertyNameProperty);
			set => SetValue(TextPropertyNameProperty, value);
		}
		#endregion
	}
}