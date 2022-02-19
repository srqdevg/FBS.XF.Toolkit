namespace FBS.XF.Toolkit.Models
{
	/// <summary>
	/// Id Text.
	/// </summary>
	public class IdText
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="IdText"/> class.
		/// </summary>
		public IdText()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IdText"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="text">The text.</param>
		/// <param name="isSelected">if set to <c>true</c> [is selected].</param>
		public IdText(int id, string text, bool isSelected = true)
		{
			Id = id.ToString();
			Text = text;
			IsSelected = isSelected;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IdText" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="text">The name.</param>
		/// <param name="isSelected">if set to <c>true</c> [is selected].</param>
		public IdText(string id, string text, bool isSelected = true)
		{
			Id = id;
			Text = text;
			IsSelected = isSelected;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is selected.
		/// </summary>
		/// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
		public bool IsSelected { get; set; }
		#endregion
	}
}