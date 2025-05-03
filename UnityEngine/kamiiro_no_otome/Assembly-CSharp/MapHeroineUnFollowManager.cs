using I2.Loc;
using UnityEngine;

public class MapHeroineUnFollowManager : MonoBehaviour
{
	private HeaderStatusManager headerStatusManager;

	private TotalMapAccessManager totalMapAccessManager;

	public GameObject mapUnFollowButtonGo;

	public CanvasGroup mapUnFollowButtonCanvasGroup;

	public GameObject alertUnFollowFrameGo;

	public GameObject mapUnFollowDialogWindowGo;

	public Localize unFollowNameTextLoc;

	private void Awake()
	{
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		mapUnFollowButtonGo.SetActive(value: false);
		alertUnFollowFrameGo.SetActive(value: false);
		mapUnFollowDialogWindowGo.SetActive(value: false);
	}

	public void RefreshUnFollowButtonVisible()
	{
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			if (PlayerDataManager.isHeroineSpecifyFollow && PlayerDataManager.heroineSpecifyFollowPoint == PlayerDataManager.currentAccessPointName)
			{
				mapUnFollowButtonCanvasGroup.interactable = false;
				mapUnFollowButtonCanvasGroup.alpha = 0.5f;
			}
			else
			{
				mapUnFollowButtonCanvasGroup.interactable = true;
				mapUnFollowButtonCanvasGroup.alpha = 1f;
			}
			mapUnFollowButtonGo.SetActive(value: true);
			Debug.Log("同行解除ボタン／表示");
		}
		else
		{
			mapUnFollowButtonGo.SetActive(value: false);
			Debug.Log("同行解除ボタン／非表示");
		}
	}

	public void OpenMapUnFollowDialogWindow()
	{
		headerStatusManager.headerStatusCanvasGroup.interactable = false;
		totalMapAccessManager.mapCanvasGroupArray[0].interactable = false;
		totalMapAccessManager.mapCanvasGroupArray[1].interactable = false;
		unFollowNameTextLoc.Term = "character" + PlayerDataManager.DungeonHeroineFollowNum;
		mapUnFollowDialogWindowGo.SetActive(value: true);
	}

	public void PushUnFollowOkButton()
	{
		mapUnFollowDialogWindowGo.SetActive(value: false);
		PlayerNonSaveDataManager.isRefreshLocalMap = true;
		PlayerDataManager.isDungeonHeroineFollow = false;
		PlayerStatusDataManager.characterHp[PlayerDataManager.DungeonHeroineFollowNum] = PlayerStatusDataManager.characterMaxHp[PlayerDataManager.DungeonHeroineFollowNum];
		RefreshUnFollowButtonVisible();
		totalMapAccessManager.totalMapFSM.SendTrigger("RefreshOpenMap");
	}

	public void CloseMapUnFollowDialogWindow()
	{
		mapUnFollowDialogWindowGo.SetActive(value: false);
		headerStatusManager.headerStatusCanvasGroup.interactable = true;
		totalMapAccessManager.mapCanvasGroupArray[0].interactable = true;
		totalMapAccessManager.mapCanvasGroupArray[1].interactable = true;
		totalMapAccessManager.totalMapFSM.SendTrigger("CloseWorldDialog");
	}

	public void CheckHeroineSpecifyAlertOpen()
	{
		if (PlayerDataManager.isHeroineSpecifyFollow && PlayerDataManager.currentAccessPointName == PlayerDataManager.heroineSpecifyFollowPoint)
		{
			alertUnFollowFrameGo.SetActive(value: true);
		}
	}
}
