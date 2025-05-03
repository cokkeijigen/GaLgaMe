using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using I2.Loc;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class SetScenarioAttackParalyzeText : StateBehaviour
{
	public enum Type
	{
		player,
		enemy
	}

	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public Type type;

	private Transform poolGO;

	public float waitTime;

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
		int index = 0;
		switch (type)
		{
		case Type.player:
		{
			index = PlayerBattleConditionManager.playerIsDead[scenarioBattleTurnManager.playerAttackCount].memberNum;
			Transform transform2 = utageBattleSceneManager.playerFrameGoList[index].transform;
			utageBattleSceneManager.damagePointRect[0].position = new Vector3(transform2.position.x, 0f, 0f);
			poolGO = PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[5], utageBattleSceneManager.damagePointRect[0]);
			break;
		}
		case Type.enemy:
		{
			index = PlayerBattleConditionManager.enemyIsDead[scenarioBattleTurnManager.enemyAttackCount].memberNum;
			Transform transform = utageBattleSceneManager.enemyImageGoList[index].transform;
			utageBattleSceneManager.damagePointRect[0].position = new Vector3(transform.position.x, -2f, 0f);
			poolGO = PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[5], utageBattleSceneManager.damagePointRect[0]);
			break;
		}
		}
		poolGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
		float seconds = despawnTime / (float)utageBattleSceneManager.battleSpeed;
		PoolManager.Pools["BattleEffect"].Despawn(poolGO, seconds, utageBattleSceneManager.poolManagerGO);
		utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = "battleTextAttackParalyze";
		utageBattleSceneManager.battleTextGroupArray[2].SetActive(value: true);
		utageBattleSceneManager.battleTextArray3[0].SetActive(value: true);
		MasterAudio.PlaySound("SeAttackParalyze", 1f, null, 0f, null, null);
		switch (type)
		{
		case Type.player:
		{
			float duration = 0.1f;
			float strength = 20f;
			int vibrato = 2;
			utageBattleSceneManager.playerFrameGoList[index].GetComponent<RectTransform>().DOShakeAnchorPos(duration, strength, vibrato, 10f, snapping: false, fadeOut: false);
			break;
		}
		case Type.enemy:
		{
			float duration = 0.1f;
			float strength = 20f;
			int vibrato = 2;
			utageBattleSceneManager.enemyImageGoList[index].GetComponent<RectTransform>().DOShakeAnchorPos(duration, strength, vibrato, 10f, snapping: false, fadeOut: false);
			break;
		}
		}
		float time = waitTime / (float)utageBattleSceneManager.battleSpeed;
		Invoke("InvokeMethod", time);
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

	private void InvokeMethod()
	{
		Transition(stateLink);
	}
}
