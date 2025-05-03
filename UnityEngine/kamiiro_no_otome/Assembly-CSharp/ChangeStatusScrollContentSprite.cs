using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ChangeStatusScrollContentSprite : StateBehaviour
{
	private StatusManager statusManager;

	private SexStatusManager sexStatusManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
		sexStatusManager = GameObject.Find("Sex Status Manager").GetComponent<SexStatusManager>();
	}

	public override void OnStateBegin()
	{
		GameObject gameObject = null;
		int index = 0;
		switch (PlayerNonSaveDataManager.selectStatusCanvasName)
		{
		case "equipItem":
		case "item":
			gameObject = statusManager.itemContentGO;
			index = statusManager.selectItemScrollContentIndex;
			break;
		case "skill":
			gameObject = statusManager.skillContentGO;
			index = statusManager.selectSkillScrollContentIndex;
			break;
		case "sexStatus":
			gameObject = sexStatusManager.sexStatusContentGO;
			index = sexStatusManager.selectSexSkillScrollContentIndex;
			break;
		}
		foreach (Transform item in gameObject.transform)
		{
			item.GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage").image.sprite = statusManager.selectScrollContentSpriteArray[0];
		}
		gameObject.transform.GetChild(index).GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage")
			.image.sprite = statusManager.selectScrollContentSpriteArray[1];
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
