using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckSexSkillType : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	private SexBattleEffectManager sexBattleEffectManager;

	public StateLink attackLink;

	public StateLink fertilizeLink;

	public StateLink berserkLink;

	public StateLink positiveLink;

	public StateLink deBuffLink;

	public StateLink noneLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		sexBattleEffectManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleEffectManager>();
	}

	public override void OnStateBegin()
	{
		switch (sexBattleManager.selectSexSkillData.skillType)
		{
		case SexSkillData.SkillType.sexAttack:
		case SexSkillData.SkillType.caress:
			Transition(attackLink);
			break;
		case SexSkillData.SkillType.fertilize:
			Transition(fertilizeLink);
			break;
		case SexSkillData.SkillType.berserk:
			Transition(berserkLink);
			break;
		case SexSkillData.SkillType.heal:
		case SexSkillData.SkillType.buff:
			Transition(positiveLink);
			break;
		case SexSkillData.SkillType.deBuff:
			Transition(deBuffLink);
			break;
		case SexSkillData.SkillType.none:
			Transition(noneLink);
			break;
		case (SexSkillData.SkillType)7:
		case (SexSkillData.SkillType)8:
			break;
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
}
