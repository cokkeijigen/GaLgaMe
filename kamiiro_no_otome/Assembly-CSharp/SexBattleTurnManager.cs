using PathologicalGames;
using Sirenix.OdinInspector;
using UnityEngine;

public class SexBattleTurnManager : SerializedMonoBehaviour
{
	public SexBattleManager sexBattleManager;

	public int sexBattleDamageValue;

	public int sexBattleSelfDamageValue;

	public int sexBattleHealValue;

	public int sexBattleAddTranceValue;

	public int sexBattleAddSelfTranceValue;

	public int sexBattlePistonCount;

	public int sexBattleBerserkClickCount;

	public int sexBattlePlayerEcstasyCount;

	public int sexBattlePlayerOutShotCount;

	public int sexBattlePlayerInShotCount;

	public int sexBattlePlayerCondomShotCount;

	public int sexBattleTotalTurnCount;

	public int sexBattleTotalHeroineTranceNum;

	public int sexBattleHeroineEcstasyCount;

	public int sexBattleRemainCumShotCount;

	public bool isCriticalSuccess;

	public bool isSubPowerSuccess;

	public bool isCrazy;

	public bool isFertilizeRepeatPiston;

	public bool isVictoryPiston;

	public string currentEdenSkillTypeName;

	public GameObject[] tranceArrowPrefabGoArray;

	public void SpawnTranceArrow()
	{
		if (sexBattleAddSelfTranceValue > 0)
		{
			PoolManager.Pools["sexBattlePool"].Spawn(tranceArrowPrefabGoArray[0], sexBattleManager.playerTranceImageArray[0]);
		}
		else
		{
			PoolManager.Pools["sexBattlePool"].Spawn(tranceArrowPrefabGoArray[1], sexBattleManager.playerTranceImageArray[0]);
		}
		if (sexBattleAddTranceValue == 0)
		{
			PoolManager.Pools["sexBattlePool"].Spawn(tranceArrowPrefabGoArray[2], sexBattleManager.playerTranceImageArray[1]);
		}
		else if (sexBattleAddTranceValue > 0)
		{
			PoolManager.Pools["sexBattlePool"].Spawn(tranceArrowPrefabGoArray[0], sexBattleManager.playerTranceImageArray[1]);
		}
		else
		{
			PoolManager.Pools["sexBattlePool"].Spawn(tranceArrowPrefabGoArray[1], sexBattleManager.playerTranceImageArray[1]);
		}
	}

	public bool CheckSexBattleHaveCrazy()
	{
		bool result = false;
		if (PlayerSexStatusDataManager.playerSexBuffCondition[0].Count > 0 && PlayerSexBattleConditionAccess.GetSexBattleSubPower(PlayerSexStatusDataManager.playerSexSubPower[0], "crazy") > 0)
		{
			result = true;
		}
		isCrazy = result;
		return result;
	}
}
