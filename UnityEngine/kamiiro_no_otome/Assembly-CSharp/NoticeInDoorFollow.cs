using System.Collections.Generic;
using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class NoticeInDoorFollow : StateBehaviour
{
	private InDoorTalkManager inDoorTalkManager;

	private InDoorClickManager inDoorClickManager;

	private InDoorCommandManager inDoorCommandManager;

	private TotalMapAccessManager totalMapAccessManager;

	private ArborFSM headerStatusFSM;

	public StateLink stateLink;

	public StateLink innLink;

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
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		headerStatusFSM = GameObject.Find("Header Status Manager").GetComponent<ArborFSM>();
		inDoorCommandManager.inDoorDialogGroupGo.GetComponent<ParameterContainer>();
		if (inDoorTalkManager.isFollowRequest)
		{
			inDoorTalkManager.isFollowRequestApply = true;
			if (PlayerDataManager.currentPlaceName == "Inn" || PlayerDataManager.currentPlaceName == "InnStreet1")
			{
				inDoorTalkManager.commandFollowButtonLoc.Term = "buttonFollowCancel";
				inDoorTalkManager.commandFollowButtonImage.sprite = inDoorTalkManager.commandFollowButtonSpriteArray[1];
				inDoorTalkManager.isFollowRequest = false;
			}
			else
			{
				ParameterContainer component = inDoorTalkManager.positionTalkImageArray[0].GetComponent<ParameterContainer>();
				component.GetGameObject("clickOriginGo").SetActive(value: false);
				component.GetGameObject("alertGroupGo").SetActive(value: false);
				switch (component.GetString("positionName"))
				{
				case "近_左":
				case "近_右":
					inDoorTalkManager.nearAlertGroupGo.SetActive(value: false);
					break;
				case "中_左":
				case "中_右":
					inDoorTalkManager.middleAlertGroupGo.SetActive(value: false);
					break;
				case "奥_左":
				case "奥_右":
					inDoorTalkManager.farAlertGroupGo.SetActive(value: false);
					break;
				}
				inDoorTalkManager.positionTalkImageArray[0].SetActive(value: false);
				inDoorTalkManager.talkAlertGroupGo.SetActive(value: false);
				inDoorTalkManager.talkBalloonTailRightGo.SetActive(value: false);
				inDoorClickManager.CloseInDoorCommandTalk();
				Sprite characterSprite = inDoorTalkManager.inDoorCharacterCgDataBase.inDoorCharacterCgDataList.Find((InDoorCharacterCgData data) => data.sortID == PlayerDataManager.DungeonHeroineFollowNum).characterSprite;
				inDoorTalkManager.positionTalkImageArray[1].GetComponent<Image>().sprite = characterSprite;
				inDoorTalkManager.positionTalkImageArray[1].SetActive(value: true);
				InDoorCharacterCgData inDoorCharacterCgData = inDoorTalkManager.inDoorCharacterCgDataBase.inDoorCharacterCgDataList.Find((InDoorCharacterCgData data) => data.sortID == PlayerDataManager.DungeonHeroineFollowNum);
				inDoorTalkManager.positionTalkImageArray[1].GetComponent<RectTransform>().localPosition = inDoorCharacterCgData.heroinePositionV2;
				ParameterContainer component2 = inDoorTalkManager.positionTalkImageArray[1].GetComponent<ParameterContainer>();
				component2.SetString("characterName", inDoorCharacterCgData.characterName);
				component2.SetVector2("balloonPosition", inDoorCharacterCgData.heroineBalloonV2);
				inDoorClickManager.OpenInDoorCommandTalkBalloon();
			}
		}
		else if (PlayerDataManager.currentPlaceName == "Inn" || PlayerDataManager.currentPlaceName == "InStreet1")
		{
			Transition(innLink);
		}
		else
		{
			if (PlayerHeroineLocationDataManager.CheckLocalMapHeroineHere(PlayerDataManager.currentPlaceName).isHeroineHere)
			{
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
			inDoorTalkManager.exitButtonCanvasGroup.interactable = true;
			inDoorTalkManager.exitButtonCanvasGroup.alpha = 1f;
		}
		inDoorTalkManager.inDoorCanvasGo.GetComponent<CanvasGroup>().interactable = true;
		inDoorTalkManager.commandButtonGroupGo.GetComponent<CanvasGroup>().interactable = true;
		inDoorTalkManager.commandButtonGroupGo.GetComponent<CanvasGroup>().alpha = 1f;
		PlayerNonSaveDataManager.isHeroineUnFollowRightClickBlock = false;
		HeaderStatusManager component3 = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		inDoorCommandManager.blackImageGo.SetActive(value: false);
		inDoorCommandManager.inDoorDialogGroupGo.SetActive(value: false);
		component3.ResetHeaderUiBrightness();
		inDoorTalkManager.isFollowRequestApply = false;
		MasterAudio.PlaySound("SeMiniButton", 1f, null, 0f, null, null);
		headerStatusFSM.SendTrigger("HeaderStatusRefresh");
		SpriteRenderer component4 = totalMapAccessManager.localHeroineGo.GetComponent<SpriteRenderer>();
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			component4.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		}
		else
		{
			component4.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0);
		}
		PlayerEquipDataManager.CalcEquipPlayerHaveWeaponFactor(CallBackWeaponMethod, isAllCalc: true);
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

	private void CallBackWeaponMethod()
	{
		PlayerStatusDataManager.SetUpPlayerStatus(isSetUp: false, CallBackStatusMethod);
	}

	private void CallBackStatusMethod()
	{
		Transition(stateLink);
		Debug.Log("Equipデータの更新完了");
	}
}
