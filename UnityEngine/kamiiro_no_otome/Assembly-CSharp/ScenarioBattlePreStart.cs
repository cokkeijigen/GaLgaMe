using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class ScenarioBattlePreStart : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
	}

	public override void OnStateBegin()
	{
		PlayerNonSaveDataManager.isScenarioBattle = true;
		PoolManager.Pools["BattleEffect"].DespawnAll();
		PoolManager.Pools["SkillEffect"].DespawnAll();
		if (utageBattleSceneManager.playerFrameParent.transform.childCount > 0)
		{
			GameObject[] array = new GameObject[utageBattleSceneManager.playerFrameParent.transform.childCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = utageBattleSceneManager.playerFrameParent.transform.GetChild(i).gameObject;
			}
			for (int j = 0; j < array.Length; j++)
			{
				PoolManager.Pools["BattleObject"].Despawn(array[j].transform, 0f, utageBattleSceneManager.poolManagerGO);
			}
		}
		if (utageBattleSceneManager.enemyGroupPanel.transform.childCount > 0)
		{
			GameObject[] array2 = new GameObject[utageBattleSceneManager.enemyGroupPanel.transform.childCount];
			for (int k = 0; k < array2.Length; k++)
			{
				array2[k] = utageBattleSceneManager.enemyGroupPanel.transform.GetChild(k).gameObject;
			}
			for (int l = 0; l < array2.Length; l++)
			{
				PoolManager.Pools["BattleObject"].Despawn(array2[l].transform, 0f, utageBattleSceneManager.poolManagerGO);
			}
		}
		if (utageBattleSceneManager.enemyImagePanel.transform.childCount > 0)
		{
			GameObject[] array3 = new GameObject[utageBattleSceneManager.enemyImagePanel.transform.childCount];
			for (int m = 0; m < array3.Length; m++)
			{
				array3[m] = utageBattleSceneManager.enemyImagePanel.transform.GetChild(m).gameObject;
			}
			for (int n = 0; n < array3.Length; n++)
			{
				PoolManager.Pools["BattleObject"].Despawn(array3[n].transform, 0f, utageBattleSceneManager.poolManagerGO);
			}
		}
		utageBattleSceneManager.battleSpeed = PlayerDataManager.scenarioBattleSpeed;
		utageBattleSceneManager.speedTmpGO.text = PlayerDataManager.scenarioBattleSpeed.ToString();
		utageBattleSceneManager.battleCanvas.SetActive(value: true);
		utageBattleSceneManager.interactiveGroup.SetActive(value: true);
		utageBattleSceneManager.battleCanvas.GetComponent<CanvasGroup>().interactable = false;
		utageBattleSceneManager.skillScrollbar.value = 0f;
		utageBattleSceneManager.itemScrollbar.value = 0f;
		utageBattleSceneManager.battleTopText.SetActive(value: false);
		GameObject[] battleTextArray = utageBattleSceneManager.battleTextArray2;
		for (int num = 0; num < battleTextArray.Length; num++)
		{
			battleTextArray[num].SetActive(value: false);
		}
		battleTextArray = utageBattleSceneManager.battleTextArray3;
		for (int num = 0; num < battleTextArray.Length; num++)
		{
			battleTextArray[num].SetActive(value: false);
		}
		battleTextArray = utageBattleSceneManager.battleTextArray4;
		for (int num = 0; num < battleTextArray.Length; num++)
		{
			battleTextArray[num].SetActive(value: false);
		}
		utageBattleSceneManager.battleTextPanel.SetActive(value: false);
		battleTextArray = utageBattleSceneManager.commandButtonGroup;
		for (int num = 0; num < battleTextArray.Length; num++)
		{
			battleTextArray[num].SetActive(value: true);
		}
		PlayerNonSaveDataManager.battleResultDialogType = "scenarioBattle";
		ScenarioBattleData scenarioBattleData = GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData data) => data.scenarioName == PlayerNonSaveDataManager.resultScenarioName);
		switch (PlayerDataManager.currentTimeZone)
		{
		case 0:
		case 1:
			utageBattleSceneManager.battleBgImage.sprite = scenarioBattleData.battleBgSprite[0];
			break;
		case 2:
			utageBattleSceneManager.battleBgImage.sprite = scenarioBattleData.battleBgSprite[1];
			break;
		case 3:
			utageBattleSceneManager.battleBgImage.sprite = scenarioBattleData.battleBgSprite[2];
			break;
		}
		utageBattleSceneManager.battleBgImage.rectTransform.localPosition = new Vector3(scenarioBattleData.battleBgRectPosX, scenarioBattleData.battleBgRectPosY, 0f);
		utageBattleSceneManager.battleBgImage.rectTransform.localScale = new Vector3(scenarioBattleData.battleBgRectScale, scenarioBattleData.battleBgRectScale, 1f);
		utageBattleSceneManager.isEventBattle = scenarioBattleData.isEventBattle;
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
