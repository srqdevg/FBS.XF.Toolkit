namespace FBS.XF.Toolkit.Models
{
	/// <summary>
	/// Id Text.
	/// </summary>
	public class IntText
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="IntText"/> class.
		/// </summary>
		public IntText()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IntText" /> class.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="text">The text.</param>
		public IntText(int value, string text)
		{
			Value = value;
			Text = text;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text { get; set; }

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public int Value { get; set; }
		#endregion
	}
}