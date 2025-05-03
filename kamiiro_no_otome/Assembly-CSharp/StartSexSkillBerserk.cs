using Arbor;
using DarkTonic.MasterAudio;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class StartSexSkillBerserk : StateBehaviour
{
	private SexTouchStatusManager sexTouchStatusManager;

	private SexBattleManager sexBattleManager;

	private SexBattleTurnManager sexBattleTurnManager;

	private SexBattleEffectManager sexBattleEffectManager;

	private float countTime;

	public float thresholdTime;

	public float despawnTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexTouchStatusManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchStatusManager>();
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		sexBattleTurnManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleTurnManager>();
		sexBattleEffectManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleEffectManager>();
	}

	public override void OnStateBegin()
	{
		sexBattleTurnManager.sexBattleBerserkClickCount = 0;
		countTime = thresholdTime;
		sexBattleManager.SetHeroineSprite("berserkPiston");
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		countTime += Time.deltaTime;
		if (Input.GetButtonDown("Fire1"))
		{
			GameObject gameObject = sexBattleEffectManager.effectPrefabGoDictionary["berserk"];
			Transform obj = PoolManager.Pools["sexSkillPool"].Spawn(gameObject, sexBattleManager.effectPrefabParentDictionary["vagina"]);
			obj.localScale = new Vector3(1f, 1f, 1f);
			obj.localPosition = new Vector3(0f, 0f, 0f);
			sexBattleEffectManager.SetEffectDeSpawnReserve(gameObject.transform, isSkillPool: true, despawnTime);
			MasterAudio.PlaySound("SexBattle_Piston_Berserk", 1f, null, 0f, null, null);
			sexBattleTurnManager.sexBattleBerserkClickCount++;
			if (countTime >= thresholdTime)
			{
				string text = "";
				text = ((!sexBattleTurnManager.isVictoryPiston) ? ("Voice_Berserk_" + PlayerNonSaveDataManager.selectSexBattleHeroineId) : ((sexTouchStatusManager.heroineSexLvStage != 0) ? ("Voice_Berserk_" + PlayerNonSaveDataManager.selectSexBattleHeroineId) : ("Voice_VictoryPiston_" + PlayerNonSaveDataManager.selectSexBattleHeroineId)));
				MasterAudio.PlaySound(text, 1f, null, 0.2f, null, null);
				countTime = 0f;
			}
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
