using Arbor;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class ExtensionCraftLvCheck : StateBehaviour
{
	private CraftExtensionManager craftExtensionManager;

	private ParameterContainer parameterContainer;

	private Slider expSlider;

	private TextMeshProUGUI[] expTextArray = new TextMeshProUGUI[2];

	private bool isInitialize;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftExtensionManager = GameObject.Find("Craft Extension Manager").GetComponent<CraftExtensionManager>();
		parameterContainer = craftExtensionManager.craftLvParameterContainer;
	}

	public override void OnStateBegin()
	{
		int playerCraftExp = PlayerCraftStatusManager.playerCraftExp;
		if (PlayerCraftStatusManager.playerCraftLv < 10)
		{
			_ = GameDataManager.instance.needExpDataBase.needCraftLvExpList[PlayerCraftStatusManager.playerCraftLv];
		}
		if (!craftExtensionManager.isAllMpRecovery)
		{
			playerCraftExp += 30;
		}
		else
		{
			int num = craftExtensionManager.needAllMpRecoveryPowderNum * 30;
			playerCraftExp += num;
		}
		playerCraftExp = (PlayerCraftStatusManager.playerCraftExp = Mathf.Clamp(playerCraftExp, 0, 99999));
		expSlider = parameterContainer.GetVariable<SliderAndTmpMaxTextVariable>("craftLvExpSlider").slider;
		expTextArray[0] = parameterContainer.GetVariable<SliderAndTmpMaxTextVariable>("craftLvExpSlider").textMeshProUGUI1;
		expTextArray[1] = parameterContainer.GetVariable<SliderAndTmpMaxTextVariable>("craftLvExpSlider").textMeshProUGUI2;
		expSlider.DOValue(playerCraftExp, 0.3f).OnComplete(delegate
		{
			CompleteMethod();
		});
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		expTextArray[0].text = expSlider.value.ToString();
		if (expSlider.value >= expSlider.maxValue && PlayerCraftStatusManager.playerCraftLv < 10)
		{
			PlayerCraftStatusManager.playerCraftLv++;
			int num = GameDataManager.instance.needExpDataBase.needCraftLvExpList[PlayerCraftStatusManager.playerCraftLv - 1];
			int num2 = GameDataManager.instance.needExpDataBase.needCraftLvExpList[PlayerCraftStatusManager.playerCraftLv];
			expSlider.minValue = num;
			expSlider.maxValue = num2;
			expTextArray[1].text = num2.ToString();
			parameterContainer.GetVariable<TmpText>("craftLvNumText").textMeshProUGUI.text = PlayerCraftStatusManager.playerCraftLv.ToString();
		}
	}

	public override void OnStateLateUpdate()
	{
	}

	private void CompleteMethod()
	{
		Transition(stateLink);
	}
}
