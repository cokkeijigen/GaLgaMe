using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class ApplyCreateItemComplete : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	private CraftCheckManager craftCheckManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
		craftCheckManager = GameObject.Find("Craft Check Manager").GetComponent<CraftCheckManager>();
	}

	public override void OnStateBegin()
	{
		PlayerInventoryDataAccess.HaveItemListSortAll();
		if (PoolManager.Pools["Craft Item Pool"].IsSpawned(craftCheckManager.craftedEffectSpawnGo))
		{
			PoolManager.Pools["Craft Item Pool"].Despawn(craftCheckManager.craftedEffectSpawnGo, 0f, craftManager.poolParentGO.transform);
		}
		craftCheckManager.uIParticle.RefreshParticles();
		craftCheckManager.animationCanvas.SetActive(value: false);
		craftCheckManager.getFactorWindow.SetActive(value: false);
		craftCheckManager.craftedSummaryWindow.SetActive(value: false);
		craftCheckManager.craftedSummaryWindow.SetActive(value: false);
		craftManager.canvasGroupArray[0].gameObject.SetActive(value: true);
		craftManager.canvasGroupArray[1].gameObject.SetActive(value: false);
		craftCheckManager.blackImageGo.transform.SetSiblingIndex(0);
		craftCanvasManager.isCraftComplete = true;
		if (!PlayerFlagDataManager.tutorialFlagDictionary["craft"])
		{
			PlayerNonSaveDataManager.isTutorialCrafted = true;
		}
		PlayerNonSaveDataManager.isRequiredCalcCarriageStore = true;
		GameObject.Find("AddTime Manager").GetComponent<PlayMakerFSM>().SendEvent("AddTimeZone");
		if (craftCanvasManager.isPowerUpCraft || craftCanvasManager.isCompleteEnhanceCount)
		{
			PlayerEquipDataManager.CalcEquipPlayerHaveWeaponFactor(CallBackWeaponMethod, isAllCalc: true);
		}
		else
		{
			Transition(stateLink);
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

	private void CallBackWeaponMethod()
	{
		PlayerStatusDataManager.SetUpPlayerStatus(isSetUp: true, CallBackStatusMethod);
	}

	private void CallBackStatusMethod()
	{
		Transition(stateLink);
		Debug.Log("Equipデータの更新完了");
	}
}
