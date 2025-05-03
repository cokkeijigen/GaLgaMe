using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class DungeonRoute_Change_PlayableBehaviour : PlayableBehaviour
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
		if (!PlayerDataManager.isDungeonHeroineFollow)
		{
			return;
		}
		int num = PlayerDataManager.DungeonHeroineFollowNum - 1;
		if (dungeonMapManager.isBossRouteSelect)
		{
			dungeonRouteAnimationManager.heroineAnimationImageGo.GetComponent<Image>().sprite = dungeonRouteAnimationManager.heroineBattleSpriteArray[num];
			return;
		}
		string text = "";
		if (dungeonMapManager.selectCardList != null && dungeonMapManager.selectCardList.Count > 0)
		{
			text = dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].subTypeString;
		}
		if (text == "battle" || text == "hardBattle")
		{
			dungeonRouteAnimationManager.heroineAnimationImageGo.GetComponent<Image>().sprite = dungeonRouteAnimationManager.heroineBattleSpriteArray[num];
			Debug.Log("ダンジョンルート：ヒロイン戦闘");
		}
		else
		{
			dungeonRouteAnimationManager.heroineAnimationImageGo.GetComponent<Image>().sprite = dungeonRouteAnimationManager.heroineWaitSpriteArray[num];
			Debug.Log("ダンジョンルート：ヒロイン待機");
		}
	}

	public override void OnBehaviourPause(Playable playable, FrameData info)
	{
	}

	public override void PrepareFrame(Playable playable, FrameData info)
	{
	}
}
