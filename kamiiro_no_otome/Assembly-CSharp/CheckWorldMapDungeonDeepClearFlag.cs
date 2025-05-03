using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckWorldMapDungeonDeepClearFlag : StateBehaviour
{
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
		string text = base.transform.parent.name;
		GameObject gameObject = parameterContainer.GetGameObject("flagImageGo");
		if (PlayerNonSaveDataManager.isDungeonDeepClear)
		{
			if (PlayerDataManager.currentDungeonName == text)
			{
				gameObject.SetActive(value: false);
			}
			else if (PlayerFlagDataManager.dungeonDeepClearFlagDictionary[text])
			{
				gameObject.SetActive(value: true);
			}
			else
			{
				gameObject.SetActive(value: false);
			}
		}
		else if (PlayerFlagDataManager.dungeonDeepClearFlagDictionary[text])
		{
			gameObject.SetActive(value: true);
		}
		else
		{
			gameObject.SetActive(value: false);
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
