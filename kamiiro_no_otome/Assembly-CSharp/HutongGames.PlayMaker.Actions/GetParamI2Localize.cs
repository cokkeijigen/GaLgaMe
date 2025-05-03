using Arbor;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Arbor")]
	public class GetParamI2Localize : FsmStateAction
	{
		public FsmGameObject gameObject;

		[RequiredField]
		public FsmString valueName;

		[RequiredField]
		public FsmObject getValue;

		public override void Reset()
		{
			gameObject = null;
			getValue = null;
		}

		public override void OnEnter()
		{
			if (gameObject.Value.TryGetComponent<ParameterContainer>(out var component))
			{
				getValue = component.GetVariable<I2LocalizeComponent>(valueName.Value).localize;
			}
			else
			{
				Debug.Log("GetParamI2Localize／パラメータコンテナが存在しません：" + gameObject.Value.name);
			}
			Finish();
		}
	}
}
