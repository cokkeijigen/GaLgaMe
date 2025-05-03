using System.Collections.Generic;
using Arbor;
using Coffee.UIExtensions;
using DarkTonic.MasterAudio;
using I2.Loc;
using PathologicalGames;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class CarriageStoreNoticeManager : SerializedMonoBehaviour
{
	private HeaderStatusManager headerStatusManager;

	public ArborFSM storeTendingFSM;

	public GameObject animationCanvasGo;

	public GameObject carriageStoreNoticeWindow;

	public Button carriageStoreNoticeOkButton;

	public Localize storeTendingResultTextLoc;

	public Text carriageStoreTradeCountText;

	public Text carriageStoreTradeQuantityText;

	public Text carriageStoreTradeMoneyText;

	public PlayableDirector storeDirector;

	public PlayableAsset[] storeTendingTimelineArray;

	public Image storeTendingBgImage;

	public Image storeNoticeBgImage;

	public Dictionary<string, Sprite> storeTendingBgSpriteDictionary;

	public Dictionary<string, Sprite> storeNoticeBgSpriteDictionary;

	public PlayableDirector storeNoticeDirector;

	public UIParticle uIParticle;

	public GameObject moneyImageGo;

	private Transform effectSpawnGo;

	public GameObject storeSuccessPrefabGo;

	public GameObject storeFailurePrefabGo;

	public GameObject poolParentGo;

	private bool isSellSuccess;

	private void Awake()
	{
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		animationCanvasGo.SetActive(value: false);
		carriageStoreNoticeWindow.SetActive(value: false);
	}

	public void StartCarriageStoreNotice()
	{
		animationCanvasGo.SetActive(value: true);
		storeTendingFSM.SendTrigger("StartStoreNoticeAnimation");
	}

	public void OpenCarriageStoreNotice()
	{
		if (PlayerDataManager.carriageStoreTradeCount > 0)
		{
			Debug.Log("アイテムは売れた／個数：" + PlayerDataManager.carriageStoreTradeCount);
			storeTendingResultTextLoc.Term = "storeTendingSuccess";
			carriageStoreTradeCountText.gameObject.SetActive(value: true);
			carriageStoreTradeQuantityText.gameObject.SetActive(value: true);
			carriageStoreTradeCountText.text = PlayerDataManager.carriageStoreTradeCount.ToString();
			PlayerDataManager.AddHaveMoney(PlayerDataManager.carriageStoreTradeMoneyNum);
			PlayerDataManager.totalSalesAmount += PlayerDataManager.carriageStoreTradeMoneyNum;
			PlayerDataManager.totalSalesAmount = (int)Mathf.Clamp(PlayerDataManager.totalSalesAmount, 0f, 1E+10f);
			PlayerQuestDataManager.RefreshStoryQuestFlagData("carriageStore", PlayerDataManager.carriageStoreTradeCount);
		}
		else
		{
			Debug.Log("アイテムは売れなかった／個数：" + PlayerDataManager.carriageStoreTradeCount);
			storeTendingResultTextLoc.Term = "storeTendingFailed";
			carriageStoreTradeCountText.gameObject.SetActive(value: false);
			carriageStoreTradeQuantityText.gameObject.SetActive(value: false);
		}
		carriageStoreTradeMoneyText.text = PlayerDataManager.carriageStoreTradeMoneyNum.ToString();
		carriageStoreNoticeOkButton.interactable = false;
		carriageStoreNoticeWindow.SetActive(value: true);
		storeNoticeDirector.time = 0.0;
		storeNoticeDirector.Play();
		Debug.Log("販売結果のアニメを再生／売り上げ個数：" + PlayerDataManager.carriageStoreTradeCount);
		headerStatusManager.carriageStoreButton.interactable = true;
	}

	public void SpawnStoreNoticeEffect()
	{
		effectSpawnGo = null;
		Debug.Log("売り上げ通知のエフェクトをスポーンする／売上個数：" + PlayerDataManager.carriageStoreTradeCount);
		if (PlayerDataManager.carriageStoreTradeCount > 0)
		{
			effectSpawnGo = PoolManager.Pools["totalMapPool"].Spawn(storeSuccessPrefabGo, uIParticle.transform);
			MasterAudio.PlaySound("SeShopSell", 1f, null, 0f, null, null);
			Debug.Log("陳列販売：売れた");
		}
		else
		{
			effectSpawnGo = PoolManager.Pools["totalMapPool"].Spawn(storeFailurePrefabGo, uIParticle.transform);
			MasterAudio.PlaySound("SeShopFailure", 1f, null, 0f, null, null);
			Debug.Log("陳列販売：売れなかった");
		}
		effectSpawnGo.localScale = new Vector3(1f, 1f, 1f);
		effectSpawnGo.localPosition = new Vector3(0f, 0f, 0f);
		Debug.Log("売り上げエフェクトをスポーン");
		uIParticle.RefreshParticles();
	}

	public void SetCarriageStoreNoticeButtonInteractable()
	{
		carriageStoreNoticeOkButton.interactable = true;
	}

	public bool GetCarriageStoreNoticeButtonInteractable()
	{
		return carriageStoreNoticeOkButton.interactable;
	}

	public void PushCarriageStoreOkButton()
	{
		Debug.Log("陳列販売リザルトのOKボタンを押すと呼ばれる");
		animationCanvasGo.SetActive(value: false);
		carriageStoreNoticeWindow.SetActive(value: false);
		isSellSuccess = false;
		if (PlayerDataManager.carriageStoreTradeCount > 0)
		{
			isSellSuccess = true;
		}
		PlayerDataManager.carriageStoreTradeCount = 0;
		PlayerDataManager.carriageStoreTradeMoneyNum = 0;
		PlayerDataManager.storeTradeSuccessItemList.Clear();
		PlayerDataManager.isStoreTending = false;
		PlayerNonSaveDataManager.isCheckShopRankChange = true;
		if (PoolManager.Pools["totalMapPool"].IsSpawned(effectSpawnGo))
		{
			PoolManager.Pools["totalMapPool"].Despawn(effectSpawnGo, poolParentGo.transform);
		}
		uIParticle.RefreshParticles();
		foreach (Transform item in uIParticle.gameObject.transform)
		{
			Object.Destroy(item.gameObject);
		}
		GameObject.Find("Header Status Manager").GetComponent<ArborFSM>().SendTrigger("HeaderStatusRefresh");
		GameObject.Find("Quest Check Manager").GetComponent<ArborFSM>().SendTrigger("RefreshEnableQuestList");
		if (PlayerNonSaveDataManager.isOpencarriageStoreResult)
		{
			PlayerNonSaveDataManager.isOpencarriageStoreResult = false;
		}
		else
		{
			GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>().carriageStoreNoticeIconGo.SetActive(value: false);
		}
	}

	public void CheckCarriageEvent()
	{
		if (isSellSuccess)
		{
			ExceptionEventCheckData exceptionEventCheckData = PlayerExceptionScenarioDataManager.CheckCariageStoreCurrentEvent();
			if (string.IsNullOrEmpty(exceptionEventCheckData.currentScenarioName))
			{
				Debug.Log("売り上げあり／シナリオなし");
				GameObject.Find("Carriage Store Button").GetComponent<ArborFSM>().SendTrigger("LoadCarriageStoreUi");
				return;
			}
			Debug.Log("売り上げあり／シナリオあり");
			PlayerNonSaveDataManager.loadSceneName = "scenario";
			PlayerNonSaveDataManager.selectScenarioName = exceptionEventCheckData.currentScenarioName;
			GameObject.Find("Transition Manager").GetComponent<PlayMakerFSM>().SendEvent("StartFadeIn");
		}
		else
		{
			Debug.Log("売り上げなし");
			GameObject.Find("Carriage Store Button").GetComponent<ArborFSM>().SendTrigger("LoadCarriageStoreUi");
		}
	}
}
