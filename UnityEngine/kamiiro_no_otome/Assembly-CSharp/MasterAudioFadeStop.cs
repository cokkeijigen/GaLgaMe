using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class MasterAudioFadeStop : StateBehaviour
{
	public float fadeTime;

	public bool willStopAfterFade;

	public bool willResetVolumeAfterFade;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		MasterAudio.FadePlaylistToVolume("PlaylistController", 0f, fadeTime);
		Invoke("CallBackMethod", fadeTime);
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
		PlaylistController component = GameObject.Find("PlaylistController").GetComponent<PlaylistController>();
		MasterAudio.StopPlaylist("PlaylistController");
		component.PlaylistVolume = PlayerOptionsDataManager.optionsBgmVolume;
		Transition(stateLink);
	}
}
