using System;
using System.Globalization;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Helpers
{
	/// <summary>
	/// ColorHelper.
	/// </summary>
	public static class ColorHelper
	{
		#region Public methods
		/// <summary>
		/// Colors to name.
		/// </summary>
		/// <param name="color">The color.</param>
		/// <returns>System.String.</returns>
		public static string ColorToName(Color color)
		{
			// Get hex value
			var colorString = color.ToString();
			var colorHex = color.ToHex();

			if (Color.AliceBlue.ToHex() == colorHex)
				return "AliceBlue";
			if (Color.AntiqueWhite.ToHex() == colorHex)
				return "AntiqueWhite";
			if (Color.Aqua.ToHex() == colorHex)
				return "Aqua";
			if (Color.Aquamarine.ToHex() == colorHex)
				return "Aquamarine";
			if (Color.Azure.ToHex() == colorHex)
				return "Azure";
			if (Color.Beige.ToHex() == colorHex)
				return "Beige";
			if (Color.Bisque.ToHex() == colorHex)
				return "Bisque";
			if (Color.Black.ToHex() == colorHex)
				return "Black";
			if (Color.BlanchedAlmond.ToHex() == colorHex)
				return "BlanchedAlmond";
			if (Color.Blue.ToHex() == colorHex)
				return "Blue";
			if (Color.BlueViolet.ToHex() == colorHex)
				return "BlueViolet";
			if (Color.Brown.ToHex() == colorHex)
				return "Brown";
			if (Color.BurlyWood.ToHex() == colorHex)
				return "BurlyWood";
			if (Color.CadetBlue.ToHex() == colorHex)
				return "CadetBlue";
			if (Color.Chartreuse.ToHex() == colorHex)
				return "Chartreuse";
			if (Color.Chocolate.ToHex() == colorHex)
				return "Chocolate";
			if (Color.Coral.ToHex() == colorHex)
				return "Coral";
			if (Color.CornflowerBlue.ToHex() == colorHex)
				return "CornflowerBlue";
			if (Color.Cornsilk.ToHex() == colorHex)
				return "Cornsilk";
			if (Color.Crimson.ToHex() == colorHex)
				return "Crimson";
			if (Color.Cyan.ToHex() == colorHex)
				return "Cyan";
			if (Color.DarkBlue.ToHex() == colorHex)
				return "DarkBlue";
			if (Color.DarkCyan.ToHex() == colorHex)
				return "DarkCyan";
			if (Color.DarkGoldenrod.ToHex() == colorHex)
				return "DarkGoldenrod";
			if (Color.DarkGray.ToHex() == colorHex)
				return "DarkGray";
			if (Color.DarkGreen.ToHex() == colorHex)
				return "DarkGreen";
			if (Color.DarkKhaki.ToHex() == colorHex)
				return "DarkKhaki";
			if (Color.DarkMagenta.ToHex() == colorHex)
				return "DarkMagenta";
			if (Color.DarkOliveGreen.ToHex() == colorHex)
				return "DarkOliveGreen";
			if (Color.DarkOrange.ToHex() == colorHex)
				return "DarkOrange";
			if (Color.DarkOrchid.ToHex() == colorHex)
				return "DarkOrchid";
			if (Color.DarkRed.ToHex() == colorHex)
				return "DarkRed";
			if (Color.DarkSalmon.ToHex() == colorHex)
				return "DarkSalmon";
			if (Color.DarkSeaGreen.ToHex() == colorHex)
				return "DarkSeaGreen";
			if (Color.DarkSlateBlue.ToHex() == colorHex)
				return "DarkSlateBlue";
			if (Color.DarkSlateGray.ToHex() == colorHex)
				return "DarkSlateGray";
			if (Color.DarkTurquoise.ToHex() == colorHex)
				return "DarkTurquoise";
			if (Color.DarkViolet.ToHex() == colorHex)
				return "DarkViolet";
			if (Color.DeepPink.ToHex() == colorHex)
				return "DeepPink";
			if (Color.DeepSkyBlue.ToHex() == colorHex)
				return "DeepSkyBlue";
			if (Color.DimGray.ToHex() == colorHex)
				return "DimGray";
			if (Color.DodgerBlue.ToHex() == colorHex)
				return "DodgerBlue";
			if (Color.Firebrick.ToHex() == colorHex)
				return "Firebrick";
			if (Color.FloralWhite.ToHex() == colorHex)
				return "FloralWhite";
			if (Color.ForestGreen.ToHex() == colorHex)
				return "ForestGreen";
			if (Color.Fuchsia.ToHex() == colorHex)
				return "Fuchsia";
			if (Color.Gainsboro.ToHex() == colorHex)
				return "Gainsboro";
			if (Color.GhostWhite.ToHex() == colorHex)
				return "GhostWhite";
			if (Color.Gold.ToHex() == colorHex)
				return "Gold";
			if (Color.Goldenrod.ToHex() == colorHex)
				return "Goldenrod";
			if (Color.Gray.ToHex() == colorHex)
				return "Gray";
			if (Color.Green.ToHex() == colorHex)
				return "Green";
			if (Color.GreenYellow.ToHex() == colorHex)
				return "GreenYellow";
			if (Color.Honeydew.ToHex() == colorHex)
				return "Honeydew";
			if (Color.HotPink.ToHex() == colorHex)
				return "HotPink";
			if (Color.IndianRed.ToHex() == colorHex)
				return "IndianRed";
			if (Color.Indigo.ToHex() == colorHex)
				return "Indigo";
			if (Color.Ivory.ToHex() == colorHex)
				return "Ivory";
			if (Color.Khaki.ToHex() == colorHex)
				return "Khaki";
			if (Color.Lavender.ToHex() == colorHex)
				return "Lavender";
			if (Color.LavenderBlush.ToHex() == colorHex)
				return "LavenderBlush";
			if (Color.LawnGreen.ToHex() == colorHex)
				return "LawnGreen";
			if (Color.LemonChiffon.ToHex() == colorHex)
				return "LemonChiffon";
			if (Color.LightBlue.ToHex() == colorHex)
				return "LightBlue";
			if (Color.LightCoral.ToHex() == colorHex)
				return "LightCoral";
			if (Color.LightCyan.ToHex() == colorHex)
				return "LightCyan";
			if (Color.LightGoldenrodYellow.ToHex() == colorHex)
				return "LightGoldenrodYellow";
			if (Color.LightGray.ToHex() == colorHex)
				return "LightGray";
			if (Color.LightGreen.ToHex() == colorHex)
				return "LightGreen";
			if (Color.LightPink.ToHex() == colorHex)
				return "LightPink";
			if (Color.LightSalmon.ToHex() == colorHex)
				return "LightSalmon";
			if (Color.LightSeaGreen.ToHex() == colorHex)
				return "LightSeaGreen";
			if (Color.LightSkyBlue.ToHex() == colorHex)
				return "LightSkyBlue";
			if (Color.LightSlateGray.ToHex() == colorHex)
				return "LightSlateGray";
			if (Color.LightSteelBlue.ToHex() == colorHex)
				return "LightSteelBlue";
			if (Color.LightYellow.ToHex() == colorHex)
				return "LightYellow";
			if (Color.Lime.ToHex() == colorHex)
				return "Lime";
			if (Color.LimeGreen.ToHex() == colorHex)
				return "LimeGreen";
			if (Color.Linen.ToHex() == colorHex)
				return "Linen";
			if (Color.Magenta.ToHex() == colorHex)
				return "Magenta";
			if (Color.Maroon.ToHex() == colorHex)
				return "Maroon";
			if (Color.MediumAquamarine.ToHex() == colorHex)
				return "MediumAquamarine";
			if (Color.MediumBlue.ToHex() == colorHex)
				return "MediumBlue";
			if (Color.MediumOrchid.ToHex() == colorHex)
				return "MediumOrchid";
			if (Color.MediumPurple.ToHex() == colorHex)
				return "MediumPurple";
			if (Color.MediumSeaGreen.ToHex() == colorHex)
				return "MediumSeaGreen";
			if (Color.MediumSlateBlue.ToHex() == colorHex)
				return "MediumSlateBlue";
			if (Color.MediumSpringGreen.ToHex() == colorHex)
				return "MediumSpringGreen";
			if (Color.MediumTurquoise.ToHex() == colorHex)
				return "MediumTurquoise";
			if (Color.MediumVioletRed.ToHex() == colorHex)
				return "MediumVioletRed";
			if (Color.MidnightBlue.ToHex() == colorHex)
				return "MidnightBlue";
			if (Color.MintCream.ToHex() == colorHex)
				return "MintCream";
			if (Color.MistyRose.ToHex() == colorHex)
				return "MistyRose";
			if (Color.Moccasin.ToHex() == colorHex)
				return "Moccasin";
			if (Color.NavajoWhite.ToHex() == colorHex)
				return "NavajoWhite";
			if (Color.Navy.ToHex() == colorHex)
				return "Navy";
			if (Color.OldLace.ToHex() == colorHex)
				return "OldLace";
			if (Color.Olive.ToHex() == colorHex)
				return "Olive";
			if (Color.OliveDrab.ToHex() == colorHex)
				return "OliveDrab";
			if (Color.Orange.ToHex() == colorHex)
				return "Orange";
			if (Color.OrangeRed.ToHex() == colorHex)
				return "OrangeRed";
			if (Color.Orchid.ToHex() == colorHex)
				return "Orchid";
			if (Color.PaleGoldenrod.ToHex() == colorHex)
				return "PaleGoldenrod";
			if (Color.PaleGreen.ToHex() == colorHex)
				return "PaleGreen";
			if (Color.PaleTurquoise.ToHex() == colorHex)
				return "PaleTurquoise";
			if (Color.PaleVioletRed.ToHex() == colorHex)
				return "PaleVioletRed";
			if (Color.PapayaWhip.ToHex() == colorHex)
				return "PapayaWhip";
			if (Color.PeachPuff.ToHex() == colorHex)
				return "PeachPuff";
			if (Color.Peru.ToHex() == colorHex)
				return "Peru";
			if (Color.Pink.ToHex() == colorHex)
				return "Pink";
			if (Color.Plum.ToHex() == colorHex)
				return "Plum";
			if (Color.PowderBlue.ToHex() == colorHex)
				return "PowderBlue";
			if (Color.Purple.ToHex() == colorHex)
				return "Purple";
			if (Color.Red.ToHex() == colorHex)
				return "Red";
			if (Color.RosyBrown.ToHex() == colorHex)
				return "RosyBrown";
			if (Color.RoyalBlue.ToHex() == colorHex)
				return "RoyalBlue";
			if (Color.SaddleBrown.ToHex() == colorHex)
				return "SaddleBrown";
			if (Color.Salmon.ToHex() == colorHex)
				return "Salmon";
			if (Color.SandyBrown.ToHex() == colorHex)
				return "SandyBrown";
			if (Color.SeaGreen.ToHex() == colorHex)
				return "SeaGreen";
			if (Color.SeaShell.ToHex() == colorHex)
				return "SeaShell";
			if (Color.Sienna.ToHex() == colorHex)
				return "Sienna";
			if (Color.Silver.ToHex() == colorHex)
				return "Silver";
			if (Color.SkyBlue.ToHex() == colorHex)
				return "SkyBlue";
			if (Color.SlateBlue.ToHex() == colorHex)
				return "SlateBlue";
			if (Color.SlateGray.ToHex() == colorHex)
				return "SlateGray";
			if (Color.Snow.ToHex() == colorHex)
				return "Snow";
			if (Color.SpringGreen.ToHex() == colorHex)
				return "SpringGreen";
			if (Color.SteelBlue.ToHex() == colorHex)
				return "SteelBlue";
			if (Color.Tan.ToHex() == colorHex)
				return "Tan";
			if (Color.Teal.ToHex() == colorHex)
				return "Teal";
			if (Color.Thistle.ToHex() == colorHex)
				return "Thistle";
			if (Color.Tomato.ToHex() == colorHex)
				return "Tomato";
			if (Color.Transparent.ToHex() == colorHex)
				return "Transparent";
			if (Color.Turquoise.ToHex() == colorHex)
				return "Turquoise";
			if (Color.Violet.ToHex() == colorHex)
				return "Violet";
			if (Color.Wheat.ToHex() == colorHex)
				return "Wheat";
			if (Color.White.ToHex() == colorHex)
				return "White";
			if (Color.WhiteSmoke.ToHex() == colorHex)
				return "WhiteSmoke";
			if (Color.Yellow.ToHex() == colorHex)
				return "Yellow";
			if (Color.YellowGreen.ToHex() == colorHex)
				return "YellowGreen";

			return string.Empty;
		}
		#endregion

		private static System.Drawing.Color GetSystemDrawingColorFromHexString(string hexString)
		{
			if (!System.Text.RegularExpressions.Regex.IsMatch(hexString, @"[#]([0-9]|[a-f]|[A-F]){6}\b"))
				throw new ArgumentException();

			int a = int.Parse(hexString.Substring(1, 2), NumberStyles.HexNumber);
			int red = int.Parse(hexString.Substring(3, 2), NumberStyles.HexNumber);
			int green = int.Parse(hexString.Substring(5, 2), NumberStyles.HexNumber);
			int blue = int.Parse(hexString.Substring(7, 2), NumberStyles.HexNumber);
			return Color.FromRgba(red, green, blue, a);
		}
	}
}
