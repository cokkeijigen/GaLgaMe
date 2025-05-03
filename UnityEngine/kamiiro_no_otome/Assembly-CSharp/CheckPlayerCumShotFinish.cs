using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckPlayerCumShotFinish : StateBehaviour
{
	private SexTouchManager sexTouchManager;

	private SexTouchStatusManager sexTouchStatusManager;

	public StateLink falseLink;

	public StateLink finishLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexTouchManager = GameObject.Find("Sex Touch Manager").GetComponent<SexTouchManager>();
		sexTouchStatusManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchStatusManager>();
	}

	public override void OnStateBegin()
	{
		if (sexTouchStatusManager.playerLibidoPoint >= 80)
		{
			Transition(finishLink);
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
