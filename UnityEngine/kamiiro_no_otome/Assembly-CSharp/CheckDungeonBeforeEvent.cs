using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckDungeonBeforeEvent : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	public StateLink worldMapLink;

	public StateLink restoreLink;

	public StateLink dungeonLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
	}

	public override void OnStateBegin()
	{
		PlayerNonSaveDataManager.isDungeonScnearioBattle = false;
		PlayerNonSaveDataManager.isUtagePlayBattleBgm = false;
		if (PlayerNonSaveDataManager.battleBeforePointType == "WorldMap" || PlayerNonSaveDataManager.battleBeforePointType == "LocalMap")
		{
			if (!PlayerNonSaveDataManager.isRestoreDungeonBattleFailed)
			{
				for (int i = 0; i < PlayerStatusDataManager.playerPartyMember.Length; i++)
				{
					int num = PlayerStatusDataManager.playerPartyMember[i];
					PlayerStatusDataManager.characterHp[num] = 1;
					Debug.Log("ダンジョン敗戦／味方HPを1にする／ID：" + num);
				}
				PlayerStatusDataManager.playerAllHp = PlayerStatusDataManager.playerPartyMember.Length;
				Transition(worldMapLink);
			}
			else
			{
				Transition(restoreLink);
			}
		}
		else
		{
			Transition(dungeonLink);
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
