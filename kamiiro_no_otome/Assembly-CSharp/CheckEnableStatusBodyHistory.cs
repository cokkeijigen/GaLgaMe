using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckEnableStatusBodyHistory : StateBehaviour
{
	private StatusManager statusManager;

	private SexStatusManager sexStatusManager;

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
		if (sexStatusManager.isSelectTypePassvie)
		{
			int num = statusManager.selectCharacterNum - 1;
			int num2 = 0;
			string text = GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[num].sexHeroinePassiveDataList.Find((SexHeroinePassiveData data) => data.skillID == sexStatusManager.selectSexSkillId).bodyCategory.ToString();
			Debug.Log("ヒロインパッシブ選択部位：" + text);
			switch (text)
			{
			case "mouth":
				num2 = PlayerSexStatusDataManager.heroineMouthLv[num];
				break;
			case "tits":
				num2 = PlayerSexStatusDataManager.heroineTitsLv[num];
				break;
			case "nipple":
				num2 = PlayerSexStatusDataManager.heroineNippleLv[num];
				break;
			case "womb":
				num2 = PlayerSexStatusDataManager.heroineWombsLv[num];
				break;
			case "clitoris":
				num2 = PlayerSexStatusDataManager.heroineClitorisLv[num];
				break;
			case "vagina":
				num2 = PlayerSexStatusDataManager.heroineVaginaLv[num];
				break;
			case "anal":
				num2 = PlayerSexStatusDataManager.heroineAnalLv[num];
				break;
			}
			if (num2 >= 2 && text != "tits")
			{
				sexStatusManager.statusBodyHistoryGroupGo.SetActive(value: true);
				sexStatusManager.beforeArrowButton.interactable = true;
				sexStatusManager.nextArrowButton.interactable = true;
				int skillUnlockLv = GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[num].sexHeroinePassiveDataList.Find((SexHeroinePassiveData data) => data.skillID == sexStatusManager.selectSexSkillId).skillUnlockLv;
				if (skillUnlockLv == 1)
				{
					sexStatusManager.beforeArrowButton.interactable = false;
				}
				if (skillUnlockLv == num2 || skillUnlockLv == 3)
				{
					sexStatusManager.nextArrowButton.interactable = false;
				}
			}
			else
			{
				sexStatusManager.statusBodyHistoryGroupGo.SetActive(value: false);
			}
		}
		else
		{
			sexStatusManager.statusBodyHistoryGroupGo.SetActive(value: false);
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
}
