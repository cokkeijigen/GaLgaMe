using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class GetOpenStatusCanvas : StateBehaviour
{
	public StateLink openItem;

	public StateLink openSkill;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		string selectStatusCanvasName = PlayerNonSaveDataManager.selectStatusCanvasName;
		if (!(selectStatusCanvasName == "item"))
		{
			if (selectStatusCanvasName == "skill")
			{
				Transition(openSkill);
			}
		}
		else
		{
			Transition(openItem);
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
