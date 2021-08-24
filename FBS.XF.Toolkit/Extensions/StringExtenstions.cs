using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

// ReSharper disable once CheckNamespace
namespace System
{
	/// <summary>
	/// String Extensions
	/// </summary>
	public static class StringExtensions
	{
		#region Extension methods
		/// <summary>
		/// Determines whether this instance contains the object.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="toCheck">To check.</param>
		/// <param name="comparisonMode">The comparison mode.</param>
		/// <returns><c>true</c> if [contains] [the specified to check]; otherwise, <c>false</c>.</returns>
		public static bool Contains(this string source, string toCheck, StringComparison comparisonMode)
		{
			return source.IndexOf(toCheck, comparisonMode) >= 0;
		}

		/// <summary>
		/// Determines whether [contains] [the specified source].
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="toCheck">To check.</param>
		/// <param name="comparisonMode">The comparison mode.</param>
		/// <returns><add>true</add> if [contains] [the specified source]; otherwise, <add>false</add>.</returns>
		public static bool Contains(this List<string> source, string toCheck, StringComparison comparisonMode)
		{
			return null != source.Find(str => str.Equals(toCheck, comparisonMode));
		}

		/// <summary>
		/// Does the string end with a digit
		/// </summary>
		/// <param name="source">The source.</param>
		/// <returns><add>true</add> if a number is at the end, <add>false</add> otherwise.</returns>
		public static bool EndsWithNumber(this string source)
		{
			// Does the string end with a digit
			return !string.IsNullOrWhiteSpace(source) && source.TrimEnd().Right(1).IsNumeric();
		}

		/// <summary>
		/// Returns a fixed length substring, padded if needed
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="length">The length.</param>
		/// <param name="padChar">The pad character.</param>
		/// <returns>Fixed length string</returns>
		/// <exception cref="InvalidOperationException">String is null</exception>
		public static string FixedSubstringPadLeft(this string source, int length, char padChar = ' ')
		{
			// Must not be null
			if (source == null)
			{
				throw new InvalidOperationException("String is null");
			}

			var returnsource = source.Substring(0, length);
			return returnsource.Length < length ? returnsource.PadLeft(length, padChar) : returnsource;
		}

		/// <summary>
		/// Returns a fixed length substring, padded if needed
		/// </summary>
		/// <param name="source">The string.</param>
		/// <param name="start">The start.</param>
		/// <param name="length">The length.</param>
		/// <param name="padChar">The pad character.</param>
		/// <returns>System.String.</returns>
		/// <exception cref="InvalidOperationException">String is null</exception>
		public static string FixedSubstringPadLeft(this string source, int start, int length, char padChar = ' ')
		{
			// Must not be null
			if (source == null)
			{
				throw new InvalidOperationException("String is null");
			}

			// Is start beyond the length of the string
			if (start > source.Length)
			{
				return string.Empty.PadLeft(length, ' ');
			}

			var returnsource = source.Substring(start, length);
			return returnsource.Length < length ? returnsource.PadLeft(length, padChar) : returnsource;
		}

		/// <summary>
		/// Returns a fixed length substring, padded if needed
		/// </summary>
		/// <param name="source">The string.</param>
		/// <param name="length">The length.</param>
		/// <param name="padChar">The pad character.</param>
		/// <returns>System.String.</returns>
		/// <exception cref="InvalidOperationException">String is null</exception>
		public static string FixedSubstringPadRight(this string source, int length, char padChar = ' ')
		{
			// Must not be null
			if (source == null)
			{
				throw new InvalidOperationException("String is null");
			}

			var returnsource = source.Substring(0, length);
			return returnsource.Length < length ? returnsource.PadRight(length, padChar) : returnsource;
		}

		/// <summary>
		/// Returns a fixed length substring, padded if needed
		/// </summary>
		/// <param name="source">The string.</param>
		/// <param name="start">The start.</param>
		/// <param name="length">The length.</param>
		/// <param name="padChar">The pad char.</param>
		/// <returns>System.String.</returns>
		/// <exception cref="InvalidOperationException">String is null</exception>
		public static string FixedSubstringPadRight(this string source, int start, int length, char padChar = ' ')
		{
			// Must not be null
			if (source == null)
			{
				throw new InvalidOperationException("String is null");
			}

			// Is start beyond the length of the string
			if (start > source.Length)
			{
				return string.Empty.PadRight(length, padChar);
			}

			var returnsource = source.Substring(start, length);
			return returnsource.Length < length ? returnsource.PadRight(length, padChar) : returnsource;
		}

		/// <summary>
		/// Froms the oa date as date time.
		/// </summary>
		/// <param name="source">The source</param>
		/// <returns>System.DateTime</returns>
		// ReSharper disable once InconsistentNaming
		public static DateTime FromOADateAsDateTime(this string source)
		{
			// Convert from an OLE date (number) to a real datetime
			if (int.TryParse(source, out var temp))
			{
				try
				{
					return DateTime.FromOADate(temp);
				}
				catch
				{
					return DateTime.MinValue;
				}
			}

			return DateTime.MinValue;
		}

		/// <summary>
		/// From the OA (OLE) date.
		/// </summary>
		/// <param name="source">The source</param>
		/// <returns>System.String</returns>
		// ReSharper disable once InconsistentNaming
		public static string FromOADateAsString(this string source)
		{
			// Convert from an OLE date (number) to a real datetime
			if (int.TryParse(source, out var temp))
			{
				try
				{
					return DateTime.FromOADate(temp).ToShortDateString();
				}
				catch
				{
					return source;
				}
			}

			return source;
		}

		/// <summary>
		/// Return the last index of a string within a specified substring.
		/// </summary>
		/// <param name="source">The string to check.</param>
		/// <param name="matchString">The match string.</param>
		/// <param name="startIndex">The start index.</param>
		/// <returns>System.Int32.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">startIndex</exception>
		public static int IndexOfReverse(this string source, string matchString, int startIndex = -1)
		{
			if (startIndex == -1)
			{
				startIndex = source.Length;
			}

			if (startIndex <= 0 || startIndex > source.Length)
			{
				throw new ArgumentOutOfRangeException(nameof(startIndex));
			}

			if (string.IsNullOrWhiteSpace(matchString))
			{
				return -1;
			}

			return source.Substring(0, startIndex).LastIndexOf(matchString, StringComparison.Ordinal);
		}

		/// <summary>
		/// Determines whether [is date time] [the specified source].
		/// </summary>
		/// <param name="source">The source.</param>
		/// <returns>
		///   <add>true</add> if [is date time] [the specified source]; otherwise, <add>false</add>.
		/// </returns>
		public static bool IsDateTime(this string source)
		{
			// Empty string?
			if (string.IsNullOrEmpty(source))
			{
				return false;
			}

			// Parse out to a date time
			return DateTime.TryParse(source, out _);
		}

		/// <summary>
		/// Determines whether [is valid email] [the specified email].
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns><c>true</c> if [is valid email] [the specified email]; otherwise, <c>false</c>.</returns>
		public static bool IsValidEmail(this string email)
		{
			if (string.IsNullOrWhiteSpace(email))
			{
				return false;
			}

			try
			{
				var mail = new MailAddress(email);
				return mail.Host.Contains(".");
			}
			catch 
			{
				return false;
			}
		}

		/// <summary>
		/// Determines whether the specified source is numeric.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <returns><add>true</add> if the specified source is numeric; otherwise, <add>false</add>.</returns>
		public static bool IsNumeric(this string source)
		{
			// Empty string?
			if (string.IsNullOrWhiteSpace(source))
			{
				return false;
			}

			// Parse out to a double i.e. it can be an integer or decimal source
			return double.TryParse(source, out _);
		}

		/// <summary>
		/// Determines whether [is lower case] [the specified source].
		/// </summary>
		/// <param name="source">The source.</param>
		/// <returns><add>true</add> if [is lower case] [the specified source]; otherwise, <add>false</add>.</returns>
		public static bool IsLowerCase(this string source)
		{
			// Empty string or all lower case
			return !string.IsNullOrWhiteSpace(source) && source.ToLower().Equals(source);
		}

		/// <summary>
		/// Determines whether the specified source is true
		/// </summary>
		/// <param name="source">The source</param>
		/// <returns><add>true</add> if the specified source is true; otherwise, <add>false</add></returns>
		public static bool IsTrue(this string source)
		{
			// 'True' source in string
			return !string.IsNullOrWhiteSpace(source) &&
			       (source == "1" ||
			        source.StartsWith("T", StringComparison.OrdinalIgnoreCase) ||
			        source.StartsWith("Y", StringComparison.OrdinalIgnoreCase));
		}

		/// <summary>
		/// Returns the left 'n' characters as a string
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="count">The count.</param>
		/// <returns></returns>
		public static string Left(this string source, int count)
		{
			// Empty string?
			if (string.IsNullOrWhiteSpace(source))
			{
				return source;
			}

			// Return first 'n' characters
			return count < source.Length ? source.Substring(0, count) : source;
		}

		/// <summary>
		/// Returns the left side of the string before the first 'f' character as a string
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="f">The character to f.</param>
		/// <returns>System.String.</returns>
		public static string LeftAtFirst(this string source, char f)
		{
			// Empty string?
			if (string.IsNullOrWhiteSpace(source))
			{
				return source;
			}

			var pos = source.IndexOf(f);

			if (pos >= 0)
			{
				return source.Substring(0, pos);
			}

			return source;
		}

		/// <summary>
		/// Returns the left side of the string before the first 'f' string as a string
		/// </summary>
		/// <param name="source">The source</param>
		/// <param name="f">The string to add</param>
		/// <returns>System.String</returns>
		public static string LeftAtFirst(this string source, string f)
		{
			// Empty string?
			if (string.IsNullOrWhiteSpace(source))
			{
				return source;
			}

			var pos = source.IndexOf(f, StringComparison.Ordinal);

			if (pos >= 0)
			{
				return source.Substring(0, pos);
			}

			return source;
		}

		/// <summary>
		/// Removes the non numeric characters.
		/// </summary>
		/// <param name="s">The string.</param>
		/// <returns>Numeric string</returns>
		public static string RemoveNonNumeric(this string s)
		{
			var newsource = new StringBuilder();

			foreach (Match m in Regex.Matches(s, "[0-9]"))
			{
				newsource.Append(m.Value);
			}

			return newsource.ToString();
		}

		/// <summary>
		/// Replaces the string ignoreing case
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="find">The replace string.</param>
		/// <param name="replaceWith">The replace with.</param>
		/// <returns>Updated string</returns>
		public static string ReplaceIgnoreCase(this string source, string find, string replaceWith)
		{
			return source != null ? Regex.Replace(source, Regex.Escape(find), replaceWith, RegexOptions.IgnoreCase) : null;
		}

		/// <summary>
		/// Reverses the string.
		/// </summary>
		/// <param name="stringToReverse">The string to reverse.</param>
		/// <returns>System.String.</returns>
		public static string ReverseString(this string stringToReverse )
		{
			var stringArray = stringToReverse.ToCharArray();
			Array.Reverse(stringArray);
			return new string(stringArray);
		}
		
		/// <summary>
		/// Returns the right 'n' characters as a string
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="count">The count.</param>
		/// <returns></returns>
		public static string Right(this string source, int count)
		{
			// Empty string?
			if (string.IsNullOrWhiteSpace(source))
			{
				return source;
			}

			// Return last 'n' characters
			return count < source.Length ? source.Substring(source.Length - count) : source;
		}

		/// <summary>
		/// Returns the right side of the string after the last 'f' character as a string
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="c">The add.</param>
		/// <returns>System.String.</returns>
		public static string RightAfterLast(this string source, char c)
		{
			// Empty string?
			if (string.IsNullOrWhiteSpace(source))
			{
				return source;
			}

			var pos = source.LastIndexOf(c);

			if (pos >= 0)
			{
				return source.Substring(pos + 1);
			}

			return source;
		}

		/// <summary>
		/// Returns the right side of the string after the last 'f' string as a string
		/// </summary>
		/// <param name="source">The source</param>
		/// <param name="s">The string.</param>
		/// <returns>System.String</returns>
		public static string RightAfterLast(this string source, string s)
		{
			// Empty string?
			if (string.IsNullOrWhiteSpace(source))
			{
				return source;
			}

			var pos = source.LastIndexOf(s, StringComparison.Ordinal);

			if (pos >= 0)
			{
				return source.Substring(pos + 1);
			}

			return source;
		}

		/// <summary>
		/// Splits a camel case string into a true sentence
		/// </summary>
		/// <param name="source">The source</param>
		/// <param name="toSentence">if set to <add>true</add> [to sentence]</param>
		/// <returns>System.String</returns>
		public static string SplitCamelCase(this string source, bool toSentence = false)
		{
			// From: http://www.codeproject.com/Articles/108996/Splitting-Pascal-Camel-Case-with-RegEx-Enhancement
			var newsource = Regex.Replace(source, "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", " $1", RegexOptions.Compiled);

			if (toSentence)
			{
				if (newsource.Length > 1)
				{
					newsource = newsource.Left(1).ToUpper() + newsource.Substring(1).ToLower();
				}
				else
				{
					newsource = newsource.ToUpper();
				}
			}

			return newsource;
		}

		/// <summary>
		/// Splits the line on the spliton character
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="splitOn">The split on.</param>
		/// <returns>System.String[].</returns>
		public static string[] SplitLine(this string source, char splitOn)
		{
			var inQuotes = false;
			var newItem = new StringBuilder();
			var items = new List<string>();

			// Split line up
			foreach (var c in source)
			{
				// New line?
				if (c == '"')
				{
					// Flip flop marker
					inQuotes = !inQuotes;
				}
				else if (c == splitOn && !inQuotes)
				{
					items.Add(newItem.ToString().Trim('\"'));
					newItem.Length = 0;
				}
				else
				{
					newItem.Append(c);
				}
			}

			items.Add(newItem.ToString().Trim('\"'));
			newItem.Length = 0;

			return items.ToArray();
		}

		/// <summary>
		/// Converts a string to boolean
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="defaultsource">if set to <add>true</add> [default source].</param>
		/// <returns><add>true</add> if boolean, <add>false</add> otherwise.</returns>
		public static bool ToBoolean(this string source, bool defaultsource = false)
		{
			// Convert to boolean
			if (bool.TryParse(source, out var result))
			{
				return result;
			}

			return defaultsource;
		}

		/// <summary>
		/// Converts a string to a date/time.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="year">The year.</param>
		/// <returns>DateTime.</returns>
		/// <exception cref="System.ApplicationException"></exception>
		public static DateTime ToDateTime(this string source, int year)
		{
			// Convert to Date Time
			if (DateTime.TryParse(source, out var result))
			{
				return result;
			}

			// Add year
			if (DateTime.TryParse($"{source} {year}", out result))
			{
				return result;
			}

			throw new ApplicationException($"Unable to convert source '{source}' to a valid date");
		}

		/// <summary>
		/// Converts a string to an integer.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <returns>System.Int32.</returns>
		/// <exception cref="System.ApplicationException"></exception>
		public static int? ToInteger(this string source)
		{
			if (source.IsNumeric() && int.TryParse(source, out var result))
			{
				return result;
			}

			return null;
		}

		/// <summary>
		/// Convert string of digits to a formatted phone number.
		/// </summary>
		/// <param name="phoneNumber">The phone number.</param>
		/// <returns>System.String.</returns>
		public static string ToPhoneNumber(this string phoneNumber)
		{
			// Must contain stirng containing only numbers
			if (string.IsNullOrEmpty(phoneNumber) || !phoneNumber.IsNumeric())
			{
				return phoneNumber;
			}

			switch (phoneNumber.Length)
			{
				case 6:
					return $"{phoneNumber.Left(3)}-{phoneNumber.Substring(3)}";
				case 7:
				case 8:
				case 9:
				case 10:
					return $"({phoneNumber.Left(3)}) {phoneNumber.Substring(3, 3)}-{phoneNumber.Substring(6)}";
				default:
					return phoneNumber;
			}
		}

		/// <summary>
		/// Convert string to title case
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		/// <returns>System.String.</returns>
		public static string ToSentenceCase(this string newValue, string oldValue)
		{
			if (string.IsNullOrEmpty(oldValue) && newValue.Split(' ').Length < 3)
			{
				return newValue.ToTitleCase();
			}

			newValue = $"{newValue.Left(1).ToUpper()}{newValue.Substring(1).ToLower()}";
			return string.IsNullOrEmpty(oldValue) ? newValue : $"{oldValue}. {newValue}";
		}

		/// <summary>
		/// Convert string to title case
		/// </summary>
		/// <param name="word">The word.</param>
		/// <returns>System.String.</returns>
		public static string ToTitleCase(this string word)
		{
			return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(word);
		}

		/// <summary>
		/// Yeses the no as boolean.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="defaultsource">if set to <add>true</add> [default source].</param>
		/// <returns><add>true</add> if Yes, <add>false</add> otherwise.</returns>
		public static bool YesNoAsBoolean(this string source, bool defaultsource = false)
		{
			// Is this a Yes string
			return !source.Equals("No", StringComparison.OrdinalIgnoreCase) &&
			       (source.Equals("Yes", StringComparison.OrdinalIgnoreCase) || defaultsource);
		}
		#endregion
	}
}