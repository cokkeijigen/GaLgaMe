using System.Collections;
using Arbor;
using UnityEngine;
using Utage;

[AddComponentMenu("")]
public class UtageJumpScenario : StateBehaviour
{
	public enum Type
	{
		selectScenarioName,
		victoryScenarioName,
		rematchScenarioName
	}

	public AdvEngine advEngine;

	public Type type;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		if (advEngine == null)
		{
			advEngine = GameObject.Find("AdvEngine").GetComponent<AdvEngine>();
		}
	}

	public override void OnStateBegin()
	{
		switch (type)
		{
		case Type.selectScenarioName:
			PlayerNonSaveDataManager.currentSceneName = "scenario";
			StartCoroutine(JumpScenarioAsync());
			break;
		case Type.victoryScenarioName:
			Debug.Log("勝利シナリオへジャンプ");
			advEngine.JumpScenario(PlayerNonSaveDataManager.victoryScenarioName);
			break;
		case Type.rematchScenarioName:
			advEngine.JumpScenario(PlayerNonSaveDataManager.rematchScenarioName);
			break;
		}
		Transition(stateLink);
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
	}

	public override void OnStateLateUpdate()
	{
	}

	private IEnumerator JumpScenarioAsync()
	{
		advEngine.JumpScenario(PlayerNonSaveDataManager.selectScenarioName);
		Debug.Log("宴開始：" + PlayerNonSaveDataManager.selectScenarioName);
		while (!advEngine.IsEndScenario)
		{
			yield return null;
		}
		UtageComplete();
	}

	private void UtageComplete()
	{
		Transition(stateLink);
		Debug.Log("宴再生終了");
	}
}
