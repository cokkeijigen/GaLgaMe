using Coffee.UIExtensions;
using DarkTonic.MasterAudio;
using PathologicalGames;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

public class ExtensionDialogManager : SerializedMonoBehaviour
{
	private CraftExtensionManager craftExtensionManager;

	private ExtensionEffectManager extensionEffectManager;

	public GameObject extensionEffectPrefabGo;

	[SerializeField]
	private Transform extensionEffectSpawnGo;

	public PlayableDirector extensionDirector;

	public UIParticle uIParticle;

	public int selectExtensionIndex;

	private int selectExtensionCurrentLv;

	private int selectExtensionNextLv;

	private int selectExtensionCost;

	public bool isExtensionAnimationPlaying;

	private void Awake()
	{
		craftExtensionManager = GameObject.Find("Craft Extension Manager").GetComponent<CraftExtensionManager>();
	}

	public void PushExtensionButton(int index)
	{
		MasterAudio.PlaySound("SeMiniButton", 1f, null, 0f, null, null);
		CraftWorkShopData craftWorkShopData = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"];
		int[] array = craftExtensionManager.CalcExtensionCost(index, craftWorkShopData);
		string text = $"{array[0]:#,0}";
		craftExtensionManager.extensionDialogNeedCostText.text = text;
		switch (index)
		{
		case 0:
		{
			craftExtensionManager.extensionDialogTypeLoc.Term = "facirityLv";
			craftExtensionManager.extensionResult0Loc.Term = "facirityLv";
			craftExtensionManager.extensionResult0TextArray[0].text = craftWorkShopData.workShopLv.ToString();
			craftExtensionManager.extensionResult0TextArray[1].text = (craftWorkShopData.workShopLv + 1).ToString();
			craftExtensionManager.extensionResult1GoArray[0].SetActive(value: false);
			craftExtensionManager.extensionResult1GoArray[1].SetActive(value: true);
			craftExtensionManager.extensionResult1TextArray[0].text = craftWorkShopData.workShopLv.ToString();
			craftExtensionManager.extensionResult1TextArray[1].text = (craftWorkShopData.workShopLv + 1).ToString();
			craftExtensionManager.extensionResult1TypeLoc.Term = "extenstionDialog_faciltyLv";
			craftExtensionManager.extensionResult2TypeLoc.Term = "extenstionDialog_faciltyLvBonus";
			craftExtensionManager.extensionResult2QuantityLoc.Term = "perCentMark";
			craftExtensionManager.extensionResult2TextArray[0].text = craftWorkShopData.workShopLv.ToString();
			craftExtensionManager.extensionResult2TextArray[1].text = (craftWorkShopData.workShopLv + 1).ToString();
			GameObject[] extensionResult2GoArray = craftExtensionManager.extensionResult2GoArray;
			for (int i = 0; i < extensionResult2GoArray.Length; i++)
			{
				extensionResult2GoArray[i].SetActive(value: true);
			}
			craftExtensionManager.extensionResultLayout.preferredHeight = 235f;
			string extensionDialogRecipeLock3 = GameDataManager.instance.needExpDataBase.needFacilityLvNeedRecipeList[craftWorkShopData.workShopLv];
			SetExtensionDialogRecipeLock(extensionDialogRecipeLock3);
			break;
		}
		case 1:
		{
			craftExtensionManager.extensionDialogTypeLoc.Term = "facirityToolLv";
			craftExtensionManager.extensionResult0Loc.Term = "facirityToolLv";
			craftExtensionManager.extensionResult0TextArray[0].text = craftWorkShopData.workShopToolLv.ToString();
			craftExtensionManager.extensionResult0TextArray[1].text = (craftWorkShopData.workShopToolLv + 1).ToString();
			craftExtensionManager.extensionResult1GoArray[0].SetActive(value: true);
			craftExtensionManager.extensionResult1GoArray[1].SetActive(value: false);
			craftExtensionManager.extensionResult1LocArray[0].Term = "craftGetFactorPower" + craftWorkShopData.workShopToolLv;
			craftExtensionManager.extensionResult1LocArray[1].Term = "craftGetFactorPower" + (craftWorkShopData.workShopToolLv + 1);
			switch (craftWorkShopData.workShopToolLv)
			{
			case 1:
				craftExtensionManager.extensionResult2TextArray[0].text = "3";
				craftExtensionManager.extensionResult2TextArray[1].text = "2";
				break;
			case 2:
				craftExtensionManager.extensionResult2TextArray[0].text = "2";
				craftExtensionManager.extensionResult2TextArray[1].text = "1";
				break;
			case 3:
				craftExtensionManager.extensionResult2TextArray[0].text = "1";
				craftExtensionManager.extensionResult2TextArray[1].text = "1";
				break;
			}
			craftExtensionManager.extensionResult1TypeLoc.Term = "extenstionDialog_toolLvFactor";
			craftExtensionManager.extensionResult2TypeLoc.Term = "extenstionDialog_toolLvTime";
			craftExtensionManager.extensionResult2QuantityLoc.Term = "dialogWorldMapTimeZone";
			GameObject[] extensionResult2GoArray = craftExtensionManager.extensionResult2GoArray;
			for (int i = 0; i < extensionResult2GoArray.Length; i++)
			{
				extensionResult2GoArray[i].SetActive(value: true);
			}
			craftExtensionManager.extensionResultLayout.preferredHeight = 235f;
			string extensionDialogRecipeLock2 = GameDataManager.instance.needExpDataBase.needFacilityToolLvNeedRecipeList[craftWorkShopData.workShopToolLv];
			SetExtensionDialogRecipeLock(extensionDialogRecipeLock2);
			break;
		}
		case 2:
		{
			craftExtensionManager.extensionDialogTypeLoc.Term = "facirityFurnaceLv";
			craftExtensionManager.extensionResult0Loc.Term = "facirityFurnaceLv";
			craftExtensionManager.extensionResult0TextArray[0].text = craftWorkShopData.furnaceLv.ToString();
			craftExtensionManager.extensionResult0TextArray[1].text = (craftWorkShopData.furnaceLv + 1).ToString();
			craftExtensionManager.extensionResult1GoArray[0].SetActive(value: false);
			craftExtensionManager.extensionResult1GoArray[1].SetActive(value: true);
			craftExtensionManager.extensionResult1TextArray[0].text = craftWorkShopData.furnaceLv.ToString();
			craftExtensionManager.extensionResult1TextArray[1].text = (craftWorkShopData.furnaceLv + 1).ToString();
			craftExtensionManager.extensionResult1TypeLoc.Term = "extenstionDialog_furnaceLv";
			GameObject[] extensionResult2GoArray = craftExtensionManager.extensionResult2GoArray;
			for (int i = 0; i < extensionResult2GoArray.Length; i++)
			{
				extensionResult2GoArray[i].SetActive(value: false);
			}
			craftExtensionManager.extensionResultLayout.preferredHeight = 175f;
			string extensionDialogRecipeLock4 = GameDataManager.instance.needExpDataBase.needFacilityLvFurnaceNeedRecipeList[craftWorkShopData.furnaceLv];
			SetExtensionDialogRecipeLock(extensionDialogRecipeLock4);
			break;
		}
		case 3:
		{
			craftExtensionManager.extensionDialogTypeLoc.Term = "facilityAddOnLv";
			craftExtensionManager.extensionResult0Loc.Term = "facilityAddOnLv";
			craftExtensionManager.extensionResult0TextArray[0].text = craftWorkShopData.enableAddOnLv.ToString();
			craftExtensionManager.extensionResult0TextArray[1].text = (craftWorkShopData.enableAddOnLv + 1).ToString();
			craftExtensionManager.extensionResult1GoArray[0].SetActive(value: false);
			craftExtensionManager.extensionResult1GoArray[1].SetActive(value: true);
			craftExtensionManager.extensionResult1TextArray[0].text = craftWorkShopData.enableAddOnLv.ToString();
			craftExtensionManager.extensionResult1TextArray[1].text = (craftWorkShopData.enableAddOnLv + 1).ToString();
			craftExtensionManager.extensionResult1TypeLoc.Term = "extenstionDialog_addOnLv";
			GameObject[] extensionResult2GoArray = craftExtensionManager.extensionResult2GoArray;
			for (int i = 0; i < extensionResult2GoArray.Length; i++)
			{
				extensionResult2GoArray[i].SetActive(value: false);
			}
			craftExtensionManager.extensionResultLayout.preferredHeight = 175f;
			string extensionDialogRecipeLock = GameDataManager.instance.needExpDataBase.needFacilityLvAddOnNeedRecipeList[craftWorkShopData.enableAddOnLv];
			SetExtensionDialogRecipeLock(extensionDialogRecipeLock);
			break;
		}
		}
		switch (index)
		{
		case 0:
		case 2:
			craftExtensionManager.extensionDialogTimeZoneText.text = "3";
			PlayerNonSaveDataManager.needCraftTimeCount = 3;
			break;
		case 1:
		case 3:
			craftExtensionManager.extensionDialogTimeZoneText.text = "2";
			PlayerNonSaveDataManager.needCraftTimeCount = 2;
			break;
		}
		selectExtensionIndex = index;
		selectExtensionCurrentLv = array[1];
		selectExtensionNextLv = array[1] + 1;
		selectExtensionCost = array[0];
		craftExtensionManager.dialogWindowArray[0].SetActive(value: false);
		craftExtensionManager.dialogWindowArray[1].SetActive(value: true);
		craftExtensionManager.dialogWindowArray[2].SetActive(value: false);
		craftExtensionManager.dialogCanvas.SetActive(value: true);
		craftExtensionManager.characterTalkTextLoc.Term = "extensionBalloonConfirm";
	}

	private void SetExtensionDialogRecipeLock(string recipeName)
	{
		Debug.Log("レシピの解放を確認する：" + recipeName);
		if (PlayerFlagDataManager.recipeFlagDictionary[recipeName])
		{
			craftExtensionManager.recipeLockTextGroupGo.SetActive(value: false);
			craftExtensionManager.extensionDialogOkButtonCg.interactable = true;
			craftExtensionManager.extensionDialogOkButtonCg.alpha = 1f;
		}
		else
		{
			craftExtensionManager.recipeLockTextGroupGo.SetActive(value: true);
			craftExtensionManager.extensionDialogOkButtonCg.interactable = false;
			craftExtensionManager.extensionDialogOkButtonCg.alpha = 0.5f;
		}
	}

	public void ApplyExtensionDialogButton()
	{
		craftExtensionManager.dialogCanvas.SetActive(value: false);
		craftExtensionManager.commonCanvas.SetActive(value: false);
		craftExtensionManager.extensionCanvas.SetActive(value: false);
		craftExtensionManager.characterCanvas.SetActive(value: false);
		craftExtensionManager.craftTypeFrameGo.SetActive(value: false);
		craftExtensionManager.moneyFrameGo.SetActive(value: false);
		craftExtensionManager.craftLvWindowGo.SetActive(value: false);
		isExtensionAnimationPlaying = true;
	}

	public void StartExtensionAnimation()
	{
		extensionEffectManager = GameObject.Find("Carriage CG Group").GetComponent<ExtensionEffectManager>();
		extensionEffectManager.StartExtensionTimeline(selectExtensionIndex);
	}

	public void OpenAfterExtensionDialog()
	{
		craftExtensionManager.commonCanvas.SetActive(value: true);
		craftExtensionManager.extensionCanvas.SetActive(value: true);
		craftExtensionManager.characterCanvas.SetActive(value: true);
		craftExtensionManager.craftTypeFrameGo.SetActive(value: true);
		craftExtensionManager.moneyFrameGo.SetActive(value: true);
		craftExtensionManager.craftLvWindowGo.SetActive(value: true);
		craftExtensionManager.extensionAfterDialogImage.sprite = craftExtensionManager.facilityCategoryIcon[selectExtensionIndex];
		craftExtensionManager.extensionAfterDialogLvTextArray[0].text = selectExtensionCurrentLv.ToString();
		craftExtensionManager.extensionAfterDialogLvTextArray[1].text = selectExtensionNextLv.ToString();
		switch (selectExtensionIndex)
		{
		case 0:
			PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].workShopLv = selectExtensionNextLv;
			PlayerQuestDataManager.RefreshStoryQuestFlagData("facilityLv", selectExtensionNextLv);
			break;
		case 1:
			PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].workShopToolLv = selectExtensionNextLv;
			PlayerQuestDataManager.RefreshStoryQuestFlagData("facilityToolLv", selectExtensionNextLv);
			break;
		case 2:
			PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].furnaceLv = selectExtensionNextLv;
			PlayerQuestDataManager.RefreshStoryQuestFlagData("furnaceLv", selectExtensionNextLv);
			break;
		case 3:
			PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].enableAddOnLv = selectExtensionNextLv;
			PlayerQuestDataManager.RefreshStoryQuestFlagData("addOnLv", selectExtensionNextLv);
			break;
		}
		PlayerDataManager.AddHaveMoney(selectExtensionCost * -1);
		PlayerNonSaveDataManager.oldTimeZone = PlayerDataManager.totalTimeZoneCount;
		PlayerNonSaveDataManager.oldDayCount = PlayerDataManager.currentTotalDay;
		PlayerNonSaveDataManager.addTimeZoneNum = PlayerNonSaveDataManager.needCraftTimeCount;
		PlayerNonSaveDataManager.isRequiredCalcCarriageStore = true;
		GameObject.Find("AddTime Manager").GetComponent<PlayMakerFSM>().SendEvent("AddTimeZone");
		string text = $"{PlayerDataManager.playerHaveMoney:#,0}";
		craftExtensionManager.moneyFrameText.text = text;
		craftExtensionManager.characterTalkTextLoc.Term = "extensionBalloonCompleted";
		craftExtensionManager.dialogCanvas.SetActive(value: true);
		craftExtensionManager.dialogWindowArray[0].SetActive(value: false);
		craftExtensionManager.dialogWindowArray[1].SetActive(value: false);
		craftExtensionManager.dialogWindowArray[2].SetActive(value: true);
		extensionDirector.gameObject.GetComponent<AudioSource>().volume = PlayerOptionsDataManager.optionsSeVolume;
		extensionDirector.time = 0.0;
		extensionDirector.Play();
		isExtensionAnimationPlaying = false;
	}

	public void SpawnAfterExtensionEffect()
	{
		extensionEffectSpawnGo = PoolManager.Pools["Extension Item Pool"].Spawn(extensionEffectPrefabGo, uIParticle.transform);
		extensionEffectSpawnGo.localScale = new Vector3(1f, 1f, 1f);
		extensionEffectSpawnGo.localPosition = new Vector3(0f, 0f, 0f);
		Debug.Log("売り上げエフェクトをスポーン");
		uIParticle.RefreshParticles();
	}

	public void CloseDialogWindow(string type)
	{
		if (!(type == "recovery"))
		{
			if (type == "extension")
			{
				craftExtensionManager.extensionFSM.SendTrigger("RefreshExtensionCanvas");
			}
		}
		else
		{
			craftExtensionManager.extensionFSM.SendTrigger("RefreshMpRecoveryCanvas");
		}
		craftExtensionManager.dialogCanvas.SetActive(value: false);
	}

	public void CloseAfterDialogWindow()
	{
		if (PoolManager.Pools["Extension Item Pool"].IsSpawned(extensionEffectSpawnGo))
		{
			PoolManager.Pools["Extension Item Pool"].Despawn(extensionEffectSpawnGo, craftExtensionManager.spawnPoolParent.transform);
		}
		uIParticle.RefreshParticles();
		craftExtensionManager.extensionFSM.SendTrigger("RefreshExtensionCanvas");
		MasterAudio.PlaySound("SeMiniButton", 1f, null, 0f, null, null);
		craftExtensionManager.dialogCanvas.SetActive(value: false);
	}

	public bool CheckExtensionAnimationPlaying()
	{
		return isExtensionAnimationPlaying;
	}

	public void ApplyRecoveryDialogButton()
	{
		craftExtensionManager.extensionFSM.SendTrigger("PushMpRecoveryButton");
		craftExtensionManager.dialogCanvas.SetActive(value: false);
	}
}
