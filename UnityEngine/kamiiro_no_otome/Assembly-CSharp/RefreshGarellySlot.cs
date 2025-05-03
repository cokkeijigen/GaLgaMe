using Arbor;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class RefreshGarellySlot : StateBehaviour
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
		if (garellyManager.garellySlotGoParent.transform.childCount > 0)
		{
			Transform[] array = new Transform[garellyManager.garellySlotGoParent.transform.childCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = garellyManager.garellySlotGoParent.transform.GetChild(i);
			}
			Transform[] array2 = array;
			foreach (Transform instance in array2)
			{
				PoolManager.Pools["garellyPool"].Despawn(instance, 0f, garellyManager.garellyPoolParent.transform);
			}
		}
		int count = GameDataManager.instance.sceneGarellyDataBase.sceneGarellyDataList[garellyManager.selectTabNum].garellyDataList.Count;
		int selectPageNum = garellyManager.selectPageNum;
		count -= selectPageNum * 12;
		count = Mathf.Clamp(count, 0, 12);
		Debug.Log("必要なスロット数：" + count);
		for (int k = 0; k < count; k++)
		{
			Transform transform = PoolManager.Pools["garellyPool"].Spawn(garellyManager.poolSpawnGoArray[1], garellyManager.garellySlotGoParent.transform);
			transform.localScale = new Vector3(1f, 1f, 1f);
			int index = k + selectPageNum * 12;
			string sceneName = GameDataManager.instance.sceneGarellyDataBase.sceneGarellyDataList[garellyManager.selectTabNum].garellyDataList[index];
			Sprite thumbnailSprite = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Find((ScenarioFlagData data) => data.scenarioName == sceneName).thumbnailSprite;
			bool num = PlayerFlagDataManager.sceneGarellyFlagDictionary[sceneName];
			ParameterContainer component = transform.GetComponent<ParameterContainer>();
			Button component2 = transform.GetComponent<Button>();
			if (num)
			{
				component.GetVariable<I2LocalizeComponent>("headerTextLoc").localize.Term = "term_" + sceneName;
				component.GetVariable<UguiImage>("thumbnailImage").image.sprite = thumbnailSprite;
				component.SetString("sceneName", sceneName);
				component2.interactable = true;
			}
			else
			{
				component.GetVariable<I2LocalizeComponent>("headerTextLoc").localize.Term = "unKnown";
				component.GetVariable<UguiImage>("thumbnailImage").image.sprite = garellyManager.slotLockImageSprite;
				component.SetString("sceneName", sceneName);
				component2.interactable = false;
			}
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
