using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class OpenNoticeBranch : StateBehaviour
{
	public StateLink mapNoticeLink;

	public StateLink recipeNoticeLink;

	public StateLink noNoticeLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		if (PlayerDataManager.isNewRecipeNotice)
		{
			Transition(recipeNoticeLink);
		}
		else if (PlayerDataManager.isNewMapNotice)
		{
			Transition(mapNoticeLink);
		}
		else
		{
			Transition(noNoticeLink);
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
