using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class MasterAudioAllPlaylistFade : StateBehaviour
{
	public float setVolume;

	public float fadeTime;

	public bool isBackToOriginVolume;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		if (isBackToOriginVolume)
		{
			MasterAudio.FadeAllPlaylistsToVolume(1f, fadeTime);
		}
		else
		{
			MasterAudio.FadeAllPlaylistsToVolume(setVolume, fadeTime);
		}
		Transition(stateLink);
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
}
