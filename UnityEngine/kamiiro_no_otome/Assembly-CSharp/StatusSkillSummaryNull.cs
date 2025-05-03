using Arbor;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class StatusSkillSummaryNull : StateBehaviour
{
	private StatusManager statusManager;

	private StatusSkillViewManager statusSkillViewManager;

	private ParameterContainer summaryParameter;

	private GameObject skillSummaryWindow;

	private Localize NameTextLoc;

	private Localize skillSummaryText;

	private Image skillImage;

	private Localize[] panel_TypeTextLoc = new Localize[8];

	private Text[] panel_PowerText = new Text[8];

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
		statusSkillViewManager = GameObject.Find("Skill View Manager").GetComponent<StatusSkillViewManager>();
		skillSummaryWindow = statusManager.skillViewArray[1];
		summaryParameter = skillSummaryWindow.GetComponent<ParameterContainer>();
		NameTextLoc = summaryParameter.GetVariable<I2LocalizeComponent>("skillNameTextLoc").localize;
		skillSummaryText = summaryParameter.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize;
		skillImage = summaryParameter.GetVariable<UguiImage>("skillImage").image;
		for (int i = 0; i < 8; i++)
		{
			panel_TypeTextLoc[i] = summaryParameter.GetVariableList<I2LocalizeComponent>("statusTypeLoc")[i].localize;
			panel_PowerText[i] = summaryParameter.GetComponent<ParameterContainer>().GetVariableList<UguiTextVariable>("statusPowerText")[i].text;
		}
	}

	public override void OnStateBegin()
	{
		NameTextLoc.Term = "noStatus";
		skillImage.sprite = statusManager.noItemImageSprite;
		skillSummaryText.Term = "noSelectItemSummary";
		panel_PowerText[0].GetComponent<Localize>().Term = "noStatus";
		panel_PowerText[1].GetComponent<Localize>().Term = "noStatus";
		panel_PowerText[2].text = " ";
		panel_PowerText[3].GetComponent<Localize>().Term = "noStatus";
		panel_PowerText[4].GetComponent<Localize>().Term = "noStatus";
		panel_PowerText[5].GetComponent<Localize>().Term = "noStatus";
		panel_PowerText[6].text = " ";
		panel_PowerText[7].text = " ";
		statusSkillViewManager.skillLearnButtonLoc.Term = "buttonLearn";
		statusSkillViewManager.learnCostGroup.SetActive(value: false);
		statusSkillViewManager.skillLearnButtonGO.GetComponent<CanvasGroup>().alpha = 0.5f;
		statusSkillViewManager.skillLearnButtonGO.GetComponent<CanvasGroup>().interactable = false;
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
