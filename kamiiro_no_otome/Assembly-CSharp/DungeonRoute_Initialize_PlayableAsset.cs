using System;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class DungeonRoute_Initialize_PlayableAsset : PlayableAsset
{
	public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
	{
		DungeonRoute_Initialize_PlayableBehaviour template = new DungeonRoute_Initialize_PlayableBehaviour();
		return ScriptPlayable<DungeonRoute_Initialize_PlayableBehaviour>.Create(graph, template);
	}
}
