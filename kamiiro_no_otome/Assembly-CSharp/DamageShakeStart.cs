using Arbor;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class DamageShakeStart : StateBehaviour
{
	public enum Type
	{
		player,
		enemy
	}

	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public Type type;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
	}

	public override void OnStateBegin()
	{
		if (scenarioBattleTurnManager.battleUseSkillID == 0)
		{
			switch (type)
			{
			case Type.player:
			{
				float duration = 0.1f;
				float strength = 80f;
				int vibrato = 3;
				utageBattleSceneManager.enemyImageGoList[scenarioBattleTurnManager.playerTargetNum].GetComponent<RectTransform>().DOShakeAnchorPos(duration, strength, vibrato, 10f, snapping: false, fadeOut: false);
				break;
			}
			case Type.enemy:
			{
				float duration = 0.1f;
				float strength = 80f;
				int vibrato = 3;
				utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.enemyTargetNum].GetComponent<RectTransform>().DOShakeAnchorPos(duration, strength, vibrato, 10f, snapping: false, fadeOut: false);
				break;
			}
			}
			Transition(stateLink);
		}
		else
		{
			SkillShake();
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

	private void SkillShake()
	{
		if (scenarioBattleTurnManager.isUseSkillPlayer)
		{
			if (scenarioBattleTurnManager.isAllTargetSkill)
			{
				float duration = 0.1f;
				float strength = 0.5f;
				int vibrato = 1;
				utageBattleSceneManager.battleCanvas.transform.DOShakePosition(duration, strength, vibrato, 10f, snapping: false, fadeOut: false);
			}
			else
			{
				float duration = 0.1f;
				float strength = 80f;
				int vibrato = 3;
				utageBattleSceneManager.enemyImageGoList[scenarioBattleTurnManager.playerTargetNum].GetComponent<RectTransform>().DOShakeAnchorPos(duration, strength, vibrato, 10f, snapping: false, fadeOut: false);
			}
		}
		else if (scenarioBattleTurnManager.isAllTargetSkill)
		{
			float duration = 0.1f;
			float strength = 0.5f;
			int vibrato = 1;
			utageBattleSceneManager.battleCanvas.transform.DOShakePosition(duration, strength, vibrato, 10f, snapping: false, fadeOut: false);
		}
		else
		{
			float duration = 0.1f;
			float strength = 80f;
			int vibrato = 3;
			utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.enemyTargetNum].GetComponent<RectTransform>().DOShakeAnchorPos(duration, strength, vibrato, 10f, snapping: false, fadeOut: false);
		}
		Transition(stateLink);
	}
}
