using Arbor;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("")]
public class CheckHeroineUnFollow : StateBehaviour
{
	public enum CheckType
	{
		worldMap = 0,
		camp = 6,
		craft = 1,
		extension = 2,
		carriageStore = 3,
		shop = 4,
		rest = 5,
		mapRest = 7
	}

	public CheckType checkType;

	private int afterTimeNum;

	private float invokeTime;

	private int unFollowTimeZoneNum;

	public StateLink unFollowLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			switch (checkType)
			{
			case CheckType.worldMap:
				unFollowTimeZoneNum = 4;
				afterTimeNum = PlayerDataManager.currentTimeZone + PlayerNonSaveDataManager.needMoveDayCount * 4 + PlayerNonSaveDataManager.needMoveTimeCount + PlayerDataManager.dungeonEnterTimeZoneNum;
				if (PlayerDataManager.isSelectDungeon)
				{
					afterTimeNum++;
					if (!PlayerFlagDataManager.heroineAllTimeFollowFlagList[PlayerDataManager.DungeonHeroineFollowNum])
					{
						afterTimeNum += PlayerNonSaveDataManager.needMoveDayCount * 4 + PlayerNonSaveDataManager.needMoveTimeCount;
					}
				}
				PlayerNonSaveDataManager.heroineUnFollowBeforeStateName = "worldMap";
				invokeTime = 0f;
				PlayerNonSaveDataManager.heroineUnFollowBlackImageAlpha = 0.7f;
				if (PlayerDataManager.DungeonHeroineFollowNum == 2)
				{
					CheckWorldMapAftertTimeForRina();
				}
				else
				{
					CheckWorldMapAftertTime();
				}
				break;
			case CheckType.camp:
				unFollowTimeZoneNum = 4;
				afterTimeNum = PlayerDataManager.currentTimeZone + PlayerNonSaveDataManager.needCampTimeCount;
				PlayerNonSaveDataManager.heroineUnFollowBeforeStateName = "camp";
				invokeTime = 0f;
				PlayerNonSaveDataManager.heroineUnFollowBlackImageAlpha = 0.7f;
				CheckAfterTime();
				break;
			case CheckType.craft:
				unFollowTimeZoneNum = 3;
				afterTimeNum = PlayerDataManager.currentTimeZone + PlayerNonSaveDataManager.needCraftTimeCount;
				PlayerNonSaveDataManager.heroineUnFollowBeforeStateName = "craft";
				invokeTime = 0f;
				PlayerNonSaveDataManager.heroineUnFollowBlackImageAlpha = 0.5f;
				CheckAfterTime();
				break;
			case CheckType.extension:
				unFollowTimeZoneNum = 3;
				afterTimeNum = PlayerDataManager.currentTimeZone + PlayerNonSaveDataManager.needCraftTimeCount;
				PlayerNonSaveDataManager.heroineUnFollowBeforeStateName = "extension";
				invokeTime = 0.2f;
				PlayerNonSaveDataManager.heroineUnFollowBlackImageAlpha = 0.7f;
				CheckAfterTime();
				break;
			case CheckType.carriageStore:
				unFollowTimeZoneNum = 3;
				afterTimeNum = PlayerDataManager.currentTimeZone + 4;
				PlayerNonSaveDataManager.heroineUnFollowBeforeStateName = "carriageStore";
				invokeTime = 0f;
				PlayerNonSaveDataManager.heroineUnFollowBlackImageAlpha = 0.3f;
				CheckAfterTime();
				break;
			case CheckType.shop:
				unFollowTimeZoneNum = 3;
				afterTimeNum = PlayerDataManager.currentTimeZone + 1;
				PlayerNonSaveDataManager.heroineUnFollowBeforeStateName = "shop";
				invokeTime = 0f;
				PlayerNonSaveDataManager.heroineUnFollowBlackImageAlpha = 0.5f;
				CheckAfterTime();
				break;
			case CheckType.rest:
			{
				unFollowTimeZoneNum = 3;
				int restTimeZoneNum = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>().restTimeZoneNum;
				afterTimeNum = PlayerDataManager.currentTimeZone + restTimeZoneNum;
				PlayerNonSaveDataManager.heroineUnFollowBeforeStateName = "rest";
				invokeTime = 0f;
				PlayerNonSaveDataManager.heroineUnFollowBlackImageAlpha = 0.7f;
				CheckAfterTime();
				break;
			}
			case CheckType.mapRest:
				unFollowTimeZoneNum = 3;
				afterTimeNum = PlayerDataManager.currentTimeZone + 1;
				PlayerNonSaveDataManager.heroineUnFollowBeforeStateName = "mapRest";
				invokeTime = 0f;
				PlayerNonSaveDataManager.heroineUnFollowBlackImageAlpha = 0.7f;
				CheckAfterTime();
				break;
			}
			Transition(unFollowLink);
		}
		else
		{
			Transition(stateLink);
		}
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

	private void CheckAfterTime()
	{
		if (afterTimeNum >= unFollowTimeZoneNum)
		{
			if (!PlayerFlagDataManager.heroineAllTimeFollowFlagList[PlayerDataManager.DungeonHeroineFollowNum])
			{
				Debug.Log("ヒロイン帰宅／ID：" + PlayerDataManager.DungeonHeroineFollowNum);
				Invoke("InvokeMethod", invokeTime);
			}
			else
			{
				Transition(stateLink);
			}
		}
		else
		{
			Transition(stateLink);
		}
	}

	private void CheckWorldMapAftertTime()
	{
		if (!PlayerFlagDataManager.heroineAllTimeFollowFlagList[PlayerDataManager.DungeonHeroineFollowNum])
		{
			if (afterTimeNum >= unFollowTimeZoneNum)
			{
				PlayerNonSaveDataManager.isHeroineUnFollowReserveAtLocalMap = false;
				if (PlayerNonSaveDataManager.selectAccessPointName == "Kingdom1" || PlayerNonSaveDataManager.selectAccessPointName == "Tent1" || PlayerNonSaveDataManager.selectAccessPointName == "Fortress1" || PlayerNonSaveDataManager.selectAccessPointName == "City1")
				{
					if (afterTimeNum >= 3)
					{
						if (PlayerDataManager.currentAccessPointName == "Kingdom1" || PlayerDataManager.currentAccessPointName == "Tent1" || PlayerDataManager.currentAccessPointName == "Fortress1" || PlayerDataManager.currentAccessPointName == "City1")
						{
							Debug.Log("ヒロイン夕方帰宅／ID：" + PlayerDataManager.DungeonHeroineFollowNum);
							Invoke("InvokeMethod", invokeTime);
						}
						else
						{
							PlayerNonSaveDataManager.isHeroineUnFollowReserveAtLocalMap = true;
							Debug.Log("ヒロイン帰宅予約／ID：" + PlayerDataManager.DungeonHeroineFollowNum);
							Transition(stateLink);
						}
					}
					else if (PlayerNonSaveDataManager.needMoveDayCount >= 1)
					{
						Debug.Log("ヒロイン帰宅／ID：" + PlayerDataManager.DungeonHeroineFollowNum);
						Invoke("InvokeMethod", invokeTime);
					}
					else
					{
						PlayerNonSaveDataManager.isHeroineUnFollowReserveAtLocalMap = true;
						Debug.Log("ヒロイン帰宅予約／ID：" + PlayerDataManager.DungeonHeroineFollowNum);
						Transition(stateLink);
					}
				}
				else
				{
					Debug.Log("ヒロイン帰宅／ID：" + PlayerDataManager.DungeonHeroineFollowNum);
					Invoke("InvokeMethod", invokeTime);
				}
			}
			else if (afterTimeNum >= 3)
			{
				if (PlayerNonSaveDataManager.selectAccessPointName == "Kingdom1" || PlayerNonSaveDataManager.selectAccessPointName == "Tent1" || PlayerNonSaveDataManager.selectAccessPointName == "Fortress1" || PlayerNonSaveDataManager.selectAccessPointName == "City1")
				{
					if (PlayerDataManager.currentAccessPointName == "Kingdom1" || PlayerDataManager.currentAccessPointName == "Tent1" || PlayerDataManager.currentAccessPointName == "Fortress1" || PlayerDataManager.currentAccessPointName == "City1")
					{
						Debug.Log("ヒロイン夕方帰宅／ID：" + PlayerDataManager.DungeonHeroineFollowNum);
						Invoke("InvokeMethod", invokeTime);
					}
					else
					{
						Transition(stateLink);
					}
				}
				else
				{
					Transition(stateLink);
				}
			}
			else
			{
				Transition(stateLink);
			}
		}
		else
		{
			Transition(stateLink);
		}
	}

	private void CheckWorldMapAftertTimeForRina()
	{
		if (!PlayerFlagDataManager.heroineAllTimeFollowFlagList[PlayerDataManager.DungeonHeroineFollowNum])
		{
			if (afterTimeNum >= unFollowTimeZoneNum)
			{
				PlayerNonSaveDataManager.isHeroineUnFollowReserveAtLocalMap = false;
				if (PlayerFlagDataManager.scenarioFlagDictionary["MH_Rina_014"])
				{
					if (PlayerNonSaveDataManager.selectAccessPointName == "Kingdom1" || PlayerNonSaveDataManager.selectAccessPointName == "Tent1" || PlayerNonSaveDataManager.selectAccessPointName == "Fortress1" || PlayerNonSaveDataManager.selectAccessPointName == "City1")
					{
						if (afterTimeNum >= 3)
						{
							if (PlayerDataManager.currentAccessPointName == "Kingdom1" || PlayerDataManager.currentAccessPointName == "Tent1" || PlayerDataManager.currentAccessPointName == "Fortress1" || PlayerDataManager.currentAccessPointName == "City1")
							{
								Debug.Log("ヒロイン夕方帰宅／ID：" + PlayerDataManager.DungeonHeroineFollowNum);
								Invoke("InvokeMethod", invokeTime);
							}
							else
							{
								PlayerNonSaveDataManager.isHeroineUnFollowReserveAtLocalMap = true;
								Debug.Log("ヒロイン帰宅予約／ID：" + PlayerDataManager.DungeonHeroineFollowNum);
								Transition(stateLink);
							}
						}
						else if (PlayerNonSaveDataManager.needMoveDayCount >= 1)
						{
							Debug.Log("ヒロイン帰宅／ID：" + PlayerDataManager.DungeonHeroineFollowNum);
							Invoke("InvokeMethod", invokeTime);
						}
						else
						{
							PlayerNonSaveDataManager.isHeroineUnFollowReserveAtLocalMap = true;
							Debug.Log("ヒロイン帰宅予約／ID：" + PlayerDataManager.DungeonHeroineFollowNum);
							Transition(stateLink);
						}
					}
					else
					{
						Debug.Log("ヒロイン帰宅／ID：" + PlayerDataManager.DungeonHeroineFollowNum);
						Invoke("InvokeMethod", invokeTime);
					}
				}
				else if (PlayerNonSaveDataManager.selectAccessPointName == "Tent1" || PlayerNonSaveDataManager.selectAccessPointName == "Fortress1")
				{
					if (afterTimeNum >= 3)
					{
						if (PlayerDataManager.currentAccessPointName == "Kingdom1" || PlayerDataManager.currentAccessPointName == "Tent1" || PlayerDataManager.currentAccessPointName == "Fortress1" || PlayerDataManager.currentAccessPointName == "City1")
						{
							Debug.Log("ヒロイン夕方帰宅／ID：" + PlayerDataManager.DungeonHeroineFollowNum);
							Invoke("InvokeMethod", invokeTime);
						}
						else
						{
							PlayerNonSaveDataManager.isHeroineUnFollowReserveAtLocalMap = true;
							Debug.Log("ヒロイン帰宅予約／ID：" + PlayerDataManager.DungeonHeroineFollowNum);
							Transition(stateLink);
						}
					}
					else if (PlayerNonSaveDataManager.needMoveDayCount >= 1)
					{
						Debug.Log("ヒロイン帰宅／ID：" + PlayerDataManager.DungeonHeroineFollowNum);
						Invoke("InvokeMethod", invokeTime);
					}
					else
					{
						PlayerNonSaveDataManager.isHeroineUnFollowReserveAtLocalMap = true;
						Debug.Log("ヒロイン帰宅予約／ID：" + PlayerDataManager.DungeonHeroineFollowNum);
						Transition(stateLink);
					}
				}
				else
				{
					Debug.Log("ヒロイン帰宅／ID：" + PlayerDataManager.DungeonHeroineFollowNum);
					Invoke("InvokeMethod", invokeTime);
				}
			}
			else if (afterTimeNum >= 3)
			{
				if (PlayerFlagDataManager.scenarioFlagDictionary["MH_Rina_014"])
				{
					if (PlayerNonSaveDataManager.selectAccessPointName == "Kingdom1" || PlayerNonSaveDataManager.selectAccessPointName == "Tent1" || PlayerNonSaveDataManager.selectAccessPointName == "Fortress1" || PlayerNonSaveDataManager.selectAccessPointName == "City1")
					{
						if (PlayerDataManager.currentAccessPointName == "Kingdom1" || PlayerDataManager.currentAccessPointName == "Tent1" || PlayerDataManager.currentAccessPointName == "Fortress1" || PlayerDataManager.currentAccessPointName == "City1")
						{
							Debug.Log("ヒロイン夕方帰宅／ID：" + PlayerDataManager.DungeonHeroineFollowNum);
							Invoke("InvokeMethod", invokeTime);
						}
						else
						{
							Transition(stateLink);
						}
					}
					else
					{
						Transition(stateLink);
					}
				}
				else if (PlayerNonSaveDataManager.selectAccessPointName == "Tent1" || PlayerNonSaveDataManager.selectAccessPointName == "Fortress1")
				{
					if (PlayerDataManager.currentAccessPointName == "Kingdom1" || PlayerDataManager.currentAccessPointName == "Tent1" || PlayerDataManager.currentAccessPointName == "Fortress1" || PlayerDataManager.currentAccessPointName == "City1")
					{
						Debug.Log("ヒロイン夕方帰宅／ID：" + PlayerDataManager.DungeonHeroineFollowNum);
						Invoke("InvokeMethod", invokeTime);
					}
					else
					{
						Transition(stateLink);
					}
				}
				else
				{
					Transition(stateLink);
				}
			}
			else
			{
				Transition(stateLink);
			}
		}
		else
		{
			Transition(stateLink);
		}
	}

	private void InvokeMethod()
	{
		PlayerNonSaveDataManager.isHeroineUnFollowRightClickBlock = true;
		SceneManager.LoadSceneAsync("heroineUnFollowUI", LoadSceneMode.Additive);
	}
}
