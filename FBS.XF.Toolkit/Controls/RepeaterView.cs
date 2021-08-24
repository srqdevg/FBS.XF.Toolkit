using System;
using System.Collections;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Controls
{
	/// <summary>
	/// Repeater.
	/// Implements the <see cref="Xamarin.Forms.StackLayout" />
	/// </summary>
	/// <seealso cref="Xamarin.Forms.StackLayout" />
	/// <remarks>
	/// With help/snippets of code from this sites
	///	https://github.com/xamarinhowto/DataTemplateRepeaterControlExample
	/// https://stackoverflow.com/questions/41233767/xamarin-forms-create-instance-of-a-datatemplate
	/// </remarks>
	public class RepeaterView : StackLayout
	{
		#region Events/Delegates
		/// <summary>
		/// Occurs when [selection changed].
		/// </summary>
		public event EventHandler<RepeaterEventArgs> SelectionChanged;
		#endregion

		#region Dependency properties
		/// <summary>
		/// The background color selected property
		/// </summary>
		public static readonly BindableProperty BackgroundColorSelectedProperty =
			BindableProperty.Create(nameof(BackgroundColorSelected), typeof(Color), typeof(RepeaterView), Color.Default, 
				propertyChanged: ControlPropertyChanged);

		/// <summary>
		/// The items source property
		/// </summary>
		public static readonly BindableProperty ItemsSourceProperty =
			BindableProperty.Create(nameof(ItemsSource), typeof(object), typeof(RepeaterView),
				propertyChanged: ControlPropertyChanged);

		/// <summary>
		/// The item template property
		/// </summary>
		public static readonly BindableProperty ItemTemplateProperty =
			BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(RepeaterView),
				propertyChanged: ControlPropertyChanged);

		/// <summary>
		/// The repeat count property
		/// </summary>
		public static readonly BindableProperty RepeatCountProperty =
			BindableProperty.Create(nameof(RepeatCount), typeof(int), typeof(RepeaterView),
				propertyChanged: ControlPropertyChanged);

		/// <summary>
		/// The repeate visibility property
		/// </summary>
		public static readonly BindableProperty RepeatVisibilityProperty =
			BindableProperty.Create(nameof(RepeatVisibility), typeof(RepeatVisibilityMode), typeof(RepeaterView), 
				RepeatVisibilityMode.All);

		/// <summary>
		/// The selected item property
		/// </summary>
		public static readonly BindableProperty SelectedItemProperty =
			BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(RepeaterView), defaultBindingMode: BindingMode.TwoWay, 
				propertyChanged: SelectedItemPropertyChanged);

		/// <summary>
		/// The selected text color property
		/// </summary>
		public static readonly BindableProperty SelectedTextColorProperty =
			BindableProperty.Create(nameof(SelectedTextColor), typeof(Color), typeof(RepeaterView), Color.Default);

		/// <summary>
		/// The text color property
		/// </summary>
		public static readonly BindableProperty TextColorProperty =
			BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(RepeaterView), Color.Default);
		#endregion

		#region Constructor methods
		/// <summary>
		/// Initializes a new instance of the <see cref="RepeaterView"/> class.
		/// </summary>
		public RepeaterView()
		{
			Spacing = 0;
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Called when [property changed].
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void ControlPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			try
			{
				// Get control
				var repeater = (RepeaterView) bindable;

				// If we don't have a template yet (binding order) then bail 
				if (repeater.ItemTemplate == null)
				{
					return;
				}

				// If we have an old data then clear out the controls
				if (oldValue != null)
				{
					repeater.Children.Clear();
				}

				// New data (but it might be the template that got updated)
				if (newValue != null)
				{
					// Iterate items
					if (newValue is ICollection itemList)
					{
						if (itemList.Count > 0)
						{
							var itemCount = itemList.Count;
							var itemsArray = new object[itemCount];
							itemList.CopyTo(itemsArray, 0);

							for (var i = 0; i < itemCount; i++)
							{
								var item = itemsArray[i];

								// Do we have a template 
								if (repeater.ItemTemplate == null)
								{
									// No, so exit
									return;
								}

								View itemView;

								// Determine if this is a straight up template or using a DataTemplateSelector
								if (repeater.ItemTemplate is DataTemplateSelector dataTemplateSelector)
								{
									// Figure out template and create controls
									var template = dataTemplateSelector.SelectTemplate(item, repeater);
									itemView = (View) template.CreateContent();
								}
								else
								{
									// Create controls
									itemView = (View) repeater.ItemTemplate.CreateContent();
								}

								try
								{
									// Create tap recognizer
									var tapRecognizer = new TapGestureRecognizer();
									tapRecognizer.Tapped += repeater.Item_Tapped;

									// Bind it, add tap gesture and add it
									itemView.GestureRecognizers.Add(tapRecognizer);

									// Add item 
									itemView.BindingContext = item;
									repeater.Children.Add(itemView);
								}
								catch (Exception ex)
								{
									Console.WriteLine(ex.Message);
								}
							}
						}
					}
					else if (newValue is int repeats)
					{
						// Simple repeat count, good for ratings where you just want to repeat a 'view'
						for (var i = 0; i < repeats; i++)
						{
							// Create controls
							// https://stackoverflow.com/questions/41233767/xamarin-forms-create-instance-of-a-datatemplate
							var itemView = (View) repeater.ItemTemplate.CreateContent();

							// Create tap recognizer
							var tapRecognizer = new TapGestureRecognizer();
							tapRecognizer.Tapped += repeater.Item_Tapped;

							// Bind it, add tap gesture and add it
							itemView.GestureRecognizers.Add(tapRecognizer);

							// Add item 
							itemView.BindingContext = repeater.ItemsSource;
							repeater.Children.Add(itemView);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				throw;
			}
		}

		/// <summary>
		/// Handles the <see cref="E:ItemTapped" /> event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void Item_Tapped(object sender, EventArgs e)
		{
			if (!(sender is View view))
			{
				return;
			}

			// Get items
			var itemList = (IList) ItemsSource;

			// Get previous selection
			var previousSelection = selectedIndex >= 0 ? itemList[selectedIndex] : null;

			// Get current selection
			var capturedIndex = Children.IndexOf(view);
			var currentSelection = itemList[capturedIndex];

			// Show/hide ?
			if (RepeatVisibility.Equals(RepeatVisibilityMode.SelectedOnly))
			{
				if (previousSelection != null)
				{
					// Hide old
					selectedView.IsVisible = false;
				}

				// Show new
				view.IsVisible = true;
			}

			// Select it
			SelectedItem = currentSelection;
			selectedIndex = capturedIndex;
			selectedView = view;

			var eventArgs = new RepeaterEventArgs(previousSelection, currentSelection);
			SelectionChanged?.Invoke(this, eventArgs);

			OnPropertyChanged(SelectedItemProperty.PropertyName);
		}

		private static void ProcessChildControls(View view, Color color)
		{
			if (view is Label label)
			{
				label.TextColor = color;
			} 
			else if (view is StackLayout stackLayout)
			{
				foreach (var child in stackLayout.Children)
				{
					ProcessChildControls(child, color);
				}
			}
		}

		/// <summary>
		/// Called when [selected item property changed].
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void SelectedItemPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			// Get control
			var repeater = (RepeaterView) bindable;

			foreach (var child in repeater.Children)
			{
				if (child.BindingContext.Equals(newValue))
				{
					child.BackgroundColor = repeater.BackgroundColorSelected;
					repeater.selectedIndex = repeater.Children.IndexOf(child);

					if (repeater.RepeatVisibility.Equals(RepeatVisibilityMode.SelectedOnly))
					{
						repeater.selectedView = repeater.Children[repeater.selectedIndex];
						repeater.selectedView.IsVisible = true;
					}

					if (repeater.SelectedTextColor != Color.Default)
					{
						ProcessChildControls(child, repeater.SelectedTextColor);
					}
				}
				else
				{
					child.BackgroundColor = repeater.BackgroundColor;

					if (repeater.RepeatVisibility.Equals(RepeatVisibilityMode.SelectedOnly))
					{
						var index = repeater.Children.IndexOf(child);
						var view = repeater.Children[index];
						view.IsVisible = false;
					}

					ProcessChildControls(child, repeater.TextColor);
				}
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the background color selected.
		/// </summary>
		/// <value>The background color selected.</value>
		public Color BackgroundColorSelected
		{
			get => (Color) GetValue(BackgroundColorSelectedProperty);
			set => SetValue(BackgroundColorSelectedProperty, value);
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
		/// Gets or sets the item template.
		/// </summary>
		/// <value>The item template.</value>
		public DataTemplate ItemTemplate
		{
			get => (DataTemplate) GetValue(ItemTemplateProperty);
			set => SetValue(ItemTemplateProperty, value);
		}

		/// <summary>
		/// Gets or sets the repeat count.
		/// </summary>
		/// <value>The repeat count.</value>
		public int RepeatCount
		{
			get => (int) GetValue(RepeatCountProperty);
			set => SetValue(RepeatCountProperty, value);
		}

		/// <summary>
		/// Gets or sets the repeat visibility.
		/// </summary>
		/// <value>The repeat visibility.</value>
		public RepeatVisibilityMode RepeatVisibility
		{
			get => (RepeatVisibilityMode) GetValue(RepeatVisibilityProperty);
			set => SetValue(RepeatVisibilityProperty, value);

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
		/// Gets or sets the text color selected.
		/// </summary>
		/// <value>The text color selected.</value>
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
		#endregion

		#region Fields
		private int selectedIndex = -1;
		private View selectedView;
		#endregion
	}
}