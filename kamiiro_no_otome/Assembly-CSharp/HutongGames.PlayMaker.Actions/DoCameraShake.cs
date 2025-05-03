using DG.Tweening;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Tween")]
	[Tooltip("")]
	public class DoCameraShake : FsmStateAction
	{
		[RequiredField]
		public GameObject gameobject;

		[RequiredField]
		public FsmFloat duration;

		[RequiredField]
		public FsmFloat strength;

		[RequiredField]
		public FsmInt vibrato;

		public override void Reset()
		{
			gameobject = null;
			duration = 0f;
			strength = 1f;
			vibrato = 10;
		}

		public override void OnEnter()
		{
			gameobject.transform.DOShakePosition(duration.Value, strength.Value, vibrato.Value);
			Finish();
		}
	}
}
