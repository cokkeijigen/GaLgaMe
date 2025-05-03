using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class OpenInDoorRestDialog : StateBehaviour
{
	private InDoorTalkManager inDoorTalkManager;

	private InDoorCommandManager inDoorCommandManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
		inDoorCommandManager = GameObject.Find("InDoor Command Manager").GetComponent<InDoorCommandManager>();
	}

	public override void OnStateBegin()
	{
		ParameterContainer component = inDoorCommandManager.restDialogGroupGo.GetComponent<ParameterContainer>();
		IList<I2LocalizeComponent> variableList = component.GetVariableList<I2LocalizeComponent>("timeTextLocList");
		IList<GameObject> gameObjectList = component.GetGameObjectList("nextDayTextGoList");
		IList<int> intList = component.GetIntList("restTimeNumList");
		foreach (GameObject item in gameObjectList)
		{
			item.SetActive(value: false);
		}
		switch (PlayerDataManager.currentTimeZone)
		{
		case 0:
			variableList[0].localize.Term = "dialogRestToMorning";
			variableList[1].localize.Term = "dialogRestToAfternoon";
			variableList[2].localize.Term = "dialogRestToEvening";
			variableList[3].localize.Term = "dialogRestToNight";
			gameObjectList[0].SetActive(value: true);
			intList[0] = 4;
			intList[1] = 1;
			intList[2] = 2;
			intList[3] = 3;
			break;
		case 1:
			variableList[0].localize.Term = "dialogRestToMorning";
			variableList[1].localize.Term = "dialogRestToAfternoon";
			variableList[2].localize.Term = "dialogRestToEvening";
			variableList[3].localize.Term = "dialogRestToNight";
			gameObjectList[0].SetActive(value: true);
			gameObjectList[1].SetActive(value: true);
			intList[0] = 3;
			intList[1] = 4;
			intList[2] = 1;
			intList[3] = 2;
			break;
		case 2:
			variableList[0].localize.Term = "dialogRestToMorning";
			variableList[1].localize.Term = "dialogRestToAfternoon";
			variableList[2].localize.Term = "dialogRestToEvening";
			variableList[3].localize.Term = "dialogRestToNight";
			gameObjectList[0].SetActive(value: true);
			gameObjectList[1].SetActive(value: true);
			gameObjectList[2].SetActive(value: true);
			intList[0] = 2;
			intList[1] = 3;
			intList[2] = 4;
			intList[3] = 1;
			break;
		case 3:
			variableList[0].localize.Term = "dialogRestToMorning";
			variableList[1].localize.Term = "dialogRestToAfternoon";
			variableList[2].localize.Term = "dialogRestToEvening";
			variableList[3].localize.Term = "dialogRestToNight";
			gameObjectList[0].SetActive(value: true);
			gameObjectList[1].SetActive(value: true);
			gameObjectList[2].SetActive(value: true);
			gameObjectList[3].SetActive(value: true);
			intList[0] = 1;
			intList[1] = 2;
			intList[2] = 3;
			intList[3] = 4;
			break;
		}
		inDoorCommandManager.restDialogGroupGo.SetActive(value: true);
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
