using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("")]
public class GameSceneLoadAsync : StateBehaviour
{
	public float masterAudioFadeTime;

	private List<AsyncOperation> asyncOperationLoadList;

	private List<AsyncOperation> asyncOperationUnLoadList;

	private List<string> loadSceneNameList;

	private List<string> unLoadSceneNameList;

	private bool isDontLoad;

	private Coroutine coroutine;

	private bool isBgmFade;

	public PlaylistController playlistController;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		bool flag = false;
		isDontLoad = false;
		switch (PlayerNonSaveDataManager.loadSceneName)
		{
		case "title":
			if (PlayerNonSaveDataManager.isGarellyOpenWithTitle)
			{
				loadSceneNameList = new List<string> { "title", "garellyUI" };
				asyncOperationLoadList = new List<AsyncOperation> { null, null };
			}
			else
			{
				loadSceneNameList = new List<string> { "title" };
				asyncOperationLoadList = new List<AsyncOperation> { null };
			}
			break;
		case "main":
			if (PlayerNonSaveDataManager.currentSceneName == "main")
			{
				isDontLoad = true;
			}
			if (PlayerNonSaveDataManager.isUtageToJumpFromGarelly)
			{
				loadSceneNameList = new List<string> { "totalMap", "statusUI", "garellyUI" };
				asyncOperationLoadList = new List<AsyncOperation> { null, null, null };
			}
			else
			{
				loadSceneNameList = new List<string> { "totalMap", "statusUI" };
				asyncOperationLoadList = new List<AsyncOperation> { null, null };
			}
			break;
		case "scenario":
			if (PlayerNonSaveDataManager.currentSceneName == "scenario")
			{
				isDontLoad = true;
			}
			flag = true;
			loadSceneNameList = new List<string> { "scenario", "scenarioBattle", "dungeonBattle" };
			asyncOperationLoadList = new List<AsyncOperation> { null, null, null };
			break;
		}
		switch (PlayerNonSaveDataManager.currentSceneName)
		{
		case "title":
			if (PlayerNonSaveDataManager.isGarellyOpenWithTitle)
			{
				unLoadSceneNameList = new List<string> { "title", "garellyUI" };
				asyncOperationUnLoadList = new List<AsyncOperation> { null, null };
			}
			else
			{
				unLoadSceneNameList = new List<string> { "title" };
				asyncOperationUnLoadList = new List<AsyncOperation> { null };
			}
			break;
		case "main":
			if (PlayerNonSaveDataManager.isUtageToJumpFromGarelly)
			{
				unLoadSceneNameList = new List<string> { "totalMap", "statusUI", "garellyUI" };
				asyncOperationUnLoadList = new List<AsyncOperation> { null, null, null };
			}
			else
			{
				unLoadSceneNameList = new List<string> { "totalMap", "statusUI" };
				asyncOperationUnLoadList = new List<AsyncOperation> { null, null };
			}
			break;
		case "scenario":
			unLoadSceneNameList = new List<string> { "scenario", "scenarioBattle", "dungeonBattle" };
			asyncOperationUnLoadList = new List<AsyncOperation> { null, null, null };
			break;
		}
		if (isDontLoad)
		{
			coroutine = StartCoroutine(SystemSceneUnLoadCoroutine());
			return;
		}
		if (!flag)
		{
			Debug.Log("シナリオ以外のシーンをロード");
			StartCoroutineMethod();
			return;
		}
		Debug.Log("シナリオのシーンをロード");
		Debug.Log("プレイリスト音量フェード前：" + playlistController.PlaylistVolume);
		isBgmFade = true;
		MasterAudio.FadePlaylistToVolume("PlaylistController", 0f, masterAudioFadeTime);
		Invoke("StartCoroutineMethod", masterAudioFadeTime);
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

	private void StartCoroutineMethod()
	{
		if (isBgmFade)
		{
			MasterAudio.StopPlaylist("PlaylistController");
			playlistController.PlaylistVolume = 1f;
		}
		coroutine = StartCoroutine(GameSceneLoadCoroutine());
	}

	private IEnumerator GameSceneLoadCoroutine()
	{
		Debug.Log("シーンロード開始／ロード数：" + loadSceneNameList.Count);
		for (int i = 0; i < loadSceneNameList.Count; i++)
		{
			asyncOperationLoadList[i] = SceneManager.LoadSceneAsync(loadSceneNameList[i], LoadSceneMode.Additive);
			asyncOperationLoadList[i].allowSceneActivation = false;
		}
		float[] progressArray = new float[loadSceneNameList.Count];
		while (true)
		{
			for (int j = 0; j < loadSceneNameList.Count; j++)
			{
				progressArray[j] = asyncOperationLoadList[j].progress;
			}
			if (progressArray.All((float data) => data >= 0.9f))
			{
				break;
			}
			yield return null;
		}
		Debug.Log("シーンロード完了");
		for (int k = 0; k < loadSceneNameList.Count; k++)
		{
			asyncOperationLoadList[k].allowSceneActivation = true;
		}
		string text = loadSceneNameList[0];
		Scene scene = SceneManager.GetSceneByName(text);
		while (!scene.isLoaded)
		{
			yield return null;
		}
		SceneManager.SetActiveScene(scene);
		Debug.Log("カレントシーンのアンロード開始／アンロード数：" + unLoadSceneNameList.Count);
		for (int l = 0; l < unLoadSceneNameList.Count; l++)
		{
			Debug.Log("アンロードするシーン名：" + unLoadSceneNameList[l]);
			asyncOperationUnLoadList[l] = SceneManager.UnloadSceneAsync(unLoadSceneNameList[l]);
		}
		float[] unProgressArray = new float[unLoadSceneNameList.Count];
		while (true)
		{
			for (int m = 0; m < unLoadSceneNameList.Count; m++)
			{
				unProgressArray[m] = asyncOperationUnLoadList[m].progress;
			}
			if (unProgressArray.All((float data) => data >= 0.9f))
			{
				break;
			}
			yield return null;
		}
		Debug.Log("カレントシーン削除完了");
		if (PlayerNonSaveDataManager.isSaveDataLoad)
		{
			Debug.Log("システムシーンのアンロード開始");
			AsyncOperation systemAsync = SceneManager.UnloadSceneAsync("systemUI");
			while (!(systemAsync.progress >= 0.9f))
			{
				yield return null;
			}
			Debug.Log("システムシーン削除完了");
		}
		EndCoroutineMethod();
	}

	private void EndCoroutineMethod()
	{
		Debug.Log("シーンロード終了");
		StopCoroutine(coroutine);
		Transition(stateLink);
	}

	private IEnumerator SystemSceneUnLoadCoroutine()
	{
		Debug.Log("システムシーンのアンロード開始");
		AsyncOperation systemAsync = SceneManager.UnloadSceneAsync("systemUI");
		while (!(systemAsync.progress >= 0.9f))
		{
			yield return null;
		}
		Debug.Log("システムシーン削除完了");
		DontSceneLoad();
	}

	private void DontSceneLoad()
	{
		if (PlayerNonSaveDataManager.loadSceneName == "main")
		{
			Debug.Log("同一のシーンをロード：main");
			GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>().totalMapFSM.SendTrigger("RefreshOpenMap");
		}
		else
		{
			Debug.Log("同一のシーンをロード：scenario");
		}
		PlayerNonSaveDataManager.isSaveDataLoad = false;
		Debug.Log("システムシーンのアンロード終了");
		StopCoroutine(coroutine);
		Transition(stateLink);
	}
}
