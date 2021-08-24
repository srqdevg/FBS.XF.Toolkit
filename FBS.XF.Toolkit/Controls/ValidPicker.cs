using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using FBS.XF.Toolkit.Extensions;
using FBS.XF.Toolkit.Interfaces;
using FluentValidation.Results;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Controls
{
	/// <summary>
	/// Valid Picker.
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Grid" />
	public class ValidPicker : Grid
	{
		#region Bindable properties
		/// <summary>
		/// The font size property
		/// </summary>
		public static readonly BindableProperty FontSizeProperty =
			BindableProperty.Create(nameof(FontSize), typeof(double), typeof(ValidPicker), default(double),
				propertyChanged: FontSizePropertyChanged);

		/// <summary>
		/// The item display path property
		/// </summary>
		public static readonly BindableProperty ItemDisplayPathProperty =
			BindableProperty.Create(nameof(ItemDisplayPath), typeof(string), typeof(ValidPicker), default(string),
				propertyChanged: ItemDisplayPathPropertyChanged);

		/// <summary>
		/// The items source property
		/// </summary>
		public static readonly BindableProperty ItemsSourceProperty =
			BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(ValidPicker), default(IList),
				propertyChanged: ItemsSourcePropertyChanged);

		/// <summary>
		/// The label property
		/// </summary>
		public static readonly BindableProperty LabelProperty =
			BindableProperty.Create(nameof(Label), typeof(string), typeof(ValidPicker), default(string),
				propertyChanged: LabelPropertyChanged);

		/// <summary>
		/// The label font attributes property
		/// </summary>
		public static readonly BindableProperty LabelFontAttributesProperty =
			BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(ValidPicker), default(FontAttributes),
				propertyChanged: LabelFontAttributesPropertyChanged);

		/// <summary>
		/// The numeric maximum property
		/// </summary>
		public static readonly BindableProperty NumericMaximumProperty =
			BindableProperty.Create(nameof(NumericMaximum), typeof(int), typeof(ValidPicker), default(int),
				propertyChanged: NumericMaximumPropertyChanged);

		/// <summary>
		/// The numeric minimum property
		/// </summary>
		public static readonly BindableProperty NumericMinimumProperty =
			BindableProperty.Create(nameof(NumericMinimum), typeof(int), typeof(ValidPicker), default(int),
				propertyChanged: NumericMinimumPropertyChanged);

		/// <summary>
		/// The optional label text color property
		/// </summary>
		public static readonly BindableProperty OptionalLabelTextColorProperty =
			BindableProperty.Create(nameof(OptionalLabelTextColor), typeof(string), typeof(ValidPicker), default(string),
				propertyChanged: OptionalLabelTextColorPropertyChanged);

		/// <summary>
		/// The text property
		/// </summary>
		public static readonly BindableProperty PlaceholderProperty =
			BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(ValidPicker), default(string),
				propertyChanged: PlaceholderPropertyChanged);

		/// <summary>
		/// The selected index property
		/// </summary>
		public static readonly BindableProperty SelectedIndexProperty =
			BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(ValidPicker), default(int), BindingMode.TwoWay,
				propertyChanged: SelectedIndexPropertyChanged);

		/// <summary>
		/// The selected item property
		/// </summary>
		public static readonly BindableProperty SelectedItemProperty =
			BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(ValidPicker), null, BindingMode.TwoWay,
				propertyChanged: SelectedItemPropertyChanged);

		/// <summary>
		/// The show error message visible property
		/// </summary>
		public static readonly BindableProperty ShowErrorMessageProperty =
			BindableProperty.Create(nameof(ShowErrorMessage), typeof(bool), typeof(ValidPicker), default(bool),
				BindingMode.TwoWay, propertyChanged: ShowErrorMessageVisiblePropertyChanged);

		/// <summary>
		/// The show optional text property
		/// </summary>
		public static readonly BindableProperty ShowOptionalTextProperty =
			BindableProperty.Create(nameof(ShowOptionalText), typeof(bool), typeof(ValidPicker), default(bool),
				BindingMode.TwoWay, propertyChanged: ShowOptionalTextPropertyChanged);

		/// <summary>
		/// The text color property
		/// </summary>
		public static readonly BindableProperty TextColorProperty =
			BindableProperty.Create(nameof(TextColor), typeof(string), typeof(ValidPicker), default(string),
				propertyChanged: TextColorPropertyChanged);

		/// <summary>
		/// The title property
		/// </summary>
		public static readonly BindableProperty TitleProperty =
			BindableProperty.Create(nameof(Title), typeof(string), typeof(ValidPicker), default(string),
				BindingMode.TwoWay, propertyChanged: TitlePropertyChanged);

		/// <summary>
		/// The validate field property
		/// </summary>
		public static readonly BindableProperty ValidateFieldProperty =
			BindableProperty.Create(nameof(ValidateField), typeof(bool), typeof(ValidPicker), default(bool),
				BindingMode.OneWay, propertyChanged: ValidateFieldPropertyChanged);
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ValidPicker" /> class.
		/// </summary>
		public ValidPicker()
		{
			// Add entry
			if (picker == null)
			{
				CreateControl();
			}
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Creates the optional control.
		/// </summary>
		private void CreateOptionalControl()
		{
			if (ShowOptionalText)
			{
				// Add help image
				if (optionalLabel == null && label != null)
				{
					optionalLabel = new Label
					{
						FontAttributes = FontAttributes.Italic,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						Text= "Optional",
						TextColor = OptionalLabelTextColor
					};

					// Add item to grid
					SetGridRowColumn(optionalLabel, 0, 1);
					Children.Add(optionalLabel);
				}
			}
		}

		/// <summary>
		/// Creates the control.
		/// </summary>
		private void CreateControl()
		{
			// Add rows and columns to grid
			RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});
			ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Auto});
			ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Auto});
			ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Star});

			// Add entry field
			picker = new Picker
			{
				BindingContext = this,
				Margin = new Thickness(0, -5, 0, 0)
			};

			picker.Unfocused += Picker_Unfocused;
			picker.SelectedIndexChanged += Picker_SelectedIndexChanged;
			SetGridRowColumn(picker, 0, 0, 0, 3);
			Children.Add(picker);
		}

		/// <summary>
		/// Creates the numeric range.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="minimumValue">The minimum value.</param>
		/// <param name="maximumValue">The maximum value.</param>
		private static void CreateNumericRange(BindableObject bindable, int minimumValue, int maximumValue)
		{
			if (maximumValue > 0 && minimumValue < maximumValue)
			{
				var range = Enumerable.Range(minimumValue, maximumValue).ToList();
				ItemsSourcePropertyChanged(bindable, null, range);
			}
		}

		/// <summary>
		/// Font size property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void FontSizePropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			((ValidPicker) bindable).picker.FontSize = (double) newValue;
		}

		/// <summary>
		/// Item display path property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void ItemDisplayPathPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			((ValidPicker) bindable).picker.ItemDisplayBinding = new Binding((string) newValue, BindingMode.OneWay);
		}

		/// <summary>
		/// Items property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void ItemsSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			((ValidPicker) bindable).picker.ItemsSource = (IList) newValue;
		}

		/// <summary>
		/// Labels the property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void LabelPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue != null || oldValue != null)
			{
				var control = (ValidPicker) bindable;

				// Add the label
				if (control.label == null)
				{
					// Add extra row
					control.RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});

					// Adjust other controls
					foreach (var childControl in control.Children)
					{
						SetRow(childControl, GetRow(childControl) + 1);
					}

					// Now create text label
					control.label = new Label
					{
						FontAttributes = control.FontAttributes,
						Margin = new Thickness(3, 0, 0, 0),
						Text = (string) newValue
					};

					control.SetGridRowColumn(control.label, 0, 0);
					control.Children.Add(control.label);

					// Do we need optional?
					if (control.ShowOptionalText)
					{
						control.CreateOptionalControl();
					}
				}
			}
		}

		/// <summary>
		/// Labels the font attributes property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void LabelFontAttributesPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue != null || oldValue != null)
			{
				var control = (ValidPicker) bindable;

				if (control.label != null)
				{
					control.label.FontAttributes = control.FontAttributes;
				}
			}
		}

		/// <summary>
		/// Numerics the maximum property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void NumericMaximumPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue != null && newValue != oldValue)
			{
				CreateNumericRange(bindable, ((ValidPicker) bindable).NumericMinimum, (int) newValue);
			}
		}

		/// <summary>
		/// Numerics the minimum property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void NumericMinimumPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue != null && newValue != oldValue)
			{
				CreateNumericRange(bindable, (int) newValue, ((ValidPicker) bindable).NumericMinimum);
			}
		}

		/// <summary>
		/// Optionals the label text color property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void OptionalLabelTextColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue != null && newValue != oldValue)
			{
				if (((ValidPicker) bindable).optionalLabel != null)
				{
					((ValidPicker) bindable).optionalLabel.TextColor = (Color) newValue;
				}
			}
		}

		/// <summary>
		/// Handles the SelectedIndexChanged event of the Picker control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
		/// <exception cref="NotImplementedException"></exception>
		private void Picker_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Update
			if (((Picker) sender).SelectedItem != null)
			{
				SelectedItem = ((Picker) sender).SelectedItem;
			}
		}

		/// <summary>
		/// Handles the Unfocused event of the Picker control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="FocusEventArgs" /> instance containing the event data.</param>
		private void Picker_Unfocused(object sender, FocusEventArgs e)
		{
			if (ValidateField)
			{
				// Validate the field
				var propertyName = ValidatePropertyName(this);
				viewModel?.ValidateCommand?.Execute(propertyName);
			}
		}

		/// <summary>
		/// Placeholder property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void PlaceholderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			((ValidPicker) bindable).picker.Title = newValue.ToString();
		}

		/// <summary>
		/// Selecteds the index property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void SelectedIndexPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (ValidPicker) bindable;
			control.picker.SelectedIndex = (int) newValue;
		}

		/// <summary>
		/// Selecteds the item property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void SelectedItemPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (ValidPicker) bindable;
			control.picker.SelectedItem = newValue;
		}

		/// <summary>
		/// Sets the grid row column.
		/// </summary>
		/// <param name="control">The control.</param>
		/// <param name="row">The row.</param>
		/// <param name="column">The column.</param>
		/// <param name="rowSpan">The row span.</param>
		/// <param name="columnSpan">The column span.</param>
		protected void SetGridRowColumn(BindableObject control, int row, int column, int rowSpan = 0, int columnSpan = 0)
		{
			SetRow(control, row);

			if (rowSpan > 0)
			{
				SetRowSpan(control, rowSpan);
			}

			SetColumn(control, column);

			if (columnSpan > 0)
			{
				SetColumnSpan(control, columnSpan);
			}
		}

		/// <summary>
		/// ShowErrorMessage property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void ShowErrorMessageVisiblePropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			((ValidPicker) bindable).errorLabel.IsVisible = (bool) newValue;
		}

		/// <summary>
		/// Shows the optional text property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void ShowOptionalTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if ((bool) newValue)
			{
				((ValidPicker) bindable).CreateOptionalControl();
			}
		}

		/// <summary>
		/// Texts the color property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void TextColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue != null && newValue != oldValue)
			{
				if (((ValidPicker) bindable).picker != null)
				{
					((ValidPicker) bindable).picker.TextColor = (Color) newValue;
				}
			}
		}   
		
		/// <summary>
		/// Titles the property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void TitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue != null || oldValue != null)
			{
				((ValidPicker) bindable).picker.Title = (string) newValue;
			}
		}

		/// <summary>
		/// Handles the ErrorsChanged event of the ValidateField control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="args">The <see cref="System.ComponentModel.DataErrorsChangedEventArgs" /> instance containing the event data.</param>
		private void ValidateField_ErrorsChanged(object sender, DataErrorsChangedEventArgs args)
		{
			// Get the first error for this property
			var error = ((INotifyDataErrorInfo) sender).GetErrors(ValidatePropertyName(this))?.Cast<ValidationFailure>().FirstOrDefault();

			// Do we have an error?
			if (error != null)
			{
				if (errorLabel == null)
				{
					// Add error label
					errorLabel = new Label
					{
						FontSize = 10,
						Margin = new Thickness(5, -5, 0, 0),
						TextColor = Color.Red,
						VerticalOptions = LayoutOptions.Start
					};

					var row = GetRow(picker) + 1;

					if (row >= RowDefinitions.Count)
					{
						RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
					}

					SetGridRowColumn(errorLabel, row, 0);
					Children.Add(errorLabel);
				}

				// Set label color and error context
				errorLabel.Text = error.ErrorMessage;
			}
			else if (errorLabel != null)
			{
				// No, so remove it
				var row = GetRow(errorLabel);
				Children.Remove(errorLabel);
				errorLabel = null;
				RowDefinitions.RemoveAt(row);
			}
		}

		/// <summary>
		/// Validates the field property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldvalue">The oldvalue.</param>
		/// <param name="newvalue">The newvalue.</param>
		private static void ValidateFieldPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var control = (ValidPicker) bindable;

			// If equal, do nothing
			if (oldvalue == newvalue)
			{
				return;
			}

			// Save viewmodel if it is valid
			if (control.BindingContext is IValidateViewModel model)
			{
				control.viewModel = model;
			}

			if (control.BindingContext is INotifyDataErrorInfo errorModel)
			{
				errorModel.ErrorsChanged -= control.ValidateField_ErrorsChanged;

				if (bool.TryParse(newvalue.ToString(), out var validatesOnDataErrors) && validatesOnDataErrors)
					errorModel.ErrorsChanged += control.ValidateField_ErrorsChanged;
			}
		}

		/// <summary>
		/// Validates the name of the property.
		/// </summary>
		/// <param name="validPicker">The valid entry.</param>
		/// <returns>System.String.</returns>
		private static string ValidatePropertyName(ValidPicker validPicker)
		{
			// Get binding
			validPicker.binding = validPicker.binding ?? validPicker.GetBinding(SelectedItemProperty);

			if (string.IsNullOrWhiteSpace(validPicker.binding.Path))
				throw new ArgumentNullException($"{nameof(validPicker.binding.Path)} cannot be null");

			// Get property name after first . 
			if (validPicker.binding.Path.Contains("."))
			{
				return validPicker.binding.Path.Substring(validPicker.binding.Path.IndexOf(".", StringComparison.Ordinal) + 1);
			}

			return validPicker.binding.Path;
		}
		#endregion

		#region Override methods
		/// <summary>
		/// Application developers override this to respond when the binding context changes.
		/// </summary>
		/// <remarks>To be added.</remarks>
		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			if (ValidateField)
			{
				ValidateFieldPropertyChanged(this, null, ValidateField);
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the label font attributes.
		/// </summary>
		/// <value>The label font attributes.</value>
		public FontAttributes FontAttributes
		{
			get => (FontAttributes) GetValue(LabelFontAttributesProperty);
			set => SetValue(LabelFontAttributesProperty, value);
		}

		/// <summary>
		/// Gets or sets the size of the font.
		/// </summary>
		/// <value>The size of the font.</value>
		[Xamarin.Forms.TypeConverter(typeof(FontSizeConverter))]
		public double FontSize
		{
			get => (double) GetValue(FontSizeProperty);
			set => SetValue(FontSizeProperty, value);
		}

		/// <summary>
		/// Gets a value indicating whether this element is focused currently. This is a bindable property.
		/// </summary>
		/// <value><see langword="true" /> if the element is focused; otherwise, <see langword="false" />.</value>
		public new bool IsFocused
		{
			get => (bool) GetValue(IsFocusedProperty);
			set => SetValue(IsFocusedProperty, value);
		}

		/// <summary>
		/// Gets or sets the item display path.
		/// </summary>
		/// <value>The item display path.</value>
		public string ItemDisplayPath
		{
			get => (string) GetValue(ItemDisplayPathProperty);
			set => SetValue(ItemDisplayPathProperty, value);
		}

		/// <summary>
		/// Gets or sets the items source.
		/// </summary>
		/// <value>The items source.</value>
		public IList ItemsSource
		{
			get => (IList)GetValue(ItemsSourceProperty);
			set => SetValue(ItemsSourceProperty, value);
		}

		/// <summary>
		/// Gets or sets the label.
		/// </summary>
		/// <value>The label.</value>
		public string Label
		{
			get => (string) GetValue(LabelProperty);
			set => SetValue(LabelProperty, value);
		}

		/// <summary>
		/// Gets or sets the numeric maximum.
		/// </summary>
		/// <value>The numeric maximum.</value>
		public int NumericMaximum
		{
			get => (int) GetValue(NumericMaximumProperty);
			set => SetValue(NumericMaximumProperty, value);
		}

		/// <summary>
		/// Gets or sets the numeric minimum.
		/// </summary>
		/// <value>The numeric minimum.</value>
		public int NumericMinimum
		{
			get => (int) GetValue(NumericMinimumProperty);
			set => SetValue(NumericMinimumProperty, value);
		}

		/// <summary>
		/// The text link color property
		/// </summary>
		public Color OptionalLabelTextColor
		{
			get => (Color) GetValue(OptionalLabelTextColorProperty);
			set => SetValue(OptionalLabelTextColorProperty, value);
		}

		/// <summary>
		/// Gets or sets the placeholder.
		/// </summary>
		/// <value>The placeholder.</value>
		public string Placeholder
		{
			get => (string) GetValue(PlaceholderProperty);
			set => SetValue(PlaceholderProperty, value);
		}

		/// <summary>
		/// Gets or sets the selected index.
		/// </summary>
		/// <value>The selected index.</value>
		public object SelectedIndex
		{
			get => GetValue(SelectedIndexProperty);
			set => SetValue(SelectedIndexProperty, value);
		}

		/// <summary>
		/// Gets or sets the selected item.
		/// </summary>
		/// <value>The selected item.</value>
		public object SelectedItem
		{
			get => GetValue(SelectedItemProperty);
			set => SetValue(SelectedItemProperty, value);
		}

		/// <summary>
		/// Gets or sets a value indicating whether [show error message].
		/// </summary>
		/// <value><c>true</c> if [show error message]; otherwise, <c>false</c>.</value>
		public bool ShowErrorMessage
		{
			get => (bool) GetValue(ShowErrorMessageProperty);
			set => SetValue(ShowErrorMessageProperty, value);
		}

		/// <summary>
		/// Gets or sets a value indicating whether [show optional text].
		/// </summary>
		/// <value><c>true</c> if [show optional text]; otherwise, <c>false</c>.</value>
		public bool ShowOptionalText
		{
			get => (bool) GetValue(ShowOptionalTextProperty);
			set => SetValue(ShowOptionalTextProperty, value);
		}

		/// <summary>
		/// Gets or sets the color of the text.
		/// </summary>
		/// <value>The color of the text.</value>
		public Color TextColor
		{
			get => (Color) GetValue(TextColorProperty);
			set => SetValue(TextColorProperty, value);
		}

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public string Title
		{
			get => (string) GetValue(TitleProperty);
			set => SetValue(TitleProperty, value);
		}

		/// <summary>
		/// Gets or sets a value indicating whether [validate field].
		/// </summary>
		/// <value><c>true</c> if [validate field]; otherwise, <c>false</c>.</value>
		public bool ValidateField 
		{
			get => (bool) GetValue(ValidateFieldProperty);
			set => SetValue(ValidateFieldProperty, value);
		}
		#endregion

		#region Fields
		private Binding binding;
		private Label errorLabel;
		private Label label;
		private Label optionalLabel;
		private Picker picker;
		private IValidateViewModel viewModel;
		#endregion
	}
}