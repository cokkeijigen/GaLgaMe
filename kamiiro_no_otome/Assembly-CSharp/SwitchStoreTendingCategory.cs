using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SwitchStoreTendingCategory : StateBehaviour
{
	public StateLink weaponLink;

	public StateLink armorLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
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
		int num3 = Random.Range(0, 100);
		Debug.Log("販売分岐ランダム／補正なし：" + num3 + "／武器陳列数；" + num + "／防具陳列数：" + num2);
		num3 = ((PlayerDataManager.hotSellingCategoryNum != 0) ? (num3 + PlayerDataManager.hotSellingTradeBonus * 2) : (num3 - PlayerDataManager.hotSellingTradeBonus * 2));
		Debug.Log("販売分岐ランダム：" + num3 + "／売れ筋Num：" + PlayerDataManager.hotSellingCategoryNum + "／売れ筋補正：" + PlayerDataManager.hotSellingTradeBonus * 2);
		if (num3 < 50)
		{
			if (num > 0)
			{
				Debug.Log("ランダム武器／武器陳列1以上あり");
				Transition(weaponLink);
			}
			else
			{
				Debug.Log("ランダム武器／武器陳列1以上なし");
				Transition(armorLink);
			}
		}
		else if (num2 > 0)
		{
			Debug.Log("ランダム防具／防具陳列1以上あり");
			Transition(armorLink);
		}
		else
		{
			Debug.Log("ランダム防具／防具陳列1以上なし");
			Transition(weaponLink);
		}
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
