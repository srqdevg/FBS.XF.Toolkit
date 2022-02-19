using System;
using System.ComponentModel;
using System.Linq;
using FBS.XF.Toolkit.Event;
using FBS.XF.Toolkit.Extensions;
using FluentValidation.Results;
using PropertyChanged;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Controls
{
	/// <summary>
	/// Valid Date Picker.
	/// </summary>
	public class ValidDatePicker : Grid, IDisposable
	{
		#region Bindable properties
		/// <summary>
		/// The background color property
		/// </summary>
		public new static readonly BindableProperty BackgroundColorProperty =
			BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(CustomButton), Color.Default,
				propertyChanged: (bd, ov, nv) => ((ValidDatePicker) bd).BackgroundColorChanged(ov, nv));

		/// <summary>
		/// The bullet character font size property
		/// </summary>
		public static readonly BindableProperty FontSizeProperty =
			BindableProperty.Create(nameof(FontSize), typeof(double), typeof(double),
				propertyChanged: (bd, ov, nv) => ((ValidDatePicker) bd).FontSizePropertyChanged(ov, nv));

		/// <summary>
		/// The format property
		/// </summary>
		public static readonly BindableProperty FormatProperty =
			BindableProperty.Create(nameof(Format), typeof(string), typeof(ValidDatePicker));

		/// <summary>
		/// The maximum date property
		/// </summary>
		public static readonly BindableProperty MaximumDateProperty =
			BindableProperty.Create(nameof(MaximumDate), typeof(DateTime), typeof(ValidDatePicker), default(DateTime),
				propertyChanged: (bd, ov, nv) => ((ValidDatePicker) bd).MaximumDatePropertyChanged(ov, nv));

		/// <summary>
		/// The minimum date property
		/// </summary>
		public static readonly BindableProperty MinimumDateProperty =
			BindableProperty.Create(nameof(MinimumDate), typeof(DateTime), typeof(ValidDatePicker), default(DateTime),
				propertyChanged: (bd, ov, nv) => ((ValidDatePicker) bd).MinimumDatePropertyChanged(ov, nv));

		/// <summary>
		/// The label property
		/// </summary>
		public static readonly BindableProperty LabelProperty =
			BindableProperty.Create(nameof(Label), typeof(string), typeof(ValidDatePicker), default(string),
				propertyChanged: (bd, ov, nv) => ((ValidDatePicker) bd).LabelPropertyChanged(ov, nv));

		/// <summary>
		/// The label font attributes property
		/// </summary>
		public static readonly BindableProperty LabelFontAttributesProperty =
			BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(ValidDatePicker), default(FontAttributes),
				propertyChanged: (bd, ov, nv) => ((ValidDatePicker) bd).LabelFontAttributesPropertyChanged(ov, nv));

		/// <summary>
		/// The selected date property
		/// </summary>
		public static readonly BindableProperty SelectedDateProperty =
			BindableProperty.Create(nameof(SelectedDateProperty), typeof(DateTime?), typeof(ValidDatePicker), default(DateTime),
				BindingMode.TwoWay, propertyChanged: (bd, ov, nv) => ((ValidDatePicker) bd).SelectedDatePropertyChanged(ov, nv));

		/// <summary>
		/// The show error message visible property
		/// </summary>
		public static readonly BindableProperty ShowErrorMessageProperty =
			BindableProperty.Create(nameof(ShowErrorMessage), typeof(bool), typeof(ValidDatePicker), default(bool),
				BindingMode.TwoWay, propertyChanged: (bd, ov, nv) => ((ValidDatePicker) bd).ShowErrorMessageVisiblePropertyChanged(ov, nv));

		/// <summary>
		/// The show optional text property
		/// </summary>
		public static readonly BindableProperty ShowOptionalTextProperty =
			BindableProperty.Create(nameof(ShowOptionalText), typeof(bool), typeof(ValidDatePicker), default(bool),
				BindingMode.TwoWay, propertyChanged: (bd, ov, nv) => ((ValidDatePicker) bd).ShowOptionalTextPropertyChanged(ov, nv));

		/// <summary>
		/// The title property
		/// </summary>
		public static readonly BindableProperty TitleProperty =
			BindableProperty.Create(nameof(TitleProperty), typeof(string), typeof(ValidDatePicker), null,
				BindingMode.OneWay, propertyChanged: (bd, ov, nv) => ((ValidDatePicker) bd).TitlePropertyChanged(ov, nv));

		/// <summary>
		/// The validate field property
		/// </summary>
		public static readonly BindableProperty ValidateFieldProperty =
			BindableProperty.Create(nameof(ValidateField), typeof(bool), typeof(ValidDatePicker), default(bool),
				BindingMode.OneWay, propertyChanged: (bd, ov, nv) => ((ValidDatePicker) bd).ValidateFieldPropertyChanged(ov, nv));
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ValidDatePicker"/> class.
		/// </summary>
		public ValidDatePicker()
		{
			// Add entry
			if (Children.Count == 0)
			{
				CreateControl();
			}
		}
		#endregion

		#region IDisposable
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			tapGestureRecognizer.Tapped -= TapGestureRecognizer_Tapped;
			calendarPopupPage.DataChanged -= CalendarPopupPage_DataChanged;
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Backgrounds the color changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void BackgroundColorChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue)
			{
				BackgroundColor = (Color) newValue;
			}
		}

		/// <summary>
		/// Calendars the popup page data changed.
		/// </summary>
		/// <param name="obj">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void CalendarPopupPage_DataChanged(CalendarEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(Format))
			{
				SelectedDate = e.NewDate;
				dateLabel.Text = e.NewDate?.Date.ToString(Format);
			}
			else
			{
				SelectedDate = e.NewDate;
				dateLabel.Text = e.NewDate?.Date.ToShortDateString();
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
				if (optionalLabel == null && label != null)
				{
					optionalLabel = new Label
					{
						FontAttributes = FontAttributes.Italic,
						HorizontalOptions = LayoutOptions.FillAndExpand,
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
		/// Creates the control.
		/// </summary>
		private void CreateControl()
		{
			// Add rows and columns to grid
			RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			ColumnDefinitions.Add(new ColumnDefinition { Width = 90 });
			ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

			// Create tap handler and add to grid
			tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
			GestureRecognizers.Add(tapGestureRecognizer);

			// Create date label
			dateLabel = new Label
			{
				BindingContext = this,
				HorizontalOptions = LayoutOptions.StartAndExpand
			};

			SetGridRowColumn(dateLabel, 0, 0, 0, 2);
			Children.Add(dateLabel);

			// Boxview 
			var boxView = new BoxView
			{
				Color = Application.Current.RequestedTheme == OSAppTheme.Dark ? Color.FromHex("#616161") : Color.FromHex("#414141"),
				HeightRequest = 1,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions= LayoutOptions.Start
			};

			SetGridRowColumn(boxView, 1, 0, 0, 2);
			Children.Add(boxView);

			// Create calendar popup page if we are not in WPF
			if (Device.RuntimePlatform != Device.WPF)
			{
				calendarPopupPage = new CalendarPopup();
				calendarPopupPage.DataChanged += CalendarPopupPage_DataChanged;
			}
		}

		/// <summary>
		/// Fonts the size property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void FontSizePropertyChanged(object oldValue, object newValue)
		{
			if (newValue != oldValue)
			{
				if (newValue is double value)
				{
					FontSize = value;
				}
			}
		}

		/// <summary>
		/// Labels the property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void LabelPropertyChanged(object oldValue, object newValue)
		{
			if (newValue != oldValue)
			{
				// Add the label
				if (label == null)
				{
					// Add extra row
					RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

					// Adjust other controls
					foreach (var childControl in Children)
					{
						SetRow(childControl, GetRow(childControl) + 1);
					}

					// Now create text label
					label = new Label
					{
						FontAttributes = FontAttributes,
						Margin = new Thickness(3, 0, 0, 0),
						Text = (string) newValue
					};

					SetGridRowColumn(label, 0, 0);
					Children.Add(label);

					// Do we need optional?
					if (ShowOptionalText)
					{
						CreateOptionalControl();
					}
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
			if (newValue != oldValue)
			{
				if (label != null)
				{
					label.FontAttributes = FontAttributes;
				}
			}
		}

		/// <summary>
		/// Maximums the date property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void MaximumDatePropertyChanged(object oldValue, object newValue)
		{
			if (newValue != oldValue)
			{
				calendarPopupPage.MaximumDate = (DateTime) newValue;
			}
		}

		/// <summary>
		/// Minimums the date property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void MinimumDatePropertyChanged(object oldValue, object newValue)
		{
			if (newValue != oldValue)
			{
				calendarPopupPage.MinimumDate = (DateTime) newValue;
			}
		}

		/// <summary>
		/// Dates the property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void SelectedDatePropertyChanged(object oldValue, object newValue)
		{
			// If we have a value, change it,
			if (newValue != oldValue && newValue != null)
			{
				var dateTime = (DateTime) newValue;
				dateLabel.Text = !string.IsNullOrWhiteSpace(Format)
					? dateTime.ToString(Format)
					: dateTime.ToShortDateString();
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
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void ShowErrorMessageVisiblePropertyChanged(object oldValue, object newValue)
		{
			if (newValue != oldValue)
			{
				if (newValue != null)
				{
					errorLabel.IsVisible = (bool) newValue;
				}
			}
		}

		/// <summary>
		/// Shows the optional text property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void ShowOptionalTextPropertyChanged(object oldValue, object newValue)
		{
			if (newValue != oldValue)
			{
				if ((bool) newValue)
				{
					CreateOptionalControl();
				}
			}
		}

		/// <summary>
		/// Titles the property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void TitlePropertyChanged(object oldValue, object newValue)
		{
			if (newValue != oldValue)
			{
				calendarPopupPage.TitleText = (string) newValue;
			}
		}

		/// <summary>
		/// Handles the Tapped event of the TapGestureRecognizer control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
		{
			if (calendarPopupPage != null && !PopupNavigation.Instance.PopupStack.Any(p => p.Equals(calendarPopupPage)))
			{
				await PopupNavigation.Instance.PushAsync(calendarPopupPage);
			}
		}

		/// <summary>
		/// Handles the ErrorsChanged event of the ValidateField control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="args">The <see cref="System.ComponentModel.DataErrorsChangedEventArgs"/> instance containing the event data.</param>
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
		/// <param name="oldvalue">The oldvalue.</param>
		/// <param name="newvalue">The newvalue.</param>
		private void ValidateFieldPropertyChanged(object oldvalue, object newvalue)
		{
			// If equal, do nothing
			if (oldvalue == newvalue)
			{
				return;
			}

			if (BindingContext is INotifyDataErrorInfo errorModel)
			{
				errorModel.ErrorsChanged -= ValidateField_ErrorsChanged;

				if (bool.TryParse(newvalue.ToString(), out var validatesOnDataErrors) && validatesOnDataErrors)
					errorModel.ErrorsChanged += ValidateField_ErrorsChanged;
			}
		}

		/// <summary>
		/// Validates the name of the property.
		/// </summary>
		/// <param name="validPicker">The valid entry.</param>
		/// <returns>System.String.</returns>
		private static string ValidatePropertyName(ValidDatePicker validPicker)
		{
			// Get binding
			validPicker.binding ??= validPicker.GetBinding(SelectedDateProperty);

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
				ValidateFieldPropertyChanged(null, ValidateField);
			}
		}
		#endregion

		#region Properties
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
		/// Gets or sets the label font attributes.
		/// </summary>
		/// <value>The label font attributes.</value>
		public FontAttributes FontAttributes
		{
			get => (FontAttributes) GetValue(LabelFontAttributesProperty);
			set => SetValue(LabelFontAttributesProperty, value);
		}

		/// <summary>
		/// Gets or sets the format.
		/// </summary>
		/// <value>The format.</value>
		[AlsoNotifyFor(nameof(SelectedDate))]
		public string Format
		{
			get => (string) GetValue(FormatProperty);
			set => SetValue(FormatProperty, value);
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
		/// Gets or sets the maximum date.
		/// </summary>
		/// <value>The maximum date.</value>
		public DateTime MaximumDate
		{
			get => (DateTime) GetValue(MaximumDateProperty);
			set => SetValue(MaximumDateProperty, value);
		}

		/// <summary>
		/// Gets or sets the minimum date.
		/// </summary>
		/// <value>The minimum date.</value>
		public DateTime MinimumDate
		{
			get => (DateTime) GetValue(MinimumDateProperty);
			set => SetValue(MinimumDateProperty, value);
		}

		/// <summary>
		/// Gets or sets the selected date.
		/// </summary>
		/// <value>The selected date.</value>
		public DateTime? SelectedDate
		{
			get => (DateTime?) GetValue(SelectedDateProperty);
			set => SetValue(SelectedDateProperty, value);
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
		/// Gets or sets a value indicating whether [validate field].
		/// </summary>
		/// <value><c>true</c> if [validate field]; otherwise, <c>false</c>.</value>
		public bool ValidateField
		{
			get => (bool) GetValue(ValidateFieldProperty);
			set => SetValue(ValidateFieldProperty, value);
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
		#endregion

		#region Fields
		private Binding binding;
		private CalendarPopup calendarPopupPage;
		private Label dateLabel;
		private Label errorLabel;
		private Label label;
		private Label optionalLabel;
		private TapGestureRecognizer tapGestureRecognizer;
		#endregion
	}
}