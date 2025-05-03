using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace I2.Loc
{
	public abstract class I2BasePersistentStorage
	{
		public virtual void SetSetting_String(string key, string value)
		{
			try
			{
				int length = value.Length;
				int num = 8000;
				if (length <= num)
				{
					PlayerPrefs.SetString(key, value);
					return;
				}
				int num2 = Mathf.CeilToInt((float)length / (float)num);
				for (int i = 0; i < num2; i++)
				{
					int num3 = num * i;
					PlayerPrefs.SetString($"[I2split]{i}{key}", value.Substring(num3, Mathf.Min(num, length - num3)));
				}
				PlayerPrefs.SetString(key, "[$I2#@div$]" + num2);
			}
			catch (Exception)
			{
				Debug.LogError("Error saving PlayerPrefs " + key);
			}
		}

		public virtual string GetSetting_String(string key, string defaultValue)
		{
			try
			{
				string text = PlayerPrefs.GetString(key, defaultValue);
				if (!string.IsNullOrEmpty(text) && text.StartsWith("[I2split]"))
				{
					int num = int.Parse(text.Substring("[I2split]".Length));
					text = "";
					for (int i = 0; i < num; i++)
					{
						text += PlayerPrefs.GetString($"[I2split]{i}{key}", "");
					}
				}
				return text;
			}
			catch (Exception)
			{
				Debug.LogError("Error loading PlayerPrefs " + key);
				return defaultValue;
			}
		}

		public virtual void DeleteSetting(string key)
		{
			try
			{
				string @string = PlayerPrefs.GetString(key, null);
				if (!string.IsNullOrEmpty(@string) && @string.StartsWith("[I2split]"))
				{
					int num = int.Parse(@string.Substring("[I2split]".Length));
					for (int i = 0; i < num; i++)
					{
						PlayerPrefs.DeleteKey($"[I2split]{i}{key}");
					}
				}
				PlayerPrefs.DeleteKey(key);
			}
			catch (Exception)
			{
				Debug.LogError("Error deleting PlayerPrefs " + key);
			}
		}

		public virtual void ForceSaveSettings()
		{
			PlayerPrefs.Save();
		}

		public virtual bool HasSetting(string key)
		{
			return PlayerPrefs.HasKey(key);
		}

		public virtual bool CanAccessFiles()
		{
			return true;
		}

		private string UpdateFilename(PersistentStorage.eFileType fileType, string fileName)
		{
			switch (fileType)
			{
			case PersistentStorage.eFileType.Persistent:
				fileName = Application.persistentDataPath + "/" + fileName;
				break;
			case PersistentStorage.eFileType.Temporal:
				fileName = Application.temporaryCachePath + "/" + fileName;
				break;
			case PersistentStorage.eFileType.Streaming:
				fileName = Application.streamingAssetsPath + "/" + fileName;
				break;
			}
			return fileName;
		}

		public virtual bool SaveFile(PersistentStorage.eFileType fileType, string fileName, string data, bool logExceptions = true)
		{
			if (!CanAccessFiles())
			{
				return false;
			}
			try
			{
				fileName = UpdateFilename(fileType, fileName);
				File.WriteAllText(fileName, data, Encoding.UTF8);
				return true;
			}
			catch (Exception ex)
			{
				if (logExceptions)
				{
					Debug.LogError("Error saving file '" + fileName + "'\n" + ex);
				}
				return false;
			}
		}

		public virtual string LoadFile(PersistentStorage.eFileType fileType, string fileName, bool logExceptions = true)
		{
			if (!CanAccessFiles())
			{
				return null;
			}
			try
			{
				fileName = UpdateFilename(fileType, fileName);
				return File.ReadAllText(fileName, Encoding.UTF8);
			}
			catch (Exception ex)
			{
				if (logExceptions)
				{
					Debug.LogError("Error loading file '" + fileName + "'\n" + ex);
				}
				return null;
			}
		}

		public virtual bool DeleteFile(PersistentStorage.eFileType fileType, string fileName, bool logExceptions = true)
		{
			if (!CanAccessFiles())
			{
				return false;
			}
			try
			{
				fileName = UpdateFilename(fileType, fileName);
				File.Delete(fileName);
				return true;
			}
			catch (Exception ex)
			{
				if (logExceptions)
				{
					Debug.LogError("Error deleting file '" + fileName + "'\n" + ex);
				}
				return false;
			}
		}

		public virtual bool HasFile(PersistentStorage.eFileType fileType, string fileName, bool logExceptions = true)
		{
			if (!CanAccessFiles())
			{
				return false;
			}
			try
			{
				fileName = UpdateFilename(fileType, fileName);
				return File.Exists(fileName);
			}
			catch (Exception ex)
			{
				if (logExceptions)
				{
					Debug.LogError("Error requesting file '" + fileName + "'\n" + ex);
				}
				return false;
			}
		}
	}
}
