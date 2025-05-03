using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SettingInheritButton : StateBehaviour
{
	private CraftCanvasManager craftCanvasManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
		if (craftCanvasManager.isInheritButtonLock)
		{
			craftCanvasManager.craftInheritGoArray[0].SetActive(value: false);
			craftCanvasManager.craftInheritGoArray[1].SetActive(value: false);
			craftCanvasManager.craftInheritGoArray[2].SetActive(value: true);
			Debug.Log("継承ボタンはロック");
		}
		else
		{
			craftCanvasManager.craftInheritGoArray[0].SetActive(value: true);
			craftCanvasManager.craftInheritGoArray[1].SetActive(value: true);
			craftCanvasManager.craftInheritGoArray[2].SetActive(value: false);
			Debug.Log("継承ボタンは使用可能");
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
