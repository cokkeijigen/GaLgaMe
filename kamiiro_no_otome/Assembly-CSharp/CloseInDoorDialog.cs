using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class CloseInDoorDialog : StateBehaviour
{
	private InDoorTalkManager inDoorTalkManager;

	private InDoorClickManager inDoorClickManager;

	private InDoorCommandManager inDoorCommandManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
		inDoorClickManager = GameObject.Find("InDoor Click Manager").GetComponent<InDoorClickManager>();
		inDoorCommandManager = GameObject.Find("InDoor Command Manager").GetComponent<InDoorCommandManager>();
	}

	public override void OnStateBegin()
	{
		inDoorTalkManager.inDoorCanvasGo.GetComponent<CanvasGroup>().interactable = true;
		inDoorCommandManager.inDoorDialogGroupGo.SetActive(value: false);
		if (inDoorTalkManager.clickTargetGo.name == "Heroine Image")
		{
			inDoorTalkManager.positionTalkImageArray[1].GetComponent<Image>().sprite = inDoorTalkManager.clickTargetGoSprite;
		}
		else
		{
			inDoorClickManager.SetCommandTalkCharacterSprite();
		}
		inDoorClickManager.OpenInDoorCommandTalkBalloon();
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
