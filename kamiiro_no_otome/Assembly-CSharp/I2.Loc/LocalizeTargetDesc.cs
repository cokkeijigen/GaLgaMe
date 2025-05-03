using System;
using UnityEngine;

namespace I2.Loc
{
	public abstract class LocalizeTargetDesc<T> : ILocalizeTargetDescriptor where T : ILocalizeTarget
	{
		public override ILocalizeTarget CreateTarget(Localize cmp)
		{
			return ScriptableObject.CreateInstance<T>();
		}

		public override Type GetTargetType()
		{
			return typeof(T);
		}
	}
}
