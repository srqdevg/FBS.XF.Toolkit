using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using FBS.XF.Toolkit.Android.Renderers;
using FBS.XF.Toolkit.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using RadioButton = Android.Widget.RadioButton;

[assembly: ExportRenderer(typeof(SegmentedControl), typeof(SegmentedControlRenderer))]
namespace FBS.XF.Toolkit.Android.Renderers
{
	/// <summary>
	/// SegmentedControlRenderer.
	/// Implements the <see cref="Xamarin.Forms.Platform.Android.ViewRenderer{FBS.XF.Toolkit.Controls.SegmentedControl, Android.Widget.RadioGroup}" />
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Platform.Android.ViewRenderer{FBS.XF.Toolkit.Controls.SegmentedControl, Android.Widget.RadioGroup}" />
	[Preserve(AllMembers = true)]
	public class SegmentedControlRenderer : ViewRenderer<SegmentedControl, RadioGroup>
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="SegmentedControlRenderer"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public SegmentedControlRenderer(Context context) : base(context)
		{
			this.context = context;
		}
		#endregion

		#region Override methods
		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (!(nativeControl is null))
			{
				nativeControl.CheckedChange -= NativeControl_ValueChanged;
			}

			if (!(nativeRadioButtonControl is null))
			{
				nativeRadioButtonControl.Dispose();
				nativeRadioButtonControl = null;
			}

			RemoveElementHandlers();

			try
			{
				base.Dispose(disposing);
				nativeControl = null;
			}
			catch (Exception)
			{
				return;
			}
		}

		/// <summary>
		/// Called when [element changed].
		/// </summary>
		/// <param name="e">The e.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<SegmentedControl> e)
		{
			base.OnElementChanged(e);

			if (Control is null)
			{
				// Instantiate the native control and assign it to the Control property with
				// the SetNativeControl method
			}

			if (!(e.OldElement is null))
			{
				// Unsubscribe from event handlers and cleanup any resources
				if (nativeControl != null)
				{
					nativeControl.CheckedChange -= NativeControl_ValueChanged;
				}

				RemoveElementHandlers();
			}

			if (!(e.NewElement is null))
			{
				// Configure the control and subscribe to event handlers
				AddElementHandlers();
			}
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			switch (e.PropertyName)
			{
				case "Renderer":
					Element_SizeChanged(null, null);
					Element?.RaiseSelectionChanged();
					break;

				case nameof(SegmentedControl.SelectedSegment):
					if (!(nativeControl is null) && !(Element is null))
					{
						if (Element.SelectedSegment < 0)
						{
							var layoutInflater = LayoutInflater.From(context);

							//nativeControl = (RadioGroup) layoutInflater.Inflate(Xamarin.Forms.Platform.Android.Resource.Layout.RadioGroup, null);

							SetNativeControlSegments(layoutInflater);

							nativeControl.CheckedChange += NativeControl_ValueChanged;

							SetNativeControl(nativeControl);
						}

						SetSelectedRadioButton(Element.SelectedSegment);

						Element.RaiseSelectionChanged();
					}
					break;

				case nameof(SegmentedControl.TintColor):
				case nameof(SegmentedControl.IsEnabled):
				case nameof(SegmentedControl.FontSize):
				case nameof(SegmentedControl.FontFamily):
				case nameof(SegmentedControl.TextColor):
				case nameof(SegmentedControl.BorderColor):
				case nameof(SegmentedControl.BorderWidth):
					OnPropertyChanged();
					break;

				case nameof(SegmentedControl.SelectedTextColor):
					if (!(nativeControl is null) && !(Element is null))
					{
						var v = (RadioButton) nativeControl.GetChildAt(Element.SelectedSegment);

						v.SetTextColor(Element.SelectedTextColor.ToAndroid());
					}
					break;

				case nameof(SegmentedControl.Children):
					SetNativeControlSegments(LayoutInflater.FromContext(context));

					AddElementHandlers(true);
					break;
			}
		}

		/// <summary>
		/// Sets the background color for this view.
		/// </summary>
		/// <param name="color">the color of the background</param>
		public override void SetBackgroundColor(global::Android.Graphics.Color color)
		{
			unselectedItemBackgroundColor = color;
			OnPropertyChanged();

			base.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Used for registration with dependency service
		/// </summary>
		public static void Init()
		{
			// ReSharper disable once UnusedVariable
			var temp = DateTime.Now;
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Adds the element handlers.
		/// </summary>
		/// <param name="addChildrenHandlersOnly">if set to <c>true</c> [add children handlers only].</param>
		private void AddElementHandlers(bool addChildrenHandlersOnly = false)
		{
			if (!(Element is null))
			{
				if (!addChildrenHandlersOnly)
				{
					Element.SizeChanged += Element_SizeChanged;
					Element.OnElementChildrenChanging += OnElementChildrenChanging;
				}

				if (!(Element.Children is null))
				{
					foreach (var child in Element.Children)
					{
						child.PropertyChanged += Segment_PropertyChanged;
					}
				}
			}
		}

		/// <summary>
		/// Configures the RadioButton.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="radioButton">The radio button.</param>
		private void ConfigureRadioButton(int index, RadioButton radioButton)
		{
			if (index == Element.SelectedSegment)
			{
				radioButton.SetTextColor(Element.SelectedTextColor.ToAndroid());

				nativeRadioButtonControl = radioButton;
			}
			else
			{
				var textColor = Element.IsEnabled
					? Element.TextColor.ToAndroid()
					: Element.DisabledColor.ToAndroid();

				radioButton.SetTextColor(textColor);
			}

			radioButton.TextSize = Convert.ToSingle(Element.FontSize);

			var font = Font.OfSize(Element.FontFamily, Element.FontSize).ToTypeface();

			radioButton.SetTypeface(font, TypefaceStyle.Normal);

			var gradientDrawable = (StateListDrawable) radioButton.Background;

			var drawableContainerState = (DrawableContainer.DrawableContainerState) gradientDrawable?.GetConstantState();

			var children = drawableContainerState?.GetChildren();

			if (!(children is null))
			{
				var selectedShape = children[0] is GradientDrawable drawable
					? drawable
					: (GradientDrawable) ((InsetDrawable) children[0]).Drawable;

				var unselectedShape = children[1] is GradientDrawable drawable1
					? drawable1
					: (GradientDrawable) ((InsetDrawable) children[1]).Drawable;

				var backgroundColor = Element.IsEnabled ? Element.TintColor.ToAndroid() : Element.DisabledColor.ToAndroid();
				var borderColor = Element.IsEnabled ? Element.BorderColor.ToAndroid() : Element.DisabledColor.ToAndroid();
				var borderWidthInPixel = ConvertDipToPixel(Element.BorderWidth);

				if (!(selectedShape is null))
				{
					selectedShape.SetStroke(borderWidthInPixel, borderColor);

					selectedShape.SetColor(backgroundColor);
				}

				if (!(unselectedShape is null))
				{

					unselectedShape.SetStroke(borderWidthInPixel, borderColor);
					unselectedShape.SetColor(unselectedItemBackgroundColor);
				}
			}

			radioButton.Enabled = Element.Children[index].IsEnabled;
		}

		/// <summary>
		/// Converts the dip to pixel.
		/// </summary>
		/// <param name="dip">The dip.</param>
		/// <returns>System.Int32.</returns>
		private int ConvertDipToPixel(double dip)
		{
			return (int) global::Android.Util.TypedValue.ApplyDimension(global::Android.Util.ComplexUnitType.Dip, (float) dip, context.Resources.DisplayMetrics);
		}

		/// <summary>
		/// Handles the SizeChanged event of the Element control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void Element_SizeChanged(object sender, EventArgs e)
		{
			if (Control is null && !(Element is null))
			{
				var layoutInflater = LayoutInflater.From(context);

				nativeControl = (RadioGroup) layoutInflater.Inflate(Resource.Layout.RadioGroup, null);

				SetNativeControlSegments(layoutInflater);

				var option = (RadioButton) nativeControl.GetChildAt(Element.SelectedSegment);

				if (!(option is null))
				{
					option.Checked = true;
				}

				nativeControl.CheckedChange += NativeControl_ValueChanged;

				SetNativeControl(nativeControl);
			}
		}

		/// <summary>
		/// Handles the ValueChanged event of the NativeControl control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RadioGroup.CheckedChangeEventArgs"/> instance containing the event data.</param>
		private void NativeControl_ValueChanged(object sender, RadioGroup.CheckedChangeEventArgs e)
		{
			var rg = (RadioGroup) sender;

			if (rg.CheckedRadioButtonId != -1)
			{
				var id = rg.CheckedRadioButtonId;
				var radioButton = rg.FindViewById(id);
				var radioId = rg.IndexOfChild(radioButton);
				var v = (RadioButton) rg.GetChildAt(radioId);
				var color = Element.IsEnabled ? Element.TextColor.ToAndroid() : Element.DisabledColor.ToAndroid();

				nativeRadioButtonControl?.SetTextColor(color);

				v.SetTextColor(Element.SelectedTextColor.ToAndroid());

				nativeRadioButtonControl = v;

				Element.SelectedSegment = radioId;
			}
		}

		private void OnElementChildrenChanging(object sender, EventArgs e)
		{
			RemoveElementHandlers(true);
		}

		/// <summary>
		/// Called when [property changed].
		/// </summary>
		private void OnPropertyChanged()
		{
			if (nativeControl is null || Element is null)
			{
				return;
			}

			for (var i = 0; i < Element.Children.Count; i++)
			{
				var radioButton = (RadioButton) nativeControl.GetChildAt(i);

				ConfigureRadioButton(i, radioButton);
			}
		}

		/// <summary>
		/// Removes the element handlers.
		/// </summary>
		/// <param name="removeChildrenHandlersOnly">if set to <c>true</c> [remove children handlers only].</param>
		private void RemoveElementHandlers(bool removeChildrenHandlersOnly = false)
		{
			if (!(Element is null))
			{
				if (!removeChildrenHandlersOnly)
				{
					Element.SizeChanged -= Element_SizeChanged;
					Element.OnElementChildrenChanging -= OnElementChildrenChanging;
				}

				if (!(Element.Children is null))
				{
					foreach (var child in Element.Children)
					{
						child.PropertyChanged -= Segment_PropertyChanged;
					}
				}
			}
		}

		/// <summary>
		/// Handles the PropertyChanged event of the Segment control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
		private void Segment_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (!(nativeControl is null) && !(Element is null) && sender is SegmentedControlOption option)
			{
				var index = Element.Children.IndexOf(option);

				if (nativeControl.GetChildAt(index) is RadioButton segment)
				{
					switch (e.PropertyName)
					{
						case nameof(SegmentedControlOption.Text):
							segment.Text = option.Text;
							break;

						case nameof(SegmentedControlOption.IsEnabled):
							segment.Enabled = option.IsEnabled;
							break;
					}
				}
			}
		}

		/// <summary>
		/// Sets the native control segments.
		/// </summary>
		/// <param name="layoutInflater">The layout inflater.</param>
		private void SetNativeControlSegments(LayoutInflater layoutInflater)
		{
			if (nativeControl is null || Element?.Children is null)
			{
				return;
			}

			if (nativeControl.ChildCount > 0)
			{
				nativeControl.RemoveAllViews();
			}

			for (var i = 0; i < Element.Children.Count; i++)
			{
				var o = Element.Children[i];
				var radioButton = (RadioButton) layoutInflater.Inflate(Resource.Layout.RadioButton, null);

				if (radioButton is null)
				{
					return;
				}

				radioButton.LayoutParameters = new RadioGroup.LayoutParams(0, LayoutParams.WrapContent, 1f);
				radioButton.Text = o.Text;

				if (i == 0)
				{
					radioButton.SetBackgroundResource(Resource.Drawable.segmented_control_first_background);
				}
				else if (i == Element.Children.Count - 1)
				{
					radioButton.SetBackgroundResource(Resource.Drawable.segmented_control_last_background);
				}

				ConfigureRadioButton(i, radioButton);

				nativeControl.AddView(radioButton);
			}

			SetSelectedRadioButton(Element.SelectedSegment);
		}

		/// <summary>
		/// Sets the selected RadioButton.
		/// </summary>
		/// <param name="index">The index.</param>
		private void SetSelectedRadioButton(int index)
		{
			if (nativeControl.GetChildAt(index) is RadioButton radioButton)
			{
				radioButton.Checked = true;
			}
		}
		#endregion

		#region Fields
		private readonly Context context;
		private RadioGroup nativeControl;
		private RadioButton nativeRadioButtonControl;
		private global::Android.Graphics.Color unselectedItemBackgroundColor = global::Android.Graphics.Color.Transparent;
		#endregion
	}
}