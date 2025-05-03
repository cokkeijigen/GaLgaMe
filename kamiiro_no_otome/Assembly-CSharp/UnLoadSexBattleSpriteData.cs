using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class UnLoadSexBattleSpriteData : StateBehaviour
{
	private SexTouchHeroineDataManager sexTouchHeroineDataManager;

	private SexBattleManager sexBattleManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexTouchHeroineDataManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchHeroineDataManager>();
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
	}

	public override void OnStateBegin()
	{
		sexTouchHeroineDataManager.UnLoadHeroineSpriteData();
		sexBattleManager.UnLoadHeroineSpriteData();
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
