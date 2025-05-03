using Arbor;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class ResetEnemyButton : StateBehaviour
{
	private ParameterContainer parameterContainer;

	private UtageBattleSceneManager utageBattleSceneManager;

	public Localize enemyNameTerm;

	public Slider hpSlider;

	public Image enemyThumnailImage;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		parameterContainer = GetComponent<ParameterContainer>();
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
	}

	public override void OnStateBegin()
	{
		enemyNameTerm.Term = "enemy" + parameterContainer.GetInt("enemyID");
		hpSlider.maxValue = PlayerStatusDataManager.enemyMaxHp[parameterContainer.GetInt("enemyPartyNum")];
		enemyThumnailImage.sprite = utageBattleSceneManager.enemyButtonSprite[parameterContainer.GetInt("enemyPartyNum")];
		ResetBuffIcon();
		hpSlider.value = PlayerStatusDataManager.enemyHp[parameterContainer.GetInt("enemyPartyNum")];
		CanvasGroup component = GetComponent<CanvasGroup>();
		component.alpha = 1f;
		component.interactable = true;
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

	private void ResetBuffIcon()
	{
		foreach (GameObject gameObject in parameterContainer.GetGameObjectList("buffImageGoList"))
		{
			gameObject.SetActive(value: false);
		}
		foreach (GameObject gameObject2 in parameterContainer.GetGameObjectList("badStateImageGoList"))
		{
			gameObject2.SetActive(value: false);
		}
	}
}
