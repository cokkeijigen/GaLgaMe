using Arbor;
using DarkTonic.MasterAudio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class CheckResultLevelUp : StateBehaviour
{
	private ParameterContainer parameterContainer;

	private int characterID;

	private int upCount;

	public bool checkBool;

	private TextMeshProUGUI lvText;

	private Slider expSlider;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		parameterContainer = GetComponent<ParameterContainer>();
		expSlider = base.transform.Find("Exp Slider").GetComponent<Slider>();
		lvText = base.transform.Find("Lv Frame/Lv Text").GetComponent<TextMeshProUGUI>();
	}

	public override void OnStateBegin()
	{
		characterID = parameterContainer.GetInt("characterID");
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		if (expSlider.value >= expSlider.maxValue && !checkBool && PlayerStatusDataManager.characterLv[characterID] < 50)
		{
			checkBool = true;
			upCount = parameterContainer.GetInt("levelUpCount") + 1;
			parameterContainer.SetInt("levelUpCount", upCount);
			int num = GameDataManager.instance.needExpDataBase.needCharacterLvExpList[PlayerStatusDataManager.characterLv[characterID] + upCount - 1];
			int num2 = GameDataManager.instance.needExpDataBase.needCharacterLvExpList[PlayerStatusDataManager.characterLv[characterID] + upCount];
			expSlider.maxValue = num2;
			expSlider.minValue = num;
			int num3 = (PlayerStatusDataManager.characterLv[characterID] += upCount);
			PlayerStatusDataManager.characterLv[characterID] = num3;
			lvText.text = num3.ToString();
			Debug.Log("LVアップしたキャラID：" + characterID + "／新たなLV：" + num3);
			PlayerStatusDataManager.LvUpPlayerStatus(characterID, CallBackMethod);
		}
	}

	public override void OnStateLateUpdate()
	{
	}

	private void CallBackMethod()
	{
		checkBool = false;
		MasterAudio.PlaySound("SeLevelUp", 1f, null, 0f, null, null);
	}
}
