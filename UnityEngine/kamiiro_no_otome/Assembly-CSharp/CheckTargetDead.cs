using System.Collections.Generic;
using System.Linq;
using Arbor;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class CheckTargetDead : StateBehaviour
{
	public enum Type
	{
		player,
		enemy
	}

	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	public Type type;

	public StateLink aliveLink;

	public StateLink deathLink;

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
		List<int> list = new List<int>();
		list.Clear();
		switch (type)
		{
		case Type.player:
		{
			if (scenarioBattleTurnManager.isAllTargetSkill)
			{
				if (!scenarioBattleTurnManager.skillAttackHitDataList.Select((SkillAttackHitData data) => data.isAttackHit).Any())
				{
					break;
				}
				for (int l = 0; l < scenarioBattleTurnManager.skillAttackHitDataList.Count; l++)
				{
					int memberNum = scenarioBattleTurnManager.skillAttackHitDataList[l].memberNum;
					int id = scenarioBattleTurnManager.skillAttackHitDataList[l].memberID;
					if (PlayerStatusDataManager.characterHp[id] <= 0)
					{
						PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == id).isDead = true;
						Debug.Log("キャラフレームを黒くする／num：" + memberNum);
						scenarioBattleTurnManager.SetMiniFrameInteractable(utageBattleSceneManager.playerFrameGoList[memberNum], isEnable: false, memberNum);
						PlayerBattleConditionAccess.ResetTargetBuffCondtion("player", memberNum);
						PlayerBattleConditionAccess.ResetTargetBadState("player", memberNum);
						ParameterContainer component7 = utageBattleSceneManager.playerFrameGoList[l].GetComponent<ParameterContainer>();
						ResetBuffIcon(component7);
						list.Add(id);
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
					SetInActiveBattleText();
					flag = true;
				}
				break;
			}
			int num2 = PlayerStatusDataManager.playerPartyMember[scenarioBattleTurnManager.enemyTargetNum];
			if (PlayerStatusDataManager.characterHp[num2] <= 0)
			{
				PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.enemyTargetNum).isDead = true;
				int enemyTargetNum = scenarioBattleTurnManager.enemyTargetNum;
				scenarioBattleTurnManager.SetMiniFrameInteractable(utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.enemyTargetNum], isEnable: false, enemyTargetNum);
				PlayerBattleConditionAccess.ResetTargetBuffCondtion("player", scenarioBattleTurnManager.enemyTargetNum);
				PlayerBattleConditionAccess.ResetTargetBadState("player", scenarioBattleTurnManager.enemyTargetNum);
				ParameterContainer component8 = utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.enemyTargetNum].GetComponent<ParameterContainer>();
				ResetBuffIcon(component8);
				utageBattleSceneManager.battleTextArray4[0].GetComponent<Localize>().Term = "character" + num2;
				utageBattleSceneManager.battleTextArray4[1].GetComponent<Localize>().Term = "battleTextPlayerDeath";
				utageBattleSceneManager.battleTextArray4[0].SetActive(value: true);
				utageBattleSceneManager.battleTextArray4[1].SetActive(value: true);
				SetInActiveBattleText();
				flag = true;
			}
			break;
		}
		case Type.enemy:
		{
			Debug.Log("味方の攻撃／死亡確認中");
			if (scenarioBattleTurnManager.isAllTargetSkill)
			{
				Debug.Log("味方の攻撃／死亡確認中／全員対象のスキル攻撃");
				if (!scenarioBattleTurnManager.skillAttackHitDataList.Select((SkillAttackHitData data) => data.isAttackHit).Any())
				{
					break;
				}
				for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
				{
					int num = scenarioBattleTurnManager.skillAttackHitDataList[i].memberNum;
					int memberID = scenarioBattleTurnManager.skillAttackHitDataList[i].memberID;
					if (PlayerStatusDataManager.enemyHp[num] <= 0)
					{
						PlayerBattleConditionManager.enemyIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == num).isDead = true;
						if (scenarioBattleTurnManager.playerFocusTargetNum == num)
						{
							utageBattleSceneManager.SetPlayerFocusTarget(num);
						}
						CanvasGroup component = utageBattleSceneManager.enemyButtonGoList[num].GetComponent<CanvasGroup>();
						component.alpha = 0.5f;
						component.interactable = false;
						CanvasGroup component2 = utageBattleSceneManager.enemyImageGoList[num].GetComponent<CanvasGroup>();
						component2.interactable = false;
						component2.alpha = 0f;
						PlayerBattleConditionAccess.ResetTargetBuffCondtion("enemy", num);
						PlayerBattleConditionAccess.ResetTargetBadState("enemy", num);
						ParameterContainer component3 = utageBattleSceneManager.enemyButtonGoList[i].GetComponent<ParameterContainer>();
						ResetBuffIcon(component3);
						PlayerStatusDataManager.enemyChargeTurnList[num] = 0;
						component3.GetGameObject("maxEffectGo").SetActive(value: false);
						for (int j = 0; j < component3.GetInt("maxChargeNum"); j++)
						{
							component3.GetGameObjectList("ChargeImageGoList")[j].GetComponent<Image>().sprite = scenarioBattleSkillManager.enemyChargeSpriteArray[1];
						}
						list.Add(memberID);
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
					SetInActiveBattleText();
					flag = true;
					Debug.Log("enemy" + list[0] + "：死亡／全体攻撃");
				}
				break;
			}
			int num2 = PlayerStatusDataManager.enemyMember[scenarioBattleTurnManager.playerTargetNum];
			Debug.Log("敵ID：" + num2 + "／敵num：" + scenarioBattleTurnManager.playerTargetNum + "／敵のHP：" + PlayerStatusDataManager.enemyHp[scenarioBattleTurnManager.playerTargetNum]);
			if (PlayerStatusDataManager.enemyHp[scenarioBattleTurnManager.playerTargetNum] <= 0)
			{
				PlayerBattleConditionManager.enemyIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.playerTargetNum).isDead = true;
				if (scenarioBattleTurnManager.playerFocusTargetNum == scenarioBattleTurnManager.playerTargetNum)
				{
					utageBattleSceneManager.SetPlayerFocusTarget(scenarioBattleTurnManager.playerTargetNum);
				}
				CanvasGroup component4 = utageBattleSceneManager.enemyButtonGoList[scenarioBattleTurnManager.playerTargetNum].GetComponent<CanvasGroup>();
				component4.alpha = 0.5f;
				component4.interactable = false;
				CanvasGroup component5 = utageBattleSceneManager.enemyImageGoList[scenarioBattleTurnManager.playerTargetNum].GetComponent<CanvasGroup>();
				component5.interactable = false;
				component5.alpha = 0f;
				PlayerBattleConditionAccess.ResetTargetBuffCondtion("enemy", scenarioBattleTurnManager.playerTargetNum);
				PlayerBattleConditionAccess.ResetTargetBadState("enemy", scenarioBattleTurnManager.playerTargetNum);
				ParameterContainer component6 = utageBattleSceneManager.enemyButtonGoList[scenarioBattleTurnManager.playerTargetNum].GetComponent<ParameterContainer>();
				ResetBuffIcon(component6);
				PlayerStatusDataManager.enemyChargeTurnList[scenarioBattleTurnManager.playerTargetNum] = 0;
				component6.GetGameObject("maxEffectGo").SetActive(value: false);
				for (int k = 0; k < component6.GetInt("maxChargeNum"); k++)
				{
					component6.GetGameObjectList("ChargeImageGoList")[k].GetComponent<Image>().sprite = scenarioBattleSkillManager.enemyChargeSpriteArray[1];
				}
				utageBattleSceneManager.battleTextArray4[0].GetComponent<Localize>().Term = "enemy" + num2;
				utageBattleSceneManager.battleTextArray4[1].GetComponent<Localize>().Term = "battleTextEnemyDeath";
				utageBattleSceneManager.battleTextArray4[0].SetActive(value: true);
				utageBattleSceneManager.battleTextArray4[1].SetActive(value: true);
				SetInActiveBattleText();
				flag = true;
				Debug.Log("enemy" + num2 + "：死亡／単体攻撃");
			}
			break;
		}
		}
		if (flag)
		{
			Debug.Log("誰かが死んでいる");
			Transition(deathLink);
		}
		else
		{
			Debug.Log("誰も死んでいない");
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

	private void SetInActiveBattleText()
	{
		utageBattleSceneManager.battleTextArray4[2].SetActive(value: false);
		utageBattleSceneManager.battleTextArray4[3].SetActive(value: false);
		utageBattleSceneManager.battleTextArray4[4].SetActive(value: false);
		utageBattleSceneManager.battleTextArray4[5].SetActive(value: false);
	}
}
