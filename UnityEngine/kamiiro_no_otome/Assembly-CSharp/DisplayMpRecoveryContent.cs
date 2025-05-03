using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class DisplayMpRecoveryContent : StateBehaviour
{
	private CraftExtensionManager craftExtensionManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftExtensionManager = GameObject.Find("Craft Extension Manager").GetComponent<CraftExtensionManager>();
	}

	public override void OnStateBegin()
	{
		ItemWeaponData itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftExtensionManager.clickedItemID);
		ParameterContainer component = craftExtensionManager.itemSummaryWindowGo.GetComponent<ParameterContainer>();
		component.GetVariable<UguiImage>("iconImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemWeaponData.category.ToString()];
		string text = itemWeaponData.category.ToString() + itemWeaponData.itemID;
		component.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = text;
		component.GetVariable<UguiImage>("itemImage").image.sprite = itemWeaponData.itemSprite;
		component.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = text + "_summary";
		IList<I2LocalizeComponent> variableList = component.GetVariableList<I2LocalizeComponent>("statusTypeLoc");
		IList<UguiTextVariable> variableList2 = component.GetVariableList<UguiTextVariable>("statusPowerText");
		variableList[0].localize.Term = "statusAttack";
		variableList2[0].text.text = itemWeaponData.attackPower.ToString();
		variableList[1].localize.Term = "statusMagicAttack";
		variableList2[1].text.text = itemWeaponData.magicAttackPower.ToString();
		variableList[2].localize.Term = "statusAccuracy";
		variableList2[2].text.text = itemWeaponData.accuracy.ToString();
		variableList[3].localize.Term = "statusCritical";
		variableList2[3].text.text = itemWeaponData.critical.ToString();
		variableList[4].localize.Term = "statusItemMp";
		variableList2[4].text.text = itemWeaponData.weaponMp.ToString();
		variableList[5].localize.Term = "empty";
		variableList2[5].text.text = "";
		variableList[6].localize.Term = "statusFactorSlot";
		variableList2[6].text.text = itemWeaponData.factorSlot.ToString();
		variableList[7].localize.Term = "statusFactorLimitNum";
		variableList2[7].text.text = itemWeaponData.factorHaveLimit.ToString();
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
