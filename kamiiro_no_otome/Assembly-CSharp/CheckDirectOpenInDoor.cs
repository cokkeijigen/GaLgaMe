using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckDirectOpenInDoor : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	public StateLink localMap;

	public StateLink inDoor;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
	}

	public override void OnStateBegin()
	{
		Debug.Log("マップ番号インドア分岐：" + PlayerDataManager.mapPlaceStatusNum);
		switch (PlayerDataManager.mapPlaceStatusNum)
		{
		case 1:
			Transition(localMap);
			break;
		case 2:
		{
			Transform obj = totalMapAccessManager.localAreaParentGo.transform.Find(PlayerDataManager.currentAccessPointName).transform.Find(PlayerDataManager.currentPlaceName);
			bool value = false;
			if (obj.GetComponent<ParameterContainer>().TryGetBool("isHeroineExist", out value))
			{
				PlayerNonSaveDataManager.inDoorHeroineExist = value;
			}
			else
			{
				PlayerNonSaveDataManager.inDoorHeroineExist = false;
			}
			Transition(inDoor);
			break;
		}
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
