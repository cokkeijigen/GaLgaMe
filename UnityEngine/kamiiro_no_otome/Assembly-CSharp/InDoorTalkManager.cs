using System.Collections.Generic;
using Arbor;
using I2.Loc;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class InDoorTalkManager : SerializedMonoBehaviour
{
	public InDoorCommandManager inDoorCommandManager;

	public ArborFSM talkFSM;

	public ArborFSM commandFSM;

	public ArborFSM cancelFSM;

	public PlayMakerFSM eventCheckFSM;

	public LocalMapUnlockDataBase localMapUnlockDataBase;

	public InDoorCharacterCgDataBase inDoorCharacterCgDataBase;

	public InDoorTalkDataBase inDoorTalkDataBase;

	public InDoorHeroineFollowSpriteDataBase inDoorHeroineFollowSpriteDataBase;

	public InDoorHeroineTalkSpriteDataBase inDoorHeroineTalkSpriteDataBase;

	public GameObject inDoorCanvasGo;

	public Image inDoorBgImage;

	public GameObject carriageBgGroup;

	public Button FullScreenTextButton;

	public GameObject[] positionFarImageArray;

	public GameObject[] positionMiddleImageArray;

	public GameObject[] positionNearImageArray;

	public GameObject[] positionTalkImageArray;

	public GameObject[] positionParrentGoArray;

	public GameObject commandTalkGroup;

	public GameObject heroineTalkGroup;

	public GameObject farAlertGroupGo;

	public GameObject middleAlertGroupGo;

	public GameObject nearAlertGroupGo;

	public GameObject talkAlertGroupGo;

	public GameObject heroineAlertGroupGo;

	public GameObject[] farAlertGoArray;

	public GameObject[] middleAlertGoArray;

	public GameObject[] nearAlertGoArray;

	public GameObject[] talkAlertGoArray;

	public GameObject[] heroineAlertGoArray;

	public GameObject talkBalloonTailRightGo;

	public GameObject talkBalloonTailLeftGo;

	public GameObject talkBalloonHeroineGo;

	public Localize talkBalloonTailRightTerm;

	public Localize talkBalloonTailLeftTerm;

	public Localize talkBalloonHeroineTerm;

	public GameObject commandButtonGroupGo;

	public Localize commandFollowButtonLoc;

	public Image commandFollowButtonImage;

	public Sprite[] commandFollowButtonSpriteArray;

	public Dictionary<string, GameObject> commandButtonDictionary = new Dictionary<string, GameObject>();

	public CanvasGroup exitButtonCanvasGroup;

	public Localize exitButtonTextLoc;

	public GameObject alertFollowFrameGo;

	public Localize alertFollowTextLoc;

	public GameObject alertRestFrameGo;

	public GameObject alertTalkEventFrameGo;

	public GameObject clickSummaryGo;

	public GameObject clickTargetGo;

	public GameObject commandTalkOriginGo;

	public InDoorCharacterLocationData currentInDoorLocationData;

	public List<InDoorLocationData> currentInDoorLocationDataList;

	public Sprite clickTargetGoSprite;

	public GameObject checkTargetGo;

	public InDoorCharacterTalkData eventCheckTalkData;

	public int currentInDoorLocationCheckCount;

	public bool isInitializeCommandTalk;

	public bool isShopBuy;

	public bool isFollowRequest;

	public bool isFollowRequestApply;

	public int restTimeZoneNum;

	private void Awake()
	{
		inDoorCanvasGo.SetActive(value: false);
	}

	public void ResetInDoorCanvas()
	{
		commandButtonGroupGo.SetActive(value: false);
		FullScreenTextButton.interactable = true;
		inDoorCommandManager.blackImageGo.SetActive(value: false);
		inDoorCommandManager.inDoorDialogGroupGo.SetActive(value: false);
		inDoorCommandManager.restDialogGroupGo.SetActive(value: false);
		commandButtonGroupGo.SetActive(value: false);
		commandButtonGroupGo.GetComponent<CanvasGroup>().interactable = true;
		alertFollowFrameGo.SetActive(value: false);
		alertRestFrameGo.SetActive(value: false);
		alertTalkEventFrameGo.SetActive(value: false);
		talkBalloonTailLeftGo.SetActive(value: false);
		talkBalloonTailRightGo.SetActive(value: false);
		talkBalloonHeroineGo.SetActive(value: false);
		GameObject[] array = positionFarImageArray;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
		array = positionMiddleImageArray;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
		array = positionNearImageArray;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
		array = positionTalkImageArray;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
		farAlertGroupGo.SetActive(value: false);
		middleAlertGroupGo.SetActive(value: false);
		nearAlertGroupGo.SetActive(value: false);
		talkAlertGroupGo.SetActive(value: false);
		heroineAlertGroupGo.SetActive(value: false);
		array = farAlertGoArray;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].GetComponent<Image>().enabled = true;
		}
		array = middleAlertGoArray;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].GetComponent<Image>().enabled = true;
		}
		array = nearAlertGoArray;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].GetComponent<Image>().enabled = true;
		}
		array = talkAlertGoArray;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].GetComponent<Image>().enabled = true;
		}
		array = heroineAlertGoArray;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].GetComponent<Image>().enabled = true;
		}
		exitButtonCanvasGroup.interactable = true;
		exitButtonCanvasGroup.alpha = 1f;
		clickTargetGo = null;
		commandTalkOriginGo = null;
		positionTalkImageArray[0].GetComponent<ParameterContainer>().SetGameObject("clickOriginGo", null);
		isInitializeCommandTalk = false;
		isFollowRequestApply = false;
		PlayerNonSaveDataManager.isUsedShop = false;
		PlayerDataManager.mapPlaceStatusNum = 2;
		PlayerNonSaveDataManager.openUiScreenLevel = 2;
		PlayerNonSaveDataManager.inDoorAllTalkDictionary.Clear();
		if (PlayerNonSaveDataManager.isRequiedUtageResume)
		{
			exitButtonTextLoc.Term = "buttonInDoorEnd";
			exitButtonCanvasGroup.gameObject.SetActive(value: false);
		}
		else if (PlayerNonSaveDataManager.isWorldMapToInDoor)
		{
			exitButtonTextLoc.Term = "buttonGoToWorldMap";
			exitButtonCanvasGroup.gameObject.SetActive(value: true);
		}
		else
		{
			exitButtonTextLoc.Term = "buttonGoToTown";
			exitButtonCanvasGroup.gameObject.SetActive(value: true);
		}
	}

	public int GetThisPlaceInHeroineId(InDoorLocationData locationData)
	{
		int num = 0;
		switch (locationData.talkCharacterName)
		{
		case "ルーシー":
			return 1;
		case "リィナ":
			return 2;
		case "シア":
			return 3;
		case "レヴィ":
			return 4;
		default:
			return 0;
		}
	}

	public bool GetScnearioClearFlag(string scenarioName)
	{
		return PlayerFlagDataManager.scenarioFlagDictionary[scenarioName];
	}

	public string GetInitializeCharacterName()
	{
		return eventCheckTalkData.characterName;
	}

	public void SetBalloonActive(bool value, string scenarioName, bool isInitialize)
	{
		ParameterContainer component = checkTargetGo.GetComponent<ParameterContainer>();
		if (value)
		{
			if (GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Find((ScenarioFlagData data) => data.scenarioName == scenarioName).isSexEvent)
			{
				component.GetGameObject("sexBalloon").GetComponent<Image>().enabled = true;
				component.GetGameObject("eventBalloon").GetComponent<Image>().enabled = false;
				component.GetStringList("scenarioNameList")[0] = scenarioName;
				component.GetStringList("scenarioNameList")[1] = "";
			}
			else
			{
				component.GetGameObject("sexBalloon").GetComponent<Image>().enabled = false;
				component.GetGameObject("eventBalloon").GetComponent<Image>().enabled = true;
				component.GetStringList("scenarioNameList")[0] = "";
				component.GetStringList("scenarioNameList")[1] = scenarioName;
			}
			component.SetBool("isCommandTalk", value: true);
			component.GetGameObject("alertGroupGo").SetActive(value: true);
		}
		else
		{
			component.GetGameObject("alertGroupGo").SetActive(value: false);
			component.GetStringList("scenarioNameList")[0] = "";
			component.GetStringList("scenarioNameList")[1] = "";
		}
		if (!isInitialize)
		{
			return;
		}
		int thisPlaceInHeroineId = GetThisPlaceInHeroineId(currentInDoorLocationDataList[currentInDoorLocationCheckCount]);
		if (PlayerDataManager.DungeonHeroineFollowNum == thisPlaceInHeroineId && PlayerDataManager.isDungeonHeroineFollow)
		{
			Debug.Log("居場所のヒロインは同行中");
			GameObject gameObject = null;
			switch (currentInDoorLocationDataList[currentInDoorLocationCheckCount].talkPositionName)
			{
			case "近_左":
				gameObject = positionNearImageArray[0];
				break;
			case "近_右":
				gameObject = positionNearImageArray[1];
				break;
			case "中_左":
				gameObject = positionMiddleImageArray[0];
				break;
			case "中_右":
				gameObject = positionMiddleImageArray[1];
				break;
			case "奥_左":
				gameObject = positionFarImageArray[0];
				break;
			case "奥_右":
				gameObject = positionFarImageArray[1];
				break;
			}
			gameObject.SetActive(value: false);
			component.GetGameObject("alertGroupGo").SetActive(value: false);
			component.GetStringList("scenarioNameList")[0] = "";
			component.GetStringList("scenarioNameList")[1] = "";
		}
		currentInDoorLocationCheckCount++;
		talkFSM.SendTrigger("LocationCheckEnd");
		Debug.Log("イベントチェック完了Index：" + currentInDoorLocationCheckCount);
	}

	public void CheckScnerioInDoorAllTalk()
	{
		bool flag = false;
		foreach (KeyValuePair<int, bool> item in PlayerNonSaveDataManager.inDoorAllTalkDictionary)
		{
			if (!item.Value)
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			exitButtonCanvasGroup.gameObject.SetActive(value: true);
		}
	}

	public void SetExitButtonFsmEnable(bool value)
	{
		cancelFSM.enabled = value;
	}

	public void SetInDoorEventScenarioName(int index)
	{
		string text = "";
		int num = 0;
		num = ((!positionTalkImageArray[0].activeInHierarchy) ? 1 : 0);
		ParameterContainer component = positionTalkImageArray[num].GetComponent<ParameterContainer>();
		int @int = component.GetInt("sortID");
		text = component.GetStringList("scenarioNameList")[index];
		string text2 = text.Substring(0, 6);
		Debug.Log("ジャンプするシナリオ名の前半部分：" + text2);
		if (text2 == "H_Lucy" && PlayerOptionsDataManager.isLucyVoiceTypeSexy)
		{
			text += "_sexy";
		}
		PlayerNonSaveDataManager.selectScenarioName = text;
		Debug.Log("ジャンプ先は：" + text);
		if (GameObject.Find("AdvEngine") != null && !PlayerNonSaveDataManager.inDoorAllTalkDictionary[@int])
		{
			PlayerNonSaveDataManager.inDoorAllTalkDictionary[@int] = true;
		}
	}

	public void SetRestTimeZoneNum(int index)
	{
		restTimeZoneNum = inDoorCommandManager.restDialogGroupGo.GetComponent<ParameterContainer>().GetIntList("restTimeNumList")[index];
		GameObject.Find("InDoor Rest Manager").GetComponent<ArborFSM>().SendTrigger("PushRestButton");
	}

	public void AfterReserveHeroineUnFollow()
	{
		PlayerNonSaveDataManager.isHeroineUnFollowReserveAtLocalMap = false;
		talkFSM.SendTrigger("AfterHeroineUnFollow");
	}

	public void CheckHeroineSpecifyAlertOpen(GameObject buttonGo)
	{
		if (PlayerDataManager.isHeroineSpecifyFollow && PlayerDataManager.currentAccessPointName == PlayerDataManager.heroineSpecifyFollowPoint)
		{
			if (isFollowRequest)
			{
				alertFollowTextLoc.Term = "alertHeroineCannotFollow";
			}
			else
			{
				alertFollowTextLoc.Term = "alertHeroineCannotFollowRelease";
			}
			alertFollowFrameGo.SetActive(value: true);
		}
	}

	public void CheckRestAlertOpen()
	{
		if (PlayerDataManager.isLocalMapActionLimit)
		{
			alertRestFrameGo.SetActive(value: true);
		}
	}

	public void CheckTalkEventAlertOpen()
	{
		if (clickTargetGo.GetComponent<ParameterContainer>().GetBool("isDisableTalkEvent"))
		{
			alertTalkEventFrameGo.SetActive(value: true);
		}
	}
}
