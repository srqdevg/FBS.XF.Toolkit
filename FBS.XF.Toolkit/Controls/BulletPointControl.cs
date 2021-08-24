﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using FBS.XF.Toolkit.Images;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Controls
{
	/// <summary>
	/// BulletListControl.
	/// Implements the <see cref="Xamarin.Forms.ContentView" />
	/// </summary>
	/// <seealso cref="Xamarin.Forms.ContentView" />
	/// <remarks>
	/// Control is from https://github.com/AuriR/XamarinFormsBulletListControl
	/// but tweaked to add border 
	/// </remarks>
	public class BulletListControl : ContentView
	{
		#region Events/Delegates
		private const string DefaultBulletCharacter = "\u2022";
		#endregion

		#region Bindable Properties
		/// <summary>
		/// The items property
		/// </summary>
		public static readonly BindableProperty ItemsProperty = 
			BindableProperty.Create(nameof(Items), typeof(IEnumerable<string>), typeof(BulletListControl), new List<string>(),
				propertyChanged: ItemsPropertyChanged);

		/// <summary>
		/// The bullet image property
		/// </summary>
		public static readonly BindableProperty BulletImageProperty = 
			BindableProperty.Create(nameof(BulletImageProperty), typeof(Stream), typeof(Stream));

		/// <summary>
		/// The bullet character property
		/// </summary>
		public static readonly BindableProperty BulletCharacterProperty = 
			BindableProperty.Create(nameof(BulletCharacter), typeof(string), typeof(string),
			propertyChanged: BulletCharacterPropertyChanged);

		/// <summary>
		/// The bullet character font size property
		/// </summary>
		public static readonly BindableProperty BulletCharacterFontSizeProperty =
			BindableProperty.Create(nameof(BulletCharacterFontSize), typeof(double), typeof(double), 
				propertyChanged: BulletCharacterFontSizePropertyChanged);

		/// <summary>
		/// The list item font size property
		/// </summary>
		public static readonly BindableProperty ListItemFontSizeProperty = 
			BindableProperty.Create(nameof(ListItemFontSize), typeof(double), typeof(double),
			propertyChanged: ListItemFontSizePropertyChanged);
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="BulletListControl"/> class.
		/// </summary>
		public BulletListControl()
		{
			// Assuming Items will be set via binding. Otherwise, call Render() to render empty... but why?
			Render();
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Handles changing the bullet character font size.
		/// </summary>
		/// <param name="bindable"></param>
		/// <param name="oldValue"></param>
		/// <param name="newValue"></param>
		private static void BulletCharacterFontSizePropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue is double value)
			{
				var control = (BulletListControl) bindable;
				control.BulletCharacterFontSize = value;
				control.Render();
			}
		}

		/// <summary>
		/// Handles changing the character used for bullets.
		/// </summary>
		/// <param name="bindable"></param>
		/// <param name="oldValue"></param>
		/// <param name="newValue"></param>
		private static void BulletCharacterPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue is string value)
			{
				var control = (BulletListControl) bindable;
				control.BulletCharacter = value;
				control.Render();
			}
		}

		/// <summary>
		/// Handles new bound item data coming in.
		/// </summary>
		/// <param name="bindable"></param>
		/// <param name="oldValue"></param>
		/// <param name="newValue"></param>
		private static void ItemsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue is IEnumerable<string> value)
			{
				var control = (BulletListControl) bindable;
				control.Items = value;
				control.Render();
			}
		}

		/// <summary>
		/// Handles changing the list item font size.
		/// </summary>
		/// <param name="bindable"></param>
		/// <param name="oldValue"></param>
		/// <param name="newValue"></param>
		private static void ListItemFontSizePropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue is double value)
			{
				var control = (BulletListControl) bindable;
				control.ListItemFontSize = value;
				control.Render();
			}
		}

		/// <summary>
		/// Renders this instance.
		/// </summary>
		private void Render()
		{
			if (Items == null || !Items.Any())
			{
				return;
			}

			// Create the container.
			var parentLayout = new StackLayout { HorizontalOptions = LayoutOptions.Fill, Padding = new Thickness(1) };

			// Render the list.
			foreach (var item in Items)
			{
				// Make sure they provided a good bullet.
				if (string.IsNullOrWhiteSpace(BulletCharacter) && BulletImage == null)
					BulletCharacter = DefaultBulletCharacter;

				// Choose the bullet. Default to text if no image defined.
				var bullet = !string.IsNullOrWhiteSpace(BulletCharacter) && BulletImage == null
					? (View) new Label 
						{
							FontSize = BulletCharacterFontSize,
							Margin = ListLayoutPadding,
							Text = BulletCharacter,  
							VerticalTextAlignment = TextAlignment.Start 
						}
					: new Image { Source = ImageSource.FromStream(() => BulletImage) };

				// Create label
				var label = new Label
				{
					FontSize = ListItemFontSize,
					Margin = new Thickness(10, 0, 10, 0),
					Text = item,
					VerticalTextAlignment = TextAlignment.Start
				};

				// Create the horizontal container.
				var container = new StackLayout 
				{
					HorizontalOptions = LayoutOptions.Fill, 
					Orientation = StackOrientation.Horizontal 
				};

				container.Children.Add(bullet);
				container.Children.Add(label);
				parentLayout.Children.Add(container);
			}

			// Render.
			Content = parentLayout;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the bullet character.
		/// </summary>
		/// <value>The bullet character.</value>
		public string BulletCharacter { get; set; } = DefaultBulletCharacter;

		/// <summary>
		/// Gets or sets the size of the bullet character font.
		/// </summary>
		/// <value>The size of the bullet character font.</value>
		public double BulletCharacterFontSize { get; set; } = 14;

		/// <summary>
		/// Gets or sets the bullet image.
		/// </summary>
		/// <value>The bullet image.</value>
		public Stream BulletImage { get; set; } = null;

		/// <summary>
		/// Gets or sets the items.
		/// </summary>
		/// <value>The items.</value>
		public IEnumerable<string> Items
		{
			get => GetValue(ItemsProperty) as IEnumerable<string>;
			set => SetValue(ItemsProperty, value);
		}

		/// <summary>
		/// Gets or sets the size of the list item font.
		/// </summary>
		/// <value>The size of the list item font.</value>
		public double ListItemFontSize { get; set; } = 12;

		/// <summary>
		/// Gets or sets the list item margin.
		/// </summary>
		/// <value>The list item margin.</value>
		public Thickness ListItemMargin { get; set; } = new Thickness(1);

		/// <summary>
		/// Gets or sets the list layout padding.
		/// </summary>
		/// <value>The list layout padding.</value>
		public Thickness ListLayoutPadding { get; set; } = new Thickness(1);
		#endregion
	}
}