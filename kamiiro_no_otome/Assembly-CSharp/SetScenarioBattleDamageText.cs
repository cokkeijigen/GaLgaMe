using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using I2.Loc;
using PathologicalGames;
using TMPro;
using UnityEngine;

[AddComponentMenu("")]
public class SetScenarioBattleDamageText : StateBehaviour
{
	public enum Type
	{
		player,
		enemy
	}

	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public Type type;

	public string textSituation;

	public float animationTime;

	private int targetID;

	public FlexibleInt damageValue;

	private int afterHP;

	private Transform poolGO;

	public float despawnTime;

	public float shakeTime;

	public float shakePower;

	public int shakeCount;

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
		switch (type)
		{
		case Type.player:
			targetID = PlayerStatusDataManager.enemyMember[scenarioBattleTurnManager.playerTargetNum];
			utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = "enemy" + targetID;
			break;
		case Type.enemy:
			targetID = PlayerStatusDataManager.playerPartyMember[scenarioBattleTurnManager.enemyTargetNum];
			utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = "character" + targetID;
			break;
		}
		utageBattleSceneManager.battleTextArray3[2].GetComponent<TextMeshProUGUI>().text = damageValue.value.ToString();
		utageBattleSceneManager.battleTextGroupArray[2].SetActive(value: true);
		utageBattleSceneManager.battleTextArray3[0].SetActive(value: true);
		utageBattleSceneManager.battleTextArray3[1].SetActive(value: true);
		utageBattleSceneManager.battleTextArray3[2].SetActive(value: true);
		utageBattleSceneManager.battleTextArray3[3].SetActive(value: true);
		switch (type)
		{
		case Type.player:
		{
			Transform transform2 = utageBattleSceneManager.enemyImageGoList[scenarioBattleTurnManager.playerTargetNum].transform;
			utageBattleSceneManager.damagePointRect[0].position = new Vector3(transform2.position.x, 0f, 0f);
			poolGO = PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[0], utageBattleSceneManager.damagePointRect[0]);
			MasterAudio.PlaySound("SeAttackSword", 1f, null, 0f, null, null);
			utageBattleSceneManager.enemyImageGoList[scenarioBattleTurnManager.playerTargetNum].GetComponent<RectTransform>().DOShakeAnchorPos(shakeTime, shakePower, shakeCount, 10f, snapping: false, fadeOut: false);
			afterHP = Mathf.Clamp(PlayerStatusDataManager.enemyHp[scenarioBattleTurnManager.playerTargetNum] - damageValue.value, 0, 999999);
			utageBattleSceneManager.enemyButtonGoList[scenarioBattleTurnManager.playerTargetNum].GetComponent<ParameterContainer>().GetVariable<SliderAndTmpText>("hpGroup").slider.DOValue(afterHP, animationTime).SetEase(Ease.Linear).OnComplete(delegate
			{
				PlayerStatusDataManager.enemyHp[scenarioBattleTurnManager.playerTargetNum] = afterHP;
				Transition(stateLink);
			});
			break;
		}
		case Type.enemy:
		{
			Transform transform = utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.enemyTargetNum].transform;
			utageBattleSceneManager.damagePointRect[0].position = new Vector3(transform.position.x, -2f, 0f);
			poolGO = PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[1], utageBattleSceneManager.damagePointRect[0]);
			MasterAudio.PlaySound("SeAttackDageki", 1f, null, 0f, null, null);
			utageBattleSceneManager.battleCanvas.transform.DOShakePosition(shakeTime, shakePower, shakeCount, 10f, snapping: false, fadeOut: false);
			afterHP = Mathf.Clamp(PlayerStatusDataManager.characterHp[scenarioBattleTurnManager.enemyTargetNum] - damageValue.value, 0, 999999);
			utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.enemyTargetNum].GetComponent<ParameterContainer>().GetVariable<SliderAndTmpText>("hpGroup").slider.DOValue(afterHP, animationTime).SetEase(Ease.Linear).OnComplete(delegate
			{
				PlayerStatusDataManager.characterHp[scenarioBattleTurnManager.enemyTargetNum] = afterHP;
				Transition(stateLink);
			});
			break;
		}
		}
		poolGO.GetComponent<TextMeshProUGUI>().text = damageValue.value.ToString();
		poolGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
		PoolManager.Pools["BattleEffect"].Despawn(poolGO, despawnTime, utageBattleSceneManager.poolManagerGO);
	}

	public override void OnStateEnd()
	{
		Type type = this.type;
		if (type != 0 && type == Type.enemy)
		{
			sliderAndTmpText = utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.enemyTargetNum].GetComponent<ParameterContainer>().GetVariable<SliderAndTmpText>("hpGroup");
			sliderAndTmpText.textMeshProUGUI.text = sliderAndTmpText.slider.value.ToString();
		}
	}

	public override void OnStateUpdate()
	{
		Type type = this.type;
		if (type != 0 && type == Type.enemy)
		{
			sliderAndTmpText = utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.enemyTargetNum].GetComponent<ParameterContainer>().GetVariable<SliderAndTmpText>("hpGroup");
			sliderAndTmpText.textMeshProUGUI.text = sliderAndTmpText.slider.value.ToString();
		}
	}

	public override void OnStateLateUpdate()
	{
	}

	private void InvokeMethod()
	{
		Transition(stateLink);
	}
}
