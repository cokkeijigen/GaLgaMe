using System.Linq;
using Arbor;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class BattleAttackDamageSliderAnimation : StateBehaviour
{
	public enum Type
	{
		player,
		enemy
	}

	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public Type type;

	public FlexibleInt damageValue;

	private int afterHP;

	public float animationTime;

	private SliderAndTmpText sliderAndTmpText;

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
		switch (type)
		{
		case Type.player:
		{
			ScenarioBattleData scenarioBattleData2 = GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData data) => data.scenarioName == PlayerNonSaveDataManager.resultScenarioName);
			if (scenarioBattleData2.isDefeatBattle || scenarioBattleData2.isEventBattle)
			{
				afterHP = Mathf.Clamp(PlayerStatusDataManager.enemyHp[scenarioBattleTurnManager.playerTargetNum] - damageValue.value, 1, 999999);
				Debug.Log("負け戦闘bool：" + scenarioBattleData2.isDefeatBattle + "／イベント戦闘bool" + scenarioBattleData2.isEventBattle);
			}
			else
			{
				afterHP = Mathf.Clamp(PlayerStatusDataManager.enemyHp[scenarioBattleTurnManager.playerTargetNum] - damageValue.value, 0, 999999);
			}
			utageBattleSceneManager.enemyButtonGoList[scenarioBattleTurnManager.playerTargetNum].GetComponent<ParameterContainer>().GetVariable<SliderAndTmpText>("hpGroup").slider.DOValue(afterHP, num).SetEase(Ease.Linear);
			break;
		}
		case Type.enemy:
		{
			int id = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.enemyTargetNum).memberID;
			ScenarioBattleData scenarioBattleData = GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData data) => data.scenarioName == PlayerNonSaveDataManager.resultScenarioName);
			if (scenarioBattleData.isVictoryBattle || scenarioBattleData.supportBattleCharacterID == id)
			{
				afterHP = Mathf.Clamp(PlayerStatusDataManager.characterHp[id] - damageValue.value, 1, 999999);
			}
			else
			{
				afterHP = Mathf.Clamp(PlayerStatusDataManager.characterHp[id] - damageValue.value, 0, 999999);
			}
			int index2 = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == id);
			if ((from ano in PlayerBattleConditionManager.playerBuffCondition[index2].Select((PlayerBattleConditionManager.MemberBuffCondition con, int index) => new
				{
					Content = con,
					Index = index
				})
				where ano.Content.type == "endurance"
				select ano.Index).ToList().Any() && afterHP <= 0)
			{
				afterHP = Mathf.Clamp(PlayerStatusDataManager.characterHp[id] - damageValue.value, 1, 999999);
				Debug.Log("忍耐スキルがある／ID：" + id + "／index：" + index2);
				PlayerBattleConditionManager.playerBuffCondition[index2].Find((PlayerBattleConditionManager.MemberBuffCondition data) => data.type == "endurance").continutyTurn = 0;
			}
			utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.enemyTargetNum].GetComponent<ParameterContainer>().GetVariable<SliderAndTmpText>("hpGroup").slider.DOValue(afterHP, num).SetEase(Ease.Linear);
			break;
		}
		}
		float time = num + 0.1f;
		Invoke("InvokeMethod", time);
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
		Type type = this.type;
		if (type != 0 && type == Type.enemy)
		{
			sliderAndTmpText = utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.enemyTargetNum].GetComponent<ParameterContainer>().GetVariable<SliderAndTmpText>("hpGroup");
			sliderAndTmpText.textMeshProUGUI.text = sliderAndTmpText.slider.value.ToString();
		}
	}

	private void InvokeMethod()
	{
		switch (type)
		{
		case Type.player:
			PlayerStatusDataManager.enemyHp[scenarioBattleTurnManager.playerTargetNum] = afterHP;
			break;
		case Type.enemy:
		{
			int memberID = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.enemyTargetNum).memberID;
			PlayerStatusDataManager.characterHp[memberID] = afterHP;
			break;
		}
		}
		Transition(stateLink);
	}
}
