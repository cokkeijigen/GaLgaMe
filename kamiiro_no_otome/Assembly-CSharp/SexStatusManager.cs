using Arbor;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class SexStatusManager : MonoBehaviour
{
	private StatusManager statusManager;

	private StatusCustomManager statusCustomManager;

	public ArborFSM sexStatusFSM;

	public GameObject sexStatusButtonGo;

	public GameObject sexScheduleWindowGo;

	public GameObject sexStatusContentGO;

	public GameObject sexSkillScrollPrefabGo;

	public GameObject[] sexSkillTabButtonGoArray;

	public Localize[] sexSkillTabButtonLocArray;

	public GameObject[] sexSkillTypeButtonGoArray;

	public Sprite[] sexSkillTypeSpriteArray;

	public GameObject passiveAlertFrameGo;

	public ParameterContainer passiveParam;

	public ParameterContainer activeParam;

	public GameObject statusBodyHistoryGroupGo;

	public Button nextArrowButton;

	public Button beforeArrowButton;

	public GameObject[] sexStatusViewArray;

	public Localize[] characterSexStatusLocArray;

	public Text[] characterSexStatusTextArray;

	public GameObject sexLvLockImageGo;

	public GameObject sexLvLockAlertFrameGo;

	public Sprite[] characterSexSpriteArray;

	public Sprite[] characterSexBgSpriteArray;

	public ParameterContainer[] heroineScheduleParamArray;

	public Sprite[] heroineScheduleIconSpriteArray;

	public int selectSexSkillCharacterTabIndex;

	public int selectSexSkillScrollContentIndex;

	public int selectSexSkillId;

	public bool isSelectTypePassvie;

	public bool isPassiveButtonDisable;

	private void Awake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
		statusCustomManager = GameObject.Find("Status Custom Manager").GetComponentInParent<StatusCustomManager>();
	}

	private void Start()
	{
		bool active = false;
		int i;
		for (i = 1; i < 5; i++)
		{
			CharacterStatusData characterStatusData = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == i);
			if (PlayerFlagDataManager.scenarioFlagDictionary[characterStatusData.characterDungeonSexUnLockFlag])
			{
				active = true;
				break;
			}
		}
		sexStatusButtonGo.SetActive(active);
	}

	public void EnterMousePassiveButton()
	{
		if (statusManager.selectCharacterNum == 0)
		{
			passiveAlertFrameGo.SetActive(value: true);
		}
	}

	public void EnterMouseSexLvLockImage()
	{
		sexLvLockAlertFrameGo.SetActive(value: true);
	}

	public void PushSexSkillCharacterTab(int characterId)
	{
		statusManager.selectCharacterNum = characterId;
		switch (characterId)
		{
		case 0:
			selectSexSkillCharacterTabIndex = 0;
			break;
		case 2:
			selectSexSkillCharacterTabIndex = 1;
			break;
		case 3:
			selectSexSkillCharacterTabIndex = 2;
			break;
		case 4:
			selectSexSkillCharacterTabIndex = 3;
			break;
		}
		sexStatusFSM.SendTrigger("ChangeCharacter");
	}

	public void PushSexSkillTypeTab(bool isPassive)
	{
		isSelectTypePassvie = isPassive;
		sexStatusFSM.SendTrigger("PushSexSkillTypeTab");
	}

	public void PushBodyHistoryArrowButton(bool isNext)
	{
		if (isNext)
		{
			selectSexSkillId++;
		}
		else
		{
			selectSexSkillId--;
		}
		sexStatusFSM.SendTrigger("SendSexSkillListIndex");
	}
}
