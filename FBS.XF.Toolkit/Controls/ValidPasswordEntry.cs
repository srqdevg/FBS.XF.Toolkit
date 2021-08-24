using System;
using PropertyChanged;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Controls
{
	/// <summary>
	/// Valid Password Entry.
	/// </summary>
	[AddINotifyPropertyChangedInterface]
	public class ValidPasswordEntry : ValidEntry
	{
		#region Constants/Enumerations
		#endregion

		#region Bindable properties
		/// <summary>
		/// The password lower error property
		/// </summary>
		public static readonly BindableProperty PasswordLowerErrorProperty =
			BindableProperty.Create(nameof(PasswordLowerError), typeof(bool), typeof(ValidPasswordEntry), default(bool),
				propertyChanged: PasswordLowerErrorPropertyChanged);

		/// <summary>
		/// The password minimum length property
		/// </summary>
		public static readonly BindableProperty PasswordMinLengthProperty =
			BindableProperty.Create(nameof(PasswordMinLength), typeof(int), typeof(ValidPasswordEntry), default(int),
				BindingMode.TwoWay, propertyChanged: PasswordMinLengthPropertyChanged);

		/// <summary>
		/// The password minimum length error property
		/// </summary>
		public static readonly BindableProperty PasswordMinLengthErrorProperty =
			BindableProperty.Create(nameof(PasswordMinLengthError), typeof(bool), typeof(ValidPasswordEntry), default(bool),
				propertyChanged: PasswordMinLengthErrorPropertyChanged);

		/// <summary>
		/// The password numeric error property
		/// </summary>
		public static readonly BindableProperty PasswordNumericErrorProperty =
			BindableProperty.Create(nameof(PasswordNumericError), typeof(bool), typeof(ValidPasswordEntry), default(bool),
				propertyChanged: PasswordNumericErrorPropertyChanged);

		/// <summary>
		/// The password symbol error property
		/// </summary>
		public static readonly BindableProperty PasswordSymbolErrorProperty =
			BindableProperty.Create(nameof(PasswordSymbolError), typeof(bool), typeof(ValidPasswordEntry), default(bool),
				propertyChanged: PasswordSymbolErrorPropertyChanged);

		/// <summary>
		/// The password upper error property
		/// </summary>
		public static readonly BindableProperty PasswordUpperErrorProperty =
			BindableProperty.Create(nameof(PasswordUpperError), typeof(bool), typeof(ValidPasswordEntry), default(bool),
				propertyChanged: PasswordUpperErrorPropertyChanged);

		/// <summary>
		/// The show password rules property
		/// </summary>
		public static readonly BindableProperty ShowPasswordRulesProperty =
			BindableProperty.Create(nameof(ShowPasswordRules), typeof(bool), typeof(ValidPasswordEntry), default(bool),
				BindingMode.TwoWay, propertyChanged: ShowPasswordRulesPropertyChanged);
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ValidPasswordEntry"/> class.
		/// </summary>
		public ValidPasswordEntry() : base(Keyboard.Default, true)
		{
			// Initial setup
			PasswordMinLength = 6;
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Creates the image text block.
		/// </summary>
		/// <param name="ruleGrid">The rule grid.</param>
		/// <param name="row">The row.</param>
		/// <param name="column">The column.</param>
		/// <param name="text">The text.</param>
		/// <returns>Label.</returns>
		private Tuple<Label, Image> CreateImageTextBlock(Grid ruleGrid, int row, int column, string text)
		{
			var image = new Image {Aspect = Aspect.AspectFit, Source = "Tick"};
			SetGridRowColumn(image, row, column);
			ruleGrid.Children.Add(image);

			var label = new Label {FontSize = 10, Text = text};
			SetGridRowColumn(label, row, column + 1);
			ruleGrid.Children.Add(label);

			return new Tuple<Label, Image>(label, image);
		}

		/// <summary>
		/// Creates the text entry.
		/// </summary>
		private void CreateRulesPasswordEntry()
		{
			// Get max rows
			var row = RowDefinitions.Count;

			// Create grid for rules
			var ruleGrid = new Grid {Margin = new Thickness(10, -5, 10, 15)};
			ruleGrid.RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});
			ruleGrid.RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});
			ruleGrid.RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});
			ruleGrid.ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Auto});
			ruleGrid.ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Star});
			ruleGrid.ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Auto});
			ruleGrid.ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Star});

			// Bind grid visible context to isfocused
			ruleGrid.BindingContext = this;
			ruleGrid.SetBinding(IsVisibleProperty, "IsFocused", BindingMode.OneWay);

			// Add grid to parent
			RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});
			SetGridRowColumn(ruleGrid, row, 0, 0, 2);
			Children.Add(ruleGrid);

			// Start row for each column
			var rowC1 = 0;
			var rowC2 = 0;

			// Add each rule, image and text
			if (ShowPasswordRules)
			{
				(minLengthLabel, minLengthImage) = CreateImageTextBlock(ruleGrid, rowC1++, 0, $"Minimum {PasswordMinLength} character");
				(_, upperImage) = CreateImageTextBlock(ruleGrid, rowC1++, 0, "Uppercase letters (A-Z)");
				(_, lowerImage) = CreateImageTextBlock(ruleGrid, rowC1, 0, "Lowercase letters (a-z)");
				(_, numericImage) = CreateImageTextBlock(ruleGrid, rowC2++, 2, "Numbers (0-9)");
				(_, symbolImage) = CreateImageTextBlock(ruleGrid, rowC2, 2, "Symbols (...)");
			}
		}

		/// <summary>
		/// Images the change.
		/// </summary>
		/// <param name="image">The image.</param>
		/// <param name="value">if set to <c>true</c> [value].</param>
		private static void ImageChange(Image image, bool value)
		{
			if (image != null)
			{
				image.Source = value ? "Cross" : "Tick";
			}
		}

		/// <summary>
		/// Passwords the lower error property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The oldValue.</param>
		/// <param name="newValue">The oldValue.</param>
		private static void PasswordLowerErrorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (((ValidPasswordEntry) bindable).lowerImage != null)
			{
				ImageChange(((ValidPasswordEntry) bindable).lowerImage, (bool) newValue);
			}
		}

		/// <summary>
		/// Passwords the minimum length property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The oldValue.</param>
		/// <param name="newValue">The oldValue.</param>
		private static void PasswordMinLengthPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (((ValidPasswordEntry) bindable).minLengthLabel != null)
			{
				((ValidPasswordEntry) bindable).minLengthLabel.Text = $"Minimum {(int) newValue} characters";
			}
		}

		/// <summary>
		/// Passwords the minimum length error property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The oldValue.</param>
		/// <param name="newValue">The oldValue.</param>
		private static void PasswordMinLengthErrorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (((ValidPasswordEntry) bindable).minLengthImage != null)
			{
				ImageChange(((ValidPasswordEntry) bindable).minLengthImage, (bool) newValue);
			}
		}

		/// <summary>
		/// Passwords the numeric error property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The oldValue.</param>
		/// <param name="newValue">The oldValue.</param>
		private static void PasswordNumericErrorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (((ValidPasswordEntry) bindable).numericImage != null)
			{
				ImageChange(((ValidPasswordEntry) bindable).numericImage, (bool) newValue);
			}
		}

		/// <summary>
		/// Passwords the symbol error property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The oldValue.</param>
		/// <param name="newValue">The oldValue.</param>
		private static void PasswordSymbolErrorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (((ValidPasswordEntry) bindable).symbolImage != null)
			{
				ImageChange(((ValidPasswordEntry) bindable).symbolImage, (bool) newValue);
			}
		}

		/// <summary>
		/// Passwords the upper error property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The oldValue.</param>
		/// <param name="newValue">The oldValue.</param>
		private static void PasswordUpperErrorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (((ValidPasswordEntry) bindable).upperImage != null)
			{
				ImageChange(((ValidPasswordEntry) bindable).upperImage, (bool) newValue);
			}
		}

		/// <summary>
		/// Shows the password rules property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The oldValue.</param>
		/// <param name="newValue">The oldValue.</param>
		private static void ShowPasswordRulesPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			// Add extra controls?
			if ((bool) newValue)
			{
				((ValidPasswordEntry) bindable).CreateRulesPasswordEntry();
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets a value indicating whether [password lower error].
		/// </summary>
		/// <value><c>true</c> if [password lower error]; otherwise, <c>false</c>.</value>
		public bool PasswordLowerError
		{
			get => (bool) GetValue(PasswordLowerErrorProperty);
			set => SetValue(PasswordLowerErrorProperty, value);
		}

		/// <summary>
		/// Gets or sets the minimum length of the password.
		/// </summary>
		/// <value>The minimum length of the password.</value>
		public int PasswordMinLength
		{
			get => (int) GetValue(PasswordMinLengthProperty);
			set => SetValue(PasswordMinLengthProperty, value);
		}

		/// <summary>
		/// Gets or sets a value indicating whether [password minimum length error].
		/// </summary>
		/// <value><c>true</c> if [password minimum length error]; otherwise, <c>false</c>.</value>
		public bool PasswordMinLengthError
		{
			get => (bool) GetValue(PasswordMinLengthErrorProperty);
			set => SetValue(PasswordMinLengthErrorProperty, value);
		}

		/// <summary>
		/// Gets or sets a value indicating whether [password numeric error].
		/// </summary>
		/// <value><c>true</c> if [password numeric error]; otherwise, <c>false</c>.</value>
		public bool PasswordNumericError
		{
			get => (bool) GetValue(PasswordNumericErrorProperty);
			set => SetValue(PasswordNumericErrorProperty, value);
		}

		/// <summary>
		/// Gets or sets a value indicating whether [password symbol error].
		/// </summary>
		/// <value><c>true</c> if [password symbol error]; otherwise, <c>false</c>.</value>
		public bool PasswordSymbolError
		{
			get => (bool) GetValue(PasswordSymbolErrorProperty);
			set => SetValue(PasswordSymbolErrorProperty, value);
		}

		/// <summary>
		/// Gets or sets a value indicating whether [password upper error].
		/// </summary>
		/// <value><c>true</c> if [password upper error]; otherwise, <c>false</c>.</value>
		public bool PasswordUpperError
		{
			get => (bool) GetValue(PasswordUpperErrorProperty);
			set => SetValue(PasswordUpperErrorProperty, value);
		}

		/// <summary>
		/// Gets or sets a value indicating whether [show password rules].
		/// </summary>
		/// <value><c>true</c> if [show password rules]; otherwise, <c>false</c>.</value>
		public bool ShowPasswordRules
		{
			get => (bool) GetValue(ShowPasswordRulesProperty);
			set => SetValue(ShowPasswordRulesProperty, value);
		}
		#endregion

		#region Fields
		private Image lowerImage;
		private Image minLengthImage;
		private Label minLengthLabel;
		private Image numericImage;
		private Image symbolImage;
		private Image upperImage;
		#endregion
	}
}