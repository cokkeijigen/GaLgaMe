using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class CheckSexVoiceLoaded : StateBehaviour
{
	private SexTouchHeroineDataManager sexTouchHeroineDataManager;

	private bool isInitialized;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexTouchHeroineDataManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchHeroineDataManager>();
	}

	public override void OnStateBegin()
	{
		if (sexTouchHeroineDataManager.dynamicSoundGroup == null)
		{
			Debug.Log("音声リソースは未生成");
			isInitialized = false;
			string path = "Sex Voice/DynamicSoundGroup_SexTouch_" + PlayerNonSaveDataManager.selectSexBattleHeroineId;
			sexTouchHeroineDataManager.dynamicSoundGroup = Resources.Load<DynamicSoundGroupCreator>(path);
		}
		else
		{
			Debug.Log("すでに音声リソースは生成済み");
			isInitialized = true;
			Transition(stateLink);
		}
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		if (sexTouchHeroineDataManager.dynamicSoundGroup != null && !isInitialized)
		{
			isInitialized = true;
			Object.Instantiate(sexTouchHeroineDataManager.dynamicSoundGroup);
			GameObject.Find("Bgm Play Manager").GetComponent<MasterAudioCustomManager>().ChangeMasterAudioVoiceVolume();
			Transition(stateLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
