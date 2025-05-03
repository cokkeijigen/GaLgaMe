using System.Collections.Generic;
using System.Linq;
using Arbor;
using I2.Loc;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class DungeonMapManager : SerializedMonoBehaviour
{
	public GameObject dungeonMapCanvas;

	public GameObject dungeonBattleCanvas;

	public CanvasGroup[] mapCanvasGroupArray;

	public CanvasGroup miniCardGroupCanvasGroup;

	public ArborFSM mapFSM;

	public PlayMakerFSM routeApplyGroupFSM;

	public GameObject sexButton;

	public Localize sexButtonLoc;

	public GameObject bossButton;

	public Localize autoButtonLoc;

	public GameObject subGroup;

	public GameObject questNewAlertIconGo;

	public GameObject questNoticeAlertIconGo;

	public Localize dungeonNameTerm;

	public GameObject basementTextGo;

	public TextMeshProUGUI dungeonCurrentFloorText;

	public TextMeshProUGUI dungeonMaxFloorText;

	public TextMeshProUGUI playerLibidoText;

	public DungeonLibidoDataBase dungeonLibidoDataBase;

	public Sprite[] characterThumnailSpriteArray;

	public Sprite[] characterBattleThumnailSpriteArray;

	public GameObject[] chracterImageGoArray;

	public Image[] dungeonBgImageArray;

	public GameObject routeSelectGroupWindow;

	public GameObject[] routeGroupArray;

	public GameObject[] routeSelectFrameArray;

	public GameObject routeSelectBigFrame;

	public CanvasGroup routeSelectApplyButton;

	public GameObject routeButtonGroup;

	public GameObject autoAlertGroup;

	public GameObject routeAnimationGroupWindow;

	public GameObject[] routeAnimationGroupArray;

	public GameObject[] routeSelectFrameMiniArray;

	public GameObject routeSelectBigFrameMini;

	public GameObject[] routeResultGroupArray;

	public Localize[] routeResultGroupLocArray;

	public TextMeshProUGUI[] routeResultNumText;

	public GameObject cardInfoFrame;

	public GameObject miniCardGroup;

	public GameObject miniCardGo;

	public string selectCardTerm;

	public bool isCardMouseOver;

	public Dictionary<string, float> chooseCardDictionary = new Dictionary<string, float>();

	public List<DungeonSelectCardData> miniCardList = new List<DungeonSelectCardData>();

	public List<DungeonSelectCardData> selectCardList = new List<DungeonSelectCardData>();

	public int[] selectCardBonusArray;

	public LocalizationParamsManager localizationParamsManager;

	public Dictionary<int, int> getDropItemDictionary = new Dictionary<int, int>();

	public Dictionary<int, int> getDropMagicMaterialDictionary = new Dictionary<int, int>();

	public int getDropMoney;

	public bool isGetDropBonus;

	public int getDropItemNum;

	public int getDropBonusNum;

	public int getMaterialBonusNum;

	public Transform poolManagerGO;

	private const int changeLibidoCardProbability = 100;

	public bool isMimicBattle;

	public bool isSexLibidoEventEnable;

	public bool isSexFloorEventEnable;

	public bool dungeonAutoMove;

	public TextMeshProUGUI speedTmpGO;

	private bool isDungeon3AtFloor41;

	public int battleConsecutiveTotalNum;

	public int battleConsecutiveRoundNum;

	public TextMeshProUGUI[] roundNumText;

	public bool isDungeonRouteAction;

	public bool isBossRouteSelect;

	public PlayableDirector dungeonRoundDirector;

	public int[] consecutiveResultData;

	public List<int> consecutiveResultEnemyMember;

	public int thisFloorActionNum;

	public int dungeonCurrentFloorNum;

	public int dungeonMaxFloorNum;

	public int currentBorderNum;

	public PlayableDirector dungeonMapDirector;

	public float dungeonMapAnimationDuration;

	private void Awake()
	{
		dungeonMapCanvas.SetActive(value: false);
		dungeonBattleCanvas.SetActive(value: false);
		dungeonRoundDirector.gameObject.SetActive(value: false);
	}

	public bool GetIsScnearioBattle()
	{
		return PlayerNonSaveDataManager.isScenarioBattle;
	}

	public bool CheckIsDungeonMapAuto()
	{
		return PlayerDataManager.isDungeonMapAuto;
	}

	public void ChooseCardInittialize()
	{
		chooseCardDictionary.Clear();
		string name = PlayerDataManager.currentDungeonName;
		int drawCardTypeCount = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == name).drawCardTypeCount;
		List<string> cardList = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == name).drawCardNameList;
		int i;
		for (i = 0; i < drawCardTypeCount; i++)
		{
			float drawWait = GameDataManager.instance.dungeonMapCardDataBase.dungeonCardDataList.Find((DungeonCardData data) => data.cardLocalizeTerm == cardList[i]).drawWait;
			chooseCardDictionary.Add(cardList[i], drawWait);
		}
		if (PlayerEquipDataManager.accessoryItemDiscover > 0)
		{
			float num = (float)PlayerEquipDataManager.accessoryItemDiscover / 100f + 1f;
			chooseCardDictionary["dungeonCardCollect"] *= num;
			chooseCardDictionary["dungeonCardCorpse"] *= num;
			chooseCardDictionary["dungeonCardTreasure"] *= num;
			if (chooseCardDictionary.ContainsKey("dungeonCardBigTreasure"))
			{
				chooseCardDictionary["dungeonCardBigTreasure"] *= num;
			}
		}
		if (PlayerDataManager.rareDropRateRaisePowerNum > 0 && PlayerDataManager.rareDropRateRaiseRaimingDaysNum > 0)
		{
			float num2 = (float)PlayerDataManager.rareDropRateRaisePowerNum / 100f + 1f;
			chooseCardDictionary["dungeonCardTreasure"] *= num2;
			if (chooseCardDictionary.ContainsKey("dungeonCardBigTreasure"))
			{
				chooseCardDictionary["dungeonCardBigTreasure"] *= num2;
			}
		}
		if (!PlayerDataManager.isDungeonHeroineFollow)
		{
			return;
		}
		string characterDungeonSexUnLockFlag = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == PlayerDataManager.DungeonHeroineFollowNum).characterDungeonSexUnLockFlag;
		if (PlayerFlagDataManager.scenarioFlagDictionary[characterDungeonSexUnLockFlag])
		{
			chooseCardDictionary.Add("dungeonCardVigilant", 5f);
			if (PlayerNonSaveDataManager.isDungeonSexEvent)
			{
				chooseCardDictionary.Add("dungeonCardHeroineTalk", 100f);
			}
			else
			{
				chooseCardDictionary.Add("dungeonCardHeroineTalk", 20f);
			}
		}
	}

	public void ChangeHeroineCardProbability(bool isHeroineLibidoHigher)
	{
		if (!PlayerDataManager.isDungeonHeroineFollow)
		{
			Debug.Log("ヒロインが同行していない");
		}
		else if (isHeroineLibidoHigher || PlayerNonSaveDataManager.isDungeonSexEvent)
		{
			chooseCardDictionary["dungeonCardHeroineTalk"] = 100f;
			Debug.Log("イチャイチャ発生時にヒロインカードの重みを変更する／イチャイチャ時");
		}
		else
		{
			chooseCardDictionary["dungeonCardHeroineTalk"] = 20f;
			Debug.Log("イチャイチャ発生時にヒロインカードの重みを変更する／通常時");
		}
	}

	public string GetRandomDungeonCard()
	{
		Dictionary<string, float> dictionary = new Dictionary<string, float>();
		dictionary = new Dictionary<string, float>(chooseCardDictionary);
		float num = 0f;
		IEnumerable<DungeonSelectCardData> enumerable = miniCardList.Where((DungeonSelectCardData data) => data.multiType == DungeonCardData.Type.none.ToString());
		if (enumerable.Count() > 0)
		{
			foreach (DungeonSelectCardData item in enumerable)
			{
				Debug.Log("辞書からユニークKeyを削除：" + item.localizeTerm);
				dictionary.Remove(item.localizeTerm);
			}
		}
		if (miniCardList.Where((DungeonSelectCardData data) => data.subTypeString == "camp" || data.subTypeString == "healFountain" || data.subTypeString == "medicFountain").Count() > 0)
		{
			if (dictionary.ContainsKey("dungeonCardCamp"))
			{
				dictionary.Remove("dungeonCardCamp");
			}
			if (dictionary.ContainsKey("dungeonCardHealFountain"))
			{
				dictionary.Remove("dungeonCardHealFountain");
			}
			if (dictionary.ContainsKey("dungeonCardMedicFountain"))
			{
				dictionary.Remove("dungeonCardMedicFountain");
			}
			Debug.Log("辞書からキャンプKeyを削除");
		}
		foreach (KeyValuePair<string, float> item2 in dictionary)
		{
			num += item2.Value;
		}
		float num2 = Random.value * num;
		foreach (KeyValuePair<string, float> item3 in dictionary)
		{
			if (num2 < item3.Value)
			{
				Debug.Log("引いたカードは：" + item3.Key);
				return item3.Key;
			}
			num2 -= item3.Value;
		}
		Debug.Log("重み抽選で例外発生：" + num2);
		return "dungeonCardBattle";
	}

	public void SortInPlayDungeonCard()
	{
		miniCardList = (from data in miniCardList
			orderby data.sortID, data.enemyCountNum descending, data.powerNum descending
			select data).ToList();
		for (int i = 0; i < miniCardList.Count; i++)
		{
			miniCardList[i].gameObject.transform.SetAsLastSibling();
		}
		Debug.Log("カードのソート完了");
	}

	public Sprite GetDungeonCardTypeSprite(string typeName)
	{
		return GameDataManager.instance.itemCategoryDataBase.dungeonCardTypeIconDictionary[typeName];
	}

	public void ResetDungeonRouteFrame()
	{
		for (int i = 0; i < routeSelectFrameArray.Length; i++)
		{
			ParameterContainer component = routeSelectFrameArray[i].GetComponent<ParameterContainer>();
			component.GetVariable<I2LocalizeComponent>("nameTextTerm").localize.Term = "empty";
			component.GetGameObject("numFrame").SetActive(value: false);
			component.GetGameObject("typeFrame").SetActive(value: false);
			component.GetGameObject("bonusTextGo").SetActive(value: false);
			component.GetVariable<TmpText>("modText").textMeshProUGUI.text = "+";
			component.GetVariable<TmpText>("numText").textMeshProUGUI.text = "0";
			component.GetVariable<UguiImage>("iconImageGo").image.sprite = GameDataManager.instance.itemCategoryDataBase.dungeonCardIconDictionary["noRoute"];
		}
	}

	public bool GetRouteIsBossSelect()
	{
		return isBossRouteSelect;
	}

	public int GetRouteSelectedCount()
	{
		return selectCardList.Count;
	}

	public void SetDropItemDictionary(int itemID, bool bonusChance)
	{
		getMaterialBonusNum = 0;
		getDropBonusNum = 0;
		int num = 1;
		if (PlayerDataManager.currentDungeonName == "Dungeon3" && dungeonCurrentFloorNum > 40)
		{
			isDungeon3AtFloor41 = true;
		}
		else
		{
			isDungeon3AtFloor41 = false;
		}
		if (!isBossRouteSelect && !PlayerNonSaveDataManager.isDungeonScnearioBattle)
		{
			string subTypeString = selectCardList[thisFloorActionNum].subTypeString;
			if ((subTypeString == "collect" || subTypeString == "corpse") && bonusChance)
			{
				int num2 = selectCardBonusArray[thisFloorActionNum] * 2;
				int num3 = Random.Range(1, 11);
				Debug.Log("ボーナス率：" + num2 + "／ランダム係数：" + num3);
				if (num3 <= num2)
				{
					getDropBonusNum = Random.Range(1, 4);
					num += getDropBonusNum;
					isGetDropBonus = true;
				}
				else
				{
					isGetDropBonus = false;
				}
			}
		}
		if ((100 <= itemID && itemID < 600) || (840 <= itemID && itemID < 900))
		{
			int num4 = Random.Range(2, 4);
			if (200 <= itemID && itemID < 300)
			{
				num4++;
			}
			if (PlayerNonSaveDataManager.isDungeonGetItemHighlight && (itemID == 150 || itemID == 202 || itemID == 421 || itemID == 480))
			{
				num4 *= 3;
				Debug.Log("強調表示アイテムの獲得量を増やす：" + itemID);
			}
			if (isDungeon3AtFloor41 && (itemID == 333 || itemID == 340 || itemID == 343 || itemID == 441 || itemID == 502))
			{
				num4 *= 3;
				Debug.Log("ビーカニアの41層以降の指定アイテムの獲得量を増やす：" + itemID);
			}
			getMaterialBonusNum = num4;
			num = (getDropItemNum = num + num4);
		}
		if (itemID < 100)
		{
			int itemSortID = PlayerInventoryDataAccess.GetItemSortID(itemID);
			PlayerInventoryDataAccess.PlayerHaveItemAdd(itemID, itemSortID, num);
			PlayerInventoryDataAccess.HaveItemListSort();
			return;
		}
		if (getDropItemDictionary.ContainsKey(itemID))
		{
			getDropItemDictionary[itemID] += num;
		}
		else
		{
			getDropItemDictionary.Add(itemID, num);
		}
		IOrderedEnumerable<KeyValuePair<int, int>> source = getDropItemDictionary.OrderBy((KeyValuePair<int, int> data) => data.Key);
		getDropItemDictionary = source.ToDictionary((KeyValuePair<int, int> data) => data.Key, (KeyValuePair<int, int> data) => data.Value);
	}

	public void SetDropMagicMateterialDictionary(int itemID, int count)
	{
		if (getDropItemDictionary.ContainsKey(itemID))
		{
			getDropItemDictionary[itemID] += count;
		}
		else
		{
			getDropItemDictionary.Add(itemID, count);
		}
		IOrderedEnumerable<KeyValuePair<int, int>> source = getDropItemDictionary.OrderBy((KeyValuePair<int, int> data) => data.Key);
		getDropItemDictionary = source.ToDictionary((KeyValuePair<int, int> data) => data.Key, (KeyValuePair<int, int> data) => data.Value);
	}

	public void GetDropMagicMaterialDictionary()
	{
		getDropMagicMaterialDictionary.Clear();
		foreach (KeyValuePair<int, int> item in PlayerNonSaveDataManager.preGetDropMagicMaterial)
		{
			if (getDropMagicMaterialDictionary.ContainsKey(item.Value))
			{
				getDropMagicMaterialDictionary[item.Value]++;
			}
			else
			{
				getDropMagicMaterialDictionary.Add(item.Value, 1);
			}
		}
	}

	public int GetItemSortID(int id)
	{
		int num = 0;
		if (id < 100)
		{
			return GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData data) => data.itemID == id).sortID;
		}
		if (id < 600)
		{
			return GameDataManager.instance.itemMaterialDataBase.itemMaterialDataList.Find((ItemMaterialData data) => data.itemID == id).sortID;
		}
		if (id < 900)
		{
			return GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList.Find((ItemMagicMaterialData data) => data.itemID == id).sortID;
		}
		if (id < 950)
		{
			return GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData data) => data.itemID == id).sortID;
		}
		if (id < 1000)
		{
			return GameDataManager.instance.itemCashableItemDataBase.itemCashableItemDataList.Find((ItemCashableItemData data) => data.itemID == id).sortID;
		}
		return GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData data) => data.itemID == id).sortID;
	}

	public void GetItemToHaveData()
	{
		PlayerDataManager.AddHaveMoney(getDropMoney);
		foreach (KeyValuePair<int, int> data in getDropItemDictionary)
		{
			int itemSortID = GetItemSortID(data.Key);
			if (data.Key < 100)
			{
				PlayerInventoryDataAccess.PlayerHaveItemAdd(data.Key, itemSortID, data.Value);
				continue;
			}
			if (data.Key < 600)
			{
				PlayerInventoryDataAccess.PlayerHaveItemAdd(data.Key, itemSortID, data.Value);
				continue;
			}
			if (data.Key < 900)
			{
				PlayerInventoryDataAccess.PlayerHaveItemAdd(data.Key, itemSortID, data.Value);
				continue;
			}
			if (data.Key < 950)
			{
				PlayerInventoryDataAccess.PlayerHaveEventItemAdd(data.Key, itemSortID);
				continue;
			}
			if (data.Key < 1000)
			{
				PlayerInventoryDataAccess.PlayerHaveItemAdd(data.Key, itemSortID, data.Value);
				continue;
			}
			ItemAccessoryData data2 = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData item) => item.itemID == data.Key);
			for (int i = 0; i < data.Value; i++)
			{
				PlayerInventoryDataEquipAccess.PlayerHaveAccessoryAdd(data2);
			}
		}
		PlayerInventoryDataAccess.HaveItemListSortAll();
	}

	public int GetDungeonBattleDropItem(Dictionary<int, float> itemDictionary)
	{
		float num = 0f;
		foreach (KeyValuePair<int, float> item in itemDictionary)
		{
			num += item.Value;
		}
		float num2 = Random.value * num;
		foreach (KeyValuePair<int, float> item2 in itemDictionary)
		{
			if (num2 < item2.Value)
			{
				Debug.Log("ドロップアイテムIDは：" + item2.Key);
				return item2.Key;
			}
			num2 -= item2.Value;
		}
		Debug.Log("重み抽選で例外発生：" + num2);
		return 0;
	}

	public void SetFloorBonusNum(int[] bonusNumArray)
	{
		Debug.Log("ボーナス計算終了");
		for (int i = 0; i < selectCardBonusArray.Length; i++)
		{
			selectCardBonusArray[i] = bonusNumArray[i];
		}
		Debug.Log("ボーナス計算終了を送信");
		mapFSM.SendTrigger("EndCheckDungeonBonus");
	}
}
