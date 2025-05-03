using System;

namespace I2.Loc
{
	public class TranslationJob : IDisposable
	{
		public enum eJobState
		{
			Running,
			Succeeded,
			Failed
		}

		public eJobState mJobState;

		public virtual eJobState GetState()
		{
			return mJobState;
		}

		public virtual void Dispose()
		{
		}
	}
}
