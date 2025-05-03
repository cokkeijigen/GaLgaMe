using System.Collections.Generic;
using Arbor;
using UnityEngine;

public class CraftAddOnManager : MonoBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	private CraftTalkManager craftTalkManager;

	private ArborFSM addOnFSM;

	public GameObject overlayCanvas;

	public GameObject overlayCanvasSelectWindow;

	public GameObject overlayCanvasScrollContent;

	public GameObject overlayBackButton;

	public string selectAddOnType;

	public int selectScrollContentIndex;

	public int[] selectedMagicMatrialID = new int[2];

	public int selectedMagicMaterialID_Temp;

	public GameObject[] craftAddOnItemGoArray;

	public Sprite[] addOnAddButtonSprite;

	private void Awake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
		craftTalkManager = GameObject.Find("Craft Talk Manager").GetComponent<CraftTalkManager>();
		addOnFSM = GetComponent<ArborFSM>();
	}

	public void PushCraftAddButton(string type)
	{
		craftTalkManager.TalkBalloonAddOnItemSelect();
		selectAddOnType = type;
		addOnFSM.SendTrigger("OpenCraftAddOnWindow");
	}

	public void PushCancelAddButton()
	{
		craftTalkManager.TalkBalloonItemSelect();
	}

	public void MagicMaterialListRefresh()
	{
		GameObject gameObject = craftAddOnItemGoArray[0];
		GameObject gameObject2 = craftAddOnItemGoArray[1];
		string text = "";
		string text2 = "";
		string key = "";
		List<HaveItemData> typeAddOnList = new List<HaveItemData>();
		List<HaveItemData> powerAddOnList = new List<HaveItemData>();
		switch (PlayerNonSaveDataManager.selectCraftCanvasName)
		{
		case "craft":
		case "newCraft":
			PlayerInventoryDataAccess.GetPlayerHaveAddOnItems(out typeAddOnList, out powerAddOnList, craftManager.selectCategoryNum);
			text = "craftAddOnUnselect_Type";
			text2 = "craftAddOnUnselect_Power";
			key = "addOnMaterial";
			break;
		case "merge":
			PlayerInventoryDataAccess.GetPlayerHaveWonderItems(out typeAddOnList, out powerAddOnList, craftManager.selectCategoryNum);
			text = "craftWonderUnselect_Type";
			text2 = "craftWonderUnselect_Power";
			key = "wonderMaterial";
			break;
		}
		string term = text;
		_ = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[key];
		bool flag = false;
		if (selectedMagicMatrialID[0] != 0)
		{
			term = GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList.Find((ItemMagicMaterialData data) => data.itemID == selectedMagicMatrialID[0]).category.ToString() + selectedMagicMatrialID[0];
			flag = true;
		}
		ParameterContainer component = gameObject.GetComponent<ParameterContainer>();
		component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = term;
		if (flag)
		{
			component.GetVariable<UguiTextVariable>("nameText").text.color = craftManager.enableColor;
			component.GetVariable<UguiImage>("addButtonImage").image.sprite = addOnAddButtonSprite[1];
		}
		else
		{
			component.GetVariable<UguiTextVariable>("nameText").text.color = craftManager.disableColor;
			component.GetVariable<UguiImage>("addButtonImage").image.sprite = addOnAddButtonSprite[0];
		}
		term = text2;
		flag = false;
		if (selectedMagicMatrialID[1] != 0)
		{
			term = GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList.Find((ItemMagicMaterialData data) => data.itemID == selectedMagicMatrialID[1]).category.ToString() + selectedMagicMatrialID[1];
			flag = true;
		}
		ParameterContainer component2 = gameObject2.GetComponent<ParameterContainer>();
		component2.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = term;
		if (flag)
		{
			component2.GetVariable<UguiTextVariable>("nameText").text.color = craftManager.enableColor;
			component2.GetVariable<UguiImage>("addButtonImage").image.sprite = addOnAddButtonSprite[1];
		}
		else
		{
			component2.GetVariable<UguiTextVariable>("nameText").text.color = craftManager.disableColor;
			component2.GetVariable<UguiImage>("addButtonImage").image.sprite = addOnAddButtonSprite[0];
		}
		craftCanvasManager.ResetGetFactorInfo_ADDON();
	}
}
