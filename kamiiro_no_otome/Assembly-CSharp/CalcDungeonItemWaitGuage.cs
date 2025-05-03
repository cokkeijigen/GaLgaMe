using System.Collections;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcDungeonItemWaitGuage : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	private DungeonCharacterAgility dungeonCharacterAgility;

	private ParameterContainer parameterContainer;

	public float time;

	public int addValue;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = GameObject.Find("Dungeon Battle Manager").GetComponent<DungeonBattleManager>();
		dungeonCharacterAgility = GetComponent<DungeonCharacterAgility>();
		parameterContainer = GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		dungeonCharacterAgility.isCoroutineStop = false;
		if (dungeonBattleManager.itemWaitSlider.value < dungeonBattleManager.itemWaitSlider.maxValue)
		{
			StartCoroutine("AddItemWaitSlider");
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

	private IEnumerator AddItemWaitSlider()
	{
		WaitForSeconds cachedWait = new WaitForSeconds(time / (float)PlayerDataManager.dungeonBattleSpeed);
		while (dungeonBattleManager.itemWaitSlider.value < dungeonBattleManager.itemWaitSlider.maxValue)
		{
			if (dungeonCharacterAgility.isCoroutineStop)
			{
				yield return null;
				continue;
			}
			dungeonBattleManager.itemWaitSlider.value += addValue;
			yield return cachedWait;
		}
		Transition(stateLink);
	}

	public void StopCurrentCoroutine()
	{
		StopCoroutine("AddItemWaitSlider");
		Debug.Log("アイテムゲージコルーチン：停止");
	}
}
