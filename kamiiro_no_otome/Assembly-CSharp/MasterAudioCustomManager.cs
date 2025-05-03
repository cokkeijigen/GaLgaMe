using DarkTonic.MasterAudio;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MasterAudioCustomManager : MonoBehaviour
{
	public PlaylistController playlistController;

	public AudioMixer audioMixer;

	public void ChangeMasterAudioVolume()
	{
		string text = SceneManager.GetActiveScene().name;
		if (text == "sexTouch")
		{
			Debug.Log("BGM音量を変更する／身体検査");
			Debug.Log("現在のシーン名：" + text);
			float value = Mathf.Clamp(Mathf.Log10(PlayerOptionsDataManager.optionsHBgmVolume) * 20f, -80f, 0f);
			audioMixer.SetFloat("MAVolumeBgm", value);
		}
		else
		{
			Debug.Log("BGM音量を変更する／通常");
			float value2 = Mathf.Clamp(Mathf.Log10(PlayerOptionsDataManager.optionsBgmVolume) * 20f, -80f, 0f);
			audioMixer.SetFloat("MAVolumeBgm", value2);
		}
		float value3 = Mathf.Clamp(Mathf.Log10(PlayerOptionsDataManager.optionsSeVolume) * 20f, -80f, 0f);
		audioMixer.SetFloat("MAVolumeSe", value3);
		if (PlayerOptionsDataManager.isAllVoiceDisable)
		{
			float value4 = Mathf.Clamp(Mathf.Log10(0f) * 20f, -80f, 0f);
			audioMixer.SetFloat("MAVolumeLucy", value4);
			audioMixer.SetFloat("MAVolumeRina", value4);
			audioMixer.SetFloat("MAVolumeShia", value4);
			audioMixer.SetFloat("MAVolumeLevy", value4);
		}
		else
		{
			float value5 = Mathf.Clamp(Mathf.Log10(PlayerOptionsDataManager.optionsVoice1Volume) * 20f, -80f, 0f);
			float value6 = Mathf.Clamp(Mathf.Log10(PlayerOptionsDataManager.optionsVoice2Volume) * 20f, -80f, 0f);
			float value7 = Mathf.Clamp(Mathf.Log10(PlayerOptionsDataManager.optionsVoice3Volume) * 20f, -80f, 0f);
			float value8 = Mathf.Clamp(Mathf.Log10(PlayerOptionsDataManager.optionsVoice4Volume) * 20f, -80f, 0f);
			audioMixer.SetFloat("MAVolumeLucy", value5);
			audioMixer.SetFloat("MAVolumeRina", value6);
			audioMixer.SetFloat("MAVolumeShia", value7);
			audioMixer.SetFloat("MAVolumeLevy", value8);
		}
		Debug.Log("MasterAudio音量変更");
	}

	public void FadeMasterAudioPlaylist()
	{
		float masterAudioFadeTime = PlayerNonSaveDataManager.masterAudioFadeTime;
		DOTween.To(() => playlistController.PlaylistVolume, delegate(float x)
		{
			playlistController.PlaylistVolume = x;
		}, 0f, masterAudioFadeTime).OnComplete(StopMasterAudioPlaylist);
		Debug.Log("BGMプレイリストをフェード停止");
	}

	public void StopMasterAudioPlaylist()
	{
		MasterAudio.StopPlaylist();
		Debug.Log("BGMプレイリストを停止");
	}

	public void ChangeMasterAudioPlaylist()
	{
		string text = "";
		bool flag = false;
		Debug.Log("BGMカテゴリ" + PlayerDataManager.playBgmCategoryName);
		switch (PlayerDataManager.playBgmCategoryName)
		{
		case "title":
			text = "BgmTitle";
			break;
		case "localMap":
		{
			string currentAccessPointName = PlayerDataManager.currentAccessPointName;
			if (!(currentAccessPointName == "Kingdom1"))
			{
				if (currentAccessPointName == "City1")
				{
					text = "BgmLocalMap2";
				}
			}
			else
			{
				text = "BgmLocalMap1";
			}
			if (PlayerDataManager.mapPlaceStatusNum == 2)
			{
				if (!string.IsNullOrEmpty(GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>().localMapUnlockDataBase.localMapUnlockDataList.Find((LocalMapUnlockData data) => data.currentPlaceName == PlayerDataManager.currentPlaceName).inDoorBgmName))
				{
					flag = true;
					Debug.Log("インドアに固有BGMがある");
				}
				else
				{
					Debug.Log("インドアに固有BGMはない");
				}
			}
			break;
		}
		case "worldMap":
			text = "BgmWorldMap1";
			break;
		case "scenarioBattle":
			text = "BgmScenarioBattle1";
			break;
		case "scenarioBoss":
			text = "BgmScenarioBattle2";
			break;
		case "scenarioBoss2":
			text = "BgmScenarioBattle7";
			break;
		case "scenarioBoss3":
			text = "BgmScenarioBattle8";
			break;
		case "scenarioBigBoss":
			text = "BgmScenarioBattle3";
			break;
		case "scenarioBigBoss2":
			text = "BgmScenarioBattle4";
			break;
		case "scenarioBigBoss3":
			text = "BgmScenarioBattle6";
			break;
		case "scenarioDefeatBoss":
			text = "BgmScenarioBattle5";
			break;
		case "kessenBattle":
			text = "BgmKessenBattle";
			break;
		case "gruzzoBattle":
			text = "Bgm_GruzzoBattle";
			break;
		case "levyBattle":
			text = "Bgm_LevyBattle";
			break;
		case "rinaBattle":
			text = "BGM_danchouSentoukyoku";
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
		case "sexTouch":
			text = "BgmSexTouch";
			break;
		case "sexBattle":
			text = "BgmSexBattle";
			break;
		case "utagePlaylist":
			text = PlayerNonSaveDataManager.utagePlayBattleBgmName;
			break;
		}
		bool flag2 = playlistController.IsSongPlaying(text);
		if (!flag2)
		{
			if (!flag)
			{
				float value = Mathf.Clamp(Mathf.Log10(PlayerOptionsDataManager.optionsBgmVolume) * 20f, -80f, 0f);
				audioMixer.SetFloat("MAVolumeBgm", value);
				playlistController.PlaylistVolume = 1f;
				Debug.Log("BGM音量を戻す／" + text + "／isSongPlaying：" + flag2);
				PlayerNonSaveDataManager.currentBgmCategoryName = PlayerDataManager.playBgmCategoryName;
				MasterAudio.StartPlaylistOnClip("Playlist", text);
				Debug.Log("BGMを再生／" + text);
			}
			else
			{
				Debug.Log("宴から直接インドアに行くので、ローカルのBGMは再生なし");
			}
		}
		else if (PlayerDataManager.playBgmCategoryName == "sexTouch" || PlayerDataManager.playBgmCategoryName == "sexBattle")
		{
			float value2 = Mathf.Clamp(Mathf.Log10(PlayerOptionsDataManager.optionsHBgmVolume) * 20f, -80f, 0f);
			audioMixer.SetFloat("MAVolumeBgm", value2);
			playlistController.PlaylistVolume = 1f;
			Debug.Log("HシーンのBGM音量を適用");
		}
		else
		{
			Debug.Log("同じBGM");
		}
	}

	public void ChangeInDoorMasterAudioPlaylist()
	{
		string text = "";
		float num = 0f;
		bool flag = false;
		Debug.Log("インドアBGMカテゴリ" + PlayerDataManager.playBgmCategoryName);
		switch (PlayerDataManager.playBgmCategoryName)
		{
		case "BgmVillage":
			text = "BgmVillage";
			break;
		case "BgmCalliage":
			text = "BgmCalliage";
			flag = true;
			break;
		case "BgmHunterGuild":
			text = "BgmHunterGuild";
			break;
		case "BgmPirateHideout":
			text = "BgmPirateHideout";
			flag = true;
			break;
		case "BgmItemShop":
			text = "BgmItemShop";
			break;
		}
		bool flag2 = playlistController.IsSongPlaying(text);
		if (!flag2)
		{
			PlayerNonSaveDataManager.currentBgmCategoryName = PlayerDataManager.playBgmCategoryName;
			MasterAudio.StartPlaylistOnClip("Playlist", text);
			Debug.Log("BGMを再生");
		}
		else
		{
			Debug.Log("同じBGM");
		}
		num = ((!flag) ? Mathf.Clamp(Mathf.Log10(PlayerOptionsDataManager.optionsBgmVolume) * 20f, -80f, 0f) : Mathf.Clamp(Mathf.Log10(PlayerOptionsDataManager.optionsAmbienceVolume) * 20f, -80f, 0f));
		audioMixer.SetFloat("MAVolumeBgm", num);
		playlistController.PlaylistVolume = 1f;
		Debug.Log("BGM音量を戻す／" + text + "／isSongPlaying：" + flag2);
	}

	public void ChangeMasterAudioVoiceVolume()
	{
		switch (PlayerNonSaveDataManager.selectSexBattleHeroineId)
		{
		case 1:
		{
			float value4 = Mathf.Clamp(Mathf.Log10(PlayerOptionsDataManager.optionsVoice1Volume) * 20f, -80f, 0f);
			audioMixer.SetFloat("MAVolumeLucy", value4);
			break;
		}
		case 2:
		{
			float value3 = Mathf.Clamp(Mathf.Log10(PlayerOptionsDataManager.optionsVoice2Volume) * 20f, -80f, 0f);
			audioMixer.SetFloat("MAVolumeRina", value3);
			break;
		}
		case 3:
		{
			float value2 = Mathf.Clamp(Mathf.Log10(PlayerOptionsDataManager.optionsVoice3Volume) * 20f, -80f, 0f);
			audioMixer.SetFloat("MAVolumeShia", value2);
			break;
		}
		case 4:
		{
			float value = Mathf.Clamp(Mathf.Log10(PlayerOptionsDataManager.optionsVoice4Volume) * 20f, -80f, 0f);
			audioMixer.SetFloat("MAVolumeLevy", value);
			break;
		}
		}
	}
}
