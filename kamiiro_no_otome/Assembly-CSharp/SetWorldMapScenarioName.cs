using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetWorldMapScenarioName : StateBehaviour
{
	private ParameterContainer parameterContainer;

	public string scenarioName;

	public int talkPhaseNum;

	public bool isBalloonActive;

	public bool isWorldMapToUtage;

	public bool isWorldMapToInDoor;

	public bool isWorldMapPointDisable;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		parameterContainer = base.transform.parent.GetComponent<ParameterContainer>();
		if (parameterContainer == null)
		{
			GetComponent<ParameterContainer>();
		}
	}

	public override void OnStateBegin()
	{
		parameterContainer.SetString("scenarioName", scenarioName);
		parameterContainer.SetInt("talkPhaseNum", talkPhaseNum);
		parameterContainer.SetBool("isWorldMapToUtage", isWorldMapToUtage);
		parameterContainer.SetBool("isWorldMapToInDoor", isWorldMapToInDoor);
		parameterContainer.SetBool("isWorldMapPointDisable", isWorldMapPointDisable);
		if (isBalloonActive)
		{
			Debug.Log("バルーン表示" + base.gameObject.name);
			parameterContainer.GetGameObject("alertBalloon").SetActive(value: true);
		}
		else
		{
			Debug.Log("バルーン非表示" + base.gameObject.name);
			parameterContainer.GetGameObject("alertBalloon").SetActive(value: false);
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
