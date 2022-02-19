using Xamarin.Forms;
using Color = Xamarin.Forms.Color;

namespace FBS.XF.Toolkit.Controls
{
	/// <summary>
	/// Image Repeater.
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Frame" />
	public class ImageRepeater : StackLayout
	{
		#region Bindable Properties
		/// <summary>
		/// The image color property
		/// </summary>
		public static readonly BindableProperty ImageColorProperty =
			BindableProperty.Create(nameof(ImageColor), typeof(Color), typeof(ImageRepeater), Color.Default,
				propertyChanged: (bd, ov, nv)=> ((ImageRepeater) bd).ControlPropertyChanged(ov, nv));

		/// <summary>
		/// The image height property
		/// </summary>
		public static readonly BindableProperty ImageHeightProperty =
			BindableProperty.Create(nameof(ImageHeight), typeof(int), typeof(ImageRepeater), -1,
				propertyChanged: (bd, ov, nv)=> ((ImageRepeater) bd).ControlPropertyChanged(ov, nv));

		/// <summary>
		/// The image property
		/// </summary>
		public static readonly BindableProperty ImageProperty =
			BindableProperty.Create(nameof(Image), typeof(string), typeof(ImageRepeater), default(string),
				propertyChanged: (bd, ov, nv)=> ((ImageRepeater) bd).ControlPropertyChanged(ov, nv));

		/// <summary>
		/// The image width property
		/// </summary>
		public static readonly BindableProperty ImageWidthProperty =
			BindableProperty.Create(nameof(ImageWidth), typeof(int), typeof(ImageRepeater), -1,
				propertyChanged: (bd, ov, nv)=> ((ImageRepeater) bd).ControlPropertyChanged(ov, nv));

		/// <summary>
		/// The repeat property
		/// </summary>
		public static readonly BindableProperty RepeatProperty =
			BindableProperty.Create(nameof(Repeat), typeof(int), typeof(ImageRepeater), -1,
				propertyChanged: (bd, ov, nv)=> ((ImageRepeater) bd).ControlPropertyChanged(ov, nv));

		/// <summary>
		/// The WPF mode property
		/// </summary>
		public static readonly BindableProperty WPFModeProperty =
			BindableProperty.Create(nameof(WPFMode), typeof(WPFMode), typeof(CustomButton), WPFMode.None);
		#endregion

		#region Private methods
		/// <summary>
		/// Controls the property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void ControlPropertyChanged(object oldValue, object newValue)
		{
			// If the same, do nothing
			if (newValue == oldValue)
			{
				return;
			}

			// We need to have all the values
			if (ImageColor == Color.Default || ImageHeight == -1 || ImageWidth == 1 || Repeat == -1 || string.IsNullOrWhiteSpace(Image))
			{
				return;
			}

			// Remove any prior variant
			if (imageStack == null)
			{
				VerticalOptions = LayoutOptions.Center;

				imageStack = new StackLayout
				{
					HorizontalOptions = LayoutOptions.Center,
					Orientation = StackOrientation.Horizontal,
					Padding = 0,
					VerticalOptions = LayoutOptions.Center
				};

				Children.Add(imageStack);
			}
			else
			{
				imageStack.Children.Clear();
			}

			// Create control
			for (var r = 0; r < Repeat; r++)
			{
				// Create image and add
				var image = new SvgImage { Color = ImageColor, HeightRequest = ImageHeight, Source = Image, WidthRequest = ImageWidth, WPFMode = WPFMode };
				imageStack.Children.Add(image);
			}
		}
		#endregion

		#region Properties
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
		/// Gets or sets the width of the image.
		/// </summary>
		/// <value>The width of the image.</value>
		public int ImageWidth
		{
			get => (int) GetValue(ImageWidthProperty);
			set => SetValue(ImageWidthProperty, value);
		}

		/// <summary>
		/// Gets or sets the repeat.
		/// </summary>
		/// <value>The repeat.</value>
		public int Repeat
		{
			get => (int) GetValue(RepeatProperty);
			set => SetValue(RepeatProperty, value);
		}

		/// <summary>
		/// Gets or sets the WPF mode.
		/// </summary>
		/// <value>The WPF mode.</value>
		public WPFMode WPFMode
		{
			get => (WPFMode) GetValue(WPFModeProperty);
			set => SetValue(WPFModeProperty, value);
		}
		#endregion

		#region Fields
		private StackLayout imageStack;
		#endregion
	}
}