using Arbor;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class SetSexBattleCg : StateBehaviour
{
	public enum Type
	{
		berserk = 1,
		ecstasy,
		victoryEcstasy,
		cumShot,
		victoryCumShot
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
		sexBattleManager.battleHeroineBeforeSprite.GetComponent<SpriteRenderer>().sprite = sexBattleManager.battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite;
		sexBattleManager.battleHeroineBeforeSprite.color = Color.white;
		switch (type)
		{
		case Type.berserk:
			sexBattleManager.SetHeroineSprite("berserkPiston");
			break;
		case Type.ecstasy:
			sexBattleManager.SetHeroineSprite("ecstasy");
			break;
		case Type.victoryEcstasy:
			sexBattleManager.SetHeroineSprite("victoryEcstasy");
			break;
		case Type.cumShot:
			sexBattleManager.SetHeroineSprite("cumShot");
			break;
		case Type.victoryCumShot:
			sexBattleManager.SetHeroineSprite("victoryCumShot");
			break;
		}
		sexBattleManager.battleHeroineBeforeSprite.DOColor(new Color(1f, 1f, 1f, 0f), fadeTime).OnComplete(FinishFadeOut);
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
