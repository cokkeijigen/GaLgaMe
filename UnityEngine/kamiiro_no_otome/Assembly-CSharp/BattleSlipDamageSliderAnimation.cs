using System.Collections.Generic;
using Arbor;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class BattleSlipDamageSliderAnimation : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public float animationTime;

	private List<int> playerAfterHpList = new List<int>();

	private List<int> enemyAfterHpList = new List<int>();

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
		List<Slider> list = new List<Slider>();
		List<Slider> list2 = new List<Slider>();
		playerAfterHpList.Clear();
		enemyAfterHpList.Clear();
		list.Clear();
		list2.Clear();
		if (scenarioBattleTurnManager.skillAttackHitDataList.Count > 0)
		{
			for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
			{
				int memberNum = scenarioBattleTurnManager.skillAttackHitDataList[i].memberNum;
				int memberID = scenarioBattleTurnManager.skillAttackHitDataList[i].memberID;
				int attackDamage = scenarioBattleTurnManager.skillAttackHitDataList[i].attackDamage;
				int item = Mathf.Clamp(PlayerStatusDataManager.characterHp[memberID] - attackDamage, 1, 99999);
				playerAfterHpList.Add(item);
				Debug.Log("味方への毒ダメージ／num：" + memberNum + "／ID：" + memberID);
			}
			for (int j = 0; j < scenarioBattleTurnManager.skillAttackHitDataList.Count; j++)
			{
				int memberNum2 = scenarioBattleTurnManager.skillAttackHitDataList[j].memberNum;
				list.Add(utageBattleSceneManager.playerFrameGoList[memberNum2].GetComponent<ParameterContainer>().GetVariable<SliderAndTmpText>("hpGroup").slider);
			}
			for (int k = 0; k < scenarioBattleTurnManager.skillAttackHitDataList.Count; k++)
			{
				list[k].DOValue(playerAfterHpList[k], num).SetEase(Ease.Linear);
			}
		}
		if (scenarioBattleTurnManager.skillAttackHitDataSubList.Count > 0)
		{
			for (int l = 0; l < scenarioBattleTurnManager.skillAttackHitDataSubList.Count; l++)
			{
				int memberNum3 = scenarioBattleTurnManager.skillAttackHitDataSubList[l].memberNum;
				int attackDamage = scenarioBattleTurnManager.skillAttackHitDataSubList[l].attackDamage;
				int item = Mathf.Clamp(PlayerStatusDataManager.enemyHp[memberNum3] - attackDamage, 1, 999999);
				enemyAfterHpList.Add(item);
			}
			for (int m = 0; m < scenarioBattleTurnManager.skillAttackHitDataSubList.Count; m++)
			{
				int memberNum4 = scenarioBattleTurnManager.skillAttackHitDataSubList[m].memberNum;
				list2.Add(utageBattleSceneManager.enemyButtonGoList[memberNum4].GetComponent<ParameterContainer>().GetVariable<SliderAndTmpText>("hpGroup").slider);
			}
			for (int n = 0; n < scenarioBattleTurnManager.skillAttackHitDataSubList.Count; n++)
			{
				list2[n].DOValue(enemyAfterHpList[n], num).SetEase(Ease.Linear);
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
		if (scenarioBattleTurnManager.skillAttackHitDataList.Count > 0)
		{
			for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
			{
				_ = scenarioBattleTurnManager.skillAttackHitDataList[i].memberID;
				int memberNum = scenarioBattleTurnManager.skillAttackHitDataList[i].memberNum;
				sliderAndTmpText = utageBattleSceneManager.playerFrameGoList[memberNum].GetComponent<ParameterContainer>().GetVariable<SliderAndTmpText>("hpGroup");
				sliderAndTmpText.textMeshProUGUI.text = sliderAndTmpText.slider.value.ToString();
			}
		}
	}

	private void InvokeMethod()
	{
		for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
		{
			int memberID = scenarioBattleTurnManager.skillAttackHitDataList[i].memberID;
			PlayerStatusDataManager.characterHp[memberID] = playerAfterHpList[i];
		}
		for (int j = 0; j < scenarioBattleTurnManager.skillAttackHitDataSubList.Count; j++)
		{
			int memberNum = scenarioBattleTurnManager.skillAttackHitDataSubList[j].memberNum;
			PlayerStatusDataManager.enemyHp[memberNum] = enemyAfterHpList[j];
		}
		Transition(stateLink);
	}
}
