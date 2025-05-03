using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ChangePushExtensionScrollContentSprite : StateBehaviour
{
	private CraftExtensionManager craftExtensionManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftExtensionManager = GameObject.Find("Craft Extension Manager").GetComponent<CraftExtensionManager>();
	}

	public override void OnStateBegin()
	{
		foreach (Transform item in craftExtensionManager.scrollContentGoArray[0].transform)
		{
			item.GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage").image.sprite = craftExtensionManager.selectScrollContentSpriteArray[0];
		}
		craftExtensionManager.scrollContentGoArray[0].transform.GetChild(craftExtensionManager.clickedScrollContentIndex).GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage")
			.image.sprite = craftExtensionManager.selectScrollContentSpriteArray[1];
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
