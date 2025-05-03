using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ResetLocalMapHeroinePosition : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	private HeaderStatusManager headerStatusManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
	}

	public override void OnStateBegin()
	{
		SpriteRenderer component = totalMapAccessManager.localHeroineGo.GetComponent<SpriteRenderer>();
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			component.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
			component.sprite = headerStatusManager.heroineSpriteArrayList[PlayerDataManager.DungeonHeroineFollowNum - 1][0];
		}
		else
		{
			component.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0);
		}
		Vector3 position = totalMapAccessManager.localPlayerGo.transform.position + new Vector3(0.9f, 0f, 0f);
		totalMapAccessManager.localHeroineGo.transform.position = position;
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
