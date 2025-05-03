using Arbor;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class BattleMpSpSliderAnimation : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ParameterContainer parameterContainer;

	private int characterID;

	private HaveWeaponData weaponData;

	private SliderAndTmpText weaponMpGroup;

	private SliderAndTmpText mpGroup;

	private SliderAndTmpText spGroup;

	public float animationTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		parameterContainer = GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		float num = animationTime / (float)utageBattleSceneManager.battleSpeed;
		characterID = parameterContainer.GetInt("characterID");
		mpGroup = parameterContainer.GetVariable<SliderAndTmpText>("mpGroup");
		if (characterID == 0)
		{
			weaponMpGroup = parameterContainer.GetVariable<SliderAndTmpText>("weaponMpGroup");
			int weaponID = PlayerEquipDataManager.playerEquipWeaponID[0];
			weaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == weaponID && data.equipCharacter == 0);
			weaponMpGroup.slider.DOValue(weaponData.weaponIncludeMp, num).SetEase(Ease.Linear);
			Debug.Log("武器防具のMP変更／キャラID：" + characterID);
		}
		else
		{
			spGroup = parameterContainer.GetVariable<SliderAndTmpText>("spGroup");
			spGroup.slider.DOValue(PlayerStatusDataManager.characterSp[characterID], num).SetEase(Ease.Linear);
			Debug.Log("SP変更／キャラID：" + characterID);
		}
		mpGroup.slider.DOValue(PlayerStatusDataManager.characterMp[characterID], num).SetEase(Ease.Linear);
		float time = num + 0.1f;
		Invoke("InvokeMethod", time);
	}

	public override void OnStateEnd()
	{
		if (characterID == 0)
		{
			weaponMpGroup.textMeshProUGUI.text = weaponData.weaponIncludeMp.ToString();
		}
		else
		{
			spGroup.textMeshProUGUI.text = PlayerStatusDataManager.characterSp[characterID].ToString();
		}
		mpGroup.textMeshProUGUI.text = PlayerStatusDataManager.characterMp[characterID].ToString();
	}

	public override void OnStateUpdate()
	{
		SliderSyncText();
	}

	public override void OnStateLateUpdate()
	{
	}

	private void SliderSyncText()
	{
		if (characterID == 0)
		{
			weaponMpGroup.textMeshProUGUI.text = weaponMpGroup.slider.value.ToString();
		}
		else
		{
			spGroup.textMeshProUGUI.text = spGroup.slider.value.ToString();
		}
		mpGroup.textMeshProUGUI.text = mpGroup.slider.value.ToString();
	}

	private void InvokeMethod()
	{
		Transition(stateLink);
	}
}
