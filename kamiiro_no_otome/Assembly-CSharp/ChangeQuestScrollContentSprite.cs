using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ChangeQuestScrollContentSprite : StateBehaviour
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
		foreach (Transform item in questManager.questScrollContentGo.transform)
		{
			item.GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage").image.sprite = questManager.questScrollContentSpriteArray[0];
		}
		questManager.questScrollContentGo.transform.GetChild(questManager.selectScrollContentIndex).GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage")
			.image.sprite = questManager.questScrollContentSpriteArray[1];
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
