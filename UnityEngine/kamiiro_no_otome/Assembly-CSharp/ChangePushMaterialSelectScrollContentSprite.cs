using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ChangePushMaterialSelectScrollContentSprite : StateBehaviour
{
	private CraftManager craftManager;

	private CraftAddOnManager craftAddOnManager;

	public bool ResetOnly;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftAddOnManager = GameObject.Find("Craft AddOn Manager").GetComponent<CraftAddOnManager>();
	}

	public override void OnStateBegin()
	{
		foreach (Transform item in craftAddOnManager.overlayCanvasScrollContent.transform)
		{
			item.GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage").image.sprite = craftManager.selectScrollContentSpriteArray[0];
		}
		if (!ResetOnly)
		{
			craftAddOnManager.overlayCanvasScrollContent.transform.GetChild(craftAddOnManager.selectScrollContentIndex).GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage")
				.image.sprite = craftManager.selectScrollContentSpriteArray[1];
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
}
