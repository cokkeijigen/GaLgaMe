using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CraftCancelManager : StateBehaviour
{
	private CraftManager craftManager;

	private CraftTalkManager craftTalkManager;

	private CraftAddOnManager craftAddOnManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GetComponentInParent<CraftManager>();
		craftTalkManager = GameObject.Find("Craft Talk Manager").GetComponent<CraftTalkManager>();
		craftAddOnManager = GameObject.Find("Craft AddOn Manager").GetComponent<CraftAddOnManager>();
	}

	public override void OnStateBegin()
	{
		if (craftAddOnManager.overlayCanvas.activeInHierarchy)
		{
			craftTalkManager.TalkBalloonAddOnItemSelect();
			craftAddOnManager.overlayBackButton.GetComponent<ArborFSM>().SendTrigger("PushCancelButton");
			Debug.Log("オーバーレイを閉じる");
		}
		else if (craftManager.canvasGroupArray[1].gameObject.activeInHierarchy)
		{
			craftManager.canvasGroupArray[1].gameObject.SetActive(value: false);
			craftAddOnManager.selectedMagicMatrialID[0] = 0;
			craftAddOnManager.selectedMagicMatrialID[1] = 0;
			craftAddOnManager.MagicMaterialListRefresh();
			Debug.Log("必要素材を閉じる");
		}
		else
		{
			Debug.Log("クラフトを閉じる");
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
