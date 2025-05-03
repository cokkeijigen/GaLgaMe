using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class MasterAudioVoiceFadeStop : StateBehaviour
{
	private MasterAudioCustomManager masterAudioCustomManager;

	public float fadeTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		masterAudioCustomManager = GameObject.Find("Bgm Play Manager").GetComponent<MasterAudioCustomManager>();
	}

	public override void OnStateBegin()
	{
		float endValue = Mathf.Clamp(Mathf.Log10(0f) * 20f, -80f, 0f);
		masterAudioCustomManager.audioMixer.DOSetFloat("MAVolumeVoice", endValue, fadeTime).OnComplete(CallBackMethod);
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

	private void CallBackMethod()
	{
		MasterAudio.StopBus("Voice1");
		MasterAudio.StopBus("Voice2");
		MasterAudio.StopBus("Voice3");
		MasterAudio.StopBus("Voice4");
		float value = Mathf.Clamp(Mathf.Log10(1f) * 20f, -80f, 0f);
		masterAudioCustomManager.audioMixer.SetFloat("MAVolumeVoice", value);
		Transition(stateLink);
	}
}
