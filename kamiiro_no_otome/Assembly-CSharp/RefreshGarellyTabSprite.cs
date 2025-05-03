using Arbor;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class RefreshGarellyTabSprite : StateBehaviour
{
	private GarellyManager garellyManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		garellyManager = GameObject.Find("Garelly Manager").GetComponent<GarellyManager>();
	}

	public override void OnStateBegin()
	{
		garellyManager.pageButtonGoList.Clear();
		if (garellyManager.garellyPageButtonGoParent.transform.childCount > 0)
		{
			Transform[] array = new Transform[garellyManager.garellyPageButtonGoParent.transform.childCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = garellyManager.garellyPageButtonGoParent.transform.GetChild(i);
			}
			Transform[] array2 = array;
			foreach (Transform instance in array2)
			{
				PoolManager.Pools["garellyPool"].Despawn(instance, 0f, garellyManager.garellyPoolParent.transform);
			}
		}
		int count = GameDataManager.instance.sceneGarellyDataBase.sceneGarellyDataList[garellyManager.selectTabNum].garellyDataList.Count;
		count = Mathf.CeilToInt((float)count / 12f);
		Debug.Log("必要なページ数は：" + count);
		for (int k = 0; k < count; k++)
		{
			Transform transform = PoolManager.Pools["garellyPool"].Spawn(garellyManager.poolSpawnGoArray[0], garellyManager.garellyPageButtonGoParent.transform);
			transform.localScale = new Vector3(1f, 1f, 1f);
			garellyManager.pageButtonGoList.Add(transform.gameObject);
			ParameterContainer component = transform.GetComponent<ParameterContainer>();
			component.SetInt("pageNum", k);
			component.GetVariable<TmpText>("pageNumText").textMeshProUGUI.text = (k + 1).ToString();
		}
		GameObject[] characterTabGoArray = garellyManager.characterTabGoArray;
		for (int j = 0; j < characterTabGoArray.Length; j++)
		{
			characterTabGoArray[j].GetComponent<Image>().sprite = garellyManager.characterTabSprite[0];
		}
		foreach (GameObject pageButtonGo in garellyManager.pageButtonGoList)
		{
			pageButtonGo.GetComponent<Image>().sprite = garellyManager.pageButtonSprite[0];
		}
		garellyManager.characterTabGoArray[garellyManager.selectTabNum].GetComponent<Image>().sprite = garellyManager.characterTabSprite[1];
		garellyManager.pageButtonGoList[garellyManager.selectPageNum].GetComponent<Image>().sprite = garellyManager.pageButtonSprite[1];
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
