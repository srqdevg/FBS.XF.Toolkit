using System;
using System.Collections.Generic;
using System.Linq;
using FBS.XF.Toolkit.Images;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Controls
{
	/// <summary>
	/// SvgImage
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Image" />
	public class SvgImage : Image
	{
		#region Bindable Properties
		/// <summary>
		/// The color property
		/// </summary>
		public static readonly BindableProperty ColorProperty =
			BindableProperty.Create(nameof(Color), typeof(Color), typeof(SvgImage), Color.Default,
				propertyChanged: (bd, ov, nv) => ((SvgImage) bd).ColorPropertyChanged(ov, nv));

		/// <summary>
		/// The is selected property
		/// </summary>
		public static readonly BindableProperty IsSelectedProperty =
			BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(SvgImage), false,
				propertyChanged: (bd, ov, nv) => ((SvgImage) bd).IsSelectedPropertyChanged(ov, nv));

		/// <summary>
		/// The selected color property
		/// </summary>
		public static readonly BindableProperty SelectedColorProperty =
			BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(SvgImage), Color.Default,
				propertyChanged: (bd, ov, nv) => ((SvgImage) bd).SelectedColorPropertyChanged(ov, nv));

		/// <summary>
		/// The selected source property
		/// </summary>
		public static readonly BindableProperty SelectedSourceProperty =
			BindableProperty.Create(nameof(SelectedSource), typeof(string), typeof(SvgImage), null,
				propertyChanged: (bd, ov, nv) => ((SvgImage) bd).SourcePropertyChanged(ov, nv));

		/// <summary>
		/// The selected source property
		/// </summary>
		public static readonly BindableProperty SelectedSourceWPFProperty =
			BindableProperty.Create(nameof(SelectedSourceWPF), typeof(string), typeof(SvgImage), null,
				propertyChanged: (bd, ov, nv) => ((SvgImage) bd).SourcePropertyChanged(ov, nv));

		/// <summary>
		/// The source property
		/// </summary>
		public new static readonly BindableProperty SourceProperty =
			BindableProperty.Create(nameof(Source), typeof(string), typeof(SvgImage), null,
				propertyChanged: (bd, ov, nv) => ((SvgImage) bd).SourcePropertyChanged(ov, nv));

		/// <summary>
		/// The source property
		/// </summary>
		public static readonly BindableProperty SourceWPFProperty =
			BindableProperty.Create(nameof(SourceWPF), typeof(string), typeof(SvgImage), null,
				propertyChanged: (bd, ov, nv) => ((SvgImage) bd).SourcePropertyChanged(ov, nv));
		#endregion

		#region Private methods
		/// <summary>
		/// Sources the property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void ColorPropertyChanged(object oldValue, object newValue)
		{
			/// Do we have a new value
			if (newValue != oldValue)
			{
				actualColor = newValue is Color value ? value : DetermineColor((string) newValue);
				DrawImage();
			}
		}

		/// <summary>
		/// Draws the image.
		/// </summary>
		private void DrawImage(bool mustDraw = false)
		{
			var color = IsSelected ? actualSelectedColor : actualColor;

			if (mustDraw || isDrawn)
			{
				var source = Source;

				if (Device.RuntimePlatform == Device.WPF)
				{
					if (IsSelected && !string.IsNullOrWhiteSpace(SelectedSourceWPF))
					{
						source = SelectedSourceWPF;
					} 
					else if (!string.IsNullOrWhiteSpace(SourceWPF))
					{
						source = SourceWPF;
					}
				}
				else if (IsSelected && !string.IsNullOrWhiteSpace(SelectedSource))
				{
					source = SelectedSource;
				}

				if (!string.IsNullOrWhiteSpace(source))
				{
					base.Source = CachedImageSource.FindOrAddImage(source, actualWidth, actualHeight, color, cachedImages).Image;
				}

				isDrawn = true;
			}
		}

		/// <summary>
		/// Determines the color.
		/// </summary>
		/// <param name="color">The color.</param>
		/// <returns>Color.</returns>
		private Color DetermineColor(string color)
		{
			if (!string.IsNullOrEmpty(color))
			{
				if (color.StartsWith("[Color"))
				{
					//[Color: A=1, R=0.737254917621613, G=0, B=0.0274509806185961, Hue=0.99379426240921, Saturation=1, Luminosity=0.368627458810806]/
					var colorElements = color.Substring(7).Split(',');
					var colorA = Convert.ToDouble(colorElements[0].Split('=')[1]);
					var colorR = Convert.ToDouble(colorElements[1].Split('=')[1]);
					var colorG = Convert.ToDouble(colorElements[2].Split('=')[1]);
					var colorB = Convert.ToDouble(colorElements[3].Split('=')[1]);
					return Color.FromRgba(colorR, colorG, colorB, colorA);
				}

				// Convert from name
				var converter = new ColorTypeConverter();
				return (Color) converter.ConvertFromInvariantString(color);
			}

			return Color.Default;
		}

		/// <summary>
		/// Determines whether [is selected property changed] [the specified old value].
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void IsSelectedPropertyChanged(object oldValue, object newValue)
		{
			// Do we have a new value
			if (newValue != oldValue)
			{
				DrawImage();
			}
		}

		/// <summary>
		/// Selecteds the color property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void SelectedColorPropertyChanged(object oldValue, object newValue)
		{
			// Do we have a new value
			if (newValue != oldValue)
			{
				actualSelectedColor = newValue is Color value ? value : DetermineColor((string) newValue);
				DrawImage();
			}
		}

		/// <summary>
		/// Called when [property changed].
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void SourcePropertyChanged(object oldValue, object newValue)
		{
			// Do we have a new value
			if (newValue != oldValue)
			{
				DrawImage();
			}
		}
		#endregion

		#region Override methods
		/// <summary>
		/// This method is called when the size of the element is set during a layout cycle. This method is called directly before the
		/// <see cref="E:Xamarin.Forms.VisualElement.SizeChanged" /> event is emitted. Implement this method to add class handling for this event.
		/// </summary>
		/// <param name="width">The new width of the element.</param>
		/// <param name="height">The new height of the element.</param>
		/// <remarks>This method has no default implementation. You should still call the base implementation in case an intermediate class
		/// has implemented this method. Most layouts will want to implement this method in order to layout their children during a layout cycle.
		/// </remarks>
		protected override void OnSizeAllocated(double width, double height)
		{
			// Store width and height
			actualHeight = height;
			actualWidth = width;

			if (!string.IsNullOrWhiteSpace(Source) && height > 1 && width > 1)
			{
				DrawImage(true);
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>The color.</value>
		public Color Color
		{
			get => (Color) GetValue(ColorProperty);
			set => SetValue(ColorProperty, value);
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is selected.
		/// </summary>
		/// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
		public bool IsSelected
		{
			get => (bool) GetValue(IsSelectedProperty);
			set => SetValue(IsSelectedProperty, value);
		}

		/// <summary>
		/// Gets or sets the color of the selected.
		/// </summary>
		/// <value>The color of the selected.</value>
		public Color SelectedColor
		{
			get => (Color) GetValue(SelectedColorProperty);
			set => SetValue(SelectedColorProperty, value);
		}

		/// <summary>
		/// Gets or sets the selected source.
		/// </summary>
		/// <value>The selected source.</value>
		public string SelectedSource
		{
			get => (string) GetValue(SelectedSourceProperty);
			set => SetValue(SelectedSourceProperty, value);
		}

		/// <summary>
		/// Gets or sets the selected source WPF.
		/// </summary>
		/// <value>The selected source WPF.</value>
		public string SelectedSourceWPF
		{
			get => (string) GetValue(SelectedSourceWPFProperty);
			set => SetValue(SelectedSourceWPFProperty, value);
		}

		/// <summary>
		/// Gets or sets the source of the image. This is a bindable property.
		/// </summary>
		/// <value>An <see cref="T:Xamarin.Forms.ImageSource" /> representing the image source. Default is null.</value>
		/// <remarks>To be added.</remarks>
		public new string Source
		{
			get => (string) GetValue(SourceProperty);
			set => SetValue(SourceProperty, value);
		}

		/// <summary>
		/// Gets or sets the source WPF.
		/// </summary>
		/// <value>The source WPF.</value>
		public string SourceWPF
		{
			get => (string) GetValue(SourceWPFProperty);
			set => SetValue(SourceWPFProperty, value);
		}
		#endregion

		#region Fields
		private Color actualColor;
		private double actualHeight;
		private Color actualSelectedColor;
		private double actualWidth;
		private bool isDrawn;
		private static readonly List<CachedImageSource> cachedImages = new List<CachedImageSource>();
		#endregion

		#region Nested Types
		/// <summary>
		/// CachedImageSource.
		/// </summary>
		internal class CachedImageSource
		{
			#region Constructor
			/// <summary>
			/// Initializes a new instance of the <see cref="CachedImageSource"/> class.
			/// </summary>
			/// <param name="name">The name.</param>
			/// <param name="width">The width.</param>
			/// <param name="height">The height.</param>
			/// <param name="color">The color.</param>
			/// <param name="image">The image.</param>
			public CachedImageSource(string name, double width, double height, Color color)
			{
				Color = Color;
				Name = name;
				Height = height;

				if (Device.RuntimePlatform != Device.WPF)
				{
					Image = SvgImageSource.FromSvgResource(name.ToLower(), width, height, color, GetType());
				}
				else
				{
					Image = name.ToLower();
				}

				Width = width;
			}
			#endregion

			#region Public methods
			/// <summary>
			/// Finds the or add image.
			/// </summary>
			/// <param name="name">The name.</param>
			/// <param name="width">The width.</param>
			/// <param name="height">The height.</param>
			/// <param name="color">The color.</param>
			/// <param name="image">The image.</param>
			/// <param name="cachedImages">The cached images.</param>
			/// <returns>CachedImageSource.</returns>
			public static CachedImageSource FindOrAddImage(string name, double width, double height, Color color, List<CachedImageSource> cachedImages)
			{
				var foundImage = cachedImages.FirstOrDefault(ci => ci.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
																   ci.Color.Equals(color) &&
																   ci.Height.Equals(height) &&
																   ci.Width.Equals(width));

				if (foundImage == null)
				{
					foundImage = new CachedImageSource(name, width, height, color);
					cachedImages.Add(foundImage);
				}

				return foundImage;
			}

			/// <summary>
			/// Finds the image.
			/// </summary>
			/// <param name="name">The name.</param>
			/// <param name="width">The width.</param>
			/// <param name="height">The height.</param>
			/// <param name="color">The color.</param>
			/// <param name="cachedImages">The cached images.</param>
			/// <returns>CachedImageSource.</returns>
			public CachedImageSource FindImage(string name, double width, double height, Color color, List<CachedImageSource> cachedImages)
			{
				var image = cachedImages.FirstOrDefault(ci => ci.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
															  ci.Color.Equals(color) &&
															  ci.Height.Equals(height) &&
															  ci.Width.Equals(width));
				return image;
			}
			#endregion

			#region Properties
			/// <summary>
			/// Gets or sets the color.
			/// </summary>
			/// <value>The color.</value>
			public Color Color { get; set; }

			/// <summary>
			/// Gets or sets the height.
			/// </summary>
			/// <value>The height.</value>
			public double Height { get; set; }

			/// <summary>
			/// Gets or sets the image.
			/// </summary>
			/// <value>The image.</value>
			public ImageSource Image { get; set; }

			/// <summary>
			/// Gets or sets the name.
			/// </summary>
			/// <value>The name.</value>
			public string Name { get; set; }

			/// <summary>
			/// Gets or sets the width.
			/// </summary>
			/// <value>The width.</value>
			public double Width { get; set; }
			#endregion
		}
		#endregion
	}
}