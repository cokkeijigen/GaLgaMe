using Arbor;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class SexStatusSkillSummaryRefresh : StateBehaviour
{
	private StatusManager statusManager;

	private SexStatusManager sexStatusManager;

	private Localize NameTextLoc;

	private Localize skillSummaryText;

	private Image skillImage;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
		sexStatusManager = GameObject.Find("Sex Status Manager").GetComponent<SexStatusManager>();
	}

	public override void OnStateBegin()
	{
		if (!sexStatusManager.isSelectTypePassvie)
		{
			Localize[] array = new Localize[10];
			Text[] array2 = new Text[10];
			sexStatusManager.activeParam.gameObject.SetActive(value: true);
			sexStatusManager.passiveParam.gameObject.SetActive(value: false);
			NameTextLoc = sexStatusManager.activeParam.GetVariable<I2LocalizeComponent>("skillNameTextLoc").localize;
			skillSummaryText = sexStatusManager.activeParam.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize;
			skillImage = sexStatusManager.activeParam.GetVariable<UguiImage>("skillImage").image;
			for (int i = 0; i < 10; i++)
			{
				array[i] = sexStatusManager.activeParam.GetVariableList<I2LocalizeComponent>("statusTypeLoc")[i].localize;
				array2[i] = sexStatusManager.activeParam.GetComponent<ParameterContainer>().GetVariableList<UguiTextVariable>("statusPowerText")[i].text;
			}
			int skillID = sexStatusManager.selectSexSkillId;
			SexSkillData sexSkillData = GameDataManager.instance.sexSkillDataBase.sexSkillDataList.Find((SexSkillData m) => m.skillID == skillID);
			if (sexSkillData != null)
			{
				NameTextLoc.Term = "sexSkill" + skillID;
				Sprite skillSprite = sexSkillData.skillSprite;
				skillImage.sprite = skillSprite;
				skillSummaryText.Term = "sexSkill" + skillID + "_summary";
				array2[0].GetComponent<Localize>().Term = "sexSkillType_" + sexSkillData.skillType;
				array2[1].GetComponent<Localize>().Term = "sexActionType_" + sexSkillData.actionType;
				array2[2].text = sexSkillData.skillPower.ToString();
				array2[3].text = sexSkillData.healPower.ToString();
				array2[4].GetComponent<Localize>().Term = "sexSkillBuffType_" + sexSkillData.buffType;
				array2[5].GetComponent<Localize>().Term = "sexSkillSubType_" + sexSkillData.subType;
				if (sexSkillData.playerAffect == SexSkillData.TranceAffect.tranceUp)
				{
					array2[7].GetComponent<Localize>().Term = "sexHeroineAffect_Up";
				}
				else
				{
					array2[7].GetComponent<Localize>().Term = "sexHeroineAffect_Down";
				}
				if (sexSkillData.heroineAffect == SexSkillData.TranceAffect.tranceUp)
				{
					array2[6].GetComponent<Localize>().Term = "sexHeroineAffect_Up";
				}
				else
				{
					array2[6].GetComponent<Localize>().Term = "sexHeroineAffect_Down";
				}
				array2[8].text = sexSkillData.skillRecharge.ToString();
				array2[9].text = sexSkillData.skillContinuityTurn.ToString();
			}
		}
		else
		{
			Localize[] array3 = new Localize[4];
			Text[] array4 = new Text[4];
			sexStatusManager.activeParam.gameObject.SetActive(value: false);
			sexStatusManager.passiveParam.gameObject.SetActive(value: true);
			NameTextLoc = sexStatusManager.passiveParam.GetVariable<I2LocalizeComponent>("skillNameTextLoc").localize;
			skillSummaryText = sexStatusManager.passiveParam.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize;
			skillImage = sexStatusManager.passiveParam.GetVariable<UguiImage>("skillImage").image;
			for (int j = 0; j < 4; j++)
			{
				array3[j] = sexStatusManager.passiveParam.GetVariableList<I2LocalizeComponent>("statusTypeLoc")[j].localize;
				array4[j] = sexStatusManager.passiveParam.GetComponent<ParameterContainer>().GetVariableList<UguiTextVariable>("statusPowerText")[j].text;
			}
			int skillID2 = sexStatusManager.selectSexSkillId;
			SexHeroinePassiveData sexHeroinePassiveData = GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[statusManager.selectCharacterNum - 1].sexHeroinePassiveDataList.Find((SexHeroinePassiveData m) => m.skillID == skillID2);
			if (sexHeroinePassiveData != null)
			{
				NameTextLoc.Term = "sexBodyPassive_" + sexHeroinePassiveData.bodyCategory.ToString() + skillID2;
				Sprite skillSprite2 = sexHeroinePassiveData.skillSprite;
				skillImage.sprite = skillSprite2;
				skillSummaryText.Term = "sexBodyPassiveSummary_" + sexHeroinePassiveData.bodyCategory.ToString() + skillID2;
				array4[0].GetComponent<Localize>().Term = "sexPassiveType_" + sexHeroinePassiveData.passiveType;
				array4[1].GetComponent<Localize>().Term = "sexBodyCategory_" + sexHeroinePassiveData.bodyCategory;
				array4[2].text = sexHeroinePassiveData.skillPower.ToString();
				string text = sexHeroinePassiveData.statusType.ToString().Substring(0, 1).ToUpper() + sexHeroinePassiveData.statusType.ToString().Substring(1);
				array4[3].GetComponent<Localize>().Term = "status" + text;
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
}
