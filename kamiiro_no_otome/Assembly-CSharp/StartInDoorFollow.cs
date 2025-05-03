using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class StartInDoorFollow : StateBehaviour
{
	private InDoorTalkManager inDoorTalkManager;

	private InDoorClickManager inDoorClickManager;

	public StateLink requestLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
		inDoorClickManager = GameObject.Find("InDoor Click Manager").GetComponent<InDoorClickManager>();
	}

	public override void OnStateBegin()
	{
		int @int = inDoorTalkManager.commandTalkOriginGo.GetComponent<ParameterContainer>().GetInt("sortID");
		PlayerNonSaveDataManager.isHeroineUnFollowRightClickBlock = true;
		if (inDoorTalkManager.isFollowRequest)
		{
			PlayerDataManager.isDungeonHeroineFollow = true;
			PlayerDataManager.DungeonHeroineFollowNum = @int;
		}
		else
		{
			PlayerDataManager.isDungeonHeroineFollow = false;
		}
		if (inDoorTalkManager.isFollowRequest)
		{
			MasterAudio.PlaySound("SeApplyButton", 1f, null, 0f, null, null);
			Transition(requestLink);
		}
		else
		{
			MasterAudio.PlaySound("SeApplyButton", 1f, null, 0f, null, null);
			Transition(requestLink);
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
