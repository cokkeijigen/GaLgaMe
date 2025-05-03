using Arbor;
using UnityEngine;
using UnityEngine.Playables;

public class CountDownPlayableBehaviour : PlayableBehaviour
{
	public override void OnGraphStart(Playable playable)
	{
	}

	public override void OnGraphStop(Playable playable)
	{
	}

	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		GameObject.Find("Craft Check Manager").GetComponent<ArborFSM>().SendTrigger("StartCraftCheck");
	}

	public override void OnBehaviourPause(Playable playable, FrameData info)
	{
	}

	public override void PrepareFrame(Playable playable, FrameData info)
	{
	}
}
