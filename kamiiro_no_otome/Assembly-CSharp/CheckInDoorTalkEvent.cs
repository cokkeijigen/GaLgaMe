using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckInDoorTalkEvent : StateBehaviour
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
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < inDoorTalkManager.positionFarImageArray.Length; i++)
		{
			if (inDoorTalkManager.positionFarImageArray[i].activeInHierarchy)
			{
				list.Add(inDoorTalkManager.positionFarImageArray[i]);
			}
		}
		for (int j = 0; j < inDoorTalkManager.positionMiddleImageArray.Length; j++)
		{
			if (inDoorTalkManager.positionMiddleImageArray[j].activeInHierarchy)
			{
				list.Add(inDoorTalkManager.positionMiddleImageArray[j]);
			}
		}
		for (int k = 0; k < inDoorTalkManager.positionNearImageArray.Length; k++)
		{
			if (inDoorTalkManager.positionNearImageArray[k].activeInHierarchy)
			{
				list.Add(inDoorTalkManager.positionNearImageArray[k]);
			}
		}
		if (list.Count > 0 && list != null)
		{
			for (int l = 0; l < list.Count; l++)
			{
				string characterName = list[l].GetComponent<ParameterContainer>().GetString("characterName");
				InDoorCharacterTalkData inDoorCharacterTalkData = inDoorTalkManager.inDoorTalkDataBase.inDoorCharacterTalkDataList.Find((InDoorCharacterTalkData data) => data.accessPointName == PlayerDataManager.currentAccessPointName && data.placeName == PlayerDataManager.currentPlaceName && data.characterName == characterName);
				if (inDoorCharacterTalkData.isOccurEvent)
				{
					inDoorTalkManager.checkTargetGo = list[l];
					inDoorTalkManager.eventCheckTalkData = inDoorCharacterTalkData;
					InDoorEventCheckData inDoorEventCheckData = PlayerScenarioDataManager.CheckInDoorCurrentEvent(PlayerDataManager.currentPlaceName);
					if (string.IsNullOrEmpty(inDoorEventCheckData.currentScenarioName))
					{
						inDoorTalkManager.SetBalloonActive(value: false, "", isInitialize: false);
						Debug.Log("再計算シナリオ消滅");
					}
					else if (GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Find((ScenarioFlagData data) => data.scenarioName == inDoorEventCheckData.currentScenarioName).scenarioTalkCharacter == characterName)
					{
						inDoorTalkManager.SetBalloonActive(value: true, inDoorEventCheckData.currentScenarioName, isInitialize: false);
						Debug.Log("再計算シナリオあり");
					}
				}
				else
				{
					inDoorTalkManager.currentInDoorLocationCheckCount++;
					Debug.Log("再計算シナリオなし");
				}
			}
		}
		else
		{
			Debug.Log("再計算シナリオなし");
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
