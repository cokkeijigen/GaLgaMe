using Arbor;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("")]
public class CheckHeroineUnFollowInDoor : StateBehaviour
{
	private float invokeTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		if (PlayerNonSaveDataManager.isHeroineUnFollowReserveAtLocalMap)
		{
			PlayerNonSaveDataManager.isUsedShop = false;
			PlayerNonSaveDataManager.heroineUnFollowBeforeStateName = "worldToInDoor";
			invokeTime = 0.4f;
			PlayerNonSaveDataManager.heroineUnFollowBlackImageAlpha = 0.7f;
			Debug.Log("ヒロイン帰宅／ID：" + PlayerDataManager.DungeonHeroineFollowNum);
			Invoke("InvokeMethod", invokeTime);
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

	private void InvokeMethod()
	{
		PlayerNonSaveDataManager.isHeroineUnFollowRightClickBlock = true;
		SceneManager.LoadSceneAsync("heroineUnFollowUI", LoadSceneMode.Additive);
	}
}
