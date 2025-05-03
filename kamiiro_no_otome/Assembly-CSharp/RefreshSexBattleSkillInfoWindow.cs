using Arbor;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class RefreshSexBattleSkillInfoWindow : StateBehaviour
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
		summaryParam = sexBattleManager.skillInfoWindowSummaryParameter;
	}

	public override void OnStateBegin()
	{
		SexSkillData sexSkillData = GameDataManager.instance.sexSkillDataBase.sexSkillDataList.Find((SexSkillData data) => data.skillID == sexBattleManager.selectSkillID);
		summaryParam.GetVariable<I2LocalizeComponent>("skillNameTextLoc").localize.Term = "sexSkill" + sexBattleManager.selectSkillID;
		summaryParam.GetVariable<UguiImage>("skillImage").image.sprite = sexSkillData.skillSprite;
		summaryParam.GetVariable<I2LocalizeComponent>("skillSummaryTextLoc").localize.Term = "sexSkill" + sexBattleManager.selectSkillID + "_summary";
		summaryParam.GetGameObjectList("statusPowerGoGroup")[0].GetComponent<Localize>().Term = "sexSkillType_" + sexSkillData.skillType;
		summaryParam.GetGameObjectList("statusPowerGoGroup")[1].GetComponent<Localize>().Term = "sexActionType_" + sexSkillData.actionType;
		summaryParam.GetGameObjectList("statusPowerGoGroup")[2].GetComponent<Text>().text = sexSkillData.skillPower.ToString();
		summaryParam.GetGameObjectList("statusPowerGoGroup")[3].GetComponent<Text>().text = sexSkillData.healPower.ToString();
		switch (sexSkillData.playerAffect)
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
		switch (sexSkillData.heroineAffect)
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
		summaryParam.GetGameObjectList("statusPowerGoGroup")[6].GetComponent<Localize>().Term = "sexSkillBuffType_" + sexSkillData.buffType;
		summaryParam.GetGameObjectList("statusPowerGoGroup")[7].GetComponent<Localize>().Term = "sexSkillSubType_" + sexSkillData.subType;
		if (sexSkillData.skillNeedTrance == 0)
		{
			summaryParam.GetGameObjectList("statusPowerGoGroup")[8].GetComponent<Localize>().Term = "sexSkillNeedTrance_0";
		}
		else if (sexSkillData.skillNeedTrance < 70)
		{
			summaryParam.GetGameObjectList("statusPowerGoGroup")[8].GetComponent<Localize>().Term = "sexSkillNeedTrance_1";
		}
		else if (sexSkillData.skillNeedTrance < 100)
		{
			summaryParam.GetGameObjectList("statusPowerGoGroup")[8].GetComponent<Localize>().Term = "sexSkillNeedTrance_2";
		}
		else
		{
			summaryParam.GetGameObjectList("statusPowerGoGroup")[8].GetComponent<Localize>().Term = "sexSkillNeedTrance_3";
		}
		summaryParam.GetGameObjectList("statusPowerGoGroup")[9].GetComponent<Text>().text = sexSkillData.skillRecharge.ToString();
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
