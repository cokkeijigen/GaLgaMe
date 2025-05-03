using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetStaticInt : StateBehaviour
{
	public enum Type
	{
		playerHaveMoney = 0,
		timeZone = 1,
		localMapHeroineFollowNum = 2,
		selectSlotPageNum = 3,
		mapPlaceStatusNum = 4,
		openUiScreenLevel = 6,
		playerLibido = 5,
		sendHelpMarkButtonIndex = 9
	}

	public Type type;

	public FlexibleInt setIntValue;

	public StateLink nextState;

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
		case Type.playerHaveMoney:
			PlayerDataManager.playerHaveMoney = setIntValue.value;
			break;
		case Type.timeZone:
			PlayerDataManager.currentTimeZone = setIntValue.value;
			break;
		case Type.selectSlotPageNum:
			PlayerNonSaveDataManager.selectSlotPageNum = setIntValue.value;
			break;
		case Type.mapPlaceStatusNum:
			PlayerDataManager.mapPlaceStatusNum = setIntValue.value;
			break;
		case Type.openUiScreenLevel:
			PlayerNonSaveDataManager.openUiScreenLevel = setIntValue.value;
			break;
		case Type.playerLibido:
			PlayerDataManager.playerLibido = setIntValue.value;
			break;
		case Type.sendHelpMarkButtonIndex:
			PlayerNonSaveDataManager.sendHelpMarkButtonIndex = setIntValue.value;
			break;
		}
		Transition(nextState);
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
