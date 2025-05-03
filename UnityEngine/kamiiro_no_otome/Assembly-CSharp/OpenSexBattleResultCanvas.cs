using Arbor;
using DarkTonic.MasterAudio;
using PathologicalGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class OpenSexBattleResultCanvas : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	private SexBattleTurnManager sexBattleTurnManager;

	private SexBattleFertilizationManager sexBattleFertilizationManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		sexBattleTurnManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleTurnManager>();
		sexBattleFertilizationManager = GameObject.Find("SexBattle Fertilization Manager").GetComponent<SexBattleFertilizationManager>();
	}

	public override void OnStateBegin()
	{
		ResultReset();
		sexBattleManager.resultCanvasGo.SetActive(value: true);
		PlayerDataManager.playerLibido = 0;
		if (sexBattleManager.isFinishDone[0] || sexBattleManager.isFinishDone[2])
		{
			PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[PlayerNonSaveDataManager.selectSexBattleHeroineId] = PlayerSexStatusDataManager.remainingSemenDefaultValue;
		}
		sexBattleManager.ecstasyNumText.text = sexBattleTurnManager.sexBattleHeroineEcstasyCount.ToString();
		sexBattleManager.ecstasyTotalScoreText.text = (sexBattleTurnManager.sexBattleHeroineEcstasyCount * 10).ToString();
		if (sexBattleTurnManager.sexBattleTotalTurnCount == 0)
		{
			sexBattleTurnManager.sexBattleTotalTurnCount = 1;
		}
		float num = sexBattleTurnManager.sexBattleTotalHeroineTranceNum / sexBattleTurnManager.sexBattleTotalTurnCount;
		sexBattleManager.tranceMagNumText.text = num.ToString();
		int num2 = 0;
		int num3 = 0;
		if (sexBattleTurnManager.sexBattleHeroineEcstasyCount > 0)
		{
			if (num < 33f)
			{
				num2 = 50;
				sexBattleManager.resultStarImageArray[0].sprite = sexBattleManager.resultStarSpriteArray[0];
			}
			else if (num < 66f)
			{
				num2 = 100;
				sexBattleManager.resultStarImageArray[0].sprite = sexBattleManager.resultStarSpriteArray[0];
				sexBattleManager.resultStarImageArray[1].sprite = sexBattleManager.resultStarSpriteArray[0];
			}
			else
			{
				num2 = 200;
				sexBattleManager.resultStarImageArray[0].sprite = sexBattleManager.resultStarSpriteArray[0];
				sexBattleManager.resultStarImageArray[1].sprite = sexBattleManager.resultStarSpriteArray[0];
				sexBattleManager.resultStarImageArray[2].sprite = sexBattleManager.resultStarSpriteArray[0];
			}
		}
		else
		{
			num2 = 50;
			sexBattleManager.resultStarImageArray[0].sprite = sexBattleManager.resultStarSpriteArray[0];
		}
		sexBattleManager.tranceBonusNumText.text = num2.ToString();
		if (sexBattleFertilizationManager.isFertilizationSuccess)
		{
			num3 = 100;
			sexBattleManager.kizunaGroupLayoutElement.minHeight = 310f;
			sexBattleManager.fertilizeBonusGroup.SetActive(value: true);
		}
		else
		{
			sexBattleManager.kizunaGroupLayoutElement.minHeight = 280f;
			sexBattleManager.fertilizeBonusGroup.SetActive(value: false);
		}
		int num4 = sexBattleTurnManager.sexBattleHeroineEcstasyCount * 10;
		int num5 = num2 + num3 + num4;
		if (PlayerInventoryDataManager.haveEventItemList.Find((HaveEventItemData data) => data.itemID == 925) != null)
		{
			int num6 = Mathf.RoundToInt((float)num5 * 0.3f);
			Debug.Log("タリスマンボーナス：" + num6);
			sexBattleManager.talismanBonusGroup.SetActive(value: true);
			sexBattleManager.talismanBonusNumText.text = num6.ToString();
			num5 += num6;
			if (sexBattleFertilizationManager.isFertilizationSuccess)
			{
				sexBattleManager.kizunaGroupLayoutElement.minHeight = 336f;
			}
			else
			{
				sexBattleManager.kizunaGroupLayoutElement.minHeight = 310f;
			}
		}
		else
		{
			sexBattleManager.talismanBonusGroup.SetActive(value: false);
		}
		if (sexBattleManager.isSexBattleDefeat)
		{
			float num7 = 0f;
			num7 = ((sexBattleTurnManager.sexBattleHeroineEcstasyCount >= 1) ? ((float)num5 * 0.75f) : ((float)num5 * 0.5f));
			num5 = Mathf.RoundToInt(num7);
		}
		sexBattleManager.getTranceNumText.text = num5.ToString();
		PlayerDataManager.playerHaveKizunaPoint = Mathf.Clamp(PlayerDataManager.playerHaveKizunaPoint + num5, 0, 99999);
		for (int i = 0; i < 2; i++)
		{
			Transform transform = PoolManager.Pools["sexBattlePool"].Spawn(sexBattleManager.expPrefabGo, sexBattleManager.expPrefabSpawnParrentGo);
			if (i == 0)
			{
				int playerSexLv = PlayerSexStatusDataManager.playerSexLv;
				transform.transform.Find("Lv Frame/Lv Text").GetComponent<TextMeshProUGUI>().text = playerSexLv.ToString();
				transform.transform.Find("Mask/Face Image").GetComponent<Image>().sprite = GameDataManager.instance.playerResultFrameSprite[0];
				int playerSexExp = PlayerSexStatusDataManager.playerSexExp;
				Slider component = transform.transform.Find("Exp Slider").GetComponent<Slider>();
				component.maxValue = PlayerSexStatusDataManager.playerSexNextLvExp[0];
				component.minValue = PlayerSexStatusDataManager.playerSexCurrentLvExp[0];
				component.value = playerSexExp;
				transform.gameObject.GetComponent<ParameterContainer>().SetInt("characterID", 0);
				transform.gameObject.GetComponent<ParameterContainer>().SetInt("levelUpCount", 0);
			}
			else
			{
				int num8 = PlayerNonSaveDataManager.selectSexBattleHeroineId - 1;
				int num9 = PlayerSexStatusDataManager.heroineSexLv[num8];
				transform.transform.Find("Lv Frame/Lv Text").GetComponent<TextMeshProUGUI>().text = num9.ToString();
				transform.transform.Find("Mask/Face Image").GetComponent<Image>().sprite = GameDataManager.instance.playerResultFrameSprite[PlayerNonSaveDataManager.selectSexBattleHeroineId];
				int num10 = PlayerSexStatusDataManager.heroineSexExp[num8];
				Slider component2 = transform.transform.Find("Exp Slider").GetComponent<Slider>();
				component2.maxValue = PlayerSexStatusDataManager.playerSexNextLvExp[PlayerNonSaveDataManager.selectSexBattleHeroineId];
				component2.minValue = PlayerSexStatusDataManager.playerSexCurrentLvExp[PlayerNonSaveDataManager.selectSexBattleHeroineId];
				component2.value = num10;
				transform.gameObject.GetComponent<ParameterContainer>().SetInt("characterID", PlayerNonSaveDataManager.selectSexBattleHeroineId);
				transform.gameObject.GetComponent<ParameterContainer>().SetInt("levelUpCount", 0);
			}
			sexBattleManager.expPrefabSpawnGoList.Add(transform);
		}
		int num11 = (sexBattleTurnManager.sexBattleTotalTurnCount + 1) * 40 * PlayerSexStatusDataManager.heroineSexLv[PlayerNonSaveDataManager.selectSexBattleHeroineId - 1];
		int num12 = sexBattleTurnManager.sexBattleRemainCumShotCount * 10 * PlayerSexStatusDataManager.heroineSexLv[PlayerNonSaveDataManager.selectSexBattleHeroineId - 1];
		num11 = Mathf.Clamp(num11 + num12, 0, 99999);
		if (sexBattleFertilizationManager.isFertilizationSuccess)
		{
			num11 += 100;
		}
		sexBattleManager.getExpText.text = num11.ToString();
		if (sexBattleManager.isSexBattleDefeat)
		{
			num11 = Mathf.RoundToInt((float)num11 * 0.7f);
		}
		PlayerSexStatusDataManager.AddPlayerSexExp(isHeroine: false, num11);
		PlayerSexStatusDataManager.AddPlayerSexExp(isHeroine: true, num11);
		Transform transform2 = PoolManager.Pools["sexTouchPool"].Spawn(sexBattleManager.resultEffectPrefabGo, sexBattleManager.uIParticleSex.transform);
		sexBattleManager.resultEffectSpawnGo = transform2;
		transform2.localPosition = new Vector3(0f, 0f, 0f);
		transform2.localScale = new Vector3(1f, 1f, 1f);
		sexBattleManager.uIParticleSex.RefreshParticles();
		MasterAudio.PlaySound("SeSexResult", 1f, null, 0f, null, null);
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

	private void ResultReset()
	{
		sexBattleManager.expPrefabSpawnGoList.Clear();
		for (int i = 0; i < sexBattleManager.resultStarImageArray.Length; i++)
		{
			sexBattleManager.resultStarImageArray[i].sprite = sexBattleManager.resultStarSpriteArray[1];
		}
		int childCount = sexBattleManager.expPrefabSpawnParrentGo.childCount;
		Transform[] array = new Transform[childCount];
		for (int j = 0; j < childCount; j++)
		{
			array[j] = sexBattleManager.expPrefabSpawnParrentGo.GetChild(j);
		}
		Transform[] array2 = array;
		foreach (Transform instance in array2)
		{
			PoolManager.Pools["sexBattlePool"].Despawn(instance, 0f, sexBattleManager.skillPrefabParent);
		}
	}
}
