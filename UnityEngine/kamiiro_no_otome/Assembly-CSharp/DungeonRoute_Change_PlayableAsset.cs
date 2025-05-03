using System;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class DungeonRoute_Change_PlayableAsset : PlayableAsset
{
	public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
	{
		DungeonRoute_Change_PlayableBehaviour template = new DungeonRoute_Change_PlayableBehaviour();
		return ScriptPlayable<DungeonRoute_Change_PlayableBehaviour>.Create(graph, template);
	}
}
