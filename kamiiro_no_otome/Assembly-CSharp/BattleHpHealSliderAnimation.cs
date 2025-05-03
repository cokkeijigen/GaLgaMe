using System.Collections.Generic;
using Arbor;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class BattleHpHealSliderAnimation : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public float animationTime;

	private List<int> playerAfterHpList = new List<int>();

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
		List<Slider> list = new List<Slider>();
		playerAfterHpList.Clear();
		list.Clear();
		for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
		{
			if (scenarioBattleTurnManager.skillAttackHitDataList[i].isHealHit)
			{
				int healValue = scenarioBattleTurnManager.skillAttackHitDataList[i].healValue;
				int max = PlayerStatusDataManager.characterMaxHp[scenarioBattleTurnManager.skillAttackHitDataList[i].memberID];
				int item = Mathf.Clamp(PlayerStatusDataManager.characterHp[scenarioBattleTurnManager.skillAttackHitDataList[i].memberID] + healValue, 0, max);
				playerAfterHpList.Add(item);
			}
		}
		int j;
		for (j = 0; j < scenarioBattleTurnManager.skillAttackHitDataList.Count; j++)
		{
			int memberNum = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == scenarioBattleTurnManager.skillAttackHitDataList[j].memberID).memberNum;
			list.Add(utageBattleSceneManager.playerFrameGoList[memberNum].GetComponent<ParameterContainer>().GetVariable<SliderAndTmpText>("hpGroup").slider);
		}
		for (int k = 0; k < scenarioBattleTurnManager.skillAttackHitDataList.Count; k++)
		{
			if (scenarioBattleTurnManager.skillAttackHitDataList[k].isHealHit)
			{
				list[k].DOValue(playerAfterHpList[k], num).SetEase(Ease.Linear);
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
		for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
		{
			if (scenarioBattleTurnManager.skillAttackHitDataList[i].isHealHit)
			{
				int memberNum = scenarioBattleTurnManager.skillAttackHitDataList[i].memberNum;
				SliderAndTmpText variable = utageBattleSceneManager.playerFrameGoList[memberNum].GetComponent<ParameterContainer>().GetVariable<SliderAndTmpText>("hpGroup");
				variable.textMeshProUGUI.text = variable.slider.value.ToString();
			}
		}
	}

	private void InvokeMethod()
	{
		for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
		{
			if (scenarioBattleTurnManager.skillAttackHitDataList[i].isHealHit)
			{
				int memberID = scenarioBattleTurnManager.skillAttackHitDataList[i].memberID;
				PlayerStatusDataManager.characterHp[memberID] = playerAfterHpList[i];
			}
		}
		Transition(stateLink);
	}
}
