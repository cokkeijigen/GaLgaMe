using System.Collections.Generic;
using Arbor;
using DarkTonic.MasterAudio;
using I2.Loc;
using PathologicalGames;
using TMPro;
using UnityEngine;

[AddComponentMenu("")]
public class SetBattleItemHealText : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	private ItemData itemData;

	private List<Transform> poolGoList = new List<Transform>();

	public float despawnTime;

	public StateLink healLink;

	public StateLink medicLink;

	public StateLink mpHealLink;

	public StateLink hpMpHealLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
	}

	public override void OnStateBegin()
	{
		int num = 0;
		int num2 = 0;
		bool active = true;
		itemData = scenarioBattleTurnManager.useItemData;
		string text = itemData.category.ToString();
		poolGoList.Clear();
		if (itemData.target == ItemData.Target.all)
		{
			string allTargetTerm = scenarioBattleSkillManager.GetAllTargetTerm();
			utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = allTargetTerm;
			if (text != "medicine")
			{
				for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
				{
					num += scenarioBattleTurnManager.skillAttackHitDataList[i].healValue;
					if (scenarioBattleTurnManager.skillAttackHitDataList[i].isHealHit)
					{
						num2++;
					}
				}
				num /= num2;
				utageBattleSceneManager.battleTextArray3[2].GetComponent<TextMeshProUGUI>().text = num.ToString();
			}
		}
		else
		{
			int playerTargetNum = scenarioBattleTurnManager.playerTargetNum;
			utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = "character" + playerTargetNum;
			utageBattleSceneManager.battleTextArray3[2].GetComponent<TextMeshProUGUI>().text = scenarioBattleTurnManager.skillAttackHitDataList[0].healValue.ToString();
		}
		switch (text)
		{
		case "potion":
		case "allPotion":
			active = true;
			CalcHealSkill();
			break;
		case "medicine":
			active = false;
			CalcMedicSkill();
			break;
		case "revive":
			active = false;
			CalcReviveSkill();
			break;
		case "mpPotion":
		case "allMpPotion":
			active = true;
			CalcMpHealSkill();
			break;
		case "elixir":
			active = true;
			CalcHpMpHealSkill();
			break;
		}
		utageBattleSceneManager.battleTextGroupArray[2].SetActive(value: true);
		utageBattleSceneManager.battleTextArray3[0].SetActive(value: true);
		utageBattleSceneManager.battleTextArray3[1].SetActive(value: true);
		utageBattleSceneManager.battleTextArray3[2].SetActive(active);
		utageBattleSceneManager.battleTextArray3[3].SetActive(active);
		string effectType = itemData.effectType;
		string text2 = effectType.Substring(0, 1).ToUpper();
		effectType = "SeSkill" + text2 + effectType.Substring(1);
		MasterAudio.PlaySound(effectType, 1f, null, 0f, null, null);
		switch (text)
		{
		case "potion":
		case "allPotion":
		case "revive":
			Transition(healLink);
			break;
		case "medicine":
			Transition(medicLink);
			break;
		case "mpPotion":
		case "allMpPotion":
			Transition(mpHealLink);
			break;
		case "elixir":
			Transition(hpMpHealLink);
			break;
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

	private void CalcHealSkill()
	{
		if (scenarioBattleTurnManager.isAllTargetSkill)
		{
			utageBattleSceneManager.battleTextArray3[1].GetComponent<Localize>().Term = "battleTextEffectHpAverage";
		}
		else
		{
			utageBattleSceneManager.battleTextArray3[1].GetComponent<Localize>().Term = "battleTextEffectHp";
		}
		utageBattleSceneManager.battleTextArray3[3].GetComponent<Localize>().Term = "battleTextEffectHpHeal";
		for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
		{
			int memberNum = scenarioBattleTurnManager.skillAttackHitDataList[i].memberNum;
			Transform transform = utageBattleSceneManager.playerFrameGoList[memberNum].transform;
			utageBattleSceneManager.damagePointRect[i].position = new Vector3(transform.position.x, -2f, 0f);
			poolGoList.Add(PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[3], utageBattleSceneManager.damagePointRect[i]));
		}
		for (int j = 0; j < poolGoList.Count; j++)
		{
			poolGoList[j].GetComponent<TextMeshProUGUI>().text = scenarioBattleTurnManager.skillAttackHitDataList[j].healValue.ToString();
			PoolManager.Pools["BattleEffect"].Despawn(poolGoList[j], despawnTime, utageBattleSceneManager.poolManagerGO);
		}
	}

	private void CalcMedicSkill()
	{
		Localize component = utageBattleSceneManager.battleTextArray3[1].GetComponent<Localize>();
		switch (itemData.itemPower)
		{
		case 0:
			if (scenarioBattleTurnManager.isMedicineNoTarget)
			{
				component.Term = "battleTextEffectNoSoloTarget";
			}
			else
			{
				component.Term = "battleTextEffectMedicPoison";
			}
			break;
		case 1:
			if (scenarioBattleTurnManager.isMedicineNoTarget)
			{
				component.Term = "battleTextEffectNoSoloTarget";
			}
			else
			{
				component.Term = "battleTextEffectMedicPoison";
			}
			break;
		case 2:
			if (scenarioBattleTurnManager.isMedicineNoTarget)
			{
				component.Term = "battleTextEffectNoSoloTarget";
			}
			else
			{
				component.Term = "battleTextEffectMedicAll";
			}
			break;
		}
	}

	private void CalcReviveSkill()
	{
		utageBattleSceneManager.battleTextArray3[1].GetComponent<Localize>().Term = "battleTextEffectRevive";
		for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
		{
			int memberNum = scenarioBattleTurnManager.skillAttackHitDataList[i].memberNum;
			Transform transform = utageBattleSceneManager.playerFrameGoList[memberNum].transform;
			utageBattleSceneManager.damagePointRect[i].position = new Vector3(transform.position.x, -2f, 0f);
			poolGoList.Add(PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[3], utageBattleSceneManager.damagePointRect[i]));
		}
		for (int j = 0; j < poolGoList.Count; j++)
		{
			poolGoList[j].GetComponent<TextMeshProUGUI>().text = scenarioBattleTurnManager.skillAttackHitDataList[j].healValue.ToString();
			PoolManager.Pools["BattleEffect"].Despawn(poolGoList[j], despawnTime, utageBattleSceneManager.poolManagerGO);
		}
	}

	private void CalcMpHealSkill()
	{
		if (scenarioBattleTurnManager.isAllTargetSkill)
		{
			utageBattleSceneManager.battleTextArray3[1].GetComponent<Localize>().Term = "battleTextEffectMpAverage";
		}
		else
		{
			utageBattleSceneManager.battleTextArray3[1].GetComponent<Localize>().Term = "battleTextEffectMp";
		}
		utageBattleSceneManager.battleTextArray3[3].GetComponent<Localize>().Term = "battleTextEffectMpHeal";
		for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
		{
			int memberNum = scenarioBattleTurnManager.skillAttackHitDataList[i].memberNum;
			Transform transform = utageBattleSceneManager.playerFrameGoList[memberNum].transform;
			utageBattleSceneManager.damagePointRect[i].position = new Vector3(transform.position.x, -2f, 0f);
			poolGoList.Add(PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[3], utageBattleSceneManager.damagePointRect[i]));
		}
		for (int j = 0; j < poolGoList.Count; j++)
		{
			poolGoList[j].GetComponent<TextMeshProUGUI>().text = scenarioBattleTurnManager.skillAttackHitDataList[j].healValue.ToString();
			PoolManager.Pools["BattleEffect"].Despawn(poolGoList[j], despawnTime, utageBattleSceneManager.poolManagerGO);
		}
	}

	private void CalcHpMpHealSkill()
	{
		if (scenarioBattleTurnManager.isAllTargetSkill)
		{
			utageBattleSceneManager.battleTextArray3[1].GetComponent<Localize>().Term = "battleTextEffectHpMpAverage";
		}
		else
		{
			utageBattleSceneManager.battleTextArray3[1].GetComponent<Localize>().Term = "battleTextEffectHpMp";
		}
		utageBattleSceneManager.battleTextArray3[3].GetComponent<Localize>().Term = "battleTextEffectHpMpHeal";
		for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
		{
			int memberNum = scenarioBattleTurnManager.skillAttackHitDataList[i].memberNum;
			Transform transform = utageBattleSceneManager.playerFrameGoList[memberNum].transform;
			utageBattleSceneManager.damagePointRect[i].position = new Vector3(transform.position.x, -2f, 0f);
			poolGoList.Add(PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[3], utageBattleSceneManager.damagePointRect[i]));
		}
		for (int j = 0; j < poolGoList.Count; j++)
		{
			poolGoList[j].GetComponent<TextMeshProUGUI>().text = scenarioBattleTurnManager.skillAttackHitDataList[j].healValue.ToString();
			PoolManager.Pools["BattleEffect"].Despawn(poolGoList[j], despawnTime, utageBattleSceneManager.poolManagerGO);
		}
	}
}
