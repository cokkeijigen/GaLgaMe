using Arbor;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class MpRecoveryItemListRefresh : StateBehaviour
{
	private CraftExtensionManager craftExtensionManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftExtensionManager = GameObject.Find("Craft Extension Manager").GetComponent<CraftExtensionManager>();
	}

	public override void OnStateBegin()
	{
		CraftScrollItemDesapwnAll();
		ItemWeaponData itemWeaponData = null;
		craftExtensionManager.needAllMpRecoveryPowderNum = 0;
		int num = 0;
		int n;
		for (n = 0; n < PlayerInventoryDataManager.haveWeaponList.Count; n++)
		{
			itemWeaponData = null;
			itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData item) => item.itemID == PlayerInventoryDataManager.haveWeaponList[n].itemID);
			if (itemWeaponData != null)
			{
				Transform transform = PoolManager.Pools["Extension Item Pool"].Spawn(craftExtensionManager.scrollPrefabGoArray[0]);
				ParameterContainer component = transform.transform.GetComponent<ParameterContainer>();
				RefreshItemList(transform, num++);
				string text = itemWeaponData.category.ToString() + itemWeaponData.itemID;
				Debug.Log(text);
				component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = text;
				component.GetVariable<UguiTextVariable>("powerText").text.text = itemWeaponData.attackPower.ToString();
				component.GetVariable<UguiImage>("itemImage").image.sprite = craftExtensionManager.selectScrollContentSpriteArray[0];
				int weaponIncludeMaxMp = PlayerInventoryDataManager.haveWeaponList[n].weaponIncludeMaxMp;
				component.GetVariable<UguiTextVariable>("maxText").text.text = weaponIncludeMaxMp.ToString();
				int weaponIncludeMp = PlayerInventoryDataManager.haveWeaponList[n].weaponIncludeMp;
				component.GetVariable<UguiTextVariable>("currentText").text.text = weaponIncludeMp.ToString();
				int powderCost = itemWeaponData.powderCost;
				component.GetVariable<UguiTextVariable>("costText").text.text = powderCost.ToString();
				float num2 = Mathf.Ceil((float)(weaponIncludeMaxMp - weaponIncludeMp) / 30f);
				craftExtensionManager.needAllMpRecoveryPowderNum += (int)num2;
				SetItemIconSprite(component.GetVariable<UguiImage>("iconImage").image, "greatSword");
				ExtensionItemListClickAction component2 = transform.GetComponent<ExtensionItemListClickAction>();
				component2.itemID = itemWeaponData.itemID;
				SetFirstItemData(instanceID: component2.instanceID = PlayerInventoryDataManager.haveWeaponList[n].itemUniqueID, loopNum: n, itemID: itemWeaponData.itemID, go: transform.gameObject);
				Debug.Log("武器データ設定完了");
			}
			else
			{
				Debug.Log("武器データなし");
			}
		}
		if (craftExtensionManager.isMpRecoveryComplete)
		{
			int scrollContentIndexFromItemId = craftExtensionManager.GetScrollContentIndexFromItemId(craftExtensionManager.clickedItemID, craftExtensionManager.clickedItemUniqueID);
			craftExtensionManager.scrollContentGoArray[0].transform.GetChild(scrollContentIndexFromItemId).GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage")
				.image.sprite = craftExtensionManager.selectScrollContentSpriteArray[1];
			Debug.Log("選択中の項目は：" + scrollContentIndexFromItemId + "番目");
		}
		craftExtensionManager.characterTalkTextLoc.Term = "mpRecoveryBalloonItemSelect";
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

	private void CraftScrollItemDesapwnAll()
	{
		int childCount = craftExtensionManager.scrollContentGoArray[0].transform.childCount;
		Transform[] array = new Transform[childCount];
		for (int i = 0; i < childCount; i++)
		{
			array[i] = craftExtensionManager.scrollContentGoArray[0].transform.GetChild(i);
		}
		Transform[] array2 = array;
		foreach (Transform transform in array2)
		{
			transform.GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage").image.sprite = craftExtensionManager.selectScrollContentSpriteArray[0];
			PoolManager.Pools["Extension Item Pool"].Despawn(transform, 0f, craftExtensionManager.spawnPoolParent.transform);
		}
		Debug.Log("項目を全部デスポーン");
	}

	private void RefreshItemList(Transform transform, int i)
	{
		transform.SetParent(craftExtensionManager.scrollContentGoArray[0].transform);
		transform.transform.localScale = new Vector3(1f, 1f, 1f);
		transform.transform.SetSiblingIndex(i);
	}

	private void SetItemIconSprite(Image imageComponent, string category)
	{
		Sprite sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[category];
		imageComponent.sprite = sprite;
	}

	private void SetFirstItemData(int loopNum, int itemID, int instanceID, GameObject go)
	{
		if (loopNum == 0 && craftExtensionManager.clickedItemID == 0)
		{
			craftExtensionManager.clickedItemID = itemID;
			craftExtensionManager.clickedItemUniqueID = instanceID;
			craftExtensionManager.clickedScrollContentIndex = 0;
		}
	}
}
