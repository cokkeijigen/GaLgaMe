using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckHeroineFollowMember : StateBehaviour
{
	public int requiredHeroineNum;

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
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			if (PlayerDataManager.DungeonHeroineFollowNum == requiredHeroineNum)
			{
				Transition(trueLink);
			}
			else
			{
				Transition(falseLink);
			}
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
