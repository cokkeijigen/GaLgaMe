using System.Collections.Generic;
using System.Linq;
using Arbor;
using I2.Loc;
using PathologicalGames;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class CraftManager : SerializedMonoBehaviour
{
	private CraftCanvasManager craftCanvasManager;

	private CraftCheckManager craftCheckManager;

	public ArborFSM arborFSM;

	public ArborFSM craftAndMergeFSM;

	public ArborFSM newCraftFSM;

	public ArborFSM craftCheckFSM;

	public Localize craftCommandTypeLoc;

	public GameObject blackSmithButtonGroup;

	public GameObject mergeButton;

	public CanvasGroup exitButtonCanvasGroup;

	public GameObject[] infoWindowGoArray;

	public CanvasGroup[] canvasGroupArray;

	public Localize itemSelectHeaderLocalize;

	public GameObject[] itemCategoryTabGoArray;

	public GameObject[] itemSerectScrollSummaryGoArray;

	public GameObject itemSelectScrollContentGO;

	public Scrollbar itemSelectScrollBar;

	public GameObject poolParentGO;

	public GameObject[] scrollItemGoArray;

	public GameObject[] craftAddOnGoArray;

	public Localize addOnLockTextLoc;

	public Localize addOnHeaderTextLoc;

	public Sprite[] selectScrollContentSpriteArray;

	public Sprite[] selectTabSpriteArray;

	public int selectCategoryNum;

	public int selectScrollContentIndex;

	public int selectPartyMemberNum;

	public Image characterImage;

	public Sprite[] characterImageSpriteArray;

	public int clickedItemID;

	public int nextItemID;

	public Color enableColor = new Color(0.196f, 0.196f, 0.196f, 1f);

	public Color disableColor = new Color(0.6f, 0.6f, 0.6f, 1f);

	public int clickedUniqueID = int.MaxValue;

	public int[] itemStatusParam = new int[8];

	public bool[] requiredItemENOUGH = new bool[4];

	private void Awake()
	{
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
		craftCheckManager = GameObject.Find("Craft Check Manager").GetComponent<CraftCheckManager>();
		canvasGroupArray[0].gameObject.SetActive(value: true);
		canvasGroupArray[1].gameObject.SetActive(value: false);
	}

	private void Start()
	{
		GameObject gameObject = GameObject.Find("Header Status Manager");
		if (gameObject != null)
		{
			HeaderStatusManager component = gameObject.GetComponent<HeaderStatusManager>();
			component.clockGroupGo.SetActive(value: false);
			component.clockCanvasGroup.gameObject.SetActive(value: false);
		}
	}

	public void PushSelectCategoryButton(int categoryNum)
	{
		if (selectCategoryNum != categoryNum)
		{
			selectCategoryNum = categoryNum;
			clickedItemID = 0;
			craftCanvasManager.isCraftComplete = false;
			switch (PlayerNonSaveDataManager.selectCraftCanvasName)
			{
			case "craft":
				craftAndMergeFSM.SendTrigger("ChangeCraftCanvas");
				break;
			case "newCraft":
				newCraftFSM.SendTrigger("ChangeNewCraftCanvas");
				break;
			case "merge":
				craftAndMergeFSM.SendTrigger("ChangeCraftCanvas");
				break;
			}
		}
	}

	public void PushSelectScrollButton()
	{
		switch (PlayerNonSaveDataManager.selectCraftCanvasName)
		{
		case "craft":
		case "merge":
			craftAndMergeFSM.SendTrigger("SendCraftItemListIndex");
			break;
		case "newCraft":
			newCraftFSM.SendTrigger("SendCraftItemListIndex");
			break;
		}
	}

	public void PushInfoApplyButton()
	{
		switch (PlayerNonSaveDataManager.selectCraftCanvasName)
		{
		case "craft":
			if (craftCanvasManager.isRemainingDaysZero || craftCanvasManager.isCompleteEnhanceCount)
			{
				craftCheckManager.isAnimationCraft = true;
			}
			else
			{
				craftCheckManager.isAnimationCraft = false;
			}
			craftAndMergeFSM.SendTrigger("OpenNeedMaterialCanvas");
			break;
		case "merge":
			craftCheckManager.isAnimationCraft = true;
			craftCanvasManager.isPowerUpCraft = false;
			craftAndMergeFSM.SendTrigger("OpenNeedMaterialCanvas");
			break;
		case "newCraft":
			craftCheckManager.isAnimationCraft = true;
			switch (selectCategoryNum)
			{
			case 0:
			case 1:
			case 2:
				newCraftFSM.SendTrigger("OpenNeedMaterialCanvas");
				break;
			case 3:
			case 4:
				craftCheckManager.OpenConfirmDialog();
				break;
			}
			break;
		}
	}

	public void PushInfoMergeInheritButton()
	{
		craftCheckManager.isAnimationCraft = true;
		craftCanvasManager.isPowerUpCraft = true;
		craftAndMergeFSM.SendTrigger("OpenNeedMaterialCanvas");
	}

	public bool GetCommonDataFromItemID(int itemID, ref string itemName, ref Sprite itemIconSprite, ref string CategoryName, ref Sprite itemSprite)
	{
		if (itemID < 100)
		{
			ItemData itemData = null;
			itemData = GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData item) => item.itemID == itemID);
			if (!(itemData != null))
			{
				return false;
			}
			itemName = itemData.itemName;
			itemIconSprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemData.category.ToString()];
			CategoryName = itemData.category.ToString();
			itemSprite = itemData.itemSprite;
		}
		else if (itemID < 600)
		{
			ItemMaterialData itemMaterialData = null;
			itemMaterialData = GameDataManager.instance.itemMaterialDataBase.itemMaterialDataList.Find((ItemMaterialData item) => item.itemID == itemID);
			if (!(itemMaterialData != null))
			{
				return false;
			}
			itemName = itemMaterialData.itemName;
			itemIconSprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemMaterialData.category.ToString()];
			CategoryName = itemMaterialData.category.ToString();
			itemSprite = itemMaterialData.itemSprite;
		}
		else if (itemID < 630)
		{
			ItemCanMakeMaterialData itemCanMakeMaterialData = null;
			itemCanMakeMaterialData = GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Find((ItemCanMakeMaterialData item) => item.itemID == itemID);
			if (!(itemCanMakeMaterialData != null))
			{
				return false;
			}
			itemName = itemCanMakeMaterialData.itemName;
			itemIconSprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemCanMakeMaterialData.category.ToString()];
			CategoryName = itemCanMakeMaterialData.category.ToString();
			itemSprite = itemCanMakeMaterialData.itemSprite;
		}
		else if (itemID < 680)
		{
			ItemCampItemData itemCampItemData = null;
			itemCampItemData = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData item) => item.itemID == itemID);
			if (!(itemCampItemData != null))
			{
				return false;
			}
			itemName = itemCampItemData.itemName;
			itemIconSprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemCampItemData.category.ToString()];
			CategoryName = itemCampItemData.category.ToString();
			itemSprite = itemCampItemData.itemSprite;
		}
		else if (itemID < 900)
		{
			ItemMagicMaterialData itemMagicMaterialData = null;
			itemMagicMaterialData = GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList.Find((ItemMagicMaterialData item) => item.itemID == itemID);
			if (!(itemMagicMaterialData != null))
			{
				return false;
			}
			itemName = itemMagicMaterialData.itemName;
			itemIconSprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemMagicMaterialData.category.ToString()];
			CategoryName = itemMagicMaterialData.category.ToString();
			itemSprite = itemMagicMaterialData.itemSprite;
		}
		else if (itemID < 1000)
		{
			ItemEventItemData itemEventItemData = null;
			itemEventItemData = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData item) => item.itemID == itemID);
			if (!(itemEventItemData != null))
			{
				return false;
			}
			itemName = itemEventItemData.itemName;
			itemIconSprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemEventItemData.category.ToString()];
			CategoryName = itemEventItemData.category.ToString();
			itemSprite = itemEventItemData.itemSprite;
		}
		else if (itemID < 1300)
		{
			ItemWeaponData itemWeaponData = null;
			itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData item) => item.itemID == itemID);
			if (!(itemWeaponData != null))
			{
				return false;
			}
			itemName = itemWeaponData.itemName;
			itemIconSprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemWeaponData.category.ToString()];
			CategoryName = itemWeaponData.category.ToString();
			itemSprite = itemWeaponData.itemSprite;
		}
		else if (itemID < 2000)
		{
			ItemPartyWeaponData itemPartyWeaponData = null;
			itemPartyWeaponData = GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData item) => item.itemID == itemID);
			if (!(itemPartyWeaponData != null))
			{
				return false;
			}
			itemName = itemPartyWeaponData.itemName;
			itemIconSprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemPartyWeaponData.category.ToString()];
			CategoryName = itemPartyWeaponData.category.ToString();
			itemSprite = itemPartyWeaponData.itemSprite;
		}
		else if (itemID < 2300)
		{
			ItemArmorData itemArmorData = null;
			itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData item) => item.itemID == itemID);
			if (!(itemArmorData != null))
			{
				return false;
			}
			itemName = itemArmorData.itemName;
			itemIconSprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary["armor"];
			CategoryName = "armor";
			itemSprite = itemArmorData.itemSprite;
		}
		else if (itemID < 3000)
		{
			ItemPartyArmorData itemPartyArmorData = null;
			itemPartyArmorData = GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData item) => item.itemID == itemID);
			if (!(itemPartyArmorData != null))
			{
				return false;
			}
			itemName = itemPartyArmorData.itemName;
			itemIconSprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary["armor"];
			CategoryName = "armor";
			itemSprite = itemPartyArmorData.itemSprite;
		}
		else
		{
			ItemAccessoryData itemAccessoryData = null;
			itemAccessoryData = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData item) => item.itemID == itemID);
			if (!(itemAccessoryData != null))
			{
				return false;
			}
			itemName = itemAccessoryData.itemName;
			itemIconSprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemAccessoryData.category.ToString()];
			CategoryName = itemAccessoryData.category.ToString();
			itemSprite = itemAccessoryData.itemSprite;
		}
		return true;
	}

	public int GetNextUpgradeItemID(int itemID)
	{
		if (itemID < 2000)
		{
			int num = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.FindIndex((ItemWeaponData m) => m.itemID == itemID);
			if (num == -1)
			{
				return -1;
			}
			int num2 = num;
			if (num < GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Count - 1)
			{
				num2++;
				if (GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList[num2].itemID == 1120)
				{
					return -1;
				}
				return GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList[num2].itemID;
			}
			return -1;
		}
		int num3 = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.FindIndex((ItemArmorData m) => m.itemID == itemID);
		if (num3 == -1)
		{
			return -1;
		}
		int num4 = num3;
		if (num3 < GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Count - 1)
		{
			num4++;
			if (GameDataManager.instance.itemArmorDataBase.itemArmorDataList[num4].itemID == 2070)
			{
				return -1;
			}
			return GameDataManager.instance.itemArmorDataBase.itemArmorDataList[num4].itemID;
		}
		return -1;
	}

	public int GetNextUpgradePartyItemID(int itemID)
	{
		int num = 0;
		num = (itemID % 1000 - 200) / 100;
		if (num < 1 || num > 4)
		{
			return -1;
		}
		if (num != 0)
		{
			if (itemID < 2000)
			{
				ItemPartyWeaponData itemData = GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData data) => data.itemID == itemID);
				List<ItemPartyWeaponData> list = (from data in GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Where((ItemPartyWeaponData data) => data.itemEquipCharacterID == itemData.itemEquipCharacterID).ToList()
					orderby data.sortID
					select data).ToList();
				int num2 = list.FindIndex((ItemPartyWeaponData m) => m.itemID == itemID);
				if (num2 == -1)
				{
					return -1;
				}
				int num3 = num2;
				if (num2 < list.Count - 1)
				{
					num3++;
					return list[num3].itemID;
				}
				return -1;
			}
			ItemPartyArmorData itemData2 = GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData data) => data.itemID == itemID);
			List<ItemPartyArmorData> list2 = (from data in GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Where((ItemPartyArmorData data) => data.itemEquipCharacterID == itemData2.itemEquipCharacterID).ToList()
				orderby data.sortID
				select data).ToList();
			int num4 = list2.FindIndex((ItemPartyArmorData m) => m.itemID == itemID);
			if (num4 == -1)
			{
				return -1;
			}
			int num5 = num4;
			if (num4 < list2.Count - 1)
			{
				num5++;
				return list2[num5].itemID;
			}
			return -1;
		}
		return -1;
	}

	public void SetCharacterImage()
	{
		switch (selectCategoryNum)
		{
		case 0:
			if (clickedItemID < 1400)
			{
				characterImage.sprite = characterImageSpriteArray[1];
			}
			else if (clickedItemID < 1500)
			{
				characterImage.sprite = characterImageSpriteArray[2];
			}
			else if (clickedItemID < 1600)
			{
				characterImage.sprite = characterImageSpriteArray[3];
			}
			else
			{
				characterImage.sprite = characterImageSpriteArray[4];
			}
			break;
		case 1:
			if (clickedItemID < 2400)
			{
				characterImage.sprite = characterImageSpriteArray[1];
			}
			else if (clickedItemID < 2500)
			{
				characterImage.sprite = characterImageSpriteArray[2];
			}
			else if (clickedItemID < 2600)
			{
				characterImage.sprite = characterImageSpriteArray[3];
			}
			else
			{
				characterImage.sprite = characterImageSpriteArray[4];
			}
			break;
		}
	}

	public void CraftScrollItemDesapwnAll()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("CraftScrollItem");
		PoolManager.Pools["Craft Item Pool"].DespawnAll();
		if (array.Length != 0 && array != null)
		{
			GameObject[] array2 = array;
			foreach (GameObject obj in array2)
			{
				obj.GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage").image.sprite = selectScrollContentSpriteArray[0];
				obj.transform.SetParent(poolParentGO.transform);
			}
		}
		Debug.Log("項目を全部デスポーン");
	}

	public int GetScrollContentIndexFromItemId(int itemID, int uniqueID)
	{
		int result = 0;
		int childCount = itemSelectScrollContentGO.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			if (PlayerNonSaveDataManager.selectCraftCanvasName == "craft" || PlayerNonSaveDataManager.selectCraftCanvasName == "newCraft")
			{
				int itemID2 = itemSelectScrollContentGO.transform.GetChild(i).GetComponent<CraftItemListClickAction>().itemID;
				int instanceID = itemSelectScrollContentGO.transform.GetChild(i).GetComponent<CraftItemListClickAction>().instanceID;
				if (itemID2 == itemID && instanceID == uniqueID)
				{
					result = i;
					break;
				}
			}
			else if (itemSelectScrollContentGO.transform.GetChild(i).GetComponent<MergeItemListClickAction>().itemID == itemID)
			{
				result = i;
				break;
			}
		}
		return result;
	}

	public bool CheckNextItemUnlockRecipe(int selectItemId)
	{
		bool result = false;
		int nextID = GetNextUpgradeItemID(selectItemId);
		Debug.Log("次の継承アイテムID：" + nextID);
		if (nextID != -1)
		{
			if (selectItemId < 2000)
			{
				ItemWeaponData itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == nextID);
				result = PlayerFlagDataManager.recipeFlagDictionary[itemWeaponData.recipeFlagName];
			}
			else
			{
				ItemArmorData itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == nextID);
				result = PlayerFlagDataManager.recipeFlagDictionary[itemArmorData.recipeFlagName];
			}
		}
		return result;
	}

	public void SetIconAndTextColor(ParameterContainer param, bool isDefault)
	{
		if (isDefault)
		{
			param.GetVariable<UguiImage>("iconImage").image.color = new Color(0.247f, 0.188f, 0.16f);
			param.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.gameObject.GetComponent<Text>().color = new Color(0.196f, 0.196f, 0.196f);
			param.GetVariable<UguiTextVariable>("powerText").text.color = new Color(0.196f, 0.196f, 0.196f);
			param.GetVariable<UguiTextVariable>("numText").text.color = new Color(0.196f, 0.196f, 0.196f);
		}
		else
		{
			Color color = new Color(0.5f, 0.5f, 0.5f);
			param.GetVariable<UguiImage>("iconImage").image.color = color;
			param.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.gameObject.GetComponent<Text>().color = color;
			param.GetVariable<UguiTextVariable>("powerText").text.color = color;
			param.GetVariable<UguiTextVariable>("numText").text.color = color;
		}
	}

	public void SetCraftItemIconAndTextColor(ParameterContainer param, bool isDefault)
	{
		if (isDefault)
		{
			param.GetVariable<UguiImage>("iconImage").image.color = new Color(0.247f, 0.188f, 0.16f);
			param.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.gameObject.GetComponent<Text>().color = new Color(0.196f, 0.196f, 0.196f);
			param.GetVariable<UguiTextVariable>("numText").text.color = new Color(0.196f, 0.196f, 0.196f);
		}
		else
		{
			Color color = new Color(0.5f, 0.5f, 0.5f);
			param.GetVariable<UguiImage>("iconImage").image.color = color;
			param.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.gameObject.GetComponent<Text>().color = color;
			param.GetVariable<UguiTextVariable>("numText").text.color = color;
		}
	}
}
