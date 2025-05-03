using System;
using System.Collections.Generic;
using UnityEngine;

namespace I2.Loc
{
	public static class GoogleLanguages
	{
		public struct LanguageCodeDef
		{
			public string Code;

			public string GoogleCode;

			public bool HasJoinedWords;

			public int PluralRule;
		}

		public static Dictionary<string, LanguageCodeDef> mLanguageDef = new Dictionary<string, LanguageCodeDef>(StringComparer.Ordinal)
		{
			{
				"Abkhazian",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ab",
					GoogleCode = "-"
				}
			},
			{
				"Afar",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "aa",
					GoogleCode = "-"
				}
			},
			{
				"Afrikaans",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "af"
				}
			},
			{
				"Akan",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ak",
					GoogleCode = "-"
				}
			},
			{
				"Albanian",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "sq"
				}
			},
			{
				"Amharic",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "am"
				}
			},
			{
				"Arabic",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar"
				}
			},
			{
				"Arabic/Algeria",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-DZ",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Bahrain",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-BH",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Egypt",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-EG",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Iraq",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-IQ",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Jordan",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-JO",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Kuwait",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-KW",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Lebanon",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-LB",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Libya",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-LY",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Morocco",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-MA",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Oman",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-OM",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Qatar",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-QA",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Saudi Arabia",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-SA",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Syria",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-SY",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Tunisia",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-TN",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/U.A.E.",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-AE",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Yemen",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-YE",
					GoogleCode = "ar"
				}
			},
			{
				"Aragonese",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "an",
					GoogleCode = "-"
				}
			},
			{
				"Armenian",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "hy"
				}
			},
			{
				"Assamese",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "as",
					GoogleCode = "-"
				}
			},
			{
				"Avaric",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "av",
					GoogleCode = "-"
				}
			},
			{
				"Avestan",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ae",
					GoogleCode = "-"
				}
			},
			{
				"Aymara",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ay",
					GoogleCode = "-"
				}
			},
			{
				"Azerbaijani",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "az"
				}
			},
			{
				"Bambara",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "bm",
					GoogleCode = "-"
				}
			},
			{
				"Bashkir",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ba",
					GoogleCode = "-"
				}
			},
			{
				"Basque",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "eu"
				}
			},
			{
				"Basque/Spain",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "eu-ES",
					GoogleCode = "eu"
				}
			},
			{
				"Belarusian",
				new LanguageCodeDef
				{
					PluralRule = 6,
					Code = "be"
				}
			},
			{
				"Bengali",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "bn"
				}
			},
			{
				"Bihari",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "bh",
					GoogleCode = "-"
				}
			},
			{
				"Bislama",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "bi",
					GoogleCode = "-"
				}
			},
			{
				"Bosnian",
				new LanguageCodeDef
				{
					PluralRule = 6,
					Code = "bs"
				}
			},
			{
				"Breton",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "br",
					GoogleCode = "-"
				}
			},
			{
				"Bulgariaa",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "bg"
				}
			},
			{
				"Burmese",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "my"
				}
			},
			{
				"Catalan",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ca"
				}
			},
			{
				"Chamorro",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ch",
					GoogleCode = "-"
				}
			},
			{
				"Chechen",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ce",
					GoogleCode = "-"
				}
			},
			{
				"Chichewa",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ny"
				}
			},
			{
				"Chinese",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "zh",
					GoogleCode = "zh-CN",
					HasJoinedWords = true
				}
			},
			{
				"Chinese/Hong Kong",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "zh-HK",
					GoogleCode = "zh-TW",
					HasJoinedWords = true
				}
			},
			{
				"Chinese/Macau",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "zh-MO",
					GoogleCode = "zh-CN",
					HasJoinedWords = true
				}
			},
			{
				"Chinese/PRC",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "zh-CN",
					GoogleCode = "zh-CN",
					HasJoinedWords = true
				}
			},
			{
				"Chinese/Simplified",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "zh-CN",
					GoogleCode = "zh-CN",
					HasJoinedWords = true
				}
			},
			{
				"Chinese/Singapore",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "zh-SG",
					GoogleCode = "zh-CN",
					HasJoinedWords = true
				}
			},
			{
				"Chinese/Taiwan",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "zh-TW",
					GoogleCode = "zh-TW",
					HasJoinedWords = true
				}
			},
			{
				"Chinese/Traditional",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "zh-TW",
					GoogleCode = "zh-TW",
					HasJoinedWords = true
				}
			},
			{
				"Chuvash",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "cv",
					GoogleCode = "-"
				}
			},
			{
				"Cornish",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "kw",
					GoogleCode = "-"
				}
			},
			{
				"Corsican",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "co"
				}
			},
			{
				"Cree",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "cr",
					GoogleCode = "-"
				}
			},
			{
				"Croatian",
				new LanguageCodeDef
				{
					PluralRule = 6,
					Code = "hr"
				}
			},
			{
				"Croatian/Bosnia and Herzegovina",
				new LanguageCodeDef
				{
					PluralRule = 5,
					Code = "hr-BA",
					GoogleCode = "hr"
				}
			},
			{
				"Czech",
				new LanguageCodeDef
				{
					PluralRule = 7,
					Code = "cs"
				}
			},
			{
				"Danish",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "da"
				}
			},
			{
				"Divehi",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "dv",
					GoogleCode = "-"
				}
			},
			{
				"Dutch",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "nl"
				}
			},
			{
				"Dutch/Belgium",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "nl-BE",
					GoogleCode = "nl"
				}
			},
			{
				"Dutch/Netherlands",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "nl-NL",
					GoogleCode = "nl"
				}
			},
			{
				"Dzongkha",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "dz",
					GoogleCode = "-"
				}
			},
			{
				"English",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en"
				}
			},
			{
				"English/Australia",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-AU",
					GoogleCode = "en"
				}
			},
			{
				"English/Belize",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-BZ",
					GoogleCode = "en"
				}
			},
			{
				"English/Canada",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-CA",
					GoogleCode = "en"
				}
			},
			{
				"English/Caribbean",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-CB",
					GoogleCode = "en"
				}
			},
			{
				"English/Ireland",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-IE",
					GoogleCode = "en"
				}
			},
			{
				"English/Jamaica",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-JM",
					GoogleCode = "en"
				}
			},
			{
				"English/New Zealand",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-NZ",
					GoogleCode = "en"
				}
			},
			{
				"English/Republic of the Philippines",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-PH",
					GoogleCode = "en"
				}
			},
			{
				"English/South Africa",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-ZA",
					GoogleCode = "en"
				}
			},
			{
				"English/Trinidad",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-TT",
					GoogleCode = "en"
				}
			},
			{
				"English/United Kingdom",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-GB",
					GoogleCode = "en"
				}
			},
			{
				"English/United States",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-US",
					GoogleCode = "en"
				}
			},
			{
				"English/Zimbabwe",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-ZW",
					GoogleCode = "en"
				}
			},
			{
				"Esperanto",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "eo"
				}
			},
			{
				"Estonian",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "et"
				}
			},
			{
				"Ewe",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ee",
					GoogleCode = "-"
				}
			},
			{
				"Faeroese",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "fo",
					GoogleCode = "-"
				}
			},
			{
				"Fijian",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "fj",
					GoogleCode = "-"
				}
			},
			{
				"Finnish",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "fi"
				}
			},
			{
				"French",
				new LanguageCodeDef
				{
					PluralRule = 2,
					Code = "fr"
				}
			},
			{
				"French/Belgium",
				new LanguageCodeDef
				{
					PluralRule = 2,
					Code = "fr-BE",
					GoogleCode = "fr"
				}
			},
			{
				"French/Canada",
				new LanguageCodeDef
				{
					PluralRule = 2,
					Code = "fr-CA",
					GoogleCode = "fr"
				}
			},
			{
				"French/France",
				new LanguageCodeDef
				{
					PluralRule = 2,
					Code = "fr-FR",
					GoogleCode = "fr"
				}
			},
			{
				"French/Luxembourg",
				new LanguageCodeDef
				{
					PluralRule = 2,
					Code = "fr-LU",
					GoogleCode = "fr"
				}
			},
			{
				"French/Principality of Monaco",
				new LanguageCodeDef
				{
					PluralRule = 2,
					Code = "fr-MC",
					GoogleCode = "fr"
				}
			},
			{
				"French/Switzerland",
				new LanguageCodeDef
				{
					PluralRule = 2,
					Code = "fr-CH",
					GoogleCode = "fr"
				}
			},
			{
				"Fulah",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ff",
					GoogleCode = "-"
				}
			},
			{
				"Galician",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "gl"
				}
			},
			{
				"Galician/Spain",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "gl-ES",
					GoogleCode = "gl"
				}
			},
			{
				"Georgian",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "ka"
				}
			},
			{
				"German",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "de"
				}
			},
			{
				"German/Austria",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "de-AT",
					GoogleCode = "de"
				}
			},
			{
				"German/Germany",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "de-DE",
					GoogleCode = "de"
				}
			},
			{
				"German/Liechtenstein",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "de-LI",
					GoogleCode = "de"
				}
			},
			{
				"German/Luxembourg",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "de-LU",
					GoogleCode = "de"
				}
			},
			{
				"German/Switzerland",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "de-CH",
					GoogleCode = "de"
				}
			},
			{
				"Greek",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "el"
				}
			},
			{
				"Guaraní",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "gn",
					GoogleCode = "-"
				}
			},
			{
				"Gujarati",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "gu"
				}
			},
			{
				"Haitian",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ht"
				}
			},
			{
				"Hausa",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ha"
				}
			},
			{
				"Hebrew",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "he",
					GoogleCode = "iw"
				}
			},
			{
				"Herero",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "hz",
					GoogleCode = "-"
				}
			},
			{
				"Hindi",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "hi"
				}
			},
			{
				"Hiri Motu",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ho",
					GoogleCode = "-"
				}
			},
			{
				"Hungarian",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "hu"
				}
			},
			{
				"Interlingua",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ia",
					GoogleCode = "-"
				}
			},
			{
				"Indonesian",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "id"
				}
			},
			{
				"Interlingue",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ie",
					GoogleCode = "-"
				}
			},
			{
				"Irish",
				new LanguageCodeDef
				{
					PluralRule = 10,
					Code = "ga"
				}
			},
			{
				"Igbo",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ig"
				}
			},
			{
				"Inupiaq",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ik",
					GoogleCode = "-"
				}
			},
			{
				"Ido",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "io",
					GoogleCode = "-"
				}
			},
			{
				"Icelandic",
				new LanguageCodeDef
				{
					PluralRule = 14,
					Code = "is"
				}
			},
			{
				"Italian",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "it"
				}
			},
			{
				"Italian/Italy",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "it-IT",
					GoogleCode = "it"
				}
			},
			{
				"Italian/Switzerland",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "it-CH",
					GoogleCode = "it"
				}
			},
			{
				"Inuktitut",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "iu",
					GoogleCode = "-"
				}
			},
			{
				"Japanese",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "ja",
					HasJoinedWords = true
				}
			},
			{
				"Javanese",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "jv"
				}
			},
			{
				"Kalaallisut",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "kl",
					GoogleCode = "-"
				}
			},
			{
				"Kannada",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "kn"
				}
			},
			{
				"Kanuri",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "kr",
					GoogleCode = "-"
				}
			},
			{
				"Kashmiri",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ks",
					GoogleCode = "-"
				}
			},
			{
				"Kazakh",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "kk"
				}
			},
			{
				"Central Khmer",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "km"
				}
			},
			{
				"Kikuyu",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ki",
					GoogleCode = "-"
				}
			},
			{
				"Kinyarwanda",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "rw",
					GoogleCode = "-"
				}
			},
			{
				"Kirghiz",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ky"
				}
			},
			{
				"Komi",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "kv",
					GoogleCode = "-"
				}
			},
			{
				"Kongo",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "kg",
					GoogleCode = "-"
				}
			},
			{
				"Korean",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "ko"
				}
			},
			{
				"Kurdish",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ku"
				}
			},
			{
				"Kuanyama",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "kj",
					GoogleCode = "-"
				}
			},
			{
				"Latin",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "la"
				}
			},
			{
				"Luxembourgish",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "lb"
				}
			},
			{
				"Ganda",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "lg",
					GoogleCode = "-"
				}
			},
			{
				"Limburgan",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "li",
					GoogleCode = "-"
				}
			},
			{
				"Lingala",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ln",
					GoogleCode = "-"
				}
			},
			{
				"Lao",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "lo"
				}
			},
			{
				"Latvian",
				new LanguageCodeDef
				{
					PluralRule = 5,
					Code = "lv"
				}
			},
			{
				"Luba-Katanga",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "lu",
					GoogleCode = "-"
				}
			},
			{
				"Lithuanian",
				new LanguageCodeDef
				{
					PluralRule = 5,
					Code = "lt"
				}
			},
			{
				"Manx",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "gv",
					GoogleCode = "-"
				}
			},
			{
				"Macedonian",
				new LanguageCodeDef
				{
					PluralRule = 13,
					Code = "mk"
				}
			},
			{
				"Malagasy",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "mg"
				}
			},
			{
				"Malay",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "ms"
				}
			},
			{
				"Malay/Brunei Darussalam",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "ms-BN",
					GoogleCode = "ms"
				}
			},
			{
				"Malay/Malaysia",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "ms-MY",
					GoogleCode = "ms"
				}
			},
			{
				"Malayalam",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ml"
				}
			},
			{
				"Maltese",
				new LanguageCodeDef
				{
					PluralRule = 12,
					Code = "mt"
				}
			},
			{
				"Maori",
				new LanguageCodeDef
				{
					PluralRule = 2,
					Code = "mi"
				}
			},
			{
				"Marathi",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "mr"
				}
			},
			{
				"Marshallese",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "mh",
					GoogleCode = "-"
				}
			},
			{
				"Mongolian",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "mn"
				}
			},
			{
				"Nauru",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "na",
					GoogleCode = "-"
				}
			},
			{
				"Navajo",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "nv",
					GoogleCode = "-"
				}
			},
			{
				"North Ndebele",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "nd",
					GoogleCode = "-"
				}
			},
			{
				"Nepali",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ne"
				}
			},
			{
				"Ndonga",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ng",
					GoogleCode = "-"
				}
			},
			{
				"Northern Sotho",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ns",
					GoogleCode = "st"
				}
			},
			{
				"Norwegian",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "nb",
					GoogleCode = "no"
				}
			},
			{
				"Norwegian/Nynorsk",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "nn",
					GoogleCode = "no"
				}
			},
			{
				"Sichuan Yi",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ii",
					GoogleCode = "-"
				}
			},
			{
				"South Ndebele",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "nr",
					GoogleCode = "-"
				}
			},
			{
				"Occitan",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "oc",
					GoogleCode = "-"
				}
			},
			{
				"Ojibwa",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "oj",
					GoogleCode = "-"
				}
			},
			{
				"Church\u00a0Slavic",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "cu",
					GoogleCode = "-"
				}
			},
			{
				"Oromo",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "om",
					GoogleCode = "-"
				}
			},
			{
				"Oriya",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "or",
					GoogleCode = "-"
				}
			},
			{
				"Ossetian",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "os",
					GoogleCode = "-"
				}
			},
			{
				"Pali",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "pi",
					GoogleCode = "-"
				}
			},
			{
				"Pashto",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ps"
				}
			},
			{
				"Persian",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "fa"
				}
			},
			{
				"Polish",
				new LanguageCodeDef
				{
					PluralRule = 8,
					Code = "pl"
				}
			},
			{
				"Portuguese",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "pt"
				}
			},
			{
				"Portuguese/Brazil",
				new LanguageCodeDef
				{
					PluralRule = 2,
					Code = "pt-BR",
					GoogleCode = "pt"
				}
			},
			{
				"Portuguese/Portugal",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "pt-PT",
					GoogleCode = "pt"
				}
			},
			{
				"Punjabi",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "pa"
				}
			},
			{
				"Quechua",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "qu",
					GoogleCode = "-"
				}
			},
			{
				"Quechua/Bolivia",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "qu-BO",
					GoogleCode = "-"
				}
			},
			{
				"Quechua/Ecuador",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "qu-EC",
					GoogleCode = "-"
				}
			},
			{
				"Quechua/Peru",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "qu-PE",
					GoogleCode = "-"
				}
			},
			{
				"Rhaeto-Romanic",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "rm",
					GoogleCode = "ro"
				}
			},
			{
				"Romanian",
				new LanguageCodeDef
				{
					PluralRule = 4,
					Code = "ro"
				}
			},
			{
				"Rundi",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "rn",
					GoogleCode = "-"
				}
			},
			{
				"Russian",
				new LanguageCodeDef
				{
					PluralRule = 6,
					Code = "ru"
				}
			},
			{
				"Russian/Republic of Moldova",
				new LanguageCodeDef
				{
					PluralRule = 6,
					Code = "ru-MO",
					GoogleCode = "ru"
				}
			},
			{
				"Sanskrit",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "sa",
					GoogleCode = "-"
				}
			},
			{
				"Sardinian",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "sc",
					GoogleCode = "-"
				}
			},
			{
				"Sindhi",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "sd"
				}
			},
			{
				"Northern Sami",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "se",
					GoogleCode = "-"
				}
			},
			{
				"Samoan",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "sm"
				}
			},
			{
				"Sango",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "sg",
					GoogleCode = "-"
				}
			},
			{
				"Serbian",
				new LanguageCodeDef
				{
					PluralRule = 6,
					Code = "sr"
				}
			},
			{
				"Serbian/Bosnia and Herzegovina",
				new LanguageCodeDef
				{
					PluralRule = 5,
					Code = "sr-BA",
					GoogleCode = "sr"
				}
			},
			{
				"Serbian/Serbia and Montenegro",
				new LanguageCodeDef
				{
					PluralRule = 5,
					Code = "sr-SP",
					GoogleCode = "sr"
				}
			},
			{
				"Scottish Gaelic",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "gd"
				}
			},
			{
				"Shona",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "sn"
				}
			},
			{
				"Sinhala",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "si"
				}
			},
			{
				"Slovak",
				new LanguageCodeDef
				{
					PluralRule = 7,
					Code = "sk"
				}
			},
			{
				"Slovenian",
				new LanguageCodeDef
				{
					PluralRule = 9,
					Code = "sl"
				}
			},
			{
				"Somali",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "so"
				}
			},
			{
				"Southern Sotho",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "st"
				}
			},
			{
				"Spanish",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es"
				}
			},
			{
				"Spanish/Argentina",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-AR",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Bolivia",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-BO",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Castilian",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-ES",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Chile",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-CL",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Colombia",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-CO",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Costa Rica",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-CR",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Dominican Republic",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-DO",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Ecuador",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-EC",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/El Salvador",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-SV",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Guatemala",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-GT",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Honduras",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-HN",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Mexico",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-MX",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Nicaragua",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-NI",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Panama",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-PA",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Paraguay",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-PY",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Peru",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-PE",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Puerto Rico",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-PR",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Spain",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-ES",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Uruguay",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-UY",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Venezuela",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-VE",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Latin Americas",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-US",
					GoogleCode = "es"
				}
			},
			{
				"Sundanese",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "su"
				}
			},
			{
				"Swahili",
				new LanguageCodeDef
				{
					Code = "sw"
				}
			},
			{
				"Swati",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ss",
					GoogleCode = "-"
				}
			},
			{
				"Swedish",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "sv"
				}
			},
			{
				"Swedish/Finland",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "sv-FI",
					GoogleCode = "sv"
				}
			},
			{
				"Swedish/Sweden",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "sv-SE",
					GoogleCode = "sv"
				}
			},
			{
				"Tamil",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ta"
				}
			},
			{
				"Tatar",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "tt",
					GoogleCode = "-"
				}
			},
			{
				"Telugu",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "te"
				}
			},
			{
				"Tajik",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "tg"
				}
			},
			{
				"Thai",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "th",
					HasJoinedWords = true
				}
			},
			{
				"Tigrinya",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ti",
					GoogleCode = "-"
				}
			},
			{
				"Tibetan",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "bo",
					GoogleCode = "-"
				}
			},
			{
				"Turkmen",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "tk",
					GoogleCode = "-"
				}
			},
			{
				"Tagalog",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "tl"
				}
			},
			{
				"Tswana",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "tn",
					GoogleCode = "-"
				}
			},
			{
				"Tonga",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "to",
					GoogleCode = "-"
				}
			},
			{
				"Turkish",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "tr"
				}
			},
			{
				"Tsonga",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ts",
					GoogleCode = "-"
				}
			},
			{
				"Twi",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "tw",
					GoogleCode = "-"
				}
			},
			{
				"Tahitian",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ty",
					GoogleCode = "-"
				}
			},
			{
				"Uighur",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ug",
					GoogleCode = "-"
				}
			},
			{
				"Ukrainian",
				new LanguageCodeDef
				{
					PluralRule = 6,
					Code = "uk"
				}
			},
			{
				"Urdu",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ur"
				}
			},
			{
				"Uzbek",
				new LanguageCodeDef
				{
					PluralRule = 2,
					Code = "uz"
				}
			},
			{
				"Venda",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ve",
					GoogleCode = "-"
				}
			},
			{
				"Vietnamese",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "vi"
				}
			},
			{
				"Volapük",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "vo",
					GoogleCode = "-"
				}
			},
			{
				"Walloon",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "wa",
					GoogleCode = "-"
				}
			},
			{
				"Welsh",
				new LanguageCodeDef
				{
					PluralRule = 16,
					Code = "cy"
				}
			},
			{
				"Wolof",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "wo",
					GoogleCode = "-"
				}
			},
			{
				"Frisian",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "fy"
				}
			},
			{
				"Xhosa",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "xh"
				}
			},
			{
				"Yiddish",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "yi"
				}
			},
			{
				"Yoruba",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "yo"
				}
			},
			{
				"Zhuang",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "za",
					GoogleCode = "-"
				}
			},
			{
				"Zulu",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "zu"
				}
			}
		};

		public static string GetLanguageCode(string Filter, bool ShowWarnings = false)
		{
			if (string.IsNullOrEmpty(Filter))
			{
				return string.Empty;
			}
			string[] filters = Filter.ToLowerInvariant().Split(" /(),".ToCharArray());
			foreach (KeyValuePair<string, LanguageCodeDef> item in mLanguageDef)
			{
				if (LanguageMatchesFilter(item.Key, filters))
				{
					return item.Value.Code;
				}
			}
			if (ShowWarnings)
			{
				Debug.Log($"Language '{Filter}' not recognized. Please, add the language code to GoogleTranslation.cs");
			}
			return string.Empty;
		}

		public static List<string> GetLanguagesForDropdown(string Filter, string CodesToExclude)
		{
			string[] filters = Filter.ToLowerInvariant().Split(" /(),".ToCharArray());
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, LanguageCodeDef> item in mLanguageDef)
			{
				if (string.IsNullOrEmpty(Filter) || LanguageMatchesFilter(item.Key, filters))
				{
					string text = string.Concat("[" + item.Value.Code + "]");
					if (!CodesToExclude.Contains(text))
					{
						list.Add(item.Key + " " + text);
					}
				}
			}
			for (int num = list.Count - 2; num >= 0; num--)
			{
				string text2 = list[num].Substring(0, list[num].IndexOf(" ["));
				if (list[num + 1].StartsWith(text2))
				{
					list[num] = text2 + "/" + list[num];
					list.Insert(num + 1, text2 + "/");
				}
			}
			return list;
		}

		private static bool LanguageMatchesFilter(string Language, string[] Filters)
		{
			Language = Language.ToLowerInvariant();
			int i = 0;
			for (int num = Filters.Length; i < num; i++)
			{
				if (Filters[i] != "")
				{
					if (!Language.Contains(Filters[i].ToLower()))
					{
						return false;
					}
					Language = Language.Remove(Language.IndexOf(Filters[i]), Filters[i].Length);
				}
			}
			return true;
		}

		public static string GetFormatedLanguageName(string Language)
		{
			string empty = string.Empty;
			int num = Language.IndexOf(" [");
			if (num > 0)
			{
				Language = Language.Substring(0, num);
			}
			num = Language.IndexOf('/');
			if (num > 0)
			{
				empty = Language.Substring(0, num);
				if (Language == empty + "/" + empty)
				{
					return empty;
				}
				Language = Language.Replace("/", " (") + ")";
			}
			return Language;
		}

		public static string GetCodedLanguage(string Language, string code)
		{
			string languageCode = GetLanguageCode(Language);
			if (string.Compare(code, languageCode, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return Language;
			}
			return Language + " [" + code + "]";
		}

		public static void UnPackCodeFromLanguageName(string CodedLanguage, out string Language, out string code)
		{
			if (string.IsNullOrEmpty(CodedLanguage))
			{
				Language = string.Empty;
				code = string.Empty;
				return;
			}
			int num = CodedLanguage.IndexOf("[");
			if (num < 0)
			{
				Language = CodedLanguage;
				code = GetLanguageCode(Language);
			}
			else
			{
				Language = CodedLanguage.Substring(0, num).Trim();
				code = CodedLanguage.Substring(num + 1, CodedLanguage.IndexOf("]", num) - num - 1);
			}
		}

		public static string GetGoogleLanguageCode(string InternationalCode)
		{
			foreach (KeyValuePair<string, LanguageCodeDef> item in mLanguageDef)
			{
				if (InternationalCode == item.Value.Code)
				{
					if (item.Value.GoogleCode == "-")
					{
						return null;
					}
					return (!string.IsNullOrEmpty(item.Value.GoogleCode)) ? item.Value.GoogleCode : InternationalCode;
				}
			}
			return InternationalCode;
		}

		public static string GetLanguageName(string code, bool useParenthesesForRegion = false, bool allowDiscardRegion = true)
		{
			foreach (KeyValuePair<string, LanguageCodeDef> item in mLanguageDef)
			{
				if (!(code == item.Value.Code))
				{
					continue;
				}
				string text = item.Key;
				if (useParenthesesForRegion)
				{
					int num = text.IndexOf('/');
					if (num > 0)
					{
						text = text.Substring(0, num) + " (" + text.Substring(num + 1) + ")";
					}
				}
				return text;
			}
			if (allowDiscardRegion)
			{
				int num2 = code.IndexOf("-");
				if (num2 > 0)
				{
					return GetLanguageName(code.Substring(0, num2), useParenthesesForRegion, allowDiscardRegion: false);
				}
			}
			return null;
		}

		public static List<string> GetAllInternationalCodes()
		{
			HashSet<string> hashSet = new HashSet<string>();
			foreach (KeyValuePair<string, LanguageCodeDef> item in mLanguageDef)
			{
				hashSet.Add(item.Value.Code);
			}
			return new List<string>(hashSet);
		}

		public static bool LanguageCode_HasJoinedWord(string languageCode)
		{
			foreach (KeyValuePair<string, LanguageCodeDef> item in mLanguageDef)
			{
				if (languageCode == item.Value.GoogleCode || languageCode == item.Value.Code)
				{
					return item.Value.HasJoinedWords;
				}
			}
			return false;
		}

		private static int GetPluralRule(string langCode)
		{
			if (langCode.Length > 2)
			{
				langCode = langCode.Substring(0, 2);
			}
			langCode = langCode.ToLower();
			foreach (KeyValuePair<string, LanguageCodeDef> item in mLanguageDef)
			{
				if (item.Value.Code == langCode)
				{
					return item.Value.PluralRule;
				}
			}
			return 0;
		}

		public static bool LanguageHasPluralType(string langCode, string pluralType)
		{
			switch (pluralType)
			{
			case "Plural":
			case "Zero":
			case "One":
				return true;
			default:
				switch (GetPluralRule(langCode))
				{
				case 3:
					if (!(pluralType == "Two"))
					{
						return pluralType == "Few";
					}
					return true;
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
					return pluralType == "Few";
				case 9:
					if (!(pluralType == "Two"))
					{
						return pluralType == "Few";
					}
					return true;
				case 10:
				case 11:
				case 15:
				case 16:
					if (!(pluralType == "Two") && !(pluralType == "Few"))
					{
						return pluralType == "Many";
					}
					return true;
				case 12:
					if (!(pluralType == "Few"))
					{
						return pluralType == "Many";
					}
					return true;
				case 13:
					return pluralType == "Two";
				default:
					return false;
				}
			}
		}

		public static ePluralType GetPluralType(string langCode, int n)
		{
			switch (n)
			{
			case 0:
				return ePluralType.Zero;
			case 1:
				return ePluralType.One;
			default:
				switch (GetPluralRule(langCode))
				{
				case 0:
					return ePluralType.Plural;
				case 1:
					if (n != 1)
					{
						return ePluralType.Plural;
					}
					return ePluralType.One;
				case 2:
					if (n > 1)
					{
						return ePluralType.Plural;
					}
					return ePluralType.One;
				case 3:
					switch (n)
					{
					default:
						if (!inRange(n, 3, 10) && !inRange(n, 13, 19))
						{
							return ePluralType.Plural;
						}
						return ePluralType.Few;
					case 2:
					case 12:
						return ePluralType.Two;
					case 1:
					case 11:
						return ePluralType.One;
					}
				case 4:
					if (n != 1)
					{
						if (!inRange(n % 100, 1, 19))
						{
							return ePluralType.Plural;
						}
						return ePluralType.Few;
					}
					return ePluralType.One;
				case 5:
					if (n % 10 != 1 || n % 100 == 11)
					{
						if (n % 10 < 2 || (n % 100 >= 10 && n % 100 < 20))
						{
							return ePluralType.Plural;
						}
						return ePluralType.Few;
					}
					return ePluralType.One;
				case 6:
					if (n % 10 != 1 || n % 100 == 11)
					{
						if (!inRange(n % 10, 2, 4) || inRange(n % 100, 12, 14))
						{
							return ePluralType.Plural;
						}
						return ePluralType.Few;
					}
					return ePluralType.One;
				case 7:
					if (n != 1)
					{
						if (!inRange(n, 2, 4))
						{
							return ePluralType.Plural;
						}
						return ePluralType.Few;
					}
					return ePluralType.One;
				case 8:
					if (n != 1)
					{
						if (!inRange(n % 10, 2, 4) || inRange(n % 100, 12, 14))
						{
							return ePluralType.Plural;
						}
						return ePluralType.Few;
					}
					return ePluralType.One;
				case 9:
					if (n % 100 != 1)
					{
						if (n % 100 != 2)
						{
							if (!inRange(n % 100, 3, 4))
							{
								return ePluralType.Plural;
							}
							return ePluralType.Few;
						}
						return ePluralType.Two;
					}
					return ePluralType.One;
				case 10:
					switch (n)
					{
					default:
						if (!inRange(n, 3, 6))
						{
							if (!inRange(n, 7, 10))
							{
								return ePluralType.Plural;
							}
							return ePluralType.Many;
						}
						return ePluralType.Few;
					case 2:
						return ePluralType.Two;
					case 1:
						return ePluralType.One;
					}
				case 11:
					switch (n)
					{
					default:
						if (!inRange(n % 100, 3, 10))
						{
							if (n % 100 < 11)
							{
								return ePluralType.Plural;
							}
							return ePluralType.Many;
						}
						return ePluralType.Few;
					case 2:
						return ePluralType.Two;
					case 1:
						return ePluralType.One;
					case 0:
						return ePluralType.Zero;
					}
				case 12:
					if (n != 1)
					{
						if (!inRange(n % 100, 1, 10))
						{
							if (!inRange(n % 100, 11, 19))
							{
								return ePluralType.Plural;
							}
							return ePluralType.Many;
						}
						return ePluralType.Few;
					}
					return ePluralType.One;
				case 13:
					if (n % 10 != 1)
					{
						if (n % 10 != 2)
						{
							return ePluralType.Plural;
						}
						return ePluralType.Two;
					}
					return ePluralType.One;
				case 14:
					if (n % 10 != 1 || n % 100 == 11)
					{
						return ePluralType.Plural;
					}
					return ePluralType.One;
				case 15:
					if (n % 10 != 1 || n % 100 == 11 || n % 100 == 71 || n % 100 == 91)
					{
						if (n % 10 != 2 || n % 100 == 12 || n % 100 == 72 || n % 100 == 92)
						{
							if ((n % 10 != 3 && n % 10 != 4 && n % 10 != 9) || n % 100 == 13 || n % 100 == 14 || n % 100 == 19 || n % 100 == 73 || n % 100 == 74 || n % 100 == 79 || n % 100 == 93 || n % 100 == 94 || n % 100 == 99)
							{
								if (n % 1000000 != 0)
								{
									return ePluralType.Plural;
								}
								return ePluralType.Many;
							}
							return ePluralType.Few;
						}
						return ePluralType.Two;
					}
					return ePluralType.One;
				case 16:
					switch (n)
					{
					default:
						return ePluralType.Plural;
					case 6:
						return ePluralType.Many;
					case 3:
						return ePluralType.Few;
					case 2:
						return ePluralType.Two;
					case 1:
						return ePluralType.One;
					case 0:
						return ePluralType.Zero;
					}
				default:
					return ePluralType.Plural;
				}
			}
		}

		public static int GetPluralTestNumber(string langCode, ePluralType pluralType)
		{
			switch (pluralType)
			{
			case ePluralType.Zero:
				return 0;
			case ePluralType.One:
				return 1;
			case ePluralType.Few:
				return 3;
			case ePluralType.Many:
				switch (GetPluralRule(langCode))
				{
				case 10:
					return 8;
				case 11:
				case 12:
					return 13;
				case 15:
					return 1000000;
				default:
					return 6;
				}
			default:
				return 936;
			}
		}

		private static bool inRange(int amount, int min, int max)
		{
			if (amount >= min)
			{
				return amount <= max;
			}
			return false;
		}
	}
}
