using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Sets the Parent of a Game Object.")]
	public class SetParent : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Game Object to parent.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The new parent for the Game Object. Leave empty or None to un-parent the Game Object.")]
		public FsmGameObject parent;

		[Tooltip("If true, the parent-relative position, scale and rotation are modified such that the object keeps the same world space position, rotation and scale as before. Hint: Setting to False can fix common UI scaling issues.")]
		public FsmBool worldPositionStays;

		[Tooltip("Set the local position to 0,0,0 after parenting.")]
		public FsmBool resetLocalPosition;

		[Tooltip("Set the local rotation to 0,0,0 after parenting.")]
		public FsmBool resetLocalRotation;

		public override void Reset()
		{
			gameObject = null;
			parent = null;
			worldPositionStays = true;
			resetLocalPosition = null;
			resetLocalRotation = null;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				ownerDefaultTarget.transform.SetParent((parent.Value == null) ? null : parent.Value.transform, worldPositionStays.Value);
				if (resetLocalPosition.Value)
				{
					ownerDefaultTarget.transform.localPosition = Vector3.zero;
				}
				if (resetLocalRotation.Value)
				{
					ownerDefaultTarget.transform.localRotation = Quaternion.identity;
				}
			}
			Finish();
		}
	}
}
