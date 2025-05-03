using System.Collections.Generic;
using System.Linq;
using Arbor;
using Coffee.UIExtensions;
using I2.Loc;
using PathologicalGames;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonBattleManager : SerializedMonoBehaviour
{
	public GameObject dungeonBattleCanvas;

	public GameObject commandGroup;

	public GameObject subGroup;

	public Image dungeonBattleBgImage;

	public CanvasGroup battleCommandCanvasGroup;

	public CanvasGroup battleSubButtonCanvasGroup;

	public GameObject chargetAttackButton;

	public GameObject itemButton;

	public Slider itemWaitSlider;

	public GameObject battleRetreatButtonGo;

	public GameObject skillEffectPoolParentGo;

	public UIParticle uIParticle_Charge;

	public GameObject chargeActiveEffectPrefabGo;

	public GameObject debuffSkillEffectPrefabGo;

	public Transform chargeActiveEffectSpawnGo;

	public GameObject[] chracterImageGoArray;

	public GameObject playerAgilityGo;

	public GameObject playerAgilityParent;

	public List<GameObject> playerAgilityGoList = new List<GameObject>();

	public ParameterContainer[] statusGroupArray;

	public Slider playerHpSlider;

	public TextMeshProUGUI playerHpText;

	public TextMeshProUGUI playerMaxHpText;

	public Slider playerSpSlider;

	public TextMeshProUGUI playerSpText;

	public GameObject enemyAgilityGo;

	public GameObject enemyAgilityParent;

	public List<GameObject> enemyAgilityGoList = new List<GameObject>();

	public Slider enemyHpSlider;

	public TextMeshProUGUI enemyHpText;

	public TextMeshProUGUI enemyMaxHpText;

	public Slider enemyChargeSlider;

	public TextMeshProUGUI enemyChargeText;

	public int enemyCharge;

	public ParameterContainer agilityContainer;

	public TextMeshProUGUI speedTmpGO;

	public GameObject skillNameFrameGo;

	public Localize skillNameTextLoc;

	public Image skillNameBorderImage;

	public GameObject messageWindowGO;

	public GameObject[] messageTextArray;

	public Localize[] messageWindowLoc;

	public GameObject[] poolEffectArray;

	public Transform poolManagerGO;

	public RectTransform[] damagePointRect;

	public List<bool> isCharacterAgilitySetUp;

	public List<bool> isEnemyAgilitySetUp;

	public BattleSkillData battleSkillData;

	public bool isRetreat;

	public bool isCriticalAttack;

	public bool isSkillAttack;

	public bool isSkillNameEffectEnd;

	public bool isAttackWeakHit;

	public bool isAttackResistHit;

	public void StopAglityCoroutine()
	{
		for (int i = 0; i < playerAgilityGoList.Count; i++)
		{
			playerAgilityGoList[i].GetComponent<DungeonCharacterAgility>().isCoroutineStop = true;
		}
		for (int j = 0; j < enemyAgilityGoList.Count; j++)
		{
			enemyAgilityGoList[j].GetComponent<DungeonCharacterAgility>().isCoroutineStop = true;
		}
		Debug.Log("Agilityコルーチン全部停止");
	}

	public void RestartAglityCoroutine()
	{
		for (int i = 0; i < playerAgilityGoList.Count; i++)
		{
			playerAgilityGoList[i].GetComponent<DungeonCharacterAgility>().isCoroutineStop = false;
		}
		for (int j = 0; j < enemyAgilityGoList.Count; j++)
		{
			enemyAgilityGoList[j].GetComponent<DungeonCharacterAgility>().isCoroutineStop = false;
		}
		Debug.Log("Agilityコルーチン全部再開");
	}

	public void SetBuffIcon(string type, int forceIndex, bool setValue, int power)
	{
		Sprite sprite = null;
		sprite = ((power <= 0) ? GameDataManager.instance.itemCategoryDataBase.deBuffIconDictionary[type] : GameDataManager.instance.itemCategoryDataBase.buffIconDictionary[type]);
		int index = statusGroupArray[forceIndex].GetStringList("buffTypeStringList").ToList().FindIndex((string data) => data == type);
		statusGroupArray[forceIndex].GetGameObjectList("buffImageGoList")[index].SetActive(setValue);
		statusGroupArray[forceIndex].GetGameObjectList("buffImageGoList")[index].GetComponent<Image>().sprite = sprite;
	}

	public void SetBadStateIcon(string type, int forceIndex, bool setValue)
	{
		int index = statusGroupArray[forceIndex].GetStringList("badStateTypeStringList").ToList().FindIndex((string data) => data == type);
		statusGroupArray[forceIndex].GetGameObjectList("badStateImageGoList")[index].SetActive(setValue);
	}

	public void SetAllStatusIconInVisible(int forceIndex)
	{
		IList<GameObject> gameObjectList = statusGroupArray[forceIndex].GetGameObjectList("buffImageGoList");
		for (int i = 0; i < gameObjectList.Count; i++)
		{
			gameObjectList[i].SetActive(value: false);
		}
		IList<GameObject> gameObjectList2 = statusGroupArray[forceIndex].GetGameObjectList("badStateImageGoList");
		for (int j = 0; j < gameObjectList2.Count; j++)
		{
			gameObjectList2[j].SetActive(value: false);
		}
	}

	public void PushChargeAttackButton()
	{
		agilityContainer.GetStringList("AgilityQueueList").Add("c0");
		GameObject effectPrefabGo = GameDataManager.instance.skillEffectDataBase.skillEffectDataList.Find((SkillEffectData data) => data.effectName == "simpleCircleGlowBlue").effectPrefabGo;
		damagePointRect[4].position = chargetAttackButton.GetComponent<RectTransform>().position;
		Transform transform = PoolManager.Pools["DungeonSkillEffect"].Spawn(effectPrefabGo, damagePointRect[4]);
		transform.localPosition = new Vector3(0f, 0f, 0f);
		transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
		PoolManager.Pools["DungeonSkillEffect"].Despawn(transform, 1f, skillEffectPoolParentGo.transform);
		chargetAttackButton.SetActive(value: false);
	}
}
