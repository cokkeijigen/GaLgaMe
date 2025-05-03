using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckMobHeroineVisible : StateBehaviour
{
	public int visibleProbability;

	private ParameterContainer parameterContainer;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		parameterContainer = GetComponentInParent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		if (!PlayerNonSaveDataManager.isInitializeMapData)
		{
			int @int = parameterContainer.GetInt("mobHeroineVisibleCheckedTimeCount");
			_ = base.transform.parent.gameObject.name;
			if (@int != PlayerDataManager.totalTimeZoneCount)
			{
				int num = Random.Range(0, 100);
				if (visibleProbability > num)
				{
					parameterContainer.SetBool("isMobHeroineVisible", value: true);
					Debug.Log(base.transform.parent.gameObject.name + "／モブ立ち絵を表示／ランダム：" + num);
					SetMobVisibleDictionary(value: true);
					SetMobCheckTimeDictionary(PlayerDataManager.totalTimeZoneCount);
				}
				else
				{
					parameterContainer.SetBool("isMobHeroineVisible", value: false);
					Debug.Log(base.transform.parent.gameObject.name + "／モブ立ち絵は不在／ランダム：" + num);
					SetMobVisibleDictionary(value: false);
					SetMobCheckTimeDictionary(PlayerDataManager.totalTimeZoneCount);
				}
				parameterContainer.SetInt("mobHeroineVisibleCheckedTimeCount", PlayerDataManager.totalTimeZoneCount);
			}
		}
		else
		{
			Debug.Log("ロード後なので、モブチェックはしない：" + base.transform.parent.gameObject.name);
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

	private void SetMobVisibleDictionary(bool value)
	{
		string key = base.transform.parent.gameObject.name;
		if (PlayerDataManager.currentAccessPointName == "Kingdom1")
		{
			if (PlayerDataManager.KingdomMobHeroineVisibleDictionary.ContainsKey(key))
			{
				PlayerDataManager.KingdomMobHeroineVisibleDictionary[key] = value;
			}
			else
			{
				PlayerDataManager.KingdomMobHeroineVisibleDictionary.Add(key, value);
			}
		}
		else if (PlayerDataManager.CityMobHeroineVisibleDictionary.ContainsKey(key))
		{
			PlayerDataManager.CityMobHeroineVisibleDictionary[key] = value;
		}
		else
		{
			PlayerDataManager.CityMobHeroineVisibleDictionary.Add(key, value);
		}
	}

	private void SetMobCheckTimeDictionary(int value)
	{
		string key = base.transform.parent.gameObject.name;
		if (PlayerDataManager.currentAccessPointName == "Kingdom1")
		{
			if (PlayerDataManager.KingdomMobCheckTimeDictionary.ContainsKey(key))
			{
				PlayerDataManager.KingdomMobCheckTimeDictionary[key] = value;
			}
			else
			{
				PlayerDataManager.KingdomMobCheckTimeDictionary.Add(key, value);
			}
		}
		else if (PlayerDataManager.CityMobCheckTimeDictionary.ContainsKey(key))
		{
			PlayerDataManager.CityMobCheckTimeDictionary[key] = value;
		}
		else
		{
			PlayerDataManager.CityMobCheckTimeDictionary.Add(key, value);
		}
	}
}
