using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ChangeCarriageScrollContentSprite : StateBehaviour
{
	private CarriageManager carriageManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		carriageManager = GameObject.Find("Carriage Manager").GetComponent<CarriageManager>();
	}

	public override void OnStateBegin()
	{
		if (!carriageManager.isScrollContentInitialize)
		{
			foreach (Transform item in carriageManager.itemSelectScrollContentGo.transform)
			{
				CarriageItemListClickAction component = item.GetComponent<CarriageItemListClickAction>();
				ParameterContainer component2 = item.GetComponent<ParameterContainer>();
				if (component.isStoreDisplay)
				{
					component2.GetVariable<UguiImage>("itemImage").image.sprite = carriageManager.selectScrollContentSpriteArray[2];
				}
				else
				{
					component2.GetVariable<UguiImage>("itemImage").image.sprite = carriageManager.selectScrollContentSpriteArray[0];
				}
			}
			carriageManager.itemSelectScrollContentGo.transform.GetChild(carriageManager.selectScrollContentIndex).GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage")
				.image.sprite = carriageManager.selectScrollContentSpriteArray[1];
		}
		else
		{
			carriageManager.isScrollContentInitialize = false;
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
