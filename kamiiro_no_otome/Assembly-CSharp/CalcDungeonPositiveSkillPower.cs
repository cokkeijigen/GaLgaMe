using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcDungeonPositiveSkillPower : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	private ParameterContainer parameterContainer;

	public OutputSlotInt outputHealValue;

	private BattleSkillData battleSkillData;

	public StateLink buffLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = GetComponentInParent<DungeonBattleManager>();
		parameterContainer = GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		battleSkillData = dungeonBattleManager.battleSkillData;
		switch (battleSkillData.skillType.ToString())
		{
		case "buff":
		case "deSpell":
		case "regeneration":
			Transition(buffLink);
			break;
		case "heal":
			CalcHealSkill();
			break;
		case "medic":
			CalcMedicSkill();
			break;
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

	private void CalcHealSkill()
	{
		int num;
		if (parameterContainer.GetBool("isPlayerSkill"))
		{
			num = PlayerStatusDataManager.characterMagicAttack[parameterContainer.GetInt("useSkillCharacterID")] + 100;
			num = battleSkillData.skillPower * (num / 100);
		}
		else
		{
			num = PlayerStatusDataManager.enemyMagicAttack[parameterContainer.GetInt("useSkillCharacterID")] + 100;
			num = battleSkillData.skillPower * (num / 100);
		}
		float num2 = Random.Range(0.9f, 1.1f);
		int value = Mathf.FloorToInt((float)num * num2);
		outputHealValue.SetValue(value);
	}

	private void CalcMedicSkill()
	{
		new List<int>();
		switch (battleSkillData.skillPower)
		{
		case 0:
			if (PlayerBattleConditionManager.playerBadState[0].Count > 0 && PlayerBattleConditionManager.playerBadState[0].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "poison").continutyTurn > 0)
			{
				PlayerBattleConditionManager.playerBadState[0].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "poison").continutyTurn = 0;
				dungeonBattleManager.SetBadStateIcon("poison", 0, setValue: false);
			}
			break;
		case 1:
			if (PlayerBattleConditionManager.playerBadState[0].Count > 0 && PlayerBattleConditionManager.playerBadState[0].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "paralyze").continutyTurn > 0)
			{
				PlayerBattleConditionManager.playerBadState[0].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "paralyze").continutyTurn = 0;
				dungeonBattleManager.SetBadStateIcon("paralyze", 0, setValue: false);
			}
			break;
		case 2:
			if (PlayerBattleConditionManager.playerBadState[0].Count <= 0)
			{
				break;
			}
			if (PlayerBattleConditionManager.playerBadState[0].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "poison").continutyTurn > 0)
			{
				PlayerBattleConditionManager.playerBadState[0].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "poison").continutyTurn = 0;
				dungeonBattleManager.SetBadStateIcon("poison", 0, setValue: false);
			}
			if (PlayerBattleConditionManager.playerBadState[0].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "paralyze").continutyTurn > 0)
			{
				PlayerBattleConditionManager.playerBadState[0].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "paralyze").continutyTurn = 0;
				dungeonBattleManager.SetBadStateIcon("paralyze", 0, setValue: false);
			}
			break;
		}
	}
}
