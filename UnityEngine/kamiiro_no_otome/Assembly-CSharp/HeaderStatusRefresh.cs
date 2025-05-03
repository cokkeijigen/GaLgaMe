using Arbor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class HeaderStatusRefresh : StateBehaviour
{
	private HeaderStatusManager headerStatusManager;

	private TotalMapAccessManager totalMapAccessManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		headerStatusManager = GetComponent<HeaderStatusManager>();
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
	}

	public override void OnStateBegin()
	{
		string text = $"{PlayerDataManager.playerHaveMoney:#,0}";
		headerStatusManager.moneyText.text = text;
		headerStatusManager.libidoText.text = PlayerDataManager.playerLibido.ToString();
		if (PlayerDataManager.rareDropRateRaisePowerNum > 0 && PlayerDataManager.rareDropRateRaiseRaimingDaysNum > 0)
		{
			headerStatusManager.rareDropPanelGo.SetActive(value: true);
			headerStatusManager.rareDropRemainingDaysText.text = PlayerDataManager.rareDropRateRaiseRaimingDaysNum.ToString();
		}
		else
		{
			headerStatusManager.rareDropPanelGo.SetActive(value: false);
		}
		if (PlayerDataManager.rareDropRateRaisePowerNum > 0 && PlayerDataManager.rareDropRateRaiseRaimingDaysNum > 0)
		{
			headerStatusManager.rareDropPanelGo.SetActive(value: true);
			headerStatusManager.rareDropRemainingDaysText.text = PlayerDataManager.rareDropRateRaiseRaimingDaysNum.ToString();
		}
		else
		{
			headerStatusManager.rareDropPanelGo.SetActive(value: false);
		}
		ParameterContainer component = headerStatusManager.partyGroupGoList[0].GetComponent<ParameterContainer>();
		component.GetGameObject("lvTmpGO").GetComponent<TextMeshProUGUI>().text = PlayerStatusDataManager.characterLv[0].ToString();
		_ = PlayerStatusDataManager.characterLv[0];
		SliderAndTmpText variable = component.GetVariable<SliderAndTmpText>("hpGroup");
		int num = PlayerStatusDataManager.characterMaxHp[0];
		int num2 = PlayerStatusDataManager.characterHp[0];
		variable.slider.maxValue = num;
		variable.slider.value = num2;
		variable.textMeshProUGUI.text = num2.ToString();
		SliderAndTmpMaxTextVariable variable2 = component.GetVariable<SliderAndTmpMaxTextVariable>("weaponGroup");
		int index = PlayerInventoryDataManager.haveWeaponList.FindIndex((HaveWeaponData m) => m.equipCharacter == 0);
		variable2.slider.maxValue = PlayerInventoryDataManager.haveWeaponList[index].weaponIncludeMaxMp;
		variable2.slider.value = PlayerInventoryDataManager.haveWeaponList[index].weaponIncludeMp;
		variable2.textMeshProUGUI1.text = PlayerInventoryDataManager.haveWeaponList[index].weaponIncludeMp.ToString();
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			headerStatusManager.partyGroupGoList[1].SetActive(value: true);
			HeroineStatusRefresh();
		}
		else
		{
			headerStatusManager.partyGroupGoList[1].SetActive(value: false);
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

	private void HeroineStatusRefresh()
	{
		ParameterContainer component = headerStatusManager.partyGroupGoList[1].GetComponent<ParameterContainer>();
		int dungeonHeroineFollowNum = PlayerDataManager.DungeonHeroineFollowNum;
		component.GetGameObject("lvTmpGO").GetComponent<TextMeshProUGUI>().text = PlayerStatusDataManager.characterLv[dungeonHeroineFollowNum].ToString();
		_ = PlayerStatusDataManager.characterLv[dungeonHeroineFollowNum];
		SliderAndTmpText variable = component.GetVariable<SliderAndTmpText>("hpGroup");
		int num = PlayerStatusDataManager.characterMaxHp[dungeonHeroineFollowNum];
		int num2 = PlayerStatusDataManager.characterHp[dungeonHeroineFollowNum];
		variable.slider.maxValue = num;
		variable.slider.value = num2;
		variable.textMeshProUGUI.text = num2.ToString();
		component.GetGameObject("characterImageGO").GetComponent<Image>().sprite = headerStatusManager.partyGroupSprite[dungeonHeroineFollowNum];
		totalMapAccessManager.localHeroineGo.GetComponent<SpriteRenderer>().sprite = headerStatusManager.heroineSpriteArrayList[PlayerDataManager.DungeonHeroineFollowNum - 1][0];
	}
}
