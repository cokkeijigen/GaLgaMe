using Arbor;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class StatusSkillSummaryRefresh : StateBehaviour
{
	private StatusManager statusManager;

	private StatusSkillViewManager statusSkillViewManager;

	private ParameterContainer summaryParameter;

	private GameObject skillSummaryWindow;

	private Localize NameTextLoc;

	private Localize skillSummaryText;

	private Image skillImage;

	private Localize[] panel_TypeTextLoc = new Localize[8];

	private Text[] panel_PowerText = new Text[8];

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
		statusSkillViewManager = GameObject.Find("Skill View Manager").GetComponent<StatusSkillViewManager>();
		skillSummaryWindow = statusManager.skillViewArray[1];
		summaryParameter = skillSummaryWindow.GetComponent<ParameterContainer>();
		NameTextLoc = summaryParameter.GetVariable<I2LocalizeComponent>("skillNameTextLoc").localize;
		skillSummaryText = summaryParameter.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize;
		skillImage = summaryParameter.GetVariable<UguiImage>("skillImage").image;
		for (int i = 0; i < 8; i++)
		{
			panel_TypeTextLoc[i] = summaryParameter.GetVariableList<I2LocalizeComponent>("statusTypeLoc")[i].localize;
			panel_PowerText[i] = summaryParameter.GetComponent<ParameterContainer>().GetVariableList<UguiTextVariable>("statusPowerText")[i].text;
		}
	}

	public override void OnStateBegin()
	{
		int skillID = statusManager.selectSkillId;
		BattleSkillData battleSkillData = GameDataManager.instance.playerSkillDataBase.skillDataList.Find((BattleSkillData m) => m.skillID == skillID);
		if (battleSkillData != null)
		{
			NameTextLoc.Term = "playerSkill" + skillID;
			Sprite skillSprite = battleSkillData.skillSprite;
			skillImage.sprite = skillSprite;
			skillSummaryText.Term = "playerSkill" + skillID + "_summary";
			panel_PowerText[0].GetComponent<Localize>().Term = "skillType_" + battleSkillData.skillType;
			panel_PowerText[1].GetComponent<Localize>().Term = "skillTarget_" + battleSkillData.skillTarget;
			panel_PowerText[2].text = battleSkillData.skillPower.ToString();
			string text;
			switch (battleSkillData.skillElement)
			{
			case "火":
				text = "fire";
				break;
			case "水":
				text = "water";
				break;
			case "雷":
				text = "lightning";
				break;
			case "氷":
				text = "ice";
				break;
			case "風":
				text = "wind";
				break;
			case "超":
				text = "super";
				break;
			default:
				text = "none";
				break;
			}
			panel_PowerText[3].GetComponent<Localize>().Term = "skillElement_" + text;
			if (battleSkillData.skillType == BattleSkillData.SkillType.buff)
			{
				panel_TypeTextLoc[4].Term = "skillBuffType";
				panel_PowerText[4].GetComponent<Localize>().Term = "skillBuffType_" + battleSkillData.buffType;
			}
			else if (battleSkillData.skillType == BattleSkillData.SkillType.deBuff)
			{
				panel_TypeTextLoc[4].Term = "skillDebuffType";
				panel_PowerText[4].GetComponent<Localize>().Term = "skillDebuffType_" + battleSkillData.buffType;
			}
			else
			{
				panel_TypeTextLoc[4].Term = "skillBuffType";
				panel_PowerText[4].GetComponent<Localize>().Term = "skillSubType_none";
			}
			if (battleSkillData.subType == BattleSkillData.SubType.buff)
			{
				panel_PowerText[5].GetComponent<Localize>().Term = "skillSubType_none";
				panel_TypeTextLoc[4].Term = "skillBuffType";
				panel_PowerText[4].GetComponent<Localize>().Term = "skillBuffType_" + battleSkillData.buffType;
			}
			else if (battleSkillData.subType == BattleSkillData.SubType.debuff)
			{
				panel_PowerText[5].GetComponent<Localize>().Term = "skillSubType_none";
				panel_TypeTextLoc[4].Term = "skillDebuffType";
				panel_PowerText[4].GetComponent<Localize>().Term = "skillDebuffType_" + battleSkillData.buffType;
			}
			else
			{
				panel_TypeTextLoc[4].Term = "skillBuffType";
				panel_PowerText[5].GetComponent<Localize>().Term = "skillSubType_" + battleSkillData.subType;
			}
			panel_PowerText[6].text = battleSkillData.skillRecharge.ToString();
			panel_PowerText[7].text = battleSkillData.skillContinuity.ToString();
		}
		statusSkillViewManager.isLearned = false;
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
