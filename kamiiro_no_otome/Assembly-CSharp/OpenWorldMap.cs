using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class OpenWorldMap : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	private WorldMapAccessManager worldMapAccessManager;

	[SerializeField]
	private int visibleAccessPointCount;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		worldMapAccessManager = GameObject.Find("WorldMap Access Manager").GetComponent<WorldMapAccessManager>();
	}

	public override void OnStateBegin()
	{
		visibleAccessPointCount = 13;
		totalMapAccessManager.mapGroupArray[0].SetActive(value: true);
		if (!PlayerNonSaveDataManager.isWorldMapToInDoor)
		{
			totalMapAccessManager.localPlayerGo.SetActive(value: false);
			totalMapAccessManager.localHeroineGo.SetActive(value: false);
			totalMapAccessManager.mapCanvasGroupArray[0].alpha = 0f;
		}
		totalMapAccessManager.ResetMapInPlace(0);
		worldMapAccessManager.isWorldMapAccessPointInitialized.Clear();
		GameObject[] array = new GameObject[totalMapAccessManager.worldAreaParentGo.transform.childCount];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = totalMapAccessManager.worldAreaParentGo.transform.GetChild(i).gameObject;
		}
		CheckWorldMapAccessPoint(array);
		GameObject[] array2 = array;
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j].transform.GetComponentInChildren<ArborFSM>().SendTrigger("ResetWorldMapPoint");
		}
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		if (worldMapAccessManager.isWorldMapAccessPointInitialized.All((bool value) => value) && worldMapAccessManager.isWorldMapAccessPointInitialized.Count == visibleAccessPointCount)
		{
			Transition(stateLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}

	private void CheckWorldMapAccessPoint(GameObject[] pointGoArray)
	{
		foreach (GameObject obj in pointGoArray)
		{
			obj.GetComponent<CanvasGroup>().alpha = 0f;
			obj.GetComponent<CanvasGroup>().interactable = false;
			obj.GetComponent<CanvasGroup>().blocksRaycasts = false;
		}
		for (int j = 0; j < pointGoArray.Length; j++)
		{
			string goName = pointGoArray[j].name;
			string needFlagName = totalMapAccessManager.worldMapUnlockDataBase.worldMapUnlockDataList.Find((WorldMapUnlockData data) => data.currentPointName == goName).needFlagName;
			if (PlayerDataManager.isNeedEffectNewWorldMapPoint && PlayerDataManager.needEffectNewWorldMapPointName == goName)
			{
				Debug.Log("登場演出があるので、いったん表示はしない／ポイント名：" + goName);
			}
			else if (PlayerFlagDataManager.scenarioFlagDictionary[needFlagName])
			{
				CanvasGroup component = totalMapAccessManager.worldAreaParentGo.transform.Find(goName).GetComponent<CanvasGroup>();
				component.alpha = 1f;
				component.interactable = true;
				component.blocksRaycasts = true;
				Debug.Log(goName + "／チェックしたポイント名：" + goName);
			}
		}
	}
}
