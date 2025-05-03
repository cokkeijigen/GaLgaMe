using Arbor;
using TMPro;
using UnityEngine;

public class CountDownText : StateMachineBehaviour
{
	public ArborFSM arborFSM;

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		int integer = animator.GetInteger("num");
		if (integer > 1)
		{
			animator.GetComponent<TextMeshProUGUI>().text = (integer - 1).ToString();
			animator.SetTrigger("countDown");
		}
		else
		{
			arborFSM.SendTrigger("StartCraftCheck");
		}
	}
}
