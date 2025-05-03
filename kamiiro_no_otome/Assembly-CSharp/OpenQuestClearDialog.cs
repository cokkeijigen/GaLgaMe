using Arbor;
using DarkTonic.MasterAudio;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class OpenQuestClearDialog : StateBehaviour
{
	private QuestManager questManager;

	private QuestApplyManager questApplyManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		questManager = GameObject.Find("Quest Manager").GetComponent<QuestManager>();
		questApplyManager = GameObject.Find("Quest Apply Manager").GetComponent<QuestApplyManager>();
	}

	public override void OnStateBegin()
	{
		QuestData questData = GameDataManager.instance.questDataBase.questDataList.Find((QuestData data) => data.sortID == questManager.clickedQuestID);
		PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questManager.clickedQuestID).isClear = true;
		questApplyManager.dialogCanvasGo.SetActive(value: true);
		questApplyManager.questTypeImage.sprite = GameDataManager.instance.itemCategoryDataBase.questIconDictionary[questData.questType.ToString()];
		for (int i = 0; i < questData.rewardList.Count; i++)
		{
			Transform obj = PoolManager.Pools["questPool"].Spawn(questManager.questRewardPrefabGo, questApplyManager.rewardContents);
			obj.localScale = new Vector3(1f, 1f, 1f);
			ParameterContainer component = obj.GetComponent<ParameterContainer>();
			string itemNameTerm = PlayerInventoryDataAccess.GetItemNameTerm(questData.rewardList[i][0]);
			Sprite itemSprite = PlayerInventoryDataAccess.GetItemSprite(questData.rewardList[i][0]);
			component.SetInt("itemID", questData.rewardList[i][0]);
			component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = itemNameTerm;
			component.GetVariable<UguiImage>("itemImage").image.sprite = itemSprite;
			if (questData.rewardList[i][0] == 9030)
			{
				component.GetVariable<TmpText>("itemCountText").textMeshProUGUI.text = "10";
			}
			else
			{
				component.GetVariable<TmpText>("itemCountText").textMeshProUGUI.text = questData.rewardList[i][1].ToString();
			}
		}
		if ((PlayerDataManager.currentPlaceName == "HunterGuild" && PlayerDataManager.mapPlaceStatusNum == 2) || (PlayerDataManager.currentPlaceName == "Bar" && PlayerDataManager.mapPlaceStatusNum == 2))
		{
			questManager.questCharacterTextLoc.Term = "questClearBalloon_HunterGuild";
			questManager.questCharacterImage.sprite = questManager.questCharacterImageDictionary["HunterGuild"];
		}
		else
		{
			questManager.questCharacterTextLoc.Term = "questClearBalloon";
			questManager.questCharacterImage.sprite = questManager.questCharacterImageDictionary["Eden"];
		}
		Transform transform = PoolManager.Pools["questPool"].Spawn(questApplyManager.questClearPrefabGo, questApplyManager.uIParticle.transform);
		questApplyManager.questClearSpawnGo = transform;
		MasterAudio.PlaySound("SeQuestClear", 1f, null, 0f, null, null);
		transform.localScale = new Vector3(1f, 1f, 1f);
		transform.localPosition = new Vector3(0f, 0f, 0f);
		Debug.Log("売り上げエフェクトをスポーン");
		questApplyManager.uIParticle.RefreshParticles();
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
