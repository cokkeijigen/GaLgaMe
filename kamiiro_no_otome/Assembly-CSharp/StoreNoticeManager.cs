using I2.Loc;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class StoreNoticeManager : MonoBehaviour
{
	public CarriageStoreNoticeManager carriageStoreNoticeManager;

	public PlayMakerFSM noticeFSM;

	public GameObject carriageStoreNoticeWindow;

	public Localize storeTendingResultTextLoc;

	public Text carriageStoreTradeCountText;

	public Text carriageStoreTradeQuantityText;

	public Text carriageStoreTradeMoneyText;

	public PlayableDirector storeDirector;
}
