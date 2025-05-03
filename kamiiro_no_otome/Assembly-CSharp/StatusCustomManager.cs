using System.Collections.Generic;
using Arbor;
using I2.Loc;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class StatusCustomManager : SerializedMonoBehaviour
{
	private StatusManager statusManager;

	private StatusCustomManager statusCustomManager;

	private SexStatusManager sexStatusManager;

	public ArborFSM skillCustomFSM;

	public ArborFSM factorCsutomFSM;

	public GameObject statusOverlayCanvas;

	public GameObject[] customWindowArray;

	public GameObject overlayBlackImageGo;

	public GameObject statusCustomContentGo;

	public GameObject[] customScrollSummaryArray;

	public Localize headerTextLoc;

	public string[] typeTextLocWeaponArray;

	public string[] typeTextLocArmorArray;

	public CanvasGroup applyButtonCanvasGroup;

	public int[] skillSolotNumArray = new int[2];

	public Text[] skillSlotNumTextAray;

	public GameObject[] customScrollPrefabArray;

	public List<int> tempEquipSkillList = new List<int>();

	public List<HaveFactorData> tempEquipFactorList = new List<HaveFactorData>();

	public HaveFactorData tempHaveFactorData;

	public int clickFactorId;

	public int clickFactorUniqueId;

	public bool tempIsEquip;

	public int customScrollContentIndex;

	public string selectCustomCanvasName;

	private void Awake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
		statusCustomManager = GameObject.Find("Status Custom Manager").GetComponentInParent<StatusCustomManager>();
		sexStatusManager = GameObject.Find("Sex Status Manager").GetComponent<SexStatusManager>();
	}

	public void PushStatusCustomButton(string type)
	{
		statusOverlayCanvas.SetActive(value: true);
		overlayBlackImageGo.SetActive(value: true);
		sexStatusManager.sexScheduleWindowGo.SetActive(value: false);
		statusCustomManager.overlayBlackImageGo.transform.SetSiblingIndex(0);
		selectCustomCanvasName = type;
		if (!(type == "factor"))
		{
			if (type == "skill")
			{
				headerTextLoc.Term = "headerCustomSkill";
				customWindowArray[0].SetActive(value: true);
				customWindowArray[1].SetActive(value: false);
				customWindowArray[2].SetActive(value: true);
				customScrollSummaryArray[0].SetActive(value: false);
				customScrollSummaryArray[1].SetActive(value: true);
				skillCustomFSM.SendTrigger("OpenSkillCustomCanvas");
			}
		}
		else
		{
			headerTextLoc.Term = "headerCustomFactor";
			customWindowArray[0].SetActive(value: true);
			customWindowArray[1].SetActive(value: true);
			customWindowArray[2].SetActive(value: false);
			customScrollSummaryArray[0].SetActive(value: true);
			customScrollSummaryArray[1].SetActive(value: false);
			factorCsutomFSM.SendTrigger("OpenFactorCustomCanvas");
		}
	}

	public void PushStatusCustomCancelButton()
	{
		statusOverlayCanvas.SetActive(value: false);
	}

	public void PushStatusCustomApplyButton()
	{
		string text = selectCustomCanvasName;
		if (!(text == "factor"))
		{
			if (text == "skill")
			{
				tempEquipSkillList.Sort();
				if (statusManager.selectCharacterNum == 0)
				{
					if (statusManager.selectItemId < 2000)
					{
						PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == statusManager.selectItemId && data.itemUniqueID == statusManager.selectItemUniqueId).weaponSetSkill = tempEquipSkillList;
						PlayerEquipDataManager.playerEquipSkillList[0] = new List<int>(tempEquipSkillList);
					}
					else
					{
						PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData data) => data.itemID == statusManager.selectItemId && data.itemUniqueID == statusManager.selectItemUniqueId).armorSetSkill = tempEquipSkillList;
						PlayerEquipDataManager.playerEquipSkillList[5] = new List<int>(tempEquipSkillList);
					}
				}
				else
				{
					if (statusManager.selectItemId < 2300)
					{
						PlayerInventoryDataManager.havePartyWeaponList.Find((HavePartyWeaponData data) => data.itemID == statusManager.selectItemId).weaponSetSkill = new List<int>(tempEquipSkillList);
					}
					else
					{
						PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData data) => data.itemID == statusManager.selectItemId).armorSetSkill = new List<int>(tempEquipSkillList);
					}
					PlayerEquipDataManager.playerEquipSkillList[statusManager.selectCharacterNum] = new List<int>(tempEquipSkillList);
				}
			}
		}
		else
		{
			PlayerNonSaveDataManager.isEquipItemChange = true;
			if (statusManager.selectCharacterNum == 0)
			{
				if (statusManager.selectItemId < 1300)
				{
					PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == statusManager.selectItemId && data.itemUniqueID == statusManager.selectItemUniqueId).weaponSetFactor = new List<HaveFactorData>(tempEquipFactorList);
				}
				else
				{
					PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData data) => data.itemID == statusManager.selectItemId && data.itemUniqueID == statusManager.selectItemUniqueId).armorSetFactor = new List<HaveFactorData>(tempEquipFactorList);
				}
			}
			else if (statusManager.selectItemId < 2300)
			{
				PlayerInventoryDataManager.havePartyWeaponList.Find((HavePartyWeaponData data) => data.itemID == statusManager.selectItemId).weaponSetFactor = new List<HaveFactorData>(tempEquipFactorList);
			}
			else
			{
				PlayerInventoryDataManager.havePartyArmorList.Find((HavePartyArmorData data) => data.itemID == statusManager.selectItemId).armorSetFactor = new List<HaveFactorData>(tempEquipFactorList);
			}
		}
		skillCustomFSM.SendTrigger("SendStatusCustomApply");
	}

	public void SetCustomSlotNumText()
	{
		skillSlotNumTextAray[0].text = skillSolotNumArray[0].ToString();
		skillSlotNumTextAray[1].text = skillSolotNumArray[1].ToString();
	}

	public void SetCustomApplyButtonCanvasGroup()
	{
		if (skillSolotNumArray[0] <= skillSolotNumArray[1])
		{
			applyButtonCanvasGroup.alpha = 1f;
			applyButtonCanvasGroup.interactable = true;
			skillSlotNumTextAray[0].color = Color.white;
		}
		else
		{
			applyButtonCanvasGroup.alpha = 0.5f;
			applyButtonCanvasGroup.interactable = false;
			skillSlotNumTextAray[0].color = Color.yellow;
		}
	}
}
