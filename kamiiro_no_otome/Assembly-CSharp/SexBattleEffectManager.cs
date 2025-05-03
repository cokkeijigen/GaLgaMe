using System.Collections.Generic;
using System.Linq;
using Arbor;
using I2.Loc;
using PathologicalGames;
using Sirenix.OdinInspector;
using UnityEngine;

public class SexBattleEffectManager : SerializedMonoBehaviour
{
	public SexBattleManager sexBattleManager;

	public SexBattleTurnManager sexBattleTurnManager;

	public GameObject buffInfoWindow;

	public Localize buffInfoLocText;

	private GameObject pointerEnterIconGo;

	public ParameterContainer playerParam;

	public ParameterContainer heroineParam;

	public Dictionary<string, GameObject> effectPrefabGoDictionary;

	public Transform sexBattleEffectPoolParent;

	public GameObject[] sexBattleEffectTextGoArray;

	public Transform[] sexBattleEffectSpawnPoint;

	public void SetEffectDeSpawnReserve(Transform spawnGo, bool isSkillPool, float despawnTime)
	{
		spawnGo.localScale = new Vector3(1f, 1f, 1f);
		spawnGo.localPosition = new Vector3(0f, 0f, 0f);
		if (isSkillPool)
		{
			PoolManager.Pools["sexSkillPool"].Despawn(spawnGo, despawnTime, sexBattleEffectPoolParent);
		}
		else
		{
			PoolManager.Pools["sexBattlePool"].Despawn(spawnGo, despawnTime, sexBattleEffectPoolParent);
		}
	}

	public void ResetBuffAndSubPowerIcon(bool targetIsHeroine)
	{
		if (!targetIsHeroine)
		{
			foreach (GameObject gameObject in playerParam.GetGameObjectList("buffIconGoList"))
			{
				gameObject.SetActive(value: false);
			}
			{
				foreach (GameObject gameObject2 in playerParam.GetGameObjectList("subPowerIconGoList"))
				{
					gameObject2.SetActive(value: false);
				}
				return;
			}
		}
		foreach (GameObject gameObject3 in heroineParam.GetGameObjectList("buffIconGoList"))
		{
			gameObject3.SetActive(value: false);
		}
		foreach (GameObject gameObject4 in heroineParam.GetGameObjectList("subPowerIconGoList"))
		{
			gameObject4.SetActive(value: false);
		}
	}

	public void SetBuffIcon(string buffType, bool targetIsHeroine, bool setVisible)
	{
		new List<string>();
		List<GameObject> list = new List<GameObject>();
		int num = 0;
		if (!targetIsHeroine)
		{
			num = playerParam.GetStringList("buffIconStringList").ToList().FindIndex((string data) => data == buffType);
			list = playerParam.GetGameObjectList("buffIconGoList").ToList();
		}
		else
		{
			num = heroineParam.GetStringList("buffIconStringList").ToList().FindIndex((string data) => data == buffType);
			list = heroineParam.GetGameObjectList("buffIconGoList").ToList();
		}
		list[num].SetActive(setVisible);
	}

	public void SetSubPowerIcon(string badStateType, bool targetIsHeroine, bool setVisible)
	{
		new List<string>();
		List<GameObject> list = new List<GameObject>();
		int num = 0;
		if (!targetIsHeroine)
		{
			num = playerParam.GetStringList("subPowerIconStringList").ToList().FindIndex((string data) => data == badStateType);
			list = playerParam.GetGameObjectList("subPowerIconGoList").ToList();
		}
		else
		{
			num = heroineParam.GetStringList("subPowerIconStringList").ToList().FindIndex((string data) => data == badStateType);
			list = heroineParam.GetGameObjectList("subPowerIconGoList").ToList();
		}
		list[num].SetActive(setVisible);
	}

	public void SetPointerEnterGo(GameObject gameObject)
	{
		pointerEnterIconGo = gameObject;
	}

	public void OpenBuffInfoWIndow(string type)
	{
		Vector3 position = pointerEnterIconGo.GetComponent<RectTransform>().position;
		buffInfoWindow.GetComponent<RectTransform>().position = new Vector2(position.x, position.y - 0.2f);
		buffInfoLocText.Term = "infoBuff_" + type;
		buffInfoWindow.SetActive(value: true);
	}

	public void OpenSubTypeInfoWIndow(string type)
	{
		Vector3 position = pointerEnterIconGo.GetComponent<RectTransform>().position;
		buffInfoWindow.GetComponent<RectTransform>().position = new Vector2(position.x, position.y - 0.2f);
		buffInfoLocText.Term = "infoSubType_" + type;
		buffInfoWindow.SetActive(value: true);
	}
}
