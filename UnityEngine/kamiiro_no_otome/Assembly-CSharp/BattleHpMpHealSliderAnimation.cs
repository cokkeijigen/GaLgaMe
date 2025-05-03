using System.Collections.Generic;
using Arbor;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class BattleHpMpHealSliderAnimation : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public float animationTime;

	private List<int> playerAfterHpList = new List<int>();

	private List<int> playerAfterMpList = new List<int>();

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
		List<Slider> list2 = new List<Slider>();
		playerAfterHpList.Clear();
		playerAfterMpList.Clear();
		if (scenarioBattleTurnManager.skillAttackHitDataList.Count > 0)
		{
			for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
			{
				int healValue = scenarioBattleTurnManager.skillAttackHitDataList[i].healValue;
				int max = PlayerStatusDataManager.characterMaxHp[scenarioBattleTurnManager.skillAttackHitDataList[i].memberID];
				int max2 = PlayerStatusDataManager.characterMaxMp[scenarioBattleTurnManager.skillAttackHitDataList[i].memberID];
				int item = Mathf.Clamp(PlayerStatusDataManager.characterHp[scenarioBattleTurnManager.skillAttackHitDataList[i].memberID] + healValue, 0, max);
				playerAfterHpList.Add(item);
				int item2 = Mathf.Clamp(PlayerStatusDataManager.characterMp[scenarioBattleTurnManager.skillAttackHitDataList[i].memberID] + healValue, 0, max2);
				playerAfterMpList.Add(item2);
			}
			for (int j = 0; j < scenarioBattleTurnManager.skillAttackHitDataList.Count; j++)
			{
				list.Add(utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.skillAttackHitDataList[j].memberNum].GetComponent<ParameterContainer>().GetVariable<SliderAndTmpText>("hpGroup").slider);
				list2.Add(utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.skillAttackHitDataList[j].memberNum].GetComponent<ParameterContainer>().GetVariable<SliderAndTmpText>("mpGroup").slider);
			}
			for (int k = 0; k < scenarioBattleTurnManager.skillAttackHitDataList.Count; k++)
			{
				list[k].DOValue(playerAfterHpList[k], num).SetEase(Ease.Linear);
				list2[k].DOValue(playerAfterMpList[k], num).SetEase(Ease.Linear);
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
			SliderAndTmpText variable = utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.skillAttackHitDataList[i].memberNum].GetComponent<ParameterContainer>().GetVariable<SliderAndTmpText>("hpGroup");
			variable.textMeshProUGUI.text = variable.slider.value.ToString();
			SliderAndTmpText variable2 = utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.skillAttackHitDataList[i].memberNum].GetComponent<ParameterContainer>().GetVariable<SliderAndTmpText>("mpGroup");
			variable2.textMeshProUGUI.text = variable2.slider.value.ToString();
		}
	}

	private void InvokeMethod()
	{
		for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
		{
			int memberID = scenarioBattleTurnManager.skillAttackHitDataList[i].memberID;
			PlayerStatusDataManager.characterHp[memberID] = playerAfterHpList[i];
			PlayerStatusDataManager.characterMp[memberID] = playerAfterMpList[i];
		}
		Transition(stateLink);
	}
}
