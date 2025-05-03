using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckDungeonAttackFactorEffect : StateBehaviour
{
	private ParameterContainer parameterContainer;

	private int poisonSuccessRate;

	private int paralyzeSuccessRate;

	private int staggerSuccessRate;

	public StateLink effectHitLink;

	public StateLink noEffectLink;

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
		switch (text)
		{
		case "p":
		{
			num2 = PlayerStatusDataManager.playerPartyMember[num];
			poisonSuccessRate = PlayerEquipDataManager.equipFactorPoison[num2];
			paralyzeSuccessRate = PlayerEquipDataManager.equipFactorParalyze[num2];
			staggerSuccessRate = PlayerEquipDataManager.equipFactorStagger[num2];
			if (poisonSuccessRate == 0 && paralyzeSuccessRate == 0 && staggerSuccessRate == 0)
			{
				Debug.Log("ファクター付与効果なし");
				Transition(noEffectLink);
			}
			else
			{
				int num3 = 0;
				foreach (int item in PlayerStatusDataManager.enemyResist)
				{
					num3 += item;
				}
				poisonSuccessRate -= num3;
				poisonSuccessRate = Mathf.Clamp(poisonSuccessRate, 0, 100);
				paralyzeSuccessRate -= num3;
				paralyzeSuccessRate = Mathf.Clamp(paralyzeSuccessRate, 0, 100);
				staggerSuccessRate -= num3;
				staggerSuccessRate = Mathf.Clamp(staggerSuccessRate, 0, 100);
			}
			int num4 = Random.Range(0, 100);
			Debug.Log($"毒ファクター命中率：{poisonSuccessRate}／命中ランダム：{num4}");
			if (poisonSuccessRate >= num4 && poisonSuccessRate > 0)
			{
				boolList[0] = true;
			}
			int num5 = Random.Range(0, 100);
			Debug.Log($"麻痺ファクター命中率：{paralyzeSuccessRate}／命中ランダム：{num5}");
			if (paralyzeSuccessRate >= num5 && paralyzeSuccessRate > 0)
			{
				boolList[1] = true;
			}
			int num6 = Random.Range(0, 100);
			Debug.Log($"崩しファクター命中率：{staggerSuccessRate}／命中ランダム：{num6}");
			if (staggerSuccessRate >= num6 && staggerSuccessRate > 0)
			{
				boolList[2] = true;
			}
			break;
		}
		}
		parameterContainer.SetBoolList("factorEffectSuccessList", boolList);
		if (boolList.Any((bool data) => data))
		{
			Debug.Log("ファクター付与効果あり");
			Transition(effectHitLink);
		}
		else
		{
			Debug.Log("ファクター付与効果なし");
			Transition(noEffectLink);
		}
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
