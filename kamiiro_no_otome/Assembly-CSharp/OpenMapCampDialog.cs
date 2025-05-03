using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class OpenMapCampDialog : StateBehaviour
{
	private HeaderStatusManager headerStatusManager;

	private MapCampManager mapCampManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		mapCampManager = GameObject.Find("Map Camp Manager").GetComponent<MapCampManager>();
	}

	public override void OnStateBegin()
	{
		List<HaveCampItemData> source = PlayerInventoryDataManager.haveItemCampItemList.Where((HaveCampItemData data) => data.itemType == "camp").ToList();
		HaveCampItemData kitData = source.OrderBy((HaveCampItemData data) => data.itemSortID).LastOrDefault();
		int num = (PlayerNonSaveDataManager.needCampTimeCount = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData data) => data.itemID == kitData.itemID).power);
		mapCampManager.campOkButtonGo.SetActive(value: true);
		mapCampManager.mapRestOkButtonGo.SetActive(value: false);
		mapCampManager.needCampTimeText.text = num.ToString();
		mapCampManager.mapCampDialogGo.SetActive(value: true);
		headerStatusManager.menuCanvasGroup.interactable = false;
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
