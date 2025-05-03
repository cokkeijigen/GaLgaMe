using System.Collections.Generic;
using Arbor;
using Coffee.UIExtensions;
using DarkTonic.MasterAudio;
using PathologicalGames;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultDialogManager : SerializedMonoBehaviour
{
	public ArborFSM arborFSM;

	public GameObject resultCanvasGO;

	public Image resultBlackImage;

	public TextMeshProUGUI goldText;

	public TextMeshProUGUI expText;

	public TextMeshProUGUI bonusExpText;

	public GameObject bonusExpGroupGo;

	public GameObject characterImageParentGo;

	public GameObject characterImagePrefabGo;

	public List<GameObject> characterImageSpawnGoList;

	public List<Slider> expSliderList;

	public List<TextMeshProUGUI> lvTextList;

	public GameObject itemImageParentGo;

	public GameObject itemImagePrefabGo;

	public List<GameObject> itemImageSpawnGoList;

	public Toggle autoCloseToggle;

	public GameObject autoCloseGroupGo;

	public Image autoCloseImageFill;

	public Transform battleWinEffectPrefabGo;

	public Transform battleWinEffectSpawnGo;

	public UIParticle uIParticle;

	public Transform resultPoolParent;

	public bool isResultAnimationEnd;

	public bool isAutoCloseToggleInitialize;

	private void Awake()
	{
		resultCanvasGO.SetActive(value: false);
	}

	public void SpawnScenarioBattleWinEffect()
	{
		resultBlackImage.enabled = false;
	}

	public void SpawnDungeonBattleWinEffect()
	{
		resultBlackImage.enabled = true;
		if (!PlayerNonSaveDataManager.isScenarioBattle && !PlayerNonSaveDataManager.isDungeonScnearioBattle)
		{
			MasterAudio.PlaySound("SeScenarioBattleWin", 1f, null, 0f, null, null);
			Debug.Log("ダンジョンバトルの勝利SEを再生");
		}
	}

	public void DeSpawnBattleWinEffect()
	{
		if (PoolManager.Pools["ResultDialogPool"].IsSpawned(battleWinEffectSpawnGo))
		{
			PoolManager.Pools["ResultDialogPool"].Despawn(battleWinEffectSpawnGo, resultPoolParent);
		}
		uIParticle.RefreshParticles();
	}

	public void SetResultAutoClose()
	{
		if (isAutoCloseToggleInitialize)
		{
			MasterAudio.PlaySound("SeTabSwitch", 1f, null, 0f, null, null);
		}
		else
		{
			Debug.Log("初期化前なので効果音は鳴らない");
		}
		PlayerDataManager.isResultAutoClose = autoCloseToggle.isOn;
		Debug.Log("リザルト表示は終わっている？：" + isResultAnimationEnd);
		if (autoCloseToggle.isOn && isResultAnimationEnd)
		{
			arborFSM.SendTrigger("AutoCloseResult");
		}
	}

	public void DespawnResultGetItem()
	{
		if (characterImageParentGo.transform.childCount > 0)
		{
			foreach (GameObject characterImageSpawnGo in characterImageSpawnGoList)
			{
				characterImageSpawnGo.GetComponent<PlayMakerFSM>().enabled = false;
				PoolManager.Pools["ResultDialogPool"].Despawn(characterImageSpawnGo.transform, 0f, resultPoolParent);
			}
		}
		if (itemImageParentGo.transform.childCount > 0)
		{
			foreach (GameObject itemImageSpawnGo in itemImageSpawnGoList)
			{
				PoolManager.Pools["ResultDialogPool"].Despawn(itemImageSpawnGo.transform, 0f, resultPoolParent);
			}
		}
		if (PoolManager.Pools["ResultDialogPool"].IsSpawned(battleWinEffectSpawnGo))
		{
			PoolManager.Pools["ResultDialogPool"].Despawn(battleWinEffectSpawnGo, 0f, resultPoolParent);
		}
	}

	public void SetDropItemGroupUiElements()
	{
		ContentSizeFitter component = itemImageParentGo.GetComponent<ContentSizeFitter>();
		if (itemImageParentGo.transform.childCount <= 7)
		{
			itemImageParentGo.GetComponent<RectTransform>().offsetMax = new Vector2(760f, 0f);
			component.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
		}
		else
		{
			component.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
		}
	}
}
