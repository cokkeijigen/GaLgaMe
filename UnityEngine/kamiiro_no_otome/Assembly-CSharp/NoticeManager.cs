using I2.Loc;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NoticeManager : SerializedMonoBehaviour
{
	public PlayMakerFSM noticeFSM;

	public Localize mapPointTextLoc;

	public Localize pointTextLoc;

	public Localize freeDungeonNameTextLoc;

	public Localize deepDungeonNameTextLoc;

	public Localize enableMapMenuUpperTextLoc;

	public Localize enableMapMenuTextLoc;

	public Localize enableMapMenuNameTextLoc;

	public Localize craftAndExtensionTextLoc;

	public Localize heroineNameTextLoc;

	public Localize allTimeHeroineNameTextLoc;

	public Localize specifyHeroinePointNameTextLoc;

	public Localize specifyHeroineNameTextLoc;

	public Localize specifyHeroineUnFollowPointNameTextLoc;

	public Localize dungeonSexNameTextLoc;

	public Localize sexTouchNameTextLoc;

	public Localize fellatioNameTextLoc;

	public Localize sexBattleNameTextLoc;

	public Localize powerUpNameTextLoc;

	public Localize mergeNameTextLoc;

	public Localize sexStatusViewNameTextLoc;

	public Localize menstruationNameTextLoc;

	public Localize enableFertilizeNameTextLoc;

	public Localize scenarioAllClearNameTextLoc;

	public GameObject noticeCanvasGo;

	public Image blackPanelImage;

	public GameObject dungeonFailedNoticeWindow;

	public GameObject noticeCommonWindow;

	public GameObject mapNoticeGroup;

	public GameObject itemNoticeGroup;

	public GameObject questNoticeGroup;

	public GameObject heroineNoticeGroup;

	public GameObject heroineMoveNoticeGroup;

	public GameObject newMapNoticeGroup;

	public GameObject freeDungeonNoticeGroup;

	public GameObject deepDungeonNoticeGroup;

	public GameObject enableMapMenuNoticeGroup;

	public GameObject enableMapRestNoticeGroup;

	public GameObject craftNoticeGroup;

	public GameObject craftAndExtensionNoticeGroup;

	public GameObject newItemShopNoticeGroup;

	public GameObject newQuestNoticeGroup;

	public GameObject newSubQuestNoticeGroup;

	public GameObject heroineFollowNoticeGroup;

	public GameObject heroineAllTimeFollowNoticeGroup;

	public GameObject heroineSpecifyFollowNoticeGroup;

	public GameObject heroineSpecifyUnFollowNoticeGroup;

	public GameObject dungeonSexNoticeGroup;

	public GameObject sexTouchNoticeGroup;

	public GameObject fellatioNoticeGroup;

	public GameObject sexBattleNoticeGroup;

	public GameObject heroinePowerUpNoticeGroup;

	public GameObject heroineMergeNoticeGroup;

	public GameObject heroineSexStatusViewGroup;

	public GameObject heroineMenstruationStartGroup;

	public GameObject heroineMenstruationViewGroup;

	public GameObject heroineEnableFertilizeGroup;

	public GameObject heroineScenarioAllClearGroup;

	public GameObject heroineSpecifySoloGroup;

	public GameObject heroineSpecifyFollowGroup;

	private void Awake()
	{
		noticeCanvasGo.SetActive(value: false);
	}

	private void CloseAllNoticeWindow()
	{
		mapNoticeGroup.SetActive(value: false);
		itemNoticeGroup.SetActive(value: false);
		questNoticeGroup.SetActive(value: false);
		heroineNoticeGroup.SetActive(value: false);
		dungeonFailedNoticeWindow.SetActive(value: false);
		noticeCommonWindow.SetActive(value: false);
		heroineMoveNoticeGroup.SetActive(value: false);
		newMapNoticeGroup.SetActive(value: false);
		freeDungeonNoticeGroup.SetActive(value: false);
		deepDungeonNoticeGroup.SetActive(value: false);
		enableMapMenuNoticeGroup.SetActive(value: false);
		enableMapRestNoticeGroup.SetActive(value: false);
		craftNoticeGroup.SetActive(value: false);
		craftAndExtensionNoticeGroup.SetActive(value: false);
		newItemShopNoticeGroup.SetActive(value: false);
		newQuestNoticeGroup.SetActive(value: false);
		newSubQuestNoticeGroup.SetActive(value: false);
		heroineFollowNoticeGroup.SetActive(value: false);
		heroineAllTimeFollowNoticeGroup.SetActive(value: false);
		heroineSpecifyFollowNoticeGroup.SetActive(value: false);
		heroineSpecifyUnFollowNoticeGroup.SetActive(value: false);
		heroineEnableFertilizeGroup.SetActive(value: false);
		heroineScenarioAllClearGroup.SetActive(value: false);
		dungeonSexNoticeGroup.SetActive(value: false);
		sexTouchNoticeGroup.SetActive(value: false);
		fellatioNoticeGroup.SetActive(value: false);
		sexBattleNoticeGroup.SetActive(value: false);
		heroinePowerUpNoticeGroup.SetActive(value: false);
		heroineMergeNoticeGroup.SetActive(value: false);
		heroineSexStatusViewGroup.SetActive(value: false);
		heroineMenstruationStartGroup.SetActive(value: false);
		heroineMenstruationViewGroup.SetActive(value: false);
	}

	public bool CheckNoticeFlag()
	{
		bool result = false;
		if (PlayerNonSaveDataManager.isHeroineMoveNotice)
		{
			result = true;
		}
		if (PlayerNonSaveDataManager.isDungeonBattleFailedNotice)
		{
			result = true;
		}
		if (PlayerDataManager.isNewMapNotice)
		{
			result = true;
		}
		if (PlayerNonSaveDataManager.isFreeDungeonNotice)
		{
			result = true;
		}
		if (PlayerNonSaveDataManager.isDeepDungeonNotice)
		{
			result = true;
		}
		if (PlayerNonSaveDataManager.isEnableMapMenuNotice)
		{
			result = true;
		}
		if (PlayerNonSaveDataManager.isHeroineFollowNotice)
		{
			result = true;
		}
		if (PlayerNonSaveDataManager.isHeroineAllTimeFollowNotice)
		{
			result = true;
		}
		if (PlayerNonSaveDataManager.isHeroineSpecifyFollowNotice)
		{
			result = true;
		}
		if (PlayerNonSaveDataManager.isDungeonSexNotice)
		{
			result = true;
		}
		if (PlayerNonSaveDataManager.isSexTouchxNotice)
		{
			result = true;
		}
		if (PlayerNonSaveDataManager.isFellatioNotice)
		{
			result = true;
		}
		if (PlayerNonSaveDataManager.isSexBattleNotice)
		{
			result = true;
		}
		if (PlayerDataManager.isNewRecipeNotice)
		{
			result = true;
		}
		if (PlayerDataManager.isNewCraftAndExtensionNotice)
		{
			result = true;
		}
		if (PlayerNonSaveDataManager.isNewItemShopNotice)
		{
			result = true;
		}
		if (PlayerNonSaveDataManager.isNewStoryQuestNotice)
		{
			result = true;
		}
		if (PlayerNonSaveDataManager.isNewStorySubQuestNotice)
		{
			result = true;
		}
		if (PlayerNonSaveDataManager.isHeroinePowerUpNotice)
		{
			result = true;
		}
		if (PlayerNonSaveDataManager.isHeroineMergeNotice)
		{
			result = true;
		}
		if (PlayerNonSaveDataManager.isHeroineSexStatusViewNotice)
		{
			result = true;
		}
		if (PlayerNonSaveDataManager.isHeroineMenstruationStartNotice)
		{
			result = true;
		}
		if (PlayerNonSaveDataManager.isHeroineMenstruationViewNotice)
		{
			result = true;
		}
		if (PlayerNonSaveDataManager.isHeroineEnableFertilizeNotice)
		{
			result = true;
		}
		if (PlayerNonSaveDataManager.isHeroineScnearioAllClearNotice)
		{
			result = true;
		}
		return result;
	}

	public string CheckNoticeType()
	{
		string text = "";
		if (PlayerNonSaveDataManager.isHeroineMoveNotice)
		{
			return "map";
		}
		if (PlayerNonSaveDataManager.isDungeonBattleFailedNotice)
		{
			return "dungeonFailed";
		}
		if (PlayerDataManager.isNewRecipeNotice)
		{
			return "recipe";
		}
		if (PlayerDataManager.isNewCraftAndExtensionNotice)
		{
			return "recipe";
		}
		if (PlayerDataManager.isNewMapNotice)
		{
			return "map";
		}
		if (PlayerNonSaveDataManager.isFreeDungeonNotice)
		{
			return "freeDungeon";
		}
		if (PlayerNonSaveDataManager.isDeepDungeonNotice)
		{
			return "deepDungeon";
		}
		if (PlayerNonSaveDataManager.isEnableMapMenuNotice)
		{
			return "enableMapMenu";
		}
		if (PlayerNonSaveDataManager.isHeroineFollowNotice)
		{
			return "heroineFollow";
		}
		if (PlayerNonSaveDataManager.isHeroineAllTimeFollowNotice)
		{
			return "heroineAllTimeFollow";
		}
		if (PlayerNonSaveDataManager.isHeroineSpecifyFollowNotice)
		{
			return "heroineSpecifyFollow";
		}
		if (PlayerNonSaveDataManager.isHeroineSpecifyFollowLocalMapNotice)
		{
			return "heroineSpecifyFollow";
		}
		if (PlayerNonSaveDataManager.isDungeonSexNotice)
		{
			return "dungeonSex";
		}
		if (PlayerNonSaveDataManager.isSexTouchxNotice)
		{
			return "sexTouch";
		}
		if (PlayerNonSaveDataManager.isFellatioNotice)
		{
			return "fellatio";
		}
		if (PlayerNonSaveDataManager.isSexBattleNotice)
		{
			return "sexBattle";
		}
		if (PlayerNonSaveDataManager.isHeroinePowerUpNotice)
		{
			return "heroinePowerUp";
		}
		if (PlayerNonSaveDataManager.isHeroineMergeNotice)
		{
			return "heroineMerge";
		}
		if (PlayerNonSaveDataManager.isHeroineSexStatusViewNotice)
		{
			return "sexStatusView";
		}
		if (PlayerNonSaveDataManager.isHeroineMenstruationStartNotice)
		{
			return "menstruationStart";
		}
		if (PlayerNonSaveDataManager.isHeroineMenstruationViewNotice)
		{
			return "menstruationView";
		}
		if (PlayerNonSaveDataManager.isHeroineEnableFertilizeNotice)
		{
			return "fertilize";
		}
		if (PlayerNonSaveDataManager.isHeroineScnearioAllClearNotice)
		{
			return "allClear";
		}
		if (PlayerNonSaveDataManager.isNewItemShopNotice)
		{
			return "newItem";
		}
		if (PlayerNonSaveDataManager.isNewStoryQuestNotice)
		{
			return "quest";
		}
		if (PlayerNonSaveDataManager.isNewStorySubQuestNotice)
		{
			return "quest";
		}
		return "none";
	}

	public void OpenDungeonBattleFailedNotice()
	{
		CloseAllNoticeWindow();
		dungeonFailedNoticeWindow.SetActive(value: true);
		noticeCanvasGo.SetActive(value: true);
	}

	public void OpenCommonNotice()
	{
		CloseAllNoticeWindow();
		if (PlayerDataManager.isNewRecipeNotice)
		{
			itemNoticeGroup.SetActive(value: true);
			craftNoticeGroup.SetActive(value: true);
		}
		if (PlayerDataManager.isNewCraftAndExtensionNotice)
		{
			itemNoticeGroup.SetActive(value: true);
			craftAndExtensionNoticeGroup.SetActive(value: true);
			switch (PlayerNonSaveDataManager.noticeTermString)
			{
			case "recipe":
				craftAndExtensionTextLoc.Term = "noticeNewRecipe2";
				break;
			case "extension":
				craftAndExtensionTextLoc.Term = "noticeNewExtension";
				break;
			case "double":
				craftAndExtensionTextLoc.Term = "noticeNewCraftAndExtension";
				break;
			}
		}
		if (PlayerNonSaveDataManager.isHeroineMoveNotice)
		{
			mapNoticeGroup.SetActive(value: true);
			heroineMoveNoticeGroup.SetActive(value: true);
		}
		if (PlayerDataManager.isNewMapNotice)
		{
			mapPointTextLoc.Term = PlayerDataManager.newMapPointName[0];
			pointTextLoc.Term = PlayerDataManager.newMapPointName[1];
			mapNoticeGroup.SetActive(value: true);
			newMapNoticeGroup.SetActive(value: true);
		}
		if (PlayerNonSaveDataManager.isFreeDungeonNotice)
		{
			freeDungeonNameTextLoc.Term = PlayerNonSaveDataManager.noticeDungeonTermString;
			mapNoticeGroup.SetActive(value: true);
			freeDungeonNoticeGroup.SetActive(value: true);
		}
		if (PlayerNonSaveDataManager.isDeepDungeonNotice)
		{
			deepDungeonNameTextLoc.Term = PlayerNonSaveDataManager.noticeDungeonTermString;
			mapNoticeGroup.SetActive(value: true);
			deepDungeonNoticeGroup.SetActive(value: true);
		}
		if (PlayerNonSaveDataManager.isEnableMapMenuNotice)
		{
			switch (PlayerNonSaveDataManager.noticeTermString)
			{
			case "camp":
				enableMapRestNoticeGroup.SetActive(value: true);
				break;
			case "quest":
				enableMapMenuNoticeGroup.SetActive(value: true);
				enableMapMenuUpperTextLoc.Term = "noticeEnableMenuButton1";
				enableMapMenuNameTextLoc.Term = "noticeEnableQuest";
				enableMapMenuTextLoc.Term = "noticeEnableMenuButton2";
				break;
			case "carriageStore":
				enableMapMenuNoticeGroup.SetActive(value: true);
				enableMapMenuUpperTextLoc.Term = "noticeEnableMenuButton1";
				enableMapMenuNameTextLoc.Term = "noticeEnableCarriageStore";
				enableMapMenuTextLoc.Term = "noticeEnableMenuButton2";
				break;
			}
			mapNoticeGroup.SetActive(value: true);
		}
		if (PlayerNonSaveDataManager.isHeroineFollowNotice)
		{
			heroineNameTextLoc.Term = PlayerNonSaveDataManager.noticeTermString;
			heroineNoticeGroup.SetActive(value: true);
			heroineFollowNoticeGroup.SetActive(value: true);
		}
		if (PlayerNonSaveDataManager.isHeroineAllTimeFollowNotice)
		{
			allTimeHeroineNameTextLoc.Term = PlayerNonSaveDataManager.noticeTermString;
			heroineNoticeGroup.SetActive(value: true);
			heroineAllTimeFollowNoticeGroup.SetActive(value: true);
		}
		if (PlayerNonSaveDataManager.isHeroineSpecifyFollowNotice || PlayerNonSaveDataManager.isHeroineSpecifyFollowLocalMapNotice)
		{
			if (PlayerDataManager.isHeroineSpecifyFollow)
			{
				specifyHeroinePointNameTextLoc.Term = "area" + PlayerDataManager.heroineSpecifyFollowPoint;
				if (PlayerDataManager.heroineSpecifyFollowId == 0)
				{
					heroineSpecifySoloGroup.SetActive(value: true);
					heroineSpecifyFollowGroup.SetActive(value: false);
				}
				else
				{
					heroineSpecifySoloGroup.SetActive(value: false);
					heroineSpecifyFollowGroup.SetActive(value: true);
					specifyHeroineNameTextLoc.Term = "character" + PlayerDataManager.heroineSpecifyFollowId;
				}
				heroineNoticeGroup.SetActive(value: true);
				heroineSpecifyFollowNoticeGroup.SetActive(value: true);
			}
			else
			{
				specifyHeroineUnFollowPointNameTextLoc.Term = "area" + PlayerDataManager.heroineSpecifyFollowPoint;
				heroineNoticeGroup.SetActive(value: true);
				heroineSpecifyUnFollowNoticeGroup.SetActive(value: true);
			}
		}
		if (PlayerNonSaveDataManager.isDungeonSexNotice)
		{
			dungeonSexNameTextLoc.Term = PlayerNonSaveDataManager.noticeTermString;
			heroineNoticeGroup.SetActive(value: true);
			dungeonSexNoticeGroup.SetActive(value: true);
		}
		if (PlayerNonSaveDataManager.isSexTouchxNotice)
		{
			sexTouchNameTextLoc.Term = PlayerNonSaveDataManager.noticeTermString;
			heroineNoticeGroup.SetActive(value: true);
			sexTouchNoticeGroup.SetActive(value: true);
		}
		if (PlayerNonSaveDataManager.isFellatioNotice)
		{
			fellatioNameTextLoc.Term = PlayerNonSaveDataManager.noticeTermString;
			heroineNoticeGroup.SetActive(value: true);
			fellatioNoticeGroup.SetActive(value: true);
		}
		if (PlayerNonSaveDataManager.isSexBattleNotice)
		{
			sexBattleNameTextLoc.Term = PlayerNonSaveDataManager.noticeTermString;
			heroineNoticeGroup.SetActive(value: true);
			sexBattleNoticeGroup.SetActive(value: true);
		}
		if (PlayerNonSaveDataManager.isHeroinePowerUpNotice)
		{
			powerUpNameTextLoc.Term = PlayerNonSaveDataManager.noticeTermString;
			heroineNoticeGroup.SetActive(value: true);
			heroinePowerUpNoticeGroup.SetActive(value: true);
		}
		if (PlayerNonSaveDataManager.isHeroineMergeNotice)
		{
			mergeNameTextLoc.Term = PlayerNonSaveDataManager.noticeTermString;
			heroineNoticeGroup.SetActive(value: true);
			heroineMergeNoticeGroup.SetActive(value: true);
		}
		if (PlayerNonSaveDataManager.isHeroineSexStatusViewNotice)
		{
			sexStatusViewNameTextLoc.Term = PlayerNonSaveDataManager.noticeTermString;
			heroineNoticeGroup.SetActive(value: true);
			heroineSexStatusViewGroup.SetActive(value: true);
		}
		if (PlayerNonSaveDataManager.isHeroineMenstruationStartNotice)
		{
			heroineNoticeGroup.SetActive(value: true);
			heroineMenstruationStartGroup.SetActive(value: true);
		}
		if (PlayerNonSaveDataManager.isHeroineMenstruationViewNotice)
		{
			menstruationNameTextLoc.Term = PlayerNonSaveDataManager.noticeTermString;
			heroineNoticeGroup.SetActive(value: true);
			heroineMenstruationViewGroup.SetActive(value: true);
		}
		if (PlayerNonSaveDataManager.isHeroineEnableFertilizeNotice)
		{
			enableFertilizeNameTextLoc.Term = PlayerNonSaveDataManager.noticeTermString;
			heroineNoticeGroup.SetActive(value: true);
			heroineEnableFertilizeGroup.SetActive(value: true);
		}
		if (PlayerNonSaveDataManager.isHeroineScnearioAllClearNotice)
		{
			scenarioAllClearNameTextLoc.Term = PlayerNonSaveDataManager.noticeTermString;
			heroineNoticeGroup.SetActive(value: true);
			heroineScenarioAllClearGroup.SetActive(value: true);
		}
		if (PlayerNonSaveDataManager.isNewItemShopNotice)
		{
			itemNoticeGroup.SetActive(value: true);
			newItemShopNoticeGroup.SetActive(value: true);
		}
		if (PlayerNonSaveDataManager.isNewStoryQuestNotice)
		{
			questNoticeGroup.SetActive(value: true);
			newQuestNoticeGroup.SetActive(value: true);
		}
		if (PlayerNonSaveDataManager.isNewStorySubQuestNotice)
		{
			questNoticeGroup.SetActive(value: true);
			newSubQuestNoticeGroup.SetActive(value: true);
		}
		noticeCommonWindow.SetActive(value: true);
		noticeCanvasGo.SetActive(value: true);
	}

	public void PushNoticeWindowOkButton(string type)
	{
		if (!(type == "dungeonFailed"))
		{
			if (type == "common")
			{
				PlayerNonSaveDataManager.isHeroineMoveNotice = false;
				PlayerDataManager.isNewMapNotice = false;
				PlayerNonSaveDataManager.isFreeDungeonNotice = false;
				PlayerNonSaveDataManager.isDeepDungeonNotice = false;
				PlayerNonSaveDataManager.isEnableMapMenuNotice = false;
				PlayerDataManager.isNewRecipeNotice = false;
				PlayerDataManager.isNewCraftAndExtensionNotice = false;
				PlayerNonSaveDataManager.isHeroineSexStatusViewNotice = false;
				PlayerNonSaveDataManager.isHeroineMenstruationStartNotice = false;
				PlayerNonSaveDataManager.isHeroineMenstruationViewNotice = false;
				PlayerNonSaveDataManager.isHeroineAllTimeFollowNotice = false;
				PlayerNonSaveDataManager.isHeroineSpecifyFollowNotice = false;
				PlayerNonSaveDataManager.isHeroineSpecifyFollowLocalMapNotice = false;
				PlayerNonSaveDataManager.isHeroineEnableFertilizeNotice = false;
				PlayerNonSaveDataManager.isHeroineScnearioAllClearNotice = false;
				PlayerNonSaveDataManager.isDungeonSexNotice = false;
				PlayerNonSaveDataManager.isSexTouchxNotice = false;
				PlayerNonSaveDataManager.isFellatioNotice = false;
				PlayerNonSaveDataManager.isSexBattleNotice = false;
				PlayerNonSaveDataManager.isHeroinePowerUpNotice = false;
				PlayerNonSaveDataManager.isHeroineMergeNotice = false;
				PlayerNonSaveDataManager.isNewItemShopNotice = false;
				PlayerNonSaveDataManager.isNewStoryQuestNotice = false;
				PlayerNonSaveDataManager.isNewStorySubQuestNotice = false;
				if (PlayerNonSaveDataManager.isHeroineFollowNotice)
				{
					PlayerNonSaveDataManager.isHeroineFollowNotice = false;
					if (!PlayerFlagDataManager.tutorialFlagDictionary["heroineFollow"])
					{
						PlayerNonSaveDataManager.isHeroineFollowTutorial = true;
						PlayerNonSaveDataManager.selectTutorialName = "heroineFollow";
					}
				}
			}
		}
		else
		{
			PlayerNonSaveDataManager.isDungeonBattleFailedNotice = false;
		}
		noticeFSM.SendEvent("PushNoticeOkButton");
	}

	public bool CheckHeroineFollowTutorial()
	{
		return PlayerNonSaveDataManager.isHeroineFollowTutorial;
	}

	public void OpenHeroineFollowTutorial()
	{
		PlayerNonSaveDataManager.isHeroineFollowTutorial = false;
		PlayerFlagDataManager.tutorialFlagDictionary["heroineFollow"] = true;
		Debug.Log("ヒロイン同行可のチュートリアル開始");
		SceneManager.LoadSceneAsync("tutorialUI", LoadSceneMode.Additive);
	}

	public void EndHeroineFollowTutorial()
	{
		noticeFSM.SendEvent("EndHeroineFollowTutorial");
	}

	public bool CheckDeepDungeonClearNotice()
	{
		return PlayerNonSaveDataManager.isDungeonDeepClear;
	}
}
