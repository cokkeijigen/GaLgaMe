using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ClickSexTouchAreaPoint : StateBehaviour
{
	public enum Type
	{
		touch,
		watch
	}

	private SexTouchManager sexTouchManager;

	private ParameterContainer parameterContainer;

	public Type type;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexTouchManager = GameObject.Find("Sex Touch Manager").GetComponent<SexTouchManager>();
		parameterContainer = GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		sexTouchManager.clickSelectAreaPointIndex = parameterContainer.GetInt("areaPointIndex");
		sexTouchManager.clickSelectAreaPointName = parameterContainer.GetString("areaPointName");
		switch (type)
		{
		case Type.touch:
			sexTouchManager.sexTouchArborFSM.SendTrigger("LeftClickAreaPoint");
			break;
		case Type.watch:
			sexTouchManager.sexTouchArborFSM.SendTrigger("RightClickAreaPoint");
			break;
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
