using System;
using System.Collections;
using System.Linq;
using Xamarin.Forms;
using FBS.XF.Toolkit.Interfaces;

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
		#region Dependency properties
		/// <summary>
		/// The size to fit property
		/// </summary>
		public static readonly BindableProperty SizeToFitProperty =
			BindableProperty.Create(nameof(SizeToFit), typeof(bool), typeof(CustomPicker), default(bool), BindingMode.OneWay,
				propertyChanged: (bd, ov, nv) => ((CustomPicker)bd).PropertyChanged(ov, nv));

		/// <summary>
		/// The items source property
		/// </summary>
		public new static readonly BindableProperty ItemsSourceProperty =
					BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(CustomPicker), null, BindingMode.OneWay,
						propertyChanged: (bd, ov, nv) => ((CustomPicker)bd).PropertyChanged(ov, nv));
		#endregion

		#region Private methods
		/// <summary>
		/// Properties the changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private new void PropertyChanged(object oldValue, object newValue)
		{
			if (SizeToFit && ItemsSource != null)
			{
				if (newValue.Equals(ItemsSource))
				{
					base.ItemsSource = ItemsSource;
				}

				if (Device.RuntimePlatform == Device.WPF)
				{
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
							var property =
								properties.FirstOrDefault(p => p.Name.Equals(((Binding) ItemDisplayBinding).Path));
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
							width = formattedWidth + 30;
						}
					}

					WidthRequest = Math.Round(comboBoxWidth + width);
				}
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets a value indicating whether [size to fit].
		/// </summary>
		/// <value><c>true</c> if [size to fit]; otherwise, <c>false</c>.</value>
		public bool SizeToFit
		{
			get => (bool)GetValue(SizeToFitProperty);
			set => SetValue(SizeToFitProperty, value);
		}

		/// <summary>
		/// Gets or sets the items source.
		/// </summary>
		/// <value>To be added.</value>
		/// <remarks>To be added.</remarks>
		public new IList ItemsSource
		{
			get => (IList)GetValue(ItemsSourceProperty);
			set => SetValue(ItemsSourceProperty, value);
		}
		#endregion
	}
}
