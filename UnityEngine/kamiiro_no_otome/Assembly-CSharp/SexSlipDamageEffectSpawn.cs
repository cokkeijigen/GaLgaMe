using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class SexSlipDamageEffectSpawn : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	private SexBattleEffectManager sexBattleEffectManager;

	public float despawnTime;

	public bool zoomEffectEnable;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		sexBattleEffectManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleEffectManager>();
	}

	public override void OnStateBegin()
	{
		float time = 0f;
		float duration = 0f;
		RectTransform rectTransform = null;
		switch (sexBattleManager.battleSpeed)
		{
		case 1:
			time = 0.4f;
			duration = 0.15f;
			break;
		case 2:
			time = 0.3f;
			duration = 0.12f;
			break;
		case 4:
			time = 0.2f;
			duration = 0.09f;
			break;
		}
		if (zoomEffectEnable)
		{
			DOTween.Sequence().Append(sexBattleManager.sexTouchCamera.DOOrthoSize(5.3f, duration).SetEase(Ease.Linear)).Append(sexBattleManager.sexTouchCamera.DOOrthoSize(5.4f, duration))
				.SetEase(Ease.Linear)
				.SetRecyclable();
		}
		rectTransform = sexBattleManager.effectPrefabParentDictionary["vagina"];
		sexBattleManager.SetHeroineSprite("piston");
		MasterAudio.PlaySound("SexBattle_Piston_Middle", 1f, null, 0f, null, null);
		GameObject effectPrefabGo = GameDataManager.instance.skillEffectDataBase.skillEffectDataList.Find((SkillEffectData data) => data.effectName == "sexPistonExplosion_Middle").effectPrefabGo;
		Transform transform = PoolManager.Pools["sexSkillPool"].Spawn(effectPrefabGo, rectTransform.transform);
		sexBattleEffectManager.SetEffectDeSpawnReserve(transform.transform, isSkillPool: true, despawnTime);
		Invoke("InvokeMethod", time);
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
