using Arbor;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class MoveWorldMapDijkstraRoute : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	private DijkstraManager dijkstraManager;

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
		dijkstraManager = GameObject.Find("Dijkstra Manager").GetComponent<DijkstraManager>();
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
			num = ((dijkstraManager.shortestRouteList.Count <= 2) ? numJumps : 2);
			num2 = time * 2f / (float)dijkstraManager.shortestRouteList.Count;
			PlayerNonSaveDataManager.beforeCurrentTimeZone = PlayerDataManager.currentTimeZone;
			PlayerNonSaveDataManager.beforeCurrentMonthDay = PlayerDataManager.currentMonthDay;
			PlayerNonSaveDataManager.beforeCurrentTotalDay = PlayerDataManager.currentTotalDay;
			PlayerNonSaveDataManager.oldTimeZone = PlayerDataManager.totalTimeZoneCount;
			PlayerNonSaveDataManager.oldDayCount = PlayerDataManager.currentTotalDay;
			PlayerNonSaveDataManager.addTimeZoneNum = PlayerNonSaveDataManager.needMoveDayCount * 4;
			PlayerNonSaveDataManager.addTimeZoneNum += PlayerNonSaveDataManager.needMoveTimeCount;
			PlayerNonSaveDataManager.isRequiredCalcCarriageStore = true;
			GameObject.Find("AddTime Manager").GetComponent<PlayMakerFSM>().SendEvent("AddTimeZone");
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
		if (isSamePlace)
		{
			worldPlayerImage.sprite = totalMapAccessManager.carriageSpriteArray[0];
			ShortcutExtensions.DOJump(endValue: new Vector3(PlayerNonSaveDataManager.selectAccessPointGO.transform.position.x, PlayerNonSaveDataManager.selectAccessPointGO.transform.position.y + 0.35f, 0f), target: totalMapAccessManager.worldPlayerGo.transform, jumpPower: jumpPower, numJumps: num, duration: num2).SetEase(Ease.Linear).OnComplete(delegate
			{
				PlayerMoveEnd();
			});
			return;
		}
		dijkstraManager.characterAngleImageList.Clear();
		for (int i = 0; i < dijkstraManager.shortestRouteList.Count - 1; i++)
		{
			Vector3 localPosition = dijkstraManager.shortestRouteList[i].transform.localPosition;
			Vector3 localPosition2 = dijkstraManager.shortestRouteList[i + 1].transform.localPosition;
			Sprite characterAngleImage = GetCharacterAngleImage(localPosition, localPosition2);
			dijkstraManager.characterAngleImageList.Add(characterAngleImage);
		}
		worldPlayerImage.sprite = dijkstraManager.characterAngleImageList[0];
		int moveCount = 1;
		int spriteCount = dijkstraManager.characterAngleImageList.Count;
		Sequence s = DOTween.Sequence();
		for (int j = 1; j < dijkstraManager.shortestRouteList.Count; j++)
		{
			s.Append(ShortcutExtensions.DOJump(endValue: new Vector3(dijkstraManager.shortestRouteList[j].transform.position.x, dijkstraManager.shortestRouteList[j].transform.position.y + 0.35f, 0f), target: totalMapAccessManager.worldPlayerGo.transform, jumpPower: jumpPower, numJumps: num, duration: num2).SetEase(Ease.Linear));
			s.AppendCallback(delegate
			{
				if (moveCount < spriteCount)
				{
					worldPlayerImage.sprite = dijkstraManager.characterAngleImageList[moveCount];
					moveCount++;
					Debug.Log("キャラの向きを変更");
				}
			});
		}
		s.AppendCallback(delegate
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

	private Sprite GetCharacterAngleImage(Vector2 currentPos, Vector2 targetPos)
	{
		Sprite sprite = null;
		Vector2 vector = targetPos - currentPos;
		float num = Mathf.Atan2(vector.y, vector.x);
		num *= 57.29578f;
		if (num <= 45f && num >= -45f)
		{
			sprite = totalMapAccessManager.carriageSpriteArray[1];
			Debug.Log("キャラの向き：右");
		}
		else if (num > 45f && num < 135f)
		{
			sprite = totalMapAccessManager.carriageSpriteArray[2];
			Debug.Log("キャラの向き：上");
		}
		else if (num >= 135f || num <= -135f)
		{
			sprite = totalMapAccessManager.carriageSpriteArray[3];
			Debug.Log("キャラの向き：左");
		}
		else
		{
			sprite = totalMapAccessManager.carriageSpriteArray[0];
			Debug.Log("キャラの向き：下");
		}
		return sprite;
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
		Debug.Log("全ルートの移動終了");
		Transition(statelink);
	}
}
