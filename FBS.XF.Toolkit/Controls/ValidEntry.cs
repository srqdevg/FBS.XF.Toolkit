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
	public class ValidEntry : Grid, IDisposable
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
				propertyChanged: (bd, ov, nv) => ((ValidEntry) bd).AutoCapitalizationPropertyChanged(ov, nv));

		/// <summary>
		/// The background color property
		/// </summary>
		public new static readonly BindableProperty BackgroundColorProperty =
			BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(CustomButton), Color.Default,
				propertyChanged: (bd, ov, nv) => ((ValidEntry) bd).BackgroundColorPropertyChanged(ov, nv));

		/// <summary>
		/// The font family property
		/// </summary>
		public static readonly BindableProperty FontFamilyProperty =
			BindableProperty.Create(nameof(FontSize), typeof(string), typeof(ValidEntry), default(string),
				propertyChanged: (bd, ov, nv) => ((ValidEntry) bd).FontFamilyPropertyChanged(ov, nv));

		/// <summary>
		/// The font size property
		/// </summary>
		public static readonly BindableProperty FontSizeProperty =
			BindableProperty.Create(nameof(FontSize), typeof(double), typeof(ValidEntry), default(double),
				propertyChanged: (bd, ov, nv) => ((ValidEntry) bd).FontSizePropertyChanged(ov, nv));

		/// <summary>
		/// The height request property
		/// </summary>
		public new static readonly BindableProperty HeightRequestProperty =
			BindableProperty.Create(nameof(HeightRequest), typeof(double), typeof(ValidEntry), default(double),
				propertyChanged: (bd, ov, nv) => ((ValidEntry) bd).HeightRequestPropertyChanged(ov, nv));

		/// <summary>
		/// The horizontal text alignment property
		/// </summary>
		public static readonly BindableProperty HorizontalTextAlignmentProperty =
			BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(ValidEntry), TextAlignment.Start,
				propertyChanged: (bd, ov, nv) => ((ValidEntry) bd).HorizontalTextAlignmentPropertyChanged(ov, nv));

		/// <summary>
		/// The is editable property
		/// </summary>
		public static readonly BindableProperty IsEditableProperty =
			BindableProperty.Create(nameof(IsEditable), typeof(bool), typeof(ValidEntry), default(bool),
				BindingMode.TwoWay, propertyChanged: (bd, ov, nv) => ((ValidEntry) bd).IsEditablePropertyChanged(ov, nv));

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
				propertyChanged: (bd, ov, nv) => ((ValidEntry) bd).ReturnTypePropertyChanged(ov, nv));

		/// <summary>
		/// The label property
		/// </summary>
		public static readonly BindableProperty LabelProperty =
			BindableProperty.Create(nameof(Label), typeof(string), typeof(ValidEntry), default(string),
				propertyChanged: (bd, ov, nv) => ((ValidEntry) bd).LabelPropertyChanged(ov, nv));

		/// <summary>
		/// The label font attributes property
		/// </summary>
		public static readonly BindableProperty LabelFontAttributesProperty =
			BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(ValidEntry), default(FontAttributes),
				propertyChanged: (bd, ov, nv) => ((ValidEntry) bd).LabelFontAttributesPropertyChanged(ov, nv));

		/// <summary>
		/// The length property
		/// </summary>
		public static readonly BindableProperty MaxLengthProperty =
			BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(ValidEntry), default(int),
				propertyChanged: (bd, ov, nv) => ((ValidEntry) bd).MaxLengthPropertyChanged(ov, nv));

		/// <summary>
		/// The text property
		/// </summary>
		public static readonly BindableProperty PlaceholderProperty =
			BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(ValidEntry), default(string),
				propertyChanged: (bd, ov, nv) => ((ValidEntry) bd).PlaceholderPropertyChanged(ov, nv));

		/// <summary>
		/// The show error message visible property
		/// </summary>
		public static readonly BindableProperty ShowErrorMessageProperty =
			BindableProperty.Create(nameof(ShowErrorMessage), typeof(bool), typeof(ValidEntry), default(bool),
				propertyChanged: (bd, ov, nv) => ((ValidEntry) bd).ShowErrorMessageVisiblePropertyChanged(ov, nv));

		/// <summary>
		/// The show help icon property
		/// </summary>
		public static readonly BindableProperty ShowHelpIconProperty =
			BindableProperty.Create(nameof(ShowHelpIcon), typeof(bool), typeof(ValidEntry), default(bool),
				propertyChanged: (bd, ov, nv) => ((ValidEntry) bd).ShowHelpIconPropertyChanged(ov, nv));

		/// <summary>
		/// The show optional text property
		/// </summary>
		public static readonly BindableProperty ShowOptionalTextProperty =
			BindableProperty.Create(nameof(ShowOptionalText), typeof(bool), typeof(ValidEntry), default(bool),
				propertyChanged: (bd, ov, nv) => ((ValidEntry) bd).ShowOptionalTextPropertyChanged(ov, nv));

		/// <summary>
		/// The text property
		/// </summary>
		public static readonly BindableProperty TextProperty =
			BindableProperty.Create(nameof(Text), typeof(string), typeof(ValidEntry), defaultBindingMode: BindingMode.TwoWay,
				propertyChanged: (bd, ov, nv) => ((ValidEntry) bd).TextPropertyChanged(ov, nv));

		/// <summary>
		/// The text color property
		/// </summary>
		public static readonly BindableProperty TextColorProperty =
			BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(ValidEntry), default(Color), BindingMode.TwoWay,
				propertyChanged: (bd, ov, nv) => ((ValidEntry) bd).TextColorChanged(ov, nv));

		/// <summary>
		/// The validate field property
		/// </summary>
		public static readonly BindableProperty ValidateFieldProperty =
			BindableProperty.Create(nameof(ValidateField), typeof(bool), typeof(ValidEntry), default(bool),
				propertyChanged: (bd, ov, nv) => ((ValidEntry) bd).ValidateFieldPropertyChanged(ov, nv));

		/// <summary>
		/// The vertical text alignment property
		/// </summary>
		public static readonly BindableProperty VerticalTextAlignmentProperty =
			BindableProperty.Create(nameof(VerticalTextAlignment), typeof(TextAlignment), typeof(ValidEntry), TextAlignment.Center,
				propertyChanged: (bd, ov, nv) => ((ValidEntry) bd).VerticalTextAlignmentPropertyChanged(ov, nv));
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ValidEntry"/> class.
		/// </summary>
		public ValidEntry()
		{
			// Add entry
			CreateControl(Keyboard.Default, false);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidEntry" /> class.
		/// </summary>
		/// <param name="keyboard">The keyboard.</param>
		/// <param name="isPassword">if set to <c>true</c> [is password].</param>
		public ValidEntry(Keyboard keyboard, bool isPassword = false)
		{
			// Add entry
			CreateControl(keyboard, isPassword);
		}
		#endregion

		#region IDisposable
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			textEntry.TextChanged -= TextEntry_TextChanged;
			textEntry.Unfocused -= TextEntry_Unfocused;
			
			if (showHideTap != null)
			{
				showHideTap.Tapped -= Show_Tapped;
			}

			if (helpImageTap != null)
			{
				helpImageTap.Tapped -= HelpImage_Tapped;
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
		#endregion

		#region Protected methods
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
		#endregion

		#region Private methods
		/// <summary>
		/// Automatics the capitalization property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void AutoCapitalizationPropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue && newValue != null && textEntry != null)
			{
				textEntry.Keyboard = Keyboard.Create(KeyboardFlags.None);
			}
		}

		/// <summary>
		/// Backgrounds the color property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void BackgroundColorPropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue && newValue != null && textEntry != null)
			{
				textEntry.BackgroundColor = (Color) newValue;
			}
		}

		/// <summary>
		/// Creates the control.
		/// </summary>
		/// <param name="keyboard">The keyboard.</param>
		/// <param name="isPassword">if set to <c>true</c> [is password].</param>
		private void CreateControl(Keyboard keyboard, bool isPassword)
		{
			if (RowDefinitions.Any())
			{
				return;
			}

			// Add rows and columns to grid
			RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});
			ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Auto});
			ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Auto});
			ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Star});
			ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Auto});

			// Add entry field
			textEntry = new CustomEntry
			{
				BackgroundColor = BackgroundColor,
				BindingContext = this,
				FontSize = FontSize > 0 ? FontSize : Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				FontFamily = FontFamily,
				HorizontalTextAlignment = HorizontalTextAlignment,
				IsPassword = isPassword,
				Keyboard = keyboard,
				Margin = new Thickness(0, -5, 0, 0),
				ReturnType = ReturnType,
				TextColor = TextColor,
				VerticalTextAlignment = VerticalTextAlignment,
			};

			textEntry.TextChanged += TextEntry_TextChanged;
			textEntry.SetBinding(Entry.TextProperty, "Text", BindingMode.TwoWay);
			textEntry.SetBinding(Entry.IsFocusedProperty, "IsFocused", BindingMode.OneWayToSource);
			textEntry.Unfocused += TextEntry_Unfocused;

			////if (Device.RuntimePlatform == Device.iOS)
			////{
			////	// We need a frame around the text box
			////	frame = new Frame
			////	{
			////		BorderColor = TextColor,
			////		WidthRequest = 1
			////	};

			////	SetGridRowColumn(frame, 0, 0, 0, 4);
			////	Children.Add(frame);

			////	frame.Content = textEntry;
			////}
			////else
			////{
				SetGridRowColumn(textEntry, 0, 0, 0, 4);
				Children.Add(textEntry);
			////}

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

					showHideTap = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
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
						FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
						HorizontalOptions = LayoutOptions.End,
						Margin = new Thickness(0, 0, 5, 0),
						Text = "Show",
						TextColor = Color.Red,
						VerticalOptions = LayoutOptions.Center
					};

					showHideTap = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
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

					helpImageTap = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
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
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void FontFamilyPropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue && newValue != null && textEntry != null)
			{
				textEntry.FontFamily = (string) newValue;

				if (optionalLabel != null)
				{
					optionalLabel.FontFamily = (string) newValue;
				}
			}
		}

		/// <summary>
		/// Font size property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void FontSizePropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue && newValue != null && textEntry != null)
			{
				textEntry.FontSize = (double) newValue;

				if (optionalLabel != null)
				{
					optionalLabel.FontSize = (double) newValue;
				}
			}
		}

		/// <summary>
		/// Heights the request property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void HeightRequestPropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue && newValue != null && textEntry != null)
			{
				textEntry.HeightRequest = (double) newValue;

				if (optionalLabel != null)
				{
					optionalLabel.HeightRequest = (double) newValue;
				}
			}
		}

		/// <summary>
		/// Horizontals the text alignment property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void HorizontalTextAlignmentPropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue && newValue != null && textEntry != null)
			{
				textEntry.HorizontalTextAlignment = (TextAlignment) newValue;
			}
		}

		/// <summary>
		/// Determines whether [is read only property changed] [the specified bindable].
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void IsEditablePropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue && newValue != null && textEntry != null)
			{
				// Make text field readonly
				textEntry.IsReadOnly = true;
			}
		}

		/// <summary>
		/// Labels the property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void LabelPropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue && newValue != null)
			{
				if (textLabel == null)
				{
					// Add extra row
					RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});

					// Adjust other controls
					foreach (var childControl in Children)
					{
						SetRow(childControl, GetRow(childControl) + 1);
					}

					// Now create text label
					textLabel = new Label
					{
						FontAttributes = FontAttributes,
						FontSize = FontSize > 0 ? FontSize : Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
						Margin = new Thickness(3, 0, 0, 0),
						Text = (string) newValue
					};

					SetGridRowColumn(textLabel, 0, 0, 0, 1);
					Children.Add(textLabel);
				}
				else
				{
					Text = (string) newValue;
				}

				// Do we need helptext
				if (ShowHelpIcon)
				{
					CreateHelpIconControl();
				}

				// Do we need optional?
				if (ShowOptionalText)
				{
					CreateOptionalControl();
				}
			}
		}

		/// <summary>
		/// Labels the font attributes property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void LabelFontAttributesPropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue && newValue != null && textLabel != null)
			{
				textLabel.FontAttributes = FontAttributes;
			}
		}

		/// <summary>
		/// Maximum length property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void MaxLengthPropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue && newValue != null && textEntry != null)
			{
				// Adjust max length behavior
				textEntry.MaxLength = (int) newValue;
			}
		}

		/// <summary>
		/// Placeholder property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void PlaceholderPropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue && newValue != null && textEntry != null)
			{
				textEntry.Placeholder = newValue.ToString();
			}
		}

		/// <summary>
		/// Returns the type property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void ReturnTypePropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue && newValue != null && textEntry != null)
			{
				textEntry.ReturnType = (ReturnType) newValue;
			}
		}

		/// <summary>
		/// ShowErrorMessage property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void ShowErrorMessageVisiblePropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue && newValue != null && errorLabel != null)
			{
				errorLabel.IsVisible = (bool) newValue;
			}
		}

		/// <summary>
		/// Helps the text property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void ShowHelpIconPropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue && newValue != null && (bool) newValue)
			{
				CreateHelpIconControl();
			}
		}

		/// <summary>
		/// Shows the optional text property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void ShowOptionalTextPropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue && newValue != null && (bool) newValue)
			{
				CreateOptionalControl();
			}
		}

		/// <summary>
		/// Text color changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void TextColorChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue && newValue != null && textEntry != null && textLabel != null)
			{
				if (frame != null)
				{
					frame.BorderColor = (Color) newValue;
				}

				if (textLabel != null)
				{
					textLabel.TextColor = (Color) newValue;
				}

				if (textEntry != null)
				{
					textEntry.PlaceholderColor = (Color) newValue;
					textEntry.TextColor = (Color) newValue;
				}
			}
		}

		/// <summary>
		/// Text property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void TextPropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue && newValue != null)
			{
				if (ValidateField)
				{
					// Validate the field
					var propertyName = ValidatePropertyName(this);
					viewModel?.ValidateCommand?.Execute(propertyName);
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
						RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
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
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void ValidateFieldPropertyChanged(object oldValue, object newValue)
		{
			// If equal, do nothing
			if (oldValue != newValue && newValue != null)
			{
				return;
			}

			// Save viewmodel if it is valid
			if (BindingContext is IValidateViewModel model)
			{
				viewModel = model;
			}

			if (BindingContext is INotifyDataErrorInfo errorModel)
			{
				errorModel.ErrorsChanged -= ValidateField_ErrorsChanged;

				if (bool.TryParse(newValue.ToString(), out var validatesOnDataErrors) && validatesOnDataErrors)
				{
					errorModel.ErrorsChanged += ValidateField_ErrorsChanged;
				}
			}
		}

		/// <summary>
		/// Validates the name of the property.
		/// </summary>
		/// <param name="validEntry">The valid entry.</param>
		private static string ValidatePropertyName(ValidEntry validEntry)
		{
			// Get binding
			validEntry.binding ??= validEntry.GetBinding(TextProperty);

			if (string.IsNullOrWhiteSpace(validEntry.binding.Path))
				throw new ArgumentNullException($"{nameof(validEntry.binding.Path)} cannot be null");

			// Get property name after first . 
			if (validEntry.binding.Path.Contains("."))
			{
				return validEntry.binding.Path.Substring(validEntry.binding.Path.IndexOf(".", StringComparison.Ordinal) + 1);
			}

			return validEntry.binding.Path;
		}

		/// <summary>
		/// Verticals the text alignment property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void VerticalTextAlignmentPropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue && newValue != null && textEntry != null)
			{
				textEntry.VerticalTextAlignment = (TextAlignment) newValue;
			}
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

			ValidateFieldPropertyChanged(null, ValidateField);
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the color which will fill the background of a VisualElement. This is a bindable property.
		/// </summary>
		/// <value>The color that is used to fill the background of a VisualElement. The default is <see cref="P:Xamarin.Forms.Color.Default" />.</value>
		/// <remarks>To be added.</remarks>
		public new Color BackgroundColor
		{
			get => (Color) GetValue(BackgroundColorProperty);
			set => SetValue(BackgroundColorProperty, value);
		}

		/// <summary>
		/// Gets or sets a value indicating whether [automatic capitalization].
		/// </summary>
		/// <value><c>true</c> if [automatic capitalization]; otherwise, <c>false</c>.</value>
		public bool AutoCapitalization
		{
			get => (bool) GetValue(AutoCapitalizationProperty);
			set => SetValue(AutoCapitalizationProperty, value);
		}

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
		/// Gets or sets the horizontal text alignment.
		/// </summary>
		/// <value>The horizontal text alignment.</value>
		public TextAlignment HorizontalTextAlignment
		{
			get => (TextAlignment) GetValue(HorizontalTextAlignmentProperty);
			set => SetValue(HorizontalTextAlignmentProperty, value);
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
			get => (ReturnType) GetValue(ReturnTypeProperty);
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

		/// <summary>
		/// Gets or sets the vertical text alignment.
		/// </summary>
		/// <value>The vertical text alignment.</value>
		public TextAlignment VerticalTextAlignment
		{
			get => (TextAlignment) GetValue(VerticalTextAlignmentProperty);
			set => SetValue(VerticalTextAlignmentProperty, value);
		}
		#endregion

		#region Fields
		private Binding binding;
		private Frame frame;
		private Image helpImage;
		private TapGestureRecognizer helpImageTap;
		private Label errorLabel;
		private Label optionalLabel;
		private TapGestureRecognizer showHideTap;
		private Label showLabel;
		private Image showImage;
		private CustomEntry textEntry;
		private Label textLabel;
		private IValidateViewModel viewModel;
		#endregion
	}
}