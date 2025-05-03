using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class CheckSkillVoiceEnd : StateBehaviour
{
	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
	}

	public override void OnStateBegin()
	{
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		if (!PlayerOptionsDataManager.isAllVoiceDisable)
		{
			if (!MasterAudio.IsSoundGroupPlaying(scenarioBattleSkillManager.playingSKillVoiceGroupName))
			{
				Transition(stateLink);
			}
		}
		else
		{
			Transition(stateLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
