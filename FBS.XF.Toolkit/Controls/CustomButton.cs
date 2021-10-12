using System;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Controls
{
	/// <summary>
	/// Custom Button.
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Frame" />
	public class CustomButton : Frame
	{
		#region Events/Delegates
		public event EventHandler<ClickedEventArgs> Clicked;
		#endregion

		#region Bindable Properties
		/// <summary>
		/// The background color property
		/// </summary>
		public new static readonly BindableProperty BackgroundColorProperty =
			BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(CustomButton), Color.Default,
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).IsToggledPropertyChanged(ov, nv));

		/// <summary>
		/// The border color property
		/// </summary>
		public new static readonly BindableProperty BorderColorProperty =
			BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CustomButton), Color.Default,
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).IsToggledPropertyChanged(ov, nv));

		/// <summary>
		/// The font family property
		/// </summary>
		public static readonly BindableProperty FontFamilyProperty =
			BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(CustomButton), default(string),
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).FontFamilyPropertyChanged(ov, nv));

		/// <summary>
		/// The font size property
		/// </summary>
		public static readonly BindableProperty FontSizeProperty =
			BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CustomButton), default(double),
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).FontSizePropertyChanged(ov, nv));

		/// <summary>
		/// The horizontal image options property
		/// </summary>
		public static readonly BindableProperty HorizontalImageOptionsProperty =
			BindableProperty.Create(nameof(HorizontalImageOptions), typeof(LayoutOptions), typeof(CustomButton), LayoutOptions.Center,
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).HorizontalImageOptionsPropertyChanged(ov, nv));

		/// <summary>
		/// The horizontal text alignment property
		/// </summary>
		public static readonly BindableProperty HorizontalTextAlignmentProperty =
			BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(CustomButton), TextAlignment.Center,
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).HorizontalTextAlignmentPropertyChanged(ov, nv));

		/// <summary>
		/// The horizontal text options property
		/// </summary>
		public static readonly BindableProperty HorizontalTextOptionsProperty =
			BindableProperty.Create(nameof(HorizontalTextOptions), typeof(LayoutOptions), typeof(CustomButton), LayoutOptions.Center,
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).HorizontalTextOptionsPropertyChanged(ov, nv));

		/// <summary>
		/// The image color property
		/// </summary>
		public static readonly BindableProperty ImageColorProperty =
			BindableProperty.Create(nameof(ImageColor), typeof(Color), typeof(CustomButton), Color.Default,
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).ImageColorPropertyChanged(ov, nv));

		/// <summary>
		/// The image toggle color property
		/// </summary>
		public static readonly BindableProperty ImageToggleColorProperty =
			BindableProperty.Create(nameof(ImageToggleColor), typeof(Color), typeof(CustomButton), Color.Default,
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).IsToggledPropertyChanged(ov, nv));

		/// <summary>
		/// The image height property
		/// </summary>
		public static readonly BindableProperty ImageHeightProperty =
			BindableProperty.Create(nameof(ImageHeight), typeof(int), typeof(CustomButton), default(int),
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).ImageHeightPropertyChanged(ov, nv));

		/// <summary>
		/// The image WPF property
		/// </summary>
		public static readonly BindableProperty ImageWPFProperty =
			BindableProperty.Create(nameof(Image), typeof(string), typeof(CustomButton), default(string),
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).ImageSourceWPFPropertyChanged(ov, nv));

		/// <summary>
		/// The image property
		/// </summary>
		public static readonly BindableProperty ImageProperty =
			BindableProperty.Create(nameof(Image), typeof(string), typeof(CustomButton), default(string),
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).ImageSourcePropertyChanged(ov, nv));

		/// <summary>
		/// The image width property
		/// </summary>
		public static readonly BindableProperty ImageWidthProperty =
			BindableProperty.Create(nameof(ImageWidth), typeof(int), typeof(CustomButton), default(int),
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).ImageWidthPropertyChanged(ov, nv));

		/// <summary>
		/// The is toggled property
		/// </summary>
		public static readonly BindableProperty IsToggledProperty =
			BindableProperty.Create(nameof(IsToggled), typeof(bool), typeof(CustomButton), default(bool), BindingMode.TwoWay,
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).IsToggledPropertyChanged(ov, nv));

		/// <summary>
		/// The mode property
		/// </summary>
		public static readonly BindableProperty ModeProperty =
			BindableProperty.Create(nameof(ButtonMode), typeof(ButtonMode), typeof(CustomButton), ButtonMode.Normal);

		/// <summary>
		/// The number of taps
		/// </summary>
		public static readonly BindableProperty NumberOfTapsProperty =
			BindableProperty.Create(nameof(NumberOfTaps), typeof(int), typeof(CustomButton), 1,
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).NumberOfTapsPropertyChanged(ov, nv));

		/// <summary>
		/// The padding property
		/// </summary>
		public new static readonly BindableProperty PaddingProperty =
			BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(CustomButton), default(Thickness),
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).PaddingPropertyChanged(ov, nv));

		/// <summary>
		/// The toggle background color property
		/// </summary>
		public static readonly BindableProperty ToggleBackgroundColorProperty =
			BindableProperty.Create(nameof(ToggleBackgroundColor), typeof(Color), typeof(CustomButton), Color.Default,
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).IsToggledPropertyChanged(ov, nv));

		/// <summary>
		/// The toggle border color property
		/// </summary>
		public static readonly BindableProperty ToggleBorderColorProperty =
			BindableProperty.Create(nameof(ToggleBorderColor), typeof(Color), typeof(CustomButton), Color.Default,
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).IsToggledPropertyChanged(ov, nv));

		/// <summary>
		/// The text color property
		/// </summary>
		public static readonly BindableProperty TextColorProperty =
			BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CustomButton), Color.Default,
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).IsToggledPropertyChanged(ov, nv));

		/// <summary>
		/// The text location property
		/// </summary>
		public static readonly BindableProperty TextLocationProperty =
			BindableProperty.Create(nameof(TextLocationOption), typeof(TextLocationOption), typeof(CustomButton), TextLocationOption.AfterImage,
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).TextLocationPropertyChanged(ov, nv));

		/// <summary>
		/// The text margin property
		/// </summary>
		public static readonly BindableProperty TextMarginProperty =
			BindableProperty.Create(nameof(TextMargin), typeof(Thickness), typeof(CustomButton), default(Thickness),
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).TextMarginPropertyChanged(ov, nv));

		/// <summary>
		/// The text property
		/// </summary>
		public static readonly BindableProperty TextProperty =
			BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomButton), default(string),
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).TextPropertyChanged(ov, nv));

		/// <summary>
		/// The toggle image color property
		/// </summary>
		public static readonly BindableProperty ToggleImageColorProperty =
			BindableProperty.Create(nameof(ToggleImageColor), typeof(Color), typeof(CustomButton), Color.Default,
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).IsToggledPropertyChanged(ov, nv));

		/// <summary>
		/// The toggle text property property
		/// </summary>
		public static readonly BindableProperty ToggleTextProperty =
			BindableProperty.Create(nameof(ToggleText), typeof(string), typeof(CustomButton));

		/// <summary>
		/// The toggle text color property
		/// </summary>
		public static readonly BindableProperty ToggleTextColorProperty =
			BindableProperty.Create(nameof(ToggleTextColor), typeof(Color), typeof(CustomButton), Color.Default,
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).IsToggledPropertyChanged(ov, nv));

		/// <summary>
		/// The vertical image options property
		/// </summary>
		public static readonly BindableProperty VerticalImageOptionsProperty =
			BindableProperty.Create(nameof(VerticalImageOptions), typeof(LayoutOptions), typeof(CustomButton), LayoutOptions.Center,
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).VerticalImageOptionsPropertyChanged(ov, nv));

		/// <summary>
		/// The vertical text alignment property
		/// </summary>
		public static readonly BindableProperty VerticalTextAlignmentProperty =
			BindableProperty.Create(nameof(VerticalTextAlignment), typeof(TextAlignment), typeof(CustomButton), TextAlignment.Center,
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).VerticalTextAlignmentPropertyChanged(ov, nv));

		/// <summary>
		/// The vertical text options property
		/// </summary>
		public static readonly BindableProperty VerticalTextOptionsProperty =
			BindableProperty.Create(nameof(VerticalTextOptions), typeof(LayoutOptions), typeof(CustomButton), LayoutOptions.Center,
				propertyChanged: (bd, ov, nv) => ((CustomButton) bd).VerticalTextOptionsPropertyChanged(ov, nv));
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CustomButton"/> class.
		/// </summary>
		public CustomButton()
		{
			// Add control children
			if (Children.Count == 0)
			{
				CreateControl();
			}
		}
		#endregion

		#region UI methods
		/// <summary>
		/// Called when [toggle button touch down].
		/// </summary>
		private void Button_TouchDown()
		{
			if (IsEnabled)
			{
				Highlight(true);
			}
		}

		/// <summary>
		/// Called when [toggle button touch up].
		/// </summary>
		private void Button_TouchUp()
		{
			if (IsEnabled)
			{
				Highlight(false);

				if (Mode == ButtonMode.Toggle)
				{
					IsToggled = !IsToggled;
				}

				Clicked?.Invoke(this, null);
			}
		}

		/// <summary>
		/// Handles the Tapped event of the TapRecognizer control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void TapRecognizer_Tapped(object sender, EventArgs e)
		{
			if (IsEnabled)
			{
				// If WPF we have to fake the double tap
				if (Device.RuntimePlatform == Device.WPF && Mode != ButtonMode.Toggle && NumberOfTaps > 1)
				{
					var now = DateTime.Now;

					if (lastTapTime.HasValue && (now - lastTapTime.Value).Milliseconds < 300)
					{
						Clicked?.Invoke(this, null);
						lastTapTime = null;
						return;
					}

					lastTapTime = now;
					return;
				}

				Highlight(false);

				if (Mode == ButtonMode.Toggle)
				{
					IsToggled = !IsToggled;
				}

				Clicked?.Invoke(this, null);
			}
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Creates color with corrected brightness.
		/// </summary>
		/// <param name="color">Color to correct.</param>
		/// <param name="correctionFactor">The brightness correction factor. Must be between -1 and 1. 
		/// Negative values produce darker colors.</param>
		/// <returns>
		/// Corrected <see cref="Color"/> structure.
		/// </returns>
		private static Color ChangeColorTint(Color color, float correctionFactor)
		{
			if (color == Color.Transparent)
			{
				color = Application.Current.RequestedTheme == OSAppTheme.Dark ? Color.Black : Color.White;
			}

			var red = (float) color.R * 255;
			var green = (float) color.G * 255;
			var blue = (float) color.B * 255;

			if (correctionFactor < 0)
			{
				correctionFactor = 1 + correctionFactor;
				red += red * correctionFactor;
				green += green * correctionFactor;
				blue += blue * correctionFactor;
			}
			else
			{
				red -= red * correctionFactor;
				green -= green * correctionFactor;
				blue -= blue * correctionFactor;
			}

			// If value is greater than 255 flip then change
			if (red > 255)
			{
				red -= red - 255;
			}

			if (green > 255)
			{
				green -= green - 255;
			}

			if (blue > 255)
			{
				blue -= blue - 255;
			}

			var alpha = 255 * (float) color.A;
			return Color.FromRgba((int) red, (int) green, (int) blue, (int) alpha);
		}

		/// <summary>
		/// Creates the control.
		/// </summary>
		private void CreateControl()
		{
			// Frame parameters
			HasShadow = false;
			IsClippedToBounds = true;
			Padding = new Thickness(2);

			// Add tap gesture recognizer
			tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.Tapped += TapRecognizer_Tapped;

			// If WPF we have to fake the double tap
			if (Device.RuntimePlatform != Device.WPF)
			{
				tapGestureRecognizer.NumberOfTapsRequired = NumberOfTaps;
			}

			// Add it to the this
			GestureRecognizers.Add(tapGestureRecognizer);

			// Add touch gesture recognizer
			touchGestureRecognizer = new TouchGestureRecognizer();
			touchGestureRecognizer.TouchDown += Button_TouchDown;
			touchGestureRecognizer.TouchUp += Button_TouchUp;

			// Add it to the this
			GestureRecognizers.Add(touchGestureRecognizer);

			// Create the outer stack layout
			buttonStackLayout = new StackLayout
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			// Create image
			buttonImage = new SvgImage
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				IsVisible = false,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			buttonStackLayout.Children.Add(buttonImage);

			// Create label
			buttonLabel = new Label
			{
				FontSize = FontSize > 0 ? FontSize : Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				FontFamily = FontFamily,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				IsVisible = false,
				Text = "",
				TextColor = Color.White,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			buttonStackLayout.Children.Add(buttonLabel);

			// Add it
			Content = buttonStackLayout;
		}

		/// <summary>
		/// Font family property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void FontFamilyPropertyChanged(object oldValue, object newValue)
		{
			if (newValue != null && oldValue != newValue && buttonLabel != null)
			{
				buttonLabel.FontFamily = (string) newValue;
			}
		}

		/// <summary>
		/// Font size property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void FontSizePropertyChanged(object oldValue, object newValue)
		{
			if (newValue != null && oldValue != newValue && buttonLabel != null)
			{
				buttonLabel.FontSize = (double) newValue;
			}
		}

		/// <summary>
		/// Highlights the specified is highlighted.
		/// </summary>
		/// <param name="isHighlighted">if set to <c>true</c> [is highlighted].</param>
		private void Highlight(bool isHighlighted)
		{
			if (isHighlighted)
			{
				base.BackgroundColor = ChangeColorTint(BackgroundColor, 0.19F);
				base.BorderColor = ChangeColorTint(BorderColor, 0.19F);
				buttonLabel.TextColor = ChangeColorTint(TextColor, 0.19F);
			}
			else
			{
				base.BackgroundColor = BackgroundColor;
				base.BorderColor = BorderColor;
				buttonLabel.TextColor = TextColor;
			}
		}

		/// <summary>
		/// Horizontals the image options property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void HorizontalImageOptionsPropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue)
			{
				buttonImage.HorizontalOptions = (LayoutOptions) newValue;
			}
		}

		/// <summary>
		/// Horizontals the text alignment property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void HorizontalTextAlignmentPropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue)
			{
				buttonLabel.HorizontalTextAlignment = (TextAlignment) newValue;
			}
		}

		/// <summary>
		/// Horizontals the text options property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void HorizontalTextOptionsPropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue)
			{
				buttonLabel.HorizontalOptions = (LayoutOptions) newValue;
			}
		}

		/// <summary>
		/// Images the color property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void ImageColorPropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue && buttonImage is SvgImage svgImage)
			{
				svgImage.Color = (Color) newValue;
			}
		}

		/// <summary>
		/// Images the height property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void ImageHeightPropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue)
			{
				buttonImage.HeightRequest = (int) newValue;
			}
		}

		/// <summary>
		/// Images the source WPF property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void ImageSourceWPFPropertyChanged(object oldValue, object newValue)
		{
			if (newValue != oldValue)
			{
				if (Device.RuntimePlatform == Device.WPF)
				{
					// We know the orginal image would be an SVG image, so let's change to PNG
					var image = new Image
					{
						HeightRequest = ImageHeight,
						HorizontalOptions = LayoutOptions.Center,
						Source = newValue.ToString(),
						IsVisible = true,
						VerticalOptions = LayoutOptions.Center,
						WidthRequest = ImageHeight
					};

					// Swap
					buttonImage = image;
					SetLayout(this, TextLocation);
				}
			}
		}

		/// <summary>
		/// Images the source SVG property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void ImageSourcePropertyChanged(object oldValue, object newValue)
		{
			if (newValue != oldValue && Device.RuntimePlatform != Device.WPF)
			{
				if (buttonImage is SvgImage svgImage)
				{
					svgImage.Color = ImageColor;
					svgImage.HeightRequest = ImageHeight;
					svgImage.Source = newValue.ToString();
					svgImage.IsVisible = true;
					svgImage.WidthRequest = ImageHeight;
					SetLayout(this, TextLocation);
				}
			}
		}

		/// <summary>
		/// Images the width property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void ImageWidthPropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue)
			{
				buttonImage.WidthRequest = (int) newValue;
			}
		}

		/// <summary>
		/// Determines whether [is toggled property changed] [the specified bindable].
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void IsToggledPropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue)
			{
				if (IsToggled)
				{
					base.BackgroundColor = ToggleBackgroundColor;
					base.BorderColor = ToggleBorderColor;
					buttonLabel.TextColor = ToggleTextColor;

					if (buttonImage is SvgImage svgImage)
					{
						svgImage.Color = ImageToggleColor;
					}

					if (buttonLabel != null)
					{
						buttonLabel.Text = string.IsNullOrWhiteSpace(ToggleText) ? Text : ToggleText;
					}
				}
				else
				{
					base.BackgroundColor = BackgroundColor;
					base.BorderColor = BorderColor;
					buttonLabel.TextColor = TextColor;

					if (buttonImage is SvgImage svgImage)
					{
						svgImage.Color = ImageColor;
					}

					if (buttonLabel != null)
					{
						buttonLabel.Text = Text;
					}
				}
			}
		}

		/// <summary>
		/// Numbers the of taps property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void NumberOfTapsPropertyChanged(object oldValue, object newValue)
		{
			if (newValue != oldValue && Device.RuntimePlatform != Device.WPF)
			{
				tapGestureRecognizer.NumberOfTapsRequired = NumberOfTaps;

				if (NumberOfTaps != 1)
				{
					GestureRecognizers.Remove(touchGestureRecognizer);
					touchGestureRecognizer = null;
				}
			}
		}

		/// <summary>
		/// Paddings the property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void PaddingPropertyChanged(object oldValue, object newValue)
		{
			if (newValue != oldValue)
			{
				SetPadding((Thickness) newValue);
			}
		}

		/// <summary>
		/// Sets the layout.
		/// </summary>
		/// <param name="button">The button.</param>
		/// <param name="location">The location.</param>
		private static void SetLayout(CustomButton button, TextLocationOption location)
		{
			if (location.Equals(TextLocationOption.AboveImage))
			{
				button.buttonStackLayout.Orientation = StackOrientation.Vertical;
				button.buttonStackLayout.Children.Clear();
				button.buttonStackLayout.Children.Add(button.buttonLabel);
				button.buttonStackLayout.Children.Add(button.buttonImage);
			}
			else if (location.Equals(TextLocationOption.BelowImage))
			{
				button.buttonStackLayout.Orientation = StackOrientation.Vertical;
				button.buttonStackLayout.Children.Clear();
				button.buttonStackLayout.Children.Add(button.buttonImage);
				button.buttonStackLayout.Children.Add(button.buttonLabel);
			}
			else if (location.Equals(TextLocationOption.BeforeImage))
			{
				button.buttonStackLayout.Orientation = StackOrientation.Horizontal;
				button.buttonStackLayout.Children.Clear();
				button.buttonStackLayout.Children.Add(button.buttonLabel);
				button.buttonStackLayout.Children.Add(button.buttonImage);
			}
			else
			{
				button.buttonStackLayout.Orientation = StackOrientation.Horizontal;
				button.buttonStackLayout.Children.Clear();
				button.buttonStackLayout.Children.Add(button.buttonImage);
				button.buttonStackLayout.Children.Add(button.buttonLabel);
			}
		}

		/// <summary>
		/// Sets the padding.
		/// </summary>
		/// <param name="padding">The padding.</param>
		private void SetPadding(Thickness padding)
		{
			base.Padding = padding;
		}

		/// <summary>
		/// Text property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void TextPropertyChanged(object oldValue, object newValue)
		{
			if (newValue != oldValue)
			{
				buttonLabel.Text = IsToggled ? ToggleText : Text;
				buttonLabel.IsVisible = true;
			}
			else if (newValue == null)
			{
				buttonLabel.IsVisible = false;
			}
		}

		/// <summary>
		/// Texts the location property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void TextLocationPropertyChanged(object oldValue, object newValue)
		{
			if (newValue != oldValue)
			{
				SetLayout(this, (TextLocationOption) newValue);
			}
		}

		/// <summary>
		/// Texts the margin property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void TextMarginPropertyChanged(object oldValue, object newValue)
		{
			if (newValue != oldValue)
			{
				buttonLabel.Margin = (Thickness) newValue;
			}
		}

		/// <summary>
		/// Verticals the image options property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void VerticalImageOptionsPropertyChanged(object oldValue, object newValue)
		{
			if (newValue != oldValue)
			{
				buttonImage.VerticalOptions = (LayoutOptions) newValue;
			}
		}

		/// <summary>
		/// Verticals the text alignment property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void VerticalTextAlignmentPropertyChanged(object oldValue, object newValue)
		{
			if (newValue != oldValue)
			{
				buttonLabel.VerticalTextAlignment = (TextAlignment) newValue;
			}
		}

		/// <summary>
		/// Verticals the text options property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void VerticalTextOptionsPropertyChanged(object oldValue, object newValue)
		{
			if (newValue != oldValue)
			{
				buttonLabel.VerticalOptions = (LayoutOptions) newValue;
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
		/// Gets or sets the border color for the frame.
		/// </summary>
		/// <value>The border color for the frame.</value>
		/// <remarks>To be added.</remarks>
		public new Color BorderColor
		{
			get => (Color) GetValue(BorderColorProperty);
			set => SetValue(BorderColorProperty, value);
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
		/// Gets or sets the horizontal image options.
		/// </summary>
		/// <value>The horizontal image options.</value>
		public LayoutOptions HorizontalImageOptions
		{
			get => (LayoutOptions) GetValue(HorizontalImageOptionsProperty);
			set => SetValue(HorizontalImageOptionsProperty, value);
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
		/// Gets or sets the horizontal text options.
		/// </summary>
		/// <value>The horizontal text options.</value>
		public LayoutOptions HorizontalTextOptions
		{
			get => (LayoutOptions) GetValue(HorizontalTextOptionsProperty);
			set => SetValue(HorizontalTextOptionsProperty, value);
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
		/// Gets or sets the height of the image.
		/// </summary>
		/// <value>The height of the image.</value>
		public int ImageHeight
		{
			get => (int) GetValue(ImageHeightProperty);
			set => SetValue(ImageHeightProperty, value);
		}

		/// <summary>
		/// Gets or sets the image.
		/// </summary>
		/// <value>The image.</value>
		public string Image
		{
			get => (string) GetValue(ImageProperty);
			set => SetValue(ImageProperty, value);
		}

		/// <summary>
		/// Gets or sets the color of the image toggle.
		/// </summary>
		/// <value>The color of the image toggle.</value>
		public Color ImageToggleColor
		{
			get => (Color) GetValue(ImageToggleColorProperty);
			set => SetValue(ImageToggleColorProperty, value);
		}

		/// <summary>
		/// Gets or sets the image WPF.
		/// </summary>
		/// <value>The image WPF.</value>
		public string ImageWPF
		{
			get => (string) GetValue(ImageWPFProperty);
			set => SetValue(ImageWPFProperty, value);
		}

		/// <summary>
		/// Gets or sets the width of the image.
		/// </summary>
		/// <value>The width of the image.</value>
		public int ImageWidth
		{
			get => (int) GetValue(ImageWidthProperty);
			set => SetValue(ImageWidthProperty, value);
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is toggled.
		/// </summary>
		/// <value><c>true</c> if this instance is toggled; otherwise, <c>false</c>.</value>
		public bool IsToggled
		{
			get => (bool) GetValue(IsToggledProperty);
			set => SetValue(IsToggledProperty, value);
		}

		/// <summary>
		/// Gets or sets the toggle mode.
		/// </summary>
		/// <value>The toggle mode.</value>
		public ButtonMode Mode
		{
			get => (ButtonMode) GetValue(ModeProperty);
			set => SetValue(ModeProperty, value);
		}

		/// <summary>
		/// Gets or sets the number of taps.
		/// </summary>
		/// <value>The number of taps.</value>
		public int NumberOfTaps
		{
			get => (int) GetValue(NumberOfTapsProperty);
			set => SetValue(NumberOfTapsProperty, value);
		}

		/// <summary>
		/// Gets or sets the inner padding of the Layout.
		/// </summary>
		/// <value>The Thickness values for the layout. The default value is a Thickness with all values set to 0.</value>
		public new Thickness Padding
		{
			get => (Thickness) GetValue(PaddingProperty);
			set => SetValue(PaddingProperty, value);
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
		/// Gets or sets the text location.
		/// </summary>
		/// <value>The text location.</value>
		public TextLocationOption TextLocation
		{
			get => (TextLocationOption) GetValue(TextLocationProperty);
			set => SetValue(TextLocationProperty, value);
		}

		/// <summary>
		/// Gets or sets the text margin.
		/// </summary>
		/// <value>The text margin.</value>
		public Thickness TextMargin
		{
			get => (Thickness) GetValue(TextMarginProperty);
			set => SetValue(TextMarginProperty, value);
		}

		/// <summary>
		/// Gets or sets the color of the toggle background.
		/// </summary>
		/// <value>The color of the toggle background.</value>
		public Color ToggleBackgroundColor
		{
			get => (Color) GetValue(ToggleBackgroundColorProperty);
			set => SetValue(ToggleBackgroundColorProperty, value);
		}

		/// <summary>
		/// Gets or sets the color of the toggle border.
		/// </summary>
		/// <value>The color of the toggle border.</value>
		public Color ToggleBorderColor
		{
			get => (Color) GetValue(ToggleBorderColorProperty);
			set => SetValue(ToggleBorderColorProperty, value);
		}

		/// <summary>
		/// Gets or sets the color of the toggle image.
		/// </summary>
		/// <value>The color of the toggle image.</value>
		public Color ToggleImageColor
		{
			get => (Color) GetValue(ToggleImageColorProperty);
			set => SetValue(ToggleImageColorProperty, value);
		}

		/// <summary>
		/// Gets or sets the toggle text.
		/// </summary>
		/// <value>The toggle text.</value>
		public string ToggleText
		{
			get => (string) GetValue(ToggleTextProperty);
			set => SetValue(ToggleTextProperty, value);
		}

		/// <summary>
		/// Gets or sets the color of the toggle text.
		/// </summary>
		/// <value>The color of the toggle text.</value>
		public Color ToggleTextColor
		{
			get => (Color) GetValue(ToggleTextColorProperty);
			set => SetValue(ToggleTextColorProperty, value);
		}

		/// <summary>
		/// Gets or sets the vertical image options.
		/// </summary>
		/// <value>The vertical image options.</value>
		public LayoutOptions VerticalImageOptions
		{
			get => (LayoutOptions) GetValue(VerticalImageOptionsProperty);
			set => SetValue(VerticalImageOptionsProperty, value);
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

		/// <summary>
		/// Gets or sets the vertical text options.
		/// </summary>
		/// <value>The vertical text options.</value>
		public LayoutOptions VerticalTextOptions
		{
			get => (LayoutOptions) GetValue(VerticalTextOptionsProperty);
			set => SetValue(VerticalTextOptionsProperty, value);
		}
		#endregion

		#region Fields
		//private SvgImage buttonSvgImage;
		private Image buttonImage;
		private Label buttonLabel;
		private StackLayout buttonStackLayout;
		private DateTime? lastTapTime; 
		private TapGestureRecognizer tapGestureRecognizer;
		private TouchGestureRecognizer touchGestureRecognizer;
		#endregion
	}
}