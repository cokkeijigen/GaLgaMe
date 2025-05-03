using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class OpenNewRecipeNotice : StateBehaviour
{
	private ParameterContainer parameterContainer;

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
		parameterContainer.GetGameObjectList("noticeWindowGoList")[0].SetActive(value: false);
		parameterContainer.GetGameObjectList("noticeWindowGoList")[1].SetActive(value: true);
		parameterContainer.GetGameObject("noticeCanvasGo").SetActive(value: true);
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
