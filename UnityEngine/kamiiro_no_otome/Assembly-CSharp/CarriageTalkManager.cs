using Coffee.UIExtensions;
using I2.Loc;
using PathologicalGames;
using Sirenix.OdinInspector;
using UnityEngine;

public class CarriageTalkManager : SerializedMonoBehaviour
{
	public CarriageManager carriageManager;

	public GameObject balloonCharacterFrameGo;

	public Localize balloonTextLoc;

	public GameObject priceSettingNoticeDialogGo;

	public Localize priceSettingNoticeTextLoc;

	public UIParticle uIParticle;

	public Transform effectSpawnGo;

	public GameObject priceSettingNoticePrefabGo;

	public void TalkBalloonItemSelect()
	{
		switch (carriageManager.selectCategoryNum)
		{
		case 0:
			balloonTextLoc.Term = "carriageBalloonItemSelectWeapon";
			break;
		case 1:
			balloonTextLoc.Term = "carriageBalloonItemSelectArmor";
			break;
		}
	}

	public void TalkBalloonStoreStart()
	{
		balloonTextLoc.Term = "carriageBalloonStoreStart";
	}

	public void PushPriceSettingNoticeDialogOkButton()
	{
		string key = "shopRank_second" + PlayerDataManager.currentShopRankSecondNum;
		PlayerFlagDataManager.priceSettingNoticeFlagDictionary[key] = true;
		PoolManager.Pools["Carriage Item Pool"].Despawn(effectSpawnGo, carriageManager.poolParentGo.transform);
		uIParticle.RefreshParticles();
		priceSettingNoticeDialogGo.SetActive(value: false);
		carriageManager.arborFSM.SendTrigger("EndPriceSettingNotice");
	}
}
