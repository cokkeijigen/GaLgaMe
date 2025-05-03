using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcItemIsInStore : StateBehaviour
{
	private CarriageManager carriageManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		carriageManager = GameObject.Find("Carriage Manager").GetComponent<CarriageManager>();
	}

	public override void OnStateBegin()
	{
		List<HaveWeaponData> list = PlayerInventoryDataManager.haveWeaponList.Where((HaveWeaponData data) => data.isItemStoreDisplay).ToList();
		List<HaveArmorData> list2 = PlayerInventoryDataManager.haveArmorList.Where((HaveArmorData data) => data.isItemStoreDisplay).ToList();
		int num = 0;
		int num2 = 0;
		if (list.Any())
		{
			num = list.Count;
		}
		if (list2.Any())
		{
			num2 = list2.Count;
		}
		if (num > 0)
		{
			carriageManager.isItemInStore = true;
			carriageManager.startButtonCanvasGroup.interactable = true;
			carriageManager.startButtonCanvasGroup.alpha = 1f;
		}
		else if (num2 > 0)
		{
			carriageManager.isItemInStore = true;
			carriageManager.startButtonCanvasGroup.interactable = true;
			carriageManager.startButtonCanvasGroup.alpha = 1f;
		}
		else
		{
			carriageManager.isItemInStore = false;
			carriageManager.startButtonCanvasGroup.interactable = false;
			carriageManager.startButtonCanvasGroup.alpha = 0.5f;
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
