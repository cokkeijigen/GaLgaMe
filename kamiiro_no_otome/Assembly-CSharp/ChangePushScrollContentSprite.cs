using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ChangePushScrollContentSprite : StateBehaviour
{
	private CraftManager craftManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
	}

	public override void OnStateBegin()
	{
		foreach (Transform item in craftManager.itemSelectScrollContentGO.transform)
		{
			item.GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage").image.sprite = craftManager.selectScrollContentSpriteArray[0];
		}
		craftManager.itemSelectScrollContentGO.transform.GetChild(craftManager.selectScrollContentIndex).GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage")
			.image.sprite = craftManager.selectScrollContentSpriteArray[1];
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
