using System.Collections.Generic;
using Arbor;
using I2.Loc;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

public class ShopCustomManager : MonoBehaviour
{
	private ShopManager shopManager;

	public ArborFSM shopCustomFSM;

	public GameObject shopOverlayCanvas;

	public GameObject[] customWindowArray;

	public Transform shopCustomContentGo;

	public GameObject[] customScrollSummaryArray;

	public Localize headerTextLoc;

	public string[] typeTextLocWeaponArray;

	public string[] typeTextLocArmorArray;

	public int[] skillSlotNumArray = new int[2];

	public Text[] skillSlotNumTextAray;

	public GameObject[] customScrollPrefabArray;

	public List<int> tempEquipSkillList = new List<int>();

	public List<HaveFactorData> tempEquipFactorList = new List<HaveFactorData>();

	public HaveFactorData tempHaveFactorData;

	public int customScrollContentIndex;

	public string selectCustomCanvasName;

	private void Awake()
	{
		shopManager = GameObject.Find("Shop Manager").GetComponent<ShopManager>();
	}

	public void PushShopCustomOpenButton(string type)
	{
		shopOverlayCanvas.SetActive(value: true);
		if (!(type == "skill"))
		{
			if (type == "factor")
			{
				customWindowArray[0].SetActive(value: true);
				customWindowArray[1].SetActive(value: false);
				customWindowArray[2].SetActive(value: true);
				customScrollSummaryArray[0].SetActive(value: false);
				customScrollSummaryArray[1].SetActive(value: true);
				shopCustomFSM.SendTrigger("OpenShopFactorCanvas");
			}
		}
		else
		{
			customWindowArray[0].SetActive(value: true);
			customWindowArray[1].SetActive(value: true);
			customWindowArray[2].SetActive(value: false);
			customScrollSummaryArray[0].SetActive(value: true);
			customScrollSummaryArray[1].SetActive(value: false);
			shopCustomFSM.SendTrigger("OpenShopSkillCanvas");
		}
	}

	public void PushShopCustomCloseButton()
	{
		shopOverlayCanvas.SetActive(value: false);
	}

	public void SetCustomSlotNumText()
	{
		skillSlotNumTextAray[0].text = skillSlotNumArray[0].ToString();
		skillSlotNumTextAray[1].text = skillSlotNumArray[1].ToString();
	}

	public void ShopCustomScrollItemDesapwnAll()
	{
		int childCount = shopCustomContentGo.childCount;
		if (childCount > 0)
		{
			Transform[] array = new Transform[childCount];
			for (int i = 0; i < childCount; i++)
			{
				array[i] = shopCustomContentGo.GetChild(i);
			}
			Transform[] array2 = array;
			foreach (Transform instance in array2)
			{
				PoolManager.Pools["Shop Pool Item"].Despawn(instance, 0f, shopManager.prefabParentGo);
			}
			Debug.Log("項目を全部デスポーン");
		}
	}
}
