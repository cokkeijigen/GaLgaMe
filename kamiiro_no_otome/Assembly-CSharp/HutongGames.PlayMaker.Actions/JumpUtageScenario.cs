using UnityEngine;
using Utage;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Utage")]
	[Tooltip("")]
	public class JumpUtageScenario : FsmStateAction
	{
		[RequiredField]
		public FsmString loadScenarioName;

		[RequiredField]
		[ObjectType(typeof(AdvEngine))]
		public FsmObject engine;

		[RequiredField]
		[ObjectType(typeof(AdvUguiManager))]
		public FsmObject engineUgui;

		public override void Reset()
		{
			loadScenarioName = null;
		}

		public override void OnEnter()
		{
			AdvEngine obj = engine.Value as AdvEngine;
			(engineUgui.Value as AdvUguiManager).enabled = true;
			obj.JumpScenario(loadScenarioName.Value);
			Debug.Log("宴ジャンプ開始");
			Finish();
		}
	}
}
