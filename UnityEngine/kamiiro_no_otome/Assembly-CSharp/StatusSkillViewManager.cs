using Coffee.UIExtensions;
using I2.Loc;
using PathologicalGames;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusSkillViewManager : SerializedMonoBehaviour
{
	private StatusManager statusManager;

	public GameObject[] skillCategoryTabArray;

	public Sprite[] skillCategoryTabSpriteArray;

	public Localize skillScrollMpTextLoc;

	public GameObject kizunaFrameGo;

	public Text kizunaPointText;

	public GameObject skillLearnButtonGO;

	public Localize skillLearnButtonLoc;

	public GameObject learnCostGroup;

	public TextMeshProUGUI learnCostText;

	public GameObject dialogCanvas;

	public Localize learnSkillNameLoc;

	public Localize learnSkillSummaryLoc;

	public GameObject[] dialogButtonArray;

	public GameObject poolParentGo;

	public GameObject learnedEffectPrefabGo;

	public UIParticle uIParticle;

	public Transform learnedEffectSpawnGo;

	public bool isSelectSkillLearnTab;

	public bool isLearned;

	private void Awake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
	}

	public void PushLearnButton()
	{
		learnSkillNameLoc.Term = "playerSkill" + statusManager.selectSkillId;
		learnSkillSummaryLoc.Term = "alertSkillLearn";
		dialogButtonArray[0].SetActive(value: true);
		dialogButtonArray[1].SetActive(value: false);
		dialogCanvas.SetActive(value: true);
	}

	public void PushLearnDialogOkButton()
	{
		statusManager.skillFSM.SendTrigger("PushLearnOkButton");
	}

	public void PushLearnDialogCancelButton()
	{
		dialogCanvas.SetActive(value: false);
	}

	public void PushLearnedDialogOkButton()
	{
		if (PoolManager.Pools["Status Custom Pool"].IsSpawned(learnedEffectSpawnGo))
		{
			PoolManager.Pools["Status Custom Pool"].Despawn(learnedEffectSpawnGo, 0f, poolParentGo.transform);
		}
		uIParticle.RefreshParticles();
		dialogCanvas.SetActive(value: false);
		isLearned = true;
		statusManager.skillFSM.SendTrigger("OpenSkillCanvas");
	}
}
