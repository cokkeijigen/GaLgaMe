using Arbor;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class FactorDisposalManager : MonoBehaviour
{
	private CraftManager craftManager;

	private CraftCheckManager craftCheckManager;

	public ArborFSM arborFSM;

	public ToggleGroup disposalToggleGroup;

	public GameObject haveFactorScrollContent;

	public GameObject disposalFrameGo;

	public Image disposalFrameIcon;

	public Localize disposalFrameTextLoc;

	public Text disposalFrameNumText;

	public CanvasGroup applyButton;

	public int selectSiblingIndex;

	public int tempSelectFactorID;

	public int tempSelectUniqueID;

	private void Awake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftCheckManager = GameObject.Find("Craft Check Manager").GetComponent<CraftCheckManager>();
	}

	public void PushDisposalOKButton()
	{
		arborFSM.SendTrigger("PushDisposalOKButton");
	}

	public void SetDisposalApplyButtonInteractable()
	{
		if (tempSelectFactorID == 999)
		{
			applyButton.alpha = 0.5f;
			applyButton.interactable = false;
		}
		else
		{
			applyButton.alpha = 1f;
			applyButton.interactable = true;
		}
	}

	public void ResetScrollContentSprite()
	{
		int childCount = haveFactorScrollContent.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			haveFactorScrollContent.transform.GetChild(i).GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage")
				.image.sprite = craftManager.selectScrollContentSpriteArray[0];
		}
	}
}
