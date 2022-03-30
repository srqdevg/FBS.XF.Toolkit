using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace FBS.XF.Toolkit.WPF
{
	internal class UIHelper
	{
		#region Public methods
		/// <summary>
		/// Creates the menu item.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="name">The name.</param>
		/// <param name="imageName">Name of the image.</param>
		/// <param name="clickHandler">The click handler.</param>
		/// <returns>MenuItem.</returns>
		public static System.Windows.Controls.MenuItem CreateMenuItem(string text, string name, string imageName, RoutedEventHandler clickHandler)
		{
			// Create image
			var image = new BitmapImage();
			image.BeginInit();
			image.UriSource = new Uri($@"pack://application:,,,/FBS.XF.Toolkit.WPF;component/Resources/{imageName}.png");
			image.EndInit();

			// Create menu item
			var menuItem = new System.Windows.Controls.MenuItem
			{
				Header = text,
				Icon = new System.Windows.Controls.Image { Source = image },
				Name = name
			};

			menuItem.Click += clickHandler;
			return menuItem;
		}
		#endregion
	}
}
