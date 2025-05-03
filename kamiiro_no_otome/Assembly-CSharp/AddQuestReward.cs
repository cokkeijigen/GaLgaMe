using Arbor;
using DarkTonic.MasterAudio;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class AddQuestReward : StateBehaviour
{
	private HeaderStatusManager headerStatusManager;

	private QuestManager questManager;

	private QuestApplyManager questApplyManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		questManager = GameObject.Find("Quest Manager").GetComponent<QuestManager>();
		questApplyManager = GameObject.Find("Quest Apply Manager").GetComponent<QuestApplyManager>();
	}

	public override void OnStateBegin()
	{
		questApplyManager.levelUpFrameGo.SetActive(value: false);
		QuestData questData = GameDataManager.instance.questDataBase.questDataList.Find((QuestData data) => data.sortID == questManager.clickedQuestID);
		int i;
		for (i = 0; i < questData.rewardList.Count; i++)
		{
			string itemCategory = PlayerInventoryDataAccess.GetItemCategory(questData.rewardList[i][0]);
			int itemSortID = PlayerInventoryDataAccess.GetItemSortID(questData.rewardList[i][0]);
			switch (itemCategory)
			{
			case "jewel":
				PlayerInventoryDataEquipAccess.PlayerHaveAccessoryAdd(GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData item) => item.itemID == questData.rewardList[i][0]));
				break;
			case "eventItem":
				PlayerInventoryDataAccess.PlayerHaveEventItemAdd(questData.rewardList[i][0], itemSortID);
				break;
			case "questItem":
				switch (questData.rewardList[i][0])
				{
				case 9000:
					PlayerDataManager.AddHaveMoney(questData.rewardList[i][1]);
					break;
				case 9010:
				{
					PlayerStatusDataManager.AddCharacterExp(0, questData.rewardList[i][1]);
					int characterLvFromExp = PlayerStatusDataManager.GetCharacterLvFromExp(0);
					if (characterLvFromExp > PlayerStatusDataManager.characterLv[0] && PlayerStatusDataManager.characterLv[0] < 50)
					{
						PlayerStatusDataManager.characterLv[0] = characterLvFromExp;
						questApplyManager.levelUpFrameGo.SetActive(value: true);
						questApplyManager.levelUpTypeTextLoc.Term = "questLevelUpType_eden";
						questApplyManager.beforeLvNumText.text = (characterLvFromExp - 1).ToString();
						questApplyManager.afterLvNumText.text = characterLvFromExp.ToString();
						PlayerStatusDataManager.LvUpPlayerStatus(0, null);
						Invoke("SpawnLevelUpEffect", 0.5f);
					}
					break;
				}
				case 9020:
				{
					PlayerCraftStatusManager.AddCraftExp(questData.rewardList[i][1]);
					int craftLvFromExp = PlayerCraftStatusManager.GetCraftLvFromExp();
					if (craftLvFromExp > PlayerCraftStatusManager.playerCraftLv && PlayerCraftStatusManager.playerCraftLv < 10)
					{
						PlayerCraftStatusManager.playerCraftLv = craftLvFromExp;
						questApplyManager.levelUpFrameGo.SetActive(value: true);
						questApplyManager.levelUpTypeTextLoc.Term = "questLevelUpType_craftLv";
						questApplyManager.beforeLvNumText.text = (craftLvFromExp - 1).ToString();
						questApplyManager.afterLvNumText.text = craftLvFromExp.ToString();
						Invoke("SpawnLevelUpEffect", 0.5f);
					}
					break;
				}
				case 9050:
					PlayerDataManager.playerHaveKizunaPoint += questData.rewardList[i][1];
					break;
				}
				break;
			default:
				PlayerInventoryDataAccess.PlayerHaveItemAdd(questData.rewardList[i][0], itemSortID, questData.rewardList[i][1]);
				break;
			}
		}
		PlayerInventoryDataAccess.HaveItemListSortAll();
		headerStatusManager.headerFSM.SendTrigger("HeaderStatusRefresh");
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

	private void SpawnLevelUpEffect()
	{
		Transform transform = PoolManager.Pools["questPool"].Spawn(questApplyManager.questLevelUpPrefabGo, questApplyManager.uIParticle_levelUp.transform);
		questApplyManager.questLevelUpSpawnGo = transform;
		MasterAudio.PlaySound("SeQuestLevelUp", 1f, null, 0f, null, null);
		transform.localScale = new Vector3(1f, 1f, 2f);
		transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
		transform.localPosition = new Vector3(0f, 0f, 0f);
		Debug.Log("売り上げエフェクトをスポーン");
		questApplyManager.uIParticle_levelUp.RefreshParticles();
	}
}
