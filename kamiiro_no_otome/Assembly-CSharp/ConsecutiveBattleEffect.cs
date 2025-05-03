using Arbor;
using UnityEngine;
using UnityEngine.Playables;

[AddComponentMenu("")]
public class ConsecutiveBattleEffect : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	public float waitTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
	}

	public override void OnStateBegin()
	{
		if (dungeonMapManager.battleConsecutiveTotalNum > 0)
		{
			float num = 0f;
			switch (PlayerDataManager.dungeonBattleSpeed)
			{
			case 1:
				num = 1.2f;
				break;
			case 2:
				num = 1.4f;
				break;
			case 4:
				num = 2f;
				break;
			default:
				num = 1.2f;
				break;
			}
			dungeonMapManager.dungeonRoundDirector.gameObject.SetActive(value: true);
			dungeonMapManager.dungeonRoundDirector.Play();
			dungeonMapManager.dungeonRoundDirector.playableGraph.GetRootPlayable(0).SetTime(0.0);
			dungeonMapManager.dungeonRoundDirector.playableGraph.GetRootPlayable(0).SetSpeed(num);
		}
		else
		{
			Transition(stateLink);
		}
	}

	public override void OnStateEnd()
	{
		dungeonMapManager.dungeonRoundDirector.gameObject.SetActive(value: false);
	}

	public override void OnStateUpdate()
	{
	}

	public override void OnStateLateUpdate()
	{
	}
}
