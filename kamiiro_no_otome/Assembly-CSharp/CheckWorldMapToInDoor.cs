using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckWorldMapToInDoor : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	public StateLink localMap;

	public StateLink localToInDoor;

	public StateLink worldToInDoor;

	public StateLink utageToWorldInDoor;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
	}

	public override void OnStateBegin()
	{
		if (PlayerNonSaveDataManager.isUtageToWorldMapInDoor)
		{
			PlayerNonSaveDataManager.inDoorHeroineExist = totalMapAccessManager.worldAreaParentGo.transform.Find(PlayerDataManager.currentPlaceName).GetComponent<ParameterContainer>().GetBool("isHeroineExist");
			Transition(utageToWorldInDoor);
		}
		else if (PlayerNonSaveDataManager.isWorldMapToInDoor)
		{
			PlayerNonSaveDataManager.inDoorHeroineExist = totalMapAccessManager.worldAreaParentGo.transform.Find(PlayerDataManager.currentPlaceName).GetComponent<ParameterContainer>().GetBool("isHeroineExist");
			Transition(worldToInDoor);
		}
		else if (PlayerDataManager.mapPlaceStatusNum == 1)
		{
			Transition(localMap);
		}
		else
		{
			Transition(localToInDoor);
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
