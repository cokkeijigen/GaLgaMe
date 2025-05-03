using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class DungeonRoute_Initialize_PlayableBehaviour : PlayableBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private DungeonRouteAnimationManager dungeonRouteAnimationManager;

	public override void OnGraphStart(Playable playable)
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		dungeonRouteAnimationManager = GameObject.Find("Dungeon Card Manager").GetComponent<DungeonRouteAnimationManager>();
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			dungeonRouteAnimationManager.heroineAnimationImageGo.SetActive(value: true);
			int num = PlayerDataManager.DungeonHeroineFollowNum - 1;
			if (dungeonMapManager.isBossRouteSelect)
			{
				return;
			}
			string text = "";
			if (dungeonMapManager.selectCardList != null && dungeonMapManager.selectCardList.Count > 0)
			{
				text = dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].subTypeString;
			}
			if (text == "heroineTalk")
			{
				string characterDungeonSexUnLockFlag = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[PlayerDataManager.DungeonHeroineFollowNum].characterDungeonSexUnLockFlag;
				bool flag = PlayerFlagDataManager.scenarioFlagDictionary[characterDungeonSexUnLockFlag];
				if (PlayerDataManager.playerLibido >= 40 && flag)
				{
					dungeonRouteAnimationManager.edenAnimationImageGo.GetComponent<Image>().sprite = dungeonRouteAnimationManager.edenTalkSpriteArray[1];
					dungeonRouteAnimationManager.heroineAnimationImageGo.GetComponent<Image>().sprite = dungeonRouteAnimationManager.heroineTalk2SpriteArray[num];
					Debug.Log("ダンジョンルート：ヒロインTalk2");
				}
				else
				{
					dungeonRouteAnimationManager.edenAnimationImageGo.GetComponent<Image>().sprite = dungeonRouteAnimationManager.edenTalkSpriteArray[0];
					dungeonRouteAnimationManager.heroineAnimationImageGo.GetComponent<Image>().sprite = dungeonRouteAnimationManager.heroineTalk1SpriteArray[num];
					Debug.Log("ダンジョンルート：ヒロインTalk1");
				}
			}
			else
			{
				dungeonRouteAnimationManager.heroineAnimationImageGo.GetComponent<Image>().sprite = dungeonRouteAnimationManager.heroineWalkSpriteArray[num];
				Debug.Log("ダンジョンルート：ヒロインWalk");
			}
		}
		else
		{
			dungeonRouteAnimationManager.heroineAnimationImageGo.SetActive(value: false);
			Debug.Log("ダンジョンルート：ヒロイン非表示");
		}
	}

	public override void OnGraphStop(Playable playable)
	{
	}

	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
	}

	public override void OnBehaviourPause(Playable playable, FrameData info)
	{
	}

	public override void PrepareFrame(Playable playable, FrameData info)
	{
	}
}
