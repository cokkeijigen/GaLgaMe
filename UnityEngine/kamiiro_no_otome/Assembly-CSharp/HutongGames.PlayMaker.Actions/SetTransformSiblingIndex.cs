using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Move the transform to a given index in the local transform list.")]
	public class SetTransformSiblingIndex : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Game Object to set its sibling index")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The index in the local parent transform list to move the transform to. Value will be clamped between 0 and the last child")]
		public FsmInt index;

		public override void Reset()
		{
			gameObject = null;
			index = null;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				ownerDefaultTarget.transform.SetSiblingIndex(Mathf.Clamp(index.Value, 0, ownerDefaultTarget.transform.parent.childCount - 1));
			}
			Finish();
		}
	}
}
