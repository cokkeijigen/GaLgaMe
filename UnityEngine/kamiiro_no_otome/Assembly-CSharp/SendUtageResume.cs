using Arbor;
using UnityEngine;
using Utage;

[AddComponentMenu("")]
public class SendUtageResume : StateBehaviour
{
	public AdvEngine advEngine;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		if (advEngine == null)
		{
			advEngine = GameObject.Find("AdvEngine").GetComponent<AdvEngine>();
		}
		advEngine.ResumeScenario();
		Debug.Log("宴リジューム");
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
