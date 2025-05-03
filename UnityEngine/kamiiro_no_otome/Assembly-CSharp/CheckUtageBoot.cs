using Arbor;
using UnityEngine;
using Utage;

[AddComponentMenu("")]
public class CheckUtageBoot : StateBehaviour
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
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		AssetFileManager.InitLoadTypeSetting(AssetFileManagerSettings.LoadType.Local);
		if (!advEngine.IsWaitBootLoading)
		{
			Debug.Log("宴起動");
			Transition(stateLink);
		}
		else
		{
			Debug.Log("宴起動待ち");
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
