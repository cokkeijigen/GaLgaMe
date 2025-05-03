using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class ChangeTouchBodyCategory : StateBehaviour
{
	private SexTouchManager sexTouchManager;

	private SexTouchHeroineDataManager sexTouchHeroineDataManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexTouchManager = GameObject.Find("Sex Touch Manager").GetComponent<SexTouchManager>();
		sexTouchHeroineDataManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchHeroineDataManager>();
	}

	public override void OnStateBegin()
	{
		sexTouchManager.SetBodyCategoryButtonInteractable();
		sexTouchManager.bodyClickInfoWindowGo.SetActive(value: false);
		for (int i = 0; i < sexTouchManager.skillWindowIsCloseArray.Length; i++)
		{
			sexTouchManager.skillWindowIsCloseArray[i] = false;
		}
		Sprite sprite = null;
		SexTouchBgSpriteData sexTouchBgSpriteData = sexTouchManager.sexTouchBgDataBase.sexTouchBgSpriteDataList.Find((SexTouchBgSpriteData data) => data.placeName == PlayerDataManager.currentPlaceName);
		switch (sexTouchManager.selectBodyCategoryNum)
		{
		case 0:
			sexTouchManager.touchHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexTouchHeroineDataManager.sexTouchHeroineSpriteData.heroineCgHeadBaseList[0];
			switch (PlayerDataManager.currentTimeZone)
			{
			case 0:
			case 1:
				sprite = sexTouchBgSpriteData.sexTouchBgFaceList[0];
				break;
			case 2:
				sprite = sexTouchBgSpriteData.sexTouchBgFaceList[1];
				break;
			case 3:
				sprite = sexTouchBgSpriteData.sexTouchBgFaceList[2];
				break;
			}
			sexTouchManager.touchBgSpriteGo.GetComponent<SpriteRenderer>().sprite = sprite;
			break;
		case 1:
			sexTouchManager.touchHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexTouchHeroineDataManager.sexTouchHeroineSpriteData.heroineCgUpperBaseList[0];
			switch (PlayerDataManager.currentTimeZone)
			{
			case 0:
			case 1:
				sprite = sexTouchBgSpriteData.sexTouchBgUpperList[0];
				break;
			case 2:
				sprite = sexTouchBgSpriteData.sexTouchBgUpperList[1];
				break;
			case 3:
				sprite = sexTouchBgSpriteData.sexTouchBgUpperList[2];
				break;
			}
			sexTouchManager.touchBgSpriteGo.GetComponent<SpriteRenderer>().sprite = sprite;
			break;
		case 2:
		{
			int num = 0;
			if (PlayerNonSaveDataManager.selectSexBattleHeroineId != 3)
			{
				num = ((PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[PlayerNonSaveDataManager.selectSexBattleHeroineId] > 0) ? 1 : 0);
			}
			else
			{
				int num2 = PlayerSexStatusDataManager.heroineVaginaLv[PlayerNonSaveDataManager.selectSexBattleHeroineId - 1];
				Debug.Log("シアのおまんこLV：" + num2);
				num = ((num2 == 1) ? ((PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[PlayerNonSaveDataManager.selectSexBattleHeroineId] <= 0) ? 2 : 3) : ((PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[PlayerNonSaveDataManager.selectSexBattleHeroineId] > 0) ? 1 : 0));
			}
			sexTouchManager.touchHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexTouchHeroineDataManager.sexTouchHeroineSpriteData.heroineCgLowerBaseList[num];
			switch (PlayerDataManager.currentTimeZone)
			{
			case 0:
			case 1:
				sprite = sexTouchBgSpriteData.sexTouchBgLowerList[0];
				break;
			case 2:
				sprite = sexTouchBgSpriteData.sexTouchBgLowerList[1];
				break;
			case 3:
				sprite = sexTouchBgSpriteData.sexTouchBgLowerList[2];
				break;
			}
			sexTouchManager.touchBgSpriteGo.GetComponent<SpriteRenderer>().sprite = sprite;
			break;
		}
		}
		for (int j = 0; j < 4; j++)
		{
			Transform[] array = new Transform[sexTouchManager.touchAreaPointGoArray[j].transform.childCount];
			if (array.Length != 0)
			{
				for (int k = 0; k < array.Length; k++)
				{
					array[k] = sexTouchManager.touchAreaPointGoArray[j].transform.GetChild(k);
				}
				for (int l = 0; l < array.Length; l++)
				{
					PoolManager.Pools["sexTouchPool"].Despawn(array[l], 0f, sexTouchManager.prefabParentTransform);
				}
			}
		}
		for (int m = 0; m < 3; m++)
		{
			Transform[] array2 = new Transform[sexTouchManager.skillGroupPrefabParentArray[m].childCount];
			for (int n = 0; n < array2.Length; n++)
			{
				array2[n] = sexTouchManager.skillGroupPrefabParentArray[m].GetChild(n);
			}
			for (int num3 = 0; num3 < array2.Length; num3++)
			{
				PoolManager.Pools["sexTouchPool"].Despawn(array2[num3], 0f, sexTouchManager.prefabParentTransform);
			}
		}
		sexTouchHeroineDataManager.SetBodyCategorySkillView(0, isInitialize: true);
		sexTouchManager.SetBodyAreaPoint();
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
