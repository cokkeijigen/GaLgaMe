using Utage;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Utage")]
	[Tooltip("")]
	public class SetConfigTaggedVoiceVolume : FsmStateAction
	{
		[RequiredField]
		public FsmString stringTag;

		[RequiredField]
		public FsmFloat setValue;

		[RequiredField]
		[ObjectType(typeof(AdvConfig))]
		public FsmObject config;

		public override void Reset()
		{
			stringTag = null;
			setValue = 0f;
			config = null;
		}

		public override void OnEnter()
		{
			(config.Value as AdvConfig).SetTaggedMasterVolume(stringTag.Value, setValue.Value);
			Finish();
		}
	}
}
