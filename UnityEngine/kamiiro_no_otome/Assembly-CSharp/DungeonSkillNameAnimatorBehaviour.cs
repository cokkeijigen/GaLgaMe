using UnityEngine;

public class DungeonSkillNameAnimatorBehaviour : StateMachineBehaviour
{
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		GameObject.Find("Dungeon Battle Manager").GetComponent<DungeonBattleManager>().isSkillNameEffectEnd = true;
	}
}
