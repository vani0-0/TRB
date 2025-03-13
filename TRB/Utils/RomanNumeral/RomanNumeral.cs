using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TRB.Resources;

namespace TRB.Utils
{
	public class RomanNumeral
	{
		private static readonly Dictionary<char, int> RomanToIntegerMap = new Dictionary<char, int>()
		{
			{'I', 1}, {'V', 5}, {'X', 10}, {'L', 50},
			{'C', 100}, {'D', 500}, {'M', 1000}
		};
		private static readonly (int Value, string Symbol)[] IntegerToRomanMap =
		{
			(1000, "M"), (900, "CM"), (500, "D"), (400, "CD"),
			(100, "C"), (90, "XC"), (50, "L"), (40, "XL"),
			(10, "X"), (9, "IX"), (5, "V"), (4, "IV"), (1, "I")
		};
		/// <summary>
		/// Converts Integer to a Roman Numeral
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>
		public static string ToRoman(int value)
		{
			if (value < 1 || value > 3999)
			{
				throw new ArgumentException(TRBLocalization.Get(MessageKey.OutOfRange), nameof(value));
			}

			StringBuilder result = new StringBuilder();

			foreach ((int number, string symbol) in IntegerToRomanMap)
			{
				while (value >= number)
				{
					result.Append(symbol);
					value -= number;
				}
			}

			return result.ToString();
		}


		public static int ToInt(string value)
		{
			value = value.Trim().ToUpper();
			if (string.IsNullOrWhiteSpace(value) || !IsValid(value))
			{
				throw new ArgumentException(TRBLocalization.Get(MessageKey.InvalidRoman));
			}

			int result = 0;
			int prevValue = 0;

			foreach (char c in value)
			{
				if (!RomanToIntegerMap.TryGetValue(c, out int number))
				{
					throw new ArgumentException(TRBLocalization.Get(MessageKey.InvalidCharacter));
				}

				result += number;
				if (number > prevValue)
				{
					result -= 2 * prevValue;
				}
				prevValue = number;
			}
			return result;
		}

		/// <summary>
		/// Validates if a string is a correct Roman Numeral
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool IsValid(string value)
		{
			if (string.IsNullOrWhiteSpace(value)) return false;
			bool match = Regex.IsMatch(value, @"^M{0,4}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})$");
			return match;
		}

		/// <summary>
		/// Extracts all valid Roman Numerals from a sentence.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static List<string> ExtractRomanNumerals(string value)
		{
			MatchCollection matches = Regex.Matches(value.ToUpper(), @"\bM{0,4}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})\b");
			List<string> extracted = new List<string>();

			foreach (Match match in matches)
			{
				if (IsValid(match.Value))
				{
					extracted.Add(match.Value);
				}
			}
			return extracted;
		}
	}
}
