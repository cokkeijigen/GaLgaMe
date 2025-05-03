using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckAccessoryEquipCharacter : StateBehaviour
{
	private StatusManager statusManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
	}

	public override void OnStateBegin()
	{
		if (statusManager.selectItemCategoryNum == 9)
		{
			int equipCharacter = PlayerInventoryDataManager.haveAccessoryList.Find((HaveAccessoryData data) => data.itemID == statusManager.selectItemId).equipCharacter;
			if (GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData data) => data.itemID == statusManager.selectItemId).category == ItemAccessoryData.Category.earRing)
			{
				if (statusManager.selectCharacterNum == 0)
				{
					SetEquipButtonEnable(isEnable: true);
					if (equipCharacter == statusManager.selectCharacterNum)
					{
						statusManager.itemEquipButtonLoc.Term = "buttonEquipRemove";
					}
					else
					{
						statusManager.itemEquipButtonLoc.Term = "buttonEquip";
					}
				}
				else
				{
					SetEquipButtonEnable(isEnable: false);
					statusManager.itemEquipButtonLoc.Term = "buttonCannotEquip";
					Debug.Log("エデン専用なので装備不可");
				}
			}
			else
			{
				SetEquipButtonEnable(isEnable: true);
				if (equipCharacter == statusManager.selectCharacterNum)
				{
					statusManager.itemEquipButtonLoc.Term = "buttonEquipRemove";
				}
				else
				{
					statusManager.itemEquipButtonLoc.Term = "buttonEquip";
				}
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

	private void SetEquipButtonEnable(bool isEnable)
	{
		CanvasGroup component = statusManager.itemEquipButtonGO.GetComponent<CanvasGroup>();
		if (isEnable)
		{
			component.alpha = 1f;
			component.interactable = true;
		}
		else
		{
			component.alpha = 0.5f;
			component.interactable = false;
		}
	}
}
