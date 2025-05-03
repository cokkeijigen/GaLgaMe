using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class PlayMouthCumShotVoice : StateBehaviour
{
	private SexTouchManager sexTouchManager;

	private SexTouchStatusManager sexTouchStatusManager;

	private SexTouchHeroineDataManager sexTouchHeroineDataManager;

	private bool isSkiped;

	private bool isVoiceStart;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexTouchManager = GameObject.Find("Sex Touch Manager").GetComponent<SexTouchManager>();
		sexTouchStatusManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchStatusManager>();
		sexTouchHeroineDataManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchHeroineDataManager>();
	}

	public override void OnStateBegin()
	{
		isSkiped = false;
		isVoiceStart = false;
		string text = "";
		string sType = "Voice_Fellatio_CumShot_In_" + PlayerNonSaveDataManager.selectSexBattleHeroineId + sexTouchStatusManager.heroineSexLvStage;
		int num = Random.Range(1, 3);
		text = "voice_Fellatio" + PlayerNonSaveDataManager.selectSexBattleHeroineId + "_CumShot_In_" + sexTouchStatusManager.heroineSexLvStage.ToString() + num;
		PlaySoundResult playSoundResult = MasterAudio.PlaySound(sType, 1f, null, 0f, text + "(Clone)", null);
		if (playSoundResult != null && playSoundResult.SoundPlayed)
		{
			playSoundResult.ActingVariation.SoundFinished += VoiceFinished;
		}
		isVoiceStart = true;
		sexTouchManager.skipInfoFrame.SetActive(value: true);
		sexTouchManager.textVisibleButtonGo.SetActive(value: true);
		sexTouchManager.cumShotTextLoc.gameObject.SetActive(value: true);
		sexTouchManager.cumShotTextLoc.Term = "voice_Fellatio" + PlayerNonSaveDataManager.selectSexBattleHeroineId + "_CumShot_In_" + sexTouchStatusManager.heroineSexLvStage.ToString() + num;
	}

	public override void OnStateEnd()
	{
		sexTouchManager.skipInfoFrame.SetActive(value: false);
		sexTouchManager.textVisibleButtonGo.SetActive(value: false);
		sexTouchManager.cumShotTextLoc.gameObject.SetActive(value: false);
	}

	public override void OnStateUpdate()
	{
		if (Input.GetButtonDown("Fire2") && isVoiceStart)
		{
			isSkiped = true;
			Transition(stateLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}

	private void VoiceFinished()
	{
		if (!isSkiped)
		{
			Transition(stateLink);
		}
	}
}
