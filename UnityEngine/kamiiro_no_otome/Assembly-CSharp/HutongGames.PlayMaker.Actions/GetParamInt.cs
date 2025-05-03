using Arbor;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Arbor")]
	public class GetParamInt : FsmStateAction
	{
		public FsmGameObject gameObject;

		[RequiredField]
		public FsmString valueName;

		[RequiredField]
		public FsmInt getValue;

		public override void Reset()
		{
			gameObject = null;
			getValue = 0;
		}

		public override void OnEnter()
		{
			if (gameObject.Value.TryGetComponent<ParameterContainer>(out var component))
			{
				getValue.Value = component.GetInt(valueName.Value);
			}
			else
			{
				Debug.Log("GetParamInt／パラメータコンテナが存在しません：" + gameObject.Value.name);
			}
			Finish();
		}
	}
}
