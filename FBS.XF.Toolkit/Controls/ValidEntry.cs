using System;
using System.ComponentModel;
using System.Linq;
using FBS.XF.Toolkit.Extensions;
using FBS.XF.Toolkit.Images;
using FBS.XF.Toolkit.Interfaces;
using FluentValidation.Results;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Controls
{
	/// <summary>
	/// Valid Entry.
	/// </summary>
	public class ValidEntry : Grid
	{
		#region Events/Delegates
		/// <summary>
		/// Occurs when [show help tapped].
		/// </summary>
		public event EventHandler<ItemTappedEventArgs> ShowHelpTapped;

		/// <summary>
		/// Occurs when [text changed].
		/// </summary>
		public event EventHandler<TextChangedEventArgs> TextChanged;
		#endregion

		#region Bindable properties
		/// <summary>
		/// The automatic capitalization property
		/// </summary>
		public static readonly BindableProperty AutoCapitalizationProperty =
			BindableProperty.Create(nameof(AutoCapitalization), typeof(bool), typeof(ValidEntry), true,
				BindingMode.TwoWay, propertyChanged: AutoCapitalizationPropertyChanged);

		/// <summary>
		/// The font family property
		/// </summary>
		public static readonly BindableProperty FontFamilyProperty =
			BindableProperty.Create(nameof(FontSize), typeof(string), typeof(ValidEntry), default(string),
				propertyChanged: FontFamilyPropertyChanged);

		/// <summary>
		/// The font size property
		/// </summary>
		public static readonly BindableProperty FontSizeProperty =
			BindableProperty.Create(nameof(FontSize), typeof(double), typeof(ValidEntry), default(double),
				propertyChanged: FontSizePropertyChanged);

		/// <summary>
		/// The is editable property
		/// </summary>
		public static readonly BindableProperty IsEditableProperty =
			BindableProperty.Create(nameof(IsEditable), typeof(bool), typeof(ValidEntry), default(bool),
				BindingMode.TwoWay, propertyChanged: IsEditablePropertyChanged);

		/// <summary>
		/// The is focused property
		/// </summary>
		public new static readonly BindableProperty IsFocusedProperty =
			BindableProperty.Create(nameof(IsFocused), typeof(bool), typeof(ValidEntry), default(bool));

		/// <summary>
		/// The return key type property
		/// </summary>
		public static readonly BindableProperty ReturnTypeProperty = 
			BindableProperty.Create(nameof(ReturnType), typeof(ReturnType), typeof(ValidEntry), ReturnType.Default,
				propertyChanged: ReturnTypePropertyChanged);

		/// <summary>
		/// The label property
		/// </summary>
		public static readonly BindableProperty LabelProperty =
			BindableProperty.Create(nameof(Label), typeof(string), typeof(ValidEntry), default(string),
				propertyChanged: LabelPropertyChanged);

		/// <summary>
		/// The label font attributes property
		/// </summary>
		public static readonly BindableProperty LabelFontAttributesProperty =
			BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(ValidEntry), default(FontAttributes),
				propertyChanged: LabelFontAttributesPropertyChanged);

		/// <summary>
		/// The length property
		/// </summary>
		public static readonly BindableProperty MaxLengthProperty =
			BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(ValidEntry), default(int), 
				propertyChanged: MaxLengthPropertyChanged);

		/// <summary>
		/// The text property
		/// </summary>
		public static readonly BindableProperty PlaceholderProperty =
			BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(ValidEntry), default(string),
				propertyChanged: PlaceholderPropertyChanged);

		/// <summary>
		/// The show error message visible property
		/// </summary>
		public static readonly BindableProperty ShowErrorMessageProperty =
			BindableProperty.Create(nameof(ShowErrorMessage), typeof(bool), typeof(ValidEntry), default(bool),
				BindingMode.TwoWay, propertyChanged: ShowErrorMessageVisiblePropertyChanged);

		/// <summary>
		/// The show help icon property
		/// </summary>
		public static readonly BindableProperty ShowHelpIconProperty =
			BindableProperty.Create(nameof(ShowHelpIcon), typeof(bool), typeof(ValidEntry), default(bool),
				BindingMode.TwoWay, propertyChanged: ShowHelpIconPropertyChanged);

		/// <summary>
		/// The show optional text property
		/// </summary>
		public static readonly BindableProperty ShowOptionalTextProperty =
			BindableProperty.Create(nameof(ShowOptionalText), typeof(bool), typeof(ValidEntry), default(bool),
				BindingMode.TwoWay, propertyChanged: ShowOptionalTextPropertyChanged);

		/// <summary>
		/// The text property
		/// </summary>
		public static readonly BindableProperty TextProperty = 
			BindableProperty.Create(nameof(Text), typeof(string), typeof(ValidEntry), defaultBindingMode: BindingMode.TwoWay, 
				propertyChanged: TextPropertyChanged);

		/// <summary>
		/// The text color property
		/// </summary>
		public static readonly BindableProperty TextColorProperty =
			BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(ValidEntry), default(Color),
				BindingMode.TwoWay, propertyChanged: TextColorChanged);

		/// <summary>
		/// The validate field property
		/// </summary>
		public static readonly BindableProperty ValidateFieldProperty =
			BindableProperty.Create(nameof(ValidateField), typeof(bool), typeof(ValidEntry), default(bool),
				BindingMode.OneWay, propertyChanged: ValidateFieldPropertyChanged);
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ValidEntry"/> class.
		/// </summary>
		public ValidEntry()
		{
			// Add entry
			if (textEntry == null)
			{
				CreateControl(Keyboard.Default, false);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidEntry" /> class.
		/// </summary>
		/// <param name="keyboard">The keyboard.</param>
		/// <param name="isPassword">if set to <c>true</c> [is password].</param>
		public ValidEntry(Keyboard keyboard, bool isPassword = false)
		{
			// Add entry
			if (textEntry == null)
			{
				CreateControl(keyboard, isPassword);
			}
		}
		#endregion

		#region UI methods
		/// <summary>
		/// Handles the Tapped event of the HelpImage control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void HelpImage_Tapped(object sender, EventArgs e)
		{
			// Invoke action
			ShowHelpTapped?.Invoke(this, null);
		}

		/// <summary>
		/// Handles the TextChanged event of the TextEntry control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
		private void TextEntry_TextChanged(object sender, TextChangedEventArgs e)
		{
			TextChanged?.Invoke(this, e);
		}

		/// <summary>
		/// Handles the Tapped event of the ShowEye control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void Show_Tapped(object sender, EventArgs e)
		{
			// Save current cursor position
			var cursorPosition = textEntry.CursorPosition;

			// Flip from password field to normal or visa-versa
			textEntry.IsPassword = !textEntry.IsPassword;

			if (showImage != null)
			{
				showImage.IsVisible = false;
			}

			if (showLabel != null)
			{
				showLabel.IsVisible = false;
			}

			// Restore cursor position
			textEntry.CursorPosition = cursorPosition;
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Automatics the capitalization property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void AutoCapitalizationPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue != null)
			{
				((ValidEntry) bindable).textEntry.Keyboard = Keyboard.Create(KeyboardFlags.None);
			}
		}

		/// <summary>
		/// Creates the control.
		/// </summary>
		/// <param name="keyboard">The keyboard.</param>
		/// <param name="isPassword">if set to <c>true</c> [is password].</param>
		private void CreateControl(Keyboard keyboard, bool isPassword)
		{
			// Add rows and columns to grid
			RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});
			ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Auto});
			ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Auto});
			ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Star});
			ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Auto});

			// Add entry field
			textEntry = new Entry
			{
				BindingContext = this,
				FontSize = FontSize > 0 ? FontSize : Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				FontFamily = FontFamily,
				IsPassword = isPassword,
				Keyboard = keyboard,
				Margin = new Thickness(0, -5, 0, 0),
				ReturnType = ReturnType
			};

			textEntry.TextChanged += TextEntry_TextChanged;
			textEntry.SetBinding(Entry.TextProperty, "Text", BindingMode.TwoWay);
			textEntry.SetBinding(Entry.IsFocusedProperty, "IsFocused", BindingMode.OneWayToSource);
			textEntry.Unfocused += TextEntry_Unfocused;
			SetGridRowColumn(textEntry, 0, 0, 0, 4);
			Children.Add(textEntry);

			// Is this a password
			if (isPassword)
			{
				// Android standards are an eye to show the password, iOS is the word Show
				if (DeviceInfo.Platform == DevicePlatform.Android)
				{
					// Add show image
					showImage = new Image
					{
						HeightRequest = 16,
						HorizontalOptions = LayoutOptions.End,
						Margin = new Thickness(0, 0, 5, 0),
						Source = SvgImageSource.FromSvgResource("eye.svg", 16, 16, Color.Red, GetType()),
						VerticalOptions = LayoutOptions.Center,
						WidthRequest = 16,
					};

					var showHideTap = new TapGestureRecognizer {NumberOfTapsRequired = 1};
					showHideTap.Tapped += Show_Tapped;
					showImage.GestureRecognizers.Add(showHideTap);

					// Add item to grid
					SetGridRowColumn(showImage, 0, 3);
					Children.Add(showImage);
				}
				else 
				{
					// Add show label
					showLabel = new Label
					{
						FontFamily = FontFamily,
						FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
						HorizontalOptions = LayoutOptions.End,
						Margin = new Thickness(0, 0, 5, 0),
						Text = "Show",
						TextColor = Color.Red,
						VerticalOptions = LayoutOptions.Center
					};

					var showHideTap = new TapGestureRecognizer {NumberOfTapsRequired = 1};
					showHideTap.Tapped += Show_Tapped;
					showLabel.GestureRecognizers.Add(showHideTap);

					// Add item to grid
					SetGridRowColumn(showLabel, 0, 3);
					Children.Add(showLabel);
				}
			}
		}

		/// <summary>
		/// Creates the help icon control.
		/// </summary>
		private void CreateHelpIconControl()
		{
			if (ShowHelpIcon)
			{
				// Add help image
				if (helpImage == null && textLabel != null)
				{
					helpImage = new Image
					{
						HorizontalOptions = LayoutOptions.End,
						Margin = new Thickness(0, 0, 5, 0),
						Source = "Information",
						VerticalOptions = LayoutOptions.Center
					};

					var helpImageTap = new TapGestureRecognizer {NumberOfTapsRequired = 1};
					helpImageTap.Tapped += HelpImage_Tapped;
					helpImage.GestureRecognizers.Add(helpImageTap);

					// Add item to grid
					SetGridRowColumn(helpImage, 0, 1);
					Children.Add(helpImage);
				}
			}
		}

		/// <summary>
		/// Creates the optional control.
		/// </summary>
		private void CreateOptionalControl()
		{
			if (ShowOptionalText)
			{
				// Add help image
				if (optionalLabel == null && textLabel != null)
				{
					optionalLabel = new Label
					{
						FontAttributes = FontAttributes.Italic,
						FontSize = textEntry.FontSize,
						Text = "Optional",
						TextColor = Color.Red
					};

					// Add item to grid
					SetGridRowColumn(optionalLabel, 0, 1);
					Children.Add(optionalLabel);
				}
			}
		}

		/// <summary>
		/// Font family property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void FontFamilyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue != null && ((ValidEntry) bindable).textEntry != null)
			{
				((ValidEntry) bindable).textEntry.FontFamily = (string) newValue;

				if (((ValidEntry) bindable).optionalLabel != null)
				{
					((ValidEntry) bindable).optionalLabel.FontFamily = (string) newValue;
				}
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
			if (newValue != null && ((ValidEntry) bindable).textEntry != null)
			{
				((ValidEntry) bindable).textEntry.FontSize = (double) newValue;

				if (((ValidEntry) bindable).optionalLabel != null)
				{
					((ValidEntry) bindable).optionalLabel.FontSize = (double) newValue;
				}
			}
		}

		/// <summary>
		/// Determines whether [is read only property changed] [the specified bindable].
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void IsEditablePropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if ((bool) newValue)
			{
				var grid = (ValidEntry) bindable;

				// Make text field readonly
				grid.textEntry.IsReadOnly = true;
			}
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
				var control = (ValidEntry) bindable;

				// Add the label
				if (control.textLabel == null)
				{
					// Add extra row
					control.RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});

					// Adjust other controls
					foreach (var childControl in control.Children)
					{
						SetRow(childControl, GetRow(childControl) + 1);
					}

					// Now create text label
					control.textLabel = new Label
					{
						FontAttributes = control.FontAttributes,
						FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
						Margin = new Thickness(3, 0, 0, 0),
						Text = (string) newValue
					};

					control.SetGridRowColumn(control.textLabel, 0, 0, 0, 1);
					control.Children.Add(control.textLabel);

					// Do we need helptext
					if (control.ShowHelpIcon)
					{
						control.CreateHelpIconControl();
					}

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
				var control = (ValidEntry) bindable;

				if (control.textLabel != null)
				{
					control.textLabel.FontAttributes = control.FontAttributes;
				}
			}
		}

		/// <summary>
		/// Maximum length property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void MaxLengthPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			// Adjust max length behavior
			((ValidEntry) bindable).textEntry.MaxLength = (int) newValue;
		}

		/// <summary>
		/// Placeholder property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void PlaceholderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			((ValidEntry) bindable).textEntry.Placeholder = newValue.ToString();
		}

		/// <summary>
		/// Returns the type property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void ReturnTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue != null)
			{
				((ValidEntry)bindable).textEntry.ReturnType = (ReturnType) newValue;
			}
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
			((ValidEntry) bindable).errorLabel.IsVisible = (bool) newValue;
		}

		/// <summary>
		/// Helps the text property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void ShowHelpIconPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if ((bool) newValue)
			{
				((ValidEntry) bindable).CreateHelpIconControl();
			}
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
				((ValidEntry) bindable).CreateOptionalControl();
			}
		}

		/// <summary>
		/// Texts the color changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void TextColorChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var validEntry = ((ValidEntry) bindable);

			if (validEntry.textLabel != null)
			{
				validEntry.textLabel.TextColor = (Color) newValue;
			}

			if (validEntry.textEntry != null)
			{
				validEntry.textEntry.PlaceholderColor = (Color) newValue;
				validEntry.textEntry.TextColor = (Color) newValue;
			}
		}

		/// <summary>
		/// Handles the Unfocused event of the TextEntry control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="FocusEventArgs"/> instance containing the event data.</param>
		private void TextEntry_Unfocused(object sender, FocusEventArgs e)
		{
			if (ValidateField)
			{
				// Validate the field

				var propertyName = ValidatePropertyName(this);
				viewModel?.ValidateCommand?.Execute(propertyName);
			}
		}

		/// <summary>
		/// Texts the property changing.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void TextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue != null || oldValue != null)
			{
				// Get the control
				var control = (ValidEntry) bindable;

				if (control.ValidateField)
				{
					// Validate the field
					var propertyName = ValidatePropertyName(control);
					control.viewModel?.ValidateCommand?.Execute(propertyName);
				}
			}
		}

		/// <summary>
		/// Handles the ErrorsChanged event of the ValidateField control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="args">The <see cref="DataErrorsChangedEventArgs"/> instance containing the event data.</param>
		private void ValidateField_ErrorsChanged(object sender, DataErrorsChangedEventArgs args)
		{
			// Get the first error for this property
			var propertyName = ValidatePropertyName(this);

			// Is this the correct control for the property passed
			if (!args.PropertyName.Equals(propertyName))
			{
				// No, so leave
				return;
			}

			// Get errors for this property
			var error = ((INotifyDataErrorInfo) sender).GetErrors(propertyName)?.Cast<ValidationFailure>().FirstOrDefault();

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

					var row = GetRow(textEntry) + 1;

					if (row >= RowDefinitions.Count)
					{
						RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});
					}

					SetGridRowColumn(errorLabel, row, 0, 0, 3);
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
				RowDefinitions.RemoveAt(row);
				errorLabel = null;
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
			var control = (ValidEntry) bindable;

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
		/// <param name="validEntry">The valid entry.</param>
		private static string ValidatePropertyName(ValidEntry validEntry)
		{
			// Get binding
			validEntry.binding = validEntry.binding ?? validEntry.GetBinding(TextProperty);

			if (string.IsNullOrWhiteSpace(validEntry.binding.Path))
				throw new ArgumentNullException($"{nameof(validEntry.binding.Path)} cannot be null");

			// Get property name after first . 
			if (validEntry.binding.Path.Contains("."))
			{
				return validEntry.binding.Path.Substring(validEntry.binding.Path.IndexOf(".", StringComparison.Ordinal) + 1);
			}

			return validEntry.binding.Path;
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

			ValidateFieldPropertyChanged(this, null, ValidateField);
		}

		
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets a value indicating whether [automatic capitalization].
		/// </summary>
		/// <value><c>true</c> if [automatic capitalization]; otherwise, <c>false</c>.</value>
		public bool AutoCapitalization {
			get => (bool) GetValue(AutoCapitalizationProperty);
			set => SetValue(AutoCapitalizationProperty, value); }

		/// <summary>
		/// Gets or sets the font family.
		/// </summary>
		/// <value>The font family.</value>
		public string FontFamily
		{
			get => (string) GetValue(FontFamilyProperty);
			set => SetValue(FontFamilyProperty, value);
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
		/// Gets or sets a value indicating whether this instance is editable.
		/// </summary>
		/// <value><c>true</c> if this instance is editable; otherwise, <c>false</c>.</value>
		public bool IsEditable
		{
			get => (bool) GetValue(IsEditableProperty);
			set => SetValue(IsEditableProperty, value);
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
		/// Gets or sets the label.
		/// </summary>
		/// <value>The label.</value>
		public string Label
		{
			get => (string) GetValue(LabelProperty);
			set => SetValue(LabelProperty, value);
		}

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
		/// Gets or sets the maximum length.
		/// </summary>
		/// <value>The maximum length.</value>
		public int MaxLength
		{
			get => (int) GetValue(MaxLengthProperty);
			set => SetValue(MaxLengthProperty, value);
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
		/// Gets or sets the return key type.
		/// </summary>
		/// <value>The return key type.</value>
		public ReturnType ReturnType
		{
			get => (ReturnType)GetValue(ReturnTypeProperty);
			set => SetValue(ReturnTypeProperty, value);
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
		/// Gets or sets a value indicating whether [show help icon].
		/// </summary>
		/// <value><c>true</c> if [show help icon]; otherwise, <c>false</c>.</value>
		public bool ShowHelpIcon
		{
			get => (bool) GetValue(ShowHelpIconProperty);
			set => SetValue(ShowHelpIconProperty, value);
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
		/// Gets or sets the text for the Label. This is a bindable property.
		/// </summary>
		/// <value>The <see cref="T:System.String" /> value to be displayed inside of the Label.</value>
		/// <remarks>Setting Text to a non-null value will set the FormattedText property to null.</remarks>
		public string Text
		{
			get => (string) GetValue(TextProperty);
			set => SetValue(TextProperty, value);
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
		//private static Assembly assembly = Assembly.GetExecutingAssembly();
		private Binding binding;
		private Image helpImage;
		private Label errorLabel;
		private Label optionalLabel;
		private Label showLabel;
		private Image showImage;
		private Entry textEntry;
		private Label textLabel;
		private IValidateViewModel viewModel;
		#endregion
	}
}