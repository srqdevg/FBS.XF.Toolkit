using System;
using System.Collections;
using System.Linq;
using FBS.XF.Toolkit.Event;
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
	public class RepeaterView : StackLayout, IDisposable
	{
		#region Constructor methods
		/// <summary>
		/// Initializes a new instance of the <see cref="RepeaterView"/> class.
		/// </summary>
		public RepeaterView()
		{
			Spacing = 0;
			Padding = 0;
		}
		#endregion

		#region IDisposable
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			foreach (var child in Children)
			{
				if (child.GestureRecognizers.Any())
				{
					foreach (var gestureRecognizer in child.GestureRecognizers)
					{
						((TapGestureRecognizer) gestureRecognizer).Tapped -= Item_Tapped;
					}
				}
			}
		}
		#endregion

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
				propertyChanged: (bd, ov, nv) => ((RepeaterView) bd).ControlPropertyChanged(ov, nv));

		/// <summary>
		/// The items source property
		/// </summary>
		public static readonly BindableProperty ItemsSourceProperty =
			BindableProperty.Create(nameof(ItemsSource), typeof(object), typeof(RepeaterView),
				propertyChanged: (bd, ov, nv) => ((RepeaterView) bd).ControlPropertyChanged(ov, nv));

		/// <summary>
		/// The item template property
		/// </summary>
		public static readonly BindableProperty ItemTemplateProperty =
			BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(RepeaterView),
				propertyChanged: (bd, ov, nv) => ((RepeaterView) bd).ControlPropertyChanged(ov, nv));

		/// <summary>
		/// The repeat count property
		/// </summary>
		public static readonly BindableProperty RepeatCountProperty =
			BindableProperty.Create(nameof(RepeatCount), typeof(int), typeof(RepeaterView),
				propertyChanged: (bd, ov, nv) => ((RepeaterView) bd).ControlPropertyChanged(ov, nv));

		/// <summary>
		/// The repeate visibility property
		/// </summary>
		public static readonly BindableProperty RepeatVisibilityProperty =
			BindableProperty.Create(nameof(RepeatVisibility), typeof(RepeatVisibilityMode), typeof(RepeaterView),
				RepeatVisibilityMode.All);

		/// <summary>
		/// The selected indexroperty
		/// </summary>
		public static readonly BindableProperty SelectedIndexProperty =
			BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(RepeaterView), -1, BindingMode.TwoWay,
				propertyChanged: (bd, ov, nv) => ((RepeaterView) bd).SelectedItemPropertyChanged(ov, nv));

		/// <summary>
		/// The selected item property
		/// </summary>
		public static readonly BindableProperty SelectedItemProperty =
			BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(RepeaterView),
				defaultBindingMode: BindingMode.TwoWay,
				propertyChanged: (bd, ov, nv) => ((RepeaterView) bd).SelectedItemPropertyChanged(ov, nv));

		/// <summary>
		/// The select mode property
		/// </summary>
		public static readonly BindableProperty SelectionModeProperty =
			BindableProperty.Create(nameof(SelectionMode), typeof(SelectionMode), typeof(RepeaterView), SelectionMode.Single);

		/// <summary>
		/// The selected text color property
		/// </summary>
		public static readonly BindableProperty SelectedTextColorProperty =
			BindableProperty.Create(nameof(SelectedTextColor), typeof(Color), typeof(RepeaterView), Color.Default);

		/// <summary>
		/// The separator property
		/// </summary>
		public static readonly BindableProperty SeparatorProperty =
			BindableProperty.Create(nameof(Separator), typeof(string), typeof(RepeaterView),
				propertyChanged: (bd, ov, nv) => ((RepeaterView) bd).ControlPropertyChanged(ov, nv));

		/// <summary>
		/// The separator template property
		/// </summary>
		public static readonly BindableProperty SeparatorTemplateProperty =
			BindableProperty.Create(nameof(SeparatorTemplate), typeof(DataTemplate), typeof(RepeaterView),
				propertyChanged: (bd, ov, nv) => ((RepeaterView) bd).ControlPropertyChanged(ov, nv));

		/// <summary>
		/// The text color property
		/// </summary>
		public static readonly BindableProperty TextColorProperty =
			BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(RepeaterView), Color.Default);

		/// <summary>
		/// The tag property
		/// </summary>
		public static readonly BindableProperty TagProperty =
			BindableProperty.Create(nameof(Tag), typeof(string), typeof(RepeaterView));
		#endregion

		#region UI methods
		/// <summary>
		/// Buttons the on clicked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="ClickedEventArgs"/> instance containing the event data.</param>
		private void ButtonOnClicked(object sender, ClickedEventArgs e)
		{
			// Find parent 
			var button = (CustomButton) sender;
			

			// And invoke on the parent i.e. us
			Item_Tapped(button.Parent, e);
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Called when [property changed].
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void ControlPropertyChanged(object oldValue, object newValue)
		{
			try
			{
				// If we don't have a template yet (binding order) then bail 
				if (ItemTemplate == null)
				{
					return;
				}

				// If we have an old data then clear out the controls
				if (oldValue != null)
				{
					Children.Clear();
				}

				// New data (but it might be the template that got updated)
				if (newValue != null)
				{
					// Is this the data template?
					if (newValue is DataTemplate)
					{
						// Yes, so do we have actual data to work with?
						if (ItemsSource != null)
						{
							newValue = ItemsSource;
						}
					}

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
								if (ItemTemplate == null)
								{
									// No, so exit
									return;
								}

								View itemView;

								// Check for separator and if it is, we create the separator
								if (IsSeparator(item))
								{
									// Do we have a template?
									if (SeparatorTemplate == null)
									{
										continue;
									}

									// Create controls
									itemView = (View) SeparatorTemplate.CreateContent();
									itemView.BindingContext = item;
								}
								else
								{
									// Determine if this is a straight up template or using a DataTemplateSelector
									if (ItemTemplate is DataTemplateSelector dataTemplateSelector)
									{
										// Figure out template and create controls
										var template = dataTemplateSelector.SelectTemplate(item, this);
										itemView = (View) template.CreateContent();
									}
									else
									{
										// Create controls
										itemView = (View) ItemTemplate.CreateContent();
									}

									itemView.HorizontalOptions = LayoutOptions.FillAndExpand;
									itemView.BindingContext = item;
									itemView.VerticalOptions = LayoutOptions.FillAndExpand;

									// Does the item view contain a clickable action, if so we want
									// to attach to that as we won't see the action bubbled up to us
									var foundAction = false;

									if (itemView is Layout layout)
									{
										foreach (var child in layout.Children)
										{
											// Yes, becuase I know that controls I've used for this, I'm cheating and looking for specifics
											if (child is CustomButton button)
											{
												button.Clicked += ButtonOnClicked;
												foundAction = true;
											}
										}

										// If we have found buttons, then look for a layout within to add tap to, i.e. not the outer
										if (foundAction)
										{
											foreach (var child in layout.Children)
											{
												if (child is StackLayout childLayout)
												{
													var childtapRecognizer = new TapGestureRecognizer();
													childtapRecognizer.Tapped += Item_Tapped;

													// Bind it, add tap gesture and add it
													childLayout.GestureRecognizers.Add(childtapRecognizer);
												}
											}
										}
									}

									// If no actionable items found then add tap recognition
									if (!foundAction)
									{
										// Create tap recognizer
										var tapRecognizer = new TapGestureRecognizer();
										tapRecognizer.Tapped += Item_Tapped;

										// Bind it, add tap gesture and add it
										itemView.GestureRecognizers.Add(tapRecognizer);
									}
								}

								// Add item 
								Children.Add(itemView);
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
							var itemView = (View) ItemTemplate.CreateContent();

							// Create tap recognizer
							var tapRecognizer = new TapGestureRecognizer();
							tapRecognizer.Tapped += Item_Tapped;

							// Bind it, add tap gesture and add it
							itemView.GestureRecognizers.Add(tapRecognizer);

							// Add item 
							itemView.HorizontalOptions = LayoutOptions.FillAndExpand;
							itemView.BindingContext = ItemsSource;
							itemView.VerticalOptions = LayoutOptions.FillAndExpand;
							Children.Add(itemView);
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
		/// Determines whether the specified item is separator.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns>bool.</returns>
		private bool IsSeparator(object item)
		{
			// Do we have a separator column specific
			if (!string.IsNullOrWhiteSpace(Separator))
			{
				// Get value
				var value = item.GetType().GetProperty(Separator)?.GetValue(item, null);
				return value != null && (bool) value;
			}

			return false;
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

			if (itemList.Count == 0)
			{
				return;
			}

			// Get previous selection
			var previousSelection = SelectedIndex >= 0 && SelectedIndex < itemList.Count ? itemList[SelectedIndex] : null;

			// Get current selection
			var capturedIndex = Children.IndexOf(view) ;

			if (capturedIndex == -1)
			{
				capturedIndex = Children.IndexOf(view.Parent as View);
				
				if (capturedIndex == -1)
				{
					return;
				}
			}

			var currentSelection = itemList[capturedIndex];

			// Show/hide ?
			if (RepeatVisibility.Equals(RepeatVisibilityMode.SelectedOnly))
			{
				if (previousSelection != null && selectedView != null)
				{
					// Hide old
					selectedView.IsVisible = false;
				}

				// Show new
				view.IsVisible = true;
			}

			// Select it
			SelectedItem = currentSelection;
			SelectedIndex = capturedIndex;
			selectedView = view;

			var eventArgs = new RepeaterEventArgs(previousSelection, currentSelection);
			SelectionChanged?.Invoke(this, eventArgs);

			OnPropertyChanged(SelectedItemProperty.PropertyName);
		}

		/// <summary>
		/// Processes the child controls.
		/// </summary>
		/// <param name="view">The view.</param>
		/// <param name="color">The color.</param>
		/// <param name="isSelected">if set to <c>true</c> [is selected].</param>
		private static void ProcessChildControls(View view, Color color, bool isSelected)
		{
			if (view is Label label)
			{
				label.TextColor = color;
			}
			else if (view is SvgImage image)
			{
				// UGLY, BUT WITH THEM WANTING WPF AND MAC, I CAN'T USE PROPER CONTROLS, SO ONE MORE HACK..
				image.IsSelected = isSelected;
			}
			else if (view is CustomButton {IsEnabled: true} button)
			{
				// UGLY, BUT WITH THEM WANTING WPF AND MAC, I CAN'T USE PROPER CONTROLS, SO ONE MORE HACK..
				button.IsToggled = isSelected;
			}
			else if (view is StackLayout stackLayout)
			{
				foreach (var child in stackLayout.Children)
				{
					ProcessChildControls(child, color, isSelected);
				}
			}
		}

		/// <summary>
		/// Called when [selected item property changed].
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		// ReSharper disable once UnusedParameter.Local
		private void SelectedItemPropertyChanged(object oldValue, object newValue)
		{
			var tabindex = 0;
			var selectedTabIndex = newValue as int? ?? -1;

			foreach (var child in Children)
			{
				if (child.BindingContext.Equals(newValue) || tabindex.Equals(selectedTabIndex))
				{
					child.BackgroundColor = BackgroundColorSelected;

					if (RepeatVisibility.Equals(RepeatVisibilityMode.SelectedOnly))
					{
						selectedView = Children[tabindex];
						selectedView.IsVisible = true;
					}

					if (SelectedTextColor != Color.Default)
					{
						ProcessChildControls(child, SelectedTextColor, true);
					}
				}
				else
				{
					child.BackgroundColor = BackgroundColor;

					if (RepeatVisibility.Equals(RepeatVisibilityMode.SelectedOnly))
					{
						var index = Children.IndexOf(child);
						var view = Children[index];
						view.IsVisible = false;
					}

					if (SelectionMode.Equals(SelectionMode.Single))
					{
						ProcessChildControls(child, TextColor, false);
					}
				}

				tabindex++;
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
		/// Gets or sets the index of the selected.
		/// </summary>
		/// <value>The index of the selected.</value>
		public int SelectedIndex
		{
			get => (int) GetValue(SelectedIndexProperty);
			set => SetValue(SelectedIndexProperty, value);
		}

		/// <summary>
		/// Gets or sets the selection mode.
		/// </summary>
		/// <value>The selection mode.</value>
		public SelectionMode SelectionMode
		{
			get => (SelectionMode) GetValue(SelectionModeProperty);
			set => SetValue(SelectionModeProperty, value);
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
		/// Gets or sets the separator.
		/// </summary>
		/// <value>The separator.</value>
		public string Separator
		{
			get => (string) GetValue(SeparatorProperty);
			set => SetValue(SeparatorProperty, value);
		}

		/// <summary>
		/// Gets or sets the separator template.
		/// </summary>
		/// <value>The separator template.</value>
		public DataTemplate SeparatorTemplate
		{
			get => (DataTemplate) GetValue(SeparatorTemplateProperty);
			set => SetValue(SeparatorTemplateProperty, value);
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
		/// Gets or sets the tag.
		/// </summary>
		/// <value>The tag.</value>
		public string Tag
		{
			get => (string) GetValue(TagProperty);
			set => SetValue(TagProperty, value);
		}
		#endregion

		#region Fields
		private View selectedView;
		#endregion
	}
}