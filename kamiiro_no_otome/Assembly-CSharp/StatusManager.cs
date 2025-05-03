using System.Collections.Generic;
using Arbor;
using I2.Loc;
using PathologicalGames;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : SerializedMonoBehaviour
{
	private StatusCustomManager statusCustomManager;

	private StatusSkillViewManager statusSkillViewManager;

	private SexStatusManager sexStatusManager;

	public ArborFSM statusFSM;

	public ArborFSM skillFSM;

	public ArborFSM sexStatusFSM;

	public PlayMakerFSM namePanelFSM;

	public GameObject[] statusCanvasArray;

	public Image[] menuButtonArray;

	public Sprite[] menuButtonSpriteArray;

	public GameObject[] itemViewArray;

	public GameObject[] skillViewArray;

	public GameObject[] itemCategoryTabGoArray;

	public Image[] equipItemCategoryTabImageArray;

	public Image[] itemCategoryTabImageArray;

	public Sprite[] categoryTabSpriteArray;

	public GameObject[] scrollContentPrefabArray;

	public GameObject itemContentGO;

	public GameObject skillContentGO;

	public GameObject poolParentGO;

	public RectTransform itemSelectWIndowScrollView;

	public RectTransform skillSelectWIndowScrollView;

	public GameObject itemEquipButtonGO;

	public Localize itemEquipButtonLoc;

	public GameObject[] headerTextGoArray;

	public Localize headerCategoryTextLoc;

	public GameObject[] itemScrollSummaryGoArray;

	public Localize powerTextLoc;

	public Localize mpTextLoc;

	public GameObject itemSummmaryWindow;

	public GameObject skillSummaryWindow;

	public GameObject[] itemCustomTabGroupArray;

	public GameObject factorEquipButtonGo;

	public Sprite noItemImageSprite;

	public GameObject[] characterButtonArray;

	public GameObject[] statusPanelArray;

	public Text[] characterStatusTextArray;

	public Sprite[] characterSpriteArray;

	public Sprite[] characterBgSpriteArray;

	public Image characterImage;

	public Image characterBackGroundImage;

	public Localize characterNameTextLoc;

	public int selectItemCategoryNum;

	public int selectItemScrollContentIndex;

	public Sprite[] selectScrollContentSpriteArray;

	public int selectSkillScrollContentIndex;

	public int selectCharacterNum;

	public int selectCharacterListIndex;

	public int[] selectItemIdArray = new int[2];

	public int selectItemId;

	public int selectItemUniqueId;

	public int selectSkillId;

	public List<int> characterJoinIdList;

	public List<int> characterSexUnlockIdList;

	public bool isEquipedItemSummaryShow;

	private void Awake()
	{
		statusCustomManager = GameObject.Find("Status Custom Manager").GetComponentInParent<StatusCustomManager>();
		statusSkillViewManager = GameObject.Find("Skill View Manager").GetComponent<StatusSkillViewManager>();
		sexStatusManager = GameObject.Find("Sex Status Manager").GetComponent<SexStatusManager>();
		statusCanvasArray[0].SetActive(value: false);
		statusCanvasArray[1].SetActive(value: false);
		statusCustomManager.statusOverlayCanvas.SetActive(value: false);
		selectItemCategoryNum = 7;
	}

	public void SetCharacterButtonVisible()
	{
		for (int i = 0; i < 5; i++)
		{
			string characterDungeonFollowUnLockFlag = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[i].characterDungeonFollowUnLockFlag;
			bool active = PlayerFlagDataManager.scenarioFlagDictionary[characterDungeonFollowUnLockFlag];
			characterButtonArray[i].SetActive(active);
		}
	}

	public void ChangeItemCategory(int num)
	{
		selectItemCategoryNum = num;
		isEquipedItemSummaryShow = false;
		statusFSM.SendTrigger("ChangeItemCategory");
	}

	public void PushCharacterChangeArrowButton(bool isAdd)
	{
		if (isAdd)
		{
			selectCharacterListIndex++;
		}
		else
		{
			selectCharacterListIndex--;
		}
		if (sexStatusManager.sexStatusViewArray[0].activeInHierarchy)
		{
			if (selectCharacterListIndex >= characterSexUnlockIdList.Count)
			{
				selectCharacterListIndex = 0;
			}
			else if (selectCharacterListIndex < 0)
			{
				selectCharacterListIndex = characterSexUnlockIdList.Count - 1;
			}
			selectCharacterNum = characterSexUnlockIdList[selectCharacterListIndex];
		}
		else
		{
			if (selectCharacterListIndex >= characterJoinIdList.Count)
			{
				selectCharacterListIndex = 0;
			}
			else if (selectCharacterListIndex < 0)
			{
				selectCharacterListIndex = characterJoinIdList.Count - 1;
			}
			selectCharacterNum = characterJoinIdList[selectCharacterListIndex];
		}
		ChangeCharacterNum(selectCharacterNum);
	}

	public void ChangeCharacterNum(int num)
	{
		selectCharacterNum = num;
		Debug.Log("キャラ選択：" + selectCharacterNum);
		switch (PlayerNonSaveDataManager.selectStatusCanvasName)
		{
		case "equipItem":
			statusFSM.SendTrigger("ChangeItemCategory");
			break;
		case "item":
			statusFSM.SendTrigger("ChangeCharacter");
			break;
		case "skill":
			statusSkillViewManager.isSelectSkillLearnTab = false;
			skillFSM.SendTrigger("ChangeCharacter");
			break;
		case "sexStatus":
			sexStatusManager.PushSexSkillCharacterTab(num);
			break;
		}
	}

	public bool CheckCharacterFollowFlag(int index)
	{
		string characterDungeonFollowUnLockFlag = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[index].characterDungeonFollowUnLockFlag;
		return PlayerFlagDataManager.scenarioFlagDictionary[characterDungeonFollowUnLockFlag];
	}

	public bool CheckOpenSexStatus()
	{
		bool result = false;
		if (PlayerNonSaveDataManager.selectStatusCanvasName == "sexStatus")
		{
			result = true;
		}
		return result;
	}

	public bool CheckCharacterSexUnLock(int index)
	{
		CharacterStatusData characterStatusData = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == index);
		return PlayerFlagDataManager.scenarioFlagDictionary[characterStatusData.characterDungeonSexUnLockFlag];
	}

	public int GetSelectCharacterNum()
	{
		return selectCharacterNum;
	}

	public void PushSkillCharacterTab(int index)
	{
		selectCharacterNum = index;
		statusFSM.SendTrigger("ChangeCharacter");
		if (skillViewArray[0].activeInHierarchy)
		{
			statusSkillViewManager.isSelectSkillLearnTab = false;
			skillFSM.SendTrigger("ChangeCharacter");
		}
	}

	public void PushSkillCharacterLearnTab()
	{
		if (skillViewArray[0].activeInHierarchy)
		{
			selectCharacterNum = 0;
			statusSkillViewManager.isSelectSkillLearnTab = true;
			skillFSM.SendTrigger("ChangeCharacter");
		}
	}

	public void ResetScrollViewContents(Transform content, bool isCustom)
	{
		int childCount = content.childCount;
		if (childCount == 0)
		{
			Debug.Log("スクロールContentの子は0");
			return;
		}
		Debug.Log("引数のContentの子をデスポーンする");
		string text = "";
		text = (isCustom ? "Status Custom Pool" : "Status Item Pool");
		Transform[] array = new Transform[childCount];
		for (int i = 0; i < childCount; i++)
		{
			array[i] = content.GetChild(i);
		}
		if (array.Length != 0 && array != null)
		{
			for (int j = 0; j < childCount; j++)
			{
				PoolManager.Pools[text].Despawn(array[j], 0f, poolParentGO.transform);
			}
		}
	}

	public bool CheckSkillIsLearned(int skillID)
	{
		bool result = false;
		if (PlayerInventoryDataManager.playerLearnedSkillList.Find((LearnedSkillData data) => data.skillID == skillID) != null)
		{
			result = true;
		}
		return result;
	}

	public bool CheckSkillIsEquiped(int skillID, int characterID)
	{
		return PlayerEquipDataManager.playerEquipSkillList[characterID].Contains(skillID);
	}
}
