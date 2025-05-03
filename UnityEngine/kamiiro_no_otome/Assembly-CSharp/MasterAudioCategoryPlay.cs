using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class MasterAudioCategoryPlay : StateBehaviour
{
	private PlaylistController playlistController;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		playlistController = GameObject.Find("PlaylistController").GetComponent<PlaylistController>();
	}

	public override void OnStateBegin()
	{
		string text = "";
		Debug.Log("BGMカテゴリ" + PlayerDataManager.playBgmCategoryName);
		switch (PlayerDataManager.playBgmCategoryName)
		{
		case "title":
			text = "BgmTitle";
			break;
		case "localMap":
			text = GameObject.Find("WorldMap Access Manager").GetComponent<WorldMapAccessManager>().worldMapUnlockDataBase.worldMapUnlockDataList.Find((WorldMapUnlockData data) => data.currentPointName == PlayerDataManager.currentAccessPointName).bgmCategoryName;
			break;
		case "worldMap":
			text = "BgmWorldMap1";
			break;
		case "scenarioBattle":
			text = "BgmScenarioBattle1";
			break;
		case "scenarioBoss":
			text = "BgmScenarioBattle2";
			break;
		case "dungeon":
			text = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerDataManager.currentDungeonName).dungeonBgmName;
			break;
		case "dungeonBattle":
			text = "BgmDungeonBattle1";
			break;
		case "dungeonBoss":
			text = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerDataManager.currentDungeonName).dungeonBossBgmName;
			break;
		case "dungeonDeepBoss":
			text = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerDataManager.currentDungeonName).dungeonDeepBossBgmName;
			break;
		}
		playlistController.PlaylistVolume = 1f;
		Debug.Log("プレイリスト名：" + text);
		MasterAudio.StartPlaylistOnClip("Playlist", text);
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
