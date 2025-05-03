using Arbor;
using I2.Loc;
using UnityEngine;

[AddComponentMenu("")]
public class OpenBattleFailedDialog : StateBehaviour
{
	public GameObject battleFailedCanvas;

	public GameObject backEventButton;

	public GameObject quitButton;

	public Localize backEventLoc;

	public StateLink stateLink;

	private void Start()
	{
	}

	private void OnEnable()
	{
		if (string.IsNullOrEmpty(PlayerDataManager.currentAccessPointName))
		{
			backEventButton.SetActive(value: false);
			quitButton.SetActive(value: true);
		}
		else if (PlayerDataManager.isSelectDungeon)
		{
			backEventButton.SetActive(value: true);
			quitButton.SetActive(value: false);
			backEventLoc.Term = "buttonGoOutFromDungeon";
		}
		else
		{
			backEventButton.SetActive(value: true);
			quitButton.SetActive(value: false);
			backEventLoc.Term = "buttonBackBeforeEvent";
		}
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		battleFailedCanvas.SetActive(value: true);
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
