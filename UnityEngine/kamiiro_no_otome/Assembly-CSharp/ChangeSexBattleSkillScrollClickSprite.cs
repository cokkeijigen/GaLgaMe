using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class ChangeSexBattleSkillScrollClickSprite : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
	}

	public override void OnStateBegin()
	{
		Transform[] array = new Transform[sexBattleManager.skillWindowContentGo.transform.childCount];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = sexBattleManager.skillWindowContentGo.transform.GetChild(i);
		}
		for (int j = 0; j < array.Length; j++)
		{
			array[j].GetComponent<Image>().sprite = sexBattleManager.skillScrollSpriteArray[1];
		}
		array[sexBattleManager.selectSkillScrollIndex].GetComponent<Image>().sprite = sexBattleManager.skillScrollSpriteArray[0];
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
