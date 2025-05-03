using System.Collections.Generic;
using System.Linq;
using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class DrawDungeonCard : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private DungeonMapStatusManager dungeonMapStatusManager;

	public int drawMaxNum = 5;

	private int cardPowerNum;

	private int randomEnemyCount;

	private int needSkipTp;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		dungeonMapStatusManager = GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapStatusManager>();
	}

	public override void OnStateBegin()
	{
		string cardName = "";
		DungeonCardData dungeonCardData = null;
		if (dungeonMapManager.miniCardList.Count < drawMaxNum)
		{
			for (int i = dungeonMapManager.miniCardList.Count; i < drawMaxNum; i++)
			{
				Transform transform = PoolManager.Pools["DungeonObject"].Spawn(dungeonMapManager.miniCardGo, dungeonMapManager.miniCardGroup.transform);
				transform.localScale = new Vector3(1f, 1f, 1f);
				if (dungeonMapManager.miniCardList.Count == 0)
				{
					int num = Random.Range(0, 100);
					if (num < 65)
					{
						cardName = "dungeonCardBattle";
					}
					else if (num < 80)
					{
						cardName = "dungeonCardHardBattle";
					}
					else
					{
						cardName = dungeonMapManager.GetRandomDungeonCard();
					}
				}
				else
				{
					cardName = dungeonMapManager.GetRandomDungeonCard();
				}
				dungeonCardData = GameDataManager.instance.dungeonMapCardDataBase.dungeonCardDataList.Find((DungeonCardData data) => data.cardLocalizeTerm == cardName);
				SetParameterContainer(dungeonCardData, transform);
				transform.GetComponent<CanvasGroup>().interactable = true;
				transform.GetComponent<CanvasGroup>().alpha = 1f;
				DungeonSelectCardData dungeonSelectCardData = new DungeonSelectCardData();
				dungeonSelectCardData.multiType = dungeonCardData.multiType.ToString();
				dungeonSelectCardData.subTypeString = dungeonCardData.cardType.ToString();
				dungeonSelectCardData.localizeTerm = dungeonCardData.cardLocalizeTerm;
				dungeonSelectCardData.powerNum = cardPowerNum;
				dungeonSelectCardData.enemyCountNum = randomEnemyCount;
				dungeonSelectCardData.needSkipTp = needSkipTp;
				dungeonSelectCardData.sortID = dungeonCardData.sortID;
				dungeonSelectCardData.gameObject = transform.gameObject;
				dungeonMapManager.miniCardList.Add(dungeonSelectCardData);
			}
		}
		dungeonMapManager.SortInPlayDungeonCard();
		dungeonMapStatusManager.ClearTotalNeedTp();
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

	private void SetParameterContainer(DungeonCardData cardData, Transform transform)
	{
		cardPowerNum = 0;
		if (cardData.multiType == DungeonCardData.Type.heroine || cardData.cardType == DungeonCardData.SubType.vigilant)
		{
			int minInclusive = cardData.cardPower / 3;
			int cardPower = cardData.cardPower;
			cardPowerNum = Random.Range(minInclusive, cardPower);
		}
		else
		{
			cardPowerNum = cardData.cardPower;
		}
		if (cardData.multiType == DungeonCardData.Type.heroine)
		{
			float num = 0f;
			switch (PlayerEquipDataManager.accessoryLibidoUpRate)
			{
			case 50:
				num = (float)cardPowerNum * 1.5f;
				cardPowerNum = Mathf.FloorToInt(num);
				break;
			case -50:
				num = cardPowerNum / 2;
				cardPowerNum = Mathf.FloorToInt(num);
				break;
			case -100:
				cardPowerNum = 0;
				break;
			}
			if (PlayerNonSaveDataManager.isDungeonSexEvent || PlayerDataManager.playerLibido > 40)
			{
				float f = (float)cardPowerNum * 1.5f;
				cardPowerNum = Mathf.CeilToInt(f);
			}
		}
		if (cardData.cardType == DungeonCardData.SubType.buffAttack || cardData.cardType == DungeonCardData.SubType.buffDefense)
		{
			List<HaveCampItemData> list = PlayerInventoryDataManager.haveItemCampItemList.Where((HaveCampItemData data) => data.itemType == "lanthanum").ToList();
			if (list != null && list.Count > 0)
			{
				HaveCampItemData kitData = list.OrderBy((HaveCampItemData data) => data.itemSortID).LastOrDefault();
				int power = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData data) => data.itemID == kitData.itemID).power;
				cardPowerNum += power;
			}
		}
		if (cardData.cardType == DungeonCardData.SubType.deBuffAttack || cardData.cardType == DungeonCardData.SubType.deBuffDefense)
		{
			List<HaveCampItemData> list2 = PlayerInventoryDataManager.haveItemCampItemList.Where((HaveCampItemData data) => data.itemType == "charm").ToList();
			if (list2 != null && list2.Count > 0)
			{
				HaveCampItemData kitData2 = list2.OrderBy((HaveCampItemData data) => data.itemSortID).LastOrDefault();
				int power2 = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData data) => data.itemID == kitData2.itemID).power;
				cardPowerNum -= power2;
			}
		}
		if (cardData.cardType == DungeonCardData.SubType.camp || cardData.cardType == DungeonCardData.SubType.healFountain || cardData.cardType == DungeonCardData.SubType.medicFountain)
		{
			List<HaveCampItemData> list3 = PlayerInventoryDataManager.haveItemCampItemList.Where((HaveCampItemData data) => data.itemType == "camp").ToList();
			if (list3 != null && list3.Count > 0)
			{
				HaveCampItemData kitData3 = list3.OrderBy((HaveCampItemData data) => data.itemSortID).LastOrDefault();
				int subPower = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData data) => data.itemID == kitData3.itemID).subPower;
				cardPowerNum += subPower;
			}
		}
		DungeonMapData dungeonMapData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerDataManager.currentDungeonName);
		List<int> maxEnemyCount = dungeonMapData.maxEnemyCount;
		randomEnemyCount = Random.Range(1, maxEnemyCount[dungeonMapManager.currentBorderNum] + 1);
		if (cardData.cardType == DungeonCardData.SubType.battle || cardData.cardType == DungeonCardData.SubType.hardBattle)
		{
			needSkipTp = dungeonMapData.needSkipTpList[dungeonMapManager.currentBorderNum] * randomEnemyCount;
			if (cardData.cardType == DungeonCardData.SubType.hardBattle)
			{
				float f2 = (float)needSkipTp * 1.5f;
				needSkipTp = Mathf.CeilToInt(f2);
			}
		}
		else
		{
			needSkipTp = 0;
		}
		ParameterContainer component = transform.GetComponent<ParameterContainer>();
		component.GetVariable<TmpText>("nameText").textMeshProUGUI.text = cardData.cardName;
		component.GetVariable<UguiImage>("cardImage").image.sprite = cardData.cardSprite;
		component.SetInt("addLibidoNum", cardPowerNum);
		component.SetInt("enemyCountNum", randomEnemyCount);
		component.SetString("cardSubType", cardData.cardType.ToString());
		component.SetString("cardType", cardData.multiType.ToString());
		component.SetString("localizeTerm", cardData.cardLocalizeTerm);
		SetNumFrameSizeDelta(component, "default");
		if (cardData.multiType == DungeonCardData.Type.heroine)
		{
			component.GetGameObject("numFrame").SetActive(value: true);
			component.GetGameObject("typeFrame").SetActive(value: true);
			component.GetVariable<TmpText>("modText").textMeshProUGUI.text = "+";
			component.GetVariable<TmpText>("numText").textMeshProUGUI.text = cardPowerNum.ToString();
			SetTypeIconImage(cardData, component);
		}
		else if (cardData.multiType == DungeonCardData.Type.battle)
		{
			component.GetGameObject("numFrame").SetActive(value: true);
			component.GetGameObject("typeFrame").SetActive(value: true);
			SetNumFrameSizeDelta(component, "battle");
			component.GetVariable<TmpText>("modText").textMeshProUGUI.text = "×";
			component.GetVariable<TmpText>("numText").textMeshProUGUI.text = randomEnemyCount.ToString();
			component.GetVariable<TmpText>("skipTpNumText").textMeshProUGUI.text = needSkipTp.ToString();
			SetTypeIconImage(cardData, component);
		}
		else if (cardData.multiType != 0)
		{
			component.GetGameObject("numFrame").SetActive(value: false);
			component.GetGameObject("typeFrame").SetActive(value: true);
			SetTypeIconImage(cardData, component);
		}
		else if (cardData.cardType == DungeonCardData.SubType.vigilant)
		{
			component.GetGameObject("numFrame").SetActive(value: true);
			component.GetGameObject("typeFrame").SetActive(value: false);
			SetNumFrameSizeDelta(component, "vigilant");
			component.GetVariable<TmpText>("modText").textMeshProUGUI.text = "-";
			component.GetVariable<TmpText>("numText").textMeshProUGUI.text = cardPowerNum.ToString();
		}
		else
		{
			component.GetGameObject("numFrame").SetActive(value: false);
			component.GetGameObject("typeFrame").SetActive(value: false);
		}
	}

	private void SetNumFrameSizeDelta(ParameterContainer parameterContainer, string type)
	{
		RectTransform component = parameterContainer.GetGameObject("numFrame").GetComponent<RectTransform>();
		RectTransform component2 = parameterContainer.GetGameObject("numGroup").GetComponent<RectTransform>();
		switch (type)
		{
		case "default":
			parameterContainer.GetGameObject("numTypeIconGo").SetActive(value: false);
			parameterContainer.GetGameObject("skipTpNumFrame").SetActive(value: false);
			component.sizeDelta = new Vector2(122f, 58f);
			component2.offsetMin = new Vector2(15f, 4f);
			component2.offsetMax = new Vector2(-15f, -4f);
			break;
		case "battle":
			parameterContainer.GetGameObject("numTypeIconGo").SetActive(value: false);
			parameterContainer.GetGameObject("skipTpNumFrame").SetActive(value: true);
			component.sizeDelta = new Vector2(122f, 58f);
			component2.offsetMin = new Vector2(20f, 4f);
			component2.offsetMax = new Vector2(-45f, -4f);
			break;
		case "vigilant":
			parameterContainer.GetGameObject("numTypeIconGo").SetActive(value: true);
			parameterContainer.GetGameObject("skipTpNumFrame").SetActive(value: false);
			component.sizeDelta = new Vector2(122f, 58f);
			component2.offsetMin = new Vector2(50f, 4f);
			component2.offsetMax = new Vector2(-15f, -4f);
			break;
		}
	}

	private void SetTypeIconImage(DungeonCardData cardData, ParameterContainer parameterContainer)
	{
		Sprite dungeonCardTypeSprite = dungeonMapManager.GetDungeonCardTypeSprite(cardData.multiType.ToString());
		parameterContainer.GetVariable<UguiImage>("typeImage").image.sprite = dungeonCardTypeSprite;
	}
}
