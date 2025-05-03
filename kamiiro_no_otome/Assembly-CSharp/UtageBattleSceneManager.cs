using System.Collections.Generic;
using System.Linq;
using Arbor;
using Coffee.UIExtensions;
using DarkTonic.MasterAudio;
using PathologicalGames;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UtageBattleSceneManager : SerializedMonoBehaviour
{
	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	public ArborFSM supportAttackTurnFSM;

	public ArborFSM supportSkillTurnFSM;

	public ArborFSM supportHealTurnFSM;

	public GameObject battleCanvas;

	public GameObject interactiveGroup;

	public GameObject[] commandButtonGroup;

	public GameObject chargeAttackButton;

	public GameObject chargeAttackEffectGo;

	public GameObject itemButton;

	public Scrollbar skillScrollbar;

	public Scrollbar itemScrollbar;

	public Image battleBgImage;

	public List<GameObject> playerFrameGoList = new List<GameObject>();

	public Sprite[] playerFrameSprite;

	public GameObject playerFrameParent;

	public GameObject enemyImageGo;

	public GameObject enemyBigImageGo;

	public GameObject enemyImagePanel;

	public List<GameObject> enemyImageGoList = new List<GameObject>();

	public GameObject enemyButtonGo;

	public GameObject enemyGroupPanel;

	public List<GameObject> enemyButtonGoList = new List<GameObject>();

	public List<Sprite> enemyButtonSprite = new List<Sprite>();

	public GameObject battleTextPanel;

	public GameObject[] battleTextGroupArray;

	public GameObject battleTopText;

	public GameObject[] battleTextArray2;

	public GameObject[] battleTextArray3;

	public GameObject[] battleTextArray4;

	public UIParticle uIParticle_Charge;

	public GameObject chargeEffectPrefabGo;

	public Transform chargeEffectSpawnGo;

	public RectTransform chargeButtonPointRect;

	public RectTransform[] damagePointRect;

	public Transform effectSpawnPoint;

	public Transform effectSpawnParent;

	public GameObject[] poolEffectArray;

	public Transform poolManagerGO;

	public Transform poolSkillManagerGO;

	public int battleSpeed;

	public TextMeshProUGUI speedTmpGO;

	public List<bool> isCharacterButtonSetUp;

	public List<bool> isEnemyGroupSetUp;

	public List<bool> isCharacterHpGroupSetUp;

	public bool isStatusBackUp;

	public bool isRevengeBattle;

	public bool isEventBattle;

	public int supportAttackMemberId;

	private void Awake()
	{
		battleCanvas.SetActive(value: false);
	}

	private void Start()
	{
		scenarioBattleTurnManager = GetComponentInChildren<ScenarioBattleTurnManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
	}

	public void SetUpEnemyImage()
	{
		int i;
		for (i = 0; i < PlayerStatusDataManager.enemyMember.Length; i++)
		{
			Transform transform = null;
			transform = ((!(PlayerNonSaveDataManager.resultScenarioName == "MH_Shia_006-1") && !(PlayerNonSaveDataManager.resultScenarioName == "MH_Levy_013")) ? PoolManager.Pools["BattleObject"].Spawn(enemyImageGo, enemyImagePanel.transform) : PoolManager.Pools["BattleObject"].Spawn(enemyBigImageGo, enemyImagePanel.transform));
			Sprite enemyImageSprite = GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData id) => id.enemyID == PlayerStatusDataManager.enemyMember[i]).enemyImageSprite;
			transform.GetComponent<Image>().sprite = enemyImageSprite;
			transform.GetComponent<CanvasGroup>().interactable = true;
			transform.GetComponent<CanvasGroup>().alpha = 1f;
			transform.localScale = new Vector3(1f, 1f, 1f);
			enemyImageGoList.Add(transform.gameObject);
			ParameterContainer component = transform.GetComponent<ParameterContainer>();
			component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = "enemy" + PlayerStatusDataManager.enemyMember[i];
			component.GetVariable<TmpText>("selectNumText").textMeshProUGUI.text = (i + 1).ToString();
			component.SetInt("enemyPartyNum", i);
			ScenarioBattleData scenarioBattleData = GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData data) => data.scenarioName == PlayerNonSaveDataManager.resultScenarioName);
			bool active = scenarioBattleData.getMaterialIcon[i];
			component.GetGameObject("materialIconGo").SetActive(active);
			component.GetGameObject("TargetIconGo").SetActive(value: false);
			component.GetGameObject("TargetFrameGo").SetActive(value: false);
			if (PlayerStatusDataManager.enemyMember.Length == 1)
			{
				transform.GetComponent<RectTransform>().sizeDelta = new Vector2(scenarioBattleData.enemyCgSizeWidth, scenarioBattleData.enemyCgSizeHeight);
				Debug.Log("敵のサイズ変更：" + scenarioBattleData.enemyCgSizeWidth + "／" + scenarioBattleData.enemyCgSizeHeight);
			}
			else
			{
				transform.GetComponent<RectTransform>().sizeDelta = new Vector2(scenarioBattleData.enemyCgSizeWidth, scenarioBattleData.enemyCgSizeHeight);
				Debug.Log("敵のサイズ変更：" + scenarioBattleData.enemyCgSizeWidth + "／" + scenarioBattleData.enemyCgSizeHeight);
			}
			if (!(PlayerNonSaveDataManager.resultScenarioName != "MH_Levy_013"))
			{
				continue;
			}
			RectTransform component2 = component.GetGameObject("TargetFrameGo").GetComponent<RectTransform>();
			RectTransform component3 = component.GetGameObject("selectNameGroup").GetComponent<RectTransform>();
			float enemyCgSizeHeight = scenarioBattleData.enemyCgSizeHeight;
			if (enemyCgSizeHeight != 900f)
			{
				if (enemyCgSizeHeight == 1000f)
				{
					component2.anchoredPosition = new Vector2(0f, 770f);
					component3.anchoredPosition = new Vector2(0f, 770f);
				}
			}
			else
			{
				component2.anchoredPosition = new Vector2(0f, 720f);
				component3.anchoredPosition = new Vector2(0f, 720f);
			}
		}
		if (PlayerNonSaveDataManager.resultScenarioName == "Shrine1_50BossBattle")
		{
			enemyImagePanel.GetComponent<HorizontalLayoutGroup>().enabled = false;
			enemyImagePanel.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(480f, 350f);
			enemyImagePanel.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(1000f, 350f);
			enemyImagePanel.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector2(1500f, 350f);
			enemyImagePanel.transform.GetChild(1).SetAsLastSibling();
			enemyImagePanel.GetComponent<RectTransform>().sizeDelta = new Vector2(2010f, 700f);
		}
	}

	public void SetUpEnemyGroup()
	{
		isEnemyGroupSetUp.Clear();
		enemyButtonSprite.Clear();
		ScenarioBattleData scenarioBattleData = GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData data) => data.scenarioName == PlayerNonSaveDataManager.resultScenarioName);
		int i;
		for (i = 0; i < PlayerStatusDataManager.enemyMember.Length; i++)
		{
			Transform transform = PoolManager.Pools["BattleObject"].Spawn(enemyButtonGo, enemyGroupPanel.transform);
			int enemyID = GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData id) => id.enemyID == PlayerStatusDataManager.enemyMember[i]).enemyID;
			ParameterContainer component = transform.GetComponent<ParameterContainer>();
			Sprite enemyImageMiniSprite = GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData id) => id.enemyID == PlayerStatusDataManager.enemyMember[i]).enemyImageMiniSprite;
			int enemyChargeTurn = GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData id) => id.enemyID == PlayerStatusDataManager.enemyMember[i]).enemyChargeTurn;
			component.SetInt("enemyID", enemyID);
			component.SetInt("enemyPartyNum", i);
			component.SetInt("maxChargeNum", enemyChargeTurn);
			component.SetBool("isSelectEffect", value: false);
			component.GetVariable<TmpText>("selectNumberText").textMeshProUGUI.text = (i + 1).ToString();
			bool active = scenarioBattleData.getMaterialIcon[i];
			component.GetGameObject("materialIconGo").SetActive(active);
			transform.localScale = new Vector3(1f, 1f, 1f);
			enemyButtonSprite.Add(enemyImageMiniSprite);
			isEnemyGroupSetUp.Add(item: false);
			enemyButtonGoList.Add(transform.gameObject);
		}
	}

	public void SetEnemyTargetGroupVisble(bool isVisible)
	{
		if (scenarioBattleTurnManager.playerFocusTargetNum == 9)
		{
			return;
		}
		bool isDead = PlayerBattleConditionManager.enemyIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.playerFocusTargetNum).isDead;
		if (isVisible)
		{
			if (!isDead)
			{
				enemyImageGoList[scenarioBattleTurnManager.playerFocusTargetNum].GetComponent<ParameterContainer>().GetGameObject("TargetFrameGo").SetActive(value: true);
				Debug.Log("ターン終了時、集中攻撃フレームの表示を設定する");
			}
		}
		else if (!isDead)
		{
			enemyImageGoList[scenarioBattleTurnManager.playerFocusTargetNum].GetComponent<ParameterContainer>().GetGameObject("TargetFrameGo").SetActive(value: false);
			Debug.Log("ターン開始時、集中攻撃フレームの表示を設定する");
		}
	}

	public void SetPlayerFocusTarget(int num)
	{
		foreach (GameObject enemyButtonGo in enemyButtonGoList)
		{
			enemyButtonGo.transform.Find("Focus Icon").gameObject.SetActive(value: false);
		}
		foreach (GameObject enemyImageGo in enemyImageGoList)
		{
			enemyImageGo.GetComponent<ParameterContainer>().GetGameObject("TargetFrameGo").SetActive(value: false);
		}
		if (scenarioBattleTurnManager.playerFocusTargetNum == num)
		{
			scenarioBattleTurnManager.playerFocusTargetNum = 9;
			enemyButtonGoList[num].transform.Find("Focus Icon").gameObject.SetActive(value: false);
			enemyImageGoList[num].GetComponent<ParameterContainer>().GetGameObject("TargetIconGo").SetActive(value: false);
			enemyImageGoList[num].GetComponent<ParameterContainer>().GetGameObject("TargetFrameGo").SetActive(value: false);
		}
		else
		{
			scenarioBattleTurnManager.playerFocusTargetNum = num;
			enemyButtonGoList[num].transform.Find("Focus Icon").gameObject.SetActive(value: true);
			enemyImageGoList[num].GetComponent<ParameterContainer>().GetGameObject("TargetIconGo").SetActive(value: true);
			enemyImageGoList[num].GetComponent<ParameterContainer>().GetGameObject("TargetFrameGo").SetActive(value: true);
		}
	}

	public void PushFocusSelectNumberButton(int selectNumber)
	{
		if (selectNumber <= enemyButtonGoList.Count)
		{
			int num = selectNumber - 1;
			if (!PlayerBattleConditionManager.enemyIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == num).isDead)
			{
				MasterAudio.PlaySound("SeMiniButton", 1f, null, 0f, null, null);
				SetPlayerFocusTarget(num);
			}
		}
	}

	public void PushSkillSelectNumberButton(int selectNumber)
	{
		if (selectNumber <= enemyButtonGoList.Count)
		{
			int playerTargetNum = selectNumber - 1;
			MasterAudio.PlaySound("SeMiniButton", 1f, null, 0f, null, null);
			scenarioBattleTurnManager.playerTargetNum = playerTargetNum;
			scenarioBattleSkillManager.skillFSM.SendTrigger("SelectEnemyFrameButton");
		}
	}

	public void SetBuffIcon(string buffType, int num, bool targetForce, bool setVisible, bool isPositive)
	{
		new List<string>();
		List<GameObject> list = new List<GameObject>();
		int num2 = 0;
		if (targetForce)
		{
			num2 = playerFrameGoList[num].GetComponent<ParameterContainer>().GetStringList("buffTypeStringList").ToList()
				.FindIndex((string data) => data == buffType);
			list = playerFrameGoList[num].GetComponent<ParameterContainer>().GetGameObjectList("buffImageGoList").ToList();
		}
		else
		{
			num2 = enemyButtonGoList[num].GetComponent<ParameterContainer>().GetStringList("buffTypeStringList").ToList()
				.FindIndex((string data) => data == buffType);
			list = enemyButtonGoList[num].GetComponent<ParameterContainer>().GetGameObjectList("buffImageGoList").ToList();
		}
		Debug.Log("バフアイコンを表示or非表示する／num：" + num + "／Index：" + num2);
		PlayMakerFSM component = list[num2].GetComponent<PlayMakerFSM>();
		if (isPositive)
		{
			list[num2].GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.buffIconDictionary[buffType];
			component.FsmVariables.GetFsmBool("isBuff").Value = true;
		}
		else
		{
			list[num2].GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.deBuffIconDictionary[buffType];
			component.FsmVariables.GetFsmBool("isBuff").Value = false;
		}
		list[num2].SetActive(setVisible);
	}

	public void SetBadStateIcon(string badStateType, int num, bool targetForce, bool setVisible)
	{
		new List<string>();
		List<GameObject> list = new List<GameObject>();
		int num2 = 0;
		if (targetForce)
		{
			num2 = playerFrameGoList[num].GetComponent<ParameterContainer>().GetStringList("badStateTypeStringList").ToList()
				.FindIndex((string data) => data == badStateType);
			list = playerFrameGoList[num].GetComponent<ParameterContainer>().GetGameObjectList("badStateImageGoList").ToList();
		}
		else
		{
			num2 = enemyButtonGoList[num].GetComponent<ParameterContainer>().GetStringList("badStateTypeStringList").ToList()
				.FindIndex((string data) => data == badStateType);
			list = enemyButtonGoList[num].GetComponent<ParameterContainer>().GetGameObjectList("badStateImageGoList").ToList();
		}
		list[num2].SetActive(setVisible);
	}

	public void SetSelectFrameEnable(bool isEnable)
	{
		if (isEnable)
		{
			playerFrameParent.GetComponent<CanvasGroup>().interactable = true;
			playerFrameParent.GetComponent<CanvasGroup>().alpha = 1f;
			enemyGroupPanel.GetComponent<CanvasGroup>().interactable = true;
			enemyGroupPanel.GetComponent<CanvasGroup>().alpha = 1f;
		}
		else
		{
			playerFrameParent.GetComponent<CanvasGroup>().interactable = false;
			playerFrameParent.GetComponent<CanvasGroup>().alpha = 0.5f;
			enemyGroupPanel.GetComponent<CanvasGroup>().interactable = false;
			enemyGroupPanel.GetComponent<CanvasGroup>().alpha = 0.5f;
		}
	}
}
