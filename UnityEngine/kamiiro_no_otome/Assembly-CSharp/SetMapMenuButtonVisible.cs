using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetMapMenuButtonVisible : StateBehaviour
{
	private HeaderStatusManager headerStatusManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
	}

	public override void OnStateBegin()
	{
		if (PlayerFlagDataManager.scenarioFlagDictionary["MH_Lucy_001"])
		{
			headerStatusManager.mapMenuButtonArray[0].SetActive(value: false);
			headerStatusManager.restShortcutCanvasGroup.gameObject.SetActive(value: true);
		}
		else
		{
			headerStatusManager.mapMenuButtonArray[0].SetActive(value: false);
			headerStatusManager.restShortcutCanvasGroup.gameObject.SetActive(value: false);
		}
		if (PlayerFlagDataManager.scenarioFlagDictionary["M_Main_002"])
		{
			headerStatusManager.mapMenuButtonArray[1].SetActive(value: true);
		}
		else
		{
			headerStatusManager.mapMenuButtonArray[1].SetActive(value: false);
		}
		if (PlayerFlagDataManager.scenarioFlagDictionary["M_Main_001-4"])
		{
			headerStatusManager.mapMenuButtonArray[2].SetActive(value: true);
		}
		else
		{
			headerStatusManager.mapMenuButtonArray[2].SetActive(value: false);
		}
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
