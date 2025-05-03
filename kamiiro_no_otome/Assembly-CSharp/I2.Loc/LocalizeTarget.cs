using UnityEngine;

namespace I2.Loc
{
	public abstract class LocalizeTarget<T> : ILocalizeTarget where T : Object
	{
		public T mTarget;

		public override bool IsValid(Localize cmp)
		{
			if (mTarget != null)
			{
				Component component = mTarget as Component;
				if (component != null && component.gameObject != cmp.gameObject)
				{
					mTarget = null;
				}
			}
			if (mTarget == null)
			{
				mTarget = cmp.GetComponent<T>();
			}
			return mTarget != null;
		}
	}
}
