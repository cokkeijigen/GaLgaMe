using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class OpenMapRestDialog : StateBehaviour
{
	private HeaderStatusManager headerStatusManager;

	private TotalMapAccessManager totalMapAccessManager;

	private MapRestManager mapRestManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		mapRestManager = GameObject.Find("Map Rest Manager").GetComponent<MapRestManager>();
	}

	public override void OnStateBegin()
	{
		mapRestManager.campOkButtonGo.SetActive(value: false);
		mapRestManager.mapRestOkButtonGo.SetActive(value: true);
		totalMapAccessManager.mapCanvasGroupArray[2].interactable = true;
		totalMapAccessManager.mapCanvasGroupArray[2].alpha = 1f;
		mapRestManager.mapRestDialogGo.SetActive(value: true);
		headerStatusManager.headerStatusCanvasGroup.interactable = false;
		totalMapAccessManager.mapCanvasGroupArray[1].interactable = false;
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
}
