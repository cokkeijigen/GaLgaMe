using Arbor;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Arbor")]
	public class GetParamUguiText : FsmStateAction
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
				getValue = component.GetVariable<UguiTextVariable>(valueName.Value).text;
			}
			else
			{
				Debug.Log("GetParamUguiText／パラメータコンテナが存在しません：" + gameObject.Value.name);
			}
			Finish();
		}
	}
}
