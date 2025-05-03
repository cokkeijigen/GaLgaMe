using Arbor;
using UnityEngine;
using UnityEngine.Playables;

[AddComponentMenu("")]
public class SetTimelinePlayHead : StateBehaviour
{
	public PlayableDirector playableDirector;

	public InputSlotFloat setPlayHead;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		float value = 0f;
		setPlayHead.GetValue(ref value);
		playableDirector.time = value;
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
