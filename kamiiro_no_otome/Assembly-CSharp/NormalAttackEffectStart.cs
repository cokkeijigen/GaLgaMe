using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class NormalAttackEffectStart : StateBehaviour
{
	public enum Type
	{
		player,
		enemy,
		support
	}

	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public Type type;

	public OutputSlotAny outputCharacterData;

	public OutputSlotAny outputEnemyData;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
	}

	public override void OnStateBegin()
	{
		int getID = 0;
		float seconds = 0f;
		float num = 0f;
		GameObject prefab = null;
		Transform transform = null;
		Vector2 vector = default(Vector2);
		SkillEffectData skillEffectData = null;
		CharacterStatusData characterStatusData = null;
		BattleEnemyData battleEnemyData = null;
		switch (type)
		{
		case Type.player:
			getID = PlayerBattleConditionManager.playerIsDead[scenarioBattleTurnManager.playerAttackCount].memberID;
			characterStatusData = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[getID];
			outputCharacterData.SetValue(characterStatusData);
			skillEffectData = GameDataManager.instance.skillEffectDataBase.skillEffectDataList.Find((SkillEffectData data) => data.effectName == characterStatusData.normalEffectType.ToString());
			prefab = skillEffectData.effectPrefabGo;
			transform = utageBattleSceneManager.enemyImageGoList[scenarioBattleTurnManager.playerTargetNum].transform;
			vector = skillEffectData.effectLocalPosition;
			seconds = characterStatusData.effectDespawnTime;
			break;
		case Type.enemy:
		{
			getID = PlayerBattleConditionManager.enemyIsDead[scenarioBattleTurnManager.enemyAttackCount].memberID;
			int index = GameDataManager.instance.battleEnemyDataBase.enemyDataList.FindIndex((BattleEnemyData data) => data.enemyID == getID);
			battleEnemyData = GameDataManager.instance.battleEnemyDataBase.enemyDataList[index];
			outputEnemyData.SetValue(battleEnemyData);
			skillEffectData = GameDataManager.instance.skillEffectDataBase.skillEffectDataList.Find((SkillEffectData data) => data.effectName == battleEnemyData.normalEffectType.ToString());
			prefab = skillEffectData.effectPrefabGo;
			transform = utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.enemyTargetNum].transform;
			vector = new Vector2(skillEffectData.effectLocalPosition.x, -2f);
			seconds = battleEnemyData.effectDespawnTime;
			break;
		}
		case Type.support:
			getID = utageBattleSceneManager.supportAttackMemberId;
			characterStatusData = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[getID];
			outputCharacterData.SetValue(characterStatusData);
			skillEffectData = GameDataManager.instance.skillEffectDataBase.skillEffectDataList.Find((SkillEffectData data) => data.effectName == characterStatusData.normalEffectType.ToString());
			prefab = skillEffectData.effectPrefabGo;
			transform = utageBattleSceneManager.enemyImageGoList[scenarioBattleTurnManager.playerTargetNum].transform;
			vector = skillEffectData.effectLocalPosition;
			seconds = characterStatusData.effectDespawnTime;
			break;
		}
		num = transform.position.x + vector.x;
		utageBattleSceneManager.effectSpawnPoint.position = new Vector3(num, vector.y, 0f);
		Transform transform2 = PoolManager.Pools["SkillEffect"].Spawn(prefab, utageBattleSceneManager.effectSpawnPoint);
		transform2.transform.localScale = new Vector3(skillEffectData.localScale, skillEffectData.localScale, skillEffectData.localScale);
		transform2.transform.localPosition = skillEffectData.effectLocalPosition;
		PoolManager.Pools["SkillEffect"].Despawn(transform2, seconds, utageBattleSceneManager.effectSpawnParent);
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
