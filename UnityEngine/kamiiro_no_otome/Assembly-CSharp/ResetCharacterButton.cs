using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class ResetCharacterButton : StateBehaviour
{
	private ParameterContainer parameterContainer;

	private UtageBattleSceneManager utageBattleSceneManager;

	public GameObject itemMpGroup;

	public GameObject mpGroup;

	public GameObject spGroup;

	public Image characterImage;

	public bool isInisitalizeFSM;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		parameterContainer = GetComponent<ParameterContainer>();
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
	}

	public override void OnStateBegin()
	{
		int @int = parameterContainer.GetInt("characterID");
		int int2 = parameterContainer.GetInt("partyMemberNum");
		if (isInisitalizeFSM)
		{
			PlayMakerFSM skillButtonFSM = parameterContainer.GetGameObject("statusGroupParentGo").GetComponent<BattleSkillButtonManagerForPM>().skillButtonFSM;
			skillButtonFSM.FsmVariables.GetFsmInt("characterId").Value = @int;
			skillButtonFSM.FsmVariables.GetFsmInt("memberNum").Value = int2;
			parameterContainer.GetGameObject("statusGroupParentGo").GetComponent<BattleSkillButtonManagerForPM>().SetSkillButtonVisible(@int);
		}
		parameterContainer.GetVariable<SliderAndTmpText>("hpGroup").slider.maxValue = PlayerStatusDataManager.characterMaxHp[parameterContainer.GetInt("characterID")];
		parameterContainer.GetVariable<SliderAndTmpText>("mpGroup").slider.maxValue = PlayerStatusDataManager.characterMaxMp[parameterContainer.GetInt("characterID")];
		if (@int == 0)
		{
			spGroup.SetActive(value: false);
			itemMpGroup.SetActive(value: true);
			HaveWeaponData item = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.equipCharacter == 0);
			int index = PlayerInventoryDataManager.haveWeaponList.IndexOf(item);
			parameterContainer.GetVariable<SliderAndTmpText>("weaponMpGroup").slider.maxValue = PlayerInventoryDataManager.haveWeaponList[index].weaponIncludeMaxMp;
			if (PlayerNonSaveDataManager.victoryScenarioName == "M_Main_001-1after")
			{
				mpGroup.SetActive(value: false);
				Debug.Log("MPスライダ非表示");
			}
			else
			{
				mpGroup.SetActive(value: true);
				Debug.Log("MPスライダ表示");
			}
		}
		else
		{
			mpGroup.SetActive(value: true);
			spGroup.SetActive(value: true);
			itemMpGroup.SetActive(value: false);
			Debug.Log("仲間：MPスライダ表示");
		}
		characterImage.sprite = utageBattleSceneManager.playerFrameSprite[parameterContainer.GetInt("characterID")];
		ResetBuffIcon();
		int num = PlayerStatusDataManager.characterHp[parameterContainer.GetInt("characterID")];
		int num2 = PlayerStatusDataManager.characterMp[parameterContainer.GetInt("characterID")];
		parameterContainer.GetVariable<SliderAndTmpText>("hpGroup").slider.value = num;
		parameterContainer.GetVariable<SliderAndTmpText>("hpGroup").textMeshProUGUI.text = num.ToString();
		parameterContainer.GetVariable<SliderAndTmpText>("mpGroup").slider.value = num2;
		parameterContainer.GetVariable<SliderAndTmpText>("mpGroup").textMeshProUGUI.text = num2.ToString();
		if (parameterContainer.GetInt("characterID") == 0)
		{
			int beforeEquipWeaponTp = PlayerNonSaveDataManager.beforeEquipWeaponTp;
			parameterContainer.GetVariable<SliderAndTmpText>("weaponMpGroup").slider.value = beforeEquipWeaponTp;
			parameterContainer.GetVariable<SliderAndTmpText>("weaponMpGroup").textMeshProUGUI.text = beforeEquipWeaponTp.ToString();
		}
		else
		{
			int num3 = PlayerStatusDataManager.characterSp[parameterContainer.GetInt("characterID")];
			parameterContainer.GetVariable<SliderAndTmpText>("spGroup").slider.value = num3;
			parameterContainer.GetVariable<SliderAndTmpText>("spGroup").textMeshProUGUI.text = num3.ToString();
		}
		characterImage.color = new Color(1f, 1f, 1f);
		GetComponent<CanvasGroup>().interactable = true;
		utageBattleSceneManager.isCharacterButtonSetUp[parameterContainer.GetInt("partyMemberNum")] = true;
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

	private void ResetBuffIcon()
	{
		foreach (GameObject gameObject in parameterContainer.GetGameObjectList("buffImageGoList"))
		{
			gameObject.SetActive(value: false);
		}
		foreach (GameObject gameObject2 in parameterContainer.GetGameObjectList("badStateImageGoList"))
		{
			gameObject2.SetActive(value: false);
		}
	}
}
