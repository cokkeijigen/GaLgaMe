using System.Collections.Generic;
using Arbor;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class RefeshSexBattleSkillSummary : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	private ParameterContainer summaryParam;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		summaryParam = sexBattleManager.skillWindowSummaryParameter;
	}

	public override void OnStateBegin()
	{
		SexSkillData skillData = GameDataManager.instance.sexSkillDataBase.sexSkillDataList.Find((SexSkillData data) => data.skillID == sexBattleManager.selectSkillID);
		summaryParam.GetVariable<I2LocalizeComponent>("skillNameTextLoc").localize.Term = "sexSkill" + sexBattleManager.selectSkillID;
		Sprite sprite = GameDataManager.instance.itemCategoryDataBase.skillCategoryIconDictionary[skillData.skillType.ToString()];
		summaryParam.GetVariable<UguiImage>("iconImage").image.sprite = sprite;
		summaryParam.GetVariable<UguiImage>("skillImage").image.sprite = skillData.skillSprite;
		summaryParam.GetVariable<I2LocalizeComponent>("skillSummaryTextLoc").localize.Term = "sexSkill" + sexBattleManager.selectSkillID + "_summary";
		summaryParam.GetGameObjectList("statusPowerGoGroup")[0].GetComponent<Localize>().Term = "sexSkillType_" + skillData.skillType;
		summaryParam.GetGameObjectList("statusPowerGoGroup")[1].GetComponent<Localize>().Term = "sexActionType_" + skillData.actionType;
		summaryParam.GetGameObjectList("statusPowerGoGroup")[2].GetComponent<Text>().text = skillData.skillPower.ToString();
		summaryParam.GetGameObjectList("statusPowerGoGroup")[3].GetComponent<Text>().text = skillData.healPower.ToString();
		switch (skillData.playerAffect)
		{
		case SexSkillData.TranceAffect.none:
			summaryParam.GetGameObjectList("statusPowerGoGroup")[4].GetComponent<Localize>().Term = "skillSubType_none";
			break;
		case SexSkillData.TranceAffect.tranceUp:
			summaryParam.GetGameObjectList("statusPowerGoGroup")[4].GetComponent<Localize>().Term = "sexHeroineAffect_Up";
			break;
		case SexSkillData.TranceAffect.tranceDown:
			summaryParam.GetGameObjectList("statusPowerGoGroup")[4].GetComponent<Localize>().Term = "sexHeroineAffect_Down";
			break;
		}
		switch (skillData.heroineAffect)
		{
		case SexSkillData.TranceAffect.none:
			summaryParam.GetGameObjectList("statusPowerGoGroup")[5].GetComponent<Localize>().Term = "skillSubType_none";
			break;
		case SexSkillData.TranceAffect.tranceUp:
			summaryParam.GetGameObjectList("statusPowerGoGroup")[5].GetComponent<Localize>().Term = "sexHeroineAffect_Up";
			break;
		case SexSkillData.TranceAffect.tranceDown:
			summaryParam.GetGameObjectList("statusPowerGoGroup")[5].GetComponent<Localize>().Term = "sexHeroineAffect_Down";
			break;
		}
		summaryParam.GetGameObjectList("statusPowerGoGroup")[6].GetComponent<Localize>().Term = "sexSkillBuffType_" + skillData.buffType;
		summaryParam.GetGameObjectList("statusPowerGoGroup")[7].GetComponent<Localize>().Term = "sexSkillSubType_" + skillData.subType;
		summaryParam.GetGameObjectList("statusPowerGoGroup")[8].GetComponent<Text>().text = skillData.skillNeedTrance.ToString();
		summaryParam.GetGameObjectList("statusPowerGoGroup")[9].GetComponent<Text>().text = skillData.skillRecharge.ToString();
		if (PlayerSexStatusDataManager.playerSexSkillRechargeTurn[0].Find((PlayerSexStatusDataManager.MemberSexSkillReChargeTurn data) => data.skillID == skillData.skillID).needRechargeTurn > 0)
		{
			SetApplyButtonInterctable(isVisible: false);
		}
		else if (skillData.skillType == SexSkillData.SkillType.berserk)
		{
			if (PlayerSexStatusDataManager.playerSexTrance[0] >= 100)
			{
				SetApplyButtonInterctable(isVisible: true);
			}
			else
			{
				SetApplyButtonInterctable(isVisible: false);
			}
		}
		else
		{
			List<PlayerSexStatusDataManager.MemberSexSubPower> memberSexSubPower = PlayerSexStatusDataManager.playerSexSubPower[0];
			int sexBattleSubPower = PlayerSexBattleConditionAccess.GetSexBattleSubPower(memberSexSubPower, "pistonOnly");
			int sexBattleSubPower2 = PlayerSexBattleConditionAccess.GetSexBattleSubPower(memberSexSubPower, "titsOnly");
			if (sexBattleSubPower > 0)
			{
				if (skillData.actionType == SexSkillData.ActionType.piston)
				{
					SetApplyButtonInterctable(isVisible: true);
				}
				else if (skillData.actionType == SexSkillData.ActionType.caress && sexBattleSubPower2 > 0)
				{
					SetApplyButtonInterctable(isVisible: true);
				}
				else
				{
					SetApplyButtonInterctable(isVisible: false);
				}
			}
			else if (sexBattleSubPower2 > 0)
			{
				if (skillData.actionType == SexSkillData.ActionType.caress && sexBattleSubPower2 > 0)
				{
					SetApplyButtonInterctable(isVisible: true);
				}
				else if (skillData.actionType == SexSkillData.ActionType.piston && sexBattleSubPower > 0)
				{
					SetApplyButtonInterctable(isVisible: true);
				}
				else
				{
					SetApplyButtonInterctable(isVisible: false);
				}
			}
			else
			{
				SetApplyButtonInterctable(isVisible: true);
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

	private void SetApplyButtonInterctable(bool isVisible)
	{
		if (isVisible)
		{
			sexBattleManager.skillWindowApplyButtonGo.GetComponent<CanvasGroup>().interactable = true;
			sexBattleManager.skillWindowApplyButtonGo.GetComponent<CanvasGroup>().alpha = 1f;
		}
		else
		{
			sexBattleManager.skillWindowApplyButtonGo.GetComponent<CanvasGroup>().interactable = false;
			sexBattleManager.skillWindowApplyButtonGo.GetComponent<CanvasGroup>().alpha = 0.5f;
		}
	}
}
