using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class StatusCancelManager : StateBehaviour
{
	private StatusManager statusManager;

	private StatusCustomManager statusCustomManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponentInParent<StatusManager>();
		statusCustomManager = GameObject.Find("Status Custom Manager").GetComponentInParent<StatusCustomManager>();
	}

	public override void OnStateBegin()
	{
		if (!statusManager.statusCanvasArray[0].activeInHierarchy)
		{
			return;
		}
		if (GameObject.Find("World Map Group") != null)
		{
			SetWorldMapInteractable();
		}
		GameObject gameObject = GameObject.Find("Status Dialog Canvas");
		if (gameObject != null)
		{
			if (GameObject.Find("Learned Button Group") != null)
			{
				MasterAudio.PlaySound("SeApplyButton", 1f, null, 0f, null, null);
			}
			else
			{
				MasterAudio.PlaySound("SeWindowClose", 1f, null, 0f, null, null);
			}
			gameObject.SetActive(value: false);
			Debug.Log("ダイアログを閉じる");
		}
		else if (statusCustomManager.customWindowArray[0].activeInHierarchy || statusCustomManager.customWindowArray[1].activeInHierarchy)
		{
			statusCustomManager.statusOverlayCanvas.SetActive(value: false);
			MasterAudio.PlaySound("SeWindowClose", 1f, null, 0f, null, null);
			Debug.Log("ステータスカスタムを閉じる");
		}
		else
		{
			statusManager.statusCanvasArray[0].SetActive(value: false);
			statusManager.statusCanvasArray[1].SetActive(value: false);
			statusCustomManager.statusOverlayCanvas.SetActive(value: false);
			MasterAudio.PlaySound("SeWindowClose", 1f, null, 0f, null, null);
			Debug.Log("ステータスを閉じる");
		}
		SetMapMenuButtonInteractable();
		SetInDoorExitInteractable();
		Transition(stateLink);
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

	public void SetInDoorExitInteractable()
	{
		GameObject gameObject = GameObject.Find("LocalMap Access Manager");
		if (gameObject != null)
		{
			gameObject.GetComponent<LocalMapAccessManager>().SetLocalMapExitEnable(isEnable: true);
		}
	}
}
