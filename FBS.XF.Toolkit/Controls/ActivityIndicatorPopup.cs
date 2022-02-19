using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Controls
{
	/// <summary>
	/// ActivityIndicatorPopup.
	/// Implements the <see cref="Xamarin.Forms.ContentView" />
	/// </summary>
	/// <seealso cref="Xamarin.Forms.ContentView" />
	public class ActivityIndicatorPopup : ContentView
	{
		#region Bindable properties
		/// <summary>
		/// The background color property
		/// </summary>
		public new static readonly BindableProperty BackgroundColorProperty =
			BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(CustomButton), Color.Default,
				propertyChanged: BackgroundColorPropertyChanged);

		/// <summary>
		/// The font family property
		/// </summary>
		public static readonly BindableProperty FontFamilyProperty =
			BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(CustomButton), default(string),
				propertyChanged: FontFamilyPropertyChanged);

		/// <summary>
		/// The font size property
		/// </summary>
		public static readonly BindableProperty FontSizeProperty =
			BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CustomButton), default(double),
				propertyChanged: FontSizePropertyChanged);

		/// <summary>
		/// The image color property
		/// </summary>
		public static readonly BindableProperty ImageColorProperty =
			BindableProperty.Create(nameof(ImageColor), typeof(Color), typeof(CustomButton), Color.Default,
				propertyChanged: ImageColorPropertyChanged);

		/// <summary>
		/// The image property
		/// </summary>
		public static readonly BindableProperty ImageProperty =
			BindableProperty.Create(nameof(Image), typeof(string), typeof(CustomButton), default(string),
				propertyChanged: ImagePropertyChanged);

		/// <summary>
		/// The is running property
		/// </summary>
		public static readonly BindableProperty IsRunningProperty =
			BindableProperty.Create(nameof(IsRunning), typeof(bool), typeof(CustomButton), default(bool),
				BindingMode.TwoWay, propertyChanged: IsRunningPropertyChanged);

		/// <summary>
		/// The text property
		/// </summary>
		public static readonly BindableProperty TextProperty =
			BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomButton), default(string),
				propertyChanged: TextPropertyChanged);

		/// <summary>
		/// The text color property
		/// </summary>
		public static readonly BindableProperty TextColorProperty =
			BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CustomButton), Color.Default,
				propertyChanged: TextColorPropertyChanged);
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActivityIndicatorPopup"/> class.
		/// </summary>
		public ActivityIndicatorPopup()
		{
			CreateControl();
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Backgrounds the color property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void BackgroundColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue != oldValue)
			{
				((ActivityIndicatorPopup) bindable).stackLayout.BackgroundColor = (Color) newValue;
			}
		}

		/// <summary>
		/// Creates the control.
		/// </summary>
		private void CreateControl()
		{
			// Do not allow anyone to kill this
			popupPage = new PopupPage {CloseWhenBackgroundIsClicked = false};

			// Add stack layout
			stackLayout = new StackLayout
			{
				HeightRequest = 140,
				HorizontalOptions = LayoutOptions.Center,
				Padding = new Thickness(30, 20, 30, 20),
				VerticalOptions = LayoutOptions.Center,
				WidthRequest = 240
			};

			popupPage.Content = stackLayout;

			// Add label
			label = new Label
			{
				FontSize = 26,
				HorizontalOptions = LayoutOptions.Center,
				Text = "Activity"
			};

			stackLayout.Children.Add(label);

			// Add label
			activityIndicator = new ActivityIndicator
			{
				IsRunning = true,
				Margin = new Thickness(0, 20, 0, 0),
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			stackLayout.Children.Add(activityIndicator);
		}

		/// <summary>
		/// Font family property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void FontFamilyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue != oldValue)
			{
				((ActivityIndicatorPopup) bindable).label.FontFamily = (string) newValue;
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
			if (newValue != oldValue)
			{
				((ActivityIndicatorPopup) bindable).label.FontSize = (double) newValue;
			}
		}

		/// <summary>
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void ImagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue != oldValue)
			{
				// Hide activity indicator
				// Add svgImage with image, what about color, set to same size as activity indicator
			}
		}

		/// <summary>
		/// Images the color property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void ImageColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue != oldValue)
			{
				// Hide activity indicator
				// Add svgImage with image if we have it and color, set to same size as activity indicator
			}
		}

		/// <summary>
		/// Determines whether [is running property changed] [the specified bindable].
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static async void IsRunningPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue != oldValue)
			{
				var isrunning = (bool) newValue;
				var control = (ActivityIndicatorPopup) bindable;
				control.activityIndicator.IsRunning = isrunning;

				if (isrunning)
				{
					control.activityIndicator.IsRunning = isrunning;

					if (Device.RuntimePlatform != Device.WPF)
					{
						await PopupNavigation.Instance.PushAsync(control.popupPage);
					}
				}
				else
				{
					if (Device.RuntimePlatform != Device.WPF)
					{
						await PopupNavigation.Instance.PopAsync();
					}
				}
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
			if (newValue != oldValue)
			{
				((ActivityIndicatorPopup) bindable).label.TextColor = (Color) newValue;
			}
		}

		/// <summary>
		/// Text property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void TextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue != oldValue)
			{
				((ActivityIndicatorPopup) bindable).label.Text = (string) newValue;
			}
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
		[TypeConverter(typeof(FontSizeConverter))]
		public double FontSize
		{
			get => (double) GetValue(FontSizeProperty);
			set => SetValue(FontSizeProperty, value);
		}

		/// <summary>
		/// Gets or sets the image.
		/// </summary>
		/// <value>The image.</value>
		public string Image
		{
			get => GetValue(ImageProperty).ToString();
			set => SetValue(ImageProperty, value);
		}

		/// <summary>
		/// Gets or sets the color of the image.
		/// </summary>
		/// <value>The color of the image.</value>
		public Color ImageColor
		{
			get => (Color) GetValue(ImageColorProperty);
			set => SetValue(ImageColorProperty, value);
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is running.
		/// </summary>
		/// <value><c>true</c> if this instance is running; otherwise, <c>false</c>.</value>
		public bool IsRunning
		{
			get => (bool) GetValue(IsRunningProperty);
			set => SetValue(IsRunningProperty, value);
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
		#endregion

		#region Fields
		private ActivityIndicator activityIndicator;
		private Label label;
		private PopupPage popupPage;
		private StackLayout stackLayout;
		#endregion
	}
}