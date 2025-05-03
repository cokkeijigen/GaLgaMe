using Arbor;
using DarkTonic.MasterAudio;
using PathologicalGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class OpenSexTouchResultCanvas : StateBehaviour
{
	private SexTouchManager sexTouchManager;

	private SexBattleManager sexBattleManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexTouchManager = GameObject.Find("Sex Touch Manager").GetComponent<SexTouchManager>();
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
	}

	public override void OnStateBegin()
	{
		ResultReset();
		sexTouchManager.touchResultWindow.SetActive(value: true);
		PlayerDataManager.playerLibido = 0;
		int num = 30;
		sexTouchManager.defaultKizunaText.text = num.ToString();
		sexTouchManager.getKizunaText.text = num.ToString();
		PlayerDataManager.playerHaveKizunaPoint = Mathf.Clamp(PlayerDataManager.playerHaveKizunaPoint + num, 0, 99999);
		for (int i = 0; i < 2; i++)
		{
			Transform transform = PoolManager.Pools["sexBattlePool"].Spawn(sexTouchManager.touchExpPrefabGo, sexTouchManager.touchExpPrefabSpawnParentGo);
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
				int num2 = PlayerNonSaveDataManager.selectSexBattleHeroineId - 1;
				int num3 = PlayerSexStatusDataManager.heroineSexLv[num2];
				transform.transform.Find("Lv Frame/Lv Text").GetComponent<TextMeshProUGUI>().text = num3.ToString();
				transform.transform.Find("Mask/Face Image").GetComponent<Image>().sprite = GameDataManager.instance.playerResultFrameSprite[PlayerNonSaveDataManager.selectSexBattleHeroineId];
				int num4 = PlayerSexStatusDataManager.heroineSexExp[num2];
				Slider component2 = transform.transform.Find("Exp Slider").GetComponent<Slider>();
				component2.maxValue = PlayerSexStatusDataManager.playerSexNextLvExp[PlayerNonSaveDataManager.selectSexBattleHeroineId];
				component2.minValue = PlayerSexStatusDataManager.playerSexCurrentLvExp[PlayerNonSaveDataManager.selectSexBattleHeroineId];
				component2.value = num4;
				transform.gameObject.GetComponent<ParameterContainer>().SetInt("characterID", PlayerNonSaveDataManager.selectSexBattleHeroineId);
				transform.gameObject.GetComponent<ParameterContainer>().SetInt("levelUpCount", 0);
			}
			sexTouchManager.touchExpPrefabSpawnGoList.Add(transform);
		}
		int addExp = 200;
		sexTouchManager.getExpText.text = addExp.ToString();
		PlayerSexStatusDataManager.AddPlayerSexExp(isHeroine: false, addExp);
		PlayerSexStatusDataManager.AddPlayerSexExp(isHeroine: true, addExp);
		Transform transform2 = PoolManager.Pools["sexTouchPool"].Spawn(sexTouchManager.resultEffectPrefabGo, sexTouchManager.uIParticleTouch.transform);
		sexTouchManager.resultEffectSpawnGo = transform2;
		transform2.localPosition = new Vector3(0f, 0f, 0f);
		transform2.localScale = new Vector3(1f, 1f, 1f);
		sexTouchManager.uIParticleTouch.RefreshParticles();
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
		sexTouchManager.touchExpPrefabSpawnGoList.Clear();
		int childCount = sexTouchManager.touchExpPrefabSpawnParentGo.childCount;
		Transform[] array = new Transform[childCount];
		for (int i = 0; i < childCount; i++)
		{
			array[i] = sexTouchManager.touchExpPrefabSpawnParentGo.GetChild(i);
		}
		Transform[] array2 = array;
		foreach (Transform instance in array2)
		{
			PoolManager.Pools["sexBattlePool"].Despawn(instance, 0f, sexBattleManager.skillPrefabParent);
		}
	}
}
