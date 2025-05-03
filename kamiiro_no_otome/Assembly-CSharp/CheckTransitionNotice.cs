using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckTransitionNotice : StateBehaviour
{
	public StateLink noticeLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		bool flag = false;
		if (PlayerDataManager.isNewMapNotice)
		{
			flag = true;
		}
		if (PlayerDataManager.isNewRecipeNotice)
		{
			flag = true;
		}
		if (flag)
		{
			Transition(noticeLink);
		}
		else
		{
			Transition(stateLink);
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
