using Arbor;
using I2.Loc;
using UnityEngine;

[AddComponentMenu("")]
public class GetStaticString : StateBehaviour
{
	public enum Type
	{
		currentPlaceName
	}

	public Type type;

	public Localize localize;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		if (type == Type.currentPlaceName)
		{
			switch (PlayerDataManager.mapPlaceStatusNum)
			{
			case 0:
				localize.Term = "areaWorldMap";
				break;
			case 1:
				localize.Term = PlayerDataManager.currentAccessPointName;
				break;
			case 2:
				localize.Term = PlayerDataManager.currentPlaceName;
				break;
			}
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
