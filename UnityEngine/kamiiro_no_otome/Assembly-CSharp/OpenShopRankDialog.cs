using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class OpenShopRankDialog : StateBehaviour
{
	private HeaderStatusManager headerStatusManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
	}

	public override void OnStateBegin()
	{
		string rankNameTerm = GameDataManager.instance.shopRankDataBase.shopRankDataList.Find((ShopRankData data) => data.rankLevel == PlayerNonSaveDataManager.shopRankTempLvArray[0] && data.rankType == ShopRankData.RankType.scenario).rankNameTerm;
		string rankNameTerm2 = GameDataManager.instance.shopRankDataBase.shopRankDataList.Find((ShopRankData data) => data.rankLevel == PlayerNonSaveDataManager.shopRankTempLvArray[1] && data.rankType == ShopRankData.RankType.sales).rankNameTerm;
		headerStatusManager.shopRankDialogTextLoc1.Term = rankNameTerm;
		headerStatusManager.shopRankDialogTextLoc2.Term = rankNameTerm2;
		PlayerDataManager.currentShopRankFirstNum = PlayerNonSaveDataManager.shopRankTempLvArray[0];
		PlayerDataManager.currentShopRankSecondNum = PlayerNonSaveDataManager.shopRankTempLvArray[1];
		headerStatusManager.shopRankDialogCanvasGo.SetActive(value: true);
		headerStatusManager.SpawnShopRankChangeEffect();
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
