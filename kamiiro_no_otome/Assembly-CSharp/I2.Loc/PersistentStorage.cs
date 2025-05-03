namespace I2.Loc
{
	public static class PersistentStorage
	{
		public enum eFileType
		{
			Raw,
			Persistent,
			Temporal,
			Streaming
		}

		private static I2CustomPersistentStorage mStorage;

		public static void SetSetting_String(string key, string value)
		{
			if (mStorage == null)
			{
				mStorage = new I2CustomPersistentStorage();
			}
			mStorage.SetSetting_String(key, value);
		}

		public static string GetSetting_String(string key, string defaultValue)
		{
			if (mStorage == null)
			{
				mStorage = new I2CustomPersistentStorage();
			}
			return mStorage.GetSetting_String(key, defaultValue);
		}

		public static void DeleteSetting(string key)
		{
			if (mStorage == null)
			{
				mStorage = new I2CustomPersistentStorage();
			}
			mStorage.DeleteSetting(key);
		}

		public static bool HasSetting(string key)
		{
			if (mStorage == null)
			{
				mStorage = new I2CustomPersistentStorage();
			}
			return mStorage.HasSetting(key);
		}

		public static void ForceSaveSettings()
		{
			if (mStorage == null)
			{
				mStorage = new I2CustomPersistentStorage();
			}
			mStorage.ForceSaveSettings();
		}

		public static bool CanAccessFiles()
		{
			if (mStorage == null)
			{
				mStorage = new I2CustomPersistentStorage();
			}
			return mStorage.CanAccessFiles();
		}

		public static bool SaveFile(eFileType fileType, string fileName, string data, bool logExceptions = true)
		{
			if (mStorage == null)
			{
				mStorage = new I2CustomPersistentStorage();
			}
			return mStorage.SaveFile(fileType, fileName, data, logExceptions);
		}

		public static string LoadFile(eFileType fileType, string fileName, bool logExceptions = true)
		{
			if (mStorage == null)
			{
				mStorage = new I2CustomPersistentStorage();
			}
			return mStorage.LoadFile(fileType, fileName, logExceptions);
		}

		public static bool DeleteFile(eFileType fileType, string fileName, bool logExceptions = true)
		{
			if (mStorage == null)
			{
				mStorage = new I2CustomPersistentStorage();
			}
			return mStorage.DeleteFile(fileType, fileName, logExceptions);
		}

		public static bool HasFile(eFileType fileType, string fileName, bool logExceptions = true)
		{
			if (mStorage == null)
			{
				mStorage = new I2CustomPersistentStorage();
			}
			return mStorage.HasFile(fileType, fileName, logExceptions);
		}
	}
}
