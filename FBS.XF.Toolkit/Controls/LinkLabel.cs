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
		/// The show underline property
		/// </summary>
		public static readonly BindableProperty ShowUnderlineProperty =
			BindableProperty.Create(nameof(ShowUnderline), typeof(bool), typeof(LinkLabel), true,
				propertyChanged: (bd, ov, nv) => ((LinkLabel) bd).TextPropertyChanged(ov, nv));

		/// <summary>
		/// The text property
		/// </summary>
		public new static readonly BindableProperty TextProperty =
			BindableProperty.Create(nameof(Text), typeof(string), typeof(LinkLabel), default(string),
				propertyChanged: (bd, ov, nv) => ((LinkLabel) bd).TextPropertyChanged(ov, nv));

		/// <summary>
		/// The text link color property
		/// </summary>
		public static readonly BindableProperty TextLinkColorProperty =
			BindableProperty.Create(nameof(TextLinkColor), typeof(Color), typeof(LinkLabel), Color.Blue,
				propertyChanged: (bd, ov, nv) => ((LinkLabel) bd).TextPropertyChanged(ov, nv));
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

		#region Private methods
		/// <summary>
		/// Creates the 
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
		/// Rebuilds the formatted string.
		/// </summary>
		private void RebuildFormattedString()
		{
			// Now clear links 
			FormattedText.Spans.Clear();

			// Any text?
			if (!string.IsNullOrWhiteSpace(Text))
			{
				// Do we have an emebedded link?
				if (Text.Contains("[") && Text.Contains("]"))
				{
					// Get start and end of link
					var startIndex = Text.IndexOf('[');
					var endIndex = Text.IndexOf(']');

					// Get text elements
					var preLinkText = Text.Left(startIndex);
					var linkText = Text.Substring(startIndex + 1, endIndex - startIndex - 1);
					var postLinkText = Text.Right(Text.Length - endIndex - 1);

					if (!string.IsNullOrWhiteSpace(preLinkText))
					{
						preLinkSpan.Text = preLinkText;
						FormattedText.Spans.Add(preLinkSpan);
					}

					linkSpan.Text = linkText;
					linkSpan.TextColor = TextLinkColor;
					linkSpan.TextDecorations = ShowUnderline ? TextDecorations.Underline : TextDecorations.None;
					FormattedText.Spans.Add(linkSpan);

					if (!string.IsNullOrWhiteSpace(postLinkText))
					{
						postLinkSpan.Text = postLinkText;
						FormattedText.Spans.Add(postLinkSpan);
					}
				}
				else
				{
					linkSpan.Text = Text;
					linkSpan.TextColor = TextLinkColor;
					FormattedText.Spans.Add(linkSpan);
				}
			}
		}

		/// <summary>
		/// Texts the property changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void TextPropertyChanged(object oldValue, object newValue)
		{
			if (!string.IsNullOrWhiteSpace(newValue.ToString()) && newValue != oldValue)
			{
				RebuildFormattedString();
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the show underline.
		/// </summary>
		/// <value>The show underline.</value>
		public bool ShowUnderline
		{
			get => (bool) GetValue(ShowUnderlineProperty);
			set => SetValue(ShowUnderlineProperty, value);
		}

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
