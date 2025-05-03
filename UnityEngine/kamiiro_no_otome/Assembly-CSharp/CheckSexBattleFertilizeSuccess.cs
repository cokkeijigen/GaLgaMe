using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class CheckSexBattleFertilizeSuccess : StateBehaviour
{
	private SexBattleFertilizationManager sexBattleFertilizationManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleFertilizationManager = GameObject.Find("SexBattle Fertilization Manager").GetComponent<SexBattleFertilizationManager>();
	}

	public override void OnStateBegin()
	{
		if (sexBattleFertilizationManager.isFertilizationSuccess)
		{
			sexBattleFertilizationManager.fertilizationAnimationGroup.SetActive(value: true);
			sexBattleFertilizationManager.fertilizationAnimator.SetTrigger("Fertilization");
			MasterAudio.PlaySound("SeKodou", 1f, null, 0f, null, null);
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
}
