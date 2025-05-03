using Arbor;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class StatusFactorSummaryRefresh : StateBehaviour
{
	private StatusManager statusManager;

	private StatusCustomManager statusCustomManager;

	private ParameterContainer summaryParameter;

	public InputSlotBool inputSlotBool;

	private GameObject factorSummaryWindow;

	private Localize[] panel_TypeText = new Localize[11];

	private Text[] panel_PowerText = new Text[11];

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
		statusCustomManager = GameObject.Find("Status Custom Manager").GetComponent<StatusCustomManager>();
		factorSummaryWindow = statusCustomManager.customWindowArray[1];
		summaryParameter = factorSummaryWindow.GetComponent<ParameterContainer>();
		for (int i = 0; i < 11; i++)
		{
			panel_TypeText[i] = summaryParameter.GetComponent<ParameterContainer>().GetVariableList<I2LocalizeComponent>("typeTextLoc")[i].localize;
			panel_PowerText[i] = summaryParameter.GetComponent<ParameterContainer>().GetVariableList<UguiTextVariable>("factorPowerText")[i].text;
		}
	}

	public override void OnStateBegin()
	{
		int[] resultTotalPowerArray = new int[11];
		int[] resultTotalPowerLimitArray = new int[11];
		bool[] resultIsAddPercentArray = new bool[11];
		bool value = false;
		inputSlotBool.GetValue(ref value);
		if (value)
		{
			PlayerEquipDataManager.CalcWeaponEquipFactorTotalPower(statusCustomManager.tempEquipFactorList, ref resultTotalPowerArray, ref resultTotalPowerLimitArray, ref resultIsAddPercentArray);
			for (int i = 0; i < 11; i++)
			{
				panel_TypeText[i].Term = statusCustomManager.typeTextLocWeaponArray[i];
			}
		}
		else
		{
			PlayerEquipDataManager.CalcArmorEquipFactorTotalPower(statusCustomManager.tempEquipFactorList, ref resultTotalPowerArray, ref resultTotalPowerLimitArray, ref resultIsAddPercentArray);
			for (int j = 0; j < 11; j++)
			{
				panel_TypeText[j].Term = statusCustomManager.typeTextLocArmorArray[j];
			}
		}
		for (int k = 0; k < 11; k++)
		{
			if (resultTotalPowerArray[k] >= resultTotalPowerLimitArray[k])
			{
				panel_PowerText[k].color = Color.yellow;
			}
			else
			{
				panel_PowerText[k].color = Color.white;
			}
			if (resultIsAddPercentArray[k])
			{
				panel_PowerText[k].text = resultTotalPowerArray[k] + "%";
			}
			else
			{
				panel_PowerText[k].text = resultTotalPowerArray[k].ToString();
			}
		}
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
