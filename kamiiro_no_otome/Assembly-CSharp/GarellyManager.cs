using System.Collections.Generic;
using Arbor;
using I2.Loc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GarellyManager : MonoBehaviour
{
	public ArborFSM arborFSM;

	public ArborFSM garellyCancelFSM;

	private PlayMakerFSM transitionFSM;

	public GameObject[] characterTabGoArray;

	public Sprite[] characterTabSprite;

	public GameObject garellyPageButtonGoParent;

	public List<GameObject> pageButtonGoList;

	public Sprite[] pageButtonSprite;

	public GameObject garellySlotGoParent;

	public ParameterContainer[] slotParamArray;

	public Sprite slotLockImageSprite;

	public GameObject garellyPoolParent;

	public GameObject[] poolSpawnGoArray;

	public GameObject dialogCanvasGo;

	public Localize[] dialogTermArray;

	public int selectTabNum;

	public int selectPageNum;

	private void Awake()
	{
		transitionFSM = GameObject.Find("Transition Manager").GetComponent<PlayMakerFSM>();
	}

	public void PushCharacterTab(int num)
	{
		selectTabNum = num;
		selectPageNum = 0;
		arborFSM.SendTrigger("PushCharacterTab");
	}

	public void PushPageButton(int num)
	{
		selectPageNum = num;
		arborFSM.SendTrigger("PushPageButton");
	}

	public void PushSlotButton(string sceneName)
	{
		PlayerNonSaveDataManager.selectScenarioName = sceneName;
		if (selectTabNum <= 3)
		{
			dialogTermArray[0].Term = "character" + (selectTabNum + 1);
		}
		else if (selectTabNum == 4)
		{
			dialogTermArray[0].Term = "characterCharlo";
		}
		else
		{
			dialogTermArray[0].Term = "characterOther";
		}
		if (sceneName.Contains("sexy"))
		{
			sceneName = sceneName.Replace("_sexy", "");
			Debug.Log("選択したシナリオはルーシーSexy：" + sceneName);
		}
		dialogTermArray[1].Term = "term_" + sceneName;
		dialogCanvasGo.SetActive(value: true);
		garellyCancelFSM.enabled = false;
	}

	public void PushDialogOkButton()
	{
		List<string> list = new List<string>();
		for (int i = 0; i < SceneManager.sceneCount; i++)
		{
			list.Add(SceneManager.GetSceneAt(i).name);
			Debug.Log(list[i]);
		}
		bool flag = list.Contains("title");
		Debug.Log("タイトルは存在する：" + flag);
		if (flag)
		{
			PlayerNonSaveDataManager.currentSceneName = "title";
			PlayerNonSaveDataManager.isGarellyOpenWithTitle = true;
			PlayerNonSaveDataManager.isUtageToJumpFromGarelly = true;
		}
		else
		{
			PlayerNonSaveDataManager.currentSceneName = "main";
			PlayerNonSaveDataManager.isGarellyOpenWithTitle = false;
			PlayerNonSaveDataManager.isUtageToJumpFromGarelly = true;
		}
		PlayerNonSaveDataManager.garellySelectTabNum = selectTabNum;
		PlayerNonSaveDataManager.garellySelectPageNum = selectPageNum;
		PlayerNonSaveDataManager.loadSceneName = "scenario";
		transitionFSM.SendEvent("StartFadeIn");
	}

	public void PushDialogCancelButton()
	{
		dialogCanvasGo.SetActive(value: false);
		garellyCancelFSM.enabled = true;
	}
}
