using System.Collections.Generic;
using Arbor;
using DarkTonic.MasterAudio;
using PathologicalGames;
using UnityEngine;

public class SexTouchHeroineDataManager : MonoBehaviour
{
	public SexTouchManager sexTouchManager;

	public DynamicSoundGroupCreator dynamicSoundGroup;

	public SexTouchHeroineSpriteData sexTouchHeroineSpriteData;

	public List<int> heroineHaveSexSkillMouthList;

	public List<int> heroineHaveSexSkillHandList;

	public List<int> heroineHaveSexSkillTitsList;

	public List<int> heroineHaveSexSkillNippleList;

	public List<int> heroineHaveSexSkillWombsList;

	public List<int> heroineHaveSexSkillClitorisList;

	public List<int> heroineHaveSexSkillVaginaList;

	public List<int> heroineHaveSexSkillAnalList;

	public List<int> heroineHaveSexPassiveMouthList;

	public List<int> heroineHaveSexPassiveHandList;

	public List<int> heroineHaveSexPassiveTitsList;

	public List<int> heroineHaveSexPassiveNippleList;

	public List<int> heroineHaveSexPassiveWombsList;

	public List<int> heroineHaveSexPassiveClitorisList;

	public List<int> heroineHaveSexPassiveVaginaList;

	public List<int> heroineHaveSexPassiveAnalList;

	public Sprite[] skillFrameSpriteArray;

	public void SetTouchHeroineCgToIdle()
	{
		switch (sexTouchManager.selectBodyCategoryNum)
		{
		case 0:
			sexTouchManager.touchHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexTouchHeroineSpriteData.heroineCgHeadBaseList[0];
			break;
		case 1:
			sexTouchManager.touchHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexTouchHeroineSpriteData.heroineCgUpperBaseList[0];
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
			sexTouchManager.touchHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexTouchHeroineSpriteData.heroineCgLowerBaseList[num];
			break;
		}
		}
	}

	public void SetBodyCategorySkillView(int index, bool isInitialize)
	{
		if (isInitialize)
		{
			for (int i = 0; i < 3; i++)
			{
				sexTouchManager.skillWindowButtonImageArray[i].sprite = sexTouchManager.skillWindowButtonSpriteArray[0];
				sexTouchManager.skillWindowButtonImageArray[i].GetComponent<PlayMakerFSM>().FsmVariables.GetFsmInt("categoryIndex").Value = i;
			}
		}
		else
		{
			sexTouchManager.skillWindowButtonImageArray[index].sprite = sexTouchManager.skillWindowButtonSpriteArray[0];
		}
		switch (sexTouchManager.selectBodyCategoryNum)
		{
		case 0:
			if (isInitialize)
			{
				sexTouchManager.heroineSkillWindowArray[1].SetActive(value: false);
				sexTouchManager.skillGroupHeaderTextLocArray[0].Term = "sexBodyCategory_mouth";
				sexTouchManager.skillGroupHeaderTextLocArray[2].Term = "sexBodyCategory_hand";
				GameObject[] skillGroupHeaderHideGroupArray = sexTouchManager.skillGroupHeaderHideGroupArray;
				for (int j = 0; j < skillGroupHeaderHideGroupArray.Length; j++)
				{
					skillGroupHeaderHideGroupArray[j].SetActive(value: true);
				}
				List<int> skillList13 = new List<int>(heroineHaveSexPassiveMouthList);
				SpawnPassiveSkillListPrefab(0, "mouth", skillList13);
				List<int> skillList14 = new List<int>(heroineHaveSexSkillMouthList);
				SpawnActiveSkillListPrefab(0, "mouth", skillList14);
				List<int> skillList15 = new List<int>(heroineHaveSexPassiveHandList);
				SpawnPassiveSkillListPrefab(2, "hand", skillList15);
				List<int> skillList16 = new List<int>(heroineHaveSexSkillHandList);
				SpawnActiveSkillListPrefab(2, "mouth", skillList16);
			}
			else
			{
				sexTouchManager.skillWindowIsCloseArray[index] = false;
				switch (index)
				{
				case 0:
				{
					List<int> skillList19 = new List<int>(heroineHaveSexPassiveMouthList);
					SpawnPassiveSkillListPrefab(0, "mouth", skillList19);
					List<int> skillList20 = new List<int>(heroineHaveSexSkillMouthList);
					SpawnActiveSkillListPrefab(0, "mouth", skillList20);
					break;
				}
				case 2:
				{
					List<int> skillList17 = new List<int>(heroineHaveSexPassiveHandList);
					SpawnPassiveSkillListPrefab(2, "hand", skillList17);
					List<int> skillList18 = new List<int>(heroineHaveSexSkillHandList);
					SpawnActiveSkillListPrefab(2, "hand", skillList18);
					break;
				}
				}
			}
			break;
		case 1:
			if (isInitialize)
			{
				sexTouchManager.heroineSkillWindowArray[1].SetActive(value: true);
				sexTouchManager.skillGroupHeaderTextLocArray[0].Term = "sexBodyCategory_tits";
				sexTouchManager.skillGroupHeaderTextLocArray[1].Term = "sexBodyCategory_nipple";
				sexTouchManager.skillGroupHeaderTextLocArray[2].Term = "sexBodyCategory_womb";
				GameObject[] skillGroupHeaderHideGroupArray = sexTouchManager.skillGroupHeaderHideGroupArray;
				for (int j = 0; j < skillGroupHeaderHideGroupArray.Length; j++)
				{
					skillGroupHeaderHideGroupArray[j].SetActive(value: true);
				}
				List<int> skillList21 = new List<int>(heroineHaveSexPassiveTitsList);
				SpawnPassiveSkillListPrefab(0, "tits", skillList21);
				List<int> skillList22 = new List<int>(heroineHaveSexSkillTitsList);
				SpawnActiveSkillListPrefab(0, "tits", skillList22);
				List<int> skillList23 = new List<int>(heroineHaveSexPassiveNippleList);
				SpawnPassiveSkillListPrefab(1, "nipple", skillList23);
				List<int> skillList24 = new List<int>(heroineHaveSexSkillNippleList);
				SpawnActiveSkillListPrefab(1, "nipple", skillList24);
				List<int> skillList25 = new List<int>(heroineHaveSexPassiveWombsList);
				SpawnPassiveSkillListPrefab(2, "womb", skillList25);
				List<int> skillList26 = new List<int>(heroineHaveSexSkillWombsList);
				SpawnActiveSkillListPrefab(2, "womb", skillList26);
			}
			else
			{
				sexTouchManager.skillWindowIsCloseArray[index] = false;
				switch (index)
				{
				case 0:
				{
					List<int> skillList31 = new List<int>(heroineHaveSexPassiveTitsList);
					SpawnPassiveSkillListPrefab(0, "tits", skillList31);
					List<int> skillList32 = new List<int>(heroineHaveSexSkillTitsList);
					SpawnActiveSkillListPrefab(0, "tits", skillList32);
					break;
				}
				case 1:
				{
					List<int> skillList29 = new List<int>(heroineHaveSexPassiveNippleList);
					SpawnPassiveSkillListPrefab(1, "nipple", skillList29);
					List<int> skillList30 = new List<int>(heroineHaveSexSkillNippleList);
					SpawnActiveSkillListPrefab(1, "nipple", skillList30);
					break;
				}
				case 2:
				{
					List<int> skillList27 = new List<int>(heroineHaveSexPassiveWombsList);
					SpawnPassiveSkillListPrefab(2, "womb", skillList27);
					List<int> skillList28 = new List<int>(heroineHaveSexSkillWombsList);
					SpawnActiveSkillListPrefab(2, "womb", skillList28);
					break;
				}
				}
			}
			break;
		case 2:
			if (isInitialize)
			{
				sexTouchManager.heroineSkillWindowArray[1].SetActive(value: true);
				sexTouchManager.skillGroupHeaderTextLocArray[0].Term = "sexBodyCategory_clitoris";
				sexTouchManager.skillGroupHeaderTextLocArray[1].Term = "sexBodyCategory_vagina";
				sexTouchManager.skillGroupHeaderTextLocArray[2].Term = "sexBodyCategory_anal";
				GameObject[] skillGroupHeaderHideGroupArray = sexTouchManager.skillGroupHeaderHideGroupArray;
				for (int j = 0; j < skillGroupHeaderHideGroupArray.Length; j++)
				{
					skillGroupHeaderHideGroupArray[j].SetActive(value: false);
				}
				List<int> skillList = new List<int>(heroineHaveSexPassiveClitorisList);
				SpawnPassiveSkillListPrefab(0, "clitoris", skillList);
				List<int> skillList2 = new List<int>(heroineHaveSexSkillClitorisList);
				SpawnActiveSkillListPrefab(0, "clitoris", skillList2);
				List<int> skillList3 = new List<int>(heroineHaveSexPassiveVaginaList);
				SpawnPassiveSkillListPrefab(1, "vagina", skillList3);
				List<int> skillList4 = new List<int>(heroineHaveSexSkillVaginaList);
				SpawnActiveSkillListPrefab(1, "vagina", skillList4);
				List<int> skillList5 = new List<int>(heroineHaveSexPassiveAnalList);
				SpawnPassiveSkillListPrefab(2, "anal", skillList5);
				List<int> skillList6 = new List<int>(heroineHaveSexSkillAnalList);
				SpawnActiveSkillListPrefab(2, "anal", skillList6);
			}
			else
			{
				sexTouchManager.skillWindowIsCloseArray[index] = false;
				switch (index)
				{
				case 0:
				{
					List<int> skillList11 = new List<int>(heroineHaveSexPassiveClitorisList);
					SpawnPassiveSkillListPrefab(0, "clitoris", skillList11);
					List<int> skillList12 = new List<int>(heroineHaveSexSkillClitorisList);
					SpawnActiveSkillListPrefab(0, "clitoris", skillList12);
					break;
				}
				case 1:
				{
					List<int> skillList9 = new List<int>(heroineHaveSexPassiveVaginaList);
					SpawnPassiveSkillListPrefab(1, "vagina", skillList9);
					List<int> skillList10 = new List<int>(heroineHaveSexSkillVaginaList);
					SpawnActiveSkillListPrefab(1, "vagina", skillList10);
					break;
				}
				case 2:
				{
					List<int> skillList7 = new List<int>(heroineHaveSexPassiveAnalList);
					SpawnPassiveSkillListPrefab(2, "anal", skillList7);
					List<int> skillList8 = new List<int>(heroineHaveSexSkillAnalList);
					SpawnActiveSkillListPrefab(2, "anal", skillList8);
					break;
				}
				}
			}
			break;
		}
	}

	private void SpawnPassiveSkillListPrefab(int index, string categoryName, List<int> skillList)
	{
		for (int i = 0; i < skillList.Count; i++)
		{
			ParameterContainer component = PoolManager.Pools["sexTouchPool"].Spawn(sexTouchManager.heroinePassiveSkillFramePrefabGo, sexTouchManager.skillGroupPrefabParentArray[index]).GetComponent<ParameterContainer>();
			component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = "sexBodyPassive_" + categoryName + skillList[i];
			component.GetVariable<UguiImage>("frameImage").image.sprite = skillFrameSpriteArray[0];
			Sprite sprite = GameDataManager.instance.itemCategoryDataBase.bodyCategoryIconDictionary[categoryName];
			component.GetVariable<UguiImage>("iconImage").image.sprite = sprite;
		}
	}

	private void SpawnActiveSkillListPrefab(int index, string categoryName, List<int> skillList)
	{
		int i;
		for (i = 0; i < skillList.Count; i++)
		{
			ParameterContainer component = PoolManager.Pools["sexTouchPool"].Spawn(sexTouchManager.heroineActiveSkillFramePrefabGo, sexTouchManager.skillGroupPrefabParentArray[index]).GetComponent<ParameterContainer>();
			component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = "sexSkill" + skillList[i];
			component.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = "sexSkill" + skillList[i] + "_summary_short";
			component.GetVariable<UguiImage>("frameImage").image.sprite = skillFrameSpriteArray[1];
			SexSkillData sexSkillData = GameDataManager.instance.sexSkillDataBase.sexSkillDataList.Find((SexSkillData data) => data.skillID == skillList[i]);
			Sprite sprite = GameDataManager.instance.itemCategoryDataBase.skillCategoryIconDictionary[sexSkillData.skillType.ToString()];
			component.GetVariable<UguiImage>("iconImage").image.sprite = sprite;
		}
	}

	public void CloseCategorySkillView(int index)
	{
		Transform[] array = new Transform[sexTouchManager.skillGroupPrefabParentArray[index].childCount];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = sexTouchManager.skillGroupPrefabParentArray[index].GetChild(i);
		}
		Transform[] array2 = array;
		foreach (Transform instance in array2)
		{
			PoolManager.Pools["sexTouchPool"].Despawn(instance, 0f, sexTouchManager.prefabParentTransform);
		}
		sexTouchManager.skillWindowButtonImageArray[index].sprite = sexTouchManager.skillWindowButtonSpriteArray[1];
		sexTouchManager.skillWindowIsCloseArray[index] = true;
	}

	public bool GetSkillViewIsClose(int index)
	{
		return sexTouchManager.skillWindowIsCloseArray[index];
	}

	public void UnLoadHeroineSpriteData()
	{
		Resources.UnloadAsset(sexTouchHeroineSpriteData);
		Debug.Log("身体観察のスプライトデータをアンロード");
	}

	public void UnLoadHeroineVoiceData()
	{
		Resources.UnloadAsset(dynamicSoundGroup);
		Debug.Log("身体観察＆バトルの音声データをアンロード");
	}
}
