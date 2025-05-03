using Arbor;
using DarkTonic.MasterAudio;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class CheckChargeButtonInitialize : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	public bool isBattleInitialize;

	private bool isCharge;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
	}

	public override void OnStateBegin()
	{
		isCharge = false;
		ScenarioBattleData scenarioBattleData = GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData data) => data.scenarioName == PlayerNonSaveDataManager.resultScenarioName);
		if (scenarioBattleData.supportBattleCharacterID != int.MaxValue && scenarioBattleData.isSupportCharacterTakeDamage)
		{
			int[] memberArray = scenarioBattleData.battleCharacterID.ToArray();
			int i;
			for (i = 0; i < memberArray.Length; i++)
			{
				if (PlayerStatusDataManager.characterSp[memberArray[i]] >= 100)
				{
					if (!PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == memberArray[i]).isDead)
					{
						isCharge = true;
						Debug.Log("チャージ確認／チャージ済みID：" + memberArray[i]);
						break;
					}
					Debug.Log("チャージ確認／チャージ済み死亡ID：" + memberArray[i]);
				}
				else
				{
					isCharge = false;
					Debug.Log("チャージ確認／未チャージ：" + memberArray[i]);
				}
			}
		}
		else
		{
			int j;
			for (j = 0; j < PlayerStatusDataManager.playerPartyMember.Length; j++)
			{
				if (PlayerStatusDataManager.characterSp[PlayerStatusDataManager.playerPartyMember[j]] >= 100)
				{
					if (!PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == PlayerStatusDataManager.playerPartyMember[j]).isDead)
					{
						isCharge = true;
						Debug.Log("チャージ確認／チャージ済みID：" + PlayerStatusDataManager.playerPartyMember[j]);
						break;
					}
					Debug.Log("チャージ確認／チャージ済み死亡ID：" + PlayerStatusDataManager.playerPartyMember[j]);
				}
				else
				{
					isCharge = false;
					Debug.Log("チャージ確認／未チャージ：" + PlayerStatusDataManager.playerPartyMember[j]);
				}
			}
		}
		if (isCharge)
		{
			utageBattleSceneManager.chargeAttackButton.SetActive(isCharge);
			utageBattleSceneManager.chargeAttackEffectGo.SetActive(isCharge);
			if (!isBattleInitialize)
			{
				MasterAudio.PlaySound("SePlayerCharge", 1f, null, 0f, null, null);
				Transform transform = PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.chargeEffectPrefabGo, utageBattleSceneManager.uIParticle_Charge.transform);
				utageBattleSceneManager.chargeEffectSpawnGo = transform;
				transform.localPosition = new Vector3(0f, 0f, 0f);
				transform.localScale = new Vector3(1.5f, 1.5f, 1f);
				Invoke("InvokeMethod", 0.6f);
			}
			else
			{
				Transition(stateLink);
			}
		}
		else
		{
			utageBattleSceneManager.chargeAttackButton.SetActive(isCharge);
			utageBattleSceneManager.chargeAttackEffectGo.SetActive(isCharge);
			Transition(stateLink);
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

	private void InvokeMethod()
	{
		PoolManager.Pools["BattleEffect"].Despawn(utageBattleSceneManager.chargeEffectSpawnGo, 0f, utageBattleSceneManager.poolManagerGO);
		utageBattleSceneManager.uIParticle_Charge.RefreshParticles();
		Transition(stateLink);
	}
}
