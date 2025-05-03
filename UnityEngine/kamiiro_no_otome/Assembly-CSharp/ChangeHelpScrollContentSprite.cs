using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ChangeHelpScrollContentSprite : StateBehaviour
{
	private HelpDataManager helpDataManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		helpDataManager = GameObject.Find("Help Data Manager").GetComponent<HelpDataManager>();
	}

	public override void OnStateBegin()
	{
		foreach (Transform item in helpDataManager.helpScrollContentGo.transform)
		{
			item.GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage").image.sprite = helpDataManager.helpScrollContentSpriteArray[0];
		}
		helpDataManager.helpScrollContentGo.transform.GetChild(helpDataManager.selectScrollContentIndex).GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage")
			.image.sprite = helpDataManager.helpScrollContentSpriteArray[1];
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
