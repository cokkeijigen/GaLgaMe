using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class PlaySexInsertVoice : StateBehaviour
{
	private SexTouchManager sexTouchManager;

	private SexTouchStatusManager sexTouchStatusManager;

	private bool isPlayStart;

	private string voiceFileName;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexTouchManager = GameObject.Find("Sex Touch Manager").GetComponent<SexTouchManager>();
		sexTouchStatusManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchStatusManager>();
	}

	public override void OnStateBegin()
	{
		isPlayStart = false;
		string text = "";
		int num = Random.Range(1, 3);
		if (PlayerSexStatusDataManager.heroineSexLv[PlayerNonSaveDataManager.selectSexBattleHeroineId - 1] < 4)
		{
			voiceFileName = "Voice_Insert_" + PlayerNonSaveDataManager.selectSexBattleHeroineId + "A";
			text = "voice_Insert" + PlayerNonSaveDataManager.selectSexBattleHeroineId + "_A" + num;
		}
		else
		{
			voiceFileName = "Voice_Insert_" + PlayerNonSaveDataManager.selectSexBattleHeroineId + "B";
			text = "voice_Insert" + PlayerNonSaveDataManager.selectSexBattleHeroineId + "_B" + num;
		}
		Debug.Log("音声名：" + voiceFileName + "／バリエーション：" + text);
		sexTouchManager.insertCanvas.SetActive(value: true);
		sexTouchManager.insertTextLoc.Term = text;
		MasterAudio.PlaySound(voiceFileName, 1f, null, 0f, text, null);
		isPlayStart = true;
	}

	public override void OnStateEnd()
	{
		isPlayStart = false;
	}

	public override void OnStateUpdate()
	{
		if (!MasterAudio.IsSoundGroupPlaying(voiceFileName) && isPlayStart)
		{
			sexTouchManager.insertCanvas.SetActive(value: false);
			Transition(stateLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
