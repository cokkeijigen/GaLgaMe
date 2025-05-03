using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class InitFactorDisposalWindow : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCheckManager craftCheckManager;

	private FactorDisposalManager factorDisposalManager;

	private CraftTalkManager craftTalkManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftCheckManager = GameObject.Find("Craft Check Manager").GetComponent<CraftCheckManager>();
		factorDisposalManager = GameObject.Find("Factor Disposal Manager").GetComponent<FactorDisposalManager>();
		craftTalkManager = GameObject.Find("Craft Talk Manager").GetComponent<CraftTalkManager>();
	}

	public override void OnStateBegin()
	{
		Transform transform = factorDisposalManager.haveFactorScrollContent.transform;
		for (int num = transform.childCount - 1; num >= 0; num--)
		{
			Transform child = transform.GetChild(num);
			if (child.gameObject.tag == "CustomScrollItem")
			{
				child.SetParent(craftManager.poolParentGO.transform);
				if (PoolManager.Pools["Craft Item Pool"].IsSpawned(child))
				{
					PoolManager.Pools["Craft Item Pool"].Despawn(child);
				}
			}
		}
		int num2 = 0;
		foreach (HaveFactorData haveFactor in craftCheckManager.tempHaveFactorList)
		{
			Transform transform2 = PoolManager.Pools["Craft Item Pool"].Spawn(craftManager.scrollItemGoArray[4]);
			transform2.SetParent(transform);
			transform2.transform.localScale = new Vector3(1f, 1f, 1f);
			transform2.transform.SetSiblingIndex(num2);
			FactorData factorData = null;
			factorData = ((craftManager.selectCategoryNum != 0) ? GameDataManager.instance.factorDataBaseArmor.factorDataList.Find((FactorData m) => m.factorID == haveFactor.factorID) : GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData m) => m.factorID == haveFactor.factorID));
			if (factorData != null)
			{
				ParameterContainer component = transform2.GetComponent<ParameterContainer>();
				string term = "factor_" + factorData.factorType;
				component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = term;
				if (factorData.isAddPercentText)
				{
					component.GetVariable<UguiTextVariable>("powerText").text.text = haveFactor.factorPower + "%";
				}
				else
				{
					component.GetVariable<UguiTextVariable>("powerText").text.text = haveFactor.factorPower.ToString();
				}
				component.GetVariable<I2LocalizeComponent>("lvTextLoc").localize.Term = "craftGetFactorPower" + haveFactor.factorLV;
				component.GetVariable<UguiImage>("iconImage").image.sprite = factorData.factorSprite;
				if (craftCheckManager.tempSetFactorList.Find((HaveFactorData data) => data.uniqueID == haveFactor.uniqueID) != null)
				{
					component.GetGameObject("equipIconGo").SetActive(value: true);
				}
				else
				{
					component.GetGameObject("equipIconGo").SetActive(value: false);
				}
			}
			else
			{
				Debug.Log("ファクターデータがありません！");
			}
			FactorDisposalButtonClickAction component2 = transform2.GetComponent<FactorDisposalButtonClickAction>();
			component2.factorID = haveFactor.factorID;
			component2.uniqueID = haveFactor.uniqueID;
			component2.toggle.isOn = false;
			component2.isInitialized = true;
			num2++;
		}
		factorDisposalManager.tempSelectFactorID = 999;
		factorDisposalManager.tempSelectUniqueID = 0;
		factorDisposalManager.SetDisposalApplyButtonInteractable();
		craftTalkManager.TalkBalloonDisposal();
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
