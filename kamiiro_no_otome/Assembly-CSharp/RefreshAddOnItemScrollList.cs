using System.Collections.Generic;
using System.Linq;
using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class RefreshAddOnItemScrollList : StateBehaviour
{
	private CraftManager craftManager;

	private CraftAddOnManager craftAddOnManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponentInParent<CraftManager>();
		craftAddOnManager = GameObject.Find("Craft AddOn Manager").GetComponent<CraftAddOnManager>();
	}

	public override void OnStateBegin()
	{
		Transform[] array = new Transform[craftAddOnManager.overlayCanvasScrollContent.transform.childCount];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = craftAddOnManager.overlayCanvasScrollContent.transform.GetChild(i);
			if (PoolManager.Pools["Craft Item Pool"].IsSpawned(array[i]))
			{
				PoolManager.Pools["Craft Item Pool"].Despawn(array[i]);
			}
		}
		if (array.Length != 0 && array != null)
		{
			Transform[] array2 = array;
			foreach (Transform obj in array2)
			{
				obj.GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage").image.sprite = craftManager.selectScrollContentSpriteArray[0];
				obj.transform.SetParent(craftManager.poolParentGO.transform);
			}
		}
		List<HaveItemData> typeAddOnList = new List<HaveItemData>();
		List<HaveItemData> powerAddOnList = new List<HaveItemData>();
		List<HaveItemData> list = new List<HaveItemData>();
		MaterialItemListClickAction.ModeType modeType = MaterialItemListClickAction.ModeType.addOnOverlay;
		int num = 0;
		switch (PlayerNonSaveDataManager.selectCraftCanvasName)
		{
		case "craft":
		case "newCraft":
			PlayerInventoryDataAccess.GetPlayerHaveAddOnItems(out typeAddOnList, out powerAddOnList, craftManager.selectCategoryNum);
			modeType = MaterialItemListClickAction.ModeType.addOnOverlay;
			num = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].enableAddOnLv;
			break;
		case "merge":
			PlayerInventoryDataAccess.GetPlayerHaveWonderItems(out typeAddOnList, out powerAddOnList, craftManager.selectCategoryNum);
			modeType = MaterialItemListClickAction.ModeType.wonderOverlay;
			num = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].enableAddOnLv;
			break;
		}
		string selectAddOnType = craftAddOnManager.selectAddOnType;
		if (!(selectAddOnType == "addOn_TYPE"))
		{
			if (selectAddOnType == "addOn_POWER")
			{
				list = powerAddOnList.OrderBy((HaveItemData d) => d.itemID).ToList();
			}
		}
		else
		{
			list = typeAddOnList.OrderBy((HaveItemData d) => d.itemID).ToList();
		}
		Debug.Log("取得したアドオンアイテムの個数：" + list.Count);
		int num2 = 0;
		foreach (HaveItemData dat in list)
		{
			string text = "";
			ItemMagicMaterialData itemMagicMaterialData = GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList.Find((ItemMagicMaterialData data) => data.itemID == dat.itemID);
			text = itemMagicMaterialData.category.ToString();
			if (itemMagicMaterialData.addOnPower <= num)
			{
				Transform obj2 = PoolManager.Pools["Craft Item Pool"].Spawn(craftManager.scrollItemGoArray[2]);
				ParameterContainer component = obj2.transform.GetComponent<ParameterContainer>();
				obj2.SetParent(craftAddOnManager.overlayCanvasScrollContent.transform);
				obj2.transform.localScale = new Vector3(1f, 1f, 1f);
				obj2.transform.SetSiblingIndex(num2);
				string term = text + dat.itemID;
				component.GetVariable<UguiImage>("itemImage").image.sprite = craftManager.selectScrollContentSpriteArray[0];
				component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = term;
				component.GetVariable<UguiTextVariable>("numText").text.text = dat.haveCountNum.ToString();
				Sprite sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[text];
				component.GetVariable<UguiImage>("iconImage").image.sprite = sprite;
				MaterialItemListClickAction component2 = obj2.GetComponent<MaterialItemListClickAction>();
				component2.itemID = dat.itemID;
				component2.modeType = modeType;
				num2++;
			}
			Transition(stateLink);
		}
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
