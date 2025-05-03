using System.Collections.Generic;
using System.Linq;
using Arbor;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class OpenBattleSkillWindow : StateBehaviour
{
	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	private ParameterContainer skillWindowContainer;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
		skillWindowContainer = scenarioBattleSkillManager.skillWindow.GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		scenarioBattleSkillManager.isScrollContentClick = false;
		scenarioBattleSkillManager.scrollContentClickNum = 0;
		BattleSkillData battleSkillData = null;
		BattleSkillData battleSkillData2 = null;
		int skillID = 0;
		int firstSkillId = 0;
		scenarioBattleSkillManager.skillWindow.SetActive(value: true);
		scenarioBattleSkillManager.isOpenItemOrSkillWindow = true;
		GameObject[] array = GameObject.FindGameObjectsWithTag("StatusScrollItem");
		PoolManager.Pools["BattleScrollItem"].DespawnAll();
		if (array.Length != 0 && array != null)
		{
			GameObject[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].transform.SetParent(scenarioBattleSkillManager.battleItemSpawnParent.transform);
			}
		}
		int useSkillPartyMemberID = scenarioBattleTurnManager.useSkillPartyMemberID;
		_ = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == scenarioBattleTurnManager.useSkillPartyMemberID).memberNum;
		CheckMpType();
		if (useSkillPartyMemberID == 0)
		{
			if (scenarioBattleSkillManager.selectSkillMpType == 1)
			{
				useSkillPartyMemberID = PlayerStatusDataManager.partyMemberCount;
				List<LearnedSkillData> list = null;
				list = ((scenarioBattleSkillManager.selectSkillTypeNum != 0) ? PlayerInventoryDataManager.playerLearnedSkillList.Where((LearnedSkillData data) => data.skillID >= 200).ToList() : PlayerInventoryDataManager.playerLearnedSkillList.Where((LearnedSkillData data) => data.skillID < 200).ToList());
				for (int j = 0; j < list.Count; j++)
				{
					skillID = list[j].skillID;
					battleSkillData = GameDataManager.instance.playerSkillDataBase.skillDataList.Find((BattleSkillData data) => data.skillID == skillID);
					Debug.Log("表示する習得スキル：" + skillID);
					Transform transform = PoolManager.Pools["BattleScrollItem"].Spawn(scenarioBattleSkillManager.scrollContentPrefabGoArray[1]);
					ParameterContainer component = transform.GetComponent<ParameterContainer>();
					RefreshSkillList(transform, j);
					string term = "playerSkill" + battleSkillData.skillID;
					component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = term;
					component.GetVariable<UguiTextVariable>("needMpText").text.text = battleSkillData.useMP.ToString();
					int needRechargeTurn = PlayerBattleConditionManager.playerSkillRechargeTurn[useSkillPartyMemberID].Find((PlayerBattleConditionManager.MemberSkillReChargeTurn data) => data.skillID == skillID).needRechargeTurn;
					component.GetVariable<UguiTextVariable>("reChargeTurnText").text.text = needRechargeTurn.ToString();
					string category = battleSkillData.skillType.ToString();
					SetSkillIconSprite(transform, category);
					component.SetInt("skillID", battleSkillData.skillID);
					component.SetString("category", battleSkillData.skillType.ToString());
				}
				firstSkillId = list[0].skillID;
			}
			else
			{
				for (int k = 0; k < PlayerEquipDataManager.playerHaveSkillList[useSkillPartyMemberID].Count; k++)
				{
					skillID = PlayerEquipDataManager.playerHaveSkillList[useSkillPartyMemberID][k];
					battleSkillData = GameDataManager.instance.playerSkillDataBase.skillDataList.Find((BattleSkillData data) => data.skillID == skillID);
					Transform transform2 = PoolManager.Pools["BattleScrollItem"].Spawn(scenarioBattleSkillManager.scrollContentPrefabGoArray[1]);
					ParameterContainer component2 = transform2.GetComponent<ParameterContainer>();
					RefreshSkillList(transform2, k);
					string term2 = "playerSkill" + battleSkillData.skillID;
					component2.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = term2;
					component2.GetVariable<UguiTextVariable>("needMpText").text.text = battleSkillData.useMP.ToString();
					int needRechargeTurn2 = PlayerBattleConditionManager.playerSkillRechargeTurn[useSkillPartyMemberID].Find((PlayerBattleConditionManager.MemberSkillReChargeTurn data) => data.skillID == skillID).needRechargeTurn;
					component2.GetVariable<UguiTextVariable>("reChargeTurnText").text.text = needRechargeTurn2.ToString();
					string category2 = battleSkillData.skillType.ToString();
					SetSkillIconSprite(transform2, category2);
					component2.SetInt("skillID", battleSkillData.skillID);
					component2.SetString("category", battleSkillData.skillType.ToString());
				}
				firstSkillId = PlayerEquipDataManager.playerHaveSkillList[useSkillPartyMemberID][0];
			}
		}
		else
		{
			List<int> list2 = null;
			list2 = ((scenarioBattleSkillManager.selectSkillTypeNum != 0) ? PlayerEquipDataManager.playerHaveSkillList[useSkillPartyMemberID].Where((int data) => data >= 200).ToList() : PlayerEquipDataManager.playerHaveSkillList[useSkillPartyMemberID].Where((int data) => data < 200).ToList());
			for (int l = 0; l < list2.Count; l++)
			{
				skillID = list2[l];
				battleSkillData = GameDataManager.instance.playerSkillDataBase.skillDataList.Find((BattleSkillData data) => data.skillID == skillID);
				Transform transform3 = PoolManager.Pools["BattleScrollItem"].Spawn(scenarioBattleSkillManager.scrollContentPrefabGoArray[1]);
				ParameterContainer component3 = transform3.GetComponent<ParameterContainer>();
				RefreshSkillList(transform3, l);
				string term3 = "playerSkill" + battleSkillData.skillID;
				component3.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = term3;
				component3.GetVariable<UguiTextVariable>("needMpText").text.text = battleSkillData.useMP.ToString();
				int needRechargeTurn3 = PlayerBattleConditionManager.playerSkillRechargeTurn[useSkillPartyMemberID].Find((PlayerBattleConditionManager.MemberSkillReChargeTurn data) => data.skillID == skillID).needRechargeTurn;
				component3.GetVariable<UguiTextVariable>("reChargeTurnText").text.text = needRechargeTurn3.ToString();
				string category3 = battleSkillData.skillType.ToString();
				SetSkillIconSprite(transform3, category3);
				component3.SetInt("skillID", battleSkillData.skillID);
				component3.SetString("category", battleSkillData.skillType.ToString());
			}
			firstSkillId = list2[0];
		}
		battleSkillData2 = GameDataManager.instance.playerSkillDataBase.skillDataList.Find((BattleSkillData data) => data.skillID == firstSkillId);
		scenarioBattleTurnManager.battleUseSkillID = firstSkillId;
		scenarioBattleTurnManager.playerSkillData = battleSkillData2;
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

	private void RefreshSkillList(Transform transform, int i)
	{
		transform.SetParent(scenarioBattleSkillManager.skillContentGo.transform);
		transform.transform.localPosition = new Vector3(0f, 0f, 0f);
		transform.transform.localScale = new Vector3(1f, 1f, 1f);
		transform.transform.SetSiblingIndex(i);
	}

	private void SetSkillIconSprite(Transform go, string category)
	{
		Sprite sprite = GameDataManager.instance.itemCategoryDataBase.skillCategoryIconDictionary[category];
		go.transform.Find("Icon Image").GetComponent<Image>().sprite = sprite;
	}

	private void CheckMpType()
	{
		if (scenarioBattleTurnManager.useSkillPartyMemberID == 0)
		{
			scenarioBattleSkillManager.skillWindowMpCategoryGo.SetActive(value: false);
			scenarioBattleSkillManager.skillWindowMpFrame.offsetMin = new Vector2(0f, 30f);
			Debug.Log("MP切り替え非表示");
			Button[] skillWindowMpCategoryButton = scenarioBattleSkillManager.skillWindowMpCategoryButton;
			for (int i = 0; i < skillWindowMpCategoryButton.Length; i++)
			{
				skillWindowMpCategoryButton[i].GetComponent<Image>().sprite = scenarioBattleSkillManager.skillWindowMpCategoryButtonSprite[0];
			}
			switch (scenarioBattleSkillManager.selectSkillMpType)
			{
			case 0:
				scenarioBattleSkillManager.scrollSummaryNeedMpTextLoc.Term = "summaryUseTp";
				skillWindowContainer.GetVariable<TmpText>("currentMpText").textMeshProUGUI.text = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.equipCharacter == 0).weaponIncludeMp.ToString();
				skillWindowContainer.GetVariable<TmpText>("maxMpText").textMeshProUGUI.text = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.equipCharacter == 0).weaponIncludeMaxMp.ToString();
				scenarioBattleSkillManager.skillWindowMpCategoryButton[0].GetComponent<Image>().sprite = scenarioBattleSkillManager.skillWindowMpCategoryButtonSprite[1];
				scenarioBattleSkillManager.skillWindowMpCategoryLoc.Term = "statusItemWeaponMp";
				break;
			case 1:
				scenarioBattleSkillManager.scrollSummaryNeedMpTextLoc.Term = "summaryUseMp";
				skillWindowContainer.GetVariable<TmpText>("currentMpText").textMeshProUGUI.text = PlayerStatusDataManager.characterMp[0].ToString();
				skillWindowContainer.GetVariable<TmpText>("maxMpText").textMeshProUGUI.text = PlayerStatusDataManager.characterMaxMp[0].ToString();
				scenarioBattleSkillManager.skillWindowMpCategoryButton[1].GetComponent<Image>().sprite = scenarioBattleSkillManager.skillWindowMpCategoryButtonSprite[1];
				scenarioBattleSkillManager.skillWindowMpCategoryLoc.Term = "statusChatacterMp";
				break;
			}
		}
		else
		{
			scenarioBattleSkillManager.scrollSummaryNeedMpTextLoc.Term = "summaryUseMp";
			skillWindowContainer.GetVariable<TmpText>("currentMpText").textMeshProUGUI.text = PlayerStatusDataManager.characterMp[scenarioBattleTurnManager.useSkillPartyMemberID].ToString();
			skillWindowContainer.GetVariable<TmpText>("maxMpText").textMeshProUGUI.text = PlayerStatusDataManager.characterMaxMp[scenarioBattleTurnManager.useSkillPartyMemberID].ToString();
			scenarioBattleSkillManager.skillWindowMpCategoryGo.SetActive(value: false);
			scenarioBattleSkillManager.skillWindowMpFrame.offsetMin = new Vector2(0f, 30f);
			scenarioBattleSkillManager.skillWindowMpCategoryLoc.Term = "statusChatacterMp";
		}
	}
}
