using System.Collections.Generic;
using System.Linq;
using Arbor;
using Coffee.UIExtensions;
using HutongGames.PlayMaker;
using I2.Loc;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SexTouchManager : SerializedMonoBehaviour
{
	public SexTouchStatusManager sexTouchStatusManager;

	public PlayMakerFSM sexTouchCancelManagerFSM;

	public ArborFSM sexTouchArborFSM;

	public ArborFSM sexBattleArborFSM;

	public SexTouchBgDataBase sexTouchBgDataBase;

	public GameObject touchCanvas;

	public CanvasGroup touchCanvasGroup;

	public GameObject battleCanvas;

	public GameObject insertCanvas;

	public GameObject dialogCanvas;

	public Localize dialogLocText;

	public Localize menstrualNameDialogLocText;

	public GameObject menstrualDialogTextGroup;

	public GameObject dialogTouchButtonGroup;

	public GameObject dialogBattleButtonGroup;

	public GameObject dialogMenstrualButtonGroup;

	public GameObject dialogCumShotButtonGroup;

	public GameObject dialogHeroineVoiceToggleGroup;

	public Toggle dialogHeroineVoiceToggle;

	public Button[] bodyCategoryButtonArray;

	public int selectBodyCategoryNum;

	public Button[] commandButtonArray;

	public Button menuButton;

	public GameObject textVisibleButtonGo;

	public GameObject touchAreaGroupParentGo;

	public GameObject[] touchAreaPointGoArray;

	public int sexTouchModeNum;

	public string clickSelectAreaPointName;

	public int clickSelectAreaPointIndex;

	public string beforeSelectAreaPointName;

	public int beforeSelectAreaPointIndex;

	public Texture2D touchAreaHandTexture;

	public GameObject guideButton;

	public bool isTouchAreaGuideVisible;

	public GameObject touchSeButton;

	public bool isTouchSePlaying;

	public GameObject inputInfoFrame;

	public GameObject cumShotInfoFrame;

	public GameObject skipInfoFrame;

	public GameObject bodyClickInfoWindowGo;

	public Localize bodyClickInfoHeaderLocText;

	public Localize bodyClickSummaryLocText;

	public GameObject[] bodyHistoryGroupGoArray;

	public Button bodyHistoryNextArrownButton;

	public Button bodyHistoryBeforeArrowButton;

	public GameObject[] heroineSkillWindowArray;

	public Localize[] skillGroupHeaderTextLocArray;

	public GameObject[] skillGroupHeaderHideGroupArray;

	public Transform[] skillGroupPrefabParentArray;

	public Image[] skillWindowButtonImageArray;

	public Sprite[] skillWindowButtonSpriteArray;

	public bool[] skillWindowIsCloseArray;

	public Localize insertTextLoc;

	public Localize cumShotTextLoc;

	public GameObject heroinePassiveSkillFramePrefabGo;

	public GameObject heroineActiveSkillFramePrefabGo;

	public GameObject[] heroineClickHeartPrefabGoArray;

	public Transform prefabParentTransform;

	public GameObject touchExpPrefabGo;

	public Transform touchExpPrefabSpawnParentGo;

	public List<Transform> touchExpPrefabSpawnGoList;

	public UIParticle uIParticleTouch;

	public GameObject resultEffectPrefabGo;

	public Transform resultEffectSpawnGo;

	public GameObject touchHeroineSpriteGo;

	public SpriteRenderer touchHeroineBeforeSprite;

	public GameObject touchBgSpriteGo;

	public CanvasGroup touchWhiteImageCanvasGroup;

	public GameObject touchResultWindow;

	public Text defaultKizunaText;

	public Text getKizunaText;

	public Text getExpText;

	[Readonly]
	public int selectBodyLv;

	[Readonly]
	public int currentSexSkillLv;

	public List<Slider> touchExpSliderList;

	public List<TextMeshProUGUI> touchLvTextList;

	public bool isTouchResultAnimationEnd;

	private void Awake()
	{
		touchCanvas.SetActive(value: false);
		battleCanvas.SetActive(value: false);
		insertCanvas.SetActive(value: false);
	}

	public void SetBodyCategoryButtonInteractable()
	{
		Button[] array = bodyCategoryButtonArray;
		for (int i = 0; i < array.Length; i++)
		{
			CanvasGroup component = array[i].GetComponent<CanvasGroup>();
			component.alpha = 1f;
			component.interactable = true;
		}
		CanvasGroup component2 = bodyCategoryButtonArray[selectBodyCategoryNum].GetComponent<CanvasGroup>();
		component2.alpha = 0.6f;
		component2.interactable = false;
	}

	public void SetSelectBodyCategoryNum(int num)
	{
		selectBodyCategoryNum = num;
		sexTouchArborFSM.SendTrigger("ChangeTouchBodyCategory");
	}

	public int GetSelectBodyCategoryNum()
	{
		return selectBodyCategoryNum;
	}

	public void SetSelectAreaPointName(string name, int index)
	{
		clickSelectAreaPointName = name;
		clickSelectAreaPointIndex = index;
	}

	public void CloseTouchInfo()
	{
		bodyClickInfoWindowGo.SetActive(value: false);
		beforeSelectAreaPointName = "";
	}

	public void SetBodyAreaPoint()
	{
		GameObject[] array = touchAreaPointGoArray;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
		switch (selectBodyCategoryNum)
		{
		case 0:
		{
			int k;
			for (k = 0; k < 1; k++)
			{
				SexTouchClickData sexTouchClickData2 = GameDataManager.instance.sexTouchClickDataBase.sexTouchClickDataList.Where((SexTouchClickData data) => data.bodyCategory == SexTouchClickData.BodyCategory.head && data.characterID == PlayerNonSaveDataManager.selectSexBattleHeroineId).ToList().Find((SexTouchClickData data) => data.areaIndex == k + 1);
				touchAreaPointGoArray[k].GetComponent<RectTransform>().anchoredPosition = sexTouchClickData2.areaPointVector2;
				touchAreaPointGoArray[k].GetComponent<ParameterContainer>().SetString("areaPointName", sexTouchClickData2.areaName);
				touchAreaPointGoArray[k].GetComponent<ParameterContainer>().SetInt("areaPointIndex", sexTouchClickData2.areaIndex);
				touchAreaPointGoArray[k].SetActive(value: true);
			}
			break;
		}
		case 1:
		{
			int l;
			for (l = 0; l < 4; l++)
			{
				SexTouchClickData sexTouchClickData3 = GameDataManager.instance.sexTouchClickDataBase.sexTouchClickDataList.Where((SexTouchClickData data) => data.bodyCategory == SexTouchClickData.BodyCategory.upperBody && data.characterID == PlayerNonSaveDataManager.selectSexBattleHeroineId).ToList().Find((SexTouchClickData data) => data.areaIndex == l + 1);
				touchAreaPointGoArray[l].GetComponent<RectTransform>().anchoredPosition = sexTouchClickData3.areaPointVector2;
				touchAreaPointGoArray[l].GetComponent<ParameterContainer>().SetString("areaPointName", sexTouchClickData3.areaName);
				touchAreaPointGoArray[l].GetComponent<ParameterContainer>().SetInt("areaPointIndex", sexTouchClickData3.areaIndex);
				touchAreaPointGoArray[l].SetActive(value: true);
			}
			break;
		}
		case 2:
		{
			int j;
			for (j = 0; j < 3; j++)
			{
				SexTouchClickData sexTouchClickData = GameDataManager.instance.sexTouchClickDataBase.sexTouchClickDataList.Where((SexTouchClickData data) => data.bodyCategory == SexTouchClickData.BodyCategory.lowerBody && data.characterID == PlayerNonSaveDataManager.selectSexBattleHeroineId).ToList().Find((SexTouchClickData data) => data.areaIndex == j + 1);
				touchAreaPointGoArray[j].GetComponent<RectTransform>().anchoredPosition = sexTouchClickData.areaPointVector2;
				touchAreaPointGoArray[j].GetComponent<ParameterContainer>().SetString("areaPointName", sexTouchClickData.areaName);
				touchAreaPointGoArray[j].GetComponent<ParameterContainer>().SetInt("areaPointIndex", sexTouchClickData.areaIndex);
				touchAreaPointGoArray[j].SetActive(value: true);
			}
			break;
		}
		}
		touchAreaGroupParentGo.GetComponent<PlayMakerFSM>().SendEvent("TouchAreaAnimationStart");
	}

	public void SetTouchAreaGuidVisible(bool value)
	{
		isTouchAreaGuideVisible = value;
	}

	public bool GetTouchAreaGuidVisible()
	{
		return isTouchAreaGuideVisible;
	}

	public void SetTouchSePlaying(bool value)
	{
		isTouchSePlaying = value;
	}

	public bool GetTouchSePlaying()
	{
		return isTouchSePlaying;
	}

	public void PushStartCumShotButton()
	{
		dialogLocText.gameObject.SetActive(value: true);
		menstrualDialogTextGroup.SetActive(value: false);
		dialogLocText.Term = "alertStartSexCumShot";
		dialogTouchButtonGroup.SetActive(value: false);
		dialogBattleButtonGroup.SetActive(value: false);
		dialogMenstrualButtonGroup.SetActive(value: false);
		dialogCumShotButtonGroup.SetActive(value: true);
		if (PlayerSexStatusDataManager.heroineSexLv[PlayerNonSaveDataManager.selectSexBattleHeroineId - 1] < 4)
		{
			PlayerDataManager.isHeroineSexVoiceLowStage = false;
			dialogHeroineVoiceToggleGroup.SetActive(value: false);
		}
		else
		{
			dialogHeroineVoiceToggleGroup.SetActive(value: true);
			dialogHeroineVoiceToggle.isOn = PlayerDataManager.isHeroineSexVoiceLowStage;
		}
		dialogCanvas.SetActive(value: true);
	}

	public void PushStartSexButton()
	{
		dialogLocText.gameObject.SetActive(value: true);
		menstrualDialogTextGroup.SetActive(value: false);
		dialogLocText.Term = "alertStartSexBattle";
		dialogTouchButtonGroup.SetActive(value: false);
		dialogBattleButtonGroup.SetActive(value: true);
		dialogMenstrualButtonGroup.SetActive(value: false);
		dialogCumShotButtonGroup.SetActive(value: false);
		if (PlayerSexStatusDataManager.heroineSexLv[PlayerNonSaveDataManager.selectSexBattleHeroineId - 1] < 4)
		{
			PlayerDataManager.isHeroineSexVoiceLowStage = false;
			dialogHeroineVoiceToggleGroup.SetActive(value: false);
		}
		else
		{
			dialogHeroineVoiceToggleGroup.SetActive(value: true);
			dialogHeroineVoiceToggle.isOn = PlayerDataManager.isHeroineSexVoiceLowStage;
		}
		dialogCanvas.SetActive(value: true);
	}

	public void PushHeroineVoiceToggle()
	{
		PlayerDataManager.isHeroineSexVoiceLowStage = dialogHeroineVoiceToggle.isOn;
		Debug.Log("低LVステージのトグル：" + dialogHeroineVoiceToggle.isOn);
	}

	public void PushSexDialogOkButton()
	{
		if (PlayerNonSaveDataManager.isSexHeroineMenstrualDay)
		{
			CharacterStatusData characterStatusData = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == PlayerNonSaveDataManager.selectSexBattleHeroineId);
			if (PlayerFlagDataManager.scenarioFlagDictionary[characterStatusData.characterFertilizationFlag])
			{
				dialogLocText.gameObject.SetActive(value: false);
				menstrualDialogTextGroup.SetActive(value: true);
				menstrualNameDialogLocText.Term = "character" + PlayerDataManager.DungeonHeroineFollowNum;
				dialogTouchButtonGroup.SetActive(value: false);
				dialogBattleButtonGroup.SetActive(value: false);
				dialogMenstrualButtonGroup.SetActive(value: true);
				dialogCumShotButtonGroup.SetActive(value: false);
			}
			else
			{
				PlayerNonSaveDataManager.isMenstrualDaySexUseCondom = true;
				sexBattleArborFSM.SendTrigger("StartSexBattle");
				dialogCanvas.SetActive(value: false);
			}
		}
		else
		{
			PlayerNonSaveDataManager.isMenstrualDaySexUseCondom = false;
			sexBattleArborFSM.SendTrigger("StartSexBattle");
			dialogCanvas.SetActive(value: false);
		}
	}

	public void PushMenstrualDialogOkButton(bool value)
	{
		PlayerNonSaveDataManager.isMenstrualDaySexUseCondom = value;
		sexBattleArborFSM.SendTrigger("StartSexBattle");
		dialogCanvas.SetActive(value: false);
	}

	public void PushBodyHistoryArrowButton(bool isNext)
	{
		if (isNext)
		{
			currentSexSkillLv++;
		}
		else
		{
			currentSexSkillLv--;
		}
		sexTouchArborFSM.SendTrigger("ChangeBodyHistory");
	}

	public void RefreshBodyHistoryGroup()
	{
		if (selectBodyLv >= 2 && clickSelectAreaPointName != "tits")
		{
			bodyHistoryBeforeArrowButton.interactable = true;
			bodyHistoryNextArrownButton.interactable = true;
			bodyHistoryBeforeArrowButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
			bodyHistoryNextArrownButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
			if (currentSexSkillLv == 1)
			{
				bodyHistoryBeforeArrowButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
				bodyHistoryBeforeArrowButton.interactable = false;
			}
			if (currentSexSkillLv == selectBodyLv || currentSexSkillLv == 3)
			{
				bodyHistoryNextArrownButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
				bodyHistoryNextArrownButton.interactable = false;
			}
			for (int i = 0; i < bodyHistoryGroupGoArray.Length; i++)
			{
				bodyHistoryGroupGoArray[i].SetActive(value: true);
			}
			bodyClickInfoWindowGo.GetComponent<RectTransform>().sizeDelta = new Vector2(380f, 280f);
		}
		else
		{
			for (int j = 0; j < bodyHistoryGroupGoArray.Length; j++)
			{
				bodyHistoryGroupGoArray[j].SetActive(value: false);
			}
			bodyClickInfoWindowGo.GetComponent<RectTransform>().sizeDelta = new Vector2(380f, 214f);
		}
	}

	public void SetTouchTextVisible(bool value)
	{
		PlayerDataManager.isHeroineSexTouchTextVisible = value;
	}

	public bool GetTouchTextVisible()
	{
		return PlayerDataManager.isHeroineSexTouchTextVisible;
	}
}
