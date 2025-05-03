using Arbor;
using UnityEngine;
using Utage;

[AddComponentMenu("")]
public class UtagePauseScenarioJump : StateBehaviour
{
	public AdvEngine advEngine;

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
		advEngine.JumpScenario(PlayerNonSaveDataManager.selectScenarioName);
		Debug.Log("ポーズ中の宴にジャンプ");
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
}
