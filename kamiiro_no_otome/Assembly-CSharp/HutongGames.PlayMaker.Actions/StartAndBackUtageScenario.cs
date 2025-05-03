using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utage;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Utage")]
	[Tooltip("")]
	public class StartAndBackUtageScenario : FsmStateAction
	{
		[RequiredField]
		public FsmString loadScenarioName;

		[RequiredField]
		public FsmString backToSceneName;

		[RequiredField]
		public AdvEngine engine;

		public override void Reset()
		{
			loadScenarioName = null;
			backToSceneName = null;
		}

		public override void OnEnter()
		{
			StartCoroutine(CoTalk());
			Finish();
		}

		private IEnumerator CoTalk()
		{
			engine.JumpScenario(loadScenarioName.Value);
			Debug.Log("宴開始");
			while (!engine.IsEndScenario)
			{
				yield return 0;
			}
			Debug.Log("宴終了");
			SceneManager.LoadScene(backToSceneName.Value);
		}
	}
}
