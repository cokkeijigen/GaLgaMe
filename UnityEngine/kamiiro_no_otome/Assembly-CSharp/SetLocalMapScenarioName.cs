using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetLocalMapScenarioName : StateBehaviour
{
	private ParameterContainer parameterContainer;

	public string scenarioName;

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
		parameterContainer.SetString("scenarioName", scenarioName);
		if (!string.IsNullOrEmpty(scenarioName))
		{
			parameterContainer.GetGameObject("alertBalloon").SetActive(value: true);
			if (parameterContainer.GetBool("isHeroinePlaceCheck"))
			{
				SetHeroineAbsence();
			}
			Debug.Log("シナリオがある／" + base.transform.parent.gameObject.name);
		}
		else
		{
			parameterContainer.GetGameObject("alertBalloon").SetActive(value: false);
			if (parameterContainer.GetBool("isHeroinePlaceCheck"))
			{
				PlayMakerFSM[] components = GetComponents<PlayMakerFSM>();
				foreach (PlayMakerFSM playMakerFSM in components)
				{
					if (playMakerFSM.FsmName == "ヒロイン居場所確認")
					{
						playMakerFSM.SendEvent("CheckInDoorHeroine");
					}
				}
			}
			Debug.Log("シナリオはない／" + base.transform.parent.gameObject.name);
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

	private void SetHeroineAbsence()
	{
		parameterContainer.SetInt("heroineNum", 0);
		parameterContainer.GetGameObject("heroineBalloon").SetActive(value: false);
	}
}
