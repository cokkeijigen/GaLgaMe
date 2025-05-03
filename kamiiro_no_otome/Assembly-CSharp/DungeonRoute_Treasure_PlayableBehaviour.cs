using PathologicalGames;
using UnityEngine;
using UnityEngine.Playables;

public class DungeonRoute_Treasure_PlayableBehaviour : PlayableBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private DungeonRouteAnimationManager dungeonRouteAnimationManager;

	public override void OnGraphStart(Playable playable)
	{
	}

	public override void OnGraphStop(Playable playable)
	{
	}

	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		dungeonRouteAnimationManager = GameObject.Find("Dungeon Card Manager").GetComponent<DungeonRouteAnimationManager>();
		string text = "";
		if (dungeonMapManager.selectCardList != null && dungeonMapManager.selectCardList.Count > 0)
		{
			text = dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].subTypeString;
		}
		if (!(text == "treasure"))
		{
			if (text == "bigTreasure")
			{
				Transform transform = null;
				transform = ((!dungeonMapManager.isMimicBattle) ? PoolManager.Pools["DungeonObject"].Spawn(dungeonRouteAnimationManager.treasurePrefabGoArray[0], dungeonRouteAnimationManager.uIParticle.transform) : PoolManager.Pools["DungeonObject"].Spawn(dungeonRouteAnimationManager.treasurePrefabGoArray[1], dungeonRouteAnimationManager.uIParticle.transform));
				dungeonRouteAnimationManager.treasureSpawnGo = transform;
				transform.localScale = new Vector3(1f, 1f, 1f);
				transform.localPosition = new Vector3(0f, 0f, 0f);
				Vector3 eulerAngles = new Vector3(90f, 0f, 0f);
				transform.eulerAngles = eulerAngles;
				Debug.Log("宝箱エフェクトをスポーン");
				dungeonRouteAnimationManager.uIParticle.RefreshParticles();
			}
		}
		else
		{
			Transform transform2 = PoolManager.Pools["DungeonObject"].Spawn(dungeonRouteAnimationManager.treasurePrefabGoArray[0], dungeonRouteAnimationManager.uIParticle.transform);
			dungeonRouteAnimationManager.treasureSpawnGo = transform2;
			transform2.localScale = new Vector3(1f, 1f, 1f);
			transform2.localPosition = new Vector3(0f, 0f, 0f);
			Vector3 eulerAngles2 = new Vector3(90f, 0f, 0f);
			transform2.eulerAngles = eulerAngles2;
			Debug.Log("宝箱エフェクトをスポーン");
			dungeonRouteAnimationManager.uIParticle.RefreshParticles();
		}
	}

	public override void OnBehaviourPause(Playable playable, FrameData info)
	{
	}

	public override void PrepareFrame(Playable playable, FrameData info)
	{
	}
}
