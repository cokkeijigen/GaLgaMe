using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckDungeonDebuffEffect : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	private ParameterContainer parameterContainer;

	private BattleSkillData battleSkillData;

	private int skillSuccessRate;

	private int getResistPower;

	public StateLink effectHitLink;

	public StateLink badStateLink;

	public StateLink noEffectLink;

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
		getResistPower = 0;
		int num = 0;
		if (battleSkillData.subType == BattleSkillData.SubType.none)
		{
			Debug.Log("付与効果なし");
			Transition(noEffectLink);
			return;
		}
		switch (battleSkillData.subType)
		{
		case BattleSkillData.SubType.debuff:
		{
			for (int k = 0; k < PlayerStatusDataManager.playerPartyMember.Length; k++)
			{
				num = PlayerEquipDataManager.accessoryResistDebuff[PlayerStatusDataManager.playerPartyMember[k]];
				getResistPower += num;
			}
			getResistPower /= 2;
			break;
		}
		case BattleSkillData.SubType.poison:
		{
			for (int num2 = 0; num2 < PlayerStatusDataManager.playerPartyMember.Length; num2++)
			{
				num = PlayerEquipDataManager.accessoryResistPoison[PlayerStatusDataManager.playerPartyMember[num2]];
				getResistPower += num;
			}
			for (int num3 = 0; num3 < PlayerStatusDataManager.playerPartyMember.Length; num3++)
			{
				num = PlayerEquipDataManager.accessoryResistUp[PlayerStatusDataManager.playerPartyMember[num3]];
				getResistPower += num;
			}
			for (int num4 = 0; num4 < PlayerStatusDataManager.playerPartyMember.Length; num4++)
			{
				num = PlayerEquipDataManager.accessoryResistAll[PlayerStatusDataManager.playerPartyMember[num4]];
				getResistPower += num;
			}
			getResistPower /= 2;
			break;
		}
		case BattleSkillData.SubType.paralyze:
		{
			for (int l = 0; l < PlayerStatusDataManager.playerPartyMember.Length; l++)
			{
				num = PlayerEquipDataManager.accessoryResistParalyze[PlayerStatusDataManager.playerPartyMember[l]];
				getResistPower += num;
			}
			for (int m = 0; m < PlayerStatusDataManager.playerPartyMember.Length; m++)
			{
				num = PlayerEquipDataManager.accessoryResistUp[PlayerStatusDataManager.playerPartyMember[m]];
				getResistPower += num;
			}
			for (int n = 0; n < PlayerStatusDataManager.playerPartyMember.Length; n++)
			{
				num = PlayerEquipDataManager.accessoryResistAll[PlayerStatusDataManager.playerPartyMember[n]];
				getResistPower += num;
			}
			getResistPower /= 2;
			break;
		}
		case BattleSkillData.SubType.stagger:
		{
			for (int i = 0; i < PlayerStatusDataManager.playerPartyMember.Length; i++)
			{
				num = PlayerEquipDataManager.accessoryResistUp[PlayerStatusDataManager.playerPartyMember[i]];
				getResistPower += num;
			}
			for (int j = 0; j < PlayerStatusDataManager.playerPartyMember.Length; j++)
			{
				num = PlayerEquipDataManager.accessoryResistAll[PlayerStatusDataManager.playerPartyMember[j]];
				getResistPower += num;
			}
			getResistPower /= 2;
			break;
		}
		}
		int num5 = PlayerStatusDataManager.playerAllResist + getResistPower;
		skillSuccessRate = battleSkillData.successRate - num5;
		skillSuccessRate = Mathf.Clamp(skillSuccessRate, 0, 100);
		int num6 = Random.Range(0, 100);
		Debug.Log("サブ命中率：" + battleSkillData.successRate + " / スキル命中ランダム：" + num6);
		if (battleSkillData.successRate >= num6 && battleSkillData.successRate > 0)
		{
			parameterContainer.SetBool("isHitDebuffEffect", value: true);
			CheckSubType();
		}
		else
		{
			Debug.Log("付与効果回避");
			Invoke("InvokeMethod", 0.3f);
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

	private void InvokeMethod()
	{
		Transition(noEffectLink);
	}

	private void CheckSubType()
	{
		if (battleSkillData.subType == BattleSkillData.SubType.buff || battleSkillData.subType == BattleSkillData.SubType.debuff)
		{
			Transition(effectHitLink);
		}
		else
		{
			Transition(badStateLink);
		}
	}
}
