using Arbor;
using I2.Loc;
using UnityEngine;

[AddComponentMenu("")]
public class SwitchDialogCanvas : StateBehaviour
{
	public GameObject dialogGO;

	public Localize i2Loc;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		switch (PlayerNonSaveDataManager.openDialogName)
		{
		case "craft":
			switch (PlayerNonSaveDataManager.selectCraftCanvasName)
			{
			case "craft":
			case "newCraft":
				i2Loc.Term = "alertCraftDialog";
				break;
			case "merge":
				i2Loc.Term = "alertMergeDialog";
				break;
			}
			break;
		case "saveLoad":
			if (PlayerNonSaveDataManager.selectSystemTabName == "save")
			{
				i2Loc.Term = "alertSaveDialog";
			}
			else if (PlayerNonSaveDataManager.selectSystemTabName == "load")
			{
				i2Loc.Term = "alertLoadDialog";
			}
			break;
		case "quit":
			i2Loc.Term = "alertQuitDialog";
			break;
		case "retreat":
			Time.timeScale = 0f;
			i2Loc.Term = "alertRetreatDialog";
			break;
		case "dungeonMapRetreat":
			Time.timeScale = 0f;
			i2Loc.Term = "alertRetreatDialog";
			break;
		case "dungeonBattleRetreat":
			i2Loc.Term = "alertRetreatDialog";
			break;
		case "dungeonMapAuto":
			Time.timeScale = 0f;
			if (PlayerDataManager.isDungeonMapAuto)
			{
				i2Loc.Term = "alertDungeonAutoBeforeStop";
			}
			else
			{
				i2Loc.Term = "alertDungeonAutoBeforeStart";
			}
			break;
		}
		dialogGO.SetActive(value: true);
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
