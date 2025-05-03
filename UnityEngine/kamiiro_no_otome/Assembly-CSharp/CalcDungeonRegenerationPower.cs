using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcDungeonRegenerationPower : StateBehaviour
{
	private ParameterContainer parameterContainer;

	public StateLink noRegeneLink;

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
		new List<int>();
		bool flag = false;
		string text = parameterContainer.GetStringList("AgilityQueueList")[0].Substring(0, 1);
		int.Parse(parameterContainer.GetStringList("AgilityQueueList")[0].Substring(1));
		switch (text)
		{
		case "p":
			if ((from ano in PlayerBattleConditionManager.playerBadState[0].Select((PlayerBattleConditionManager.MemberBadState con, int index) => new
				{
					Content = con,
					Index = index
				})
				where ano.Content.type == "regeneration"
				select ano.Index).ToList().Any())
			{
				float num3 = (float)PlayerStatusDataManager.playerAllMaxHp * 0.1f;
				float num4 = Random.Range(0.9f, 1.1f);
				int value2 = Mathf.FloorToInt(num3 * num4);
				parameterContainer.SetInt("slipDamage", value2);
				parameterContainer.SetBool("isPlayerSlip", value: true);
				flag = true;
			}
			break;
		case "e":
			if ((from ano in PlayerBattleConditionManager.enemyBadState[0].Select((PlayerBattleConditionManager.MemberBadState con, int index) => new
				{
					Content = con,
					Index = index
				})
				where ano.Content.type == "regeneration"
				select ano.Index).ToList().Any())
			{
				float num = (float)PlayerStatusDataManager.enemyAllMaxHp * 0.1f;
				float num2 = Random.Range(0.9f, 1.1f);
				int value = Mathf.FloorToInt(num * num2);
				parameterContainer.SetInt("slipDamage", value);
				parameterContainer.SetBool("isPlayerSlip", value: false);
				flag = true;
			}
			break;
		}
		if (flag)
		{
			Debug.Log("毒メンバーがいる");
			Transition(stateLink);
		}
		else
		{
			Debug.Log("毒メンバーはいない");
			Transition(noRegeneLink);
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
