using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using PathologicalGames;
using TMPro;
using UnityEngine;

[AddComponentMenu("")]
public class DungeonUseItemRefreshPlayerHp : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	private DungeonMapStatusManager dungeonMapStatusManager;

	private DungeonItemManager dungeonItemManager;

	public float tweenTime;

	public StateLink rareDropRateUpLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = GetComponentInParent<DungeonBattleManager>();
		dungeonMapStatusManager = GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapStatusManager>();
		dungeonItemManager = GetComponent<DungeonItemManager>();
	}

	public override void OnStateBegin()
	{
		if (dungeonItemManager.selectItemID == 70)
		{
			Transition(rareDropRateUpLink);
			return;
		}
		dungeonItemManager.useItemWindowGo.GetComponent<CanvasGroup>().interactable = false;
		ItemData itemData = GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData data) => data.itemID == dungeonItemManager.selectItemID);
		dungeonMapStatusManager.beforePlayerStatusValue = PlayerStatusDataManager.playerAllHp;
		PlayerStatusDataManager.playerAllHp = Mathf.Clamp(PlayerStatusDataManager.playerAllHp + itemData.itemPower, 0, PlayerStatusDataManager.playerAllMaxHp);
		dungeonMapStatusManager.isPlayerStatusViewSetUp.Clear();
		PlayerInventoryDataManager.haveItemList.Find((HaveItemData data) => data.itemID == dungeonItemManager.selectItemID).haveCountNum--;
		SpawnHealEffect(itemData);
		dungeonMapStatusManager.playerHpText.DOCounter(dungeonMapStatusManager.beforePlayerStatusValue, PlayerStatusDataManager.playerAllHp, tweenTime, addThousandsSeparator: false);
		dungeonMapStatusManager.playerHpSlider.DOValue(PlayerStatusDataManager.playerAllHp, tweenTime);
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

	private void SpawnHealEffect(ItemData itemData)
	{
		GameObject effectPrefabGo = GameDataManager.instance.skillEffectDataBase.skillEffectDataList.Find((SkillEffectData data) => data.effectName == itemData.effectType.ToString()).effectPrefabGo;
		dungeonBattleManager.damagePointRect[3].anchoredPosition = new Vector2(660f, 400f);
		Transform transform = PoolManager.Pools["DungeonSkillEffect"].Spawn(effectPrefabGo, dungeonBattleManager.damagePointRect[3]);
		transform.localPosition = new Vector2(0f, 0f);
		dungeonBattleManager.damagePointRect[1].anchoredPosition = new Vector2(660f, 400f);
		Transform transform2 = PoolManager.Pools["DungeonBattleEffect"].Spawn(dungeonBattleManager.poolEffectArray[3], dungeonBattleManager.damagePointRect[1]);
		transform2.GetComponent<TextMeshProUGUI>().fontSize = 70f;
		transform2.GetComponent<TextMeshProUGUI>().text = itemData.itemPower.ToString();
		transform.localScale = new Vector3(2f, 2f, 2f);
		transform2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
		string effectType = itemData.effectType;
		string text = effectType.Substring(0, 1).ToUpper();
		effectType = "SeSkill" + text + effectType.Substring(1);
		MasterAudio.PlaySound(effectType, 1f, null, 0f, null, null);
		PoolManager.Pools["DungeonSkillEffect"].Despawn(transform, itemData.animationTime, dungeonBattleManager.skillEffectPoolParentGo.transform);
		PoolManager.Pools["DungeonBattleEffect"].Despawn(transform2, itemData.animationTime, dungeonBattleManager.poolManagerGO);
	}

	private void InvokeMethod()
	{
		dungeonItemManager.useItemWindowGo.GetComponent<CanvasGroup>().interactable = true;
		Debug.Log("回復処理終了");
		Transition(stateLink);
	}
}
