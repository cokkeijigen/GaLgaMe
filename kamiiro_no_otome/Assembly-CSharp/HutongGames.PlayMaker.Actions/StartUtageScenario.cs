using System.Collections;
using UnityEngine;
using Utage;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Utage")]
	[Tooltip("")]
	public class StartUtageScenario : FsmStateAction
	{
		[RequiredField]
		public FsmString loadScenarioName;

		[RequiredField]
		[ObjectType(typeof(AdvEngine))]
		public FsmObject engine;

		public override void Reset()
		{
			loadScenarioName = null;
		}

		public override void OnEnter()
		{
			StartCoroutine(CoTalk());
			Finish();
		}

		private IEnumerator CoTalk()
		{
			AdvEngine e = engine.Value as AdvEngine;
			e.JumpScenario(loadScenarioName.Value);
			Debug.Log("宴開始");
			while (!e.IsEndScenario)
			{
				yield return 0;
			}
			Debug.Log("宴終了");
		}
	}
}
