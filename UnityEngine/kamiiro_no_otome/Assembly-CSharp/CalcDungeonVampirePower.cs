using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcDungeonVampirePower : StateBehaviour
{
	private ParameterContainer parameterContainer;

	public StateLink noVampireLink;

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
		float num = 0f;
		int num2 = 0;
		bool flag = false;
		string text = parameterContainer.GetStringList("AgilityQueueList")[0].Substring(0, 1);
		int.Parse(parameterContainer.GetStringList("AgilityQueueList")[0].Substring(1));
		switch (text)
		{
		case "e":
			num2 = parameterContainer.GetInt("enemyAttackDamage");
			if (num2 > 0)
			{
				for (int i = 0; i < PlayerStatusDataManager.playerPartyMember.Length; i++)
				{
					num += (float)PlayerEquipDataManager.equipFactorVampire[PlayerStatusDataManager.playerPartyMember[i]];
					num += (float)PlayerEquipDataManager.accessoryVampire[PlayerStatusDataManager.playerPartyMember[i]];
				}
				num /= 100f;
				float num3 = (float)num2 * num;
				float num4 = Random.Range(0.9f, 1.1f);
				int num5 = Mathf.FloorToInt(num3 * num4);
				Debug.Log($"ダメージ吸収量は：{num5}／ダメージ吸収力：{num}");
				if (num5 > 0)
				{
					parameterContainer.SetInt("slipDamage", num5);
					parameterContainer.SetBool("isPlayerSlip", value: true);
					flag = true;
				}
			}
			break;
		}
		if (flag)
		{
			Debug.Log("ダメージ吸収メンバーがいる");
			Transition(stateLink);
		}
		else
		{
			Debug.Log("ダメージ吸収メンバーはいない");
			Transition(noVampireLink);
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
