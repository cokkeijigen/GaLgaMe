using Arbor;
using DarkTonic.MasterAudio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class CheckSexResultLevelUp : StateBehaviour
{
	private ParameterContainer parameterContainer;

	private int characterID;

	private int upCount;

	private int currentLv;

	private int unLockFlagCount;

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
		if (characterID == 0)
		{
			currentLv = PlayerSexStatusDataManager.playerSexLv;
		}
		else
		{
			currentLv = PlayerSexStatusDataManager.heroineSexLv[PlayerNonSaveDataManager.selectSexBattleHeroineId - 1];
		}
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		if (!(expSlider.value >= expSlider.maxValue) || checkBool || currentLv >= 5)
		{
			return;
		}
		checkBool = true;
		currentLv++;
		upCount = parameterContainer.GetInt("levelUpCount") + 1;
		parameterContainer.SetInt("levelUpCount", upCount);
		int num = 0;
		int num2 = 0;
		if (characterID == 0)
		{
			if (PlayerSexStatusDataManager.playerSexLv + upCount >= 5)
			{
				num = 5800;
				num2 = 9999;
			}
			else
			{
				num = GameDataManager.instance.needExpDataBase.needSexLvExpList[PlayerSexStatusDataManager.playerSexLv + upCount - 1];
				num2 = GameDataManager.instance.needExpDataBase.needSexLvExpList[PlayerSexStatusDataManager.playerSexLv + upCount];
			}
			unLockFlagCount = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[0].characterSexLvCapFlag[PlayerSexStatusDataManager.playerSexLv + upCount - 1];
		}
		else
		{
			if (PlayerSexStatusDataManager.heroineSexLv[characterID - 1] + upCount >= 5)
			{
				num = 5800;
				num2 = 9999;
			}
			else
			{
				num = GameDataManager.instance.needExpDataBase.needSexLvExpList[PlayerSexStatusDataManager.heroineSexLv[characterID - 1] + upCount - 1];
				num2 = GameDataManager.instance.needExpDataBase.needSexLvExpList[PlayerSexStatusDataManager.heroineSexLv[characterID - 1] + upCount];
			}
			unLockFlagCount = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[characterID].characterSexLvCapFlag[PlayerSexStatusDataManager.heroineSexLv[characterID - 1] + upCount - 1];
		}
		expSlider.maxValue = num2;
		expSlider.minValue = num;
		if (PlayerSexStatusDataManager.totalUniqueSexCount[characterID] >= unLockFlagCount)
		{
			if (characterID == 0)
			{
				PlayerSexStatusDataManager.playerSexLv += upCount;
				lvText.text = PlayerSexStatusDataManager.playerSexLv.ToString();
			}
			else
			{
				PlayerSexStatusDataManager.heroineSexLv[characterID - 1] += upCount;
				lvText.text = PlayerSexStatusDataManager.heroineSexLv[characterID - 1].ToString();
			}
			MasterAudio.PlaySound("SeLevelUp", 1f, null, 0f, null, null);
			checkBool = false;
			Debug.Log("えっちLVアップ／必要ユニーク数：" + unLockFlagCount + "／現在のユニーク数：" + PlayerSexStatusDataManager.totalUniqueSexCount[characterID]);
		}
		else
		{
			Debug.Log("えっちLVはロック中／必要ユニーク数：" + unLockFlagCount + "／現在のユニーク数：" + PlayerSexStatusDataManager.totalUniqueSexCount[characterID]);
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
