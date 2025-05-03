using Arbor;
using DarkTonic.MasterAudio;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class CheckDungeonBattleEnd : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	private DungeonItemManager dungeonItemManager;

	private bool isCharge;

	public StateLink continueLink;

	public StateLink victoryLink;

	public StateLink failedLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = GameObject.Find("Dungeon Battle Manager").GetComponent<DungeonBattleManager>();
		dungeonItemManager = GameObject.Find("Dungeon Item Manager").GetComponent<DungeonItemManager>();
	}

	public override void OnStateBegin()
	{
		dungeonBattleManager.isSkillAttack = false;
		if (PlayerStatusDataManager.playerAllHp <= 0 && !dungeonBattleManager.isRetreat)
		{
			Debug.Log("ダンジョン戦闘／味方全滅");
			dungeonBattleManager.dungeonBattleCanvas.GetComponent<CanvasGroup>().interactable = false;
			dungeonItemManager.useItemWindowGo.SetActive(value: false);
			dungeonBattleManager.itemWaitSlider.GetComponent<DungeonCharacterAgility>().isCoroutineStop = true;
			dungeonBattleManager.StopAglityCoroutine();
			Transition(failedLink);
		}
		else if (PlayerStatusDataManager.enemyAllHp <= 0 && !dungeonBattleManager.isRetreat)
		{
			Debug.Log("ダンジョン戦闘／エネミー全滅");
			dungeonBattleManager.dungeonBattleCanvas.GetComponent<CanvasGroup>().interactable = false;
			dungeonItemManager.useItemWindowGo.SetActive(value: false);
			dungeonBattleManager.itemWaitSlider.GetComponent<DungeonCharacterAgility>().isCoroutineStop = true;
			dungeonBattleManager.StopAglityCoroutine();
			Transition(victoryLink);
		}
		else
		{
			Debug.Log("ダンジョン戦闘／まだどちらも生存");
			CheckSpCharge();
		}
		if (!isCharge)
		{
			Transition(continueLink);
		}
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

	private void CheckSpCharge()
	{
		isCharge = false;
		if (!PlayerDataManager.isDungeonHeroineFollow)
		{
			return;
		}
		if (!dungeonBattleManager.agilityContainer.GetStringList("AgilityQueueList").Contains("c0"))
		{
			if (PlayerStatusDataManager.characterSp[PlayerDataManager.DungeonHeroineFollowNum] >= 100 && !dungeonBattleManager.chargetAttackButton.activeInHierarchy)
			{
				isCharge = true;
				dungeonBattleManager.chargetAttackButton.SetActive(value: true);
				MasterAudio.PlaySound("SePlayerCharge", 1f, null, 0f, null, null);
				Transform transform = PoolManager.Pools["DungeonBattleEffect"].Spawn(dungeonBattleManager.chargeActiveEffectPrefabGo, dungeonBattleManager.uIParticle_Charge.transform);
				dungeonBattleManager.chargeActiveEffectSpawnGo = transform;
				transform.localPosition = new Vector3(0f, 0f, 0f);
				transform.localScale = new Vector3(1f, 1f, 1f);
				dungeonBattleManager.uIParticle_Charge.RefreshParticles();
				Invoke("InvokeMethod", 0.6f);
			}
		}
		else
		{
			Debug.Log("すでにチャージキューがある");
		}
	}

	private void InvokeMethod()
	{
		PoolManager.Pools["DungeonBattleEffect"].Despawn(dungeonBattleManager.chargeActiveEffectSpawnGo, 0f, dungeonBattleManager.poolManagerGO);
		dungeonBattleManager.uIParticle_Charge.RefreshParticles();
		Transition(continueLink);
	}
}
