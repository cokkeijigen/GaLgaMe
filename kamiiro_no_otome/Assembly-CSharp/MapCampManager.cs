using Arbor;
using UnityEngine;
using UnityEngine.UI;

public class MapCampManager : MonoBehaviour
{
	public TotalMapAccessManager totalMapAccessManager;

	public HeaderStatusManager headerStatusManager;

	public ArborFSM mapCampFSM;

	public CanvasGroup campButtonCanvasGroup;

	public GameObject mapCampDialogGo;

	public Text needCampTimeText;

	public Image campBlackImage;

	public GameObject campOkButtonGo;

	public GameObject mapRestOkButtonGo;

	public void PushCampDialogOkButton()
	{
		mapCampDialogGo.SetActive(value: false);
		mapCampFSM.SendTrigger("StartCampRest");
	}

	public void CloseMapCampDialog()
	{
		mapCampDialogGo.SetActive(value: false);
		headerStatusManager.menuCanvasGroup.interactable = true;
		totalMapAccessManager.totalMapFSM.SendTrigger("CloseWorldDialog");
	}

	public void AfterWorldMapCampHeroineUnFollow()
	{
		totalMapAccessManager.mapDialogGo.SetActive(value: false);
		mapCampFSM.SendTrigger("StartCampRest");
	}
}
