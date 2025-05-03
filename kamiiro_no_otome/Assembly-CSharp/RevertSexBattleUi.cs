using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class RevertSexBattleUi : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	private SexBattleTurnManager sexBattleTurnManager;

	private SexBattleMessageTextManager sexBattleMessageTextManager;

	private SexBattleFertilizationManager sexBattleFertilizationManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		sexBattleTurnManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleTurnManager>();
		sexBattleMessageTextManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleMessageTextManager>();
		sexBattleFertilizationManager = GameObject.Find("SexBattle Fertilization Manager").GetComponent<SexBattleFertilizationManager>();
	}

	public override void OnStateBegin()
	{
		sexBattleManager.menuButtonGo.SetActive(value: true);
		sexBattleManager.cumShotToggleGo.SetActive(value: true);
		sexBattleManager.skillWindowGo.SetActive(value: false);
		sexBattleManager.blackImageGo.SetActive(value: false);
		sexBattleManager.skillButtonGroupGo.SetActive(value: true);
		sexBattleTurnManager.sexBattleTotalTurnCount++;
		sexBattleTurnManager.sexBattleTotalHeroineTranceNum += PlayerSexStatusDataManager.playerSexTrance[1];
		sexBattleManager.SetHeroineSprite("idle");
		PlayerSexBattleConditionAccess.ReCalcPlayerSexSkillRecharge();
		PlayerSexBattleConditionAccess.ReCalcSexBuffContinutyTurn();
		PlayerSexBattleConditionAccess.ReCalcSubPowerTurn();
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < sexBattleManager.skillButtonGroupParentArray[i].transform.childCount; j++)
			{
				ParameterContainer component = sexBattleManager.skillButtonGroupParentArray[i].transform.GetChild(j).GetComponent<ParameterContainer>();
				int skillID = component.GetInt("skillID");
				SexSkillData skillData = GameDataManager.instance.sexSkillDataBase.sexSkillDataList.Find((SexSkillData data) => data.skillID == skillID);
				int needRechargeTurn = PlayerSexStatusDataManager.playerSexSkillRechargeTurn[0].Find((PlayerSexStatusDataManager.MemberSexSkillReChargeTurn data) => data.skillID == skillID).needRechargeTurn;
				component.GetVariable<UguiTextVariable>("reChargeTurnText").text.text = needRechargeTurn.ToString();
				CheckButtonInteractable(skillData, i, j);
			}
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

	private void CheckButtonInteractable(SexSkillData skillData, int groupIndex, int childIndex)
	{
		if (PlayerSexStatusDataManager.playerSexSkillRechargeTurn[0].Find((PlayerSexStatusDataManager.MemberSexSkillReChargeTurn data) => data.skillID == skillData.skillID).needRechargeTurn > 0)
		{
			SetApplyButtonInterctable(isVisible: false, groupIndex, childIndex);
			return;
		}
		if (skillData.skillNeedTrance > PlayerSexStatusDataManager.playerSexTrance[0])
		{
			SetApplyButtonInterctable(isVisible: false, groupIndex, childIndex);
			return;
		}
		if (skillData.skillNeedRemainPlayerLimit > PlayerSexStatusDataManager.playerSexExtasyLimit[0])
		{
			SetApplyButtonInterctable(isVisible: false, groupIndex, childIndex);
			return;
		}
		List<PlayerSexStatusDataManager.MemberSexSubPower> memberSexSubPower = PlayerSexStatusDataManager.playerSexSubPower[0];
		int sexBattleSubPower = PlayerSexBattleConditionAccess.GetSexBattleSubPower(memberSexSubPower, "pistonOnly");
		int sexBattleSubPower2 = PlayerSexBattleConditionAccess.GetSexBattleSubPower(memberSexSubPower, "titsOnly");
		if (sexBattleSubPower > 0)
		{
			if (skillData.actionType == SexSkillData.ActionType.piston)
			{
				SetApplyButtonInterctable(isVisible: true, groupIndex, childIndex);
			}
			else if (skillData.actionType == SexSkillData.ActionType.caress && sexBattleSubPower2 > 0)
			{
				SetApplyButtonInterctable(isVisible: true, groupIndex, childIndex);
			}
			else
			{
				SetApplyButtonInterctable(isVisible: false, groupIndex, childIndex);
			}
		}
		else if (sexBattleSubPower2 > 0)
		{
			if (skillData.actionType == SexSkillData.ActionType.caress && skillData.bodyCategory == SexSkillData.BodyCategory.tits && sexBattleSubPower2 > 0)
			{
				SetApplyButtonInterctable(isVisible: true, groupIndex, childIndex);
			}
			else if (skillData.actionType == SexSkillData.ActionType.piston && sexBattleSubPower > 0)
			{
				SetApplyButtonInterctable(isVisible: true, groupIndex, childIndex);
			}
			else
			{
				SetApplyButtonInterctable(isVisible: false, groupIndex, childIndex);
			}
		}
		else
		{
			SetApplyButtonInterctable(isVisible: true, groupIndex, childIndex);
		}
	}

	private void SetApplyButtonInterctable(bool isVisible, int groupIndex, int childIndex)
	{
		CanvasGroup component = sexBattleManager.skillButtonGroupParentArray[groupIndex].transform.GetChild(childIndex).GetComponent<CanvasGroup>();
		if (isVisible)
		{
			component.interactable = true;
			component.alpha = 1f;
		}
		else
		{
			component.interactable = false;
			component.alpha = 0.5f;
		}
	}
}
