using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class OpenMapDialog : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	private WorldMapAccessManager worldMapAccessManager;

	private DungeonItemInfoManager dungeonItemInfoManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		worldMapAccessManager = GameObject.Find("WorldMap Access Manager").GetComponent<WorldMapAccessManager>();
		dungeonItemInfoManager = GameObject.Find("Dungeon Item Info Manager").GetComponent<DungeonItemInfoManager>();
	}

	public override void OnStateBegin()
	{
		totalMapAccessManager.dialogUnFollowGroupGo.SetActive(value: false);
		totalMapAccessManager.dialogAlertGroupGo.SetActive(value: false);
		totalMapAccessManager.mapCanvasGroupArray[2].interactable = true;
		totalMapAccessManager.mapCanvasGroupArray[2].alpha = 1f;
		if (PlayerDataManager.mapPlaceStatusNum == 0)
		{
			worldMapAccessManager.OpenWorldMapDialog(CallbackMethod);
			return;
		}
		totalMapAccessManager.pointNameTextLoc.Term = "place" + PlayerNonSaveDataManager.selectPlaceName;
		totalMapAccessManager.dialogTimeGroupGo.SetActive(value: false);
		totalMapAccessManager.dialogOkButtonMouseIconGo.SetActive(value: true);
		totalMapAccessManager.mapDialogGo.SetActive(value: true);
		Transition(stateLink);
	}

	public override void OnStateEnd()
	{
		PlayerNonSaveDataManager.isWorldMapPointDialogAlert = false;
	}

	public override void OnStateUpdate()
	{
	}

	public override void OnStateLateUpdate()
	{
	}

	private void CallbackMethod()
	{
		totalMapAccessManager.pointNameTextLoc.Term = "area" + PlayerNonSaveDataManager.selectAccessPointName;
		int num = worldMapAccessManager.GetNeedAccessDay(PlayerDataManager.currentAccessPointName, PlayerNonSaveDataManager.selectAccessPointName);
		int num2 = worldMapAccessManager.GetNeedAccessTime(PlayerDataManager.currentAccessPointName, PlayerNonSaveDataManager.selectAccessPointName);
		if (num2 >= 4)
		{
			int num3 = Mathf.FloorToInt(num2 / 4);
			num += num3;
			num2 -= num3 * 4;
		}
		PlayerNonSaveDataManager.needMoveDayCount = num;
		totalMapAccessManager.needMoveDayText.text = num.ToString();
		PlayerNonSaveDataManager.needMoveTimeCount = num2;
		totalMapAccessManager.needMoveTimeText.text = num2.ToString();
		int num4 = PlayerDataManager.currentTimeZone + PlayerNonSaveDataManager.needMoveDayCount * 4 + PlayerNonSaveDataManager.needMoveTimeCount + PlayerDataManager.dungeonEnterTimeZoneNum;
		Debug.Log("移動後の時間：" + num4 + "／ヒロインずっと同行フラグ：" + PlayerFlagDataManager.heroineAllTimeFollowFlagList[PlayerDataManager.DungeonHeroineFollowNum]);
		if (PlayerDataManager.isSelectDungeon)
		{
			num4++;
			if (!PlayerFlagDataManager.heroineAllTimeFollowFlagList[PlayerDataManager.DungeonHeroineFollowNum])
			{
				num4 += PlayerNonSaveDataManager.needMoveDayCount * 4 + PlayerNonSaveDataManager.needMoveTimeCount;
			}
			Debug.Log("移動後の時間：" + num4 + "／ヒロインずっと同行フラグ：" + PlayerFlagDataManager.heroineAllTimeFollowFlagList[PlayerDataManager.DungeonHeroineFollowNum]);
		}
		if (num4 >= 4 && PlayerDataManager.isDungeonHeroineFollow && !PlayerFlagDataManager.heroineAllTimeFollowFlagList[PlayerDataManager.DungeonHeroineFollowNum])
		{
			totalMapAccessManager.dialogUnFollowGroupGo.SetActive(value: true);
			totalMapAccessManager.unFollowNameTextLoc.Term = "character" + PlayerDataManager.DungeonHeroineFollowNum;
		}
		if (PlayerNonSaveDataManager.isWorldMapPointDialogAlert)
		{
			totalMapAccessManager.dialogAlertGroupGo.SetActive(value: true);
		}
		if (PlayerDataManager.isSelectDungeon)
		{
			dungeonItemInfoManager.OpenDungeonItemInfoWindow();
			totalMapAccessManager.dialogOkButtonMouseIconGo.SetActive(value: false);
		}
		else
		{
			totalMapAccessManager.dialogOkButtonMouseIconGo.SetActive(value: true);
		}
		totalMapAccessManager.dialogTimeGroupGo.SetActive(value: true);
		totalMapAccessManager.needTimeGroupGo.SetActive(value: true);
		if (PlayerDataManager.isSelectDungeon)
		{
			totalMapAccessManager.needEventTimeText.text = "1";
			totalMapAccessManager.needEventTimeGroupGo.SetActive(value: true);
		}
		else
		{
			totalMapAccessManager.needEventTimeGroupGo.SetActive(value: false);
		}
		totalMapAccessManager.mapDialogGo.SetActive(value: true);
		Transition(stateLink);
	}
}
