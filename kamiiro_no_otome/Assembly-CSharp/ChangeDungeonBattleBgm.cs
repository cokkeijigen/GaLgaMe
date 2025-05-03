using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ChangeDungeonBattleBgm : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private DungeonMapStatusManager dungeonMapStatusManager;

	private PlayMakerFSM bgmManegerFSM;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		dungeonMapStatusManager = GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapStatusManager>();
		bgmManegerFSM = GameObject.Find("Bgm Play Manager").GetComponent<PlayMakerFSM>();
	}

	public override void OnStateBegin()
	{
		if (!dungeonMapManager.isBossRouteSelect)
		{
			if (dungeonMapStatusManager.isTpSkipEnable)
			{
				int weaponID = PlayerEquipDataManager.playerEquipWeaponID[0];
				int weaponIncludeMp = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == weaponID && data.equipCharacter == 0).weaponIncludeMp;
				weaponIncludeMp -= dungeonMapStatusManager.needSkipTp;
				weaponIncludeMp = Mathf.Clamp(weaponIncludeMp, 0, 999);
				PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == weaponID && data.equipCharacter == 0).weaponIncludeMp = weaponIncludeMp;
			}
		}
		else
		{
			DungeonMapData dungeonMapData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerDataManager.currentDungeonName);
			if (dungeonMapManager.dungeonCurrentFloorNum == dungeonMapData.maxFloor)
			{
				Debug.Log("最下層ボス");
				PlayerDataManager.playBgmCategoryName = "dungeonDeepBoss";
				bgmManegerFSM.SendEvent("ChangeMasterAudioPlaylist");
			}
			else
			{
				Debug.Log("最下層ではないボス");
				PlayerDataManager.playBgmCategoryName = "dungeonBoss";
				bgmManegerFSM.SendEvent("ChangeMasterAudioPlaylist");
			}
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
