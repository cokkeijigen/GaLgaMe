using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class OpenCraftAddOnWindow : StateBehaviour
{
	private CraftAddOnManager craftAddOnManager;

	private bool isPushAdd;

	public StateLink addLink;

	public StateLink trashLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftAddOnManager = GameObject.Find("Craft AddOn Manager").GetComponent<CraftAddOnManager>();
	}

	public override void OnStateBegin()
	{
		string selectAddOnType = craftAddOnManager.selectAddOnType;
		if (!(selectAddOnType == "addOn_TYPE"))
		{
			if (selectAddOnType == "addOn_POWER")
			{
				if (craftAddOnManager.selectedMagicMatrialID[1] != 0)
				{
					craftAddOnManager.selectedMagicMatrialID[1] = 0;
					isPushAdd = false;
					craftAddOnManager.MagicMaterialListRefresh();
					MasterAudio.PlaySound("SeWindowClose", 1f, null, 0f, null, null);
					return;
				}
				craftAddOnManager.selectedMagicMaterialID_Temp = 0;
				isPushAdd = true;
				MasterAudio.PlaySound("SeMiniButton", 1f, null, 0f, null, null);
			}
		}
		else
		{
			if (craftAddOnManager.selectedMagicMatrialID[0] != 0)
			{
				craftAddOnManager.selectedMagicMatrialID[0] = 0;
				isPushAdd = false;
				craftAddOnManager.MagicMaterialListRefresh();
				MasterAudio.PlaySound("SeWindowClose", 1f, null, 0f, null, null);
				return;
			}
			craftAddOnManager.selectedMagicMaterialID_Temp = 0;
			isPushAdd = true;
			MasterAudio.PlaySound("SeMiniButton", 1f, null, 0f, null, null);
		}
		ParameterContainer component = craftAddOnManager.overlayCanvasSelectWindow.GetComponent<ParameterContainer>();
		component.GetGameObject("overlayApplyButton").GetComponent<CanvasGroup>().alpha = 0.5f;
		component.GetGameObject("overlayApplyButton").GetComponent<CanvasGroup>().interactable = false;
		component.GetGameObject("summaryWindowGo").GetComponent<ParameterContainer>().GetGameObjectList("contentsGroupArray")[0].SetActive(value: false);
		component.GetGameObject("summaryWindowGo").GetComponent<ParameterContainer>().GetGameObjectList("contentsGroupArray")[1].SetActive(value: true);
		if (isPushAdd)
		{
			craftAddOnManager.overlayCanvas.SetActive(value: true);
			craftAddOnManager.overlayCanvasSelectWindow.SetActive(value: true);
			Transition(addLink);
		}
		else
		{
			Transition(trashLink);
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
