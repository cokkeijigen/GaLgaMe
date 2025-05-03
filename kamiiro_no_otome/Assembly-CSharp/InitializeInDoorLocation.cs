using System.Collections.Generic;
using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class InitializeInDoorLocation : StateBehaviour
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
		List<InDoorLocationData> locationDataList = inDoorTalkManager.currentInDoorLocationDataList;
		int currentCount = inDoorTalkManager.currentInDoorLocationCheckCount;
		GameObject gameObject = null;
		switch (locationDataList[currentCount].talkPositionName)
		{
		case "近_左":
			gameObject = inDoorTalkManager.positionNearImageArray[0];
			break;
		case "近_右":
			gameObject = inDoorTalkManager.positionNearImageArray[1];
			break;
		case "中_左":
			gameObject = inDoorTalkManager.positionMiddleImageArray[0];
			break;
		case "中_右":
			gameObject = inDoorTalkManager.positionMiddleImageArray[1];
			break;
		case "奥_左":
			gameObject = inDoorTalkManager.positionFarImageArray[0];
			break;
		case "奥_右":
			gameObject = inDoorTalkManager.positionFarImageArray[1];
			break;
		}
		int thisPlaceInHeroineId = inDoorTalkManager.GetThisPlaceInHeroineId(locationDataList[currentCount]);
		gameObject.SetActive(value: true);
		Sprite sprite = null;
		if (string.IsNullOrEmpty(locationDataList[currentCount].talkCharacterSprite))
		{
			sprite = ((!locationDataList[currentCount].exceptionTalkCharacterImage) ? inDoorTalkManager.inDoorCharacterCgDataBase.inDoorCharacterCgDataList.Find((InDoorCharacterCgData data) => data.characterName == locationDataList[currentCount].talkCharacterName).characterSprite : inDoorTalkManager.inDoorCharacterCgDataBase.inDoorCharacterCgDataList.Find((InDoorCharacterCgData data) => data.characterName == locationDataList[currentCount].talkCharacterName).characterExceptionSprite);
		}
		else
		{
			Debug.Log("インドア待機立ち絵指定：" + locationDataList[currentCount].talkCharacterSprite);
			sprite = inDoorTalkManager.inDoorHeroineTalkSpriteDataBase.inDoorHeroineTalkSpriteList.Find((InDoorHeroineTalkSpriteData data) => data.spriteName == locationDataList[currentCount].talkCharacterSprite).talkSprite;
		}
		gameObject.GetComponent<Image>().sprite = sprite;
		gameObject.GetComponent<RectTransform>().localPosition = locationDataList[currentCount].characterPotisionV2;
		gameObject.GetComponent<RectTransform>().sizeDelta = locationDataList[currentCount].localSizeV2;
		ParameterContainer component = gameObject.GetComponent<ParameterContainer>();
		component.SetString("characterName", locationDataList[currentCount].talkCharacterName);
		component.SetString("positionName", locationDataList[currentCount].talkPositionName);
		InDoorCharacterCgData inDoorCharacterCgData = inDoorTalkManager.inDoorCharacterCgDataBase.inDoorCharacterCgDataList.Find((InDoorCharacterCgData data) => data.characterName == locationDataList[currentCount].talkCharacterName);
		component.SetInt("sortID", inDoorCharacterCgData.sortID);
		if (!PlayerNonSaveDataManager.inDoorAllTalkDictionary.ContainsKey(inDoorCharacterCgData.sortID))
		{
			PlayerNonSaveDataManager.inDoorAllTalkDictionary.Add(inDoorCharacterCgData.sortID, value: false);
		}
		component.SetVector2("balloonPosition", locationDataList[currentCount].balloonPotisionV2);
		component.GetGameObject("alertGroupGo").GetComponent<RectTransform>().anchoredPosition = locationDataList[currentCount].alertPotisionV2;
		InDoorCharacterTalkData inDoorCharacterTalkData = inDoorTalkManager.inDoorTalkDataBase.inDoorCharacterTalkDataList.Find((InDoorCharacterTalkData data) => data.accessPointName == PlayerDataManager.currentAccessPointName && data.placeName == PlayerDataManager.currentPlaceName && data.characterName == locationDataList[currentCount].talkCharacterName && data.sortID < 1000);
		bool flag = false;
		if (inDoorCharacterTalkData.isAlwaysCommandTalk || locationDataList[currentCount].talkCharacterName == "ルーシー")
		{
			if (inDoorCharacterTalkData.sortID < 1000)
			{
				Debug.Log("同行可能フラグ確認：" + locationDataList[currentCount].talkCharacterName);
				switch (locationDataList[currentCount].talkCharacterName)
				{
				case "ルーシー":
				case "リィナ":
				case "シア":
				case "レヴィ":
				{
					string characterDungeonFollowUnLockFlag = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characterName == locationDataList[currentCount].talkCharacterName).characterDungeonFollowUnLockFlag;
					bool value = PlayerFlagDataManager.scenarioFlagDictionary[characterDungeonFollowUnLockFlag];
					component.SetBool("isCommandTalk", value);
					Debug.Log("同行可能フラグ：" + locationDataList[currentCount].talkCharacterName + "／" + value);
					flag = true;
					break;
				}
				default:
					component.SetBool("isCommandTalk", value: true);
					break;
				}
			}
			else
			{
				component.SetBool("isCommandTalk", value: true);
				Debug.Log("立ち絵表示：同行者");
			}
		}
		else
		{
			component.SetBool("isCommandTalk", value: false);
			Debug.Log("立ち絵表示：コマンドなし");
		}
		if (PlayerDataManager.isDungeonHeroineFollow && PlayerDataManager.DungeonHeroineFollowNum == thisPlaceInHeroineId && flag)
		{
			gameObject.SetActive(value: false);
			Debug.Log("インドア：ヒロインは同行中");
		}
		else if (!PlayerNonSaveDataManager.inDoorHeroineExist && flag)
		{
			gameObject.SetActive(value: false);
			Debug.Log("インドア：ヒロインは不在");
		}
		else
		{
			gameObject.SetActive(value: true);
			Debug.Log("立ち絵を表示する：" + locationDataList[currentCount].talkCharacterName);
		}
		if (inDoorCharacterTalkData.isOccurEvent)
		{
			inDoorTalkManager.checkTargetGo = gameObject;
			inDoorTalkManager.eventCheckTalkData = inDoorCharacterTalkData;
			InDoorEventCheckData inDoorEventCheckData = PlayerScenarioDataManager.CheckInDoorCurrentTalkEvent(PlayerDataManager.currentPlaceName, inDoorCharacterTalkData.characterName);
			if (string.IsNullOrEmpty(inDoorEventCheckData.currentScenarioName))
			{
				inDoorTalkManager.SetBalloonActive(value: false, "", isInitialize: true);
				component.SetBool("isDisableTalkEvent", value: false);
				if (inDoorCharacterCgData.visibleProbability < 100)
				{
					CheckMobHeroineVisible(gameObject);
				}
				return;
			}
			inDoorTalkManager.SetBalloonActive(value: true, inDoorEventCheckData.currentScenarioName, isInitialize: true);
			Debug.Log("インドアの会話イベントがある：" + inDoorCharacterTalkData.characterName);
			component.SetBool("isDisableTalkEvent", inDoorEventCheckData.isDisableTalkEvent);
			if (!PlayerNonSaveDataManager.inDoorHeroineExist && flag)
			{
				component.GetGameObject("alertGroupGo").SetActive(value: false);
				Debug.Log("ヒロイン不在なのでインドアの会話イベント実行不可：" + inDoorCharacterTalkData.characterName);
			}
		}
		else
		{
			inDoorTalkManager.currentInDoorLocationCheckCount++;
			Transition(stateLink);
		}
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

	private void CheckMobHeroineVisible(GameObject targetGo)
	{
		TotalMapAccessManager component = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		if (component != null)
		{
			if (!(PlayerDataManager.currentAccessPointName == "Kingdom1") && !(PlayerDataManager.currentAccessPointName == "City1"))
			{
				return;
			}
			ParameterContainer component2 = component.localAreaParentGo.transform.Find(PlayerDataManager.currentAccessPointName).transform.Find(PlayerDataManager.currentPlaceName).GetComponent<ParameterContainer>();
			int value = 0;
			component2.TryGetInt("mobHeroineVisibleCheckedTimeCount", out value);
			if (value > 0)
			{
				if (component2.GetBool("isMobHeroineVisible"))
				{
					targetGo.SetActive(value: true);
					Debug.Log(PlayerDataManager.currentPlaceName + "／モブを表示");
				}
				else
				{
					targetGo.SetActive(value: false);
					Debug.Log(PlayerDataManager.currentPlaceName + "／モブは不在");
				}
			}
			else
			{
				Debug.Log("モブヒロインの変数はない");
			}
		}
		else
		{
			Debug.Log("TotalMapAccessManagerが存在しない");
		}
	}
}
