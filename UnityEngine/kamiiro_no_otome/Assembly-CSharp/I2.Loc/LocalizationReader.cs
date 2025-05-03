using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace I2.Loc
{
	public class LocalizationReader
	{
		public static Dictionary<string, string> ReadTextAsset(TextAsset asset)
		{
			StringReader stringReader = new StringReader(Encoding.UTF8.GetString(asset.bytes, 0, asset.bytes.Length).Replace("\r\n", "\n").Replace("\r", "\n"));
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
			string line;
			while ((line = stringReader.ReadLine()) != null)
			{
				if (TextAsset_ReadLine(line, out var key, out var value, out var _, out var _, out var _) && !string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
				{
					dictionary[key] = value;
				}
			}
			return dictionary;
		}

		public static bool TextAsset_ReadLine(string line, out string key, out string value, out string category, out string comment, out string termType)
		{
			key = string.Empty;
			category = string.Empty;
			comment = string.Empty;
			termType = string.Empty;
			value = string.Empty;
			int num = line.LastIndexOf("//");
			if (num >= 0)
			{
				comment = line.Substring(num + 2).Trim();
				comment = DecodeString(comment);
				line = line.Substring(0, num);
			}
			int num2 = line.IndexOf("=");
			if (num2 < 0)
			{
				return false;
			}
			key = line.Substring(0, num2).Trim();
			value = line.Substring(num2 + 1).Trim();
			value = value.Replace("\r\n", "\n").Replace("\n", "\\n");
			value = DecodeString(value);
			if (key.Length > 2 && key[0] == '[')
			{
				int num3 = key.IndexOf(']');
				if (num3 >= 0)
				{
					termType = key.Substring(1, num3 - 1);
					key = key.Substring(num3 + 1);
				}
			}
			ValidateFullTerm(ref key);
			return true;
		}

		public static string ReadCSVfile(string Path, Encoding encoding)
		{
			string text = string.Empty;
			using (StreamReader streamReader = new StreamReader(Path, encoding))
			{
				text = streamReader.ReadToEnd();
			}
			text = text.Replace("\r\n", "\n");
			return text.Replace("\r", "\n");
		}

		public static List<string[]> ReadCSV(string Text, char Separator = ',')
		{
			int iStart = 0;
			List<string[]> list = new List<string[]>();
			while (iStart < Text.Length)
			{
				string[] array = ParseCSVline(Text, ref iStart, Separator);
				if (array == null)
				{
					break;
				}
				list.Add(array);
			}
			return list;
		}

		private static string[] ParseCSVline(string Line, ref int iStart, char Separator)
		{
			List<string> list = new List<string>();
			int length = Line.Length;
			int iWordStart = iStart;
			bool flag = false;
			while (iStart < length)
			{
				char c = Line[iStart];
				if (flag)
				{
					if (c == '"')
					{
						if (iStart + 1 >= length || Line[iStart + 1] != '"')
						{
							flag = false;
						}
						else if (iStart + 2 < length && Line[iStart + 2] == '"')
						{
							flag = false;
							iStart += 2;
						}
						else
						{
							iStart++;
						}
					}
				}
				else if (c == '\n' || c == Separator)
				{
					AddCSVtoken(ref list, ref Line, iStart, ref iWordStart);
					if (c == '\n')
					{
						iStart++;
						break;
					}
				}
				else if (c == '"')
				{
					flag = true;
				}
				iStart++;
			}
			if (iStart > iWordStart)
			{
				AddCSVtoken(ref list, ref Line, iStart, ref iWordStart);
			}
			return list.ToArray();
		}

		private static void AddCSVtoken(ref List<string> list, ref string Line, int iEnd, ref int iWordStart)
		{
			string text = Line.Substring(iWordStart, iEnd - iWordStart);
			iWordStart = iEnd + 1;
			text = text.Replace("\"\"", "\"");
			if (text.Length > 1 && text[0] == '"' && text[text.Length - 1] == '"')
			{
				text = text.Substring(1, text.Length - 2);
			}
			list.Add(text);
		}

		public static List<string[]> ReadI2CSV(string Text)
		{
			string[] separator = new string[1] { "[*]" };
			string[] separator2 = new string[1] { "[ln]" };
			List<string[]> list = new List<string[]>();
			string[] array = Text.Split(separator2, StringSplitOptions.None);
			foreach (string text in array)
			{
				list.Add(text.Split(separator, StringSplitOptions.None));
			}
			return list;
		}

		public static void ValidateFullTerm(ref string Term)
		{
			Term = Term.Replace('\\', '/');
			int num = Term.IndexOf('/');
			if (num >= 0)
			{
				int startIndex;
				while ((startIndex = Term.LastIndexOf('/')) != num)
				{
					Term = Term.Remove(startIndex, 1);
				}
			}
		}

		public static string EncodeString(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return string.Empty;
			}
			return str.Replace("\r\n", "<\\n>").Replace("\r", "<\\n>").Replace("\n", "<\\n>");
		}

		public static string DecodeString(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return string.Empty;
			}
			return str.Replace("<\\n>", "\r\n");
		}
	}
}
