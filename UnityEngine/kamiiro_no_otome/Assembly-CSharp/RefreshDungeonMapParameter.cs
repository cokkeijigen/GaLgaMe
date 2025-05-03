using System.Collections;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class RefreshDungeonMapParameter : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private DungeonMapStatusManager dungeonMapStatusManager;

	private bool isCheckStart;

	private HaveWeaponData currentEquipWeaponData;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		dungeonMapStatusManager = GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapStatusManager>();
	}

	public override void OnStateBegin()
	{
		isCheckStart = false;
		if (PlayerNonSaveDataManager.isSexEnd)
		{
			PlayerStatusDataManager.playerAllHp = PlayerNonSaveDataManager.backUpPlayerAllHp;
		}
		dungeonMapStatusManager.playerHpSlider.maxValue = PlayerStatusDataManager.playerAllMaxHp;
		dungeonMapStatusManager.playerMaxHpText.text = PlayerStatusDataManager.playerAllMaxHp.ToString();
		dungeonMapStatusManager.playerHpSlider.value = PlayerStatusDataManager.playerAllHp;
		dungeonMapStatusManager.playerHpText.text = PlayerStatusDataManager.playerAllHp.ToString();
		Debug.Log("ダンジョンHPの表示を更新");
		StartCoroutine(WaitForWeaponData());
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		if (isCheckStart && dungeonMapStatusManager.isExpFrameSetUp.All((bool x) => x))
		{
			isCheckStart = false;
			Transition(stateLink);
			Debug.Log("ダンジョンマップ初期化完了");
		}
	}

	public override void OnStateLateUpdate()
	{
	}

	private IEnumerator WaitForWeaponData()
	{
		int loopCount = 0;
		int weaponID = PlayerEquipDataManager.playerEquipWeaponID[0];
		currentEquipWeaponData = null;
		while (currentEquipWeaponData == null && loopCount < 1000)
		{
			currentEquipWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == weaponID && data.equipCharacter == 0);
			if (currentEquipWeaponData != null)
			{
				Debug.Log("weaponDataの取得に成功：" + currentEquipWeaponData.itemID);
				break;
			}
			loopCount++;
			yield return null;
		}
		if (currentEquipWeaponData == null)
		{
			Debug.Log("weaponDataが取得できませんでした（1000フレーム到達）");
		}
		else
		{
			RefreshDungeonTpAndMore();
		}
	}

	private void RefreshDungeonTpAndMore()
	{
		int weaponIncludeMp = currentEquipWeaponData.weaponIncludeMp;
		int weaponIncludeMaxMp = currentEquipWeaponData.weaponIncludeMaxMp;
		dungeonMapStatusManager.playerTpSlider.maxValue = weaponIncludeMaxMp;
		dungeonMapStatusManager.playerMaxTpText.text = weaponIncludeMaxMp.ToString();
		dungeonMapStatusManager.playerTpSlider.value = weaponIncludeMp;
		dungeonMapStatusManager.playerTpText.text = weaponIncludeMp.ToString();
		Debug.Log("ダンジョンTPの表示を更新");
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			dungeonMapStatusManager.playerSpSlider.value = PlayerStatusDataManager.characterSp[PlayerDataManager.DungeonHeroineFollowNum];
			dungeonMapStatusManager.playerSpText.text = PlayerStatusDataManager.characterSp[PlayerDataManager.DungeonHeroineFollowNum].ToString();
		}
		else
		{
			dungeonMapStatusManager.playerSpSlider.value = 0f;
			dungeonMapStatusManager.playerSpText.text = "0";
		}
		Debug.Log("ダンジョンSPの表示を更新");
		dungeonMapStatusManager.playerLibidoText.text = PlayerDataManager.playerLibido.ToString();
		for (int i = 0; i < dungeonMapStatusManager.isExpFrameSetUp.Count; i++)
		{
			dungeonMapStatusManager.isExpFrameSetUp[i] = false;
		}
		foreach (GameObject playerExpGo in dungeonMapStatusManager.playerExpGoList)
		{
			playerExpGo.GetComponent<ArborFSM>().SendTrigger("ResetExpFrame");
		}
		for (int j = 0; j < dungeonMapManager.consecutiveResultData.Length; j++)
		{
			dungeonMapManager.consecutiveResultData[j] = 0;
		}
		dungeonMapManager.consecutiveResultEnemyMember.Clear();
		isCheckStart = true;
	}
}
