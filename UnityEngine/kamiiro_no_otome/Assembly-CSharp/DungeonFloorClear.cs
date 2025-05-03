using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class DungeonFloorClear : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private DungeonMapStatusManager dungeonMapStatusManager;

	private ArborFSM statusFSM;

	private float healHpPercent;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GetComponent<DungeonMapManager>();
		dungeonMapStatusManager = GetComponentInChildren<DungeonMapStatusManager>();
		statusFSM = dungeonMapStatusManager.transform.GetComponent<ArborFSM>();
	}

	public override void OnStateBegin()
	{
		DungeonMapData dungeonMapData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData item) => item.dungeonName == PlayerDataManager.currentDungeonName);
		List<HaveCampItemData> list = PlayerInventoryDataManager.haveItemCampItemList.Where((HaveCampItemData data) => data.itemType == "medicKit").ToList();
		if (list != null && list.Count > 0)
		{
			HaveCampItemData kitData = list.OrderBy((HaveCampItemData data) => data.itemSortID).LastOrDefault();
			int power = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData data) => data.itemID == kitData.itemID).power;
			healHpPercent = (float)power / 100f;
			int num = Mathf.FloorToInt((float)PlayerStatusDataManager.playerAllMaxHp * healHpPercent);
			PlayerStatusDataManager.playerAllHp = Mathf.Clamp(PlayerStatusDataManager.playerAllHp + num, 0, PlayerStatusDataManager.playerAllMaxHp);
			Debug.Log("回復量：" + num);
		}
		if (dungeonMapManager.dungeonCurrentFloorNum < dungeonMapManager.dungeonMaxFloorNum)
		{
			dungeonMapManager.dungeonCurrentFloorNum++;
			dungeonMapManager.dungeonCurrentFloorText.text = dungeonMapManager.dungeonCurrentFloorNum.ToString();
			FloorBorderCheck(dungeonMapData);
		}
		if (dungeonMapManager.dungeonCurrentFloorNum == dungeonMapManager.dungeonMaxFloorNum)
		{
			if (dungeonMapData.isBeforeFlagAbsenceBoss)
			{
				if (PlayerFlagDataManager.scenarioFlagDictionary[dungeonMapData.bossAppearFlag])
				{
					dungeonMapManager.bossButton.SetActive(value: true);
				}
				else
				{
					dungeonMapManager.bossButton.SetActive(value: false);
				}
			}
			else
			{
				dungeonMapManager.bossButton.SetActive(value: true);
			}
		}
		dungeonMapManager.thisFloorActionNum = 0;
		dungeonMapManager.isDungeonRouteAction = false;
		if (dungeonMapStatusManager.dungeonDeBuffAgiityRemainFloor > 0)
		{
			dungeonMapStatusManager.dungeonDeBuffAgiityRemainFloor--;
		}
		statusFSM.SendTrigger("RefreshDungeonBuff");
		dungeonMapManager.selectCardList.Clear();
		for (int i = 0; i < dungeonMapManager.selectCardBonusArray.Length; i++)
		{
			dungeonMapManager.selectCardBonusArray[i] = 0;
		}
		dungeonMapManager.ResetDungeonRouteFrame();
		dungeonMapManager.routeSelectGroupWindow.SetActive(value: true);
		dungeonMapManager.routeButtonGroup.SetActive(value: true);
		dungeonMapManager.miniCardGroup.SetActive(value: true);
		dungeonMapManager.routeAnimationGroupWindow.SetActive(value: false);
		Transition(stateLink);
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

	private void FloorBorderCheck(DungeonMapData dungeonMapData)
	{
		int num = dungeonMapData.borderFloorCount - 1;
		if (dungeonMapManager.currentBorderNum >= num)
		{
			return;
		}
		int index = dungeonMapManager.currentBorderNum + 1;
		Debug.Log("現在の階数：" + dungeonMapManager.dungeonCurrentFloorNum + "／境界の階数：" + dungeonMapData.dungeonBorderFloor[index]);
		if (dungeonMapManager.dungeonCurrentFloorNum >= dungeonMapData.dungeonBorderFloor[index])
		{
			dungeonMapManager.currentBorderNum++;
			if (PlayerDataManager.currentTimeZone < 2)
			{
				dungeonMapManager.dungeonBgImageArray[0].sprite = dungeonMapData.dungeonBgList[dungeonMapManager.currentBorderNum];
				dungeonMapManager.dungeonBgImageArray[1].sprite = dungeonMapData.dungeonAnimationBgList[dungeonMapManager.currentBorderNum];
			}
			else
			{
				dungeonMapManager.dungeonBgImageArray[0].sprite = dungeonMapData.dungeonNightBgList[dungeonMapManager.currentBorderNum];
				dungeonMapManager.dungeonBgImageArray[1].sprite = dungeonMapData.dungeonAnimationNightBgList[dungeonMapManager.currentBorderNum];
			}
		}
	}
}
