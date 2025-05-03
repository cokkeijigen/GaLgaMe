using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckSexTouchCumShotClick : StateBehaviour
{
	private SexTouchManager sexTouchManager;

	private SexTouchStatusManager sexTouchStatusManager;

	private SexTouchHeroineDataManager sexTouchHeroineDataManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexTouchManager = GameObject.Find("Sex Touch Manager").GetComponent<SexTouchManager>();
		sexTouchStatusManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchStatusManager>();
		sexTouchHeroineDataManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchHeroineDataManager>();
	}

	public override void OnStateBegin()
	{
		sexTouchStatusManager.isFellatioClick = true;
		int num = PlayerNonSaveDataManager.selectSexBattleHeroineId - 1;
		int num2 = 0;
		sexTouchManager.cumShotInfoFrame.GetComponent<CanvasGroup>().alpha = 0f;
		sexTouchManager.textVisibleButtonGo.SetActive(value: false);
		sexTouchManager.cumShotTextLoc.gameObject.SetActive(value: false);
		if (sexTouchStatusManager.playerLibidoPoint < 30)
		{
			sexTouchStatusManager.playerLibidoAddPoint = 3;
		}
		else if (sexTouchStatusManager.playerLibidoPoint < 55)
		{
			sexTouchStatusManager.playerLibidoAddPoint = 5;
		}
		else
		{
			sexTouchStatusManager.playerLibidoAddPoint = 7;
		}
		num2 = PlayerSexStatusDataManager.heroineMouthLv[num];
		Debug.Log("興奮度上昇値：" + sexTouchStatusManager.playerLibidoAddPoint + "／口のLV：" + num2);
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
