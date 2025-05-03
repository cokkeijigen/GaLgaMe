using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class SetMasterAudioVolume : StateBehaviour
{
	private PlaylistController playlistController;

	private MasterAudioCustomManager masterAudioCustomManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		playlistController = GameObject.Find("PlaylistController").GetComponent<PlaylistController>();
		masterAudioCustomManager = GameObject.Find("Bgm Play Manager").GetComponent<MasterAudioCustomManager>();
	}

	public override void OnStateBegin()
	{
		float value = Mathf.Clamp(Mathf.Log10(PlayerOptionsDataManager.optionsSeVolume) * 20f, -80f, 0f);
		masterAudioCustomManager.audioMixer.SetFloat("MAVolumeSe", value);
		playlistController.PlaylistVolume = 1f;
		Debug.Log("MasterAudio音量変更");
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
