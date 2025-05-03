using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckClickSamePlace : StateBehaviour
{
	private bool resultBool;

	public StateLink trueLink;

	public StateLink falseLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		switch (PlayerDataManager.mapPlaceStatusNum)
		{
		case 0:
			if (PlayerNonSaveDataManager.selectAccessPointName == PlayerDataManager.currentAccessPointName && !PlayerDataManager.isSelectDungeon)
			{
				resultBool = true;
			}
			else
			{
				resultBool = false;
			}
			break;
		case 1:
			if (PlayerNonSaveDataManager.selectPlaceName == PlayerDataManager.currentPlaceName)
			{
				resultBool = true;
			}
			else
			{
				resultBool = false;
			}
			break;
		}
		if (resultBool)
		{
			Transition(trueLink);
		}
		else
		{
			Transition(falseLink);
		}
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
