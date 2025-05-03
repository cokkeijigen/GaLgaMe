using Arbor;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class TotalMapAccessManager : MonoBehaviour
{
	public HeaderStatusManager headerStatusManager;

	public WorldMapAccessManager worldMapAccessManager;

	public LocalMapAccessManager localMapAccessManager;

	public DungeonItemInfoManager dungeonItemInfoManager;

	public ArborFSM totalMapFSM;

	public ArborFSM worldMapFSM;

	public ArborFSM localMapFSM;

	public ArborFSM headerStatusFSM;

	public GameObject[] mapGroupArray;

	public CanvasGroup[] mapCanvasGroupArray;

	public GameObject blackImageGo;

	public GameObject restShortCutButtonGo;

	public GameObject worldAreaParentGo;

	public GameObject localAreaParentGo;

	public GameObject[] localAreaGoArray;

	public GameObject localMapDefaultPositionGo;

	public GameObject destinationNodeGroupGo;

	public NeedWorldMapDateData needWorldMapDateData;

	public WorldMapUnlockDataBase worldMapUnlockDataBase;

	public LocalMapPlaceDataBase localMapPlaceDataBase;

	public GameObject worldPlayerGo;

	public GameObject localPlayerGo;

	public GameObject localHeroineGo;

	public Sprite[] playerSpriteArray;

	public Sprite[] carriageSpriteArray;

	public GameObject mapDialogGo;

	public Localize pointNameTextLoc;

	public Localize unFollowNameTextLoc;

	public Text needMoveDayText;

	public Text needMoveTimeText;

	public Text needEventDayText;

	public Text needEventTimeText;

	public GameObject[] dialogPlusTextGoArray;

	public GameObject[] dialogDayTextGroupArray;

	public GameObject[] dialogTimeTextGroupArray;

	public GameObject dialogUnFollowGroupGo;

	public GameObject dialogAlertGroupGo;

	public GameObject dialogTimeGroupGo;

	public GameObject needTimeGroupGo;

	public GameObject needEventTimeGroupGo;

	public GameObject dialogOkButtonMouseIconGo;

	public GameObject mapAlertDialogGo;

	public string clickDisableTerm;

	public Localize alertTextLoc;

	private void Awake()
	{
		mapDialogGo.SetActive(value: false);
		mapAlertDialogGo.SetActive(value: false);
	}

	public void PushLocalExitButton()
	{
		PlayerDataManager.mapPlaceStatusNum = 0;
		totalMapFSM.SendTrigger("RefreshOpenMap");
	}

	public void PushDialogOkButton()
	{
		if (PlayerDataManager.mapPlaceStatusNum == 0)
		{
			if (!PlayerDataManager.isSelectDungeon)
			{
				PlayerDataManager.dungeonEnterTimeZoneNum = 0;
				Debug.Log("ダンジョンEnterTime初期化");
			}
			else
			{
				int num = (PlayerDataManager.dungeonEnterTimeZoneNum = PlayerDataManager.currentTimeZone + PlayerNonSaveDataManager.needMoveDayCount * 4 + PlayerNonSaveDataManager.needMoveTimeCount);
				Debug.Log("ダンジョンに入った時刻：" + num + "／移動時間：" + PlayerNonSaveDataManager.needMoveTimeCount);
			}
			mapDialogGo.SetActive(value: false);
			dungeonItemInfoManager.dungeonInfoWindow.SetActive(value: false);
			worldMapFSM.SendTrigger("WorldMapDialogOK");
		}
		else
		{
			localMapAccessManager.newCapterDialogGo.SetActive(value: false);
			localMapFSM.SendTrigger("LocalMapDialogOK");
		}
	}

	public void AfterWorldMapHeroineUnFollow()
	{
		if (!PlayerDataManager.isSelectDungeon)
		{
			PlayerDataManager.dungeonEnterTimeZoneNum = 0;
			Debug.Log("ダンジョンEnterTime初期化");
		}
		mapDialogGo.SetActive(value: false);
		dungeonItemInfoManager.dungeonInfoWindow.SetActive(value: false);
		worldMapFSM.SendTrigger("UnFollowAfterWorldMap");
	}

	public void AfterReserveHeroineUnFollow()
	{
		PlayerNonSaveDataManager.isHeroineUnFollowReserveAtLocalMap = false;
		totalMapFSM.SendTrigger("AfterHeroineUnFollow");
	}

	public void ResetMapInPlace(int num)
	{
		PlayerDataManager.mapPlaceStatusNum = num;
		headerStatusFSM.SendTrigger("ResetPlacePanel");
	}

	public void CloseMapDialog()
	{
		mapDialogGo.SetActive(value: false);
		mapAlertDialogGo.SetActive(value: false);
		localMapAccessManager.newCapterDialogGo.SetActive(value: false);
		dungeonItemInfoManager.dungeonInfoWindow.SetActive(value: false);
		if (PlayerDataManager.mapPlaceStatusNum == 0)
		{
			totalMapFSM.SendTrigger("CloseWorldDialog");
			mapCanvasGroupArray[0].interactable = true;
			mapCanvasGroupArray[0].blocksRaycasts = true;
			mapCanvasGroupArray[1].interactable = true;
			mapCanvasGroupArray[1].blocksRaycasts = true;
			headerStatusManager.clockCanvasGroup.alpha = 1f;
			headerStatusManager.clockCanvasGroup.blocksRaycasts = true;
			headerStatusManager.menuCanvasGroup.alpha = 1f;
			headerStatusManager.menuCanvasGroup.blocksRaycasts = true;
			return;
		}
		mapCanvasGroupArray[0].interactable = true;
		mapCanvasGroupArray[0].blocksRaycasts = true;
		mapCanvasGroupArray[1].interactable = true;
		mapCanvasGroupArray[1].blocksRaycasts = true;
		headerStatusManager.clockCanvasGroup.alpha = 1f;
		headerStatusManager.clockCanvasGroup.blocksRaycasts = true;
		headerStatusManager.menuCanvasGroup.alpha = 1f;
		headerStatusManager.menuCanvasGroup.blocksRaycasts = true;
		if (!PlayerDataManager.isLocalMapActionLimit)
		{
			headerStatusManager.exitButton.interactable = true;
			headerStatusManager.exitButton.blocksRaycasts = true;
			headerStatusManager.exitButton.alpha = 1f;
		}
		localMapAccessManager.SetLocalMapExitEnable(isEnable: true);
		totalMapFSM.SendTrigger("CloseLocalDialog");
	}

	public void ResetMapInteractableBlock()
	{
		if (GameObject.Find("World Map Group") != null)
		{
			SetWorldMapInteractable();
		}
		SetMapMenuButtonInteractable();
		SetLocalMapExitInteractable();
	}

	private void SetWorldMapInteractable()
	{
		CanvasGroup component = GameObject.Find("World Canvas").GetComponent<CanvasGroup>();
		component.blocksRaycasts = true;
		component.interactable = true;
		PlayerDataManager.worldMapInputBlock = false;
	}

	private void SetMapMenuButtonInteractable()
	{
		CanvasGroup component = GameObject.Find("Menu Button Group").GetComponent<CanvasGroup>();
		component.blocksRaycasts = true;
		component.interactable = true;
		component.alpha = 1f;
	}

	private void SetLocalMapExitInteractable()
	{
		GameObject gameObject = GameObject.Find("LocalMap Access Manager");
		if (gameObject != null)
		{
			gameObject.GetComponent<LocalMapAccessManager>().SetLocalMapExitEnable(isEnable: true);
		}
	}

	public void SetLocalMapCanvasInteractable(bool value)
	{
		if (GameObject.Find("LocalMap Access Manager") != null)
		{
			if (value)
			{
				mapCanvasGroupArray[1].alpha = 1f;
				mapCanvasGroupArray[1].interactable = true;
			}
			else
			{
				mapCanvasGroupArray[1].interactable = false;
			}
		}
	}
}
