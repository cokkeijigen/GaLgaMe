using Arbor;
using I2.Loc;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class SetScenarioBattleMissText : StateBehaviour
{
	public enum Type
	{
		player,
		enemy
	}

	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public Type type;

	public float waitTime;

	private Transform poolGO;

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
		switch (type)
		{
		case Type.player:
		{
			Transform transform2 = utageBattleSceneManager.enemyImageGoList[scenarioBattleTurnManager.playerTargetNum].transform;
			utageBattleSceneManager.damagePointRect[0].position = new Vector3(transform2.position.x, 0f, 0f);
			poolGO = PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[2], utageBattleSceneManager.damagePointRect[0]);
			break;
		}
		case Type.enemy:
		{
			Transform transform = utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.enemyTargetNum].transform;
			utageBattleSceneManager.damagePointRect[0].position = new Vector3(transform.position.x, -2f, 0f);
			poolGO = PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[2], utageBattleSceneManager.damagePointRect[0]);
			break;
		}
		}
		poolGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
		PoolManager.Pools["BattleEffect"].Despawn(poolGO, despawnTime, utageBattleSceneManager.poolManagerGO);
		utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = "battleTextAttackMiss";
		utageBattleSceneManager.battleTextArray3[0].SetActive(value: true);
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
