using Arbor;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class MoveWorldMapAccessPoint : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	public GameObject playerGO;

	private Image worldPlayerImage;

	private bool isSamePlace;

	public float jumpPower;

	public int numJumps = 3;

	public float time;

	public StateLink statelink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		worldPlayerImage = totalMapAccessManager.worldPlayerGo.GetComponent<Image>();
	}

	public override void OnStateBegin()
	{
		int num = numJumps;
		float num2 = time;
		if (PlayerDataManager.currentAccessPointName == PlayerNonSaveDataManager.selectAccessPointName)
		{
			Debug.Log("移動場所は同じ");
			isSamePlace = true;
			num = 1;
			num2 = time / 3f;
		}
		else
		{
			Debug.Log("移動場所は違う");
			isSamePlace = false;
			num = numJumps;
			num2 = time;
			PlayerNonSaveDataManager.oldTimeZone = PlayerDataManager.totalTimeZoneCount;
			PlayerNonSaveDataManager.oldDayCount = PlayerDataManager.currentTotalDay;
			PlayerNonSaveDataManager.addTimeZoneNum = PlayerNonSaveDataManager.needMoveDayCount * 4;
			PlayerNonSaveDataManager.addTimeZoneNum += PlayerNonSaveDataManager.needMoveTimeCount;
			PlayerNonSaveDataManager.isRequiredCalcCarriageStore = true;
			GameObject.Find("AddTime Manager").GetComponent<PlayMakerFSM>().SendEvent("AddTimeZone");
		}
		Vector2 vector = GameObject.Find(PlayerDataManager.currentAccessPointName).transform.position;
		Vector2 vector2 = (Vector2)PlayerNonSaveDataManager.selectAccessPointGO.transform.position - vector;
		float num3 = Mathf.Atan2(vector2.y, vector2.x);
		num3 *= 57.29578f;
		if (isSamePlace)
		{
			worldPlayerImage.sprite = totalMapAccessManager.carriageSpriteArray[0];
			Debug.Log("キャラの向き：下");
		}
		else if (num3 <= 45f && num3 >= -45f)
		{
			worldPlayerImage.sprite = totalMapAccessManager.carriageSpriteArray[1];
			Debug.Log("キャラの向き：右");
		}
		else if (num3 > 45f && num3 < 135f)
		{
			worldPlayerImage.sprite = totalMapAccessManager.carriageSpriteArray[2];
			Debug.Log("キャラの向き：上");
		}
		else if (num3 >= 135f || num3 <= -135f)
		{
			worldPlayerImage.sprite = totalMapAccessManager.carriageSpriteArray[3];
			Debug.Log("キャラの向き：左");
		}
		else
		{
			worldPlayerImage.sprite = totalMapAccessManager.carriageSpriteArray[0];
			Debug.Log("キャラの向き：下");
		}
		PlayerNonSaveDataManager.beforeWorldMapPointName = PlayerDataManager.currentAccessPointName;
		Debug.Log("現在地：" + PlayerDataManager.currentAccessPointName + "／目的地：" + PlayerNonSaveDataManager.selectAccessPointName);
		PlayerDataManager.currentAccessPointName = PlayerNonSaveDataManager.selectAccessPointName;
		PlayerDataManager.currentPlaceName = PlayerNonSaveDataManager.selectAccessPointName;
		if (!PlayerNonSaveDataManager.isWorldMapToInDoor)
		{
			PlayerDataManager.mapPlaceStatusNum = 1;
			PlayerNonSaveDataManager.openUiScreenLevel = 1;
		}
		ShortcutExtensions.DOJump(endValue: new Vector3(PlayerNonSaveDataManager.selectAccessPointGO.transform.position.x, PlayerNonSaveDataManager.selectAccessPointGO.transform.position.y + 0.35f, 0f), target: playerGO.transform, jumpPower: jumpPower, numJumps: num, duration: num2).SetEase(Ease.Linear).OnComplete(delegate
		{
			PlayerMoveEnd();
		});
	}

	public override void OnStateEnd()
	{
		worldPlayerImage.sprite = totalMapAccessManager.carriageSpriteArray[0];
	}

	public override void OnStateUpdate()
	{
	}

	public override void OnStateLateUpdate()
	{
	}

	private void PlayerMoveEnd()
	{
		if (PlayerDataManager.isSelectDungeon)
		{
			PlayerNonSaveDataManager.addDungeonTimeZoneNum = 1;
			PlayerNonSaveDataManager.isAddTimeFromDungeon = true;
		}
		if (!PlayerNonSaveDataManager.isWorldMapToInDoor)
		{
			totalMapAccessManager.ResetMapInPlace(1);
		}
		else
		{
			totalMapAccessManager.ResetMapInPlace(0);
		}
		Transition(statelink);
	}
}
