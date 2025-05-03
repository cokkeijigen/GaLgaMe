using UnityEngine;
using Utage;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Utage")]
	[Tooltip("")]
	public class ResumeUtageScenario : FsmStateAction
	{
		[RequiredField]
		public AdvEngine engine;

		public override void Reset()
		{
		}

		public override void OnEnter()
		{
			engine.ResumeScenario();
			Debug.Log("宴再開");
			Finish();
		}
	}
}
