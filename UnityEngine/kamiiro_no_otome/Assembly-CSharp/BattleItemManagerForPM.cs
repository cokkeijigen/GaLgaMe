using Arbor;
using UnityEngine;

public class BattleItemManagerForPM : MonoBehaviour
{
	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	private void Awake()
	{
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
	}

	public int GetHaveBattleItemCount()
	{
		return PlayerInventoryDataManager.haveItemList.Count;
	}

	public ItemData GetBattleItemDataFromItemId(int itemId)
	{
		return GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData data) => data.itemID == itemId);
	}

	public ItemData GetBattleItemDataFromIndex(int index)
	{
		return GameDataManager.instance.itemDataBase.itemDataList[index];
	}

	public void SetScrollPrefabGoParam(GameObject spawnGo, int index)
	{
		ItemData battleItemDataFromIndex = GetBattleItemDataFromIndex(index);
		string term = battleItemDataFromIndex.category.ToString() + battleItemDataFromIndex.itemID;
		ParameterContainer component = spawnGo.GetComponent<ParameterContainer>();
		component.GetVariable<I2LocalizeComponent>("Name Text").localize.Term = term;
		component.GetVariable<UguiTextVariable>("haveNumText").text.text = PlayerInventoryDataManager.haveItemList[index].haveCountNum.ToString();
		string key = battleItemDataFromIndex.category.ToString();
		Sprite sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[key];
		component.GetVariable<UguiImage>("iconImage").image.sprite = sprite;
		component.SetInt("itemID", battleItemDataFromIndex.itemID);
		component.SetInt("sortID", battleItemDataFromIndex.sortID);
		component.SetString("category", battleItemDataFromIndex.category.ToString());
	}

	public void SetItemWindowInfoEmpty()
	{
		ParameterContainer component = scenarioBattleSkillManager.itemWindow.GetComponent<ParameterContainer>();
		component.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = "noStatus";
		component.GetVariable<UguiImage>("iconImage").image.enabled = false;
	}
}
