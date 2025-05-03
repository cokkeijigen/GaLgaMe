using System.Collections.Generic;
using Arbor;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class BattleMpHealSliderAnimation : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public float animationTime;

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
		playerAfterMpList.Clear();
		list.Clear();
		if (scenarioBattleTurnManager.skillAttackHitDataList.Count > 0)
		{
			for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
			{
				int healValue = scenarioBattleTurnManager.skillAttackHitDataList[i].healValue;
				int max = PlayerStatusDataManager.characterMaxMp[scenarioBattleTurnManager.skillAttackHitDataList[i].memberID];
				int item = Mathf.Clamp(PlayerStatusDataManager.characterMp[scenarioBattleTurnManager.skillAttackHitDataList[i].memberID] + healValue, 0, max);
				playerAfterMpList.Add(item);
			}
			for (int j = 0; j < scenarioBattleTurnManager.skillAttackHitDataList.Count; j++)
			{
				list.Add(utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.skillAttackHitDataList[j].memberNum].GetComponent<ParameterContainer>().GetVariable<SliderAndTmpText>("mpGroup").slider);
			}
			for (int k = 0; k < scenarioBattleTurnManager.skillAttackHitDataList.Count; k++)
			{
				list[k].DOValue(playerAfterMpList[k], num).SetEase(Ease.Linear);
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
			SliderAndTmpText variable = utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.skillAttackHitDataList[i].memberNum].GetComponent<ParameterContainer>().GetVariable<SliderAndTmpText>("mpGroup");
			variable.textMeshProUGUI.text = variable.slider.value.ToString();
		}
	}

	private void InvokeMethod()
	{
		for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
		{
			int memberID = scenarioBattleTurnManager.skillAttackHitDataList[i].memberID;
			PlayerStatusDataManager.characterMp[memberID] = playerAfterMpList[i];
		}
		Transition(stateLink);
	}
}
