using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckInDoorHeroineExist : StateBehaviour
{
	private HeroineCheckData heroineCheckData;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		if (PlayerDataManager.currentAccessPointName == "Kingdom1" || PlayerDataManager.currentAccessPointName == "City1")
		{
			string placeName = PlayerDataManager.currentPlaceName;
			if (GameDataManager.instance.heroineLocationDataBase.heroineLocationDataList.Any((HeroineLocationData data) => data.localPlaceName == placeName))
			{
				heroineCheckData = PlayerHeroineLocationDataManager.CheckLocalMapHeroineHere(placeName);
			}
			else
			{
				PlayerNonSaveDataManager.inDoorHeroineExist = false;
			}
		}
		else
		{
			string pointName = PlayerDataManager.currentAccessPointName;
			if (GameDataManager.instance.heroineLocationDataBase.heroineLocationDataList.Any((HeroineLocationData data) => data.worldPointName == pointName))
			{
				heroineCheckData = PlayerHeroineLocationDataManager.CheckWorldMapHeroineHere(pointName);
			}
			else
			{
				PlayerNonSaveDataManager.inDoorHeroineExist = false;
			}
		}
		if (heroineCheckData.isHeroineHere)
		{
			if (heroineCheckData.heroineID == PlayerDataManager.DungeonHeroineFollowNum && PlayerDataManager.isDungeonHeroineFollow)
			{
				PlayerNonSaveDataManager.inDoorHeroineExist = false;
				Debug.Log("ヒロインは同行中");
			}
			else
			{
				PlayerNonSaveDataManager.inDoorHeroineExist = true;
				Debug.Log("ヒロインがいる");
			}
		}
		else
		{
			PlayerNonSaveDataManager.inDoorHeroineExist = false;
			Debug.Log("ヒロインはいる");
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
