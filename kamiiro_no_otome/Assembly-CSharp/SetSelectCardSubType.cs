using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetSelectCardSubType : StateBehaviour
{
	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		string @string = GetComponent<ParameterContainer>().GetString("localizeTerm");
		GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>().selectCardTerm = @string;
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
