using Arbor;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class SetSexBattleAbsorbCg : StateBehaviour
{
	public enum Type
	{
		absorb
	}

	private SexBattleManager sexBattleManager;

	public Type type;

	public float fadeTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
	}

	public override void OnStateBegin()
	{
		if (sexBattleManager.heroineSexSkillData.skillType == SexSkillData.SkillType.none)
		{
			sexBattleManager.SetHeroineSprite("absorb");
			sexBattleManager.battleHeroineBeforeSprite.DOColor(new Color(1f, 1f, 1f, 0f), fadeTime).OnComplete(FinishFadeOut);
		}
		else
		{
			Transition(stateLink);
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

	private void FinishFadeOut()
	{
		Transition(stateLink);
	}
}
