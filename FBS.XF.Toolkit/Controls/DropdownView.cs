using System;
using System.Linq;
using FBS.XF.Toolkit.Event;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Controls
{
	/// <summary>
	/// DropDown control
	/// </summary>
	public class DropdownView : StackLayout, IDisposable
	{
		#region Events/Delegates
		/// <summary>
		/// Occurs when [selection changed].
		/// </summary>
		public event EventHandler<RepeaterEventArgs> SelectionChanged;
		#endregion

		#region IDisposable
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			dropdownItemsRepeaterView.SelectionChanged -= DropdownItemsRepeaterView_SelectionChanged;
			dropDownTapGestureRecognizer.Tapped -= DropdownTapGestureRecognizer_Tapped;
		}
		#endregion

		#region Dependency properties
		/// <summary>
		/// The border color property
		/// </summary>
		public static readonly BindableProperty BorderColorProperty =
			BindableProperty.Create(nameof(BorderColor), typeof(object), typeof(DropdownView), Color.Default,
				propertyChanged: (bd, ov, nv) => ((DropdownView) bd).ControlPropertyChanged(ov, nv));

		/// <summary>
		/// The font family property
		/// </summary>
		public static readonly BindableProperty FontFamilyProperty =
			BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(DropdownView), default(string),
				propertyChanged: (bd, ov, nv) => ((DropdownView) bd).ControlPropertyChanged(ov, nv));

		/// <summary>
		/// The font size property
		/// </summary>
		public static readonly BindableProperty FontSizeProperty =
			BindableProperty.Create(nameof(FontSize), typeof(double), typeof(DropdownView), default(double),
				propertyChanged: (bd, ov, nv) => ((DropdownView) bd).ControlPropertyChanged(ov, nv));

		/// <summary>
		/// The header template property
		/// </summary>
		public static readonly BindableProperty HeaderTemplateProperty =
			BindableProperty.Create(nameof(HeaderTemplate), typeof(DataTemplate), typeof(DropdownView),
				propertyChanged: (bd, ov, nv) => ((DropdownView) bd).ControlPropertyChanged(ov, nv));  
		
		/// <summary>
		/// The items source property
		/// </summary>
		public static readonly BindableProperty ItemsSourceProperty =
			BindableProperty.Create(nameof(ItemsSource), typeof(object), typeof(DropdownView),
				propertyChanged: (bd, ov, nv) => ((DropdownView) bd).ItemSourcePropertyChanged(ov, nv));

		/// <summary>
		/// The image color property
		/// </summary>
		public static readonly BindableProperty ImageColorProperty =
			BindableProperty.Create(nameof(ImageColor), typeof(Color), typeof(DropdownView), Color.Default,
				propertyChanged: (bd, ov, nv) => ((DropdownView) bd).ControlPropertyChanged(ov, nv));

		/// <summary>
		/// The image height property
		/// </summary>
		public static readonly BindableProperty ImageHeightProperty =
			BindableProperty.Create(nameof(ImageHeight), typeof(int), typeof(DropdownView), default(int),
				propertyChanged: (bd, ov, nv) => ((DropdownView) bd).ControlPropertyChanged(ov, nv));

		/// <summary>
		/// The image property
		/// </summary>
		public static readonly BindableProperty ImageProperty =
			BindableProperty.Create(nameof(Image), typeof(string), typeof(DropdownView), default(string),
				propertyChanged: (bd, ov, nv) => ((DropdownView) bd).ControlPropertyChanged(ov, nv));

		/// <summary>
		/// The image width property
		/// </summary>
		public static readonly BindableProperty ImageWidthProperty =
			BindableProperty.Create(nameof(ImageWidth), typeof(int), typeof(DropdownView), default(int),
				propertyChanged: (bd, ov, nv) => ((DropdownView) bd).ControlPropertyChanged(ov, nv));

		/// <summary>
		/// The item template property
		/// </summary>
		public static readonly BindableProperty ItemTemplateProperty =
			BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(DropdownView),
				propertyChanged: (bd, ov, nv) => ((DropdownView) bd).ControlPropertyChanged(ov, nv));

		/// <summary>
		/// The selected color property
		/// </summary>
		public static readonly BindableProperty SelectedColorProperty =
			BindableProperty.Create(nameof(SelectedColor), typeof(object), typeof(DropdownView), Color.Default,
				propertyChanged: (bd, ov, nv) => ((DropdownView) bd).ControlPropertyChanged(ov, nv));

		/// <summary>
		/// The selected item property
		/// </summary>
		public static readonly BindableProperty SelectedItemProperty =
			BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(DropdownView), defaultBindingMode: BindingMode.TwoWay,
				propertyChanged: (bd, ov, nv) => ((DropdownView) bd).ControlPropertyChanged(ov, nv));

		/// <summary>
		/// The text color property
		/// </summary>
		public static readonly BindableProperty TextColorProperty =
			BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(DropdownView), Color.Default,
				propertyChanged: (bd, ov, nv) => ((DropdownView) bd).ControlPropertyChanged(ov, nv));

		/// <summary>
		/// The selected text color property
		/// </summary>
		public static readonly BindableProperty TextFieldProperty =
			BindableProperty.Create(nameof(TextField), typeof(string), typeof(DropdownView), string.Empty,
				propertyChanged: (bd, ov, nv) => ((DropdownView) bd).ControlPropertyChanged(ov, nv));
		#endregion

		#region UI methods
		/// <summary>
		/// Handles the SelectionChanged event of the DropdownItemsRepeaterView control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RepeaterEventArgs"/> instance containing the event data.</param>
		/// <exception cref="NotImplementedException"></exception>
		private void DropdownItemsRepeaterView_SelectionChanged(object sender, RepeaterEventArgs e)
		{
			if (e.CurrentSelection != e.PreviousSelection)
			{
				SelectedItem = e.CurrentSelection;
				dropdownContentView.IsVisible = false;

				var eventArgs = new RepeaterEventArgs(e.PreviousSelection, e.CurrentSelection);
				SelectionChanged?.Invoke(this, eventArgs);
			}
		}

		/// <summary>
		/// Handles the Tapped event of the DropdownTapGestureRecognizer control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void DropdownTapGestureRecognizer_Tapped(object sender, EventArgs e)
		{
			// Create dropdown frame
			if (dropdownContentView == null)
			{
				// Frame to contain repeater view
				dropdownContentView = new ContentView
				{
					BackgroundColor = Application.Current.RequestedTheme == OSAppTheme.Dark ? Color.Black : Color.White,
					IsVisible = false
				};

				// Dropdown stacklayout
				var dropDownStackLayout = new StackLayout
				{
					BackgroundColor = Application.Current.RequestedTheme == OSAppTheme.Dark ? Color.Black : Color.White,
					Orientation = StackOrientation.Vertical,
				};

				// Dropdown items repeater view
				dropdownItemsRepeaterView = new RepeaterView
				{
					BackgroundColor = Application.Current.RequestedTheme == OSAppTheme.Dark ? Color.Black : Color.White,
					BackgroundColorSelected = SelectedColor,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					ItemsSource = ItemsSource,
					ItemTemplate = ItemTemplate,
					Orientation = StackOrientation.Vertical,
					SelectedItem = SelectedItem,
					VerticalOptions = LayoutOptions.Center,
					WidthRequest = Width
				};

				dropdownItemsRepeaterView.SelectionChanged += DropdownItemsRepeaterView_SelectionChanged;

				dropDownStackLayout.Children.Add(dropdownItemsRepeaterView);
				dropdownContentView.Content = dropDownStackLayout;

				if (Parent is Grid grid)
				{
					dropdownContentView.TranslationY = Height;
					grid.Children.Add(dropdownContentView);
				}
			}

			// Show/Hide frame
			if (!dropdownContentView.IsVisible)
			{
				dropdownContentView.WidthRequest = Width;
			}

			dropdownContentView.IsVisible = !dropdownContentView.IsVisible;
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Controls the property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void ControlPropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue && newValue != null && Children != null)
			{
				// Create container
				if (headerStackLayout == null)
				{
					// Do we need a frame?
					if (BorderColor != Color.Default)
					{
						// Create container
						headerStackLayout = new StackLayout
						{
							HorizontalOptions = LayoutOptions.FillAndExpand,
							Orientation = StackOrientation.Horizontal,
							VerticalOptions = LayoutOptions.FillAndExpand
						};

						// Create border frame 
						var borderFrame = new Frame
						{
							BorderColor = BorderColor,
							Content = headerStackLayout,
							Padding = new Thickness(0, 0, 0, 0),
						};

						Children.Add(borderFrame);
						borderFrame.Content = headerStackLayout;
					}
					else
					{
						headerStackLayout = new StackLayout
						{
							HorizontalOptions = LayoutOptions.FillAndExpand,
							Orientation = StackOrientation.Horizontal,
							VerticalOptions = LayoutOptions.FillAndExpand
						};

						Children.Add(headerStackLayout);
					}

					// Add gesture
					GestureRecognizers.Clear();
					dropDownTapGestureRecognizer = new TapGestureRecognizer();
					dropDownTapGestureRecognizer.Tapped += DropdownTapGestureRecognizer_Tapped;
					GestureRecognizers.Add(dropDownTapGestureRecognizer);
				}

				// Add either a label or a more complex header template
				if (HeaderTemplate != null)
				{
					// Create controls
					if (!headerStackLayout.Children.Any())
					{
						var headerView = (View) HeaderTemplate.CreateContent();
						headerView.BindingContext = SelectedItem;
						headerStackLayout.Children.Add(headerView);
					}
				}
				else if (TextField != null && SelectedItem != null)
				{
					var dropdownIndicator = new Label
					{
						HorizontalOptions = LayoutOptions.EndAndExpand,
						Margin = new Thickness(0, 0, 10, 0),
						Text = "v",
						TextColor = Color.Black,
						VerticalOptions = LayoutOptions.CenterAndExpand
					};

					headerStackLayout.Children.Add(dropdownIndicator);
					Children.Add(headerStackLayout);

					// Label
					var headerLabel = new Label
					{
						FontSize = FontSize > 0 ? FontSize : Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
						FontFamily = FontFamily,
						HorizontalOptions = LayoutOptions.Start,
						Margin = new Thickness(10, 0, 10, 0),
						TextColor = TextColor,
						VerticalOptions = LayoutOptions.CenterAndExpand,
					};

					var value = SelectedItem.GetType().GetProperty(TextField)?.GetValue(SelectedItem, null);
					headerLabel.Text = value != null ? value.ToString() : string.Empty;

					if (headerStackLayout.Children.Count > 1)
					{
						headerStackLayout.Children.RemoveAt(0);
					}

					headerStackLayout.Children.Insert(0, headerLabel);
				}
			}
		}

		/// <summary>
		/// Items the source property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void ItemSourcePropertyChanged(object oldValue, object newValue)
		{
			if (oldValue != newValue)
			{
				if (dropdownItemsRepeaterView != null)
				{
					dropdownItemsRepeaterView.ItemsSource = newValue;
				}
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the color of the border.
		/// </summary>
		/// <value>The color of the border.</value>
		public Color BorderColor
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
		/// Gets or sets the items source.
		/// </summary>
		/// <value>The items source.</value>
		public object ItemsSource
		{
			get => GetValue(ItemsSourceProperty);
			set => SetValue(ItemsSourceProperty, value);
		}

		/// <summary>
		/// Gets or sets the header template.
		/// </summary>
		/// <value>The header template.</value>
		public DataTemplate HeaderTemplate
		{
			get => (DataTemplate) GetValue(HeaderTemplateProperty);
			set => SetValue(HeaderTemplateProperty, value);
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
		/// Gets or sets the width of the image.
		/// </summary>
		/// <value>The width of the image.</value>
		public int ImageWidth
		{
			get => (int) GetValue(ImageWidthProperty);
			set => SetValue(ImageWidthProperty, value);
		}

		/// <summary>
		/// Gets or sets the item template.
		/// </summary>
		/// <value>The item template.</value>
		public DataTemplate ItemTemplate
		{
			get => (DataTemplate) GetValue(ItemTemplateProperty);
			set => SetValue(ItemTemplateProperty, value);
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
		/// Gets or sets the selected item.
		/// </summary>
		/// <value>The selected item.</value>
		public object SelectedItem
		{
			get => GetValue(SelectedItemProperty);
			set => SetValue(SelectedItemProperty, value);
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
		/// Gets or sets the text field.
		/// </summary>
		/// <value>The text field.</value>
		public string TextField
		{
			get => (string) GetValue(TextFieldProperty);
			set => SetValue(TextFieldProperty, value);
		}
		#endregion

		#region Fields
		private ContentView dropdownContentView;
		private RepeaterView dropdownItemsRepeaterView;
		private TapGestureRecognizer dropDownTapGestureRecognizer;
		private StackLayout headerStackLayout;
		#endregion
	}
}
