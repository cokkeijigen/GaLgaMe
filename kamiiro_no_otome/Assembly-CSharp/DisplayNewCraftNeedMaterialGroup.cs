using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class DisplayNewCraftNeedMaterialGroup : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	private CraftAddOnManager craftAddOnManager;

	private CraftTalkManager craftTalkManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
		craftAddOnManager = GameObject.Find("Craft AddOn Manager").GetComponent<CraftAddOnManager>();
		craftTalkManager = GameObject.Find("Craft Talk Manager").GetComponent<CraftTalkManager>();
	}

	public override void OnStateBegin()
	{
		craftTalkManager.TalkBalloonItemSelectAfter();
		craftManager.canvasGroupArray[0].gameObject.SetActive(value: false);
		craftManager.canvasGroupArray[1].gameObject.SetActive(value: true);
		craftCanvasManager.craftWindowGoArray[0].SetActive(value: false);
		switch (craftManager.selectCategoryNum)
		{
		case 0:
		case 1:
			craftCanvasManager.craftWindowGoArray[1].GetComponent<RectTransform>().anchoredPosition = new Vector3(-386f, -102f);
			craftCanvasManager.craftWindowGoArray[2].GetComponent<RectTransform>().anchoredPosition = new Vector3(-386f, -312f);
			break;
		case 2:
			craftCanvasManager.craftWindowGoArray[3].GetComponent<RectTransform>().anchoredPosition = new Vector3(-386f, -102f);
			break;
		}
		craftCanvasManager.applyButtonLoc.Term = "buttonNewCraftStart";
		craftCanvasManager.applyButtonGroup[0].GetComponent<Image>().sprite = craftCanvasManager.craftStartButtonSpriteArray[0];
		craftAddOnManager.MagicMaterialListRefresh();
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
