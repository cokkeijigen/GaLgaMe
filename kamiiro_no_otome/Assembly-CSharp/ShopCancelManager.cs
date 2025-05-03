using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class ShopCancelManager : StateBehaviour
{
	private ShopManager shopManager;

	private ShopCustomManager shopCustomManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		shopManager = GameObject.Find("Shop Manager").GetComponentInParent<ShopManager>();
		shopCustomManager = GameObject.Find("Shop Custom Manager").GetComponentInParent<ShopCustomManager>();
	}

	public override void OnStateBegin()
	{
		GameObject gameObject = GameObject.Find("Notice Canvas");
		if (gameObject != null)
		{
			MasterAudio.PlaySound("SeApplyButton", 1f, null, 0f, null, null);
			gameObject.SetActive(value: false);
			Debug.Log("通知キャンバスを閉じる");
		}
		else if (shopCustomManager.shopOverlayCanvas.activeInHierarchy)
		{
			shopCustomManager.shopOverlayCanvas.SetActive(value: false);
			MasterAudio.PlaySound("SeWindowClose", 1f, null, 0f, null, null);
		}
		else
		{
			MasterAudio.PlaySound("SeMiniButton", 1f, null, 0f, null, null);
			Debug.Log("ショップを閉じる");
			shopManager.PushShopExitButton();
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
}
