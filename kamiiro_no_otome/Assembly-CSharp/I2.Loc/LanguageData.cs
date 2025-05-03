using System;

namespace I2.Loc
{
	[Serializable]
	public class LanguageData
	{
		public string Name;

		public string Code;

		public byte Flags;

		[NonSerialized]
		public bool Compressed;

		public bool IsEnabled()
		{
			return (Flags & 1) == 0;
		}

		public void SetEnabled(bool bEnabled)
		{
			if (bEnabled)
			{
				Flags = (byte)(Flags & -2);
			}
			else
			{
				Flags |= 1;
			}
		}

		public bool IsLoaded()
		{
			return (Flags & 4) == 0;
		}

		public bool CanBeUnloaded()
		{
			return (Flags & 2) == 0;
		}

		public void SetLoaded(bool loaded)
		{
			if (loaded)
			{
				Flags = (byte)(Flags & -5);
			}
			else
			{
				Flags |= 4;
			}
		}

		public void SetCanBeUnLoaded(bool allowUnloading)
		{
			if (allowUnloading)
			{
				Flags = (byte)(Flags & -3);
			}
			else
			{
				Flags |= 2;
			}
		}
	}
}
