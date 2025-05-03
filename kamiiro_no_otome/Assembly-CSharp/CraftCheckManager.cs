using System.Collections.Generic;
using Arbor;
using Coffee.UIExtensions;
using I2.Loc;
using PathologicalGames;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class CraftCheckManager : SerializedMonoBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	private CraftTalkManager craftTalkManager;

	public ArborFSM craftCheckFSM;

	public GameObject animationCanvas;

	public GameObject blackImageGo;

	public GameObject factorDisposalWindow;

	public GameObject dialogWindow;

	public Localize dialogBaseTextLoc;

	public Localize dialogBeforeTypeTextLoc;

	public Localize dialogAfterTypeTextLoc;

	public Localize dialogBeforeTextLoc;

	public GameObject[] dialogAfterTextGroupArray;

	public Localize dialogAfterTextLoc;

	public Localize dialogTypeTextLoc;

	public Text dialogNeedTimeText;

	public GameObject getFactorWindow;

	public GameObject newFactorFrame;

	public GameObject newItemFrame;

	public GameObject craftedSummaryWindow;

	public UIParticle uIParticle;

	public GameObject[] craftedEffectPrefabGoArray;

	public Transform craftedEffectSpawnGo;

	public Localize infoWindowHeaderTextLoc;

	public GameObject infoFactorScrollFrameGo;

	public GameObject infoFactorScrollPrefab;

	public GameObject infoFactorScrollContentGO;

	public TextMeshProUGUI[] infoFactorGroupText;

	public GameObject animationFrame;

	public GameObject animationSkipFrame;

	public UIParticle animationUIParticle;

	public PlayableAsset[] animationAssetArray;

	public Dictionary<string, GameObject> animationEffectPrefabGoDictionary;

	public List<Transform> animationEffectSpawnGoList = new List<Transform>();

	public int newFactorID;

	public int newPowerLevel;

	public int newFactorPower;

	public FactorData.FactorType newFactorType;

	public int craftedItemID;

	public int craftedUniqueID;

	public Toggle viewChangeToggle;

	public bool isAnimationCraft;

	public HaveFactorData newFactorData = new HaveFactorData();

	public List<HaveFactorData> tempSetFactorList;

	public List<HaveFactorData> tempHaveFactorList;

	private void Awake()
	{
		animationCanvas.SetActive(value: false);
		factorDisposalWindow.SetActive(value: false);
		craftedSummaryWindow.SetActive(value: false);
		getFactorWindow.SetActive(value: false);
	}

	private void Start()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
		craftTalkManager = GameObject.Find("Craft Talk Manager").GetComponent<CraftTalkManager>();
	}

	public void OpenConfirmDialog()
	{
		PlayerNonSaveDataManager.openDialogName = "craft";
		craftTalkManager.TalkBalloonConfirm();
		switch (PlayerNonSaveDataManager.selectCraftCanvasName)
		{
		case "craft":
			if (craftCanvasManager.isPowerUpCraft || craftCanvasManager.isRemainingDaysZero)
			{
				dialogBaseTextLoc.Term = "alertInheritCraftDialog";
				dialogTypeTextLoc.Term = "dialogNeedInheritCraftTime";
				GameObject[] array = dialogAfterTextGroupArray;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetActive(value: true);
				}
				dialogBeforeTypeTextLoc.Term = "dialogCraftBeforeItem";
				dialogAfterTypeTextLoc.Term = "dialogCraftAfterItem";
				dialogBeforeTextLoc.Term = PlayerInventoryDataEquipAccess.GetCraftItemNameTerm(craftManager.clickedItemID);
				dialogAfterTextLoc.Term = PlayerInventoryDataEquipAccess.GetCraftItemNameTerm(craftManager.nextItemID);
			}
			else if (craftCanvasManager.isCompleteEnhanceCount)
			{
				dialogBaseTextLoc.Term = "alertEvolutionCraftDialog";
				dialogTypeTextLoc.Term = "dialogNeedEvolutionCraftTime";
				GameObject[] array = dialogAfterTextGroupArray;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetActive(value: true);
				}
				dialogBeforeTypeTextLoc.Term = "dialogEvolutionCraftBeforeItem";
				dialogAfterTypeTextLoc.Term = "dialogEvolutionCraftAfterItem";
				dialogBeforeTextLoc.Term = PlayerInventoryDataEquipAccess.GetCraftItemNameTerm(craftManager.clickedItemID);
				dialogAfterTextLoc.Term = PlayerInventoryDataEquipAccess.GetCraftItemNameTerm(craftManager.nextItemID);
			}
			else
			{
				dialogBaseTextLoc.Term = "alertCraftDialog";
				dialogTypeTextLoc.Term = "dialogNeedCraftTime";
				GameObject[] array = dialogAfterTextGroupArray;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetActive(value: false);
				}
				dialogBeforeTypeTextLoc.Term = "dialogSelectCraftItem";
				dialogBeforeTextLoc.Term = PlayerInventoryDataEquipAccess.GetCraftItemNameTerm(craftManager.clickedItemID);
			}
			break;
		case "newCraft":
		{
			dialogBaseTextLoc.Term = "alertNewCraftDialog";
			dialogTypeTextLoc.Term = "dialogNeedNewCraftTime";
			GameObject[] array = dialogAfterTextGroupArray;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(value: false);
			}
			dialogBeforeTypeTextLoc.Term = "dialogSelectCraftItem";
			dialogBeforeTextLoc.Term = PlayerInventoryDataEquipAccess.GetCraftItemNameTerm(craftManager.clickedItemID);
			break;
		}
		case "merge":
			if (craftCanvasManager.isPowerUpCraft)
			{
				dialogBaseTextLoc.Term = "alertMergeDialog";
				dialogTypeTextLoc.Term = "dialogNeedMergeTime";
				GameObject[] array = dialogAfterTextGroupArray;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetActive(value: true);
				}
				dialogBeforeTypeTextLoc.Term = "dialogCraftBeforeItem";
				dialogAfterTypeTextLoc.Term = "dialogCraftAfterItem";
				dialogBeforeTextLoc.Term = PlayerInventoryDataEquipAccess.GetCraftItemNameTerm(craftManager.clickedItemID);
				dialogAfterTextLoc.Term = PlayerInventoryDataEquipAccess.GetCraftItemNameTerm(craftManager.nextItemID);
			}
			else
			{
				dialogBaseTextLoc.Term = "alertEditDialog";
				dialogTypeTextLoc.Term = "dialogNeedEditTime";
				GameObject[] array = dialogAfterTextGroupArray;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetActive(value: false);
				}
				dialogBeforeTypeTextLoc.Term = "dialogSelectCraftItem";
				dialogBeforeTextLoc.Term = PlayerInventoryDataEquipAccess.GetCraftItemNameTerm(craftManager.clickedItemID);
			}
			break;
		}
		switch (PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].workShopToolLv)
		{
		case 1:
			dialogNeedTimeText.text = "3";
			PlayerNonSaveDataManager.needCraftTimeCount = 3;
			break;
		case 2:
			dialogNeedTimeText.text = "2";
			PlayerNonSaveDataManager.needCraftTimeCount = 2;
			break;
		case 3:
		case 4:
			dialogNeedTimeText.text = "1";
			PlayerNonSaveDataManager.needCraftTimeCount = 1;
			break;
		}
		dialogWindow.SetActive(value: true);
		animationCanvas.SetActive(value: true);
	}

	public void PushConfirmDialogOkButton()
	{
		craftCanvasManager.isAutoEvolutionCraft = false;
		switch (PlayerNonSaveDataManager.selectCraftCanvasName)
		{
		case "craft":
			switch (craftManager.selectCategoryNum)
			{
			case 0:
			{
				ItemWeaponData itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftManager.clickedItemID);
				HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == craftManager.clickedItemID && data.itemUniqueID == craftManager.clickedUniqueID);
				int nextID2 = craftManager.GetNextUpgradeItemID(craftManager.clickedItemID);
				Debug.Log("次の継承アイテムID：" + nextID2);
				if (nextID2 != -1)
				{
					craftManager.nextItemID = nextID2;
					ItemWeaponData itemWeaponData2 = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == nextID2);
					if (PlayerFlagDataManager.recipeFlagDictionary[itemWeaponData2.recipeFlagName])
					{
						if (haveWeaponData.remainingDaysToCraft > 0)
						{
							if (itemWeaponData.factorSlot <= haveWeaponData.itemEnhanceCount)
							{
								craftCanvasManager.isAutoEvolutionCraft = true;
							}
						}
						else
						{
							craftCanvasManager.isAutoEvolutionCraft = true;
						}
					}
				}
				craftManager.craftCheckFSM.SendTrigger("StartAutoCraft");
				break;
			}
			case 1:
			{
				ItemArmorData itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == craftManager.clickedItemID);
				HaveArmorData haveArmorData = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData data) => data.itemID == craftManager.clickedItemID && data.itemUniqueID == craftManager.clickedUniqueID);
				int nextID = craftManager.GetNextUpgradeItemID(craftManager.clickedItemID);
				Debug.Log("次の継承アイテムID：" + nextID);
				if (nextID != -1)
				{
					craftManager.nextItemID = nextID;
					ItemArmorData itemArmorData2 = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == nextID);
					if (PlayerFlagDataManager.recipeFlagDictionary[itemArmorData2.recipeFlagName])
					{
						if (haveArmorData.remainingDaysToCraft > 0)
						{
							if (itemArmorData.factorSlot <= haveArmorData.itemEnhanceCount)
							{
								craftCanvasManager.isAutoEvolutionCraft = true;
							}
						}
						else
						{
							craftCanvasManager.isAutoEvolutionCraft = true;
						}
					}
				}
				craftManager.craftCheckFSM.SendTrigger("StartAutoCraft");
				break;
			}
			}
			break;
		case "newCraft":
			switch (craftManager.selectCategoryNum)
			{
			case 0:
			case 1:
				craftManager.craftCheckFSM.SendTrigger("StartAutoCraft");
				break;
			case 2:
			case 3:
			case 4:
				craftManager.craftCheckFSM.SendTrigger("StartNoCheck");
				break;
			}
			break;
		case "merge":
		case "mergeEdit":
			craftManager.craftCheckFSM.SendTrigger("StartAutoCraft");
			break;
		}
	}

	public void PushConfirmDialogCancelButton()
	{
		craftTalkManager.TalkBalloonItemSelectAfter();
		animationCanvas.SetActive(value: false);
	}

	public void ShowStatusParameters()
	{
		int[] itemStatusParam = craftManager.itemStatusParam;
		ParameterContainer parameterContainer = null;
		switch (craftManager.selectCategoryNum)
		{
		case 0:
		case 1:
			parameterContainer = craftedSummaryWindow.GetComponent<ParameterContainer>();
			break;
		case 2:
		case 3:
		case 4:
			parameterContainer = craftedSummaryWindow.GetComponent<ParameterContainer>();
			break;
		}
		IList<I2LocalizeComponent> variableList = parameterContainer.GetVariableList<I2LocalizeComponent>("statusTypeLoc");
		IList<UguiTextVariable> variableList2 = parameterContainer.GetVariableList<UguiTextVariable>("statusPowerText");
		switch (craftManager.selectCategoryNum)
		{
		case 0:
			variableList[0].localize.Term = "statusAttack";
			variableList2[0].text.text = itemStatusParam[0].ToString();
			variableList[1].localize.Term = "statusMagicAttack";
			variableList2[1].text.text = itemStatusParam[1].ToString();
			variableList[2].localize.Term = "statusAccuracy";
			variableList2[2].text.text = itemStatusParam[2].ToString();
			variableList[3].localize.Term = "statusCritical";
			variableList2[3].text.text = itemStatusParam[3].ToString();
			variableList[4].localize.Term = "statusItemMp";
			variableList2[4].text.text = itemStatusParam[4].ToString();
			variableList[5].localize.Term = "statusSkillSlotCount";
			variableList2[5].text.text = itemStatusParam[5].ToString();
			variableList[6].localize.Term = "statusFacotrSlot";
			variableList2[6].text.text = itemStatusParam[6].ToString();
			variableList[7].localize.Term = "statusFacotrLimitNum";
			variableList2[7].text.text = itemStatusParam[7].ToString();
			break;
		case 1:
			variableList[0].localize.Term = "statusDefense";
			variableList2[0].text.text = itemStatusParam[0].ToString();
			variableList[1].localize.Term = "statusMagicDefense";
			variableList2[1].text.text = itemStatusParam[1].ToString();
			variableList[2].localize.Term = "statusEvasion";
			variableList2[2].text.text = itemStatusParam[2].ToString();
			variableList[3].localize.Term = "statusParryProbability";
			variableList2[3].text.text = itemStatusParam[3].ToString();
			variableList[4].localize.Term = "statusRecoveryMpAdd";
			variableList2[4].text.text = itemStatusParam[4].ToString();
			variableList[5].localize.Term = "statusSkillSlotCount";
			variableList2[5].text.text = itemStatusParam[5].ToString();
			variableList[6].localize.Term = "statusFacotrSlot";
			variableList2[6].text.text = itemStatusParam[6].ToString();
			variableList[7].localize.Term = "statusFacotrLimitNum";
			variableList2[7].text.text = itemStatusParam[7].ToString();
			break;
		case 2:
		{
			variableList[0].localize.Term = "itemTypeSummary_canMakeMaterial";
			variableList2[0].text.text = "";
			ItemCanMakeMaterialData.Category category2 = GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Find((ItemCanMakeMaterialData data) => data.itemID == craftManager.clickedItemID).category;
			variableList[1].localize.Term = "itemTypeSummary_" + category2;
			variableList2[1].text.text = "";
			for (int j = 2; j < 8; j++)
			{
				variableList[j].localize.Term = "noStatus";
				variableList2[j].text.text = "";
			}
			break;
		}
		case 3:
		{
			variableList[0].localize.Term = "itemTypeSummary_eventItem";
			variableList2[0].text.text = "";
			for (int k = 1; k < 8; k++)
			{
				variableList[k].localize.Term = "noStatus";
				variableList2[k].text.text = "";
			}
			break;
		}
		case 4:
		{
			variableList[0].localize.Term = "itemTypeSummary_adventureKit";
			variableList2[0].text.text = "";
			ItemCampItemData.Category category = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData data) => data.itemID == craftManager.clickedItemID).category;
			variableList[1].localize.Term = "itemTypeSummary_" + category;
			variableList2[1].text.text = "";
			if (category == ItemCampItemData.Category.camp)
			{
				variableList[2].localize.Term = "summaryDungeonRestBonus";
				variableList2[2].text.text = craftCanvasManager.itemCampItemData.subPower.ToString();
			}
			else
			{
				variableList[2].localize.Term = "skillPower";
				variableList2[2].text.text = craftCanvasManager.itemCampItemData.power.ToString();
			}
			for (int i = 3; i < 8; i++)
			{
				variableList[i].localize.Term = "noStatus";
				variableList2[i].text.text = "";
			}
			break;
		}
		}
	}

	public void ResetHaveFactorList(int MaxVal, bool ChangeMaxVal)
	{
		GameObject gameObject = infoFactorScrollContentGO;
		TextMeshProUGUI[] array = infoFactorGroupText;
		for (int num = gameObject.transform.childCount - 1; num >= 0; num--)
		{
			Transform child = gameObject.transform.GetChild(num);
			if (child.gameObject.tag == "CustomScrollItem")
			{
				child.transform.SetParent(craftManager.poolParentGO.transform);
				if (PoolManager.Pools["Craft Item Pool"].IsSpawned(child))
				{
					PoolManager.Pools["Craft Item Pool"].Despawn(child);
				}
			}
		}
		array[0].text = "0";
		if (ChangeMaxVal)
		{
			array[1].text = MaxVal.ToString();
		}
	}

	public void SpawnCraftAnimationEffect()
	{
		RectTransform component = animationUIParticle.gameObject.GetComponent<RectTransform>();
		Transform transform;
		if (isAnimationCraft)
		{
			component.anchoredPosition = new Vector2(-16f, component.anchoredPosition.y);
			transform = PoolManager.Pools["Craft Item Pool"].Spawn(animationEffectPrefabGoDictionary["craft1"], animationUIParticle.transform);
		}
		else
		{
			component.anchoredPosition = new Vector2(-8f, component.anchoredPosition.y);
			transform = PoolManager.Pools["Craft Item Pool"].Spawn(animationEffectPrefabGoDictionary["edit1"], animationUIParticle.transform);
		}
		transform.localPosition = new Vector3(0f, 0f, 0f);
		transform.localScale = new Vector3(1f, 1f, 1f);
		animationEffectSpawnGoList.Add(transform);
		animationUIParticle.RefreshParticles();
	}

	public void SpawnCraftAnimationMaxEffect()
	{
		RectTransform component = animationUIParticle.gameObject.GetComponent<RectTransform>();
		Transform transform;
		if (isAnimationCraft)
		{
			component.anchoredPosition = new Vector2(-16f, component.anchoredPosition.y);
			transform = PoolManager.Pools["Craft Item Pool"].Spawn(animationEffectPrefabGoDictionary["craft2"], animationUIParticle.transform);
		}
		else
		{
			component.anchoredPosition = new Vector2(-8f, component.anchoredPosition.y);
			transform = PoolManager.Pools["Craft Item Pool"].Spawn(animationEffectPrefabGoDictionary["edit2"], animationUIParticle.transform);
		}
		transform.localPosition = new Vector3(0f, 0f, 0f);
		transform.localScale = new Vector3(1f, 1f, 1f);
		animationEffectSpawnGoList.Add(transform);
		animationUIParticle.RefreshParticles();
	}
}
