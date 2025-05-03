using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class EndTouchCumShotFinish : StateBehaviour
{
	private SexTouchManager sexTouchManager;

	private SexTouchStatusManager sexTouchStatusManager;

	private SexTouchHeroineDataManager sexTouchHeroineDataManager;

	private bool isSkiped;

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
		string text = "";
		string sType = "Voice_Fellatio_CumShot_After_" + PlayerNonSaveDataManager.selectSexBattleHeroineId + sexTouchStatusManager.heroineSexLvStage;
		int num = Random.Range(1, 3);
		text = "voice_Fellatio" + PlayerNonSaveDataManager.selectSexBattleHeroineId + "_CumShot_After_" + sexTouchStatusManager.heroineSexLvStage.ToString() + num;
		PlaySoundResult playSoundResult = MasterAudio.PlaySound(sType, 1f, null, 0f, text + "(Clone)", null);
		if (playSoundResult != null && playSoundResult.SoundPlayed)
		{
			playSoundResult.ActingVariation.SoundFinished += AfterVoiceFinished;
		}
		sexTouchManager.skipInfoFrame.SetActive(value: true);
		sexTouchManager.textVisibleButtonGo.SetActive(value: true);
		sexTouchManager.cumShotTextLoc.gameObject.SetActive(value: true);
		sexTouchManager.cumShotTextLoc.Term = "voice_Fellatio" + PlayerNonSaveDataManager.selectSexBattleHeroineId + "_CumShot_After_" + sexTouchStatusManager.heroineSexLvStage.ToString() + num;
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
			Transition(stateLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}

	private void AfterVoiceFinished()
	{
		if (!isSkiped)
		{
			Transition(stateLink);
		}
	}
}
