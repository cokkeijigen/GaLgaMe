using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetEmptyQuestInfoWindow : StateBehaviour
{
	private QuestManager questManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		questManager = GameObject.Find("Quest Manager").GetComponent<QuestManager>();
	}

	public override void OnStateBegin()
	{
		questManager.questInfoFrameGo.SetActive(value: false);
		questManager.requirementFrameGo.SetActive(value: false);
		questManager.rewardFrameGo.SetActive(value: false);
		questManager.questNotSelectFrameGo.SetActive(value: true);
		questManager.questApplyButtonGo.SetActive(value: false);
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
