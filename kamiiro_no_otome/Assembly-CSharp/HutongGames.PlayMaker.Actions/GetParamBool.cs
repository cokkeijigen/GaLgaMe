using Arbor;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Arbor")]
	public class GetParamBool : FsmStateAction
	{
		public FsmGameObject gameObject;

		[RequiredField]
		public FsmString valueName;

		[RequiredField]
		public FsmBool getValue;

		public override void Reset()
		{
			gameObject = null;
			getValue = false;
		}

		public override void OnEnter()
		{
			if (gameObject.Value.TryGetComponent<ParameterContainer>(out var component))
			{
				getValue.Value = component.GetBool(valueName.Value);
			}
			else
			{
				Debug.Log("GetParamBool／パラメータコンテナが存在しません：" + gameObject.Value.name);
			}
			Finish();
		}
	}
}
