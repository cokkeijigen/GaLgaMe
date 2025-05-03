using Arbor;
using UnityEngine;
using Utage;

[AddComponentMenu("")]
public class SetUtageParam : StateBehaviour
{
	public enum Type
	{
		boolParam,
		intParam,
		floatParam,
		stringParam
	}

	public AdvEngine advEngine;

	public Type type;

	public string paramName;

	public bool setBoolValue;

	public int setIntValue;

	public float setFloatValue;

	public string setStringValue;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		switch (type)
		{
		case Type.boolParam:
			advEngine.Param.SetParameterBoolean(paramName, setBoolValue);
			break;
		case Type.intParam:
			advEngine.Param.SetParameterInt(paramName, setIntValue);
			break;
		case Type.floatParam:
			advEngine.Param.SetParameterFloat(paramName, setFloatValue);
			break;
		case Type.stringParam:
			advEngine.Param.SetParameterString(paramName, setStringValue);
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
