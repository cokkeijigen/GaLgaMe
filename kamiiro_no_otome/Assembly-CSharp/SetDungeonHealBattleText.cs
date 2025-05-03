using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using PathologicalGames;
using TMPro;
using UnityEngine;

[AddComponentMenu("")]
public class SetDungeonHealBattleText : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	public float animationTime;

	public InputSlotAny inputItemData;

	private ItemData itemData;

	private int afterHP;

	private bool isHpChange;

	private Transform spawnGO;

	public float despawnTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = base.transform.parent.GetComponent<DungeonBattleManager>();
	}

	public override void OnStateBegin()
	{
		inputItemData.TryGetValue<ItemData>(out itemData);
		float duration = animationTime / (float)PlayerDataManager.dungeonBattleSpeed;
		isHpChange = false;
		string effectType = itemData.effectType;
		string text = effectType.Substring(0, 1).ToUpper();
		effectType = "SeSkill" + text + effectType.Substring(1);
		switch (itemData.category)
		{
		case ItemData.Category.potion:
		case ItemData.Category.allPotion:
		case ItemData.Category.elixir:
			isHpChange = true;
			dungeonBattleManager.damagePointRect[1].anchoredPosition = new Vector2(450f, 400f);
			spawnGO = PoolManager.Pools["DungeonBattleEffect"].Spawn(dungeonBattleManager.poolEffectArray[3], dungeonBattleManager.damagePointRect[1]);
			spawnGO.GetComponent<TextMeshProUGUI>().fontSize = 110f;
			MasterAudio.PlaySound(effectType, 1f, null, 0f, null, null);
			afterHP = Mathf.Clamp(PlayerStatusDataManager.playerAllHp + itemData.itemPower, 0, PlayerStatusDataManager.playerAllMaxHp);
			dungeonBattleManager.playerHpSlider.DOValue(afterHP, duration).SetEase(Ease.Linear).OnComplete(delegate
			{
				PlayerStatusDataManager.playerAllHp = afterHP;
				Transition(stateLink);
			});
			spawnGO.GetComponent<TextMeshProUGUI>().text = itemData.itemPower.ToString();
			spawnGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
			PoolManager.Pools["DungeonBattleEffect"].Despawn(spawnGO, despawnTime, dungeonBattleManager.poolManagerGO);
			break;
		case ItemData.Category.medicine:
			MasterAudio.PlaySound(effectType, 1f, null, 0f, null, null);
			switch (itemData.itemPower)
			{
			case 0:
			{
				Debug.Log("解毒薬を使用");
				PlayerBattleConditionManager.MemberBadState memberBadState3 = PlayerBattleConditionManager.playerBadState[0].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "poison");
				if (memberBadState3 != null && memberBadState3.continutyTurn > 0)
				{
					PlayerBattleConditionManager.playerBadState[0].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "poison").continutyTurn = 0;
					dungeonBattleManager.SetBadStateIcon("poison", 0, setValue: false);
				}
				break;
			}
			case 1:
			{
				Debug.Log("麻痺解除薬を使用");
				PlayerBattleConditionManager.MemberBadState memberBadState4 = PlayerBattleConditionManager.playerBadState[0].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "paralyze");
				if (memberBadState4 != null && memberBadState4.continutyTurn > 0)
				{
					PlayerBattleConditionManager.playerBadState[0].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "paralyze").continutyTurn = 0;
					dungeonBattleManager.SetBadStateIcon("paralyze", 0, setValue: false);
				}
				break;
			}
			case 2:
			{
				Debug.Log("全治療薬を使用");
				PlayerBattleConditionManager.MemberBadState memberBadState = PlayerBattleConditionManager.playerBadState[0].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "poison");
				if (memberBadState != null && memberBadState.continutyTurn > 0)
				{
					PlayerBattleConditionManager.playerBadState[0].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "poison").continutyTurn = 0;
					dungeonBattleManager.SetBadStateIcon("poison", 0, setValue: false);
				}
				PlayerBattleConditionManager.MemberBadState memberBadState2 = PlayerBattleConditionManager.playerBadState[0].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "paralyze");
				if (memberBadState2 != null && memberBadState2.continutyTurn > 0)
				{
					PlayerBattleConditionManager.playerBadState[0].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "paralyze").continutyTurn = 0;
					dungeonBattleManager.SetBadStateIcon("paralyze", 0, setValue: false);
				}
				break;
			}
			}
			break;
		}
		float time = 0.4f / (float)PlayerDataManager.dungeonBattleSpeed;
		Invoke("InvokeMethod", time);
	}

	public override void OnStateEnd()
	{
		if (isHpChange)
		{
			dungeonBattleManager.playerHpText.text = afterHP.ToString();
		}
	}

	public override void OnStateUpdate()
	{
		if (isHpChange)
		{
			dungeonBattleManager.playerHpText.text = Mathf.Floor(dungeonBattleManager.playerHpSlider.value).ToString();
		}
	}

	public override void OnStateLateUpdate()
	{
	}

	private void InvokeMethod()
	{
		Transition(stateLink);
	}
}
