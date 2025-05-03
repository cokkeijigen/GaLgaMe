using System.Collections.Generic;
using Arbor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[AddComponentMenu("")]
public class DungeonRouteAction : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private DungeonRouteAnimationManager dungeonRouteAnimationManager;

	private DungeonMapStatusManager dungeonMapStatusManager;

	private DungeonGetItemManager dungeonGetItemManager;

	private TimelineAsset timelineAsset;

	public OutputSlotFloat closureTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GetComponent<DungeonMapManager>();
		dungeonRouteAnimationManager = GameObject.Find("Dungeon Card Manager").GetComponent<DungeonRouteAnimationManager>();
		dungeonMapStatusManager = GetComponentInChildren<DungeonMapStatusManager>();
		dungeonGetItemManager = GameObject.Find("Dungeon Get Item Manager").GetComponent<DungeonGetItemManager>();
	}

	public override void OnStateBegin()
	{
		DungeonMapData dungeonMapData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerDataManager.currentDungeonName);
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		string text = "";
		dungeonMapManager.routeSelectGroupWindow.SetActive(value: false);
		dungeonMapManager.routeButtonGroup.SetActive(value: false);
		dungeonMapManager.miniCardGroup.SetActive(value: false);
		if (PlayerDataManager.currentTimeZone < 2)
		{
			dungeonRouteAnimationManager.dungeonRouteAnimationBgImage.sprite = dungeonMapData.dungeonAnimationBgList[dungeonMapManager.currentBorderNum];
			Debug.Log("ダンジョン背景：朝昼");
		}
		else
		{
			dungeonRouteAnimationManager.dungeonRouteAnimationBgImage.sprite = dungeonMapData.dungeonAnimationNightBgList[dungeonMapManager.currentBorderNum];
			Debug.Log("ダンジョン背景：夕夜");
		}
		dungeonMapManager.routeAnimationGroupWindow.SetActive(value: true);
		dungeonRouteAnimationManager.dungeonRouteAudioSource.volume = PlayerOptionsDataManager.optionsSeVolume;
		for (int i = 0; i < dungeonMapManager.routeResultGroupArray.Length; i++)
		{
			dungeonMapManager.routeResultGroupArray[i].SetActive(value: false);
		}
		for (int j = 0; j < dungeonMapManager.routeResultGroupLocArray.Length; j++)
		{
			dungeonMapManager.routeResultGroupLocArray[j].gameObject.SetActive(value: false);
		}
		for (int k = 0; k < dungeonMapManager.routeResultNumText.Length; k++)
		{
			dungeonMapManager.routeResultNumText[k].gameObject.SetActive(value: false);
		}
		dungeonMapManager.routeResultNumText[0].color = new Color(1f, 0.7f, 1f);
		dungeonMapManager.routeResultNumText[1].color = new Color(1f, 0.7f, 1f);
		dungeonMapManager.isDungeonRouteAction = true;
		dungeonMapManager.cardInfoFrame.SetActive(value: false);
		dungeonMapStatusManager.playerStatusRefreshType = "";
		dungeonMapManager.isMimicBattle = false;
		if (!dungeonMapManager.isBossRouteSelect)
		{
			dungeonMapManager.routeAnimationGroupArray[0].SetActive(value: true);
			dungeonMapManager.routeAnimationGroupArray[1].SetActive(value: false);
			for (int l = 0; l < dungeonMapManager.routeSelectFrameMiniArray.Length; l++)
			{
				string subTypeString = dungeonMapManager.selectCardList[l].subTypeString;
				dungeonMapManager.routeSelectFrameMiniArray[l].GetComponent<ParameterContainer>().GetVariable<UguiImage>("iconImageGo").image.sprite = GameDataManager.instance.itemCategoryDataBase.dungeonCardIconDictionary[subTypeString];
			}
			switch (dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].subTypeString)
			{
			case "battle":
				dungeonMapManager.routeResultGroupLocArray[0].Term = "dungeonRoute_preBattle";
				dungeonMapManager.routeResultGroupLocArray[6].Term = "dungeonRoute_battle";
				timelineAsset = dungeonRouteAnimationManager.dungeonRouteTimelineDictionary["battle"];
				dungeonRouteAnimationManager.dungeonRouteAnimationEnemyImage.sprite = dungeonRouteAnimationManager.enemyBattleSpriteArray[0];
				closureTime.SetValue(80f);
				ResultTextSetActive("battle");
				break;
			case "hardBattle":
				dungeonMapManager.routeResultGroupLocArray[0].Term = "dungeonRoute_preBattle";
				dungeonMapManager.routeResultGroupLocArray[6].Term = "dungeonRoute_hardBattle";
				timelineAsset = dungeonRouteAnimationManager.dungeonRouteTimelineDictionary["battle"];
				dungeonRouteAnimationManager.dungeonRouteAnimationEnemyImage.sprite = dungeonRouteAnimationManager.enemyBattleSpriteArray[1];
				closureTime.SetValue(80f);
				ResultTextSetActive("battle");
				break;
			case "move":
				dungeonMapManager.routeResultGroupLocArray[0].Term = "dungeonRoute_move";
				if (Random.Range(0, 100) < 20)
				{
					num = Random.Range(0, dungeonMapData.getCollectItemID[dungeonMapManager.currentBorderNum].Length);
					num2 = dungeonMapData.getCollectItemID[dungeonMapManager.currentBorderNum][num];
					dungeonMapManager.SetDropItemDictionary(num2, bonusChance: false);
					text = GetItemCategoryToString(num2);
					dungeonMapManager.routeResultGroupLocArray[3].Term = text + num2;
					dungeonMapManager.routeResultGroupLocArray[6].Term = "dungeonRoute_GetWord";
					ResultTextSetActive("collect");
				}
				else
				{
					ResultTextSetActive("battle");
				}
				timelineAsset = dungeonRouteAnimationManager.dungeonRouteTimelineDictionary["walk"];
				closureTime.SetValue(80f);
				break;
			case "vigilant":
				dungeonMapStatusManager.playerStatusRefreshType = "libido";
				dungeonMapStatusManager.beforePlayerStatusValue = PlayerDataManager.playerLibido;
				num4 = dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].powerNum;
				PlayerDataManager.playerLibido = Mathf.Clamp(PlayerDataManager.playerLibido - num4, 0, 100);
				dungeonMapManager.routeResultGroupLocArray[0].Term = "dungeonRoute_vigilant";
				dungeonMapManager.routeResultGroupLocArray[2].Term = "dungeonRoute_Libido";
				dungeonMapManager.routeResultNumText[0].text = dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].powerNum.ToString();
				dungeonMapManager.routeResultGroupLocArray[6].Term = "dungeonRoute_StatusDown";
				timelineAsset = dungeonRouteAnimationManager.dungeonRouteTimelineDictionary["walk"];
				closureTime.SetValue(80f);
				CheckVigilantDecrementDebuff();
				ResultTextSetActive("heroineImage");
				break;
			case "collect":
				if (Random.Range(0, 100) < 60)
				{
					num2 = dungeonGetItemManager.GetCommonItem("collect");
					Debug.Log("共通獲得収集アイテムID：" + num2);
				}
				else
				{
					num = Random.Range(0, dungeonMapData.getCollectItemID[dungeonMapManager.currentBorderNum].Length);
					num2 = dungeonMapData.getCollectItemID[dungeonMapManager.currentBorderNum][num];
					if (200 > num2 || num2 >= 300)
					{
						num = Random.Range(0, dungeonMapData.getCollectItemID[dungeonMapManager.currentBorderNum].Length);
						num2 = dungeonMapData.getCollectItemID[dungeonMapManager.currentBorderNum][num];
					}
					Debug.Log("獲得アイテムのインデックス：" + num + "／獲得アイテムID：" + num2);
				}
				dungeonMapManager.SetDropItemDictionary(num2, bonusChance: true);
				text = GetItemCategoryToString(num2);
				dungeonMapManager.routeResultGroupLocArray[0].Term = "dungeonRoute_collect";
				dungeonMapManager.routeResultGroupLocArray[3].Term = text + num2;
				dungeonMapManager.routeResultGroupLocArray[6].Term = "dungeonRoute_GetWord";
				if ((100 <= num2 && num2 < 600) || (840 <= num2 && num2 < 900))
				{
					dungeonMapManager.routeResultGroupLocArray[8].gameObject.SetActive(value: true);
					dungeonMapManager.routeResultNumText[0].text = dungeonMapManager.getDropItemNum.ToString();
					dungeonMapManager.routeResultNumText[0].gameObject.SetActive(value: true);
				}
				if (dungeonMapManager.isGetDropBonus)
				{
					dungeonMapManager.routeResultGroupLocArray[4].gameObject.SetActive(dungeonMapManager.isGetDropBonus);
					dungeonMapManager.routeResultGroupLocArray[5].gameObject.SetActive(dungeonMapManager.isGetDropBonus);
					dungeonMapManager.routeResultNumText[2].gameObject.SetActive(dungeonMapManager.isGetDropBonus);
					dungeonMapManager.routeResultNumText[2].text = dungeonMapManager.getDropBonusNum.ToString();
				}
				timelineAsset = dungeonRouteAnimationManager.dungeonRouteTimelineDictionary["collect"];
				dungeonRouteAnimationManager.dungeonRouteAnimationRouteImage.sprite = dungeonRouteAnimationManager.dungeonRouteBgDictionary["collect"];
				closureTime.SetValue(80f);
				ResultTextSetActive("collect");
				break;
			case "corpse":
				if (Random.Range(0, 100) > 30)
				{
					num2 = dungeonGetItemManager.GetCommonItem("corpse");
					Debug.Log("共通獲得亡骸アイテムID：" + num2);
				}
				else
				{
					int num6 = Random.Range(0, 100);
					if (num6 < 20)
					{
						if (Random.Range(0, 100) > 50)
						{
							num2 = dungeonGetItemManager.GetCommonItem("rareItem");
							Debug.Log("共通獲得亡骸レアアイテムID：" + num2);
						}
						else
						{
							num = Random.Range(0, dungeonMapData.getRareItemID[dungeonMapManager.currentBorderNum].Length);
							num2 = dungeonMapData.getRareItemID[dungeonMapManager.currentBorderNum][num];
						}
					}
					else if (num6 < 40)
					{
						num = Random.Range(0, dungeonMapData.getCashableItemID[dungeonMapManager.currentBorderNum].Length);
						num2 = dungeonMapData.getCashableItemID[dungeonMapManager.currentBorderNum][num];
					}
					else if (num6 < 90)
					{
						int[] enemyList = dungeonMapData.battleEnemyID[dungeonMapManager.currentBorderNum];
						int maxExclusive = dungeonMapData.maxEnemyCount[dungeonMapManager.currentBorderNum];
						int randomIndex = Random.Range(0, maxExclusive);
						Dictionary<int, float> dropItemDictionary = GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData data) => data.enemyID == enemyList[randomIndex]).dropItemDictionary;
						Debug.Log("ダンジョン亡骸／倒れていた敵ID：" + enemyList[randomIndex]);
						num2 = dungeonMapManager.GetDungeonBattleDropItem(dropItemDictionary);
					}
					else
					{
						num = Random.Range(0, dungeonMapData.getTreasureItemID[dungeonMapManager.currentBorderNum].Length);
						num2 = dungeonMapData.getTreasureItemID[dungeonMapManager.currentBorderNum][num];
					}
					Debug.Log("獲得アイテムのインデックス：" + num + "／獲得アイテムID：" + num2 + "／ランダム結果：" + num6);
				}
				dungeonMapManager.SetDropItemDictionary(num2, bonusChance: true);
				text = GetItemCategoryToString(num2);
				dungeonMapManager.routeResultGroupLocArray[0].Term = "dungeonRoute_corpse";
				dungeonMapManager.routeResultGroupLocArray[3].Term = text + num2;
				dungeonMapManager.routeResultGroupLocArray[6].Term = "dungeonRoute_GetWord";
				if ((100 <= num2 && num2 < 600) || (840 <= num2 && num2 < 900))
				{
					dungeonMapManager.routeResultGroupLocArray[8].gameObject.SetActive(value: true);
					dungeonMapManager.routeResultNumText[0].text = dungeonMapManager.getDropItemNum.ToString();
					dungeonMapManager.routeResultNumText[0].gameObject.SetActive(value: true);
				}
				if (dungeonMapManager.isGetDropBonus)
				{
					dungeonMapManager.routeResultGroupLocArray[4].gameObject.SetActive(dungeonMapManager.isGetDropBonus);
					dungeonMapManager.routeResultGroupLocArray[5].gameObject.SetActive(dungeonMapManager.isGetDropBonus);
					dungeonMapManager.routeResultNumText[2].gameObject.SetActive(dungeonMapManager.isGetDropBonus);
					dungeonMapManager.routeResultNumText[2].text = dungeonMapManager.getDropBonusNum.ToString();
				}
				timelineAsset = dungeonRouteAnimationManager.dungeonRouteTimelineDictionary["wait"];
				dungeonRouteAnimationManager.dungeonRouteAnimationRouteImage.sprite = dungeonRouteAnimationManager.dungeonRouteBgDictionary["corpse"];
				closureTime.SetValue(80f);
				ResultTextSetActive("collect");
				break;
			case "treasure":
			{
				timelineAsset = dungeonRouteAnimationManager.dungeonRouteTimelineDictionary["treasure"];
				closureTime.SetValue(80f);
				int num8 = Random.Range(0, 100);
				if (PlayerDataManager.rareDropRateRaisePowerNum > 0 && PlayerDataManager.rareDropRateRaiseRaimingDaysNum > 0)
				{
					num8 = Mathf.Clamp(num8 - PlayerDataManager.rareDropRateRaisePowerNum, 0, 100);
					Debug.Log("通常宝箱：" + num8);
				}
				if (num8 < 40)
				{
					if (Random.Range(0, 100) > 50)
					{
						num2 = dungeonGetItemManager.GetCommonItem("rareItem");
						Debug.Log("共通獲得宝箱レアアイテムID：" + num2);
					}
					else
					{
						num = Random.Range(0, dungeonMapData.getRareItemID[dungeonMapManager.currentBorderNum].Length);
						num2 = dungeonMapData.getRareItemID[dungeonMapManager.currentBorderNum][num];
					}
					dungeonMapManager.SetDropItemDictionary(num2, bonusChance: true);
					text = GetItemCategoryToString(num2);
					dungeonMapManager.routeResultGroupLocArray[0].Term = "dungeonRoute_treasure";
					dungeonMapManager.routeResultGroupLocArray[3].Term = text + num2;
					dungeonMapManager.routeResultNumText[0].text = dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].powerNum.ToString();
					dungeonMapManager.routeResultGroupLocArray[6].Term = "dungeonRoute_GetWord";
					ResultTextSetActive("collect");
				}
				else if (num8 < 80)
				{
					if (Random.Range(0, 100) > 30)
					{
						num2 = dungeonGetItemManager.GetCommonItem("treasure");
						Debug.Log("共通獲得宝箱アイテムID：" + num2);
					}
					else
					{
						num = Random.Range(0, dungeonMapData.getTreasureItemID[dungeonMapManager.currentBorderNum].Length);
						num2 = dungeonMapData.getTreasureItemID[dungeonMapManager.currentBorderNum][num];
					}
					dungeonMapManager.SetDropItemDictionary(num2, bonusChance: true);
					text = GetItemCategoryToString(num2);
					dungeonMapManager.routeResultGroupLocArray[0].Term = "dungeonRoute_treasure";
					dungeonMapManager.routeResultGroupLocArray[3].Term = text + num2;
					dungeonMapManager.routeResultNumText[0].text = dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].powerNum.ToString();
					dungeonMapManager.routeResultGroupLocArray[6].Term = "dungeonRoute_GetWord";
					ResultTextSetActive("collect");
				}
				else
				{
					int moneyAdd = GetMoneyAdd(dungeonMapData);
					dungeonMapManager.routeResultGroupLocArray[0].Term = "dungeonRoute_treasure";
					dungeonMapManager.routeResultGroupLocArray[2].Term = "moneyCurrency";
					dungeonMapManager.routeResultNumText[1].text = moneyAdd.ToString();
					dungeonMapManager.routeResultGroupLocArray[6].Term = "dungeonRoute_GetWord";
					ResultTextSetActive("buff");
				}
				break;
			}
			case "bigTreasure":
			{
				closureTime.SetValue(80f);
				int num9 = Random.Range(0, 100);
				if (PlayerDataManager.rareDropRateRaisePowerNum > 0 && PlayerDataManager.rareDropRateRaiseRaimingDaysNum > 0)
				{
					num9 = Mathf.Clamp(num9 + PlayerDataManager.rareDropRateRaisePowerNum, 0, 100);
					Debug.Log("豪華な宝箱：" + num9);
				}
				if (num9 < 20)
				{
					dungeonMapManager.isMimicBattle = true;
					timelineAsset = dungeonRouteAnimationManager.dungeonRouteTimelineDictionary["treasureBig"];
					dungeonMapManager.routeResultGroupLocArray[0].Term = "dungeonRoute_bigTreasure";
					dungeonMapManager.routeResultGroupLocArray[6].Term = "dungeonRoute_mimic";
					ResultTextSetActive("battle");
					break;
				}
				dungeonMapManager.isMimicBattle = false;
				timelineAsset = dungeonRouteAnimationManager.dungeonRouteTimelineDictionary["treasureBig"];
				if (Random.Range(0, 100) > 50)
				{
					num2 = dungeonGetItemManager.GetCommonItem("rareItem");
					Debug.Log("共通獲得宝箱レアアイテムID：" + num2);
				}
				else
				{
					num = Random.Range(0, dungeonMapData.getTreasureItemID[dungeonMapManager.currentBorderNum].Length);
					num2 = dungeonMapData.getRareItemID[dungeonMapManager.currentBorderNum][num];
				}
				dungeonMapManager.SetDropItemDictionary(num2, bonusChance: true);
				text = GetItemCategoryToString(num2);
				dungeonMapManager.routeResultGroupLocArray[0].Term = "dungeonRoute_bigTreasure";
				dungeonMapManager.routeResultGroupLocArray[3].Term = text + num2;
				dungeonMapManager.routeResultGroupLocArray[6].Term = "dungeonRoute_GetWord";
				ResultTextSetActive("collect");
				break;
			}
			case "heroineImage":
				dungeonMapStatusManager.playerStatusRefreshType = "libido";
				dungeonMapStatusManager.beforePlayerStatusValue = PlayerDataManager.playerLibido;
				num3 = CheckAddLibidoBonus();
				num4 = dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].powerNum + num3;
				PlayerDataManager.playerLibido = Mathf.Clamp(PlayerDataManager.playerLibido + num4, 0, 100);
				dungeonMapManager.routeResultNumText[0].color = new Color(1f, 0.5f, 0.8f);
				dungeonMapManager.routeResultGroupLocArray[0].Term = "dungeonRoute_heroineImage";
				dungeonMapManager.routeResultGroupLocArray[2].Term = "dungeonRoute_Libido";
				dungeonMapManager.routeResultNumText[0].text = dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].powerNum.ToString();
				dungeonMapManager.routeResultGroupLocArray[6].Term = "dungeonRoute_StatusUp";
				timelineAsset = dungeonRouteAnimationManager.dungeonRouteTimelineDictionary["walk"];
				closureTime.SetValue(80f);
				CheckHeroineDebuffClear();
				ResultTextSetActive("heroineImage");
				break;
			case "heroineTalk":
			{
				dungeonMapStatusManager.playerStatusRefreshType = "libido";
				dungeonMapStatusManager.beforePlayerStatusValue = PlayerDataManager.playerLibido;
				num3 = CheckAddLibidoBonus();
				num4 = dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].powerNum + num3;
				PlayerDataManager.playerLibido = Mathf.Clamp(PlayerDataManager.playerLibido + num4, 0, 100);
				Debug.Log("性欲を加算：" + dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].powerNum + "／ボーナス：" + num3);
				dungeonMapManager.routeResultNumText[0].color = new Color(1f, 0.5f, 0.8f);
				int dungeonHeroineFollowNum = PlayerDataManager.DungeonHeroineFollowNum;
				dungeonMapManager.routeResultGroupLocArray[0].Term = "character" + dungeonHeroineFollowNum;
				dungeonMapManager.routeResultGroupLocArray[2].Term = "dungeonRoute_Libido";
				dungeonMapManager.routeResultNumText[0].text = dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].powerNum.ToString();
				dungeonMapManager.routeResultGroupLocArray[6].Term = "dungeonRoute_StatusUp";
				if (PlayerFlagDataManager.CheckScenarioFlagIsClear(GameDataManager.instance.characterStatusDataBase.characterStatusDataList[dungeonHeroineFollowNum].characterDungeonSexUnLockFlag))
				{
					if (PlayerDataManager.playerLibido < 25)
					{
						dungeonMapManager.routeResultGroupLocArray[1].Term = "dungeonRoute_heroineTalk";
					}
					else if (PlayerDataManager.playerLibido < 50)
					{
						dungeonMapManager.routeResultGroupLocArray[1].Term = "dungeonRoute_heroineWatch";
					}
					else
					{
						dungeonMapManager.routeResultGroupLocArray[1].Term = "dungeonRoute_heroineTouch";
					}
				}
				else
				{
					dungeonMapManager.routeResultGroupLocArray[1].Term = "dungeonRoute_heroineTalk";
				}
				timelineAsset = dungeonRouteAnimationManager.dungeonRouteTimelineDictionary["talk"];
				closureTime.SetValue(80f);
				CheckHeroineDebuffClear();
				ResultTextSetActive("heroine");
				break;
			}
			case "buffAttack":
			{
				int powerNum8 = dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].powerNum;
				dungeonMapStatusManager.dungeonBuffAttack += powerNum8;
				dungeonMapStatusManager.dungeonBuffAttack = Mathf.Clamp(dungeonMapStatusManager.dungeonBuffAttack, -50, 50);
				dungeonMapManager.routeResultGroupLocArray[0].Term = "dungeonRoute_buffAttack";
				dungeonMapManager.routeResultGroupLocArray[2].Term = "dungeonRoute_Attack";
				dungeonMapManager.routeResultNumText[1].text = powerNum8 + "%";
				dungeonMapManager.routeResultGroupLocArray[6].Term = "dungeonRoute_StatusUp";
				timelineAsset = dungeonRouteAnimationManager.dungeonRouteTimelineDictionary["walk"];
				closureTime.SetValue(80f);
				ResultTextSetActive("buff");
				break;
			}
			case "buffDefense":
			{
				int powerNum9 = dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].powerNum;
				dungeonMapStatusManager.dungeonBuffDefense += powerNum9;
				dungeonMapStatusManager.dungeonBuffDefense = Mathf.Clamp(dungeonMapStatusManager.dungeonBuffDefense, -50, 50);
				dungeonMapManager.routeResultGroupLocArray[0].Term = "dungeonRoute_buffDefense";
				dungeonMapManager.routeResultGroupLocArray[2].Term = "dungeonRoute_Defense";
				dungeonMapManager.routeResultNumText[1].text = powerNum9 + "%";
				dungeonMapManager.routeResultGroupLocArray[6].Term = "dungeonRoute_StatusUp";
				timelineAsset = dungeonRouteAnimationManager.dungeonRouteTimelineDictionary["walk"];
				closureTime.SetValue(80f);
				ResultTextSetActive("buff");
				break;
			}
			case "deBuffAttack":
			{
				dungeonMapStatusManager.playerStatusRefreshType = "libido";
				dungeonMapStatusManager.beforePlayerStatusValue = PlayerDataManager.playerLibido;
				PlayerDataManager.playerLibido = Mathf.Clamp(PlayerDataManager.playerLibido - 10, 0, 100);
				int powerNum6 = dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].powerNum;
				dungeonMapStatusManager.dungeonBuffAttack -= powerNum6;
				dungeonMapStatusManager.dungeonBuffAttack = Mathf.Clamp(dungeonMapStatusManager.dungeonBuffAttack, -50, 50);
				Debug.Log("ダンジョン攻撃デバフ効果：" + powerNum6 + "／結果：" + dungeonMapStatusManager.dungeonBuffAttack);
				dungeonMapManager.routeResultGroupLocArray[0].Term = "dungeonRoute_deBuffAttack";
				dungeonMapManager.routeResultGroupLocArray[2].Term = "dungeonRoute_Attack";
				dungeonMapManager.routeResultNumText[1].text = powerNum6 + "%";
				dungeonMapManager.routeResultGroupLocArray[6].Term = "dungeonRoute_StatusDown";
				timelineAsset = dungeonRouteAnimationManager.dungeonRouteTimelineDictionary["wait"];
				dungeonRouteAnimationManager.dungeonRouteAnimationRouteImage.sprite = dungeonRouteAnimationManager.dungeonRouteBgDictionary["gruesome"];
				closureTime.SetValue(80f);
				ResultTextSetActive("buff");
				break;
			}
			case "deBuffDefense":
			{
				dungeonMapStatusManager.playerStatusRefreshType = "libido";
				dungeonMapStatusManager.beforePlayerStatusValue = PlayerDataManager.playerLibido;
				PlayerDataManager.playerLibido = Mathf.Clamp(PlayerDataManager.playerLibido - 10, 0, 100);
				int powerNum7 = dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].powerNum;
				dungeonMapStatusManager.dungeonBuffDefense -= powerNum7;
				dungeonMapStatusManager.dungeonBuffDefense = Mathf.Clamp(dungeonMapStatusManager.dungeonBuffDefense, -50, 50);
				dungeonMapManager.routeResultGroupLocArray[0].Term = "dungeonRoute_deBuffDefense";
				dungeonMapManager.routeResultGroupLocArray[2].Term = "dungeonRoute_Defense";
				dungeonMapManager.routeResultNumText[1].text = powerNum7 + "%";
				dungeonMapManager.routeResultGroupLocArray[6].Term = "dungeonRoute_StatusDown";
				timelineAsset = dungeonRouteAnimationManager.dungeonRouteTimelineDictionary["wait"];
				dungeonRouteAnimationManager.dungeonRouteAnimationRouteImage.sprite = dungeonRouteAnimationManager.dungeonRouteBgDictionary["gruesome"];
				closureTime.SetValue(80f);
				ResultTextSetActive("buff");
				break;
			}
			case "deBuffAgility":
			{
				int powerNum4 = dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].powerNum;
				dungeonMapStatusManager.dungeonDeBuffAgility -= powerNum4;
				dungeonMapStatusManager.dungeonDeBuffAgility = Mathf.Clamp(dungeonMapStatusManager.dungeonDeBuffAgility, -5, 0);
				int value = Random.Range(1, 4);
				dungeonMapStatusManager.dungeonDeBuffAgiityRemainFloor = Mathf.Clamp(value, 0, 3);
				dungeonMapManager.routeResultGroupLocArray[0].Term = "dungeonRoute_deBuffAgility";
				dungeonMapManager.routeResultGroupLocArray[2].Term = "dungeonRoute_Agility";
				dungeonMapManager.routeResultNumText[1].text = powerNum4.ToString();
				dungeonMapManager.routeResultGroupLocArray[6].Term = "dungeonRoute_StatusDown";
				timelineAsset = dungeonRouteAnimationManager.dungeonRouteTimelineDictionary["wait"];
				if (dungeonRouteAnimationManager.dungeonRouteAnimationBgImage.sprite.name == "dungeon_base")
				{
					dungeonRouteAnimationManager.dungeonRouteAnimationRouteImage.sprite = dungeonRouteAnimationManager.dungeonRouteBgDictionary["muddy"];
				}
				else
				{
					dungeonRouteAnimationManager.dungeonRouteAnimationRouteImage.sprite = dungeonRouteAnimationManager.dungeonRouteBgDictionary["muddy_cave"];
				}
				closureTime.SetValue(80f);
				ResultTextSetActive("buff");
				break;
			}
			case "clearAgility":
				dungeonMapStatusManager.dungeonDeBuffAgility = 0;
				dungeonMapManager.routeResultGroupLocArray[0].Term = "dungeonRoute_clearAgility";
				dungeonMapManager.routeResultGroupLocArray[2].Term = "dungeonRoute_Agility";
				dungeonMapManager.routeResultGroupLocArray[6].Term = "dungeonRoute_StatusClear";
				timelineAsset = dungeonRouteAnimationManager.dungeonRouteTimelineDictionary["wait"];
				if (dungeonRouteAnimationManager.dungeonRouteAnimationBgImage.sprite.name == "dungeon_base")
				{
					dungeonRouteAnimationManager.dungeonRouteAnimationRouteImage.sprite = dungeonRouteAnimationManager.dungeonRouteBgDictionary["water"];
				}
				else
				{
					dungeonRouteAnimationManager.dungeonRouteAnimationRouteImage.sprite = dungeonRouteAnimationManager.dungeonRouteBgDictionary["water_cave"];
				}
				closureTime.SetValue(80f);
				ResultTextSetActive("clearAgility");
				break;
			case "buffRetreat":
			{
				int powerNum2 = dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].powerNum;
				dungeonMapStatusManager.dungeonBuffRetreat += powerNum2;
				dungeonMapStatusManager.dungeonBuffRetreat = Mathf.Clamp(dungeonMapStatusManager.dungeonBuffRetreat, -30, 30);
				dungeonMapManager.routeResultGroupLocArray[0].Term = "dungeonRoute_buffRetreat";
				dungeonMapManager.routeResultGroupLocArray[2].Term = "dungeonRoute_Retreat";
				dungeonMapManager.routeResultNumText[1].text = powerNum2 + "%";
				dungeonMapManager.routeResultGroupLocArray[6].Term = "dungeonRoute_StatusUp";
				timelineAsset = dungeonRouteAnimationManager.dungeonRouteTimelineDictionary["walk"];
				closureTime.SetValue(80f);
				ResultTextSetActive("buff");
				break;
			}
			case "deBuffRetreat":
			{
				int powerNum3 = dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].powerNum;
				dungeonMapStatusManager.dungeonBuffRetreat -= powerNum3;
				dungeonMapStatusManager.dungeonBuffRetreat = Mathf.Clamp(dungeonMapStatusManager.dungeonBuffRetreat, -30, 30);
				dungeonMapManager.routeResultGroupLocArray[0].Term = "dungeonRoute_deBuffRetreat";
				dungeonMapManager.routeResultGroupLocArray[2].Term = "dungeonRoute_Retreat";
				dungeonMapManager.routeResultNumText[1].text = powerNum3 + "%";
				dungeonMapManager.routeResultGroupLocArray[6].Term = "dungeonRoute_StatusDown";
				timelineAsset = dungeonRouteAnimationManager.dungeonRouteTimelineDictionary["walk"];
				closureTime.SetValue(80f);
				ResultTextSetActive("buff");
				break;
			}
			case "camp":
			{
				dungeonMapStatusManager.playerStatusRefreshType = "hp";
				dungeonMapStatusManager.beforePlayerStatusValue = PlayerStatusDataManager.playerAllHp;
				int powerNum10 = dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].powerNum;
				int num10 = Mathf.FloorToInt((float)PlayerStatusDataManager.playerAllMaxHp * ((float)powerNum10 / 100f));
				Debug.Log("回復前HP：" + PlayerStatusDataManager.playerAllHp);
				PlayerStatusDataManager.playerAllHp = Mathf.Clamp(PlayerStatusDataManager.playerAllHp + num10, 0, PlayerStatusDataManager.playerAllMaxHp);
				Debug.Log("回復量：" + num10 + "／回復後HP：" + PlayerStatusDataManager.playerAllHp);
				if (dungeonMapStatusManager.dungeonDeBuffAgility < 0)
				{
					dungeonMapStatusManager.dungeonDeBuffAgility = 0;
					dungeonMapStatusManager.dungeonDeBuffAgiityRemainFloor = 0;
				}
				dungeonMapManager.routeResultGroupLocArray[0].Term = "dungeonRoute_camp";
				dungeonMapManager.routeResultGroupLocArray[2].Term = "dungeonRoute_Hp";
				dungeonMapManager.routeResultNumText[1].text = powerNum10 + "%";
				dungeonMapManager.routeResultGroupLocArray[6].Term = "dungeonRoute_Heal";
				timelineAsset = dungeonRouteAnimationManager.dungeonRouteTimelineDictionary["wait"];
				dungeonRouteAnimationManager.dungeonRouteAnimationRouteImage.sprite = dungeonRouteAnimationManager.dungeonRouteBgDictionary["camp"];
				closureTime.SetValue(80f);
				ResultTextSetActive("buff");
				break;
			}
			case "healFountain":
			{
				dungeonMapStatusManager.playerStatusRefreshType = "hp";
				dungeonMapStatusManager.beforePlayerStatusValue = PlayerStatusDataManager.playerAllHp;
				int powerNum5 = dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].powerNum;
				int num7 = Mathf.FloorToInt((float)PlayerStatusDataManager.playerAllMaxHp * ((float)powerNum5 / 100f));
				Debug.Log("回復前HP：" + PlayerStatusDataManager.playerAllHp);
				PlayerStatusDataManager.playerAllHp = Mathf.Clamp(PlayerStatusDataManager.playerAllHp + num7, 0, PlayerStatusDataManager.playerAllMaxHp);
				Debug.Log("回復量：" + num7 + "／回復後HP：" + PlayerStatusDataManager.playerAllHp);
				if (dungeonMapStatusManager.dungeonBuffAttack < 0)
				{
					dungeonMapStatusManager.dungeonBuffAttack = 0;
				}
				if (dungeonMapStatusManager.dungeonBuffDefense < 0)
				{
					dungeonMapStatusManager.dungeonBuffDefense = 0;
				}
				if (dungeonMapStatusManager.dungeonBuffRetreat < 0)
				{
					dungeonMapStatusManager.dungeonBuffRetreat = 0;
				}
				if (dungeonMapStatusManager.dungeonDeBuffAgility < 0)
				{
					dungeonMapStatusManager.dungeonDeBuffAgility = 0;
					dungeonMapStatusManager.dungeonDeBuffAgiityRemainFloor = 0;
				}
				dungeonMapManager.routeResultGroupLocArray[0].Term = "dungeonRoute_camp";
				dungeonMapManager.routeResultGroupLocArray[2].Term = "dungeonRoute_Hp";
				dungeonMapManager.routeResultNumText[1].text = powerNum5 + "%";
				dungeonMapManager.routeResultGroupLocArray[6].Term = "dungeonRoute_Heal";
				timelineAsset = dungeonRouteAnimationManager.dungeonRouteTimelineDictionary["wait"];
				dungeonRouteAnimationManager.dungeonRouteAnimationRouteImage.sprite = dungeonRouteAnimationManager.dungeonRouteBgDictionary["pond"];
				closureTime.SetValue(80f);
				ResultTextSetActive("buff");
				break;
			}
			case "medicFountain":
			{
				dungeonMapStatusManager.playerStatusRefreshType = "hp";
				dungeonMapStatusManager.beforePlayerStatusValue = PlayerStatusDataManager.playerAllHp;
				int powerNum = dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].powerNum;
				int num5 = Mathf.FloorToInt((float)PlayerStatusDataManager.playerAllMaxHp * ((float)powerNum / 100f));
				Debug.Log("回復前HP：" + PlayerStatusDataManager.playerAllHp);
				PlayerStatusDataManager.playerAllHp = Mathf.Clamp(PlayerStatusDataManager.playerAllHp + num5, 0, PlayerStatusDataManager.playerAllMaxHp);
				Debug.Log("回復量：" + num5 + "／回復後HP：" + PlayerStatusDataManager.playerAllHp);
				if (dungeonMapStatusManager.dungeonBuffAttack < 0)
				{
					dungeonMapStatusManager.dungeonBuffAttack = 0;
				}
				if (dungeonMapStatusManager.dungeonBuffDefense < 0)
				{
					dungeonMapStatusManager.dungeonBuffDefense = 0;
				}
				if (dungeonMapStatusManager.dungeonBuffRetreat < 0)
				{
					dungeonMapStatusManager.dungeonBuffRetreat = 0;
				}
				if (dungeonMapStatusManager.dungeonDeBuffAgility < 0)
				{
					dungeonMapStatusManager.dungeonDeBuffAgility = 0;
					dungeonMapStatusManager.dungeonDeBuffAgiityRemainFloor = 0;
				}
				dungeonMapManager.routeResultGroupLocArray[0].Term = "dungeonRoute_camp";
				dungeonMapManager.routeResultGroupLocArray[2].Term = "dungeonRoute_Hp";
				dungeonMapManager.routeResultNumText[1].text = powerNum + "%";
				dungeonMapManager.routeResultGroupLocArray[6].Term = "dungeonRoute_Heal";
				timelineAsset = dungeonRouteAnimationManager.dungeonRouteTimelineDictionary["wait"];
				dungeonRouteAnimationManager.dungeonRouteAnimationRouteImage.sprite = dungeonRouteAnimationManager.dungeonRouteBgDictionary["pond2"];
				closureTime.SetValue(80f);
				ResultTextSetActive("buff");
				break;
			}
			}
		}
		else
		{
			dungeonMapManager.routeAnimationGroupArray[0].SetActive(value: false);
			dungeonMapManager.routeAnimationGroupArray[1].SetActive(value: true);
			for (int m = 0; m < dungeonMapManager.routeSelectFrameMiniArray.Length; m++)
			{
				dungeonMapManager.routeSelectFrameMiniArray[m].GetComponent<ParameterContainer>().GetVariable<UguiImage>("iconImageGo").image.sprite = GameDataManager.instance.itemCategoryDataBase.dungeonCardIconDictionary["noRoute"];
			}
			dungeonMapManager.routeSelectBigFrameMini.GetComponent<ParameterContainer>().GetVariable<UguiImage>("iconImageGo").image.sprite = GameDataManager.instance.itemCategoryDataBase.dungeonCardIconDictionary["boss"];
			dungeonMapManager.routeResultGroupLocArray[0].Term = "dungeonRoute_preBattle";
			dungeonMapManager.routeResultGroupLocArray[6].Term = "dungeonRoute_boss";
			timelineAsset = dungeonRouteAnimationManager.dungeonRouteTimelineDictionary["bossBattle"];
			closureTime.SetValue(80f);
			ResultTextSetActive("battle");
			PlayerNonSaveDataManager.selectScenarioName = PlayerDataManager.currentDungeonName + "_BossEvent";
		}
		dungeonMapManager.routeResultGroupArray[0].SetActive(value: true);
		dungeonMapManager.dungeonMapDirector.time = 0.0;
		dungeonMapManager.dungeonMapDirector.Play(timelineAsset);
		Debug.Log("ルートのアニメ速度：" + dungeonMapManager.dungeonMapDirector.playableGraph.GetRootPlayable(0).GetSpeed());
		int dungeonMoveSpeed = PlayerDataManager.dungeonMoveSpeed;
		dungeonMapManager.dungeonMapDirector.playableGraph.GetRootPlayable(0).SetSpeed(dungeonMoveSpeed);
		float pitch = 1 / PlayerDataManager.dungeonMoveSpeed;
		dungeonRouteAnimationManager.dungeonRouteAudioSource.pitch = pitch;
		float dungeonMapAnimationDuration = (float)dungeonMapManager.dungeonMapDirector.playableAsset.duration;
		dungeonMapManager.dungeonMapAnimationDuration = dungeonMapAnimationDuration;
		Debug.Log("ルートアニメの再生開始");
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

	private string GetItemCategoryToString(int itemID)
	{
		string result = "";
		Debug.Log("アイテムIDからカテゴリーを検索して返す：" + itemID);
		if (itemID < 100)
		{
			result = GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData data) => data.itemID == itemID).category.ToString();
		}
		else if (itemID < 600)
		{
			result = GameDataManager.instance.itemMaterialDataBase.itemMaterialDataList.Find((ItemMaterialData data) => data.itemID == itemID).category.ToString();
		}
		else if (itemID < 900)
		{
			result = GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList.Find((ItemMagicMaterialData data) => data.itemID == itemID).category.ToString();
		}
		else if (itemID < 1000)
		{
			result = GameDataManager.instance.itemCashableItemDataBase.itemCashableItemDataList.Find((ItemCashableItemData data) => data.itemID == itemID).category.ToString();
		}
		return result;
	}

	private int GetMoneyAdd(DungeonMapData dungeonMapData)
	{
		int num = Random.Range(dungeonMapData.maxDropMoney[dungeonMapManager.currentBorderNum] / 2, dungeonMapData.maxDropMoney[dungeonMapManager.currentBorderNum] + 1);
		dungeonMapManager.getDropMoney += num;
		return num;
	}

	private int CheckAddLibidoBonus()
	{
		float num = 0f;
		int result = 0;
		if (dungeonMapManager.selectCardBonusArray[dungeonMapManager.thisFloorActionNum] > 1 && PlayerEquipDataManager.accessoryLibidoUpRate != -100)
		{
			num = dungeonMapManager.selectCardBonusArray[dungeonMapManager.thisFloorActionNum];
			num /= 3f;
			result = Mathf.CeilToInt((float)dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].powerNum * num);
			dungeonMapManager.routeResultGroupLocArray[4].gameObject.SetActive(value: true);
			dungeonMapManager.routeResultGroupLocArray[4].Term = "plusMark";
			dungeonMapManager.routeResultNumText[1].text = result.ToString();
			dungeonMapManager.routeResultNumText[1].color = new Color(1f, 0.5f, 0.8f);
			dungeonMapManager.routeResultNumText[1].gameObject.SetActive(value: true);
			Debug.Log("性欲加算ボーナスあり");
		}
		else
		{
			dungeonMapManager.routeResultGroupLocArray[4].gameObject.SetActive(value: false);
			dungeonMapManager.routeResultGroupLocArray[7].gameObject.SetActive(value: false);
			dungeonMapManager.routeResultNumText[1].gameObject.SetActive(value: false);
			Debug.Log("性欲加算ボーナスなし");
		}
		Debug.Log("ボーナス性欲：" + result + "／ボーナス倍率：" + num);
		return result;
	}

	private void ResultTextSetActive(string type)
	{
		switch (type)
		{
		case "battle":
			dungeonMapManager.routeResultGroupLocArray[0].gameObject.SetActive(value: true);
			dungeonMapManager.routeResultGroupLocArray[6].gameObject.SetActive(value: true);
			break;
		case "collect":
			dungeonMapManager.routeResultGroupLocArray[0].gameObject.SetActive(value: true);
			dungeonMapManager.routeResultGroupLocArray[3].gameObject.SetActive(value: true);
			dungeonMapManager.routeResultGroupLocArray[6].gameObject.SetActive(value: true);
			break;
		case "buff":
			dungeonMapManager.routeResultGroupLocArray[0].gameObject.SetActive(value: true);
			dungeonMapManager.routeResultGroupLocArray[2].gameObject.SetActive(value: true);
			dungeonMapManager.routeResultGroupLocArray[6].gameObject.SetActive(value: true);
			dungeonMapManager.routeResultNumText[1].gameObject.SetActive(value: true);
			break;
		case "clearAgility":
			dungeonMapManager.routeResultGroupLocArray[0].gameObject.SetActive(value: true);
			dungeonMapManager.routeResultGroupLocArray[2].gameObject.SetActive(value: true);
			dungeonMapManager.routeResultGroupLocArray[6].gameObject.SetActive(value: true);
			break;
		case "heroineImage":
			dungeonMapManager.routeResultGroupLocArray[0].gameObject.SetActive(value: true);
			dungeonMapManager.routeResultGroupLocArray[2].gameObject.SetActive(value: true);
			dungeonMapManager.routeResultGroupLocArray[6].gameObject.SetActive(value: true);
			dungeonMapManager.routeResultNumText[0].gameObject.SetActive(value: true);
			break;
		case "heroine":
			dungeonMapManager.routeResultGroupLocArray[0].gameObject.SetActive(value: true);
			dungeonMapManager.routeResultGroupLocArray[1].gameObject.SetActive(value: true);
			dungeonMapManager.routeResultGroupLocArray[2].gameObject.SetActive(value: true);
			dungeonMapManager.routeResultGroupLocArray[6].gameObject.SetActive(value: true);
			dungeonMapManager.routeResultNumText[0].gameObject.SetActive(value: true);
			break;
		}
	}

	private void CheckHeroineDebuffClear()
	{
		int num = Random.Range(0, 100);
		bool flag = false;
		if (dungeonMapStatusManager.dungeonBuffAttack < 0 && Random.Range(0, 100) < 50 && num < 20)
		{
			dungeonMapStatusManager.dungeonBuffAttack = 0;
			flag = true;
			Debug.Log("攻撃力デバフを消去");
		}
		if (dungeonMapStatusManager.dungeonBuffDefense < 0 && !flag && Random.Range(0, 100) < 50 && num < 20)
		{
			dungeonMapStatusManager.dungeonBuffDefense = 0;
			Debug.Log("防御力デバフを消去");
		}
	}

	private void CheckVigilantDecrementDebuff()
	{
		if (dungeonMapStatusManager.dungeonBuffAttack < 0)
		{
			dungeonMapStatusManager.dungeonBuffAttack /= 2;
			Debug.Log("攻撃デバフを半減");
		}
		if (dungeonMapStatusManager.dungeonBuffDefense < 0)
		{
			dungeonMapStatusManager.dungeonBuffDefense /= 2;
			Debug.Log("防御デバフを半減");
		}
		if (dungeonMapStatusManager.dungeonDeBuffAgility < 0)
		{
			dungeonMapStatusManager.dungeonDeBuffAgility /= 2;
			Debug.Log("素早さデバフを半減");
		}
		if (dungeonMapStatusManager.dungeonBuffRetreat < 0)
		{
			dungeonMapStatusManager.dungeonBuffRetreat /= 2;
			Debug.Log("撤退率デバフを半減");
		}
	}
}
