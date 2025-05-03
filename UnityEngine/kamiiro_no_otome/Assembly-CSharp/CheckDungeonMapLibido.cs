using System.Collections.Generic;
using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class CheckDungeonMapLibido : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private DungeonMapEffectManager dungeonMapEffectManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		dungeonMapEffectManager = GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapEffectManager>();
	}

	public override void OnStateBegin()
	{
		string text = "";
		bool flag = false;
		bool flag2 = false;
		text = ((PlayerDataManager.playerLibido >= 70) ? "Max" : ((PlayerDataManager.playerLibido < 40) ? "Normal" : "High"));
		List<DungeonLibidoData> dungeonLibidoDataList = dungeonMapManager.dungeonLibidoDataBase.dungeonLibidoDataList;
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			CharacterStatusData characterStatusData = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[PlayerDataManager.DungeonHeroineFollowNum];
			if (PlayerFlagDataManager.scenarioFlagDictionary[characterStatusData.characterDungeonSexUnLockFlag])
			{
				flag = true;
				switch (text)
				{
				case "Normal":
				{
					dungeonMapManager.chracterImageGoArray[0].GetComponent<Image>().sprite = dungeonLibidoDataList.Find((DungeonLibidoData data) => data.characterID == 0).normalSprite;
					dungeonMapManager.chracterImageGoArray[1].GetComponent<Image>().sprite = dungeonLibidoDataList.Find((DungeonLibidoData data) => data.characterID == PlayerDataManager.DungeonHeroineFollowNum).normalSprite;
					float y = dungeonMapManager.dungeonLibidoDataBase.dungeonLibidoDataList[0].dungeonMoveV2.y;
					dungeonMapManager.chracterImageGoArray[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(80f, y);
					dungeonMapManager.chracterImageGoArray[1].SetActive(value: true);
					if (dungeonLibidoDataList.Find((DungeonLibidoData data) => data.characterID == PlayerDataManager.DungeonHeroineFollowNum).dungeonMoveIsLeadPosition)
					{
						Debug.Log("前に出る");
						dungeonMapManager.chracterImageGoArray[1].transform.SetSiblingIndex(2);
					}
					else
					{
						Debug.Log("前に出ない");
						dungeonMapManager.chracterImageGoArray[1].transform.SetSiblingIndex(1);
					}
					Vector2 dungeonMoveV = dungeonLibidoDataList.Find((DungeonLibidoData data) => data.characterID == PlayerDataManager.DungeonHeroineFollowNum).dungeonMoveV2;
					dungeonMapManager.chracterImageGoArray[1].GetComponent<RectTransform>().anchoredPosition = dungeonMoveV;
					dungeonMapEffectManager.DespawnDungeonLibidoEffect();
					dungeonMapManager.ChangeHeroineCardProbability(isHeroineLibidoHigher: false);
					Debug.Log("ダンジョンイチャイチャ：Normal");
					break;
				}
				case "High":
				{
					dungeonMapManager.chracterImageGoArray[0].GetComponent<Image>().sprite = dungeonLibidoDataList.Find((DungeonLibidoData data) => data.characterID == PlayerDataManager.DungeonHeroineFollowNum).libidoHighSprite;
					float dungeonLibidoY2 = dungeonLibidoDataList.Find((DungeonLibidoData data) => data.characterID == PlayerDataManager.DungeonHeroineFollowNum).dungeonLibidoY;
					dungeonMapManager.chracterImageGoArray[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(370f, dungeonLibidoY2);
					dungeonMapManager.chracterImageGoArray[1].SetActive(value: false);
					Vector2 libidoMaxEffectV2 = dungeonLibidoDataList.Find((DungeonLibidoData data) => data.characterID == PlayerDataManager.DungeonHeroineFollowNum).libidoMaxEffectV2;
					dungeonMapEffectManager.SpawnDungeonLibidoEffect(libidoMaxEffectV2);
					dungeonMapManager.ChangeHeroineCardProbability(isHeroineLibidoHigher: true);
					Debug.Log("ダンジョンイチャイチャ：High");
					break;
				}
				case "Max":
				{
					dungeonMapManager.chracterImageGoArray[0].GetComponent<Image>().sprite = dungeonLibidoDataList.Find((DungeonLibidoData data) => data.characterID == PlayerDataManager.DungeonHeroineFollowNum).libidoMaxSprite;
					float dungeonLibidoY = dungeonLibidoDataList.Find((DungeonLibidoData data) => data.characterID == PlayerDataManager.DungeonHeroineFollowNum).dungeonLibidoY;
					dungeonMapManager.chracterImageGoArray[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(370f, dungeonLibidoY);
					dungeonMapManager.chracterImageGoArray[1].SetActive(value: false);
					Vector2 libidoMaxEffectV = dungeonLibidoDataList.Find((DungeonLibidoData data) => data.characterID == PlayerDataManager.DungeonHeroineFollowNum).libidoMaxEffectV2;
					dungeonMapEffectManager.SpawnDungeonLibidoEffect(libidoMaxEffectV);
					dungeonMapManager.ChangeHeroineCardProbability(isHeroineLibidoHigher: true);
					Debug.Log("ダンジョンイチャイチャ：Max");
					break;
				}
				}
			}
			else
			{
				dungeonMapManager.chracterImageGoArray[0].GetComponent<Image>().sprite = dungeonLibidoDataList.Find((DungeonLibidoData data) => data.characterID == 0).normalSprite;
				dungeonMapManager.chracterImageGoArray[1].GetComponent<Image>().sprite = dungeonLibidoDataList.Find((DungeonLibidoData data) => data.characterID == PlayerDataManager.DungeonHeroineFollowNum).normalSprite;
				float y2 = dungeonMapManager.dungeonLibidoDataBase.dungeonLibidoDataList[0].dungeonMoveV2.y;
				dungeonMapManager.chracterImageGoArray[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(80f, y2);
				dungeonMapManager.chracterImageGoArray[1].SetActive(value: true);
				Vector2 dungeonMoveV2 = dungeonLibidoDataList.Find((DungeonLibidoData data) => data.characterID == PlayerDataManager.DungeonHeroineFollowNum).dungeonMoveV2;
				dungeonMapManager.chracterImageGoArray[1].GetComponent<RectTransform>().anchoredPosition = dungeonMoveV2;
				Debug.Log("ダンジョンイチャイチャ：フラグ未達成");
			}
		}
		else
		{
			dungeonMapManager.chracterImageGoArray[0].GetComponent<Image>().sprite = dungeonLibidoDataList.Find((DungeonLibidoData data) => data.characterID == 0).normalSprite;
			float y3 = dungeonMapManager.dungeonLibidoDataBase.dungeonLibidoDataList[0].dungeonMoveV2.y;
			dungeonMapManager.chracterImageGoArray[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(240f, y3);
			dungeonMapManager.chracterImageGoArray[1].SetActive(value: false);
			Debug.Log("ダンジョンイチャイチャ：エデンのみ");
		}
		SexTouchData sexTouchData = GameDataManager.instance.sexTouchDataBase.sexTouchDataList.Find((SexTouchData data) => data.characterID == PlayerDataManager.DungeonHeroineFollowNum);
		if (PlayerFlagDataManager.scenarioFlagDictionary[sexTouchData.enableSexFlag])
		{
			flag2 = true;
		}
		if (PlayerDataManager.playerLibido >= 100)
		{
			if (!string.IsNullOrEmpty(PlayerDungeonScenarioDataManager.CheckDungeonSexEvent(base.name, dungeonMapManager.dungeonCurrentFloorNum)))
			{
				dungeonMapManager.isSexLibidoEventEnable = true;
			}
			else
			{
				dungeonMapManager.isSexLibidoEventEnable = false;
			}
		}
		else
		{
			dungeonMapManager.isSexLibidoEventEnable = false;
		}
		if (dungeonMapManager.isSexLibidoEventEnable || dungeonMapManager.isSexFloorEventEnable)
		{
			dungeonMapManager.sexButtonLoc.Term = "buttonSexEvent";
			dungeonMapManager.sexButton.SetActive(value: true);
		}
		else if (flag && flag2 && PlayerDataManager.playerLibido >= 100)
		{
			dungeonMapManager.sexButtonLoc.Term = "buttonSexBattle";
			dungeonMapManager.sexButton.SetActive(value: true);
		}
		else
		{
			dungeonMapManager.sexButton.SetActive(value: false);
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
