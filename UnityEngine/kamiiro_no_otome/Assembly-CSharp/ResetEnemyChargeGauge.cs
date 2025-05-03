using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class ResetEnemyChargeGauge : StateBehaviour
{
	private ParameterContainer parameterContainer;

	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	public Sprite maxSprite;

	public Sprite trueSprite;

	public Sprite falseSprite;

	private int chargeValue;

	private int maxChargeValue;

	private int enemyPartyNum;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		parameterContainer = GetComponent<ParameterContainer>();
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
	}

	public override void OnStateBegin()
	{
		enemyPartyNum = parameterContainer.GetInt("enemyPartyNum");
		chargeValue = 0;
		maxChargeValue = parameterContainer.GetInt("maxChargeNum");
		foreach (GameObject gameObject in parameterContainer.GetGameObjectList("ChargeImageGoList"))
		{
			gameObject.SetActive(value: false);
		}
		foreach (GameObject gameObject2 in parameterContainer.GetGameObjectList("maxEffectGoList"))
		{
			gameObject2.SetActive(value: false);
		}
		for (int i = 0; i < maxChargeValue; i++)
		{
			parameterContainer.GetGameObjectList("ChargeImageGoList")[i].SetActive(value: true);
			parameterContainer.GetGameObjectList("maxEffectGoList")[i].SetActive(value: true);
		}
		if (!PlayerBattleConditionManager.enemyIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == enemyPartyNum).isDead)
		{
			chargeValue = PlayerStatusDataManager.enemyChargeTurnList[enemyPartyNum];
			for (int j = 0; j < maxChargeValue; j++)
			{
				if (j < chargeValue && chargeValue != 0)
				{
					parameterContainer.GetGameObjectList("ChargeImageGoList")[j].GetComponent<Image>().sprite = trueSprite;
				}
				else
				{
					parameterContainer.GetGameObjectList("ChargeImageGoList")[j].GetComponent<Image>().sprite = falseSprite;
				}
			}
		}
		if (chargeValue >= maxChargeValue)
		{
			scenarioBattleSkillManager.isEnemyChargeMax = true;
			for (int k = 0; k < maxChargeValue; k++)
			{
				parameterContainer.GetGameObjectList("ChargeImageGoList")[k].GetComponent<Image>().sprite = maxSprite;
			}
			parameterContainer.GetGameObject("maxEffectGo").SetActive(value: true);
		}
		else
		{
			parameterContainer.GetGameObject("maxEffectGo").SetActive(value: false);
		}
		utageBattleSceneManager.isEnemyGroupSetUp[enemyPartyNum] = true;
		Debug.Log("敵ボタン設定完了／ナンバー：" + enemyPartyNum);
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
