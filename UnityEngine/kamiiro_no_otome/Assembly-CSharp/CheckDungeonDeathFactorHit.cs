using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckDungeonDeathFactorHit : StateBehaviour
{
	private ParameterContainer parameterContainer;

	private int deathSuccessRate;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		parameterContainer = GameObject.Find("Dungeon Agility Manager").GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		IList<bool> boolList = parameterContainer.GetBoolList("factorEffectSuccessList");
		for (int i = 0; i < boolList.Count; i++)
		{
			boolList[i] = false;
		}
		string text = parameterContainer.GetStringList("AgilityQueueList")[0].Substring(0, 1);
		int num = int.Parse(parameterContainer.GetStringList("AgilityQueueList")[0].Substring(1));
		int num2 = 0;
		if (!(text == "p"))
		{
			if (text == "e")
			{
			}
		}
		else
		{
			num2 = PlayerStatusDataManager.playerPartyMember[num];
			deathSuccessRate = PlayerEquipDataManager.equipFactorDeath[num2];
			if (deathSuccessRate == 0)
			{
				Debug.Log("即死ファクター効果なし");
			}
			else
			{
				for (int j = 0; j < PlayerStatusDataManager.enemyMember.Length; j++)
				{
					int num3 = PlayerStatusDataManager.enemyResist[j];
					if (PlayerStatusDataManager.enemyDeathResist[j])
					{
						deathSuccessRate = 0;
					}
					else
					{
						deathSuccessRate -= num3;
						deathSuccessRate = Mathf.Clamp(deathSuccessRate, 0, 100);
					}
					int num4 = Random.Range(0, 100);
					Debug.Log($"ファクター命中率：{deathSuccessRate}／命中ランダム：{num4}");
					if (deathSuccessRate >= num4)
					{
						parameterContainer.GetBoolList("factorEffectSuccessList")[3] = true;
					}
				}
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
