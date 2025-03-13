using System;
using TRB.Resources;
using TRB.Utils;
using Xunit;
namespace TRB.Tests.UtilsTests
{
	public class RomanNumeralTests
	{
		public RomanNumeralTests()
		{
			TRBLocalization.SetLanguage(Language.English);
		}

		[Theory]
		[InlineData("I", 1)]
		[InlineData("IV", 4)]
		[InlineData("IX", 9)]
		[InlineData("XII", 12)]
		[InlineData("XX", 20)]
		[InlineData("XL", 40)]
		[InlineData("L", 50)]
		[InlineData("XC", 90)]
		[InlineData("C", 100)]
		[InlineData("CD", 400)]
		[InlineData("D", 500)]
		[InlineData("CM", 900)]
		[InlineData("M", 1000)]
		[InlineData("MCMXCIV", 1994)]
		public void ToInt_ShouldReturnCorrectValue(string roman, int expected)
		{
			int result = RomanNumeral.ToInt(roman);
			Assert.Equal(expected, result);
		}

		[Fact]
		public void ToInt_ShouldThrowException_ForInvalidInput()
		{
			ArgumentException ex = Assert.Throws<ArgumentException>(() => RomanNumeral.ToInt("INVALID"));
			Assert.Equal(TRBLocalization.Get(MessageKey.InvalidRoman), ex.Message);

			ArgumentException ex2 = Assert.Throws<ArgumentException>(() => RomanNumeral.ToInt("VV"));
			Assert.Equal(TRBLocalization.Get(MessageKey.InvalidRoman), ex.Message);

		}

		[Theory]
		[InlineData(1, "I")]
		[InlineData(4, "IV")]
		[InlineData(9, "IX")]
		[InlineData(12, "XII")]
		[InlineData(20, "XX")]
		[InlineData(40, "XL")]
		[InlineData(50, "L")]
		[InlineData(90, "XC")]
		[InlineData(100, "C")]
		[InlineData(400, "CD")]
		[InlineData(500, "D")]
		[InlineData(900, "CM")]
		[InlineData(1000, "M")]
		[InlineData(1994, "MCMXCIV")]
		public void ToRoman_ShouldReturnCorrectValue(int number, string expected)
		{
			string result = RomanNumeral.ToRoman(number);
			Assert.Equal(expected, result);
		}

		[Fact]
		public void ToRoman_ShouldThrowException_ForOutOfRange()
		{
			ArgumentException ex = Assert.Throws<ArgumentException>(() => RomanNumeral.ToRoman(4000));
			Assert.Contains(TRBLocalization.Get(MessageKey.OutOfRange), ex.Message);
		}
	}
}
