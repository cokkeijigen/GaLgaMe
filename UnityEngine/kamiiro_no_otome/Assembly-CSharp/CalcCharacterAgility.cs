using System.Collections;
using System.Collections.Generic;
using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class CalcCharacterAgility : StateBehaviour
{
	private DungeonMapStatusManager dungeonMapStatusManager;

	private DungeonCharacterAgility dungeonCharacterAgility;

	private ParameterContainer agilityContainer;

	private ParameterContainer parameterContainer;

	public Slider agilitySlider;

	public float time;

	public bool sendQueue;

	private int partyNum;

	private int agility;

	public StateLink stateLink;

	private void OnEnable()
	{
		sendQueue = false;
	}

	public override void OnStateAwake()
	{
		partyNum = GetComponent<ParameterContainer>().GetInt("partyMemberNum");
		agility = GetComponent<ParameterContainer>().GetInt("characterAgility");
		dungeonMapStatusManager = GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapStatusManager>();
		dungeonCharacterAgility = GetComponent<DungeonCharacterAgility>();
		parameterContainer = GetComponent<ParameterContainer>();
		agilityContainer = GameObject.Find("Dungeon Agility Manager").GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		if (!sendQueue)
		{
			StartCoroutine("SetAgilitySlider");
		}
	}

	public override void OnStateEnd()
	{
		sendQueue = false;
	}

	public override void OnStateUpdate()
	{
	}

	public override void OnStateLateUpdate()
	{
	}

	private IEnumerator SetAgilitySlider()
	{
		Debug.Log("キャラAgilityコルーチン：開始／素早さデバフ：" + dungeonMapStatusManager.dungeonDeBuffAgility);
		float totalAgility = (agility + dungeonMapStatusManager.dungeonDeBuffAgility) / 2;
		totalAgility = Mathf.Clamp(totalAgility, 1f, 100f);
		int cachedSpeed = PlayerDataManager.dungeonBattleSpeed;
		WaitForSeconds cachedWait = new WaitForSeconds(time / (float)PlayerDataManager.dungeonBattleSpeed);
		while (agilitySlider.value < agilitySlider.maxValue)
		{
			if (dungeonCharacterAgility.isCoroutineStop)
			{
				yield return null;
				continue;
			}
			agilitySlider.value += totalAgility;
			if (PlayerDataManager.dungeonBattleSpeed != cachedSpeed)
			{
				cachedSpeed = PlayerDataManager.dungeonBattleSpeed;
				cachedWait = new WaitForSeconds(time / (float)PlayerDataManager.dungeonBattleSpeed);
			}
			yield return cachedWait;
		}
		IList<string> stringList = agilityContainer.GetStringList("AgilityQueueList");
		stringList.Add("p" + partyNum);
		agilityContainer.SetStringList("AgilityQueueList", stringList);
		sendQueue = true;
		Debug.Log("キャラAgilityコルーチン：終了");
	}
}
