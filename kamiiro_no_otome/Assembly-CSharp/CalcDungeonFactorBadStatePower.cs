using System.Collections.Generic;
using System.Linq;
using Arbor;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class CalcDungeonFactorBadStatePower : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	private ParameterContainer parameterContainer;

	public float animationTime;

	private int afterCharge;

	private bool isStaggerHit;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = GameObject.Find("Dungeon Battle Manager").GetComponentInParent<DungeonBattleManager>();
		parameterContainer = GameObject.Find("Dungeon Agility Manager").GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		IList<bool> boolList = parameterContainer.GetBoolList("factorEffectSuccessList");
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		if (boolList[0])
		{
			list = (from ano in PlayerBattleConditionManager.enemyBadState[0].Select((PlayerBattleConditionManager.MemberBadState con, int index) => new
				{
					Content = con,
					Index = index
				})
				where ano.Content.type == "poison"
				select ano.Index).ToList();
			if (list.Any())
			{
				int continuityTurn = GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.poison).continuityTurn;
				continuityTurn *= PlayerStatusDataManager.playerPartyMember.Length + PlayerStatusDataManager.enemyMember.Length;
				PlayerBattleConditionManager.enemyBadState[0][list[0]].continutyTurn = continuityTurn;
				Debug.Log("毒ターン増加：turn");
			}
			else
			{
				int continuityTurn2 = GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.poison).continuityTurn;
				continuityTurn2 *= PlayerStatusDataManager.playerPartyMember.Length + PlayerStatusDataManager.enemyMember.Length;
				PlayerBattleConditionManager.MemberBadState item = new PlayerBattleConditionManager.MemberBadState
				{
					type = "poison",
					continutyTurn = continuityTurn2
				};
				PlayerBattleConditionManager.enemyBadState[0].Add(item);
				dungeonBattleManager.SetBadStateIcon("poison", 1, setValue: true);
				Debug.Log("毒データ追加：turn");
			}
		}
		if (boolList[1])
		{
			list2 = (from ano in PlayerBattleConditionManager.enemyBadState[0].Select((PlayerBattleConditionManager.MemberBadState con, int index) => new
				{
					Content = con,
					Index = index
				})
				where ano.Content.type == "paralyze"
				select ano.Index).ToList();
			if (list2.Any())
			{
				int continuityTurn3 = GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.paralyze).continuityTurn;
				continuityTurn3 *= PlayerStatusDataManager.playerPartyMember.Length + PlayerStatusDataManager.enemyMember.Length;
				PlayerBattleConditionManager.enemyBadState[0][list2[0]].continutyTurn = continuityTurn3;
				Debug.Log("麻痺ターン増加：turn");
			}
			else
			{
				int continuityTurn4 = GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.paralyze).continuityTurn;
				continuityTurn4 *= PlayerStatusDataManager.playerPartyMember.Length + PlayerStatusDataManager.enemyMember.Length;
				PlayerBattleConditionManager.MemberBadState item2 = new PlayerBattleConditionManager.MemberBadState
				{
					type = "paralyze",
					continutyTurn = continuityTurn4
				};
				PlayerBattleConditionManager.enemyBadState[0].Add(item2);
				dungeonBattleManager.SetBadStateIcon("paralyze", 1, setValue: true);
				Debug.Log("麻痺データ追加：turn");
			}
		}
		isStaggerHit = false;
		if (boolList[2])
		{
			float num = dungeonBattleManager.enemyCharge;
			if (num != 0f)
			{
				num /= 2f;
				num = Mathf.FloorToInt(num);
				afterCharge = (int)num;
				float num2 = animationTime / (float)PlayerDataManager.dungeonBattleSpeed;
				afterCharge = Mathf.Clamp(afterCharge, 0, 100);
				dungeonBattleManager.enemyChargeSlider.DOValue(afterCharge, num2 - 0.05f).SetEase(Ease.Linear);
				isStaggerHit = true;
			}
		}
		Transition(stateLink);
	}

	public override void OnStateEnd()
	{
		if (isStaggerHit)
		{
			dungeonBattleManager.enemyChargeSlider.value = afterCharge;
			dungeonBattleManager.enemyChargeText.text = afterCharge.ToString();
			dungeonBattleManager.enemyCharge = afterCharge;
		}
	}

	public override void OnStateUpdate()
	{
		if (isStaggerHit)
		{
			dungeonBattleManager.enemyChargeText.text = Mathf.Floor(dungeonBattleManager.enemyChargeSlider.value).ToString();
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
