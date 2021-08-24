using System;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Controls
{
	/// <summary>
	/// TapImage.
	/// </summary>
	public class TapImage : SvgImage
	{
		#region Events/Delegates
		public event EventHandler<ItemTappedEventArgs> Tapped;
		#endregion

		#region Bindable Properties
		/// <summary>
		/// The platform android property
		/// </summary>
		public static readonly BindableProperty PlatformAndroidProperty =
			BindableProperty.Create(nameof(PlatformAndroid), typeof(bool), typeof(TapImage), true,
				propertyChanged: PlatformAndroidPropertyChanged);

		/// <summary>
		/// The platformi os property
		/// </summary>
		public static readonly BindableProperty PlatformiOSProperty =
			BindableProperty.Create(nameof(PlatformiOS), typeof(bool), typeof(TapImage), true,
				propertyChanged: PlatformiOSPropertyChanged);
		#endregion
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="TapImage"/> class.
		/// </summary>
		public TapImage()
		{
			if (!GestureRecognizers.Any())
			{
				// Create a tap recognizer
				var tapRecognizer = new TapGestureRecognizer();
				tapRecognizer.Tapped += Image_Tapped;

				// Add it to the this
				GestureRecognizers.Add(tapRecognizer);
			}
		}
		#endregion

		#region UI methods
		/// <summary>
		/// Handles the <see cref="E:ItemTapped" /> event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void Image_Tapped(object sender, EventArgs e)
		{
			Tapped?.Invoke(this, null);
		}

		/// <summary>
		/// Platforms the android property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void PlatformAndroidPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (!Convert.ToBoolean(newValue))
			{
				((TapImage) bindable).IsVisible = DeviceInfo.Platform == DevicePlatform.iOS && ((TapImage) bindable).PlatformiOS;
			}
		}

		/// <summary>
		/// Platformis the os property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void PlatformiOSPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (!Convert.ToBoolean(newValue))
			{
				((TapImage) bindable).IsVisible = DeviceInfo.Platform == DevicePlatform.Android && ((TapImage) bindable).PlatformAndroid;
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets a value indicating whether [platform android].
		/// </summary>
		/// <value><c>true</c> if [platform android]; otherwise, <c>false</c>.</value>
		public bool PlatformAndroid
		{
			get => (bool) GetValue(PlatformAndroidProperty);
			set => SetValue(PlatformAndroidProperty, value);
		}

		/// <summary>
		/// Gets or sets a value indicating whether [platform ios].
		/// </summary>
		/// <value><c>true</c> if [platform ios]; otherwise, <c>false</c>.</value>
		public bool PlatformiOS
		{
			get => (bool) GetValue(PlatformiOSProperty);
			set => SetValue(PlatformiOSProperty, value);
		}
		#endregion
	}
}

	