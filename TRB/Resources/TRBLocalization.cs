using System.Collections.Generic;

namespace TRB.Resources
{
	public enum Language
	{
		English,
		Tagalog,
		Japanese,
		Korean,
	}

	public enum MessageKey
	{
		InvalidRoman,
		OutOfRange,
		WelcomeMessage,
		InvalidCharacter,
	}

	public static class TRBLocalization
	{
		private static readonly Dictionary<Language, Dictionary<MessageKey, string>> Translations = new Dictionary<Language, Dictionary<MessageKey, string>>
														{
															{ Language.English, new Dictionary<MessageKey, string>
																{
																	{ MessageKey.InvalidRoman, "Invalid Roman numeral." },
																	{ MessageKey.InvalidCharacter, "Invalid character in Roman numeral." },
																	{ MessageKey.OutOfRange, "Number must be between 1 and 3999." },
																	{ MessageKey.WelcomeMessage, "Welcome to TRB Utils!" }
																}
															},
															{ Language.Tagalog, new Dictionary<MessageKey, string>
																{
																	{ MessageKey.InvalidRoman, "Di-wastong Romanong numero." },
																	{ MessageKey.InvalidCharacter, "Di-wastong karakter sa Romanong numero." },
																	{ MessageKey.OutOfRange, "Ang numero ay dapat nasa pagitan ng 1 at 3999." },
																	{ MessageKey.WelcomeMessage, "Maligayang pagdating sa TRB Utils!" }
																}
															},
															{ Language.Japanese, new Dictionary<MessageKey, string>
																{
																	{ MessageKey.InvalidRoman, "無効なローマ数字です。" },
																	{ MessageKey.InvalidCharacter, "ローマ数字に無効な文字が含まれています。" },
																	{ MessageKey.OutOfRange, "数値は1から3999の間である必要があります。" },
																	{ MessageKey.WelcomeMessage, "TRB Utilsへようこそ！" }
																}
															},
															{ Language.Korean, new Dictionary<MessageKey, string>
																{
																	{ MessageKey.InvalidRoman, "잘못된 로마 숫자입니다." },
																	{ MessageKey.InvalidCharacter, "로마 숫자에 잘못된 문자가 포함되어 있습니다." },
																	{ MessageKey.OutOfRange, "숫자는 1에서 3999 사이여야 합니다." },
																	{ MessageKey.WelcomeMessage, "TRB Utils에 오신 것을 환영합니다!" }
																}
															}
														};

		private static Language _currentLanguage = Language.English;

		/// <summary>
		/// Sets the current language.
		/// </summary>
		public static void SetLanguage(Language langCode)
		{
			_currentLanguage = langCode;
		}

		/// <summary>
		/// Gets the translated string for a given key.
		/// </summary>
		public static string Get(MessageKey key)
		{
			return Translations.TryGetValue(_currentLanguage, out var dict) &&
				dict.TryGetValue(key, out var value) ? value : key.ToString();
		}
	}
}
