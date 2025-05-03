using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class StopAndPlayDungeonSound : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	public bool isBattle;

	public float fadeTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
	}

	public override void OnStateBegin()
	{
		MasterAudio.FadeBusToVolume("BGM", 0f, fadeTime);
		Invoke("MasterAudioCallBack", fadeTime);
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

	private void MasterAudioCallBack()
	{
		MasterAudio.SetBusVolumeByName("BGM", 1f);
		string text = "";
		text = ((!isBattle) ? GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerDataManager.currentDungeonName).dungeonBgmName : ((!dungeonMapManager.isBossRouteSelect) ? "BgmBattle1" : GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerDataManager.currentDungeonName).dungeonBossBgmName));
		MasterAudio.PlaySound(text, 1f, null, 0f, null, null);
		Transition(stateLink);
	}
}
