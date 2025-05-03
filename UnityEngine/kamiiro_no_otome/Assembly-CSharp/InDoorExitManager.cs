using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class InDoorExitManager : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	private LocalMapAccessManager localMapAccessManager;

	private UtageMapSceneManager utageMapSceneManager;

	private HeaderStatusManager headerStatusManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		localMapAccessManager = GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>();
		utageMapSceneManager = GameObject.Find("LocalMap Access Manager").GetComponent<UtageMapSceneManager>();
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
	}

	public override void OnStateBegin()
	{
		if (GameObject.Find("Dialog Canvas") == null)
		{
			switch (PlayerDataManager.mapPlaceStatusNum)
			{
			case 0:
				PlayerNonSaveDataManager.isWorldMapToInDoor = false;
				headerStatusManager.headerFSM.SendTrigger("ResetPlacePanel");
				break;
			case 1:
				totalMapAccessManager.mapGroupArray[1].SetActive(value: true);
				totalMapAccessManager.localPlayerGo.GetComponent<ArborFSM>().SendTrigger("ResetLocalPlayerPosition");
				break;
			}
			utageMapSceneManager.advEngine.ResumeScenario();
			Debug.Log("宴をリジュームする");
			Transition(stateLink);
		}
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
