using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using FBS.XF.Toolkit.Event;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace FBS.XF.Toolkit.Controls
{
	/// <remarks>
	/// All rights belong to https://github.com/1iveowl/Plugin.SegmentedControl
	/// MIT License
	/// I modified this control to include svg images in the 'segments'
	/// </remarks>
	[DesignTimeVisible(true)]
	[Preserve(AllMembers = true)]
	public class SegmentedControl : View, IViewContainer<SegmentedControlOption>
	{
		#region Dependency properties
		/// <summary>
		/// The border color property
		/// </summary>
		public static readonly BindableProperty BorderColorProperty = 
			BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(SegmentedControl), 
				defaultValueCreator: bindable => ((SegmentedControl) bindable).TintColor);

		/// <summary>
		/// The border width property
		/// </summary>
		public static readonly BindableProperty BorderWidthProperty = 
			BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(SegmentedControl), 
				defaultValueCreator: _ => Device.RuntimePlatform == Device.Android ? 1.0 : 0.0);

		/// <summary>
		/// The children property
		/// </summary>
		public static readonly BindableProperty ChildrenProperty = 
			BindableProperty.Create(nameof(Children), typeof(IList<SegmentedControlOption>), typeof(SegmentedControl), default(IList<SegmentedControlOption>),
				propertyChanging: OnChildrenChanging);

		/// <summary>
		/// The disabled color property
		/// </summary>
		public static readonly BindableProperty DisabledColorProperty = 
			BindableProperty.Create(nameof(DisabledColor), typeof(Color), typeof(SegmentedControl), Color.Gray);

		/// <summary>
		/// The font family property
		/// </summary>
		public static readonly BindableProperty FontFamilyProperty =
			BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(SegmentedControl));

		/// <summary>
		/// The font size property
		/// </summary>
		public static readonly BindableProperty FontSizeProperty = 
			BindableProperty.Create(nameof(FontSize), typeof(double), typeof(SegmentedControl), Device.GetNamedSize(NamedSize.Medium, typeof(Label)));

		/// <summary>
		/// The items source property
		/// </summary>
		public static readonly BindableProperty ItemsSourceProperty = 
			BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(SegmentedControl));

		/// <summary>
		/// The selected item property
		/// </summary>
		public static readonly BindableProperty SelectedItemProperty = 
			BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(SegmentedControl), defaultBindingMode: BindingMode.TwoWay);

		/// <summary>
		/// The selected segment property
		/// </summary>
		public static readonly BindableProperty SelectedSegmentProperty =
			BindableProperty.Create(nameof(SelectedSegment), typeof(int), typeof(SegmentedControl), 0);

		/// <summary>
		/// The segment selected command property
		/// </summary>
		public static readonly BindableProperty SegmentSelectedCommandProperty = 
			BindableProperty.Create(nameof(SegmentSelectedCommand), typeof(ICommand), typeof(SegmentedControl));

		/// <summary>
		/// The segment selected command parameter property
		/// </summary>
		public static readonly BindableProperty SegmentSelectedCommandParameterProperty = 
			BindableProperty.Create(nameof(SegmentSelectedCommandParameter), typeof(object), typeof(SegmentedControl));

		/// <summary>
		/// The selected text color property
		/// </summary>
		public static readonly BindableProperty SelectedTextColorProperty = 
			BindableProperty.Create(nameof(SelectedTextColor), typeof(Color), typeof(SegmentedControl), Color.White);

		/// <summary>
		/// The text color property
		/// </summary>
		public static readonly BindableProperty TextColorProperty = 
			BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(SegmentedControl), default(Color));

		/// <summary>
		/// The text property name property
		/// </summary>
		public static readonly BindableProperty TextPropertyNameProperty = 
			BindableProperty.Create(nameof(TextPropertyName), typeof(string), typeof(SegmentedControl));

		/// <summary>
		/// The tint color property
		/// </summary>
		public static readonly BindableProperty TintColorProperty = 
			BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(SegmentedControl), Color.Blue);

		#endregion

		#region Events/Delegates
		/// <summary>
		/// Occurs when [on element children changing].
		/// </summary>
		public event EventHandler<SegmentChildrenChanging> OnElementChildrenChanging;

		/// <summary>
		/// Occurs when [on segment selected].
		/// </summary>
		public event EventHandler<SegmentSelectEventArgs> OnSegmentSelected;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="SegmentedControl"/> class.
		/// </summary>
		public SegmentedControl()
		{
			Children = new List<SegmentedControlOption>();
		}
		#endregion

		#region Override methods
		/// <summary>
		/// Invoked whenever the binding context of the <see cref="T:Xamarin.Forms.View" /> changes. Override this method to add class handling for this event.
		/// </summary>
		/// <remarks>Overriders must call the base method.</remarks>
		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			if (!(Children is null))
			{
				foreach (var segment in Children)
				{
					segment.BindingContext = BindingContext;
				}
			}
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Raises the selection changed.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void RaiseSelectionChanged()
		{
			OnSegmentSelected?.Invoke(this, new SegmentSelectEventArgs { NewValue = this.SelectedSegment });

			if (!(SegmentSelectedCommand is null) && SegmentSelectedCommand.CanExecute(SegmentSelectedCommandParameter))
			{
				SegmentSelectedCommand.Execute(SegmentSelectedCommandParameter);
			}
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Called when [children changing].
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void OnChildrenChanging(BindableObject bindable, object oldValue, object newValue)
		{
			if (bindable is SegmentedControl segmentedControl &&
			    newValue is IList<SegmentedControlOption> newItemsList &&
			    segmentedControl.Children != null)
			{
				segmentedControl.OnElementChildrenChanging?.Invoke(segmentedControl, new SegmentChildrenChanging((IList<SegmentedControlOption>) oldValue, newItemsList));
				segmentedControl.Children.Clear();

				foreach (var newSegment in newItemsList)
				{
					newSegment.BindingContext = segmentedControl.BindingContext;
					segmentedControl.Children.Add(newSegment);
				}
			}
		}

		/// <summary>
		/// Called when [items source changed].
		/// </summary>
		private void OnItemsSourceChanged()
		{
			var itemsSource = ItemsSource;
			var items = itemsSource as IList;

			if (items == null && itemsSource is IEnumerable list)
			{
				items = list.Cast<object>().ToList();
			}

			if (items != null)
			{
				var textValues = items as IEnumerable<string>;

				if (textValues == null && items.Count > 0 && items[0] is string)
				{
					textValues = items.Cast<string>();
				}

				if (textValues != null)
				{
					Children = new List<SegmentedControlOption>(textValues.Select(child => new SegmentedControlOption { Text = child }));
					OnSelectedItemChanged(true);
				}
				else
				{
					var textPropertyName = TextPropertyName;

					if (textPropertyName != null)
					{
						Children = items.Cast<object>().Select(item => new SegmentedControlOption { Item = item, TextPropertyName = textPropertyName }).ToList();
						OnSelectedItemChanged(true);
					}
				}
			}
		}

		/// <summary>
		/// Method that is called when a bound property is changed.
		/// </summary>
		/// <param name="propertyName">The name of the bound property that changed.</param>
		/// <remarks>To be added.</remarks>
		protected override void OnPropertyChanged(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);

			if (propertyName == nameof(ItemsSource) || propertyName == nameof(TextPropertyName))
			{
				OnItemsSourceChanged();
			}
			else if (propertyName == nameof(SelectedItem))
			{
				OnSelectedItemChanged();
			}
			else if (propertyName == nameof(SelectedSegment))
			{
				OnSelectedSegmentChanged();
			}
		}

		/// <summary>
		/// Called when [selected segment changed].
		/// </summary>
		private void OnSelectedSegmentChanged()
		{
			var segmentIndex = SelectedSegment;

			if (segmentIndex >= 0 && segmentIndex < Children.Count && SelectedItem != Children[segmentIndex].Item)
			{
				SelectedItem = Children[segmentIndex].Item;
			}
		}

		/// <summary>
		/// Called when [selected item changed].
		/// </summary>
		/// <param name="forceUpdateSelectedSegment">if set to <c>true</c> [force update selected segment].</param>
		private void OnSelectedItemChanged(bool forceUpdateSelectedSegment = false)
		{
			if (TextPropertyName != null)
			{
				var selectedItem = SelectedItem;
				var selectedIndex = Children.IndexOf(item => item.Item == selectedItem);

				if (selectedIndex == -1)
				{
					selectedIndex = SelectedSegment;

					if (selectedIndex < 0 || selectedIndex >= Children.Count)
					{
						SelectedSegment = 0;
					}
					else if (SelectedSegment != selectedIndex)
					{
						SelectedSegment = selectedIndex;
					}
					else if (forceUpdateSelectedSegment)
					{
						OnSelectedSegmentChanged();
					}
				}
				else if (selectedIndex != SelectedSegment)
				{
					SelectedSegment = selectedIndex;
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
		/// Gets or sets the width of the border.
		/// </summary>
		/// <value>The width of the border.</value>
		public double BorderWidth
		{
			get => (double) GetValue(BorderWidthProperty);
			set => SetValue(BorderWidthProperty, value);
		}

		/// <summary>
		/// Gets or sets the children.
		/// </summary>
		/// <value>The children.</value>
		public IList<SegmentedControlOption> Children
		{
			get => (IList<SegmentedControlOption>) GetValue(ChildrenProperty);
			set => SetValue(ChildrenProperty, value);
		}

		/// <summary>
		/// Gets or sets the color of the disabled.
		/// </summary>
		/// <value>The color of the disabled.</value>
		public Color DisabledColor
		{
			get => (Color) GetValue(DisabledColorProperty);
			set => SetValue(DisabledColorProperty, value);
		}

		/// <summary>
		/// Gets or sets the size of the font.
		/// </summary>
		/// <value>The size of the font.</value>
		[Xamarin.Forms.TypeConverter(typeof(FontSizeConverter))]
		public double FontSize
		{
			get => (double) GetValue(FontSizeProperty);
			set => SetValue(FontSizeProperty, value);
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
		/// Gets or sets the items source.
		/// </summary>
		/// <value>The items source.</value>
		public IEnumerable ItemsSource
		{
			get => (IEnumerable) GetValue(ItemsSourceProperty);
			set => SetValue(ItemsSourceProperty, value);
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
		/// Gets or sets the selected segment.
		/// </summary>
		/// <value>The selected segment.</value>
		public int SelectedSegment
		{
			get => (int) GetValue(SelectedSegmentProperty);
			set => SetValue(SelectedSegmentProperty, value);
		}

		/// <summary>
		/// Gets or sets the segment selected command.
		/// </summary>
		/// <value>The segment selected command.</value>
		public ICommand SegmentSelectedCommand
		{
			get => (ICommand) GetValue(SegmentSelectedCommandProperty);
			set => SetValue(SegmentSelectedCommandProperty, value);
		}

		/// <summary>
		/// Gets or sets the segment selected command parameter.
		/// </summary>
		/// <value>The segment selected command parameter.</value>
		public object SegmentSelectedCommandParameter
		{
			get => GetValue(SegmentSelectedCommandParameterProperty);
			set => SetValue(SegmentSelectedCommandParameterProperty, value);
		}

		/// <summary>
		/// Gets or sets the color of the selected text.
		/// </summary>
		/// <value>The color of the selected text.</value>
		public Color SelectedTextColor
		{
			get => (Color) GetValue(SelectedTextColorProperty);
			set => SetValue(SelectedTextColorProperty, value);
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
		 /// Gets or sets the name of the text property.
		 /// </summary>
		 /// <value>The name of the text property.</value>
		public string TextPropertyName
		{
			get => (string) GetValue(TextPropertyNameProperty);
			set => SetValue(TextPropertyNameProperty, value);
		}

		/// <summary>
		/// Gets or sets the color of the tint.
		/// </summary>
		/// <value>The color of the tint.</value>
		public Color TintColor
		{
			get => (Color) GetValue(TintColorProperty);
			set => SetValue(TintColorProperty, value);
		}
		#endregion

		#region Fields
		#endregion


	




		

		

		
	}
}
