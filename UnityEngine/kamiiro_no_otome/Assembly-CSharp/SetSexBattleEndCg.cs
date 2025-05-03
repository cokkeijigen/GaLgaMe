using Arbor;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class SetSexBattleEndCg : StateBehaviour
{
	public enum Type
	{
		victory = 1,
		victoryAfter,
		defeat,
		turnEnd
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
		case Type.victory:
			sexBattleManager.SetHeroineSprite("absorb");
			break;
		case Type.victoryAfter:
			sexBattleManager.SetHeroineSprite("battleVictory");
			break;
		case Type.defeat:
			sexBattleManager.SetHeroineSprite("battleDefeat");
			break;
		case Type.turnEnd:
			sexBattleManager.SetHeroineSprite("idle");
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
