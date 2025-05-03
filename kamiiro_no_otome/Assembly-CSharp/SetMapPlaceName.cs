using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetMapPlaceName : StateBehaviour
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
		switch (PlayerDataManager.mapPlaceStatusNum)
		{
		case 0:
			if (PlayerNonSaveDataManager.isWorldMapToInDoor)
			{
				headerStatusManager.placeTextLoc.Term = "area" + PlayerDataManager.currentAccessPointName;
				Debug.Log("ワールドマップからインドアに行っている");
			}
			else
			{
				headerStatusManager.placeTextLoc.Term = "areaWorldMap";
			}
			break;
		case 1:
			headerStatusManager.placeTextLoc.Term = "area" + PlayerDataManager.currentAccessPointName;
			break;
		case 2:
			if (PlayerNonSaveDataManager.isWorldMapToInDoor)
			{
				headerStatusManager.placeTextLoc.Term = "area" + PlayerDataManager.currentAccessPointName;
				Debug.Log("ワールドマップからインドアに行っている");
			}
			else
			{
				headerStatusManager.placeTextLoc.Term = "place" + PlayerDataManager.currentPlaceName;
			}
			break;
		}
		Debug.Log("現在地を代入／マップNum：" + PlayerDataManager.mapPlaceStatusNum + "／アクセス：" + PlayerDataManager.currentAccessPointName + "／プレース：" + PlayerDataManager.currentPlaceName);
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
