using Arbor;
using DarkTonic.MasterAudio;
using I2.Loc;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class CraftUiManager : SerializedMonoBehaviour
{
	public CraftManager craftManager;

	public GameObject craftBookGo;

	public GameObject craftTypeFrameGo;

	public GameObject craftLvWindowGo;

	public Localize exitButtonTextLoc;

	public GameObject uiVisibleButtonGo;

	public Localize uiVisibleButtonLoc;

	public Image uiVisibleButtonImage;

	public Sprite[] uiVisibleButtonSpriteArray;

	public GameObject dropPointFrameGo;

	public Localize[] dropPointFrameLocArray;

	public bool isExtensionUiHidden;

	public void SetCraftUiVisible()
	{
		isExtensionUiHidden = !isExtensionUiHidden;
		if (isExtensionUiHidden)
		{
			craftManager.blackSmithButtonGroup.SetActive(value: false);
			craftTypeFrameGo.SetActive(value: false);
			craftLvWindowGo.SetActive(value: false);
			uiVisibleButtonLoc.Term = "buttonExtensionUiShow";
			uiVisibleButtonImage.sprite = uiVisibleButtonSpriteArray[1];
			return;
		}
		craftManager.blackSmithButtonGroup.SetActive(value: true);
		craftTypeFrameGo.SetActive(value: true);
		if (GameObject.Find("Craft Manager") == null)
		{
			craftLvWindowGo.SetActive(value: true);
		}
		else
		{
			craftLvWindowGo.SetActive(value: false);
		}
		uiVisibleButtonLoc.Term = "buttonExtensionUiHidden";
		uiVisibleButtonImage.sprite = uiVisibleButtonSpriteArray[0];
	}

	public void OpenCraftCanvas(string type)
	{
		PlayerNonSaveDataManager.selectCraftCanvasName = type;
		MasterAudio.PlaySound("SeMiniButton2", 1f, null, 0f, null, null);
		craftManager.arborFSM.SendTrigger("ReOpenCraftCanvas");
	}

	public void OpenDropPointFrame(GameObject gameObject)
	{
		gameObject.GetComponent<ParameterContainer>().GetInt("itemId");
	}
}
