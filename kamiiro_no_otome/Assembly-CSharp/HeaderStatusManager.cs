using System.Collections.Generic;
using Arbor;
using Coffee.UIExtensions;
using DarkTonic.MasterAudio;
using I2.Loc;
using PathologicalGames;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeaderStatusManager : SerializedMonoBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	private WorldMapAccessManager worldMapAccessManager;

	private LocalMapAccessManager localMapAccessManager;

	private CarriageStoreNoticeManager carriageStoreNoticeManager;

	private InDoorTalkManager inDoorTalkManager;

	public ArborFSM headerFSM;

	public ArborFSM questFSM;

	public CanvasGroup headerStatusCanvasGroup;

	public CanvasGroup statusCanvasGroup;

	public CanvasGroup clockCanvasGroup;

	public CanvasGroup restShortcutCanvasGroup;

	public CanvasGroup menuCanvasGroup;

	public CanvasGroup carriageStoreButton;

	public CanvasGroup exitButton;

	public GameObject[] mapMenuButtonArray;

	public CanvasGroup mapHelpMarkCanvasGroup;

	public GameObject alertRestFrameGo;

	public GameObject alertExitFrameGo;

	public GameObject newQuestBalloonGo;

	public GameObject alertQuestBalloonGo;

	public GameObject partyGroupParent;

	public List<GameObject> partyGroupGoList = new List<GameObject>();

	public Sprite[] partyGroupSprite;

	public GameObject partyHeroineMenstrualGroupGo;

	public List<Sprite[]> heroineSpriteArrayList;

	public GameObject clockGroupGo;

	public Image[] weekIconImageArray;

	public bool isWeekIconInitialize;

	public TextMeshProUGUI scheduleButtonTmpText;

	public GameObject placePanelGo;

	public GameObject shopRankGroupGo;

	public GameObject libidoPanelGo;

	public GameObject moneyPanelGo;

	public GameObject rareDropPanelGo;

	public Localize placeTextLoc;

	public Text moneyText;

	public Text libidoText;

	public Text rareDropRemainingDaysText;

	public Localize shopRankTextLoc1;

	public Localize shopRankTextLoc2;

	public GameObject shopRankDialogCanvasGo;

	public Localize shopRankDialogTextLoc1;

	public Localize shopRankDialogTextLoc2;

	public UIParticle uIParticle;

	private Transform effectSpawnGo;

	public GameObject shopRankChangePrefabGo;

	public GameObject rareDropInfoFrameGo;

	public Localize rareDropInfoLoc;

	public GameObject carriageStoreNoticeIconGo;

	public Material headerUiMaterial;

	public Text[] headerUiTextArray;

	public TextMeshProUGUI[] headerUiTmpTextArray;

	public Image[] headerUiImageArray;

	private void Awake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		worldMapAccessManager = GameObject.Find("WorldMap Access Manager").GetComponent<WorldMapAccessManager>();
		localMapAccessManager = GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>();
		carriageStoreNoticeManager = GameObject.Find("Store Tending Manager").GetComponent<CarriageStoreNoticeManager>();
	}

	public void SetVisibleMapExitButton(bool value)
	{
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
		localMapAccessManager.localMapExitFSM.gameObject.SetActive(value);
		localMapAccessManager.localMapExitFSM.enabled = value;
		inDoorTalkManager.exitButtonCanvasGroup.gameObject.SetActive(value);
	}

	public void SetHeaderUiBrightness(float setValue)
	{
		clockCanvasGroup.interactable = false;
		Material material = new Material(placePanelGo.GetComponent<Image>().material);
		scheduleButtonTmpText.color = new Color(setValue, setValue, setValue);
		Text[] array = headerUiTextArray;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].color = new Color(setValue, setValue, setValue);
		}
		TextMeshProUGUI[] array2 = headerUiTmpTextArray;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].color = new Color(setValue, setValue, setValue);
		}
		Image[] array3 = weekIconImageArray;
		for (int i = 0; i < array3.Length; i++)
		{
			array3[i].material = material;
		}
		array3 = headerUiImageArray;
		for (int i = 0; i < array3.Length; i++)
		{
			array3[i].material = material;
		}
		material.SetFloat("_ColorHSV_Brightness_1", setValue);
		Debug.Log("ヘッダUIの輝度変更：" + setValue);
	}

	public void ResetHeaderUiBrightness()
	{
		clockCanvasGroup.interactable = true;
		scheduleButtonTmpText.color = Color.white;
		Text[] array = headerUiTextArray;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].color = Color.white;
		}
		TextMeshProUGUI[] array2 = headerUiTmpTextArray;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].color = Color.white;
		}
		Image[] array3 = weekIconImageArray;
		for (int i = 0; i < array3.Length; i++)
		{
			array3[i].material = headerUiMaterial;
		}
		array3 = headerUiImageArray;
		for (int i = 0; i < array3.Length; i++)
		{
			array3[i].material = headerUiMaterial;
		}
		Debug.Log("ヘッダUIのマテリアルを元に戻した");
	}

	public void CheckRestAlertOpen()
	{
		if (PlayerDataManager.isLocalMapActionLimit || PlayerDataManager.mapPlaceStatusNum == 2)
		{
			alertRestFrameGo.SetActive(value: true);
		}
	}

	public void CheckExitAlertOpen()
	{
		if (PlayerDataManager.isLocalMapActionLimit)
		{
			alertExitFrameGo.SetActive(value: true);
		}
	}

	public void OpenMapIconInfo(string type)
	{
		if (type == "rareDrop")
		{
			rareDropInfoLoc.Term = "infoBuff_rareDrop";
			rareDropInfoFrameGo.SetActive(value: true);
		}
	}

	public void CloseMapIconInfo()
	{
		rareDropInfoFrameGo.SetActive(value: false);
	}

	public void RefreshFollowHeroineMenstrualDay()
	{
		CharacterStatusData characterStatusData = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == PlayerDataManager.DungeonHeroineFollowNum);
		Debug.Log("ヘッダステータス／危険日確認開始");
		if (PlayerFlagDataManager.scenarioFlagDictionary[characterStatusData.characterMenstruationViewFlag])
		{
			if (characterStatusData.characterMenstrualDayList.Contains(PlayerDataManager.currentWeekDay))
			{
				if (PlayerDataManager.DungeonHeroineFollowNum == 3)
				{
					if (PlayerFlagDataManager.scenarioFlagDictionary[characterStatusData.characterMenstruationFlag])
					{
						partyHeroineMenstrualGroupGo.SetActive(value: true);
					}
					else
					{
						partyHeroineMenstrualGroupGo.SetActive(value: false);
					}
				}
				else
				{
					partyHeroineMenstrualGroupGo.SetActive(value: true);
				}
			}
			else
			{
				partyHeroineMenstrualGroupGo.SetActive(value: false);
			}
		}
		else
		{
			partyHeroineMenstrualGroupGo.SetActive(value: false);
		}
	}

	public void SpawnShopRankChangeEffect()
	{
		effectSpawnGo = null;
		effectSpawnGo = PoolManager.Pools["totalMapPool"].Spawn(shopRankChangePrefabGo, uIParticle.transform);
		MasterAudio.PlaySound("SeShopRankChange", 1f, null, 0f, null, null);
		effectSpawnGo.localScale = new Vector3(1f, 1f, 1f);
		effectSpawnGo.localPosition = new Vector3(0f, 0f, 0f);
		Debug.Log("称号変更エフェクトをスポーン");
		uIParticle.RefreshParticles();
	}

	public void PushShopRankDialogOkButton()
	{
		PoolManager.Pools["totalMapPool"].Despawn(effectSpawnGo, carriageStoreNoticeManager.poolParentGo.transform);
		uIParticle.RefreshParticles();
		shopRankDialogCanvasGo.SetActive(value: false);
		if (!PlayerNonSaveDataManager.isShopRankChangeFromNotice)
		{
			carriageStoreNoticeManager.CheckCarriageEvent();
			return;
		}
		PlayerNonSaveDataManager.isShopRankChangeFromNotice = false;
		if (PlayerDataManager.currentShopRankFirstNum == 4 && !PlayerFlagDataManager.extraNoticeFlagDictionary["shopRank_first4"])
		{
			PlayerFlagDataManager.extraNoticeFlagDictionary["shopRank_first4"] = true;
			PlayerNonSaveDataManager.isNewStoryQuestNotice = true;
			PlayerDataManager.isNoCheckNewQuest = true;
			GameObject.Find("Transition Manager").GetComponent<PlayMakerFSM>().SendEvent("OpenNotice");
		}
	}
}
