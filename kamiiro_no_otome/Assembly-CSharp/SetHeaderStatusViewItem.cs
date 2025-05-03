using Arbor;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class SetHeaderStatusViewItem : StateBehaviour
{
	public enum Type
	{
		viewOnly,
		viewAndSize
	}

	private HeaderStatusManager headerStatusManager;

	public Type type;

	public Vector2 placePanelSizeV2;

	public bool viewPlaceGroup;

	public bool viewShopRankGroup;

	public bool viewLibidoGroup;

	public bool viewMoneyGroup;

	public bool viewRareDropGroup;

	public bool viewPartyGroup;

	public bool viewMenuButtonGroup;

	public bool viewSubMenuGroup;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		switch (type)
		{
		case Type.viewOnly:
			SetViewSetting();
			break;
		case Type.viewAndSize:
			headerStatusManager.placePanelGo.GetComponent<RectTransform>().DOSizeDelta(placePanelSizeV2, 0.1f);
			SetViewSetting();
			break;
		}
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

	private void SetViewSetting()
	{
		headerStatusManager.placePanelGo.SetActive(viewPlaceGroup);
		headerStatusManager.shopRankGroupGo.SetActive(viewShopRankGroup);
		headerStatusManager.libidoPanelGo.SetActive(viewLibidoGroup);
		headerStatusManager.moneyPanelGo.SetActive(viewMoneyGroup);
		if (viewRareDropGroup)
		{
			if (PlayerDataManager.rareDropRateRaisePowerNum > 0 && PlayerDataManager.rareDropRateRaiseRaimingDaysNum > 0)
			{
				headerStatusManager.rareDropPanelGo.SetActive(value: true);
			}
			else
			{
				headerStatusManager.rareDropPanelGo.SetActive(value: false);
			}
		}
		else
		{
			headerStatusManager.rareDropPanelGo.SetActive(value: false);
		}
		headerStatusManager.partyGroupParent.SetActive(viewPartyGroup);
		headerStatusManager.menuCanvasGroup.gameObject.SetActive(viewMenuButtonGroup);
	}
}
