using System;

namespace I2.Loc
{
	public class RTLFixer
	{
		public static string Fix(string str)
		{
			return Fix(str, showTashkeel: false, useHinduNumbers: true);
		}

		public static string Fix(string str, bool rtl)
		{
			if (rtl)
			{
				return Fix(str);
			}
			string[] array = str.Split(' ');
			string text = "";
			string text2 = "";
			string[] array2 = array;
			foreach (string text3 in array2)
			{
				if (char.IsLower(text3.ToLower()[text3.Length / 2]))
				{
					text = text + Fix(text2) + text3 + " ";
					text2 = "";
				}
				else
				{
					text2 = text2 + text3 + " ";
				}
			}
			if (text2 != "")
			{
				text += Fix(text2);
			}
			return text;
		}

		public static string Fix(string str, bool showTashkeel, bool useHinduNumbers)
		{
			string text = HindiFixer.Fix(str);
			if (text != str)
			{
				return text;
			}
			RTLFixerTool.showTashkeel = showTashkeel;
			RTLFixerTool.useHinduNumbers = useHinduNumbers;
			if (str.Contains("\n"))
			{
				str = str.Replace("\n", Environment.NewLine);
			}
			if (str.Contains(Environment.NewLine))
			{
				string[] separator = new string[1] { Environment.NewLine };
				string[] array = str.Split(separator, StringSplitOptions.None);
				if (array.Length == 0)
				{
					return RTLFixerTool.FixLine(str);
				}
				if (array.Length == 1)
				{
					return RTLFixerTool.FixLine(str);
				}
				string text2 = RTLFixerTool.FixLine(array[0]);
				int i = 1;
				if (array.Length > 1)
				{
					for (; i < array.Length; i++)
					{
						text2 = text2 + Environment.NewLine + RTLFixerTool.FixLine(array[i]);
					}
				}
				return text2;
			}
			return RTLFixerTool.FixLine(str);
		}
	}
}
