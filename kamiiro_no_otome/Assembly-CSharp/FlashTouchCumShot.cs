using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class FlashTouchCumShot : StateBehaviour
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
		sexTouchManager.touchWhiteImageCanvasGroup.DOFade(1f, 0.1f).OnComplete(delegate
		{
			FlashFadeOut();
		});
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

	private void FlashFadeOut()
	{
		if (sexTouchStatusManager.isCumShotFace)
		{
			sexTouchManager.touchHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexTouchHeroineDataManager.sexTouchHeroineSpriteData.heroineCgFaceShotList[1];
			MasterAudio.PlaySound("SexBattle_CumShot_Inside", 1f, null, 0f, null, null);
			string text = "";
			string text2 = "Voice_Fellatio_CumShot_Out_" + PlayerNonSaveDataManager.selectSexBattleHeroineId + sexTouchStatusManager.heroineSexLvStage;
			int num = Random.Range(1, 3);
			text = "voice_Fellatio" + PlayerNonSaveDataManager.selectSexBattleHeroineId + "_CumShot_Out_" + sexTouchStatusManager.heroineSexLvStage.ToString() + num;
			Debug.Log("音声グループ：" + text2 + "／バリエーション：" + text);
			PlaySoundResult playSoundResult = MasterAudio.PlaySound(text2, 1f, null, 0f, text + "(Clone)", null);
			if (playSoundResult != null && playSoundResult.SoundPlayed)
			{
				playSoundResult.ActingVariation.SoundFinished += VoiceFinished;
			}
			sexTouchManager.textVisibleButtonGo.SetActive(value: true);
			sexTouchManager.cumShotTextLoc.gameObject.SetActive(value: true);
			sexTouchManager.cumShotTextLoc.Term = "voice_Fellatio" + PlayerNonSaveDataManager.selectSexBattleHeroineId + "_CumShot_Out_" + sexTouchStatusManager.heroineSexLvStage.ToString() + num;
		}
		else
		{
			sexTouchManager.touchHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexTouchHeroineDataManager.sexTouchHeroineSpriteData.heroineCgCumShotList[0];
			MasterAudio.PlaySound("SexBattle_CumShot_Inside", 1f, null, 0f, null, null);
			PlaySoundResult playSoundResult2 = MasterAudio.PlaySound("Voice_Fellatio_CumShot_In_" + PlayerNonSaveDataManager.selectSexBattleHeroineId + sexTouchStatusManager.heroineSexLvStage, 1f, null, 0f, null, null);
			if (playSoundResult2 != null && playSoundResult2.SoundPlayed)
			{
				playSoundResult2.ActingVariation.SoundFinished += VoiceFinished;
			}
		}
		sexTouchManager.touchWhiteImageCanvasGroup.DOFade(0f, 0.1f);
		isVoiceStart = true;
		sexTouchManager.skipInfoFrame.SetActive(value: true);
	}

	private void VoiceFinished()
	{
		if (!isSkiped)
		{
			Transition(stateLink);
		}
	}
}
