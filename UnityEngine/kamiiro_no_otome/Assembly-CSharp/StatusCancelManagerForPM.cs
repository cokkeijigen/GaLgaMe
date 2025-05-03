using Arbor;
using UnityEngine;

public class StatusCancelManagerForPM : MonoBehaviour
{
	private StatusManager statusManager;

	private StatusCustomManager statusCustomManager;

	private void Awake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponentInParent<StatusManager>();
		statusCustomManager = GameObject.Find("Status Custom Manager").GetComponentInParent<StatusCustomManager>();
	}

	public void SetWorldMapInteractable()
	{
		CanvasGroup component = GameObject.Find("World Canvas").GetComponent<CanvasGroup>();
		component.blocksRaycasts = true;
		component.interactable = true;
		PlayerDataManager.worldMapInputBlock = false;
	}

	public void CloseStatusCanvas()
	{
		statusManager.statusCanvasArray[0].SetActive(value: false);
		statusManager.statusCanvasArray[1].SetActive(value: false);
		statusCustomManager.statusOverlayCanvas.SetActive(value: false);
		if (PlayerNonSaveDataManager.isEquipItemChange)
		{
			GameObject.Find("Header Status Manager").GetComponent<ArborFSM>().SendTrigger("HeaderStatusRefresh");
			PlayerNonSaveDataManager.isEquipItemChange = false;
		}
		SetMapMenuButtonInteractable();
		SetInDoorExitInteractable();
	}

	private void SetMapMenuButtonInteractable()
	{
		CanvasGroup component = GameObject.Find("Menu Button Group").GetComponent<CanvasGroup>();
		component.blocksRaycasts = true;
		component.interactable = true;
		component.alpha = 1f;
	}

	private void SetInDoorExitInteractable()
	{
		GameObject gameObject = GameObject.Find("LocalMap Access Manager");
		if (gameObject != null)
		{
			gameObject.GetComponent<LocalMapAccessManager>().SetLocalMapExitEnable(isEnable: true);
		}
	}
}
