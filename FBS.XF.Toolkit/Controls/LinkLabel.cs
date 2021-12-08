using System;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Controls
{
	/// <summary>
	/// LinkLabel.
	/// </summary>
	/// <seealso cref="Label" />
	public class LinkLabel : Label, IDisposable
	{
		#region Events/Delegates
		/// <summary>
		/// Occurs when [tapped].
		/// </summary>
		public event EventHandler<ItemTappedEventArgs> Tapped;
		#endregion

		#region Bindable Properties
		/// <summary>
		/// The text property
		/// </summary>
		public new static readonly BindableProperty TextProperty =
			BindableProperty.Create(nameof(Text), typeof(string), typeof(LinkLabel), default(string),
				propertyChanged: TextPropertyChanged);

		/// <summary>
		/// The text link color property
		/// </summary>
		public static readonly BindableProperty TextLinkColorProperty =
			BindableProperty.Create(nameof(TextLinkColor), typeof(Color), typeof(LinkLabel), default(Color),
				propertyChanged: TextLinkColorPropertyChanged);
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="LinkLabel"/> class.
		/// </summary>
		public LinkLabel()
		{
			if (FormattedText == null)
			{
				CreateControl();
			}
		}
	#endregion

		#region IDisposable
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			tapRecognizer.Tapped -= OnLink_Tapped;
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Creates the control.
		/// </summary>
		private void CreateControl()
		{
			// Create a tap recognizer
			tapRecognizer = new TapGestureRecognizer();
			tapRecognizer.Tapped += OnLink_Tapped;
			
			// Create content
			FormattedText = new FormattedString();

			// Create spans and add tap gesture to the link span
			linkSpan = new Span();
			linkSpan.GestureRecognizers.Add(tapRecognizer);
			postLinkSpan = new Span();
			preLinkSpan = new Span();
		}

		/// <summary>
		/// Texts the link color property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void TextLinkColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			RebuildFormattedString(bindable);
		}

		/// <summary>
		/// Rebuilds the formatted string.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		private static void RebuildFormattedString(BindableObject bindable)
		{
			// Get control and new value
			var control = (LinkLabel)bindable;

			// Now clear links 
			control.FormattedText.Spans.Clear();

			// Any text?
			if (!string.IsNullOrWhiteSpace(control.Text))
			{
				// Do we have an emebedded link?
				if (control.Text.Contains("[") && control.Text.Contains("]"))
				{
					// Get start and end of link
					var startIndex = control.Text.IndexOf('[');
					var endIndex = control.Text.IndexOf(']');

					// Get text elements
					var preLinkText = control.Text.Left(startIndex);
					var linkText = control.Text.Substring(startIndex + 1, endIndex - startIndex - 1);
					var postLinkText = control.Text.Right(control.Text.Length - endIndex - 1);

					if (!string.IsNullOrWhiteSpace(preLinkText))
					{
						control.preLinkSpan.Text = preLinkText;
						control.FormattedText.Spans.Add(control.preLinkSpan);
					}

					control.linkSpan.Text = linkText;
					control.linkSpan.TextColor = control.TextLinkColor;
					control.FormattedText.Spans.Add(control.linkSpan);

					if (!string.IsNullOrWhiteSpace(postLinkText))
					{
						control.postLinkSpan.Text = postLinkText;
						control.FormattedText.Spans.Add(control.postLinkSpan);
					}
				}
				else
				{
					control.linkSpan.Text = control.Text;
					control.linkSpan.TextColor = control.TextLinkColor;
					control.FormattedText.Spans.Add(control.linkSpan);
				}
			}
		}

		/// <summary>
		/// Texts the property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void TextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (!string.IsNullOrWhiteSpace(newValue.ToString()))
			{
				RebuildFormattedString(bindable);
			}
		}
		#endregion
		
		#region UI methods
		/// <summary>
		/// Handles the <see cref="E:ItemTapped" /> event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void OnLink_Tapped(object sender, EventArgs e)
		{
			Tapped?.Invoke(this, new ItemTappedEventArgs(Text, Id, 0));
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the static text.
		/// </summary>
		/// <value>The static text.</value>
		public new string Text
		{
			get => (string) GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}

		/// <summary>
		/// Gets or sets the color of the text link.
		/// </summary>
		/// <value>The color of the text link.</value>
		public Color TextLinkColor
		{
			get => (Color)GetValue(TextLinkColorProperty);
			set => SetValue(TextLinkColorProperty, value);
		}
		#endregion

		#region Fields
		private Span linkSpan;
		private Span postLinkSpan;
		private Span preLinkSpan;
		private TapGestureRecognizer tapRecognizer;
		#endregion
	}
}
