using Arbor;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class SelectBattleSkillScrollContents : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	private ScenarioBattleUiManager scenarioBattleUiManager;

	private ParameterContainer parameterContainer;

	private BattleSkillData battleSkillData;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
		scenarioBattleUiManager = GameObject.Find("Battle Ui Manager").GetComponent<ScenarioBattleUiManager>();
		parameterContainer = scenarioBattleSkillManager.skillWindow.GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		scenarioBattleTurnManager.setFrameTypeName = "reset";
		scenarioBattleUiManager.uiFSM.SendTrigger("SetCharacterFrameType");
		battleSkillData = scenarioBattleTurnManager.playerSkillData;
		if (battleSkillData != null)
		{
			foreach (Transform item in scenarioBattleSkillManager.skillContentGo.transform)
			{
				if (item.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("reChargeTurnText").text.text == "0")
				{
					item.GetComponent<Image>().sprite = scenarioBattleSkillManager.scrollContentSpriteArray[0];
				}
				else
				{
					item.GetComponent<Image>().sprite = scenarioBattleSkillManager.scrollContentSpriteArray[2];
				}
			}
			int scrollContentClickNum = scenarioBattleSkillManager.scrollContentClickNum;
			scenarioBattleSkillManager.skillContentGo.transform.GetChild(scrollContentClickNum).GetComponent<Image>().sprite = scenarioBattleSkillManager.scrollContentSpriteArray[1];
			string text = "playerSkill" + battleSkillData.skillID;
			parameterContainer.GetVariable<I2LocalizeComponent>("skillNameTextLoc").localize.Term = text;
			parameterContainer.GetVariable<I2LocalizeComponent>("useCharacterNameTextLoc").localize.Term = "character" + scenarioBattleTurnManager.useSkillPartyMemberID;
			parameterContainer.GetVariable<I2LocalizeComponent>("skillSummaryTextLoc").localize.Term = text + "_summary";
			string key = battleSkillData.skillType.ToString();
			parameterContainer.GetVariable<UguiImage>("iconImage").image.gameObject.SetActive(value: true);
			parameterContainer.GetVariable<UguiImage>("iconImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.skillCategoryIconDictionary[key];
			parameterContainer.GetVariable<UguiImage>("skillImage").image.sprite = battleSkillData.skillSprite;
			RefreshContentData();
		}
		else
		{
			parameterContainer.GetVariable<I2LocalizeComponent>("skillNameTextLoc").localize.Term = "skillNoEquip";
			parameterContainer.GetVariable<I2LocalizeComponent>("useCharacterNameTextLoc").localize.Term = "noStatus";
			parameterContainer.GetVariable<I2LocalizeComponent>("skillSummaryTextLoc").localize.Term = "skillNoEquip";
			parameterContainer.GetVariable<UguiImage>("iconImage").image.gameObject.SetActive(value: false);
			parameterContainer.GetVariable<UguiImage>("skillImage").image.sprite = scenarioBattleSkillManager.noItemImageSprite;
			NoContentData();
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

	private void RefreshContentData()
	{
		parameterContainer.GetGameObjectList("statusPowerGoGroup")[0].GetComponent<Localize>().Term = "skillType_" + battleSkillData.skillType;
		parameterContainer.GetGameObjectList("statusPowerGoGroup")[1].GetComponent<Localize>().Term = "skillTarget_" + battleSkillData.skillTarget;
		parameterContainer.GetGameObjectList("statusPowerGoGroup")[2].GetComponent<Text>().text = battleSkillData.skillPower.ToString();
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
		parameterContainer.GetGameObjectList("statusPowerGoGroup")[3].GetComponent<Localize>().Term = "skillElement_" + text;
		if (battleSkillData.skillType == BattleSkillData.SkillType.buff)
		{
			parameterContainer.GetVariableList<I2LocalizeComponent>("statusTypeLocGroup")[4].localize.Term = "skillBuffType";
			parameterContainer.GetGameObjectList("statusPowerGoGroup")[4].GetComponent<Localize>().Term = "skillBuffType_" + battleSkillData.buffType;
		}
		else if (battleSkillData.skillType == BattleSkillData.SkillType.deBuff)
		{
			parameterContainer.GetVariableList<I2LocalizeComponent>("statusTypeLocGroup")[4].localize.Term = "skillDebuffType";
			parameterContainer.GetGameObjectList("statusPowerGoGroup")[4].GetComponent<Localize>().Term = "skillDebuffType_" + battleSkillData.buffType;
		}
		else
		{
			parameterContainer.GetVariableList<I2LocalizeComponent>("statusTypeLocGroup")[4].localize.Term = "skillBuffType";
			parameterContainer.GetGameObjectList("statusPowerGoGroup")[4].GetComponent<Localize>().Term = "skillSubType_none";
		}
		if (battleSkillData.subType == BattleSkillData.SubType.buff)
		{
			parameterContainer.GetGameObjectList("statusPowerGoGroup")[5].GetComponent<Localize>().Term = "skillSubType_none";
			parameterContainer.GetVariableList<I2LocalizeComponent>("statusTypeLocGroup")[4].localize.Term = "skillBuffType";
			parameterContainer.GetGameObjectList("statusPowerGoGroup")[4].GetComponent<Localize>().Term = "skillBuffType_" + battleSkillData.buffType;
		}
		else if (battleSkillData.subType == BattleSkillData.SubType.debuff)
		{
			parameterContainer.GetGameObjectList("statusPowerGoGroup")[5].GetComponent<Localize>().Term = "skillSubType_none";
			parameterContainer.GetVariableList<I2LocalizeComponent>("statusTypeLocGroup")[4].localize.Term = "skillDebuffType";
			parameterContainer.GetGameObjectList("statusPowerGoGroup")[4].GetComponent<Localize>().Term = "skillDebuffType_" + battleSkillData.buffType;
		}
		else
		{
			parameterContainer.GetVariableList<I2LocalizeComponent>("statusTypeLocGroup")[4].localize.Term = "skillBuffType";
			parameterContainer.GetGameObjectList("statusPowerGoGroup")[5].GetComponent<Localize>().Term = "skillSubType_" + battleSkillData.subType;
		}
		parameterContainer.GetGameObjectList("statusPowerGoGroup")[6].GetComponent<Text>().text = battleSkillData.skillRecharge.ToString();
		parameterContainer.GetGameObjectList("statusPowerGoGroup")[7].GetComponent<Text>().text = battleSkillData.skillContinuity.ToString();
		if (battleSkillData.skillTarget == BattleSkillData.SkillTarget.all)
		{
			scenarioBattleSkillManager.commandClickSummaryTextLocArray[1].Term = "commandClickSummaryApplyAll";
		}
		else
		{
			scenarioBattleSkillManager.commandClickSummaryTextLocArray[1].Term = "commandClickSummaryApplySolo";
		}
		int num = scenarioBattleTurnManager.useSkillPartyMemberID;
		int num2 = PlayerStatusDataManager.characterMp[num];
		if (num == 0)
		{
			if (scenarioBattleSkillManager.selectSkillMpType == 0)
			{
				int itemID = PlayerEquipDataManager.playerEquipWeaponID[0];
				num2 = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == itemID && data.equipCharacter == 0).weaponIncludeMp;
			}
			else
			{
				num = PlayerStatusDataManager.partyMemberCount;
			}
		}
		int skillID = battleSkillData.skillID;
		int needRechargeTurn = PlayerBattleConditionManager.playerSkillRechargeTurn[num].Find((PlayerBattleConditionManager.MemberSkillReChargeTurn data) => data.skillID == skillID).needRechargeTurn;
		if (num2 >= battleSkillData.useMP && needRechargeTurn == 0)
		{
			scenarioBattleSkillManager.skillApplyButton.alpha = 1f;
			scenarioBattleSkillManager.skillApplyButton.interactable = true;
		}
		else
		{
			scenarioBattleSkillManager.skillApplyButton.alpha = 0.5f;
			scenarioBattleSkillManager.skillApplyButton.interactable = false;
		}
	}

	private void NoContentData()
	{
		parameterContainer.GetVariableList<I2LocalizeComponent>("statusTypeLocGroup")[0].localize.Term = "skillNoEquip";
		parameterContainer.GetGameObjectList("statusPowerGoGroup")[0].GetComponent<Localize>().Term = "noStatus";
		parameterContainer.GetGameObjectList("statusPowerGoGroup")[1].GetComponent<Text>().text = "−−−";
		parameterContainer.GetGameObjectList("statusPowerGoGroup")[2].GetComponent<Text>().text = "−−−";
		parameterContainer.GetGameObjectList("statusPowerGoGroup")[3].GetComponent<Localize>().Term = "noStatus";
		parameterContainer.GetGameObjectList("statusPowerGoGroup")[5].GetComponent<Localize>().Term = "noStatus";
		parameterContainer.GetGameObjectList("statusPowerGoGroup")[6].GetComponent<Text>().text = "−−−";
		parameterContainer.GetGameObjectList("statusPowerGoGroup")[7].GetComponent<Text>().text = "−−−";
		parameterContainer.GetVariableList<I2LocalizeComponent>("statusTypeLocGroup")[4].localize.Term = "skillBuffType";
		parameterContainer.GetGameObjectList("statusPowerGoGroup")[4].GetComponent<Localize>().Term = "noStatus";
		scenarioBattleSkillManager.skillApplyButton.alpha = 0.5f;
		scenarioBattleSkillManager.skillApplyButton.interactable = false;
	}
}
