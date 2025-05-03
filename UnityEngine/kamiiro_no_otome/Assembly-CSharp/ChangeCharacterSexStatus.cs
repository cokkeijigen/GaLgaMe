using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ChangeCharacterSexStatus : StateBehaviour
{
	private StatusManager statusManager;

	private SexStatusManager sexStatusManager;

	private int nextSexLv;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
		sexStatusManager = GameObject.Find("Sex Status Manager").GetComponent<SexStatusManager>();
	}

	public override void OnStateBegin()
	{
		int selectCharacterNum = statusManager.selectCharacterNum;
		Debug.Log("えっちステータス表示：" + statusManager.selectCharacterNum);
		if (selectCharacterNum == 0)
		{
			sexStatusManager.characterSexStatusTextArray[0].text = PlayerSexStatusDataManager.playerSexLv.ToString();
			nextSexLv = PlayerSexStatusDataManager.playerSexLv + 1;
			sexStatusManager.characterSexStatusLocArray[0].Term = "statusEcstasyLimitNum";
			sexStatusManager.characterSexStatusLocArray[1].Term = "statusTotalBeHeldMouthCount";
			sexStatusManager.characterSexStatusLocArray[2].Term = "statusTotalCumShotCount";
			sexStatusManager.characterSexStatusTextArray[13].text = PlayerSexStatusDataManager.playerSexExp.ToString();
		}
		else
		{
			sexStatusManager.characterSexStatusTextArray[0].text = PlayerSexStatusDataManager.heroineSexLv[selectCharacterNum - 1].ToString();
			nextSexLv = PlayerSexStatusDataManager.heroineSexLv[selectCharacterNum - 1] + 1;
			sexStatusManager.characterSexStatusLocArray[0].Term = "statusFemaleEcstasyLimitNum";
			sexStatusManager.characterSexStatusLocArray[1].Term = "statusTotalMouthCount";
			sexStatusManager.characterSexStatusLocArray[2].Term = "statusTotalEcstasyCount";
			sexStatusManager.characterSexStatusTextArray[13].text = PlayerSexStatusDataManager.heroineSexExp[selectCharacterNum - 1].ToString();
		}
		if (PlayerSexStatusDataManager.playerSexLv < 5)
		{
			sexStatusManager.characterSexStatusTextArray[14].text = PlayerSexStatusDataManager.playerSexNextLvExp[selectCharacterNum].ToString();
		}
		else
		{
			sexStatusManager.characterSexStatusTextArray[14].text = "9999";
		}
		nextSexLv = Mathf.Clamp(nextSexLv, 1, 5);
		int num = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[statusManager.selectCharacterNum].characterSexLvCapFlag[nextSexLv - 1];
		if (PlayerSexStatusDataManager.totalUniqueSexCount[statusManager.selectCharacterNum] >= num)
		{
			sexStatusManager.sexLvLockImageGo.SetActive(value: false);
		}
		else
		{
			sexStatusManager.sexLvLockImageGo.SetActive(value: true);
		}
		sexStatusManager.characterSexStatusTextArray[1].text = PlayerSexStatusDataManager.playerSexHp[selectCharacterNum].ToString();
		sexStatusManager.characterSexStatusTextArray[2].text = PlayerSexStatusDataManager.playerSexAttack[selectCharacterNum].ToString();
		sexStatusManager.characterSexStatusTextArray[3].text = PlayerSexStatusDataManager.playerSexHealPower[selectCharacterNum].ToString();
		sexStatusManager.characterSexStatusTextArray[4].text = PlayerSexStatusDataManager.playerSexSensitivity[selectCharacterNum].ToString();
		sexStatusManager.characterSexStatusTextArray[5].text = PlayerSexStatusDataManager.playerSexCritical[selectCharacterNum].ToString();
		sexStatusManager.characterSexStatusTextArray[6].text = PlayerSexStatusDataManager.totalPistonCount[selectCharacterNum].ToString();
		sexStatusManager.characterSexStatusTextArray[7].text = PlayerSexStatusDataManager.totalMouthCount[selectCharacterNum].ToString();
		sexStatusManager.characterSexStatusTextArray[8].text = PlayerSexStatusDataManager.totalOutShotCount[selectCharacterNum].ToString();
		sexStatusManager.characterSexStatusTextArray[9].text = PlayerSexStatusDataManager.totalInShotCount[selectCharacterNum].ToString();
		sexStatusManager.characterSexStatusTextArray[10].text = PlayerSexStatusDataManager.totalEcstasyCount[selectCharacterNum].ToString();
		sexStatusManager.characterSexStatusTextArray[11].text = PlayerSexStatusDataManager.totalSexCount[selectCharacterNum].ToString();
		int num2 = 0;
		num2 = ((selectCharacterNum != 0) ? GameDataManager.instance.characterStatusDataBase.characterStatusDataList[selectCharacterNum].characterSexExtasyLimitNum[PlayerSexStatusDataManager.heroineSexLv[selectCharacterNum - 1] - 1] : GameDataManager.instance.characterStatusDataBase.characterStatusDataList[selectCharacterNum].characterSexExtasyLimitNum[PlayerSexStatusDataManager.playerSexLv - 1]);
		sexStatusManager.characterSexStatusTextArray[12].text = num2.ToString();
		statusManager.characterNameTextLoc.Term = "character" + selectCharacterNum;
		statusManager.characterImage.sprite = sexStatusManager.characterSexSpriteArray[selectCharacterNum];
		statusManager.characterImage.SetNativeSize();
		statusManager.characterBackGroundImage.sprite = sexStatusManager.characterSexBgSpriteArray[selectCharacterNum];
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
