using Arbor;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class MoveLocalMapPlace : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	private HeaderStatusManager headerStatusManager;

	public float jumpPower;

	public int numJumps = 3;

	public float time;

	private int preNumJumps;

	private float preTime;

	[SerializeField]
	private float heroineDerayTime;

	private bool isSamePlace;

	private SpriteRenderer playerImage;

	private SpriteRenderer heroineImage;

	public StateLink inDoorlink;

	public StateLink scenariolink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		playerImage = totalMapAccessManager.localPlayerGo.GetComponent<SpriteRenderer>();
		heroineImage = totalMapAccessManager.localHeroineGo.GetComponent<SpriteRenderer>();
		preNumJumps = 3;
		preTime = time;
	}

	public override void OnStateBegin()
	{
		totalMapAccessManager.mapDialogGo.SetActive(value: false);
		float num = heroineDerayTime;
		if (PlayerDataManager.currentPlaceName == PlayerNonSaveDataManager.selectPlaceName)
		{
			isSamePlace = true;
			preNumJumps = 1;
			preTime = time / 3f;
		}
		else
		{
			isSamePlace = false;
			preNumJumps = numJumps;
			preTime = time;
		}
		Vector2 vector = default(Vector2);
		Vector2 vector2 = PlayerNonSaveDataManager.selectPlaceGO.transform.parent.position;
		vector = ((!string.IsNullOrEmpty(PlayerDataManager.currentPlaceName)) ? ((Vector2)GameObject.Find(PlayerDataManager.currentPlaceName).transform.position) : ((Vector2)totalMapAccessManager.localMapDefaultPositionGo.transform.position));
		Vector2 vector3 = vector2 - vector;
		float num2 = Mathf.Atan2(vector3.y, vector3.x);
		num2 *= 57.29578f;
		int index = PlayerDataManager.DungeonHeroineFollowNum - 1;
		if (isSamePlace)
		{
			playerImage.sprite = totalMapAccessManager.playerSpriteArray[0];
			heroineImage.sprite = headerStatusManager.heroineSpriteArrayList[index][0];
			Debug.Log("キャラの向き：下");
		}
		else if (num2 <= 45f && num2 >= -45f)
		{
			playerImage.sprite = totalMapAccessManager.playerSpriteArray[1];
			heroineImage.sprite = headerStatusManager.heroineSpriteArrayList[index][1];
			num += 0.1f;
			Debug.Log("キャラの向き：右");
		}
		else if (num2 > 45f && num2 < 135f)
		{
			playerImage.sprite = totalMapAccessManager.playerSpriteArray[2];
			heroineImage.sprite = headerStatusManager.heroineSpriteArrayList[index][2];
			Debug.Log("キャラの向き：上");
		}
		else if (num2 >= 135f || num2 <= -135f)
		{
			playerImage.sprite = totalMapAccessManager.playerSpriteArray[3];
			heroineImage.sprite = headerStatusManager.heroineSpriteArrayList[index][3];
			Debug.Log("キャラの向き：左");
		}
		else
		{
			playerImage.sprite = totalMapAccessManager.playerSpriteArray[0];
			heroineImage.sprite = headerStatusManager.heroineSpriteArrayList[index][0];
			Debug.Log("キャラの向き：下");
		}
		PlayerNonSaveDataManager.beforeWorldMapPointName = PlayerDataManager.currentAccessPointName;
		PlayerNonSaveDataManager.beforeLocalMapPlaceName = PlayerDataManager.currentPlaceName;
		PlayerNonSaveDataManager.beforeTotalTimeZoneCount = PlayerDataManager.totalTimeZoneCount;
		PlayerDataManager.currentPlaceName = PlayerNonSaveDataManager.selectPlaceName;
		Vector3 endValue = PlayerNonSaveDataManager.selectPlaceGO.transform.position + new Vector3(0f, 1.1f, 0f);
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			totalMapAccessManager.localPlayerGo.transform.DOJump(endValue, jumpPower, preNumJumps, preTime).SetEase(Ease.Linear);
			Invoke("HeroineMove", num);
			return;
		}
		HeroineMove();
		totalMapAccessManager.localPlayerGo.transform.DOJump(endValue, jumpPower, preNumJumps, preTime).SetEase(Ease.Linear).OnComplete(delegate
		{
			totalMapAccessManager.ResetMapInPlace(2);
			if (string.IsNullOrEmpty(PlayerNonSaveDataManager.selectScenarioName))
			{
				Transition(inDoorlink);
			}
			else
			{
				Transition(scenariolink);
			}
		});
	}

	public override void OnStateEnd()
	{
		playerImage.sprite = totalMapAccessManager.playerSpriteArray[0];
		heroineImage.sprite = headerStatusManager.heroineSpriteArrayList[PlayerDataManager.DungeonHeroineFollowNum - 1][0];
	}

	public override void OnStateUpdate()
	{
	}

	public override void OnStateLateUpdate()
	{
	}

	private void HeroineMove()
	{
		Vector3 endValue = PlayerNonSaveDataManager.selectPlaceGO.transform.position + new Vector3(1f, 1.05f, 0f);
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			totalMapAccessManager.localHeroineGo.transform.DOJump(endValue, jumpPower, preNumJumps, preTime).SetEase(Ease.Linear).OnComplete(delegate
			{
				totalMapAccessManager.ResetMapInPlace(2);
				if (string.IsNullOrEmpty(PlayerNonSaveDataManager.selectScenarioName))
				{
					Transition(inDoorlink);
				}
				else
				{
					Transition(scenariolink);
				}
			});
		}
		else
		{
			totalMapAccessManager.localHeroineGo.transform.DOJump(endValue, jumpPower, preNumJumps, preTime).SetEase(Ease.Linear);
		}
	}
}
