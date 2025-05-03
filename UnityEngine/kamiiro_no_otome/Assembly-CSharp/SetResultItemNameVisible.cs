using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetResultItemNameVisible : StateBehaviour
{
	private ParameterContainer parameterContainer;

	public bool isActive;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		parameterContainer = GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		if (isActive)
		{
			string itemNameTerm = PlayerInventoryDataAccess.GetItemNameTerm(parameterContainer.GetInt("itemID"));
			parameterContainer.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = itemNameTerm;
			parameterContainer.GetGameObject("nameFrameGo").SetActive(value: true);
		}
		else
		{
			parameterContainer.GetGameObject("nameFrameGo").SetActive(value: false);
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
