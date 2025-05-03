using System;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class DungeonRoute_Treasure_PlayableAsset : PlayableAsset
{
	public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
	{
		DungeonRoute_Treasure_PlayableBehaviour template = new DungeonRoute_Treasure_PlayableBehaviour();
		return ScriptPlayable<DungeonRoute_Treasure_PlayableBehaviour>.Create(graph, template);
	}
}
