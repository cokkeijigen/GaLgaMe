using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
	public PlayMakerFSM transitionFSM;

	private List<AsyncOperation> asyncOperationLoadList = new List<AsyncOperation>();

	private List<AsyncOperation> asyncOperationUnLoadList = new List<AsyncOperation>();

	private List<string> loadSceneNameList = new List<string>();

	private List<string> unLoadSceneNameList = new List<string>();

	private Coroutine coroutine;

	public void SetLoadSceneNameList(string[] fsmArray, int listCount)
	{
		loadSceneNameList.Clear();
		asyncOperationLoadList.Clear();
		for (int i = 0; i < listCount; i++)
		{
			string item = fsmArray[i].ToString();
			loadSceneNameList.Add(item);
			asyncOperationLoadList.Add(null);
		}
	}

	public void SetUnLoadSceneNameList(string[] fsmArray, int listCount)
	{
		unLoadSceneNameList.Clear();
		asyncOperationUnLoadList.Clear();
		for (int i = 0; i < listCount; i++)
		{
			string item = fsmArray[i].ToString();
			unLoadSceneNameList.Add(item);
			asyncOperationUnLoadList.Add(null);
		}
	}

	public void StartSceneLoad()
	{
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
			Debug.Log("ロード待機中");
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
		PlayerNonSaveDataManager.isSaveDataLoad = false;
		Debug.Log("シーンロード終了");
		StopCoroutine(coroutine);
		transitionFSM.SendEvent("EndSceneLoad");
	}

	public void StartSystemSceneUnLoad()
	{
		coroutine = StartCoroutine(SystemSceneUnLoadCoroutine());
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
		PlayerNonSaveDataManager.isSaveDataLoad = false;
		Debug.Log("システムシーンのアンロード終了");
		StopCoroutine(coroutine);
	}
}
