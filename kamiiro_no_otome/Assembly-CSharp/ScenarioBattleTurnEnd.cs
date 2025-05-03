using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ScenarioBattleTurnEnd : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleUiManager scenarioBattleUiManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
		scenarioBattleUiManager = GameObject.Find("Battle Ui Manager").GetComponent<ScenarioBattleUiManager>();
	}

	public override void OnStateBegin()
	{
		for (int i = 0; i < utageBattleSceneManager.isCharacterButtonSetUp.Count; i++)
		{
			utageBattleSceneManager.isCharacterButtonSetUp[i] = false;
		}
		for (int j = 0; j < utageBattleSceneManager.isEnemyGroupSetUp.Count; j++)
		{
			utageBattleSceneManager.isEnemyGroupSetUp[j] = false;
		}
		for (int k = 0; k < PlayerStatusDataManager.enemyChargeTurnList.Count; k++)
		{
			if (!scenarioBattleTurnManager.useSkillEnemyMemberNumList.Contains(k))
			{
				PlayerStatusDataManager.enemyChargeTurnList[k]++;
				int max = PlayerStatusDataManager.enemyMaxChargeTurnList[k];
				Mathf.Clamp(PlayerStatusDataManager.enemyChargeTurnList[k], 0, max);
			}
			utageBattleSceneManager.enemyButtonGoList[k].GetComponent<ArborFSM>().SendTrigger("ResetEnemyCharge");
		}
		PlayerBattleConditionAccess.ReCalcPlayerSkillRecharge();
		PlayerBattleConditionAccess.ReCalcBuffContinutyTurn();
		PlayerBattleConditionAccess.ReCalcBadStateTurn(isScenairoBattle: true);
		for (int l = 0; l < scenarioBattleTurnManager.isUseSkillCheckArray.Length; l++)
		{
			scenarioBattleTurnManager.isUseSkillCheckArray[l] = false;
		}
		for (int m = 0; m < PlayerBattleConditionManager.playerIsDead.Count; m++)
		{
			if (!PlayerBattleConditionManager.playerIsDead[m].isDead)
			{
				int memberNum = PlayerBattleConditionManager.playerIsDead[m].memberNum;
				int memberID = PlayerBattleConditionManager.playerIsDead[m].memberID;
				int num = PlayerStatusDataManager.characterMaxMp[PlayerStatusDataManager.playerPartyMember[memberNum]];
				float num2 = (float)PlayerStatusDataManager.characterMpRecoveryRate[memberID] / 100f;
				int num3 = Mathf.FloorToInt((float)num * num2);
				Debug.Log("最大MP：" + num + "／回復率：" + num2 + "／回復量：" + num3);
				int num4 = 0;
				int num5 = PlayerEquipDataManager.equipArmorRecoveryMP[memberID];
				if (num5 != 0)
				{
					num4 = Mathf.FloorToInt(num * (num5 / 100));
					num3 += num4;
					Debug.Log("仲間の防具のMP回復量：" + num4);
				}
				PlayerStatusDataManager.characterMp[PlayerStatusDataManager.playerPartyMember[memberNum]] = Mathf.Clamp(PlayerStatusDataManager.characterMp[PlayerStatusDataManager.playerPartyMember[memberNum]] += num3, 0, num);
				Debug.Log("ターン終了時にMPを回復／パーティnum：" + m + "／回復量：" + num3);
			}
		}
		int num6 = PlayerStatusDataManager.enemyLv.Max();
		for (int n = 0; n < PlayerBattleConditionManager.playerIsDead.Count; n++)
		{
			if (PlayerBattleConditionManager.playerIsDead[n].memberID != 0 && !PlayerBattleConditionManager.playerIsDead[n].isDead && PlayerStatusDataManager.playerPartyMember.Length > 1)
			{
				int memberNum2 = PlayerBattleConditionManager.playerIsDead[n].memberNum;
				int num7 = PlayerStatusDataManager.characterLv[PlayerStatusDataManager.playerPartyMember[memberNum2]];
				int num8 = Mathf.FloorToInt(num6 / num7);
				int value = Mathf.FloorToInt(15f * (float)num8);
				value = Mathf.Clamp(value, 15, 40);
				PlayerStatusDataManager.characterSp[PlayerStatusDataManager.playerPartyMember[memberNum2]] = Mathf.Clamp(PlayerStatusDataManager.characterSp[PlayerStatusDataManager.playerPartyMember[memberNum2]] += value, 0, 100);
				Debug.Log("ターン終了時にSPを増加／パーティnum：" + memberNum2 + "／増加量：" + value);
			}
		}
		for (int num9 = 0; num9 < PlayerBattleConditionManager.playerIsDead.Count; num9++)
		{
			if (PlayerBattleConditionManager.playerIsDead[num9].memberID != 0 && !PlayerBattleConditionManager.playerIsDead[num9].isDead && PlayerStatusDataManager.playerPartyMember.Length > 1)
			{
				int memberID2 = PlayerBattleConditionManager.playerIsDead[num9].memberID;
				int memberNum3 = PlayerBattleConditionManager.playerIsDead[num9].memberNum;
				int num10 = scenarioBattleTurnManager.enemyNormalAttackDamageList[memberNum3] + scenarioBattleTurnManager.enemySkillAttackDamageList[memberNum3];
				if (num10 > 0 && memberID2 != 0)
				{
					float num11 = (float)num10 / (float)PlayerStatusDataManager.characterMaxHp[memberID2];
					int num12 = Mathf.FloorToInt(num11 * 100f / 2f);
					PlayerStatusDataManager.characterSp[memberID2] = Mathf.Clamp(PlayerStatusDataManager.characterSp[memberID2] += num12, 0, 100);
					Debug.Log("被ダメージ分のSPを増加／ID：" + memberID2 + "／ダメージ：" + num10 + "／増加量：" + num12 + "/比率：" + num11 + "/最大HP：" + PlayerStatusDataManager.characterMaxHp[memberID2]);
				}
				else
				{
					Debug.Log("被ダメージ分のSPを増加／ID：" + memberID2 + "／ダメージ：" + num10 + "／増加なし");
				}
			}
		}
		for (int num13 = 0; num13 < PlayerStatusDataManager.playerPartyMember.Length; num13++)
		{
			utageBattleSceneManager.playerFrameGoList[num13].GetComponent<ArborFSM>().SendTrigger("ConsumeCharacterSp");
		}
		scenarioBattleTurnManager.battleUseItemCount = 0;
		scenarioBattleTurnManager.itemEnableUseCountText.text = scenarioBattleTurnManager.battleEnableUseItemMaxNum.ToString();
		utageBattleSceneManager.itemButton.GetComponent<ArborFSM>().SendTrigger("CheckItemUseCount");
		utageBattleSceneManager.battleTopText.SetActive(value: false);
		GameObject[] battleTextArray = utageBattleSceneManager.battleTextArray2;
		for (int num14 = 0; num14 < battleTextArray.Length; num14++)
		{
			battleTextArray[num14].SetActive(value: false);
		}
		battleTextArray = utageBattleSceneManager.battleTextArray3;
		for (int num14 = 0; num14 < battleTextArray.Length; num14++)
		{
			battleTextArray[num14].SetActive(value: false);
		}
		battleTextArray = utageBattleSceneManager.battleTextArray4;
		for (int num14 = 0; num14 < battleTextArray.Length; num14++)
		{
			battleTextArray[num14].SetActive(value: false);
		}
		utageBattleSceneManager.battleTextPanel.SetActive(value: false);
		scenarioBattleTurnManager.playerAttackCount = 0;
		scenarioBattleTurnManager.enemyAttackCount = 0;
		scenarioBattleTurnManager.battleUseSkillID = 0;
		scenarioBattleTurnManager.isUseSkillPlayer = false;
		scenarioBattleTurnManager.useSkillEnemyMemberNumList.Clear();
		scenarioBattleTurnManager.enemyNormalAttackDamageList = new List<int> { 0, 0, 0, 0, 0 };
		scenarioBattleTurnManager.enemySkillAttackDamageList = new List<int> { 0, 0, 0, 0, 0 };
		scenarioBattleTurnManager.isSupportHeal = false;
		scenarioBattleTurnManager.setFrameTypeName = "reset";
		scenarioBattleUiManager.uiFSM.SendTrigger("SetCharacterFrameType");
		scenarioBattleTurnManager.elapsedTurnCount++;
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
