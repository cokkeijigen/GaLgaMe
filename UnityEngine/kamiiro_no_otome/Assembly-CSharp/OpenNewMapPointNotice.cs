using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class OpenNewMapPointNotice : StateBehaviour
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
		parameterContainer.GetVariable<I2LocalizeComponent>("mapPointTextLoc").localize.Term = PlayerDataManager.newMapPointName[0];
		parameterContainer.GetVariable<I2LocalizeComponent>("pointTextLoc").localize.Term = PlayerDataManager.newMapPointName[1];
		parameterContainer.GetGameObjectList("noticeWindowGoList")[0].SetActive(value: true);
		parameterContainer.GetGameObjectList("noticeWindowGoList")[1].SetActive(value: false);
		parameterContainer.GetGameObject("noticeCanvasGo").SetActive(value: true);
		MasterAudio.PlaySound("SeNotice", 1f, null, 0f, null, null);
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
