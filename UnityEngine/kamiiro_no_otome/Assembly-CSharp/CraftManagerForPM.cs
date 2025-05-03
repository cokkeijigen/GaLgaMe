using UnityEngine;

public class CraftManagerForPM : MonoBehaviour
{
	private CraftManager craftManager;

	private InDoorTalkManager inDoorTalkManager;

	private void Awake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
	}

	public void IntializeCraftUi()
	{
		craftManager.selectScrollContentIndex = 0;
		inDoorTalkManager.exitButtonCanvasGroup.gameObject.SetActive(value: false);
		inDoorTalkManager.commandButtonGroupGo.SetActive(value: false);
		inDoorTalkManager.carriageBgGroup.SetActive(value: true);
	}

	public string GetSelectCraftCommandNum()
	{
		return PlayerNonSaveDataManager.selectCraftCanvasName;
	}
}
