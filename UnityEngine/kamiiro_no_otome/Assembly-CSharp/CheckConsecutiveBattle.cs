using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckConsecutiveBattle : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	public StateLink consecutiveLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
	}

	public override void OnStateBegin()
	{
		int thisFloorActionNum = dungeonMapManager.thisFloorActionNum;
		if (dungeonMapManager.thisFloorActionNum < 2 && !dungeonMapManager.isBossRouteSelect && !PlayerNonSaveDataManager.isDungeonScnearioBattle && (dungeonMapManager.selectCardList[thisFloorActionNum + 1].subTypeString == "battle" || dungeonMapManager.selectCardList[thisFloorActionNum + 1].subTypeString == "hardBattle"))
		{
			dungeonMapManager.thisFloorActionNum++;
			dungeonMapManager.battleConsecutiveRoundNum++;
			dungeonMapManager.isMimicBattle = false;
			CalcConsecutiveResultData();
			Transition(consecutiveLink);
		}
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

	private void CalcConsecutiveResultData()
	{
		int num = PlayerStatusDataManager.enemyGold.Sum();
		float num2 = Random.Range(0.9f, 1.1f);
		num = Mathf.FloorToInt((float)num * num2);
		dungeonMapManager.consecutiveResultData[0] += num;
		int num3 = PlayerStatusDataManager.enemyExp.Sum();
		float num4 = 0f;
		for (int i = 0; i < PlayerStatusDataManager.playerPartyMember.Length; i++)
		{
			num4 += (float)PlayerStatusDataManager.characterLv[PlayerStatusDataManager.playerPartyMember[i]];
		}
		num4 /= (float)PlayerStatusDataManager.playerPartyMember.Length;
		float num5 = PlayerStatusDataManager.enemyLv.Sum();
		num5 /= (float)PlayerStatusDataManager.enemyMember.Length;
		Debug.Log("味方LV：" + num4 + "／敵LV：" + num5);
		if (num5 > num4)
		{
			float num6 = Mathf.Clamp(num5 / num4, 0f, 2f);
			float f = (float)num3 * num6;
			num3 = Mathf.FloorToInt(f);
			Debug.Log("LV差のEXPボーナス：" + Mathf.FloorToInt(f));
		}
		if (PlayerEquipDataManager.accessoryExpUp > 0)
		{
			num3 = Mathf.FloorToInt((float)num3 * 1.5f);
		}
		dungeonMapManager.consecutiveResultData[1] += num3;
		for (int j = 0; j < PlayerStatusDataManager.enemyMember.Length; j++)
		{
			dungeonMapManager.consecutiveResultEnemyMember.Add(PlayerStatusDataManager.enemyMember[j]);
		}
	}
}
