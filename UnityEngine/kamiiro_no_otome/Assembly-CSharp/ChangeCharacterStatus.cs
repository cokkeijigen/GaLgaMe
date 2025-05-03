using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ChangeCharacterStatus : StateBehaviour
{
	private StatusManager statusManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
	}

	public override void OnStateBegin()
	{
		int selectCharacterNum = statusManager.selectCharacterNum;
		Debug.Log("ステータス表示：" + statusManager.selectCharacterNum);
		statusManager.characterStatusTextArray[0].text = PlayerStatusDataManager.characterLv[selectCharacterNum].ToString();
		statusManager.characterStatusTextArray[1].text = PlayerStatusDataManager.characterExp[selectCharacterNum].ToString();
		if (PlayerStatusDataManager.characterLv[selectCharacterNum] < 50)
		{
			statusManager.characterStatusTextArray[2].text = PlayerStatusDataManager.characterNextLvExp[selectCharacterNum].ToString();
		}
		else
		{
			statusManager.characterStatusTextArray[2].text = "999999";
		}
		statusManager.characterStatusTextArray[3].text = PlayerStatusDataManager.characterHp[selectCharacterNum].ToString();
		statusManager.characterStatusTextArray[18].text = PlayerStatusDataManager.characterMaxHp[selectCharacterNum].ToString();
		statusManager.characterStatusTextArray[4].text = PlayerStatusDataManager.characterMp[selectCharacterNum].ToString();
		statusManager.characterStatusTextArray[19].text = PlayerStatusDataManager.characterMaxMp[selectCharacterNum].ToString();
		statusManager.characterStatusTextArray[5].text = PlayerStatusDataManager.characterAttack[selectCharacterNum].ToString();
		statusManager.characterStatusTextArray[6].text = PlayerStatusDataManager.characterMagicAttack[selectCharacterNum].ToString();
		int num = Mathf.Clamp(PlayerStatusDataManager.characterDefense[selectCharacterNum], 0, 9999);
		statusManager.characterStatusTextArray[7].text = num.ToString();
		statusManager.characterStatusTextArray[8].text = PlayerStatusDataManager.characterMagicDefense[selectCharacterNum].ToString();
		statusManager.characterStatusTextArray[9].text = PlayerStatusDataManager.characterAccuracy[selectCharacterNum].ToString();
		statusManager.characterStatusTextArray[10].text = PlayerStatusDataManager.characterCritical[selectCharacterNum].ToString();
		statusManager.characterStatusTextArray[11].text = PlayerStatusDataManager.characterEvasion[selectCharacterNum].ToString();
		statusManager.characterStatusTextArray[12].text = PlayerEquipDataManager.equipFactorCriticalResist[selectCharacterNum].ToString();
		statusManager.characterStatusTextArray[13].text = PlayerStatusDataManager.characterAgility[selectCharacterNum].ToString();
		statusManager.characterStatusTextArray[14].text = PlayerStatusDataManager.characterMpRecoveryRate[selectCharacterNum].ToString();
		statusManager.characterStatusTextArray[15].text = PlayerStatusDataManager.characterResist[selectCharacterNum].ToString();
		if (selectCharacterNum == 0)
		{
			statusManager.characterStatusTextArray[16].gameObject.SetActive(value: false);
			statusManager.characterStatusTextArray[17].text = "";
		}
		else
		{
			statusManager.characterStatusTextArray[16].gameObject.SetActive(value: true);
			statusManager.characterStatusTextArray[17].text = PlayerStatusDataManager.characterComboProbability[selectCharacterNum].ToString();
		}
		statusManager.characterNameTextLoc.Term = "character" + selectCharacterNum + "_full";
		statusManager.characterImage.sprite = statusManager.characterSpriteArray[selectCharacterNum];
		statusManager.characterImage.SetNativeSize();
		statusManager.characterBackGroundImage.sprite = statusManager.characterBgSpriteArray[selectCharacterNum];
		statusManager.characterBackGroundImage.SetNativeSize();
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
