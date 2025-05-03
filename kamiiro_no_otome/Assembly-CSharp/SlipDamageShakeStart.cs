using System.Collections.Generic;
using System.Linq;
using Arbor;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class SlipDamageShakeStart : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
	}

	public override void OnStateBegin()
	{
		new List<int>();
		new List<int>();
		float duration = 0.1f;
		float strength = 20f;
		int vibrato = 2;
		for (int i = 0; i < PlayerBattleConditionManager.playerIsDead.Count; i++)
		{
			if ((from ano in PlayerBattleConditionManager.playerBadState[i].Select((PlayerBattleConditionManager.MemberBadState con, int index) => new
				{
					Content = con,
					Index = index
				})
				where ano.Content.type == "poison"
				select ano.Index).ToList().Any())
			{
				int id = PlayerBattleConditionManager.playerIsDead[i].memberID;
				int memberNum = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == id).memberNum;
				utageBattleSceneManager.playerFrameGoList[memberNum].GetComponent<RectTransform>().DOShakeAnchorPos(duration, strength, vibrato, 10f, snapping: false, fadeOut: false);
			}
		}
		duration = 0.1f;
		strength = 20f;
		vibrato = 2;
		for (int j = 0; j < PlayerBattleConditionManager.enemyIsDead.Count; j++)
		{
			if ((from ano in PlayerBattleConditionManager.enemyBadState[j].Select((PlayerBattleConditionManager.MemberBadState con, int index) => new
				{
					Content = con,
					Index = index
				})
				where ano.Content.type == "poison"
				select ano.Index).ToList().Any())
			{
				utageBattleSceneManager.enemyImageGoList[j].GetComponent<RectTransform>().DOShakeAnchorPos(duration, strength, vibrato, 10f, snapping: false, fadeOut: false);
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
