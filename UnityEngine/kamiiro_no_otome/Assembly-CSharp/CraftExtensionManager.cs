using System.Collections.Generic;
using Arbor;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftExtensionManager : MonoBehaviour
{
	private HeaderStatusManager headerStatusManager;

	public GameObject commonCanvas;

	public GameObject mpRecoveryCanvas;

	public GameObject extensionCanvas;

	public GameObject characterCanvas;

	public GameObject dialogCanvas;

	public GameObject[] dialogWindowArray;

	public ArborFSM extensionFSM;

	public ArborFSM craftLvFSM;

	public ParameterContainer craftLvParameterContainer;

	public GameObject moneyFrameGo;

	public Text moneyFrameText;

	public GameObject craftLvWindowGo;

	public GameObject craftTypeFrameGo;

	public Localize craftTypeTextLoc;

	public GameObject uiVisibleButtonGo;

	public Localize uiVisibleButtonLoc;

	public Image uiVisibleButtonImage;

	public Sprite[] uiVisibleButtonSpriteArray;

	public bool isExtensionUiHidden;

	public GameObject[] scrollPrefabGoArray;

	public GameObject[] scrollContentGoArray;

	public GameObject spawnPoolParent;

	public Sprite[] selectScrollContentSpriteArray;

	public Text[] needMoneyTextArray;

	public GameObject[] addOnLvTextGroup;

	public CanvasGroup[] extensionButtonArray;

	public Localize[] extensionButtonTextLoc;

	public Localize extensionDialogTypeLoc;

	public LayoutElement extensionResultLayout;

	public Localize extensionResult1TypeLoc;

	public Localize extensionResult2TypeLoc;

	public Localize extensionResult2QuantityLoc;

	public GameObject[] extensionResult1GoArray;

	public GameObject[] extensionResult2GoArray;

	public Localize extensionResult0Loc;

	public Text[] extensionResult0TextArray;

	public Localize[] extensionResult1LocArray;

	public Text[] extensionResult1TextArray;

	public Text[] extensionResult2TextArray;

	public Text extensionDialogTimeZoneText;

	public Text extensionDialogNeedCostText;

	public Text dialogNeedCostText;

	public Sprite[] facilityCategoryIcon;

	public Image extensionAfterDialogImage;

	public Text[] extensionAfterDialogLvTextArray;

	public CanvasGroup extensionDialogOkButtonCg;

	public GameObject recipeLockTextGroupGo;

	public int clickedItemID;

	public int clickedItemUniqueID;

	public int clickedScrollContentIndex;

	public bool isMpRecoveryComplete;

	public GameObject itemSummaryWindowGo;

	public List<HaveFactorData> tempHaveFactorList;

	public TextMeshProUGUI[] haveFactorTextArray;

	public TextMeshProUGUI havePowderText;

	public CanvasGroup[] mpRecoveryButtonArray;

	public int needAllMpRecoveryPowderNum;

	public bool isAllMpRecovery;

	public Localize characterTalkTextLoc;

	private void Awake()
	{
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
	}

	private void Start()
	{
		headerStatusManager.clockGroupGo.gameObject.SetActive(value: false);
		headerStatusManager.clockCanvasGroup.gameObject.SetActive(value: false);
	}

	public void SetExtensionUiVisible()
	{
		isExtensionUiHidden = !isExtensionUiHidden;
		if (isExtensionUiHidden)
		{
			extensionCanvas.SetActive(value: false);
			characterCanvas.SetActive(value: false);
			craftTypeFrameGo.SetActive(value: false);
			moneyFrameGo.SetActive(value: false);
			craftLvWindowGo.SetActive(value: false);
			uiVisibleButtonLoc.Term = "buttonExtensionUiShow";
			uiVisibleButtonImage.sprite = uiVisibleButtonSpriteArray[1];
		}
		else
		{
			extensionCanvas.SetActive(value: true);
			characterCanvas.SetActive(value: true);
			craftTypeFrameGo.SetActive(value: true);
			moneyFrameGo.SetActive(value: true);
			craftLvWindowGo.SetActive(value: true);
			uiVisibleButtonLoc.Term = "buttonExtensionUiHidden";
			uiVisibleButtonImage.sprite = uiVisibleButtonSpriteArray[0];
		}
	}

	public int[] CalcExtensionCost(int index, CraftWorkShopData craftData)
	{
		int num = 0;
		int num2 = 0;
		int[] array = new int[2];
		switch (index)
		{
		case 0:
			num2 = craftData.workShopLv;
			num = GameDataManager.instance.needExpDataBase.needFacilityLvCostList[num2];
			break;
		case 1:
			num2 = craftData.workShopToolLv;
			num = GameDataManager.instance.needExpDataBase.needFacilityToolLvCostList[num2];
			break;
		case 2:
			num2 = craftData.furnaceLv;
			num = GameDataManager.instance.needExpDataBase.needFacilityFurnaceLvCostList[num2];
			break;
		case 3:
			num2 = craftData.enableAddOnLv;
			num = GameDataManager.instance.needExpDataBase.needFacilityAddOnLvCostList[num2];
			break;
		}
		array[0] = num;
		array[1] = num2;
		return array;
	}

	public void PushMpRecoveryScrollButton()
	{
		extensionFSM.SendTrigger("SendItemListIndex");
	}

	public void PushMpRecoveryButton(bool isAll)
	{
		isAllMpRecovery = isAll;
		if (isAll)
		{
			dialogNeedCostText.text = needAllMpRecoveryPowderNum.ToString();
			dialogWindowArray[0].SetActive(value: true);
			dialogWindowArray[1].SetActive(value: false);
			dialogWindowArray[2].SetActive(value: false);
			dialogCanvas.SetActive(value: true);
			characterTalkTextLoc.Term = "mpRecoveryBalloonConfirm";
		}
		else
		{
			extensionFSM.SendTrigger("PushMpRecoveryButton");
		}
	}

	public int GetScrollContentIndexFromItemId(int itemID, int uniqueID)
	{
		int result = 0;
		int childCount = scrollContentGoArray[0].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			int itemID2 = scrollContentGoArray[0].transform.GetChild(i).GetComponent<CraftItemListClickAction>().itemID;
			int instanceID = scrollContentGoArray[0].transform.GetChild(i).GetComponent<CraftItemListClickAction>().instanceID;
			if (itemID2 == itemID && instanceID == uniqueID)
			{
				result = i;
				break;
			}
		}
		return result;
	}
}
