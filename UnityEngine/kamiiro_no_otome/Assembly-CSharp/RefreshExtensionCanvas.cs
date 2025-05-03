using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class RefreshExtensionCanvas : StateBehaviour
{
	private InDoorCommandManager inDoorCommandManager;

	private CraftExtensionManager craftExtensionManager;

	private ParameterContainer parameterContainer;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		inDoorCommandManager = GameObject.Find("InDoor Command Manager").GetComponent<InDoorCommandManager>();
		craftExtensionManager = GameObject.Find("Craft Extension Manager").GetComponent<CraftExtensionManager>();
		parameterContainer = GameObject.Find("Extension Window").GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		inDoorCommandManager.SetCarriageImageSprite();
		CraftWorkShopData craftWorkShopData = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"];
		parameterContainer.GetVariableList<UguiTextVariable>("currentLvText")[0].text.text = craftWorkShopData.workShopLv.ToString();
		parameterContainer.GetVariableList<UguiTextVariable>("currentLvText")[1].text.text = craftWorkShopData.workShopToolLv.ToString();
		parameterContainer.GetVariableList<UguiTextVariable>("currentLvText")[2].text.text = craftWorkShopData.furnaceLv.ToString();
		parameterContainer.GetVariableList<UguiTextVariable>("currentLvText")[3].text.text = craftWorkShopData.enableAddOnLv.ToString();
		if (craftWorkShopData.workShopLv >= 4)
		{
			SetCurrentMaxLv(0);
		}
		else
		{
			parameterContainer.GetVariableList<UguiTextVariable>("nextLvText")[0].text.text = (craftWorkShopData.workShopLv + 1).ToString();
			int[] array = craftExtensionManager.CalcExtensionCost(0, craftWorkShopData);
			string text = $"{array[0]:#,0}";
			parameterContainer.GetVariableList<UguiTextVariable>("needMoneyText")[0].text.text = text;
			CheckHaveMoney(0, array[0]);
		}
		if (craftWorkShopData.workShopToolLv >= 4)
		{
			SetCurrentMaxLv(1);
		}
		else
		{
			parameterContainer.GetVariableList<UguiTextVariable>("nextLvText")[1].text.text = (craftWorkShopData.workShopToolLv + 1).ToString();
			int[] array2 = craftExtensionManager.CalcExtensionCost(1, craftWorkShopData);
			string text2 = $"{array2[0]:#,0}";
			parameterContainer.GetVariableList<UguiTextVariable>("needMoneyText")[1].text.text = text2;
			CheckHaveMoney(1, array2[0]);
		}
		if (craftWorkShopData.furnaceLv >= 4)
		{
			SetCurrentMaxLv(2);
		}
		else
		{
			parameterContainer.GetVariableList<UguiTextVariable>("nextLvText")[2].text.text = (craftWorkShopData.furnaceLv + 1).ToString();
			int[] array3 = craftExtensionManager.CalcExtensionCost(2, craftWorkShopData);
			string text3 = $"{array3[0]:#,0}";
			parameterContainer.GetVariableList<UguiTextVariable>("needMoneyText")[2].text.text = text3;
			CheckHaveMoney(2, array3[0]);
		}
		if (craftWorkShopData.enableAddOnLv > 0)
		{
			craftExtensionManager.addOnLvTextGroup[0].SetActive(value: true);
			craftExtensionManager.addOnLvTextGroup[1].SetActive(value: false);
			if (craftWorkShopData.enableAddOnLv >= 4)
			{
				SetCurrentMaxLv(3);
			}
			else
			{
				parameterContainer.GetVariableList<UguiTextVariable>("nextLvText")[3].text.text = (craftWorkShopData.enableAddOnLv + 1).ToString();
				int[] array4 = craftExtensionManager.CalcExtensionCost(3, craftWorkShopData);
				string text4 = $"{array4[0]:#,0}";
				parameterContainer.GetVariableList<UguiTextVariable>("needMoneyText")[3].text.text = text4;
				CheckHaveMoney(3, array4[0]);
			}
		}
		else
		{
			craftExtensionManager.addOnLvTextGroup[0].SetActive(value: false);
			craftExtensionManager.addOnLvTextGroup[1].SetActive(value: true);
		}
		craftExtensionManager.characterTalkTextLoc.Term = "extensionBalloonCategorySelect";
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

	private void CheckHaveMoney(int index, int cost)
	{
		craftExtensionManager.extensionButtonTextLoc[index].Term = "buttonExtensionApply";
		SetCurrentNoMaxLv(index);
		if (PlayerDataManager.playerHaveMoney >= cost)
		{
			craftExtensionManager.extensionButtonArray[index].alpha = 1f;
			craftExtensionManager.extensionButtonArray[index].interactable = true;
		}
		else
		{
			craftExtensionManager.extensionButtonArray[index].alpha = 0.5f;
			craftExtensionManager.extensionButtonArray[index].interactable = false;
		}
	}

	private void SetCurrentNoMaxLv(int index)
	{
		parameterContainer.GetVariableList<UguiTextVariable>("nextLvText")[index].text.gameObject.SetActive(value: true);
		parameterContainer.GetVariableList<I2LocalizeComponent>("arrowTextLoc")[index].localize.Term = "nextArrow";
	}

	private void SetCurrentMaxLv(int index)
	{
		parameterContainer.GetVariableList<UguiTextVariable>("nextLvText")[index].text.gameObject.SetActive(value: false);
		parameterContainer.GetVariableList<I2LocalizeComponent>("arrowTextLoc")[index].localize.Term = "facilityMaxLv";
		parameterContainer.GetVariableList<UguiTextVariable>("needMoneyText")[index].text.text = "------";
		craftExtensionManager.extensionButtonArray[index].alpha = 0.5f;
		craftExtensionManager.extensionButtonArray[index].interactable = false;
		craftExtensionManager.extensionButtonTextLoc[index].Term = "buttonExtensionMaxLv";
	}
}
