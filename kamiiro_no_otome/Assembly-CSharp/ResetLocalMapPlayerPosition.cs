using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ResetLocalMapPlayerPosition : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	public GameObject defaultPositionGO;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
	}

	public override void OnStateBegin()
	{
		SpriteRenderer component = totalMapAccessManager.localPlayerGo.GetComponent<SpriteRenderer>();
		if (string.IsNullOrEmpty(PlayerDataManager.currentPlaceName) || PlayerNonSaveDataManager.isWorldMapToInDoor)
		{
			totalMapAccessManager.localPlayerGo.transform.position = totalMapAccessManager.localMapDefaultPositionGo.transform.position;
			Debug.Log("ローカルプレイヤーをデフォ位置に移動");
		}
		else
		{
			Vector3 position = GameObject.Find(PlayerDataManager.currentPlaceName).transform.position + new Vector3(0f, 1.1f, 0f);
			totalMapAccessManager.localPlayerGo.transform.position = position;
			Debug.Log("ローカルプレイヤーを現在位置に移動");
		}
		component.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		bool flag = false;
		if (PlayerDataManager.isHeroineSpecifyFollow && PlayerDataManager.heroineSpecifyFollowPoint == PlayerDataManager.currentAccessPointName)
		{
			if (PlayerDataManager.heroineSpecifyFollowId == 0)
			{
				if (PlayerDataManager.isDungeonHeroineFollow)
				{
					flag = true;
				}
			}
			else if (!PlayerDataManager.isDungeonHeroineFollow || PlayerDataManager.heroineSpecifyFollowId != PlayerDataManager.DungeonHeroineFollowNum)
			{
				flag = true;
			}
		}
		if (flag)
		{
			if (PlayerDataManager.heroineSpecifyFollowId == 0)
			{
				PlayerDataManager.isDungeonHeroineFollow = false;
			}
			else
			{
				PlayerDataManager.isDungeonHeroineFollow = true;
				PlayerDataManager.DungeonHeroineFollowNum = PlayerDataManager.heroineSpecifyFollowId;
			}
			Debug.Log("指定同行を設定");
			PlayerNonSaveDataManager.isHeroineSpecifyFollowLocalMapNotice = true;
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
