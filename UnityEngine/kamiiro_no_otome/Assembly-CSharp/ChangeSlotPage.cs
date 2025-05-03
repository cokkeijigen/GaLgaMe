using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class ChangeSlotPage : StateBehaviour
{
	private SystemSettingManager systemSettingManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		systemSettingManager = GameObject.Find("System Setting Manager").GetComponent<SystemSettingManager>();
	}

	public override void OnStateBegin()
	{
		GameObject[] savePageButtonArray = systemSettingManager.savePageButtonArray;
		for (int i = 0; i < savePageButtonArray.Length; i++)
		{
			savePageButtonArray[i].GetComponent<Image>().sprite = systemSettingManager.savePageButtonSpriteArray[0];
		}
		systemSettingManager.savePageButtonArray[PlayerNonSaveDataManager.selectSlotPageNum].GetComponent<Image>().sprite = systemSettingManager.savePageButtonSpriteArray[1];
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
