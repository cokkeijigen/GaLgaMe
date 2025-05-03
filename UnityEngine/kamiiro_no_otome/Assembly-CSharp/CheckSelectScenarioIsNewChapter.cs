using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckSelectScenarioIsNewChapter : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	private LocalMapAccessManager localMapAccessManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		localMapAccessManager = GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>();
	}

	public override void OnStateBegin()
	{
		if (PlayerNonSaveDataManager.selectScenarioName == "MH_Levy_001")
		{
			totalMapAccessManager.mapDialogGo.SetActive(value: false);
			localMapAccessManager.newChapterHeaderTextLoc.Term = "alertNewChapterHeader";
			localMapAccessManager.newChapterMainTextLoc.Term = "alertNewChapter";
			localMapAccessManager.newCapterDialogGo.SetActive(value: true);
		}
		else if (PlayerNonSaveDataManager.selectScenarioName == "MH_ThreeBraves_002")
		{
			totalMapAccessManager.mapDialogGo.SetActive(value: false);
			localMapAccessManager.newChapterHeaderTextLoc.Term = "alertLastChapterHeader";
			localMapAccessManager.newChapterMainTextLoc.Term = "alertLastChapter";
			localMapAccessManager.newCapterDialogGo.SetActive(value: true);
		}
		else
		{
			Transition(stateLink);
		}
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
