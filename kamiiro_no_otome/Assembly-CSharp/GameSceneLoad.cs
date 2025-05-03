using System.Collections.Generic;
using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("")]
public class GameSceneLoad : StateBehaviour
{
	public float masterAudioFadeTime;

	private int loadSceneCount;

	private int unLoadSceneCount;

	private int needLoadSceneCount;

	private int needUnLoadSceneCount;

	private List<string> unLoadSceneNameList;

	[SerializeField]
	private StateLink loadEnd;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		SceneManager.sceneLoaded += SceneLoaded;
		SceneManager.sceneUnloaded += SceneUnLoaded;
		loadSceneCount = 0;
		unLoadSceneCount = 0;
		string loadSceneName = PlayerNonSaveDataManager.loadSceneName;
		if (!(loadSceneName == "main"))
		{
			if (loadSceneName == "scenario")
			{
				needLoadSceneCount = 2;
				SceneManager.LoadSceneAsync("scenarioBattle", LoadSceneMode.Additive);
				SceneManager.LoadSceneAsync("dungeonBattle", LoadSceneMode.Additive);
				SceneManager.LoadSceneAsync("scenario", LoadSceneMode.Additive);
				MasterAudio.FadeBusToVolume("BGM", 0f, masterAudioFadeTime, null, willStopAfterFade: true, willResetVolumeAfterFade: true);
			}
		}
		else
		{
			needLoadSceneCount = 3;
			SceneManager.LoadSceneAsync("statusUI", LoadSceneMode.Additive);
			SceneManager.LoadSceneAsync("localMap", LoadSceneMode.Additive);
			SceneManager.LoadSceneAsync("worldMap", LoadSceneMode.Additive);
			MasterAudio.FadeBusToVolume("BGM", 1f, masterAudioFadeTime, null, willStopAfterFade: true, willResetVolumeAfterFade: true);
		}
	}

	public override void OnStateEnd()
	{
		SceneManager.sceneLoaded -= SceneLoaded;
		SceneManager.sceneUnloaded -= SceneUnLoaded;
	}

	public override void OnStateUpdate()
	{
	}

	public override void OnStateLateUpdate()
	{
	}

	private void SceneLoaded(Scene scene, LoadSceneMode mode)
	{
		loadSceneCount++;
		if (loadSceneCount == 1)
		{
			SceneManager.SetActiveScene(scene);
		}
		if (loadSceneCount < needLoadSceneCount)
		{
			return;
		}
		Debug.Log("シーンロード完了");
		switch (PlayerNonSaveDataManager.currentSceneName)
		{
		case "title":
			needUnLoadSceneCount = 1;
			unLoadSceneNameList = new List<string> { "title" };
			break;
		case "main":
			needUnLoadSceneCount = 3;
			unLoadSceneNameList = new List<string> { "localMap", "worldMap", "statusUI" };
			break;
		case "scenario":
			needUnLoadSceneCount = 2;
			unLoadSceneNameList = new List<string> { "scenario", "scenarioBattle", "dungeonBattle" };
			break;
		}
		foreach (string unLoadSceneName in unLoadSceneNameList)
		{
			SceneManager.UnloadSceneAsync(unLoadSceneName);
		}
	}

	private void SceneUnLoaded(Scene scene)
	{
		unLoadSceneCount++;
		if (unLoadSceneCount >= needUnLoadSceneCount)
		{
			GameObject gameObject = GameObject.FindWithTag("MainCamera");
			if (gameObject.GetComponent<AudioListener>() == null)
			{
				gameObject.AddComponent<AudioListener>();
				Debug.Log("カレントシーン削除完了");
			}
			Transition(loadEnd);
		}
	}
}
