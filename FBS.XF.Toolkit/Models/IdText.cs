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
		/// <param name="id">The identifier.</param>
		/// <param name="text">The name.</param>
		public  IdText(string id, string text)
		{
			Id = id;
			Text = text;
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
		#endregion
	}
}
