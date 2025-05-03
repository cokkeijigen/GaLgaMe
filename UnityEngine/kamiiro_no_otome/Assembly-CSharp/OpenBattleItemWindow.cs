using Arbor;
using I2.Loc;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class OpenBattleItemWindow : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
	}

	public override void OnStateBegin()
	{
		scenarioBattleSkillManager.scrollContentClickNum = 0;
		scenarioBattleSkillManager.itemWindow.SetActive(value: true);
		PoolManager.Pools["BattleScrollItem"].DespawnAll();
		GameObject[] array = GameObject.FindGameObjectsWithTag("StatusScrollItem");
		if (array.Length != 0 && array != null)
		{
			GameObject[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].transform.SetParent(scenarioBattleSkillManager.battleItemSpawnParent.transform);
			}
		}
		int j;
		for (j = 0; j < PlayerInventoryDataManager.haveItemList.Count; j++)
		{
			ItemData itemData = GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData item) => item.itemID == PlayerInventoryDataManager.haveItemList[j].itemID);
			if (itemData != null)
			{
				Transform transform = PoolManager.Pools["BattleScrollItem"].Spawn(scenarioBattleSkillManager.scrollContentPrefabGoArray[0]);
				RefreshItemList(transform, j);
				string term = itemData.category.ToString() + itemData.itemID;
				transform.transform.Find("Name Text").GetComponent<Localize>().Term = term;
				transform.transform.Find("Have Num Text").GetComponent<Text>().text = PlayerInventoryDataManager.haveItemList[j].haveCountNum.ToString();
				string category = itemData.category.ToString();
				SetItemIconSprite(transform, category);
				ParameterContainer component = transform.GetComponent<ParameterContainer>();
				component.SetInt("itemID", itemData.itemID);
				component.SetInt("sortID", itemData.sortID);
				component.SetString("category", itemData.category.ToString());
			}
		}
		if (PlayerInventoryDataManager.haveItemList.Count > 0)
		{
			int firstItemId = PlayerInventoryDataManager.haveItemList[0].itemID;
			ItemData itemData2 = GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData item) => item.itemID == firstItemId);
			scenarioBattleTurnManager.battleUseItemID = firstItemId;
			scenarioBattleTurnManager.battleUseItemCategory = itemData2.category.ToString();
		}
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

	private void RefreshItemList(Transform transform, int i)
	{
		transform.SetParent(scenarioBattleSkillManager.itemContentGo.transform);
		transform.transform.localPosition = new Vector3(0f, 0f, 0f);
		transform.transform.localScale = new Vector3(1f, 1f, 1f);
		transform.transform.SetSiblingIndex(i);
	}

	private void SetItemIconSprite(Transform go, string category)
	{
		Sprite sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[category];
		go.transform.Find("Icon Image").GetComponent<Image>().sprite = sprite;
	}
}
