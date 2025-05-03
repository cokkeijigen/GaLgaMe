using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckWorldMapAccessPoint : StateBehaviour
{
	private WorldMapAccessManager worldMapAccessManager;

	public GameObject areaParentGO;

	private GameObject[] areaPointArray;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		worldMapAccessManager = GameObject.Find("WorldMap Access Manager").GetComponent<WorldMapAccessManager>();
		areaPointArray = GameObject.FindGameObjectsWithTag("AreaPoint");
	}

	public override void OnStateBegin()
	{
		GameObject[] array = areaPointArray;
		foreach (GameObject obj in array)
		{
			obj.GetComponent<CanvasGroup>().alpha = 0f;
			obj.GetComponent<CanvasGroup>().interactable = false;
			obj.GetComponent<CanvasGroup>().blocksRaycasts = false;
		}
		for (int j = 0; j < worldMapAccessManager.worldMapUnlockDataBase.worldMapUnlockDataList.Count; j++)
		{
			string needFlagName = worldMapAccessManager.worldMapUnlockDataBase.worldMapUnlockDataList[j].needFlagName;
			if (PlayerFlagDataManager.scenarioFlagDictionary[needFlagName])
			{
				CanvasGroup component = areaParentGO.transform.Find(worldMapAccessManager.worldMapUnlockDataBase.worldMapUnlockDataList[j].currentPointName).gameObject.GetComponent<CanvasGroup>();
				component.alpha = 1f;
				component.interactable = true;
				component.blocksRaycasts = true;
			}
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
