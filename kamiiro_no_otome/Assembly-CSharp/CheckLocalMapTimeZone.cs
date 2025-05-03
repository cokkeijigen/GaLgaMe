using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckLocalMapTimeZone : StateBehaviour
{
	public bool isMorning;

	public bool isAfternoon;

	public bool isEvening;

	public bool isNight;

	private bool isResult;

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
		isResult = false;
		switch (PlayerDataManager.currentTimeZone)
		{
		case 0:
			if (isMorning)
			{
				isResult = true;
			}
			break;
		case 1:
			if (isAfternoon)
			{
				isResult = true;
			}
			break;
		case 2:
			if (isEvening)
			{
				isResult = true;
			}
			break;
		case 3:
			if (isNight)
			{
				isResult = true;
			}
			break;
		}
		if (isResult)
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
