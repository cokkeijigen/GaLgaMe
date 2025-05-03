using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using I2.Loc;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class SetScenarioAttackMissText : StateBehaviour
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
		case Type.support:
		{
			index = scenarioBattleTurnManager.playerTargetNum;
			Transform transform2 = utageBattleSceneManager.enemyImageGoList[index].transform;
			utageBattleSceneManager.damagePointRect[0].position = new Vector3(transform2.position.x, 0f, 0f);
			poolGO = PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[2], utageBattleSceneManager.damagePointRect[0]);
			break;
		}
		case Type.enemy:
		{
			index = scenarioBattleTurnManager.enemyTargetNum;
			Transform transform = utageBattleSceneManager.playerFrameGoList[index].transform;
			utageBattleSceneManager.damagePointRect[0].position = new Vector3(transform.position.x, -2f, 0f);
			poolGO = PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[2], utageBattleSceneManager.damagePointRect[0]);
			break;
		}
		}
		poolGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
		float seconds = despawnTime / (float)utageBattleSceneManager.battleSpeed;
		PoolManager.Pools["BattleEffect"].Despawn(poolGO, seconds, utageBattleSceneManager.poolManagerGO);
		utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = "battleTextAttackMiss";
		utageBattleSceneManager.battleTextGroupArray[2].SetActive(value: true);
		utageBattleSceneManager.battleTextArray3[0].SetActive(value: true);
		MasterAudio.PlaySound("SeAttackMiss", 1f, null, 0f, null, null);
		int num = ((Random.Range(0, 1) > 0) ? 1 : (-1));
		switch (type)
		{
		case Type.player:
		case Type.support:
			utageBattleSceneManager.enemyImageGoList[index].transform.DOPunchPosition(new Vector3(30f * (float)num, 0f, 0f), 0.3f, 1);
			break;
		case Type.enemy:
			utageBattleSceneManager.playerFrameGoList[index].transform.DOPunchPosition(new Vector3(30f * (float)num, 0f, 0f), 0.3f, 1);
			break;
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
