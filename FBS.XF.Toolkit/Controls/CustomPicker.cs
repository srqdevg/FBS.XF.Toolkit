using System;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Linq;
using FBS.XF.Toolkit.Interfaces;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Controls
{
	/// <summary>
	/// Custom Picker.
	/// Implements the <see cref="Picker" />
	/// </summary>
	/// <seealso cref="Picker" />
	/// <remarks>Used to fix issues in MS version of controls</remarks>
	public class CustomPicker : Picker
	{
		public static readonly BindableProperty SizeToFitProperty =
			BindableProperty.Create(nameof(SizeToFit), typeof(bool), typeof(CustomPicker), default(bool), BindingMode.OneWay,
				propertyChanged: (bd, ov, nv) => ((CustomPicker) bd).PropertyChanged(ov, nv));

		public new static readonly BindableProperty ItemsSourceProperty =
			BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(CustomPicker), null, BindingMode.OneWay,
				propertyChanged: (bd, ov, nv) => ((CustomPicker) bd).PropertyChanged(ov, nv));

		private new void PropertyChanged(object oldValue, object newValue)
		{
			if (SizeToFit && ItemsSource != null)
			{
				if (newValue.Equals(ItemsSource))
				{
					base.ItemsSource = ItemsSource;
				}

				var comboBoxWidth = WidthRequest;
				double width = 0;
				var nativeService = DependencyService.Resolve<INativeService>();

				foreach (var item in ItemsSource)
				{
					string textValue;

					if (ItemDisplayBinding != null)
					{
						var type = item.GetType();
						var properties = type.GetProperties();
						var property = properties.FirstOrDefault(p => p.Name.Equals(((Binding) ItemDisplayBinding).Path));
						var value = property?.GetValue(item);

						if (value is int i)
						{
							textValue = Convert.ToString(i);
						}
						else
						{
							textValue = (string) value;
						}
					}
					else
					{
						textValue = (string) item;
					}

					// Get text width 
					var formattedWidth = nativeService.GetTextWidth(textValue);

					if (formattedWidth > width)
					{
						width = formattedWidth + 10;
					}
				}

				// TODO: FIDDLE THE WIDTH DOWN A BIT, IT"S TOO WIDE 
				WidthRequest = Math.Round(comboBoxWidth + width);
			}
		}
		
		public bool SizeToFit
		{
			get => (bool) GetValue(SizeToFitProperty);
			set => SetValue(SizeToFitProperty, value);
		}

		public new IList ItemsSource
		{
			get => (IList) GetValue(ItemsSourceProperty);
			set => SetValue(ItemsSourceProperty, value);
		}
	}
}
