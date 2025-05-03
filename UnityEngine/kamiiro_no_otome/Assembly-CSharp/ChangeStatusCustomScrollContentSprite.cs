using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ChangeStatusCustomScrollContentSprite : StateBehaviour
{
	private StatusManager statusManager;

	private StatusCustomManager statusCustomManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
		statusCustomManager = GameObject.Find("Status Custom Manager").GetComponent<StatusCustomManager>();
	}

	public override void OnStateBegin()
	{
		GameObject gameObject = null;
		int index = 0;
		string selectCustomCanvasName = statusCustomManager.selectCustomCanvasName;
		if (!(selectCustomCanvasName == "skill"))
		{
			if (selectCustomCanvasName == "factor")
			{
				gameObject = statusCustomManager.statusCustomContentGo;
				index = statusCustomManager.customScrollContentIndex;
			}
		}
		else
		{
			gameObject = statusCustomManager.statusCustomContentGo;
			index = statusCustomManager.customScrollContentIndex;
		}
		if (gameObject.transform.childCount > 0)
		{
			foreach (Transform item in gameObject.transform)
			{
				item.GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage").image.sprite = statusManager.selectScrollContentSpriteArray[0];
			}
			gameObject.transform.GetChild(index).GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage")
				.image.sprite = statusManager.selectScrollContentSpriteArray[1];
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
