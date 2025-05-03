using UnityEngine;

public class DungeonEventManagerForPM : MonoBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private void Awake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponentInParent<DungeonMapManager>();
	}

	public bool GetIsHeroineFollow()
	{
		return PlayerDataManager.isDungeonHeroineFollow;
	}

	public int GetFollowHeroineID()
	{
		return PlayerDataManager.DungeonHeroineFollowNum;
	}

	public string GetCurrentDungeonName()
	{
		return PlayerDataManager.currentDungeonName;
	}

	public bool CheckScnearioFlagClear(string scenarioName)
	{
		return PlayerFlagDataManager.scenarioFlagDictionary[scenarioName];
	}

	public int GetCurrentDungeonFloorNum()
	{
		return dungeonMapManager.dungeonCurrentFloorNum;
	}
}
