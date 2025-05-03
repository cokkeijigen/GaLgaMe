using System.Collections.Generic;
using System.Linq;
using Arbor;
using I2.Loc;
using UnityEngine;

[AddComponentMenu("")]
public class RefreshDungeonCardInfo : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private LocalizationParamsManager localizationParamsManager;

	private int cardPowerNum;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = base.transform.parent.GetComponent<DungeonMapManager>();
		localizationParamsManager = dungeonMapManager.localizationParamsManager;
	}

	public override void OnStateBegin()
	{
		dungeonMapManager.cardInfoFrame.SetActive(value: true);
		DungeonCardData dungeonCardData = GameDataManager.instance.dungeonMapCardDataBase.dungeonCardDataList.Find((DungeonCardData data) => data.cardLocalizeTerm == dungeonMapManager.selectCardTerm);
		cardPowerNum = dungeonCardData.cardPower;
		if (dungeonCardData.cardType == DungeonCardData.SubType.buffAttack || dungeonCardData.cardType == DungeonCardData.SubType.buffDefense)
		{
			List<HaveCampItemData> list = PlayerInventoryDataManager.haveItemCampItemList.Where((HaveCampItemData data) => data.itemType == "lanthanum").ToList();
			if (list != null && list.Count > 0)
			{
				HaveCampItemData kitData = list.OrderBy((HaveCampItemData data) => data.itemSortID).LastOrDefault();
				int power = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData data) => data.itemID == kitData.itemID).power;
				cardPowerNum += power;
			}
		}
		if (dungeonCardData.cardType == DungeonCardData.SubType.deBuffAttack || dungeonCardData.cardType == DungeonCardData.SubType.deBuffDefense)
		{
			List<HaveCampItemData> list2 = PlayerInventoryDataManager.haveItemCampItemList.Where((HaveCampItemData data) => data.itemType == "charm").ToList();
			if (list2 != null && list2.Count > 0)
			{
				HaveCampItemData kitData2 = list2.OrderBy((HaveCampItemData data) => data.itemSortID).LastOrDefault();
				int power2 = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData data) => data.itemID == kitData2.itemID).power;
				cardPowerNum -= power2;
			}
		}
		if (dungeonCardData.cardType == DungeonCardData.SubType.camp || dungeonCardData.cardType == DungeonCardData.SubType.healFountain || dungeonCardData.cardType == DungeonCardData.SubType.medicFountain)
		{
			List<HaveCampItemData> list3 = PlayerInventoryDataManager.haveItemCampItemList.Where((HaveCampItemData data) => data.itemType == "camp").ToList();
			if (list3 != null && list3.Count > 0)
			{
				HaveCampItemData kitData3 = list3.OrderBy((HaveCampItemData data) => data.itemSortID).LastOrDefault();
				int subPower = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData data) => data.itemID == kitData3.itemID).subPower;
				cardPowerNum += subPower;
			}
		}
		localizationParamsManager.SetParameterValue("cardPower", cardPowerNum.ToString());
		ParameterContainer component = dungeonMapManager.cardInfoFrame.GetComponent<ParameterContainer>();
		component.GetVariable<I2LocalizeComponent>("cardSubTypeTerm").localize.Term = dungeonCardData.cardLocalizeTerm;
		component.GetVariable<I2LocalizeComponent>("cardBonusTerm").localize.Term = "dungeonType_" + dungeonCardData.multiType.ToString() + "_Bonus";
		component.GetVariable<I2LocalizeComponent>("cardSuumaryTerm").localize.Term = dungeonCardData.cardLocalizeTerm + "_Summary";
		component.GetVariable<UguiImage>("cardImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.dungeonCardIconDictionary[dungeonCardData.cardType.ToString()];
		if (dungeonCardData.multiType != 0)
		{
			component.GetGameObject("cardTypeFrame").SetActive(value: true);
			component.GetVariable<UguiImage>("cardTypeImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.dungeonCardTypeIconDictionary[dungeonCardData.multiType.ToString()];
		}
		else
		{
			component.GetGameObject("cardTypeFrame").SetActive(value: false);
		}
		dungeonMapManager.isCardMouseOver = true;
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
}
