using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class CheckSexVoiceEnd : StateBehaviour
{
	private bool isPlayStart;

	public string voiceFileName;

	private string checkVoiceFileName;

	public bool isAddHeroineNum;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		isPlayStart = false;
		if (isAddHeroineNum)
		{
			checkVoiceFileName = voiceFileName + "_" + PlayerNonSaveDataManager.selectSexBattleHeroineId;
		}
		else
		{
			checkVoiceFileName = voiceFileName;
		}
		isPlayStart = true;
	}

	public override void OnStateEnd()
	{
		isPlayStart = false;
	}

	public override void OnStateUpdate()
	{
		if (!MasterAudio.IsSoundGroupPlaying(checkVoiceFileName) && isPlayStart)
		{
			Transition(stateLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
