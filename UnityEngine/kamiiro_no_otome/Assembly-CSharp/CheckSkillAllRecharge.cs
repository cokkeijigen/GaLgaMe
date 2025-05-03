using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class CheckSkillAllRecharge : StateBehaviour
{
	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	private ParameterContainer parameterContainer;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
		parameterContainer = GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		int id = parameterContainer.GetInt("characterID");
		bool isDead = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == id).isDead;
		List<PlayerBattleConditionManager.MemberSkillReChargeTurn> list = PlayerBattleConditionManager.playerSkillRechargeTurn[id].Where((PlayerBattleConditionManager.MemberSkillReChargeTurn data) => data.needRechargeTurn == 0).ToList();
		if (id == 0)
		{
			if (isDead)
			{
				parameterContainer.GetGameObject("skillButton").GetComponent<Image>().sprite = scenarioBattleSkillManager.skillButtonSpriteArray[2];
				parameterContainer.GetVariable<UguiImage>("skillIconImage").image.sprite = scenarioBattleSkillManager.skillButtonIconSpriteArray[0];
				parameterContainer.GetGameObject("skillButton").GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f);
				parameterContainer.GetVariable<UguiImage>("skillIconImage").image.color = new Color(0.3f, 0.3f, 0.3f);
				parameterContainer.GetVariable<I2LocalizeComponent>("chargeTextLoc").localize.Term = "skillButton_enable";
				parameterContainer.GetGameObject("chargeCircleGo").SetActive(value: false);
				Debug.Log("スキルボタン死亡中");
			}
			else if (PlayerNonSaveDataManager.resultScenarioName == "M_Main_001-1")
			{
				CheckNormalRecharge(list, id, isDead);
			}
			else
			{
				List<PlayerBattleConditionManager.MemberSkillReChargeTurn> source = PlayerBattleConditionManager.playerSkillRechargeTurn[PlayerStatusDataManager.partyMemberCount].Where((PlayerBattleConditionManager.MemberSkillReChargeTurn data) => data.needRechargeTurn == 0).ToList();
				if (list.Any() || source.Any())
				{
					if (scenarioBattleTurnManager.isUseSkillCheckArray[id])
					{
						parameterContainer.GetGameObject("skillButton").GetComponent<Image>().sprite = scenarioBattleSkillManager.skillButtonSpriteArray[2];
						parameterContainer.GetVariable<UguiImage>("skillIconImage").image.sprite = scenarioBattleSkillManager.skillButtonIconSpriteArray[0];
						parameterContainer.GetGameObject("skillButton").GetComponent<Image>().color = Color.white;
						parameterContainer.GetVariable<UguiImage>("skillIconImage").image.color = new Color(0.85f, 0.85f, 0.85f);
						parameterContainer.GetVariable<I2LocalizeComponent>("chargeTextLoc").localize.Term = "skillButton_enable";
						parameterContainer.GetGameObject("chargeCircleGo").SetActive(value: false);
						Debug.Log("エデン契約後／スキルボタン使用済み");
					}
					else
					{
						parameterContainer.GetGameObject("skillButton").GetComponent<Image>().sprite = scenarioBattleSkillManager.skillButtonSpriteArray[0];
						parameterContainer.GetVariable<UguiImage>("skillIconImage").image.sprite = scenarioBattleSkillManager.skillButtonIconSpriteArray[0];
						parameterContainer.GetGameObject("skillButton").GetComponent<Image>().color = Color.white;
						parameterContainer.GetVariable<UguiImage>("skillIconImage").image.color = new Color(1f, 1f, 1f, 1f);
						parameterContainer.GetVariable<I2LocalizeComponent>("chargeTextLoc").localize.Term = "skillButton_enable";
						parameterContainer.GetGameObject("chargeCircleGo").SetActive(value: true);
						Debug.Log("エデン契約後／使用可能なスキルあり");
					}
				}
				else
				{
					parameterContainer.GetGameObject("skillButton").GetComponent<Image>().sprite = scenarioBattleSkillManager.skillButtonSpriteArray[1];
					parameterContainer.GetVariable<UguiImage>("skillIconImage").image.sprite = scenarioBattleSkillManager.skillButtonIconSpriteArray[1];
					parameterContainer.GetGameObject("skillButton").GetComponent<Image>().color = Color.white;
					parameterContainer.GetVariable<UguiImage>("skillIconImage").image.color = new Color(0.6f, 0.6f, 0.6f);
					parameterContainer.GetVariable<I2LocalizeComponent>("chargeTextLoc").localize.Term = "skillButton_allReCharge";
					parameterContainer.GetGameObject("chargeCircleGo").SetActive(value: false);
					Debug.Log("エデン契約後／スキルは全部チャージ中");
				}
			}
		}
		else
		{
			CheckNormalRecharge(list, id, isDead);
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

	private void CheckNormalRecharge(List<PlayerBattleConditionManager.MemberSkillReChargeTurn> list, int memberId, bool isDead)
	{
		if (isDead)
		{
			parameterContainer.GetGameObject("skillButton").GetComponent<Image>().sprite = scenarioBattleSkillManager.skillButtonSpriteArray[2];
			parameterContainer.GetVariable<UguiImage>("skillIconImage").image.sprite = scenarioBattleSkillManager.skillButtonIconSpriteArray[0];
			parameterContainer.GetGameObject("skillButton").GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f);
			parameterContainer.GetVariable<UguiImage>("skillIconImage").image.color = new Color(0.3f, 0.3f, 0.3f);
			parameterContainer.GetVariable<I2LocalizeComponent>("chargeTextLoc").localize.Term = "skillButton_enable";
			parameterContainer.GetGameObject("chargeCircleGo").SetActive(value: false);
			Debug.Log("スキルボタン死亡中");
		}
		else if (list.Any())
		{
			if (scenarioBattleTurnManager.isUseSkillCheckArray[memberId])
			{
				parameterContainer.GetGameObject("skillButton").GetComponent<Image>().sprite = scenarioBattleSkillManager.skillButtonSpriteArray[2];
				parameterContainer.GetVariable<UguiImage>("skillIconImage").image.sprite = scenarioBattleSkillManager.skillButtonIconSpriteArray[0];
				parameterContainer.GetGameObject("skillButton").GetComponent<Image>().color = Color.white;
				parameterContainer.GetVariable<UguiImage>("skillIconImage").image.color = new Color(0.85f, 0.85f, 0.85f);
				parameterContainer.GetVariable<I2LocalizeComponent>("chargeTextLoc").localize.Term = "skillButton_enable";
				parameterContainer.GetGameObject("chargeCircleGo").SetActive(value: false);
				Debug.Log("スキルボタン使用済み");
			}
			else
			{
				parameterContainer.GetGameObject("skillButton").GetComponent<Image>().sprite = scenarioBattleSkillManager.skillButtonSpriteArray[0];
				parameterContainer.GetVariable<UguiImage>("skillIconImage").image.sprite = scenarioBattleSkillManager.skillButtonIconSpriteArray[0];
				parameterContainer.GetGameObject("skillButton").GetComponent<Image>().color = Color.white;
				parameterContainer.GetVariable<UguiImage>("skillIconImage").image.color = new Color(1f, 1f, 1f);
				parameterContainer.GetVariable<I2LocalizeComponent>("chargeTextLoc").localize.Term = "skillButton_enable";
				parameterContainer.GetGameObject("chargeCircleGo").SetActive(value: true);
				Debug.Log("使用可能なスキルあり");
			}
		}
		else
		{
			parameterContainer.GetGameObject("skillButton").GetComponent<Image>().sprite = scenarioBattleSkillManager.skillButtonSpriteArray[1];
			parameterContainer.GetVariable<UguiImage>("skillIconImage").image.sprite = scenarioBattleSkillManager.skillButtonIconSpriteArray[1];
			parameterContainer.GetGameObject("skillButton").GetComponent<Image>().color = Color.white;
			parameterContainer.GetVariable<UguiImage>("skillIconImage").image.color = new Color(0.6f, 0.6f, 0.6f);
			parameterContainer.GetVariable<I2LocalizeComponent>("chargeTextLoc").localize.Term = "skillButton_allReCharge";
			parameterContainer.GetGameObject("chargeCircleGo").SetActive(value: false);
			Debug.Log("スキルは全部チャージ中");
		}
	}
}
