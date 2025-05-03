using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckSaveDataExist : StateBehaviour
{
	public StateLink exist;

	public StateLink notExist;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		if (ES3.FileExists("SaveFile.es3"))
		{
			Debug.Log("セーブデータあり");
			Transition(exist);
		}
		else
		{
			Debug.Log("セーブデータなし");
			Transition(notExist);
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
}
