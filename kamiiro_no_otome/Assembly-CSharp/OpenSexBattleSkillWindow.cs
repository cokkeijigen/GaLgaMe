using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class OpenSexBattleSkillWindow : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
	}

	public override void OnStateBegin()
	{
		sexBattleManager.selectSkillScrollIndex = 0;
		sexBattleManager.selectSkillID = 0;
		SexSkillData selectSexSkillData = GameDataManager.instance.sexSkillDataBase.sexSkillDataList.Find((SexSkillData data) => data.skillID == 0);
		sexBattleManager.selectSexSkillData = selectSexSkillData;
		SexSkillData sexSkillData = null;
		int skillID = 0;
		sexBattleManager.skillWindowGo.SetActive(value: true);
		sexBattleManager.blackImageGo.SetActive(value: true);
		if (sexBattleManager.skillWindowContentGo.transform.childCount > 0)
		{
			Transform[] array = new Transform[sexBattleManager.skillWindowContentGo.transform.childCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = sexBattleManager.skillWindowContentGo.transform.GetChild(i);
			}
			for (int j = 0; j < array.Length; j++)
			{
				PoolManager.Pools["sexBattlePool"].Despawn(array[j], 0f, sexBattleManager.skillPrefabParent);
			}
		}
		for (int k = 0; k < PlayerSexStatusDataManager.playerUseSexActiveSkillList[0].Count; k++)
		{
			Transform obj = PoolManager.Pools["sexBattlePool"].Spawn(sexBattleManager.skillScrollPrefabGo, sexBattleManager.skillWindowContentGo.transform);
			obj.localScale = new Vector2(1f, 1f);
			skillID = PlayerSexStatusDataManager.playerUseSexActiveSkillList[0][k].skillID;
			sexSkillData = GameDataManager.instance.sexSkillDataBase.sexSkillDataList.Find((SexSkillData data) => data.skillID == skillID);
			ParameterContainer component = obj.GetComponent<ParameterContainer>();
			component.SetInt("skillID", skillID);
			component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = "sexSkill" + skillID;
			int needRechargeTurn = PlayerSexStatusDataManager.playerSexSkillRechargeTurn[0].Find((PlayerSexStatusDataManager.MemberSexSkillReChargeTurn data) => data.skillID == skillID).needRechargeTurn;
			component.GetVariable<UguiTextVariable>("reChargeTurnText").text.text = needRechargeTurn.ToString();
			Sprite sprite = GameDataManager.instance.itemCategoryDataBase.skillCategoryIconDictionary[sexSkillData.skillType.ToString()];
			component.GetVariable<UguiImage>("iconImage").image.sprite = sprite;
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
}
