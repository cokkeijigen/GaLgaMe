using System.Collections.Generic;
using Arbor;
using I2.Loc;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonMapStatusManager : SerializedMonoBehaviour
{
	private DungeonMapManager dungeonMapManager;

	public Slider playerHpSlider;

	public TextMeshProUGUI playerHpText;

	public TextMeshProUGUI playerMaxHpText;

	public Slider playerSpSlider;

	public TextMeshProUGUI playerSpText;

	public Slider playerTpSlider;

	public TextMeshProUGUI playerTpText;

	public TextMeshProUGUI playerMaxTpText;

	public int needSkipTp;

	public TextMeshProUGUI skipInfoTpNumText;

	public GameObject skipInfoWindowGo;

	public Localize skipInfoTextLoc;

	public GameObject skipInfoTextGroup;

	public GameObject skipInfoDisableTextGroup;

	public CanvasGroup tpSkipButtonCanvasGroup;

	public GameObject tpSkipButtonCheckImageGo;

	public GameObject playerLibidoGroupGo;

	public TextMeshProUGUI playerLibidoText;

	public bool isTpSkipEnable;

	public string playerStatusRefreshType;

	public List<bool> isPlayerStatusViewSetUp;

	public int beforePlayerStatusValue;

	public GameObject playerExpGo;

	public GameObject playerExpParent;

	public List<GameObject> playerExpGoList = new List<GameObject>();

	public List<bool> isExpFrameSetUp;

	public List<bool> isAgilityFrameSetUp;

	public int dungeonBuffAttack;

	public int dungeonBuffDefense;

	public int dungeonDeBuffAgility;

	public int dungeonDeBuffAgiityRemainFloor;

	public int dungeonBuffRetreat;

	public GameObject[] dungeonBuffFrameArray;

	public Text[] dungeonBuffSymbolTextArray;

	public Text[] dungeonBuffNumTextArray;

	public GameObject[] dungeonBattleBuffFrameArray;

	public Text[] dungeonBattleBuffSymbolTextArray;

	public Text[] dungeonBattleBuffNumTextArray;

	private void Awake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
	}

	public void PointerEnterTpSkipButton()
	{
		if (dungeonMapManager.isBossRouteSelect)
		{
			skipInfoTextGroup.SetActive(value: false);
			skipInfoDisableTextGroup.SetActive(value: true);
			skipInfoWindowGo.SetActive(value: true);
		}
		else
		{
			skipInfoTextGroup.SetActive(value: true);
			skipInfoDisableTextGroup.SetActive(value: false);
			skipInfoTpNumText.text = needSkipTp.ToString();
			skipInfoWindowGo.SetActive(value: true);
		}
	}

	public void PointerExitFromTpSkipButton()
	{
		skipInfoWindowGo.SetActive(value: false);
	}

	public void ClearTotalNeedTp()
	{
		needSkipTp = 0;
		skipInfoTpNumText.text = needSkipTp.ToString();
		skipInfoTextLoc.Term = "dungeonTpSkipInfo";
		skipInfoTpNumText.color = Color.white;
		tpSkipButtonCanvasGroup.interactable = true;
		tpSkipButtonCanvasGroup.alpha = 1f;
		SetMiniCardTpFrameColor();
	}

	public void CheckTotalNeedTp()
	{
		needSkipTp = 0;
		if (!dungeonMapManager.isBossRouteSelect)
		{
			foreach (DungeonSelectCardData selectCard in dungeonMapManager.selectCardList)
			{
				needSkipTp += selectCard.needSkipTp;
			}
		}
		skipInfoTpNumText.text = needSkipTp.ToString();
		int weaponID = PlayerEquipDataManager.playerEquipWeaponID[0];
		int weaponIncludeMp = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == weaponID && data.equipCharacter == 0).weaponIncludeMp;
		Debug.Log("武器のTP：" + weaponIncludeMp);
		if (weaponIncludeMp >= needSkipTp && !dungeonMapManager.isBossRouteSelect)
		{
			Debug.Log("TPは足りている＆ボスルートではない");
			tpSkipButtonCanvasGroup.interactable = true;
			tpSkipButtonCanvasGroup.alpha = 1f;
			skipInfoTextLoc.Term = "dungeonTpSkipInfo";
			skipInfoTpNumText.color = Color.white;
		}
		else if (dungeonMapManager.isBossRouteSelect)
		{
			Debug.Log("ボスルート選択中");
			tpSkipButtonCanvasGroup.interactable = false;
			tpSkipButtonCanvasGroup.alpha = 0.5f;
			isTpSkipEnable = false;
			tpSkipButtonCheckImageGo.SetActive(value: false);
		}
		else
		{
			Debug.Log("TPが足りていない");
			tpSkipButtonCanvasGroup.interactable = false;
			tpSkipButtonCanvasGroup.alpha = 0.5f;
			skipInfoTextLoc.Term = "dungeonTpSkipInfoDisable";
			skipInfoTpNumText.color = Color.yellow;
			isTpSkipEnable = false;
			tpSkipButtonCheckImageGo.SetActive(value: false);
		}
		SetMiniCardTpFrameColor();
	}

	public void SetMiniCardTpFrameColor()
	{
		if (isTpSkipEnable)
		{
			foreach (DungeonSelectCardData miniCard in dungeonMapManager.miniCardList)
			{
				GameObject gameObject = miniCard.gameObject.GetComponent<ParameterContainer>().GetGameObject("skipTpNumFrame");
				TmpText variable = miniCard.gameObject.GetComponent<ParameterContainer>().GetVariable<TmpText>("skipTpNumText");
				gameObject.GetComponent<Image>().color = new Color(0.4f, 0.7f, 1f);
				variable.textMeshProUGUI.color = Color.white;
			}
			return;
		}
		foreach (DungeonSelectCardData miniCard2 in dungeonMapManager.miniCardList)
		{
			GameObject gameObject2 = miniCard2.gameObject.GetComponent<ParameterContainer>().GetGameObject("skipTpNumFrame");
			TmpText variable2 = miniCard2.gameObject.GetComponent<ParameterContainer>().GetVariable<TmpText>("skipTpNumText");
			gameObject2.GetComponent<Image>().color = new Color(0.39f, 0.39f, 0.39f);
			variable2.textMeshProUGUI.color = new Color(0.6f, 0.6f, 0.6f);
		}
	}

	public void PushTpSkipButton()
	{
		if (isTpSkipEnable)
		{
			isTpSkipEnable = false;
		}
		else
		{
			isTpSkipEnable = true;
		}
		tpSkipButtonCheckImageGo.SetActive(isTpSkipEnable);
		SetMiniCardTpFrameColor();
	}
}
