using System.Collections.Generic;
using System.Linq;
using Arbor;
using HutongGames.PlayMaker;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class InDoorClickManager : SerializedMonoBehaviour
{
	public InDoorTalkManager inDoorTalkManager;

	public InDoorCommandManager inDoorCommandManager;

	public PlayMakerFSM clickFSM;

	[Readonly]
	public Transform currentTalkGo;

	[Readonly]
	public Sprite currentCharacterIdleSprite;

	[Readonly]
	public Sprite currentCharacterTalkSprite;

	[Readonly]
	public InDoorCharacterTalkData currentTalkData;

	public void ClickTalkCharacter(GameObject gameObject)
	{
		clickFSM.FsmVariables.GetFsmGameObject("clickTargetGo").Value = gameObject;
		inDoorTalkManager.clickTargetGo = gameObject;
		inDoorTalkManager.clickTargetGoSprite = gameObject.GetComponent<Image>().sprite;
		clickFSM.SendEvent("ClickNormalTalk");
	}

	public void CloseInDoorCommandTalk()
	{
		HeaderStatusManager component = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		inDoorTalkManager.isInitializeCommandTalk = false;
		inDoorTalkManager.commandTalkOriginGo = null;
		inDoorTalkManager.commandButtonGroupGo.SetActive(value: false);
		inDoorTalkManager.positionTalkImageArray[1].GetComponent<Button>().interactable = true;
		inDoorCommandManager.blackImageGo.SetActive(value: false);
		component.ResetHeaderUiBrightness();
		inDoorTalkManager.exitButtonCanvasGroup.interactable = true;
		inDoorTalkManager.exitButtonCanvasGroup.alpha = 1f;
		Debug.Log("コマンドボタンを閉じる");
	}

	public void RevertInDoorCommandTalkCharacter()
	{
		ParameterContainer component = inDoorTalkManager.positionTalkImageArray[0].GetComponent<ParameterContainer>();
		bool flag = false;
		inDoorTalkManager.positionTalkImageArray[0].SetActive(value: false);
		inDoorTalkManager.talkAlertGroupGo.SetActive(value: false);
		component.GetGameObject("clickOriginGo").SetActive(value: true);
		if (!string.IsNullOrWhiteSpace(component.GetStringList("scenarioNameList")[0]))
		{
			flag = true;
		}
		else if (!string.IsNullOrWhiteSpace(component.GetStringList("scenarioNameList")[1]))
		{
			flag = true;
		}
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

	public void OpenInDoorTalkBalloon()
	{
		string text = "";
		int index = 0;
		ParameterContainer param = inDoorTalkManager.clickTargetGo.GetComponent<ParameterContainer>();
		int @int = param.GetInt("sortID");
		InDoorCharacterTalkData inDoorCharacterTalkData = inDoorTalkManager.inDoorTalkDataBase.inDoorCharacterTalkDataList.Find((InDoorCharacterTalkData data) => data.accessPointName == PlayerDataManager.currentAccessPointName && data.placeName == PlayerDataManager.currentPlaceName && data.characterName == param.GetString("characterName") && data.sortID < 1000);
		Debug.Log("会話対象：" + inDoorCharacterTalkData.characterName);
		for (int num = inDoorCharacterTalkData.talkSectionFlagNameList.Count; num > 0; num--)
		{
			if (PlayerFlagDataManager.scenarioFlagDictionary[inDoorCharacterTalkData.talkSectionFlagNameList[num - 1]])
			{
				text = inDoorCharacterTalkData.talkSectionTermList[num - 1];
				index = num - 1;
				Debug.Log("会話番号：" + index);
				break;
			}
		}
		currentTalkData = inDoorCharacterTalkData;
		currentTalkGo = inDoorTalkManager.clickTargetGo.transform;
		if (inDoorCharacterTalkData.talkCharacterSpriteList.Count > 0)
		{
			Debug.Log("インドア通常フキダシ会話立ち絵指定：" + inDoorCharacterTalkData.talkCharacterSpriteList[index].name);
			currentCharacterIdleSprite = inDoorTalkManager.clickTargetGoSprite;
			inDoorTalkManager.clickTargetGo.GetComponent<Image>().sprite = inDoorCharacterTalkData.talkCharacterSpriteList[index];
		}
		inDoorTalkManager.talkBalloonHeroineGo.SetActive(value: false);
		switch (param.GetString("positionName"))
		{
		case "近_左":
		case "中_左":
		case "奥_左":
			inDoorTalkManager.talkBalloonTailLeftGo.SetActive(value: true);
			inDoorTalkManager.talkBalloonTailRightGo.SetActive(value: false);
			Debug.Log("左側にしっぽフキダシを表示");
			inDoorTalkManager.talkBalloonTailLeftGo.GetComponent<RectTransform>().localPosition = param.GetVector2("balloonPosition");
			if (inDoorCharacterTalkData.talkSectionWordCountList[index] > 1)
			{
				int int3 = param.GetInt("talkWordCount");
				inDoorTalkManager.talkBalloonTailLeftTerm.Term = text + "_" + int3;
				int3++;
				if (int3 >= inDoorCharacterTalkData.talkSectionWordCountList.Count)
				{
					int3 = 0;
					if (!PlayerNonSaveDataManager.inDoorAllTalkDictionary[@int])
					{
						PlayerNonSaveDataManager.inDoorAllTalkDictionary[@int] = true;
					}
				}
				param.SetInt("talkWordCount", int3);
			}
			else
			{
				inDoorTalkManager.talkBalloonTailLeftTerm.Term = text;
				if (!PlayerNonSaveDataManager.inDoorAllTalkDictionary[@int])
				{
					PlayerNonSaveDataManager.inDoorAllTalkDictionary[@int] = true;
				}
			}
			break;
		case "近_右":
		case "中_右":
		case "奥_右":
			inDoorTalkManager.talkBalloonTailLeftGo.SetActive(value: false);
			inDoorTalkManager.talkBalloonTailRightGo.SetActive(value: true);
			Debug.Log("右側にしっぽフキダシを表示");
			inDoorTalkManager.talkBalloonTailRightGo.GetComponent<RectTransform>().localPosition = param.GetVector2("balloonPosition");
			if (inDoorCharacterTalkData.talkSectionWordCountList[index] > 1)
			{
				int int2 = param.GetInt("talkWordCount");
				inDoorTalkManager.talkBalloonTailRightTerm.Term = text + "_" + int2;
				int2++;
				if (int2 >= inDoorCharacterTalkData.talkSectionWordCountList.Count)
				{
					int2 = 0;
					if (!PlayerNonSaveDataManager.inDoorAllTalkDictionary[@int])
					{
						PlayerNonSaveDataManager.inDoorAllTalkDictionary[@int] = true;
					}
				}
				param.SetInt("talkWordCount", int2);
			}
			else
			{
				inDoorTalkManager.talkBalloonTailRightTerm.Term = text;
				if (!PlayerNonSaveDataManager.inDoorAllTalkDictionary[@int])
				{
					PlayerNonSaveDataManager.inDoorAllTalkDictionary[@int] = true;
				}
			}
			break;
		}
		if (PlayerNonSaveDataManager.isRequiedUtageResume)
		{
			inDoorTalkManager.CheckScnerioInDoorAllTalk();
		}
	}

	public void OpenInDoorCommandTalkBalloon()
	{
		string term = "";
		ParameterContainer param = inDoorTalkManager.clickTargetGo.GetComponent<ParameterContainer>();
		InDoorCharacterTalkData inDoorCharacterTalkData = null;
		inDoorCharacterTalkData = ((!(PlayerDataManager.currentPlaceName == "Inn") && !(PlayerDataManager.currentPlaceName == "InnStreet1")) ? ((!(inDoorTalkManager.clickTargetGo.name == "Heroine Image") && !inDoorTalkManager.isFollowRequestApply) ? inDoorTalkManager.inDoorTalkDataBase.inDoorCharacterTalkDataList.Where((InDoorCharacterTalkData data) => data.sortID < 1000).ToList().Find((InDoorCharacterTalkData data) => data.accessPointName == PlayerDataManager.currentAccessPointName && data.placeName == PlayerDataManager.currentPlaceName && data.characterName == param.GetString("characterName")) : inDoorTalkManager.inDoorTalkDataBase.inDoorCharacterTalkDataList.Where((InDoorCharacterTalkData data) => data.sortID > 1000).ToList().Find((InDoorCharacterTalkData data) => data.accessPointName == PlayerDataManager.currentAccessPointName && data.placeName == PlayerDataManager.currentPlaceName && data.characterName == param.GetString("characterName"))) : ((!PlayerDataManager.isDungeonHeroineFollow) ? inDoorTalkManager.inDoorTalkDataBase.inDoorCharacterTalkDataList.Where((InDoorCharacterTalkData data) => data.sortID < 1000).ToList().Find((InDoorCharacterTalkData data) => data.accessPointName == PlayerDataManager.currentAccessPointName && data.placeName == PlayerDataManager.currentPlaceName && data.characterName == param.GetString("characterName")) : inDoorTalkManager.inDoorTalkDataBase.inDoorCharacterTalkDataList.Where((InDoorCharacterTalkData data) => data.sortID > 1000).ToList().Find((InDoorCharacterTalkData data) => data.accessPointName == PlayerDataManager.currentAccessPointName && data.placeName == PlayerDataManager.currentPlaceName && data.characterName == param.GetString("characterName"))));
		for (int num = inDoorCharacterTalkData.talkSectionFlagNameList.Count; num > 0; num--)
		{
			if (PlayerFlagDataManager.scenarioFlagDictionary[inDoorCharacterTalkData.talkSectionFlagNameList[num - 1]])
			{
				term = inDoorCharacterTalkData.talkSectionTermList[num - 1];
				break;
			}
		}
		if (PlayerDataManager.currentPlaceName != "Inn" && PlayerDataManager.currentPlaceName != "InnStreet1")
		{
			inDoorCommandManager.blackImageGo.SetActive(value: true);
			GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>().SetHeaderUiBrightness(0.4f);
			Debug.Log("インドアUIを暗転");
		}
		if (inDoorTalkManager.clickTargetGo.name == "Heroine Image")
		{
			inDoorTalkManager.talkBalloonHeroineTerm.Term = term;
			inDoorTalkManager.heroineTalkGroup.transform.SetSiblingIndex(10);
			inDoorCommandManager.blackImageGo.transform.SetSiblingIndex(9);
			inDoorTalkManager.commandTalkGroup.transform.SetSiblingIndex(8);
		}
		else
		{
			inDoorTalkManager.talkBalloonTailRightTerm.Term = term;
			inDoorTalkManager.commandTalkGroup.transform.SetSiblingIndex(10);
			inDoorCommandManager.blackImageGo.transform.SetSiblingIndex(9);
			inDoorTalkManager.heroineTalkGroup.transform.SetSiblingIndex(8);
		}
		if (inDoorTalkManager.isFollowRequestApply)
		{
			inDoorTalkManager.talkBalloonHeroineGo.SetActive(value: false);
		}
	}

	public void InitializeInDoorCommandTalk()
	{
		ParameterContainer param = inDoorTalkManager.clickTargetGo.GetComponent<ParameterContainer>();
		RectTransform component = inDoorTalkManager.commandButtonGroupGo.GetComponent<RectTransform>();
		Vector2 vector = new Vector2(0f, 0f);
		foreach (KeyValuePair<string, GameObject> item in inDoorTalkManager.commandButtonDictionary)
		{
			item.Value.SetActive(value: false);
		}
		inDoorTalkManager.commandButtonGroupGo.SetActive(value: true);
		if (inDoorTalkManager.clickTargetGo.name == "Heroine Image")
		{
			component.anchoredPosition = new Vector2(693f, 20f);
			inDoorTalkManager.commandButtonDictionary["Follow"].SetActive(value: true);
			inDoorTalkManager.commandFollowButtonLoc.Term = "buttonFollowCancel";
			inDoorTalkManager.commandFollowButtonImage.sprite = inDoorTalkManager.commandFollowButtonSpriteArray[1];
			inDoorTalkManager.isFollowRequest = false;
			CanvasGroup component2 = inDoorTalkManager.commandButtonDictionary["Follow"].GetComponent<CanvasGroup>();
			if (PlayerDataManager.isHeroineSpecifyFollow && PlayerDataManager.heroineSpecifyFollowPoint == PlayerDataManager.currentAccessPointName)
			{
				component2.interactable = false;
				component2.alpha = 0.5f;
				Debug.Log("同行ボタン使用不可");
			}
			else
			{
				component2.interactable = true;
				component2.alpha = 1f;
			}
			vector = inDoorTalkManager.inDoorCharacterCgDataBase.inDoorCharacterCgDataList.Find((InDoorCharacterCgData data) => data.characterName == param.GetString("characterName")).heroineBalloonV2;
			inDoorTalkManager.talkBalloonHeroineGo.SetActive(value: true);
			inDoorTalkManager.talkBalloonTailRightGo.SetActive(value: false);
			inDoorTalkManager.positionTalkImageArray[0].GetComponent<ParameterContainer>().SetGameObject("clickOriginGo", inDoorTalkManager.clickTargetGo);
			bool[] array = new bool[2];
			for (int i = 0; i < 2; i++)
			{
				if (!string.IsNullOrEmpty(param.GetStringList("scenarioNameList")[i]))
				{
					array[i] = true;
				}
			}
			if (array.Any((bool data) => data))
			{
				inDoorTalkManager.heroineAlertGoArray[0].GetComponent<Image>().enabled = array[0];
				inDoorTalkManager.commandButtonDictionary["Sex"].SetActive(array[0]);
				inDoorTalkManager.heroineAlertGoArray[1].GetComponent<Image>().enabled = array[1];
				inDoorTalkManager.commandButtonDictionary["Event"].SetActive(array[1]);
				if (param.GetBool("isDisableTalkEvent"))
				{
					inDoorTalkManager.commandButtonDictionary["Event"].GetComponent<CanvasGroup>().interactable = false;
					inDoorTalkManager.commandButtonDictionary["Event"].GetComponent<CanvasGroup>().alpha = 0.5f;
				}
				else
				{
					inDoorTalkManager.commandButtonDictionary["Event"].GetComponent<CanvasGroup>().interactable = true;
					inDoorTalkManager.commandButtonDictionary["Event"].GetComponent<CanvasGroup>().alpha = 1f;
				}
				inDoorTalkManager.heroineAlertGroupGo.SetActive(value: true);
			}
		}
		else
		{
			InDoorCharacterCgData inDoorCharacterCgData = inDoorTalkManager.inDoorCharacterCgDataBase.inDoorCharacterCgDataList.Find((InDoorCharacterCgData data) => data.characterName == param.GetString("characterName"));
			ParameterContainer component3 = inDoorTalkManager.positionTalkImageArray[0].GetComponent<ParameterContainer>();
			inDoorTalkManager.clickTargetGo.SetActive(value: false);
			component3.SetString("characterName", param.GetString("characterName"));
			component3.SetString("positionName", param.GetString("positionName"));
			component3.SetStringList("scenarioNameList", param.GetStringList("scenarioNameList"));
			component3.SetGameObject("clickOriginGo", inDoorTalkManager.clickTargetGo);
			component3.SetInt("sortID", param.GetInt("sortID"));
			switch (param.GetString("positionName"))
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
			inDoorTalkManager.positionTalkImageArray[0].GetComponent<Image>().sprite = inDoorTalkManager.clickTargetGo.GetComponent<Image>().sprite;
			inDoorTalkManager.positionTalkImageArray[0].SetActive(value: true);
			inDoorTalkManager.positionTalkImageArray[0].GetComponent<RectTransform>().localPosition = inDoorCharacterCgData.talkPositionV2;
			inDoorTalkManager.positionTalkImageArray[0].GetComponent<RectTransform>().localScale = new Vector2(inDoorCharacterCgData.talkImageDisplayMagnification, inDoorCharacterCgData.talkImageDisplayMagnification);
			inDoorTalkManager.talkAlertGroupGo.GetComponent<RectTransform>().anchoredPosition = inDoorCharacterCgData.talkAlertV2;
			component.anchoredPosition = new Vector2(0f, 20f);
			inDoorTalkManager.commandFollowButtonLoc.Term = "buttonFollowRequest";
			inDoorTalkManager.commandFollowButtonImage.sprite = inDoorTalkManager.commandFollowButtonSpriteArray[0];
			inDoorTalkManager.isFollowRequest = true;
			vector = inDoorCharacterCgData.talkBalloonV2;
			inDoorTalkManager.talkBalloonHeroineGo.SetActive(value: false);
			inDoorTalkManager.talkBalloonTailRightGo.SetActive(value: true);
			List<string> commandButtonKeyList = inDoorTalkManager.inDoorCharacterCgDataBase.inDoorCharacterCgDataList.Find((InDoorCharacterCgData data) => data.characterName == param.GetString("characterName")).commandButtonKeyList;
			if (commandButtonKeyList.Any())
			{
				for (int j = 0; j < commandButtonKeyList.Count; j++)
				{
					inDoorTalkManager.commandButtonDictionary[commandButtonKeyList[j]].SetActive(value: true);
				}
			}
			if (inDoorTalkManager.commandButtonDictionary["Follow"].activeInHierarchy)
			{
				CanvasGroup component4 = inDoorTalkManager.commandButtonDictionary["Follow"].GetComponent<CanvasGroup>();
				if (PlayerDataManager.isHeroineSpecifyFollow && PlayerDataManager.heroineSpecifyFollowPoint == PlayerDataManager.currentAccessPointName)
				{
					component4.interactable = false;
					component4.alpha = 0.5f;
					Debug.Log("同行ボタン使用不可");
				}
				else
				{
					component4.interactable = true;
					component4.alpha = 1f;
					Debug.Log("同行可能");
					int @int = inDoorTalkManager.positionTalkImageArray[0].GetComponent<ParameterContainer>().GetInt("sortID");
					string characterDungeonFollowUnLockFlag = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[@int].characterDungeonFollowUnLockFlag;
					if (!PlayerFlagDataManager.scenarioFlagDictionary[characterDungeonFollowUnLockFlag])
					{
						inDoorTalkManager.commandButtonDictionary["Follow"].SetActive(value: false);
						Debug.Log("同行ボタン非表示");
					}
					else
					{
						Debug.Log("同行可能");
					}
				}
			}
			else
			{
				Debug.Log("同行ボタン表示なし");
			}
			bool[] array2 = new bool[2];
			for (int k = 0; k < 2; k++)
			{
				if (!string.IsNullOrEmpty(param.GetStringList("scenarioNameList")[k]))
				{
					array2[k] = true;
				}
			}
			if (array2.Any((bool data) => data))
			{
				inDoorTalkManager.talkAlertGoArray[0].GetComponent<Image>().enabled = array2[0];
				inDoorTalkManager.commandButtonDictionary["Sex"].SetActive(array2[0]);
				inDoorTalkManager.talkAlertGoArray[1].GetComponent<Image>().enabled = array2[1];
				inDoorTalkManager.commandButtonDictionary["Event"].SetActive(array2[1]);
				if (param.GetBool("isDisableTalkEvent"))
				{
					inDoorTalkManager.commandButtonDictionary["Event"].GetComponent<CanvasGroup>().interactable = false;
					inDoorTalkManager.commandButtonDictionary["Event"].GetComponent<CanvasGroup>().alpha = 0.5f;
				}
				else
				{
					inDoorTalkManager.commandButtonDictionary["Event"].GetComponent<CanvasGroup>().interactable = true;
					inDoorTalkManager.commandButtonDictionary["Event"].GetComponent<CanvasGroup>().alpha = 1f;
				}
				inDoorTalkManager.talkAlertGroupGo.SetActive(value: true);
			}
		}
		inDoorTalkManager.talkBalloonTailRightGo.GetComponent<RectTransform>().localPosition = vector;
		inDoorTalkManager.talkBalloonHeroineGo.GetComponent<RectTransform>().localPosition = vector;
		inDoorTalkManager.talkBalloonTailLeftGo.SetActive(value: false);
		inDoorTalkManager.isInitializeCommandTalk = true;
		inDoorTalkManager.commandTalkOriginGo = inDoorTalkManager.clickTargetGo;
		Debug.Log("インドアコマンド初期化完了");
	}

	public bool GetInDoorCommandTalkInitializeState()
	{
		return inDoorTalkManager.isInitializeCommandTalk;
	}

	public bool GetInDoorCommandTalkCharacterSameState()
	{
		bool flag = false;
		GameObject gameObject = inDoorTalkManager.positionTalkImageArray[0].GetComponent<ParameterContainer>().GetGameObject("clickOriginGo");
		flag = ((inDoorTalkManager.clickTargetGo == gameObject) ? true : false);
		Debug.Log("clickTargetGo；" + inDoorTalkManager.clickTargetGo.name + "／clickOriginGo：" + gameObject.name + "／" + flag);
		return flag;
	}

	public bool GetBalloonIsActive()
	{
		bool result = false;
		if (inDoorTalkManager.talkBalloonHeroineGo.activeInHierarchy)
		{
			result = true;
		}
		if (inDoorTalkManager.talkBalloonTailLeftGo.activeInHierarchy)
		{
			result = true;
		}
		if (inDoorTalkManager.talkBalloonTailRightGo.activeInHierarchy)
		{
			result = true;
		}
		return result;
	}

	public bool GetCurrentPlaceIsInn()
	{
		bool result = false;
		if (PlayerDataManager.currentPlaceName == "Inn" || PlayerDataManager.currentPlaceName == "InnStreet1")
		{
			result = true;
		}
		return result;
	}

	public bool GetCurrentPlaceIsItemShop()
	{
		bool result = false;
		if (PlayerDataManager.currentPlaceName == "ItemShop")
		{
			result = true;
		}
		return result;
	}

	public void OpenInnBalloon()
	{
		string term = "";
		InDoorCharacterTalkData inDoorCharacterTalkData = null;
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			List<InDoorCharacterTalkData> list = inDoorTalkManager.inDoorTalkDataBase.inDoorCharacterTalkDataList.Where((InDoorCharacterTalkData data) => data.sortID > 1000).ToList();
			ParameterContainer param = inDoorTalkManager.positionTalkImageArray[0].GetComponent<ParameterContainer>();
			inDoorCharacterTalkData = list.Find((InDoorCharacterTalkData data) => data.accessPointName == PlayerDataManager.currentAccessPointName && data.placeName == PlayerDataManager.currentPlaceName && data.characterName == param.GetString("characterName"));
			term = GetTalkTerm(inDoorCharacterTalkData);
		}
		else if (PlayerDataManager.currentPlaceName == "InnStreet1")
		{
			List<InDoorCharacterTalkData> list2 = inDoorTalkManager.inDoorTalkDataBase.inDoorCharacterTalkDataList.Where((InDoorCharacterTalkData data) => data.sortID < 1000).ToList();
			if (GameObject.Find("InnStreet1").GetComponent<ParameterContainer>().GetBool("isHeroineExist"))
			{
				ParameterContainer param2 = inDoorTalkManager.positionTalkImageArray[0].GetComponent<ParameterContainer>();
				inDoorCharacterTalkData = list2.Find((InDoorCharacterTalkData data) => data.accessPointName == PlayerDataManager.currentAccessPointName && data.placeName == PlayerDataManager.currentPlaceName && data.characterName == param2.GetString("characterName"));
				term = GetTalkTerm(inDoorCharacterTalkData);
			}
		}
		inDoorTalkManager.talkBalloonTailRightTerm.Term = term;
		inDoorTalkManager.talkBalloonTailRightGo.SetActive(value: true);
	}

	private string GetTalkTerm(InDoorCharacterTalkData inDoorCharacterTalkData)
	{
		string result = "";
		for (int num = inDoorCharacterTalkData.talkSectionFlagNameList.Count; num > 0; num--)
		{
			if (PlayerFlagDataManager.scenarioFlagDictionary[inDoorCharacterTalkData.talkSectionFlagNameList[num - 1]])
			{
				result = inDoorCharacterTalkData.talkSectionTermList[num - 1];
				break;
			}
		}
		return result;
	}

	public void SetCommandTalkCharacterSprite()
	{
		int index = 0;
		ParameterContainer param = inDoorTalkManager.clickTargetGo.GetComponent<ParameterContainer>();
		InDoorCharacterTalkData inDoorCharacterTalkData = null;
		inDoorCharacterTalkData = ((!(PlayerDataManager.currentPlaceName == "Inn") && !(PlayerDataManager.currentPlaceName == "InnStreet1")) ? ((!(inDoorTalkManager.clickTargetGo.name == "Heroine Image") && !inDoorTalkManager.isFollowRequestApply) ? inDoorTalkManager.inDoorTalkDataBase.inDoorCharacterTalkDataList.Where((InDoorCharacterTalkData data) => data.sortID < 1000).ToList().Find((InDoorCharacterTalkData data) => data.accessPointName == PlayerDataManager.currentAccessPointName && data.placeName == PlayerDataManager.currentPlaceName && data.characterName == param.GetString("characterName")) : inDoorTalkManager.inDoorTalkDataBase.inDoorCharacterTalkDataList.Where((InDoorCharacterTalkData data) => data.sortID > 1000).ToList().Find((InDoorCharacterTalkData data) => data.accessPointName == PlayerDataManager.currentAccessPointName && data.placeName == PlayerDataManager.currentPlaceName && data.characterName == param.GetString("characterName"))) : ((!PlayerDataManager.isDungeonHeroineFollow) ? inDoorTalkManager.inDoorTalkDataBase.inDoorCharacterTalkDataList.Where((InDoorCharacterTalkData data) => data.sortID < 1000).ToList().Find((InDoorCharacterTalkData data) => data.accessPointName == PlayerDataManager.currentAccessPointName && data.placeName == PlayerDataManager.currentPlaceName && data.characterName == param.GetString("characterName")) : inDoorTalkManager.inDoorTalkDataBase.inDoorCharacterTalkDataList.Where((InDoorCharacterTalkData data) => data.sortID > 1000).ToList().Find((InDoorCharacterTalkData data) => data.accessPointName == PlayerDataManager.currentAccessPointName && data.placeName == PlayerDataManager.currentPlaceName && data.characterName == param.GetString("characterName"))));
		for (int num = inDoorCharacterTalkData.talkSectionFlagNameList.Count; num > 0; num--)
		{
			if (PlayerFlagDataManager.scenarioFlagDictionary[inDoorCharacterTalkData.talkSectionFlagNameList[num - 1]])
			{
				_ = inDoorCharacterTalkData.talkSectionTermList[num - 1];
				index = num - 1;
				Debug.Log("会話立ち絵のセクション：" + index);
				break;
			}
		}
		Sprite sprite = null;
		currentCharacterIdleSprite = inDoorTalkManager.clickTargetGoSprite;
		if (inDoorTalkManager.clickTargetGo.name == "Heroine Image")
		{
			Debug.Log("インドア会話立ち絵指定：" + inDoorCharacterTalkData.talkCharacterSpriteList[index].name + "／会話セクション：" + index);
			sprite = inDoorCharacterTalkData.talkCharacterSpriteList[index];
			inDoorTalkManager.positionTalkImageArray[1].GetComponent<Image>().sprite = sprite;
		}
		else if (inDoorCharacterTalkData.talkCharacterSpriteList.Count > 0)
		{
			Debug.Log("インドア会話立ち絵指定：" + inDoorCharacterTalkData.talkCharacterSpriteList[index].name + "／会話セクション：" + index);
			sprite = inDoorCharacterTalkData.talkCharacterSpriteList[index];
			inDoorTalkManager.positionTalkImageArray[0].GetComponent<Image>().sprite = sprite;
			currentCharacterTalkSprite = sprite;
		}
		else
		{
			Debug.Log("インドア会話立ち絵指定なし／会話セクション：" + index);
		}
	}

	public void RevertCurrentClickTargetSprite()
	{
		if (currentTalkData.talkCharacterSpriteList.Count > 0)
		{
			currentTalkGo.GetComponent<Image>().sprite = currentCharacterIdleSprite;
		}
	}

	public bool CheckClickTargetIsFollowHeroine()
	{
		bool result = false;
		if (inDoorTalkManager.clickTargetGo.name == "Heroine Image")
		{
			result = true;
		}
		return result;
	}

	public void RevertForrlowHeroineSprite()
	{
		int index = PlayerDataManager.DungeonHeroineFollowNum - 2;
		Sprite sprite = inDoorTalkManager.inDoorHeroineFollowSpriteDataBase.inDoorHeroineFollowSpriteList[index].followingSpriteDictionary[PlayerDataManager.currentPlaceName];
		inDoorTalkManager.positionTalkImageArray[1].GetComponent<Image>().sprite = sprite;
	}
}
