using Arbor;
using UnityEngine;
using UnityEngine.UI;

public class MapRestManager : MonoBehaviour
{
	public TotalMapAccessManager totalMapAccessManager;

	public HeaderStatusManager headerStatusManager;

	public ArborFSM mapRestFSM;

	public GameObject mapRestDialogGo;

	public Image campBlackImage;

	public GameObject campOkButtonGo;

	public GameObject mapRestOkButtonGo;

	public void PushMapRestDialogOkButton()
	{
		mapRestDialogGo.SetActive(value: false);
		PlayerNonSaveDataManager.needCampTimeCount = 1;
		mapRestFSM.SendTrigger("StartMapRest");
	}

	public void CloseMapRestDialog()
	{
		mapRestDialogGo.SetActive(value: false);
		headerStatusManager.headerStatusCanvasGroup.alpha = 1f;
		headerStatusManager.headerStatusCanvasGroup.interactable = true;
		totalMapAccessManager.mapCanvasGroupArray[1].alpha = 1f;
		totalMapAccessManager.mapCanvasGroupArray[1].interactable = true;
		totalMapAccessManager.mapCanvasGroupArray[1].blocksRaycasts = true;
		totalMapAccessManager.totalMapFSM.SendTrigger("CloseWorldDialog");
	}

	public void AfterWorldMapRestHeroineUnFollow()
	{
		totalMapAccessManager.mapDialogGo.SetActive(value: false);
		mapRestFSM.SendTrigger("StartMapRest");
	}
}
