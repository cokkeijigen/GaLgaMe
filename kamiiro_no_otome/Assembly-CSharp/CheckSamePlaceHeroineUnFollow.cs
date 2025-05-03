using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckSamePlaceHeroineUnFollow : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	private WorldMapAccessManager worldMapAccessManager;

	public StateLink unFollowLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		worldMapAccessManager = GameObject.Find("WorldMap Access Manager").GetComponent<WorldMapAccessManager>();
	}

	public override void OnStateBegin()
	{
		int num = worldMapAccessManager.GetNeedAccessDay(PlayerDataManager.currentAccessPointName, PlayerNonSaveDataManager.selectAccessPointName);
		int num2 = worldMapAccessManager.GetNeedAccessTime(PlayerDataManager.currentAccessPointName, PlayerNonSaveDataManager.selectAccessPointName);
		if (num2 >= 4)
		{
			int num3 = Mathf.FloorToInt(num2 / 4);
			num += num3;
			num2 -= num3 * 4;
		}
		int num4 = PlayerDataManager.currentTimeZone + num * 4 + num2 + PlayerDataManager.dungeonEnterTimeZoneNum;
		Debug.Log("移動後の時間：" + num4 + "／ヒロインずっと同行フラグ：" + PlayerFlagDataManager.heroineAllTimeFollowFlagList[PlayerDataManager.DungeonHeroineFollowNum]);
		if (PlayerDataManager.isSelectDungeon)
		{
			num4++;
			if (!PlayerFlagDataManager.heroineAllTimeFollowFlagList[PlayerDataManager.DungeonHeroineFollowNum])
			{
				num4 += PlayerNonSaveDataManager.needMoveDayCount * 4 + PlayerNonSaveDataManager.needMoveTimeCount;
			}
		}
		if (num4 >= 4 && PlayerDataManager.isDungeonHeroineFollow)
		{
			if (!PlayerFlagDataManager.heroineAllTimeFollowFlagList[PlayerDataManager.DungeonHeroineFollowNum])
			{
				Debug.Log("同じ場所をクリック／ヒロイン同行解除になる");
				Transition(unFollowLink);
			}
			else
			{
				Transition(stateLink);
			}
		}
		else
		{
			Transition(stateLink);
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
