using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetScenarioFlagName : StateBehaviour
{
	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
		Dictionary<string, bool> dictionary2 = new Dictionary<string, bool>();
		PlayerFlagDataManager.scenarioFlagDictionary.Clear();
		PlayerFlagDataManager.sceneGarellyFlagDictionary.Clear();
		List<ScenarioFlagData> scenarioFlagDataList = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList;
		for (int i = 0; i < scenarioFlagDataList.Count; i++)
		{
			dictionary.Add(scenarioFlagDataList[i].scenarioName, value: false);
		}
		PlayerFlagDataManager.scenarioFlagDictionary = dictionary;
		List<ScenarioFlagData> list = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Where((ScenarioFlagData data) => data.isGarellyScene).ToList();
		for (int j = 0; j < list.Count; j++)
		{
			dictionary2.Add(list[j].scenarioName, value: false);
		}
		PlayerFlagDataManager.sceneGarellyFlagDictionary = dictionary2;
		Debug.Log("シナリオ名の読み込み完了");
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
