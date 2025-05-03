using System.Collections.Generic;
using System.Linq;
using Arbor;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class CheckSkillTargetDead : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	public StateLink aliveLink;

	public StateLink playerDeathLink;

	public StateLink enemyDeathLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
	}

	public override void OnStateBegin()
	{
		bool flag = false;
		bool flag2 = false;
		List<int> list = new List<int>();
		BattleSkillData useSkillData = scenarioBattleTurnManager.useSkillData;
		list.Clear();
		if (useSkillData.skillType == BattleSkillData.SkillType.attack || useSkillData.skillType == BattleSkillData.SkillType.magicAttack || useSkillData.skillType == BattleSkillData.SkillType.mixAttack)
		{
			if (scenarioBattleTurnManager.isUseSkillPlayer)
			{
				if (scenarioBattleTurnManager.isAllTargetSkill)
				{
					if (scenarioBattleTurnManager.skillAttackHitDataList.Select((SkillAttackHitData data) => data.isAttackHit).Any())
					{
						for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
						{
							int num = scenarioBattleTurnManager.skillAttackHitDataList[i].memberNum;
							int id = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == num).memberID;
							if (PlayerStatusDataManager.characterHp[id] <= 0)
							{
								PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == id).isDead = true;
								scenarioBattleTurnManager.SetMiniFrameInteractable(utageBattleSceneManager.playerFrameGoList[i], isEnable: false, num);
								PlayerBattleConditionAccess.ResetTargetBuffCondtion("player", num);
								PlayerBattleConditionAccess.ResetTargetBadState("player", num);
								ParameterContainer component = utageBattleSceneManager.playerFrameGoList[i].GetComponent<ParameterContainer>();
								ResetBuffIcon(component);
								list.Add(num);
								Debug.Log("全体攻撃で味方死亡／num：" + num + "／ID：" + id);
							}
						}
						if (list.Any())
						{
							if (list.Count > 1)
							{
								string allTargetTerm = scenarioBattleSkillManager.GetAllTargetTerm();
								utageBattleSceneManager.battleTextArray4[0].GetComponent<Localize>().Term = allTargetTerm;
							}
							else
							{
								utageBattleSceneManager.battleTextArray4[0].GetComponent<Localize>().Term = "character" + list[0];
							}
							utageBattleSceneManager.battleTextArray4[1].GetComponent<Localize>().Term = "battleTextPlayerDeath";
							utageBattleSceneManager.battleTextArray4[0].SetActive(value: true);
							utageBattleSceneManager.battleTextArray4[1].SetActive(value: true);
							flag = true;
						}
					}
				}
				else
				{
					int num2 = PlayerStatusDataManager.playerPartyMember[scenarioBattleTurnManager.enemyTargetNum];
					if (PlayerStatusDataManager.characterHp[scenarioBattleTurnManager.enemyTargetNum] <= 0)
					{
						PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.enemyTargetNum).isDead = true;
						int enemyTargetNum = scenarioBattleTurnManager.enemyTargetNum;
						scenarioBattleTurnManager.SetMiniFrameInteractable(utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.enemyTargetNum], isEnable: false, enemyTargetNum);
						PlayerBattleConditionAccess.ResetTargetBuffCondtion("player", scenarioBattleTurnManager.enemyTargetNum);
						PlayerBattleConditionAccess.ResetTargetBadState("player", scenarioBattleTurnManager.enemyTargetNum);
						ParameterContainer component2 = utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.enemyTargetNum].GetComponent<ParameterContainer>();
						ResetBuffIcon(component2);
						utageBattleSceneManager.battleTextArray4[0].GetComponent<Localize>().Term = "character" + num2;
						utageBattleSceneManager.battleTextArray4[1].GetComponent<Localize>().Term = "battleTextPlayerDeath";
						utageBattleSceneManager.battleTextArray4[0].SetActive(value: true);
						utageBattleSceneManager.battleTextArray4[1].SetActive(value: true);
						flag = true;
						Debug.Log("単体攻撃で味方死亡／num：" + enemyTargetNum + "／ID：" + num2);
					}
				}
			}
			else if (scenarioBattleTurnManager.isAllTargetSkill)
			{
				if (scenarioBattleTurnManager.skillAttackHitDataList.Select((SkillAttackHitData data) => data.isAttackHit).Any())
				{
					for (int j = 0; j < scenarioBattleTurnManager.skillAttackHitDataList.Count; j++)
					{
						int num3 = scenarioBattleTurnManager.skillAttackHitDataList[j].memberNum;
						if (PlayerStatusDataManager.enemyHp[num3] <= 0)
						{
							PlayerBattleConditionManager.enemyIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == num3).isDead = true;
							if (scenarioBattleTurnManager.playerFocusTargetNum == num3)
							{
								utageBattleSceneManager.SetPlayerFocusTarget(num3);
							}
							CanvasGroup component3 = utageBattleSceneManager.enemyButtonGoList[num3].GetComponent<CanvasGroup>();
							component3.alpha = 0.5f;
							component3.interactable = false;
							utageBattleSceneManager.enemyImageGoList[num3].GetComponent<CanvasGroup>().alpha = 0f;
							PlayerBattleConditionAccess.ResetTargetBuffCondtion("enemy", num3);
							PlayerBattleConditionAccess.ResetTargetBadState("enemy", num3);
							ParameterContainer component4 = utageBattleSceneManager.enemyButtonGoList[j].GetComponent<ParameterContainer>();
							ResetBuffIcon(component4);
							PlayerStatusDataManager.enemyChargeTurnList[num3] = 0;
							component4.GetGameObject("maxEffectGo").SetActive(value: false);
							for (int k = 0; k < component4.GetInt("maxChargeNum"); k++)
							{
								component4.GetGameObjectList("ChargeImageGoList")[k].GetComponent<Image>().sprite = scenarioBattleSkillManager.enemyChargeSpriteArray[1];
							}
							list.Add(num3);
							Debug.Log("全体攻撃でエネミー死亡／num：" + num3 + "／ID：" + PlayerStatusDataManager.enemyMember[num3]);
						}
					}
					if (list.Any())
					{
						if (list.Count > 1)
						{
							utageBattleSceneManager.battleTextArray4[0].GetComponent<Localize>().Term = "battleTextEnemyAllTarget";
						}
						else
						{
							utageBattleSceneManager.battleTextArray4[0].GetComponent<Localize>().Term = "enemy" + list[0];
						}
						utageBattleSceneManager.battleTextArray4[1].GetComponent<Localize>().Term = "battleTextEnemyDeath";
						utageBattleSceneManager.battleTextArray4[0].SetActive(value: true);
						utageBattleSceneManager.battleTextArray4[1].SetActive(value: true);
						flag2 = true;
					}
				}
			}
			else
			{
				int num2 = PlayerStatusDataManager.enemyMember[scenarioBattleTurnManager.playerTargetNum];
				if (PlayerStatusDataManager.enemyHp[scenarioBattleTurnManager.playerTargetNum] <= 0)
				{
					PlayerBattleConditionManager.enemyIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.playerTargetNum).isDead = true;
					if (scenarioBattleTurnManager.playerFocusTargetNum == scenarioBattleTurnManager.playerTargetNum)
					{
						utageBattleSceneManager.SetPlayerFocusTarget(scenarioBattleTurnManager.playerTargetNum);
					}
					CanvasGroup component5 = utageBattleSceneManager.enemyButtonGoList[scenarioBattleTurnManager.playerTargetNum].GetComponent<CanvasGroup>();
					component5.alpha = 0.5f;
					component5.interactable = false;
					utageBattleSceneManager.enemyImageGoList[scenarioBattleTurnManager.playerTargetNum].GetComponent<CanvasGroup>().alpha = 0f;
					PlayerBattleConditionAccess.ResetTargetBuffCondtion("enemy", scenarioBattleTurnManager.playerTargetNum);
					PlayerBattleConditionAccess.ResetTargetBadState("enemy", scenarioBattleTurnManager.playerTargetNum);
					ParameterContainer component6 = utageBattleSceneManager.enemyButtonGoList[scenarioBattleTurnManager.playerTargetNum].GetComponent<ParameterContainer>();
					ResetBuffIcon(component6);
					PlayerStatusDataManager.enemyChargeTurnList[scenarioBattleTurnManager.playerTargetNum] = 0;
					component6.GetGameObject("maxEffectGo").SetActive(value: false);
					for (int l = 0; l < component6.GetInt("maxChargeNum"); l++)
					{
						component6.GetGameObjectList("ChargeImageGoList")[l].GetComponent<Image>().sprite = scenarioBattleSkillManager.enemyChargeSpriteArray[1];
					}
					utageBattleSceneManager.battleTextArray4[0].GetComponent<Localize>().Term = "enemy" + num2;
					utageBattleSceneManager.battleTextArray4[1].GetComponent<Localize>().Term = "battleTextEnemyDeath";
					utageBattleSceneManager.battleTextArray4[0].SetActive(value: true);
					utageBattleSceneManager.battleTextArray4[1].SetActive(value: true);
					flag2 = true;
					Debug.Log("単体攻撃でエネミー死亡／num：" + scenarioBattleTurnManager.playerTargetNum + "／ID：" + num2);
				}
			}
			if (flag)
			{
				Debug.Log("味方が死んでいる");
				Transition(playerDeathLink);
			}
			else if (flag2)
			{
				Debug.Log("エネミーが死んでいる");
				Transition(enemyDeathLink);
			}
			else
			{
				Debug.Log("誰も死んでいない");
				Transition(aliveLink);
			}
		}
		else
		{
			Debug.Log("攻撃スキルではない");
			Transition(aliveLink);
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

	private void ResetBuffIcon(ParameterContainer parameterContainer)
	{
		foreach (GameObject gameObject in parameterContainer.GetGameObjectList("buffImageGoList"))
		{
			gameObject.SetActive(value: false);
		}
		foreach (GameObject gameObject2 in parameterContainer.GetGameObjectList("badStateImageGoList"))
		{
			gameObject2.SetActive(value: false);
		}
	}
}
