using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class SexSkillEffectSpawn : StateBehaviour
{
	public enum Type
	{
		player,
		heroine
	}

	private SexBattleManager sexBattleManager;

	private SexBattleEffectManager sexBattleEffectManager;

	private SexBattleTurnManager sexBattleTurnManager;

	public Type type;

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
		sexBattleTurnManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleTurnManager>();
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
		SexSkillData skillData = null;
		switch (type)
		{
		case Type.player:
			skillData = sexBattleManager.selectSexSkillData;
			sexBattleTurnManager.currentEdenSkillTypeName = skillData.actionType.ToString();
			break;
		case Type.heroine:
			skillData = sexBattleManager.heroineSexSkillData;
			break;
		}
		switch (skillData.skillType)
		{
		case SexSkillData.SkillType.sexAttack:
		case SexSkillData.SkillType.fertilize:
			if (zoomEffectEnable)
			{
				DOTween.Sequence().Append(sexBattleManager.sexTouchCamera.DOOrthoSize(5.3f, duration).SetEase(Ease.Linear)).Append(sexBattleManager.sexTouchCamera.DOOrthoSize(5.4f, duration))
					.SetEase(Ease.Linear)
					.SetRecyclable();
			}
			rectTransform = sexBattleManager.effectPrefabParentDictionary["vagina"];
			switch (type)
			{
			case Type.player:
				switch (skillData.narrativePartString)
				{
				case "piston":
					sexBattleManager.SetHeroineSprite("piston");
					MasterAudio.PlaySound("SexBattle_Piston_Middle", 1f, null, 0f, null, null);
					break;
				case "hardPiston":
					sexBattleManager.SetHeroineSprite("hardPiston");
					MasterAudio.PlaySound("SexBattle_Piston_High", 1f, null, 0f, null, null);
					break;
				case "gSpotPiston":
				case "portioPiston":
					sexBattleManager.SetHeroineSprite("hardPiston");
					MasterAudio.PlaySound("SexBattle_Piston_High", 1f, null, 0f, null, null);
					break;
				case "fertilizePiston":
					sexBattleManager.SetHeroineSprite("hardPiston");
					MasterAudio.PlaySound("SexBattle_Piston_High", 1f, null, 0f, null, null);
					break;
				}
				break;
			case Type.heroine:
				switch (skillData.narrativePartString)
				{
				case "voice":
				case "mooch":
					rectTransform = sexBattleManager.effectPrefabParentDictionary["head"];
					if (sexBattleTurnManager.currentEdenSkillTypeName == "piston")
					{
						sexBattleManager.SetHeroineSprite("piston");
						MasterAudio.PlaySound("SexBattle_Piston_Middle", 1f, null, 0f, null, null);
					}
					else
					{
						sexBattleManager.SetHeroineSprite("caress");
						MasterAudio.PlaySound("SexBattle_Caress", 1f, null, 0f, null, null);
					}
					break;
				case "kiss":
					rectTransform = sexBattleManager.effectPrefabParentDictionary["head"];
					sexBattleManager.SetHeroineSprite("kiss");
					break;
				case "hand":
				case "manNipple":
					if (sexBattleTurnManager.currentEdenSkillTypeName == "piston")
					{
						sexBattleManager.SetHeroineSprite("piston");
						MasterAudio.PlaySound("SexBattle_Piston_Middle", 1f, null, 0f, null, null);
					}
					else
					{
						sexBattleManager.SetHeroineSprite("caress");
						MasterAudio.PlaySound("SexBattle_Caress", 1f, null, 0f, null, null);
					}
					break;
				case "tits":
				case "nipple":
				case "lotion":
				case "vagina":
				case "anal":
					sexBattleManager.SetHeroineSprite("piston");
					MasterAudio.PlaySound("SexBattle_Piston_Middle", 1f, null, 0f, null, null);
					break;
				case "portio":
				case "hardMooch":
				case "report":
				case "hardMove":
				case "veryHardMove":
				case "ultimateHardMove":
					sexBattleManager.SetHeroineSprite("hardPiston");
					MasterAudio.PlaySound("SexBattle_Piston_High", 1f, null, 0f, null, null);
					break;
				}
				break;
			}
			break;
		case SexSkillData.SkillType.caress:
			if (zoomEffectEnable)
			{
				DOTween.Sequence().Append(sexBattleManager.sexTouchCamera.DOOrthoSize(5.3f, duration).SetEase(Ease.Linear)).Append(sexBattleManager.sexTouchCamera.DOOrthoSize(5.4f, duration))
					.SetEase(Ease.Linear)
					.SetRecyclable();
			}
			switch (skillData.narrativePartString)
			{
			case "kiss":
				rectTransform = sexBattleManager.effectPrefabParentDictionary["head"];
				sexBattleManager.SetHeroineSprite("kiss");
				break;
			case "tits":
			case "nipple":
				rectTransform = sexBattleManager.effectPrefabParentDictionary["tits"];
				sexBattleManager.SetHeroineSprite("caress");
				break;
			case "clitoris":
				rectTransform = sexBattleManager.effectPrefabParentDictionary["vagina"];
				sexBattleManager.SetHeroineSprite("caress");
				break;
			}
			MasterAudio.PlaySound("SexBattle_Caress", 1f, null, 0f, null, null);
			break;
		case SexSkillData.SkillType.heal:
			switch (skillData.narrativePartString)
			{
			case "voice":
				rectTransform = sexBattleManager.effectPrefabParentDictionary["head"];
				sexBattleManager.SetHeroineSprite("caress");
				MasterAudio.PlaySound("SexBattle_Heal", 1f, null, 0f, null, null);
				break;
			case "breath":
			case "calmness":
			case "concentration":
				rectTransform = sexBattleManager.effectPrefabParentDictionary["vagina"];
				sexBattleManager.SetHeroineSprite("caress");
				MasterAudio.PlaySound("SexBattle_Heal", 1f, null, 0f, null, null);
				break;
			}
			break;
		case SexSkillData.SkillType.buff:
		{
			string narrativePartString = skillData.narrativePartString;
			if (narrativePartString == "breath" || narrativePartString == "concentration")
			{
				rectTransform = sexBattleManager.effectPrefabParentDictionary["vagina"];
				if (sexBattleTurnManager.currentEdenSkillTypeName == "piston")
				{
					sexBattleManager.SetHeroineSprite("piston");
					MasterAudio.PlaySound("SexBattle_Piston_Middle", 1f, null, 0f, null, null);
				}
				else
				{
					sexBattleManager.SetHeroineSprite("caress");
					MasterAudio.PlaySound("SexBattle_Caress", 1f, null, 0f, null, null);
				}
			}
			break;
		}
		case SexSkillData.SkillType.none:
			rectTransform = sexBattleManager.effectPrefabParentDictionary["vagina"];
			sexBattleManager.SetHeroineSprite("absorb");
			break;
		}
		if (skillData.skillType != SexSkillData.SkillType.berserk)
		{
			skillData.effectType.ToString().Substring(0, 1).ToUpper();
			GameObject effectPrefabGo = GameDataManager.instance.skillEffectDataBase.skillEffectDataList.Find((SkillEffectData data) => data.effectName == skillData.effectType.ToString()).effectPrefabGo;
			Transform transform = PoolManager.Pools["sexSkillPool"].Spawn(effectPrefabGo, rectTransform.transform);
			sexBattleEffectManager.SetEffectDeSpawnReserve(transform.transform, isSkillPool: true, despawnTime);
		}
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
