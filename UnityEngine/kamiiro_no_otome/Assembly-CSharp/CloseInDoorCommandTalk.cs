using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class CloseInDoorCommandTalk : StateBehaviour
{
	private InDoorTalkManager inDoorTalkManager;

	public bool isOtherCharacterClick;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
	}

	public override void OnStateBegin()
	{
		if (!(inDoorTalkManager.commandTalkOriginGo.name == "Heroine Image"))
		{
			ParameterContainer component = inDoorTalkManager.positionTalkImageArray[0].GetComponent<ParameterContainer>();
			bool flag = false;
			inDoorTalkManager.positionTalkImageArray[0].SetActive(value: false);
			inDoorTalkManager.commandTalkOriginGo.SetActive(value: true);
			if (!string.IsNullOrWhiteSpace(component.GetStringList("scenarioNameList")[0]))
			{
				flag = true;
			}
			else if (!string.IsNullOrWhiteSpace(component.GetStringList("scenarioNameList")[1]))
			{
				flag = true;
			}
			inDoorTalkManager.talkAlertGroupGo.SetActive(value: false);
			if (flag)
			{
				switch (component.GetString("positionName"))
				{
				case "近_左":
				case "近_右":
					inDoorTalkManager.nearAlertGroupGo.SetActive(value: true);
					break;
				case "中_左":
				case "中_右":
					inDoorTalkManager.middleAlertGroupGo.SetActive(value: true);
					break;
				case "奥_左":
				case "奥_右":
					inDoorTalkManager.farAlertGroupGo.SetActive(value: true);
					break;
				}
			}
		}
		inDoorTalkManager.isInitializeCommandTalk = false;
		inDoorTalkManager.commandTalkOriginGo = null;
		if (!isOtherCharacterClick)
		{
			MasterAudio.PlaySound("SeWindowClose", 1f, null, 0f, null, null);
		}
		else
		{
			inDoorTalkManager.commandButtonGroupGo.SetActive(value: false);
			inDoorTalkManager.talkBalloonTailLeftGo.SetActive(value: false);
			inDoorTalkManager.talkBalloonTailRightGo.SetActive(value: false);
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
