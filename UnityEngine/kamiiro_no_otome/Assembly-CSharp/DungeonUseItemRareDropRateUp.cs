using Arbor;
using DarkTonic.MasterAudio;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class DungeonUseItemRareDropRateUp : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	private DungeonItemManager dungeonItemManager;

	public float tweenTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = GetComponentInParent<DungeonBattleManager>();
		dungeonItemManager = GetComponent<DungeonItemManager>();
	}

	public override void OnStateBegin()
	{
		dungeonItemManager.useItemWindowGo.GetComponent<CanvasGroup>().interactable = false;
		ItemData itemData = GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData data) => data.itemID == dungeonItemManager.selectItemID);
		PlayerDataManager.rareDropRateRaisePowerNum = itemData.itemPower;
		PlayerDataManager.rareDropRateRaiseRaimingDaysNum = Mathf.Clamp(PlayerDataManager.rareDropRateRaiseRaimingDaysNum + 10, 0, 99);
		PlayerInventoryDataManager.haveItemList.Find((HaveItemData data) => data.itemID == dungeonItemManager.selectItemID).haveCountNum--;
		SpawnRareDropUpEffect(itemData);
		Invoke("InvokeMethod", itemData.animationTime);
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

	private void SpawnRareDropUpEffect(ItemData itemData)
	{
		GameObject effectPrefabGo = GameDataManager.instance.skillEffectDataBase.skillEffectDataList.Find((SkillEffectData data) => data.effectName == itemData.effectType.ToString()).effectPrefabGo;
		dungeonBattleManager.damagePointRect[3].anchoredPosition = new Vector2(660f, 400f);
		Transform transform = PoolManager.Pools["DungeonSkillEffect"].Spawn(effectPrefabGo, dungeonBattleManager.damagePointRect[3]);
		transform.localPosition = new Vector2(0f, 0f);
		transform.localScale = new Vector3(2f, 2f, 2f);
		string effectType = itemData.effectType;
		string text = effectType.Substring(0, 1).ToUpper();
		effectType = "SeSkill" + text + effectType.Substring(1);
		MasterAudio.PlaySound(effectType, 1f, null, 0f, null, null);
		PoolManager.Pools["DungeonSkillEffect"].Despawn(transform, itemData.animationTime, dungeonBattleManager.skillEffectPoolParentGo.transform);
		GameObject.Find("Dungeon Map Status Manager").GetComponent<ArborFSM>().SendTrigger("RefreshDungeonBuff");
	}

	private void InvokeMethod()
	{
		dungeonItemManager.useItemWindowGo.GetComponent<CanvasGroup>().interactable = true;
		Debug.Log("レアドロップ率アップ処理終了");
		Transition(stateLink);
	}
}
