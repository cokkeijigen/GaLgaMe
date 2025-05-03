using System.Collections;
using Arbor;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("")]
public class PreStartCarriageStoreAnimation : StateBehaviour
{
	private CarriageStoreNoticeManager carriageStoreNoticeManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		carriageStoreNoticeManager = GameObject.Find("Store Tending Manager").GetComponent<CarriageStoreNoticeManager>();
	}

	public override void OnStateBegin()
	{
		carriageStoreNoticeManager.animationCanvasGo.SetActive(value: true);
		GameObject.Find("Carriage Cancel Manager").GetComponent<CarriageStoreCancelManagerForPM>().PreCloseCarriageStoreUI();
		StartCoroutine("StoreSceneUnLoadCoroutine");
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

	private IEnumerator StoreSceneUnLoadCoroutine()
	{
		Debug.Log("ストアシーンのアンロード開始");
		AsyncOperation systemAsync = SceneManager.UnloadSceneAsync("carriageStoreUI");
		while (!(systemAsync.progress >= 0.9f))
		{
			yield return null;
		}
		Debug.Log("ストアシーン削除完了");
		Transition(stateLink);
	}
}
