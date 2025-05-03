using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ResetScenarioBattleText : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	public float textResetTime;

	public float waitTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
	}

	public override void OnStateBegin()
	{
		float time = 0f;
		if (textResetTime > 0f)
		{
			time = textResetTime / (float)utageBattleSceneManager.battleSpeed;
		}
		Invoke("TextReset", time);
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

	private void TextReset()
	{
		utageBattleSceneManager.battleTopText.SetActive(value: false);
		GameObject[] battleTextArray = utageBattleSceneManager.battleTextArray2;
		for (int i = 0; i < battleTextArray.Length; i++)
		{
			battleTextArray[i].SetActive(value: false);
		}
		battleTextArray = utageBattleSceneManager.battleTextArray3;
		for (int i = 0; i < battleTextArray.Length; i++)
		{
			battleTextArray[i].SetActive(value: false);
		}
		battleTextArray = utageBattleSceneManager.battleTextArray4;
		for (int i = 0; i < battleTextArray.Length; i++)
		{
			battleTextArray[i].SetActive(value: false);
		}
		float time = waitTime / (float)utageBattleSceneManager.battleSpeed;
		Invoke("InvokeMethod", time);
	}

	private void InvokeMethod()
	{
		Transition(stateLink);
	}
}
