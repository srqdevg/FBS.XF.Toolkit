using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Helpers
{
	/// <summary>
	/// Dump Controls.
	/// </summary>
	public static class DumpControls
	{
		#region Public methods
		/// <summary>
		/// Processes the control.
		/// </summary>
		/// <param name="control">The control.</param>
		/// <param name="sb">The sb.</param>
		/// <param name="isGrid">if set to <c>true</c> [is grid].</param>
		public static void ProcessControl(View control, StringBuilder sb, bool isGrid = false)
		{
			// Iterate children
			if (control is StackLayout stacklayout)
			{
				// Spit out layout details
				sb.Append("<StackLayout ");

				AddGrid(control, isGrid, sb);

				AddDouble(stacklayout.HeightRequest, "HeightRequest", sb);
				AddLayout(stacklayout.HorizontalOptions, "HorizontalOptions", sb);
				AddOrientation(stacklayout.Orientation, "Orientation", sb);
				AddLayout(stacklayout.VerticalOptions, "VerticalOptions", sb);
				AddDouble(stacklayout.WidthRequest, "WidthRequest", sb);
				sb.AppendLine(">");

				foreach (var child in stacklayout.Children)
				{
					ProcessControl(child, sb);
				}

				sb.AppendLine("</StackLayout>");
			}
			else if (control is ScrollView scrollView)
			{
				// Spit out Label details
				sb.AppendLine("<ScrollView>");

				AddGrid(control, isGrid, sb);

				foreach (var child in scrollView.Children)
				{
					ProcessControl((View) child, sb);
				}

				sb.AppendLine("</ScrollView>");
			}
			else if (control is Label label)
			{
				// Spit out Label details
				sb.Append("<Label ");

				AddGrid(control, isGrid, sb);

				AddDouble(label.FontSize, "FontSize", sb);
				AddDouble(label.HeightRequest, "HeightRequest", sb);
				AddLayout(label.HorizontalOptions, "HorizontalOptions", sb);
				AddTextAlignment(label.HorizontalTextAlignment, "HorizontalTextAlignment", sb);
				AddString(label.Text, "Text", sb);
				AddLayout(label.VerticalOptions, "VerticalOptions", sb);
				AddTextAlignment(label.VerticalTextAlignment, "VerticalTextAlignment", sb);
				AddDouble(label.WidthRequest, "WidthRequest", sb);

				sb.AppendLine(" />");
			}
			else if (control is Grid grid)
			{
				// Spit out Label details
				sb.AppendLine("<Grid>");

				AddGridRowColumnDefinitions(grid, sb);

				AddGrid(control, isGrid, sb);

				foreach (var child in grid.Children)
				{
					ProcessControl(child, sb, true);
				}

				sb.AppendLine("</Grid>");
			}
			else
			{
				// Unknown
				sb.AppendLine($"Unknown {control}");
			}
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Adds the double.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="name">The name.</param>
		/// <param name="sb">The sb.</param>
		private static void AddDouble(double value, string name, StringBuilder sb)
		{
			if (value > -1)
			{
				sb.Append($" {name}=\"{value}\"");
			}
		}

		/// <summary>
		/// Adds the integer.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="name">The name.</param>
		/// <param name="sb">The sb.</param>
		private static void AddInteger(int value, string name, StringBuilder sb)
		{
			if (value > -1)
			{
				sb.Append($" {name}=\"{value}\"");
			}
		}

		/// <summary>
		/// Adds the layout.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="name">The name.</param>
		/// <param name="sb">The sb.</param>
		private static void AddLayout(LayoutOptions value, string name, StringBuilder sb)
		{
			switch (value.Alignment)
			{
				case LayoutAlignment.Start:
					sb.Append(value.Expands ? $" {name}=\"StartAndExpand\"" : $" {name}=\"Start\"");
					break;
				case LayoutAlignment.Center:
					sb.Append(value.Expands ? $" {name}=\"CenterAndExpand\"" : $" {name}=\"Center\"");
					break;
				case LayoutAlignment.End:
					sb.Append(value.Expands ? $" {name}=\"ENdAndExpand\"" : $" {name}=\"End\"");
					break;
				default:
					sb.Append(value.Expands ? $" {name}=\"FillAndExpand\"" : $" {name}=\"Fill\"");
					break;
			}
		}

		/// <summary>
		/// Adds the orientation.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="name">The name.</param>
		/// <param name="sb">The sb.</param>
		private static void AddOrientation(StackOrientation value, string name, StringBuilder sb)
		{
			sb.Append(value.Equals(StackOrientation.Horizontal)
				? $" {name}=\"Horizontal\""
				: $" {name}=\"Vertical\"");
		}

		/// <summary>
		/// Adds the text alignment.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="name">The name.</param>
		/// <param name="sb">The sb.</param>
		private static void AddTextAlignment(TextAlignment value, string name, StringBuilder sb)
		{
			switch (value)
			{
				case TextAlignment.Start:
					sb.Append($" {name}=\"Start\"");
					break;
				case TextAlignment.Center:
					sb.Append($" {name}=\"Center\"");
					break;
				default:
					sb.Append($" {name}=\"End\"");
					break;
			}
		}

		/// <summary>
		/// Adds the string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="name">The name.</param>
		/// <param name="sb">The sb.</param>
		private static void AddString(string value, string name, StringBuilder sb)
		{
			sb.Append($" {name}=\"{value.Replace("&", "&amp;")}\"");
		}

		/// <summary>
		/// Adds the grid.
		/// </summary>
		/// <param name="control">The control.</param>
		/// <param name="isGrid">if set to <c>true</c> [is grid].</param>
		/// <param name="sb">The sb.</param>
		private static void AddGrid(View control, bool isGrid, StringBuilder sb)
		{
			if (isGrid)
			{
				AddInteger(Grid.GetRow(control), "Row", sb);
				AddInteger(Grid.GetRowSpan(control), "RowSpan", sb);
				AddInteger(Grid.GetColumn(control), "Column", sb);
				AddInteger(Grid.GetColumnSpan(control), "ColumnSpan", sb);
			}
		}

		/// <summary>
		/// Adds the grid row column definitions.
		/// </summary>
		/// <param name="grid">The grid.</param>
		/// <param name="sb">The sb.</param>
		private static void AddGridRowColumnDefinitions(Grid grid, StringBuilder sb)
		{
			var rows = grid.RowDefinitions.ToList();

			if (rows.Count > 0)
			{
				sb.AppendLine("<Grid.RowDefinitions>");

				foreach (var row in rows)
				{
					sb.Append("<RowDefinition ");

					if (row.Height.IsAuto)
					{
						AddString("Auto", "Height", sb);
					}
					else if (row.Height.IsStar)
					{
						AddString("*", "Height", sb);
					}
					else
					{
						AddDouble(row.Height.Value, "Height", sb);
					}

					sb.Append("/>");
				}

				sb.AppendLine("</Grid.RowDefinitions>");
			}

			var columns = grid.ColumnDefinitions.ToList();

			if (columns.Count > 0)
			{
				sb.AppendLine("<Grid.ColumnDefinitions>");

				foreach (var column in columns)
				{
					sb.Append("<ColumnDefinition ");

					if (column.Width.IsAuto)
					{
						AddString("Auto", "Width", sb);
					}
					else if (column.Width.IsStar)
					{
						AddString("*", "Width", sb);
					}
					else
					{
						AddDouble(column.Width.Value, "Width", sb);
					}

					sb.Append("/>");
				}

				sb.AppendLine("</Grid.ColumnDefinitions>");
			}
		}
		#endregion
	}
}
