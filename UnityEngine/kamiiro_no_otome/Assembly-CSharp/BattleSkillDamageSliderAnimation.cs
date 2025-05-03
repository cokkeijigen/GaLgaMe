using System.Collections.Generic;
using System.Linq;
using Arbor;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class BattleSkillDamageSliderAnimation : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public float animationTime;

	private List<int> afterHpList = new List<int>();

	private SliderAndTmpText sliderAndTmpText;

	private List<Slider> hpSliderList = new List<Slider>();

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
	}

	public override void OnStateBegin()
	{
		float num = animationTime / (float)utageBattleSceneManager.battleSpeed;
		afterHpList.Clear();
		hpSliderList.Clear();
		if (scenarioBattleTurnManager.isUseSkillPlayer)
		{
			for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
			{
				int attackDamage = scenarioBattleTurnManager.skillAttackHitDataList[i].attackDamage;
				int item = ((!GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData data) => data.scenarioName == PlayerNonSaveDataManager.resultScenarioName).isDefeatBattle) ? Mathf.Clamp(PlayerStatusDataManager.enemyHp[scenarioBattleTurnManager.skillAttackHitDataList[i].memberNum] - attackDamage, 0, 999999) : Mathf.Clamp(PlayerStatusDataManager.enemyHp[scenarioBattleTurnManager.skillAttackHitDataList[i].memberNum] - attackDamage, 1, 999999));
				afterHpList.Add(item);
			}
			for (int j = 0; j < scenarioBattleTurnManager.skillAttackHitDataList.Count; j++)
			{
				hpSliderList.Add(utageBattleSceneManager.enemyButtonGoList[scenarioBattleTurnManager.skillAttackHitDataList[j].memberNum].GetComponent<ParameterContainer>().GetVariable<SliderAndTmpText>("hpGroup").slider);
			}
			for (int k = 0; k < scenarioBattleTurnManager.skillAttackHitDataList.Count; k++)
			{
				hpSliderList[k].DOValue(afterHpList[k], num).SetEase(Ease.Linear);
			}
			float time = animationTime + 0.1f;
			Invoke("InvokeMethod", time);
			return;
		}
		for (int l = 0; l < scenarioBattleTurnManager.skillAttackHitDataList.Count; l++)
		{
			int attackDamage = scenarioBattleTurnManager.skillAttackHitDataList[l].attackDamage;
			int id = scenarioBattleTurnManager.skillAttackHitDataList[l].memberID;
			ScenarioBattleData scenarioBattleData = GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData data) => data.scenarioName == PlayerNonSaveDataManager.resultScenarioName);
			int item = ((!scenarioBattleData.isVictoryBattle && scenarioBattleData.supportBattleCharacterID != id) ? Mathf.Clamp(PlayerStatusDataManager.characterHp[scenarioBattleTurnManager.skillAttackHitDataList[l].memberID] - attackDamage, 0, 99999) : Mathf.Clamp(PlayerStatusDataManager.characterHp[scenarioBattleTurnManager.skillAttackHitDataList[l].memberID] - attackDamage, 1, 99999));
			int index2 = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == id);
			if ((from ano in PlayerBattleConditionManager.playerBuffCondition[index2].Select((PlayerBattleConditionManager.MemberBuffCondition con, int index) => new
				{
					Content = con,
					Index = index
				})
				where ano.Content.type == "endurance"
				select ano.Index).ToList().Any() && item <= 0)
			{
				item = Mathf.Clamp(PlayerStatusDataManager.characterHp[scenarioBattleTurnManager.skillAttackHitDataList[l].memberID] - attackDamage, 1, 99999);
				Debug.Log("忍耐スキルがある／ID：" + id + "／index：" + index2);
				PlayerBattleConditionManager.playerBuffCondition[index2].Find((PlayerBattleConditionManager.MemberBuffCondition data) => data.type == "endurance").continutyTurn = 0;
			}
			afterHpList.Add(item);
		}
		for (int m = 0; m < scenarioBattleTurnManager.skillAttackHitDataList.Count; m++)
		{
			hpSliderList.Add(utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.skillAttackHitDataList[m].memberNum].GetComponent<ParameterContainer>().GetVariable<SliderAndTmpText>("hpGroup").slider);
		}
		for (int n = 0; n < scenarioBattleTurnManager.skillAttackHitDataList.Count; n++)
		{
			hpSliderList[n].DOValue(afterHpList[n], num).SetEase(Ease.Linear);
		}
		float time2 = num + 0.1f;
		Invoke("InvokeMethod", time2);
	}

	public override void OnStateEnd()
	{
		SliderSyncText();
	}

	public override void OnStateUpdate()
	{
		SliderSyncText();
	}

	public override void OnStateLateUpdate()
	{
	}

	private void SliderSyncText()
	{
		if (scenarioBattleTurnManager.isUseSkillPlayer)
		{
			return;
		}
		if (scenarioBattleTurnManager.isAllTargetSkill)
		{
			for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
			{
				sliderAndTmpText = utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.skillAttackHitDataList[i].memberNum].GetComponent<ParameterContainer>().GetVariable<SliderAndTmpText>("hpGroup");
				sliderAndTmpText.textMeshProUGUI.text = sliderAndTmpText.slider.value.ToString();
			}
		}
		else
		{
			sliderAndTmpText = utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.enemyTargetNum].GetComponent<ParameterContainer>().GetVariable<SliderAndTmpText>("hpGroup");
			sliderAndTmpText.textMeshProUGUI.text = sliderAndTmpText.slider.value.ToString();
		}
	}

	private void InvokeMethod()
	{
		for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
		{
			if (scenarioBattleTurnManager.isUseSkillPlayer)
			{
				PlayerStatusDataManager.enemyHp[scenarioBattleTurnManager.skillAttackHitDataList[i].memberNum] = afterHpList[i];
				continue;
			}
			int memberID = scenarioBattleTurnManager.skillAttackHitDataList[i].memberID;
			PlayerStatusDataManager.characterHp[memberID] = afterHpList[i];
		}
		Transition(stateLink);
	}
}
