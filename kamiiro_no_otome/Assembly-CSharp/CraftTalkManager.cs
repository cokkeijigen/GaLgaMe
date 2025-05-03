using DarkTonic.MasterAudio;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class CraftTalkManager : MonoBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	public GameObject characterCanvasGo;

	public GameObject balloonCharacterFrameGo;

	public Image balloonCharacterImage;

	public Localize balloonTextLoc;

	public Sprite[] balloonCharacterSpriteArray;

	public int selectItemEquipCharacterId;

	private void Awake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
	}

	public void TalkBalloonItemSelect()
	{
		balloonCharacterImage.sprite = balloonCharacterSpriteArray[0];
		switch (PlayerNonSaveDataManager.selectCraftCanvasName)
		{
		case "craft":
			switch (craftManager.selectCategoryNum)
			{
			case 0:
				balloonTextLoc.Term = "craftBalloonSelectWeapon";
				break;
			case 1:
				balloonTextLoc.Term = "craftBalloonSelectArmor";
				break;
			}
			break;
		case "merge":
			balloonTextLoc.Term = "craftBalloonItemSelectMerge";
			break;
		case "newCraft":
			switch (craftManager.selectCategoryNum)
			{
			case 0:
				balloonTextLoc.Term = "craftBalloonSelectNewWeapon";
				break;
			case 1:
				balloonTextLoc.Term = "craftBalloonSelectNewArmor";
				break;
			case 2:
				balloonTextLoc.Term = "craftBalloonSelectMaterial";
				break;
			case 3:
				balloonTextLoc.Term = "craftBalloonSelectTalisman";
				break;
			case 4:
				balloonTextLoc.Term = "craftBalloonSelectKit";
				break;
			}
			break;
		}
	}

	public void TalkBalloonItemSelectAfter()
	{
		switch (PlayerNonSaveDataManager.selectCraftCanvasName)
		{
		case "craft":
			balloonTextLoc.Term = "craftBalloonItemSelectAfter";
			break;
		case "merge":
			balloonCharacterImage.sprite = balloonCharacterSpriteArray[selectItemEquipCharacterId];
			balloonTextLoc.Term = "craftBalloonItemSelectAfter" + selectItemEquipCharacterId;
			break;
		case "newCraft":
			balloonTextLoc.Term = "craftBalloonNewItemSelectAfter";
			break;
		}
	}

	public void TalkBalloonAddOnItemSelect()
	{
		switch (PlayerNonSaveDataManager.selectCraftCanvasName)
		{
		case "craft":
		case "newCraft":
			balloonTextLoc.Term = "craftBalloonAddOnItemSelect";
			break;
		case "merge":
			balloonTextLoc.Term = "craftBalloonAddOnItemSelect" + selectItemEquipCharacterId;
			break;
		}
	}

	public void TalkBalloonConfirm()
	{
		switch (PlayerNonSaveDataManager.selectCraftCanvasName)
		{
		case "craft":
		case "newCraft":
			balloonTextLoc.Term = "craftBalloonConfirm";
			break;
		case "merge":
			balloonTextLoc.Term = "craftBalloonConfirm" + selectItemEquipCharacterId;
			break;
		}
	}

	public void TalkBalloonItemCrafted()
	{
		switch (PlayerNonSaveDataManager.selectCraftCanvasName)
		{
		case "craft":
			if (craftCanvasManager.isCompleteEnhanceCount || craftCanvasManager.isRemainingDaysZero)
			{
				MasterAudio.PlaySound("SeCraftedEvolutionItem", 1f, null, 0f, null, null);
				balloonTextLoc.Term = "craftBalloonItemEvolutionCrafted";
			}
			else
			{
				MasterAudio.PlaySound("SeCraftedEquipItem", 1f, null, 0f, null, null);
				balloonTextLoc.Term = "craftBalloonItemCrafted";
			}
			break;
		case "merge":
			if (craftCanvasManager.isCompleteEnhanceCount)
			{
				MasterAudio.PlaySound("SeCraftedEvolutionItem", 1f, null, 0f, null, null);
			}
			else
			{
				MasterAudio.PlaySound("SeCraftedEquipItem", 1f, null, 0f, null, null);
			}
			balloonTextLoc.Term = "craftBalloonItemCrafted" + selectItemEquipCharacterId;
			break;
		case "newCraft":
			switch (craftManager.selectCategoryNum)
			{
			case 0:
			case 1:
				MasterAudio.PlaySound("SeCraftedEquipItem", 1f, null, 0f, null, null);
				break;
			case 2:
			case 3:
			case 4:
				MasterAudio.PlaySound("SeCraftedItem", 1f, null, 0f, null, null);
				break;
			}
			balloonTextLoc.Term = "craftBalloonNewItemCrafted";
			break;
		}
	}

	public void TalkBalloonDisposal()
	{
		switch (PlayerNonSaveDataManager.selectCraftCanvasName)
		{
		case "craft":
		case "newCraft":
			balloonTextLoc.Term = "craftBalloonFactorDisposal";
			break;
		case "merge":
			balloonTextLoc.Term = "craftBalloonFactorDisposal" + selectItemEquipCharacterId;
			break;
		}
	}
}
