using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class DungeonBattlePreStart : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	private DungeonMapManager dungeonMapManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = GameObject.Find("Dungeon Battle Manager").GetComponent<DungeonBattleManager>();
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
	}

	public override void OnStateBegin()
	{
		PoolManager.Pools["DungeonBattleEffect"].DespawnAll();
		PoolManager.Pools["SkillEffect"].DespawnAll();
		GameObject playerAgilityParent = dungeonBattleManager.playerAgilityParent;
		if (playerAgilityParent.transform.childCount > 0)
		{
			GameObject[] array = new GameObject[playerAgilityParent.transform.childCount];
			for (int i = 0; i < playerAgilityParent.transform.childCount; i++)
			{
				array[i] = playerAgilityParent.transform.GetChild(i).gameObject;
			}
			GameObject[] array2 = array;
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j].transform.SetParent(dungeonBattleManager.poolManagerGO);
			}
		}
		GameObject enemyAgilityParent = dungeonBattleManager.enemyAgilityParent;
		if (enemyAgilityParent.transform.childCount > 0)
		{
			GameObject[] array3 = new GameObject[enemyAgilityParent.transform.childCount];
			for (int k = 0; k < enemyAgilityParent.transform.childCount; k++)
			{
				array3[k] = enemyAgilityParent.transform.GetChild(k).gameObject;
			}
			GameObject[] array2 = array3;
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j].transform.SetParent(dungeonBattleManager.poolManagerGO);
			}
		}
		if (PlayerDataManager.dungeonBattleSpeed < 1)
		{
			PlayerDataManager.dungeonBattleSpeed = 1;
		}
		dungeonBattleManager.speedTmpGO.text = PlayerDataManager.dungeonBattleSpeed.ToString();
		GameObject.Find("Dungeon Agility Manager").GetComponent<ParameterContainer>().GetStringList("AgilityQueueList")
			.Clear();
		dungeonBattleManager.dungeonBattleCanvas.SetActive(value: true);
		dungeonBattleManager.commandGroup.SetActive(value: true);
		dungeonBattleManager.subGroup.SetActive(value: true);
		dungeonBattleManager.messageWindowGO.SetActive(value: false);
		dungeonBattleManager.dungeonBattleCanvas.GetComponent<CanvasGroup>().interactable = false;
		if (PlayerNonSaveDataManager.isDungeonScnearioBattle)
		{
			dungeonBattleManager.battleRetreatButtonGo.GetComponent<CanvasGroup>().interactable = false;
			dungeonBattleManager.battleRetreatButtonGo.GetComponent<CanvasGroup>().alpha = 0.5f;
		}
		else
		{
			dungeonBattleManager.battleRetreatButtonGo.GetComponent<CanvasGroup>().interactable = true;
			dungeonBattleManager.battleRetreatButtonGo.GetComponent<CanvasGroup>().alpha = 1f;
		}
		PlayerNonSaveDataManager.battleResultDialogType = "dungeonBattle";
		dungeonMapManager.roundNumText[0].text = (dungeonMapManager.battleConsecutiveRoundNum + 1).ToString();
		dungeonMapManager.roundNumText[1].text = (dungeonMapManager.battleConsecutiveTotalNum + 1).ToString();
		DungeonMapData dungeonMapData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData item) => item.dungeonName == PlayerDataManager.currentDungeonName);
		if (PlayerDataManager.currentTimeZone < 2)
		{
			dungeonBattleManager.dungeonBattleBgImage.sprite = dungeonMapData.dungeonBgList[dungeonMapManager.currentBorderNum];
		}
		else
		{
			dungeonBattleManager.dungeonBattleBgImage.sprite = dungeonMapData.dungeonNightBgList[dungeonMapManager.currentBorderNum];
		}
		dungeonBattleManager.dungeonBattleBgImage.SetNativeSize();
		dungeonBattleManager.dungeonBattleBgImage.rectTransform.localScale = new Vector3(dungeonMapData.dungeonBgScale, dungeonMapData.dungeonBgScale, dungeonMapData.dungeonBgScale);
		dungeonBattleManager.dungeonBattleBgImage.rectTransform.anchoredPosition = new Vector2(dungeonMapData.dungeonBgPositonX, dungeonMapData.dungeonBgPositonY);
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
}
