using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class StartTouchCumShotFinish : StateBehaviour
{
	private SexTouchManager sexTouchManager;

	private SexTouchStatusManager sexTouchStatusManager;

	private SexTouchHeroineDataManager sexTouchHeroineDataManager;

	private bool isSkiped;

	public StateLink mouthLink;

	public StateLink faceLink;

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
		MasterAudio.FadeBusToVolume("Voice" + PlayerNonSaveDataManager.selectSexBattleHeroineId, 0f, 0.2f, null, willStopAfterFade: true, willResetVolumeAfterFade: true);
		string text = "";
		string sType = "Voice_Fellatio_CumShot_Start_" + PlayerNonSaveDataManager.selectSexBattleHeroineId + sexTouchStatusManager.heroineSexLvStage;
		int num = Random.Range(1, 3);
		text = "voice_Fellatio" + PlayerNonSaveDataManager.selectSexBattleHeroineId + "_CumShot_Start_" + sexTouchStatusManager.heroineSexLvStage.ToString() + num;
		PlaySoundResult playSoundResult = MasterAudio.PlaySound(sType, 1f, null, 0f, text + "(Clone)", null);
		if (playSoundResult != null && playSoundResult.SoundPlayed)
		{
			playSoundResult.ActingVariation.SoundFinished += PreVoiceFinished;
		}
		sexTouchManager.skipInfoFrame.SetActive(value: true);
		sexTouchManager.textVisibleButtonGo.SetActive(value: true);
		sexTouchManager.cumShotTextLoc.gameObject.SetActive(value: true);
		sexTouchManager.cumShotTextLoc.Term = "voice_Fellatio" + PlayerNonSaveDataManager.selectSexBattleHeroineId + "_CumShot_Start_" + sexTouchStatusManager.heroineSexLvStage.ToString() + num;
		sexTouchManager.cumShotInfoFrame.SetActive(value: false);
	}

	public override void OnStateEnd()
	{
		sexTouchManager.skipInfoFrame.SetActive(value: false);
		sexTouchManager.textVisibleButtonGo.SetActive(value: false);
		sexTouchManager.cumShotTextLoc.gameObject.SetActive(value: false);
	}

	public override void OnStateUpdate()
	{
		if (Input.GetButtonDown("Fire2"))
		{
			isSkiped = true;
			if (sexTouchStatusManager.isCumShotFace)
			{
				Transition(faceLink);
			}
			else
			{
				Transition(mouthLink);
			}
		}
	}

	public override void OnStateLateUpdate()
	{
	}

	private void PreVoiceFinished()
	{
		if (!isSkiped)
		{
			if (sexTouchStatusManager.isCumShotFace)
			{
				Transition(faceLink);
			}
			else
			{
				Transition(mouthLink);
			}
		}
	}
}
