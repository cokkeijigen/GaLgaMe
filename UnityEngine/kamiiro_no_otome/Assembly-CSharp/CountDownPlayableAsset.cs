using System;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class CountDownPlayableAsset : PlayableAsset
{
	public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
	{
		ScriptPlayable<CountDownPlayableBehaviour> scriptPlayable = ScriptPlayable<CountDownPlayableBehaviour>.Create(graph);
		scriptPlayable.GetBehaviour();
		return scriptPlayable;
	}
}
