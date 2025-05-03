using System;
using System.Text;

namespace I2.Loc
{
	public class StringObfucator
	{
		public static char[] StringObfuscatorPassword = "ÝúbUu\u00b8CÁÂ§*4PÚ©-á©¾@T6Dl±ÒWâuzÅm4GÐóØ$=Íg,¥Që®iKEßr¡×60Ít4öÃ~^«y:Èd1<QÛÝúbUu\u00b8CÁÂ§*4PÚ©-á©¾@T6Dl±ÒWâuzÅm4GÐóØ$=Íg,¥Që®iKEßr¡×60Ít4öÃ~^«y:Èd".ToCharArray();

		public static string Encode(string NormalString)
		{
			try
			{
				return ToBase64(XoREncode(NormalString));
			}
			catch (Exception)
			{
				return null;
			}
		}

		public static string Decode(string ObfucatedString)
		{
			try
			{
				return XoREncode(FromBase64(ObfucatedString));
			}
			catch (Exception)
			{
				return null;
			}
		}

		private static string ToBase64(string regularString)
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(regularString));
		}

		private static string FromBase64(string base64string)
		{
			byte[] array = Convert.FromBase64String(base64string);
			return Encoding.UTF8.GetString(array, 0, array.Length);
		}

		private static string XoREncode(string NormalString)
		{
			try
			{
				char[] stringObfuscatorPassword = StringObfuscatorPassword;
				char[] array = NormalString.ToCharArray();
				int num = stringObfuscatorPassword.Length;
				int i = 0;
				for (int num2 = array.Length; i < num2; i++)
				{
					array[i] = (char)(array[i] ^ stringObfuscatorPassword[i % num] ^ (byte)((i % 2 == 0) ? (i * 23) : (-i * 51)));
				}
				return new string(array);
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}
