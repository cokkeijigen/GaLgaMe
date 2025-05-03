using Arbor;
using DarkTonic.MasterAudio;
using I2.Loc;
using PathologicalGames;
using TMPro;
using UnityEngine;

[AddComponentMenu("")]
public class SetScenarioAttackDamageText : StateBehaviour
{
	public enum Type
	{
		player,
		enemy
	}

	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public Type type;

	public InputSlotAny inputCharacterData;

	public InputSlotAny inputEnemyData;

	public FlexibleInt damageValue;

	public float despawnTime;

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
		CharacterStatusData value = null;
		BattleEnemyData value2 = null;
		inputCharacterData.TryGetValue<CharacterStatusData>(out value);
		inputEnemyData.TryGetValue<BattleEnemyData>(out value2);
		Transform transform = null;
		string text = "";
		string sType = "";
		switch (type)
		{
		case Type.player:
		{
			int num = PlayerStatusDataManager.enemyMember[scenarioBattleTurnManager.playerTargetNum];
			utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = "enemy" + num;
			text = value.normalEffectType;
			break;
		}
		case Type.enemy:
		{
			int num = PlayerStatusDataManager.playerPartyMember[scenarioBattleTurnManager.enemyTargetNum];
			utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = "character" + num;
			text = value2.normalEffectType;
			break;
		}
		}
		utageBattleSceneManager.battleTextArray3[2].GetComponent<TextMeshProUGUI>().text = damageValue.value.ToString();
		utageBattleSceneManager.battleTextArray3[1].GetComponent<Localize>().Term = "textConjunctionTo";
		utageBattleSceneManager.battleTextArray3[3].GetComponent<Localize>().Term = "battleTextAttackDamage";
		if (scenarioBattleTurnManager.isParrySuccess)
		{
			utageBattleSceneManager.battleTextArray3[6].GetComponent<Localize>().Term = "battleTextParry";
			utageBattleSceneManager.battleTextArray3[6].SetActive(value: true);
		}
		utageBattleSceneManager.battleTextGroupArray[2].SetActive(value: true);
		utageBattleSceneManager.battleTextArray3[0].SetActive(value: true);
		utageBattleSceneManager.battleTextArray3[1].SetActive(value: true);
		utageBattleSceneManager.battleTextArray3[2].SetActive(value: true);
		utageBattleSceneManager.battleTextArray3[3].SetActive(value: true);
		switch (type)
		{
		case Type.player:
		{
			Transform transform3 = utageBattleSceneManager.enemyImageGoList[scenarioBattleTurnManager.playerTargetNum].transform;
			utageBattleSceneManager.damagePointRect[0].position = new Vector3(transform3.position.x, 0f, 0f);
			sType = "SeAttackPlayerCritical";
			if (scenarioBattleTurnManager.isCriticalHit)
			{
				transform = PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[6], utageBattleSceneManager.damagePointRect[0]);
				transform.GetComponent<TextMeshProUGUI>().fontSize = 130f;
			}
			else
			{
				transform = ((!scenarioBattleTurnManager.isNormalWeaklHit) ? ((!scenarioBattleTurnManager.isNormalResistHit) ? PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[0], utageBattleSceneManager.damagePointRect[0]) : PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[9], utageBattleSceneManager.damagePointRect[0])) : PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[8], utageBattleSceneManager.damagePointRect[0]));
			}
			break;
		}
		case Type.enemy:
		{
			Transform transform2 = utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.enemyTargetNum].transform;
			utageBattleSceneManager.damagePointRect[0].position = new Vector3(transform2.position.x, -2f, 0f);
			sType = "SeAttackEnemyCritical";
			if (scenarioBattleTurnManager.isCriticalHit)
			{
				transform = PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[6], utageBattleSceneManager.damagePointRect[0]);
				transform.GetComponent<TextMeshProUGUI>().fontSize = 110f;
			}
			else
			{
				transform = ((!scenarioBattleTurnManager.isNormalWeaklHit) ? ((!scenarioBattleTurnManager.isNormalResistHit) ? PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[1], utageBattleSceneManager.damagePointRect[0]) : PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[9], utageBattleSceneManager.damagePointRect[0])) : PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[8], utageBattleSceneManager.damagePointRect[0]));
			}
			break;
		}
		}
		transform.GetComponent<TextMeshProUGUI>().text = damageValue.value.ToString();
		float seconds = despawnTime / (float)utageBattleSceneManager.battleSpeed;
		PoolManager.Pools["BattleEffect"].Despawn(transform, seconds, utageBattleSceneManager.poolManagerGO);
		if (scenarioBattleTurnManager.isCriticalHit)
		{
			utageBattleSceneManager.battleTextArray2[2].GetComponent<Localize>().Term = "battleTextAttackCritical";
			utageBattleSceneManager.battleTextArray2[2].SetActive(value: true);
			MasterAudio.PlaySound(sType, 1f, null, 0f, null, null);
		}
		else
		{
			string text2 = text.Substring(0, 1).ToUpper();
			text = "SeAttack" + text2 + text.Substring(1);
			MasterAudio.PlaySound(text, 1f, null, 0f, null, null);
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

	private void SpawnCriticalEffect(Transform parentRect)
	{
		GameObject effectPrefabGo = GameDataManager.instance.skillEffectDataBase.skillEffectDataList.Find((SkillEffectData data) => data.effectName == "criticalHitSpikes").effectPrefabGo;
		Transform transform = PoolManager.Pools["BattleEffect"].Spawn(effectPrefabGo, parentRect);
		transform.localPosition = new Vector2(0f, 0f);
		transform.localScale = new Vector3(2.2f, 2.2f, 1f);
		PoolManager.Pools["BattleEffect"].Despawn(transform, 1f, utageBattleSceneManager.poolManagerGO.transform);
	}
}
