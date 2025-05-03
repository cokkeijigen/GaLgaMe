using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utage;

public class InDoorCommandManager : MonoBehaviour
{
	public enum DialogType
	{
		select,
		notice
	}

	private InDoorTalkManager inDoorTalkManager;

	public GameObject surveyCommandButtonGo;

	public GameObject inDoorExitBurronAlertGo;

	public Image inDoorExitBurronAlertImage;

	public GameObject restDialogGroupGo;

	public GameObject inDoorDialogGroupGo;

	public GameObject followRequestButtonGrouopGo;

	public GameObject followCancelButtonGrouopGo;

	public GameObject followDisableButtonGrouopGo;

	public GameObject blackImageGo;

	public DialogType dialogType;

	public Image carrageWorkShopImage;

	public Image carrageToolImage;

	public Image carrageFurnaceImage;

	public Image carrageAddOnImage;

	public List<Sprite> bgCarriageWorkShopSpriteList = new List<Sprite>();

	public List<Sprite> bgCarriageToolSpriteList = new List<Sprite>();

	public List<Sprite> bgCarriageFurnaceSpriteList = new List<Sprite>();

	public List<Sprite> bgCarriageAddOnSpriteList = new List<Sprite>();

	private void Awake()
	{
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
		inDoorExitBurronAlertGo.SetActive(value: false);
	}

	public void SetCarriageImageSprite()
	{
		CraftWorkShopData craftWorkShopData = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"];
		carrageWorkShopImage.sprite = bgCarriageWorkShopSpriteList[craftWorkShopData.workShopLv - 1];
		carrageToolImage.sprite = bgCarriageToolSpriteList[craftWorkShopData.workShopToolLv - 1];
		carrageFurnaceImage.sprite = bgCarriageFurnaceSpriteList[craftWorkShopData.furnaceLv - 1];
		carrageAddOnImage.sprite = bgCarriageAddOnSpriteList[craftWorkShopData.enableAddOnLv];
	}

	public void StartInDoorSurvey(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.selectSexBattleHeroineId = PlayerDataManager.DungeonHeroineFollowNum;
		PlayerNonSaveDataManager.loadSceneName = "sex";
		PlayerNonSaveDataManager.currentSceneName = "scenario";
		PlayerSexStatusDataManager.CheckSexHeroineMenstrualDay();
		GameObject.Find("Transition Manager").GetComponent<PlayMakerFSM>().SendEvent("StartFadeIn");
	}

	public void StartInDoorSurveyFromDialog()
	{
		PlayerNonSaveDataManager.selectSexBattleHeroineId = PlayerDataManager.DungeonHeroineFollowNum;
		PlayerNonSaveDataManager.loadSceneName = "sex";
		PlayerNonSaveDataManager.currentSceneName = "main";
		PlayerSexStatusDataManager.CheckSexHeroineMenstrualDay();
		GameObject.Find("Transition Manager").GetComponent<PlayMakerFSM>().SendEvent("StartFadeIn");
	}

	public void SetSexEndBool(AdvCommandSendMessageByName command)
	{
		bool flag = (PlayerNonSaveDataManager.isSexEnd = command.ParseCellOptional(AdvColumnName.Arg3, defaultVal: false));
		Debug.Log("えっち終了Boolは：" + flag);
	}
}
