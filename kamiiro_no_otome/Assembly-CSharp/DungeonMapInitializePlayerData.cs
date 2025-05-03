using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class DungeonMapInitializePlayerData : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private DungeonMapStatusManager dungeonMapStatusManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GetComponent<DungeonMapManager>();
		dungeonMapStatusManager = GetComponentInChildren<DungeonMapStatusManager>();
	}

	public override void OnStateBegin()
	{
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			PlayerStatusDataManager.playerPartyMember = new int[2]
			{
				0,
				PlayerDataManager.DungeonHeroineFollowNum
			};
			dungeonMapManager.chracterImageGoArray[1].SetActive(value: true);
		}
		else
		{
			PlayerStatusDataManager.playerPartyMember = new int[1];
			dungeonMapManager.chracterImageGoArray[1].SetActive(value: false);
		}
		for (int i = 0; i < PlayerStatusDataManager.playerPartyMember.Length; i++)
		{
			dungeonMapStatusManager.isExpFrameSetUp.Add(item: false);
			dungeonMapStatusManager.isAgilityFrameSetUp.Add(item: false);
		}
		for (int j = 0; j < PlayerStatusDataManager.playerPartyMember.Length; j++)
		{
			Transform transform = PoolManager.Pools["DungeonObject"].Spawn(dungeonMapStatusManager.playerExpGo, dungeonMapStatusManager.playerExpParent.transform);
			transform.GetComponent<ParameterContainer>().SetInt("characterID", PlayerStatusDataManager.playerPartyMember[j]);
			transform.GetComponent<ParameterContainer>().SetInt("partyMemberNum", j);
			transform.localScale = new Vector3(1f, 1f, 1f);
			dungeonMapStatusManager.playerExpGoList.Add(transform.gameObject);
		}
		if (!PlayerNonSaveDataManager.isSexEnd)
		{
			dungeonMapStatusManager.dungeonBuffAttack = 0;
			dungeonMapStatusManager.dungeonBuffDefense = 0;
			dungeonMapStatusManager.dungeonDeBuffAgility = 0;
			dungeonMapStatusManager.dungeonDeBuffAgiityRemainFloor = 0;
			dungeonMapStatusManager.dungeonBuffRetreat = 0;
			GameObject[] dungeonBuffFrameArray = dungeonMapStatusManager.dungeonBuffFrameArray;
			for (int k = 0; k < dungeonBuffFrameArray.Length; k++)
			{
				dungeonBuffFrameArray[k].SetActive(value: false);
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
