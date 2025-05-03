using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class DisplayCraftOverlaySimpleSummary : StateBehaviour
{
	private CraftManager craftManager;

	private CraftTalkManager craftTalkManager;

	private CraftAddOnManager craftAddOnManager;

	private ParameterContainer parameterContainer;

	private ParameterContainer parameterContainer_MSW;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftTalkManager = GameObject.Find("Craft Talk Manager").GetComponent<CraftTalkManager>();
		craftAddOnManager = GameObject.Find("Craft AddOn Manager").GetComponent<CraftAddOnManager>();
		parameterContainer_MSW = craftAddOnManager.overlayCanvasSelectWindow.GetComponent<ParameterContainer>();
		GameObject gameObject = parameterContainer_MSW.GetGameObject("summaryWindowGo");
		parameterContainer = gameObject.GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		parameterContainer.GetGameObjectList("contentsGroupArray")[0].SetActive(value: true);
		parameterContainer.GetGameObjectList("contentsGroupArray")[1].SetActive(value: false);
		parameterContainer_MSW.GetGameObject("overlayApplyButton").GetComponent<CanvasGroup>().alpha = 1f;
		parameterContainer_MSW.GetGameObject("overlayApplyButton").GetComponent<CanvasGroup>().interactable = true;
		int materialItemID = craftAddOnManager.selectedMagicMaterialID_Temp;
		ItemMagicMaterialData itemMagicMaterialData = GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList.Find((ItemMagicMaterialData data) => data.itemID == materialItemID);
		string text = itemMagicMaterialData.category.ToString() + materialItemID;
		string term = text + "_summary";
		parameterContainer.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = text;
		parameterContainer.GetVariable<UguiImage>("itemImage").image.sprite = itemMagicMaterialData.itemSprite;
		parameterContainer.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = term;
		craftTalkManager.TalkBalloonAddOnItemSelect();
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
