using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class OpenInDoorTalkBalloon : StateBehaviour
{
	private InDoorTalkManager inDoorTalkManager;

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
		string term = "";
		ParameterContainer param = inDoorTalkManager.clickTargetGo.GetComponent<ParameterContainer>();
		InDoorCharacterTalkData inDoorCharacterTalkData = inDoorTalkManager.inDoorTalkDataBase.inDoorCharacterTalkDataList.Find((InDoorCharacterTalkData data) => data.accessPointName == PlayerDataManager.currentAccessPointName && data.placeName == PlayerDataManager.currentPlaceName && data.characterName == param.GetString("characterName"));
		for (int num = inDoorCharacterTalkData.talkSectionFlagNameList.Count; num > 0; num--)
		{
			if (PlayerFlagDataManager.scenarioFlagDictionary[inDoorCharacterTalkData.talkSectionFlagNameList[num - 1]])
			{
				term = inDoorCharacterTalkData.talkSectionTermList[num - 1];
				break;
			}
		}
		switch (param.GetString("positionName"))
		{
		case "近_左":
		case "中_左":
		case "奥_左":
			inDoorTalkManager.talkBalloonTailLeftGo.SetActive(value: true);
			inDoorTalkManager.talkBalloonTailRightGo.SetActive(value: false);
			inDoorTalkManager.talkBalloonTailLeftGo.GetComponent<RectTransform>().localPosition = param.GetVector2("balloonPosition");
			inDoorTalkManager.talkBalloonTailLeftTerm.Term = term;
			break;
		case "近_右":
		case "中_右":
		case "奥_右":
			inDoorTalkManager.talkBalloonTailLeftGo.SetActive(value: false);
			inDoorTalkManager.talkBalloonTailRightGo.SetActive(value: true);
			inDoorTalkManager.talkBalloonTailRightGo.GetComponent<RectTransform>().localPosition = param.GetVector2("balloonPosition");
			inDoorTalkManager.talkBalloonTailRightTerm.Term = term;
			break;
		}
		MasterAudio.PlaySound("SeMiniButton", 1f, null, 0f, null, null);
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
