using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class DungeonBattleItemEffectStart : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	public InputSlotAny inputSlotItemData;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = GetComponentInParent<DungeonBattleManager>();
	}

	public override void OnStateBegin()
	{
		ItemData itemData = null;
		inputSlotItemData.TryGetValue<ItemData>(out itemData);
		GameObject effectPrefabGo = GameDataManager.instance.skillEffectDataBase.skillEffectDataList.Find((SkillEffectData data) => data.effectName == itemData.effectType.ToString()).effectPrefabGo;
		_ = itemData.spawnTransformV2;
		dungeonBattleManager.damagePointRect[0].anchoredPosition = new Vector2(dungeonBattleManager.damagePointRect[5].anchoredPosition.x, dungeonBattleManager.damagePointRect[5].anchoredPosition.y);
		Transform transform = PoolManager.Pools["DungeonSkillEffect"].Spawn(effectPrefabGo, dungeonBattleManager.damagePointRect[0]);
		transform.localScale = new Vector3(2f, 2f, 2f);
		transform.localPosition = new Vector2(0f, 0f);
		PoolManager.Pools["DungeonSkillEffect"].Despawn(transform, itemData.animationTime, dungeonBattleManager.skillEffectPoolParentGo.transform);
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
