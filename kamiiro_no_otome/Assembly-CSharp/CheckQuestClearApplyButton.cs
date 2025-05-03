using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckQuestClearApplyButton : StateBehaviour
{
	private QuestManager questManager;

	public StateLink requestLink;

	public StateLink clearLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		questManager = GameObject.Find("Quest Manager").GetComponent<QuestManager>();
	}

	public override void OnStateBegin()
	{
		if (questManager.isQuestClearApplyButton)
		{
			Transition(clearLink);
		}
		else
		{
			Transition(requestLink);
		}
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
