using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class CheckBattleBgmPlaylistStop : StateBehaviour
{
	public float fadeTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		if (PlayerNonSaveDataManager.isUtagePlayBattleBgmNonStop)
		{
			Transition(stateLink);
			return;
		}
		MasterAudio.FadePlaylistToVolume("PlaylistController", 0f, fadeTime);
		Invoke("CallBackMethod", fadeTime);
	}

	public override void OnStateEnd()
	{
		PlayerNonSaveDataManager.isUtagePlayBattleBgmNonStop = false;
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
		component.PlaylistVolume = 1f;
		Transition(stateLink);
	}
}
