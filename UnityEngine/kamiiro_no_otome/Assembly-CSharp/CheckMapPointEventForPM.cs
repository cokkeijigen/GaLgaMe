using Arbor;
using Sirenix.OdinInspector;
using UnityEngine;

public class CheckMapPointEventForPM : SerializedMonoBehaviour
{
	public ParameterContainer parameterContainer;

	public bool GetIsHeroineFollow()
	{
		return PlayerDataManager.isDungeonHeroineFollow;
	}

	public int GetFollowHeroineID()
	{
		return PlayerDataManager.DungeonHeroineFollowNum;
	}

	public bool CheckScnearioFlagClear(string scenarioName)
	{
		return PlayerFlagDataManager.scenarioFlagDictionary[scenarioName];
	}

	public bool CheckArrayScnearioFlagClear(GameObject gameObject, string arrayName)
	{
		bool result = false;
		string[] stringValues = gameObject.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmArray(arrayName).stringValues;
		foreach (string key in stringValues)
		{
			if (PlayerFlagDataManager.scenarioFlagDictionary[key])
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public int GetScnearioFlagClearAfterDayCount(string scenarioName)
	{
		int num = PlayerFlagDataManager.eventStartingDayDictionary[scenarioName];
		return PlayerDataManager.currentTotalDay - num;
	}

	public void SetParamScnearioName(string scenarioName, bool isBalloonActive)
	{
		parameterContainer.SetString("scenarioName", scenarioName);
		if (!string.IsNullOrEmpty(scenarioName))
		{
			parameterContainer.GetGameObject("alertBalloon").SetActive(isBalloonActive);
		}
		else
		{
			parameterContainer.GetGameObject("alertBalloon").SetActive(value: false);
		}
	}

	public void SetParamWoldMapVariable(bool isMapToUtage, bool isMapToInDoor, bool isMapPointDisable)
	{
		parameterContainer.SetBool("isWorldMapToUtage", isMapToUtage);
		parameterContainer.SetBool("isWorldMapToInDoor", isMapToInDoor);
		parameterContainer.SetBool("isWorldMapPointDisable", isMapPointDisable);
	}

	public void SetMapPointDisableTerm(string str)
	{
		parameterContainer.SetString("disablePointTerm", str);
	}

	public void SetInitializeBool(bool value)
	{
		parameterContainer.SetBool("isInitialize", value);
	}
}
