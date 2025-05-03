using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class InitializeInDoorCanvas : StateBehaviour
{
	private HeaderStatusManager headerStatusManager;

	private InDoorTalkManager inDoorTalkManager;

	private InDoorCommandManager inDoorCommandManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
		inDoorCommandManager = GameObject.Find("InDoor Command Manager").GetComponent<InDoorCommandManager>();
	}

	public override void OnStateBegin()
	{
		if (!PlayerNonSaveDataManager.isRequiedUtageResume)
		{
			headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
			headerStatusManager.placeTextLoc.Term = "place" + PlayerDataManager.currentPlaceName;
			headerStatusManager.partyGroupParent.SetActive(value: false);
			headerStatusManager.menuCanvasGroup.gameObject.SetActive(value: false);
			headerStatusManager.mapHelpMarkCanvasGroup.interactable = true;
			headerStatusManager.restShortcutCanvasGroup.interactable = false;
			headerStatusManager.restShortcutCanvasGroup.alpha = 0.5f;
		}
		inDoorTalkManager.ResetInDoorCanvas();
		Sprite sprite = inDoorTalkManager.localMapUnlockDataBase.localMapUnlockDataList.Find((LocalMapUnlockData data) => data.currentPlaceName == PlayerDataManager.currentPlaceName).inDoorBgSpriteArray[PlayerDataManager.currentTimeZone];
		inDoorTalkManager.inDoorBgImage.sprite = sprite;
		string inDoorBgmName = inDoorTalkManager.localMapUnlockDataBase.localMapUnlockDataList.Find((LocalMapUnlockData data) => data.currentPlaceName == PlayerDataManager.currentPlaceName).inDoorBgmName;
		if (!string.IsNullOrEmpty(inDoorBgmName) && !PlayerNonSaveDataManager.isRequiedUtageResume)
		{
			PlayerDataManager.playBgmCategoryName = inDoorBgmName;
			GameObject.Find("Bgm Play Manager").GetComponent<MasterAudioCustomManager>().ChangeInDoorMasterAudioPlaylist();
			Debug.Log("インドアのBGMを設定：" + inDoorBgmName);
		}
		if (PlayerDataManager.currentPlaceName == "Inn" || PlayerDataManager.currentPlaceName == "InnStreet1")
		{
			inDoorTalkManager.carriageBgGroup.SetActive(value: false);
		}
		else if (PlayerDataManager.currentPlaceName == "Carriage" || PlayerDataManager.currentPlaceName == "CityCarriage")
		{
			inDoorTalkManager.carriageBgGroup.SetActive(value: false);
			inDoorCommandManager.SetCarriageImageSprite();
		}
		else if (PlayerDataManager.currentPlaceName == "ItemShop")
		{
			inDoorTalkManager.carriageBgGroup.SetActive(value: false);
		}
		else
		{
			InDoorCharacterLocationData inDoorCharacterLocationData = null;
			new List<InDoorCharacterLocationData>();
			inDoorTalkManager.carriageBgGroup.SetActive(value: false);
			List<InDoorCharacterLocationData> list = (from data in GameDataManager.instance.inDoorLocationDataBase.inDoorCharacterLocationDataList.Where((InDoorCharacterLocationData data) => data.accessPointName == PlayerDataManager.currentAccessPointName && data.placeName == PlayerDataManager.currentPlaceName).ToList()
				orderby data.sortID descending
				select data).ToList();
			for (int i = 0; i < list.Count; i++)
			{
				Debug.Log("リスト数：" + list.Count + "／現在のカウント：" + i + "／確認するクリアフラグ：" + list[i].sectionFlagName);
				if (PlayerFlagDataManager.scenarioFlagDictionary[list[i].sectionFlagName])
				{
					inDoorCharacterLocationData = list[i];
					break;
				}
			}
			inDoorTalkManager.currentInDoorLocationData = inDoorCharacterLocationData;
			switch (PlayerDataManager.currentTimeZone)
			{
			case 0:
				inDoorTalkManager.currentInDoorLocationDataList = inDoorCharacterLocationData.talkCharacterList0;
				break;
			case 1:
				inDoorTalkManager.currentInDoorLocationDataList = inDoorCharacterLocationData.talkCharacterList1;
				break;
			case 2:
				inDoorTalkManager.currentInDoorLocationDataList = inDoorCharacterLocationData.talkCharacterList2;
				break;
			case 3:
				inDoorTalkManager.currentInDoorLocationDataList = inDoorCharacterLocationData.talkCharacterList3;
				break;
			}
			inDoorTalkManager.currentInDoorLocationCheckCount = 0;
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
