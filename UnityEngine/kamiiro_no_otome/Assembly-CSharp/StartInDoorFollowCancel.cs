using System.Collections.Generic;
using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class StartInDoorFollowCancel : StateBehaviour
{
	private InDoorTalkManager inDoorTalkManager;

	private InDoorClickManager inDoorClickManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
		inDoorClickManager = GameObject.Find("InDoor Click Manager").GetComponent<InDoorClickManager>();
	}

	public override void OnStateBegin()
	{
		inDoorTalkManager.commandTalkOriginGo.GetComponent<ParameterContainer>().GetInt("sortID");
		PlayerNonSaveDataManager.isHeroineUnFollowRightClickBlock = true;
		PlayerDataManager.isDungeonHeroineFollow = false;
		MasterAudio.PlaySound("SeApplyButton", 1f, null, 0f, null, null);
		if (PlayerDataManager.currentPlaceName == "Inn" || PlayerDataManager.currentPlaceName == "InnStreet1")
		{
			HeroineCheckData heroineCheckData = PlayerHeroineLocationDataManager.CheckLocalMapHeroineHere(PlayerDataManager.currentPlaceName);
			if (heroineCheckData.isHeroineHere)
			{
				Debug.Log("フォロー解除／宿屋／ヒロインはいる");
				inDoorTalkManager.talkBalloonTailRightGo.SetActive(value: true);
				CharacterStatusData characterStatusData = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == heroineCheckData.heroineID);
				if (PlayerFlagDataManager.scenarioFlagDictionary[characterStatusData.characterDungeonSexUnLockFlag])
				{
					inDoorTalkManager.commandButtonDictionary["Survey"].SetActive(value: true);
				}
				else
				{
					inDoorTalkManager.commandButtonDictionary["Survey"].SetActive(value: false);
				}
				inDoorTalkManager.commandButtonDictionary["Follow"].SetActive(value: true);
				inDoorTalkManager.commandFollowButtonLoc.Term = "buttonFollowRequest";
				inDoorTalkManager.commandFollowButtonImage.sprite = inDoorTalkManager.commandFollowButtonSpriteArray[1];
				inDoorTalkManager.isFollowRequest = true;
				CanvasGroup component = inDoorTalkManager.commandButtonDictionary["Follow"].GetComponent<CanvasGroup>();
				if (PlayerDataManager.isHeroineSpecifyFollow && PlayerDataManager.heroineSpecifyFollowPoint == PlayerDataManager.currentAccessPointName)
				{
					component.interactable = false;
					component.alpha = 0.5f;
					Debug.Log("同行ボタン使用不可");
				}
				else
				{
					component.interactable = true;
					component.alpha = 1f;
				}
				inDoorTalkManager.positionTalkImageArray[0].GetComponent<ParameterContainer>().SetInt("sortID", heroineCheckData.heroineID);
				inDoorTalkManager.positionTalkImageArray[0].GetComponent<ParameterContainer>().GetStringList("scenarioNameList")[0] = PlayerDataManager.currentAccessPointName + "_Survey";
			}
			else
			{
				Debug.Log("フォロー解除／宿屋／ヒロインはいない");
				inDoorTalkManager.positionTalkImageArray[0].SetActive(value: false);
				inDoorTalkManager.talkBalloonTailRightGo.SetActive(value: false);
				inDoorTalkManager.commandButtonDictionary["Survey"].SetActive(value: false);
				inDoorTalkManager.commandButtonDictionary["Follow"].SetActive(value: false);
			}
		}
		else if (PlayerDataManager.currentPlaceName == "Tent1" || PlayerDataManager.currentPlaceName == "Fortress1")
		{
			if (PlayerHeroineLocationDataManager.CheckWorldMapHeroineHere(PlayerDataManager.currentPlaceName).isHeroineHere)
			{
				Debug.Log("フォロー解除／前線基地or砦／ヒロインはいる");
				string heroineName = "";
				GameObject gameObject = null;
				List<InDoorLocationData> currentInDoorLocationDataList = inDoorTalkManager.currentInDoorLocationDataList;
				switch (PlayerDataManager.DungeonHeroineFollowNum)
				{
				case 1:
					heroineName = "ルーシー";
					break;
				case 2:
					heroineName = "リィナ";
					break;
				case 3:
					heroineName = "シア";
					break;
				case 4:
					heroineName = "レヴィ";
					break;
				}
				InDoorLocationData inDoorLocationData = currentInDoorLocationDataList.Find((InDoorLocationData data) => data.talkCharacterName == heroineName);
				if (inDoorLocationData != null)
				{
					switch (inDoorLocationData.talkPositionName)
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
					gameObject.SetActive(value: true);
				}
			}
			inDoorTalkManager.positionTalkImageArray[1].SetActive(value: false);
			inDoorTalkManager.heroineAlertGroupGo.SetActive(value: false);
			inDoorTalkManager.talkBalloonHeroineGo.SetActive(value: false);
			inDoorClickManager.CloseInDoorCommandTalk();
		}
		else
		{
			if (PlayerHeroineLocationDataManager.CheckLocalMapHeroineHere(PlayerDataManager.currentPlaceName).isHeroineHere)
			{
				Debug.Log("フォロー解除／宿屋ではない／ヒロインはいる");
				string heroineName2 = "";
				GameObject gameObject2 = null;
				List<InDoorLocationData> currentInDoorLocationDataList2 = inDoorTalkManager.currentInDoorLocationDataList;
				switch (PlayerDataManager.DungeonHeroineFollowNum)
				{
				case 1:
					heroineName2 = "ルーシー";
					break;
				case 2:
					heroineName2 = "リィナ";
					break;
				case 3:
					heroineName2 = "シア";
					break;
				case 4:
					heroineName2 = "レヴィ";
					break;
				}
				InDoorLocationData inDoorLocationData2 = currentInDoorLocationDataList2.Find((InDoorLocationData data) => data.talkCharacterName == heroineName2);
				if (inDoorLocationData2 != null)
				{
					switch (inDoorLocationData2.talkPositionName)
					{
					case "近_左":
						gameObject2 = inDoorTalkManager.positionNearImageArray[0];
						break;
					case "近_右":
						gameObject2 = inDoorTalkManager.positionNearImageArray[1];
						break;
					case "中_左":
						gameObject2 = inDoorTalkManager.positionMiddleImageArray[0];
						break;
					case "中_右":
						gameObject2 = inDoorTalkManager.positionMiddleImageArray[1];
						break;
					case "奥_左":
						gameObject2 = inDoorTalkManager.positionFarImageArray[0];
						break;
					case "奥_右":
						gameObject2 = inDoorTalkManager.positionFarImageArray[1];
						break;
					}
					gameObject2.SetActive(value: true);
				}
			}
			inDoorTalkManager.positionTalkImageArray[1].SetActive(value: false);
			inDoorTalkManager.heroineAlertGroupGo.SetActive(value: false);
			inDoorTalkManager.talkBalloonHeroineGo.SetActive(value: false);
			inDoorClickManager.CloseInDoorCommandTalk();
		}
		MapHeroineUnFollowManager component2 = GameObject.Find("Map Rest Manager").GetComponent<MapHeroineUnFollowManager>();
		if (component2 != null)
		{
			component2.RefreshUnFollowButtonVisible();
		}
		PlayerStatusDataManager.characterHp[PlayerDataManager.DungeonHeroineFollowNum] = PlayerStatusDataManager.characterMaxHp[PlayerDataManager.DungeonHeroineFollowNum];
		inDoorTalkManager.exitButtonCanvasGroup.interactable = true;
		inDoorTalkManager.exitButtonCanvasGroup.alpha = 1f;
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
