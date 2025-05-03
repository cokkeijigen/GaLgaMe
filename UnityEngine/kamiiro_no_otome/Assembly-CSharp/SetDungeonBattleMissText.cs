using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class SetDungeonBattleMissText : StateBehaviour
{
	private ParameterContainer parameterContainer;

	private DungeonBattleManager dungeonBattleManager;

	private string type;

	public string textSituation;

	private Transform poolGO;

	public float despawnTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = base.transform.parent.GetComponent<DungeonBattleManager>();
		parameterContainer = GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		float num = 0.4f;
		float x = 0f;
		type = parameterContainer.GetStringList("AgilityQueueList")[0].Substring(0, 1);
		string text = textSituation;
		if (!(text == "AttackMiss"))
		{
			if (text == "Paralyze")
			{
				string text2 = type;
				if (!(text2 == "p"))
				{
					if (text2 == "e")
					{
						x = -450f;
					}
				}
				else
				{
					x = 450f;
				}
				poolGO = PoolManager.Pools["DungeonBattleEffect"].Spawn(dungeonBattleManager.poolEffectArray[5], dungeonBattleManager.damagePointRect[0]);
				dungeonBattleManager.damagePointRect[0].anchoredPosition = new Vector2(x, 400f);
			}
		}
		else
		{
			string text2 = type;
			if (!(text2 == "p"))
			{
				if (text2 == "e")
				{
					x = 450f;
				}
			}
			else
			{
				x = -450f;
			}
			poolGO = PoolManager.Pools["DungeonBattleEffect"].Spawn(dungeonBattleManager.poolEffectArray[2], dungeonBattleManager.damagePointRect[0]);
			dungeonBattleManager.damagePointRect[0].anchoredPosition = new Vector2(x, 400f);
		}
		poolGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
		PoolManager.Pools["DungeonBattleEffect"].Despawn(poolGO, despawnTime, dungeonBattleManager.poolManagerGO);
		num /= (float)PlayerDataManager.dungeonBattleSpeed;
		Invoke("InvokeMethod", num);
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

	private void InvokeMethod()
	{
		Transition(stateLink);
	}
}
