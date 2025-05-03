using DG.Tweening;
using Doozy.PlayMaker.Actions;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("DOTween")]
	[Tooltip("Initializes DOTween. Call it without any parameter to use the preferences you set in DOTween's Utility Panel (otherwise they will be overrided by any eventual parameter passed)")]
	[HelpUrl("http://dotween.demigiant.com/documentation.php")]
	public class DOTweenInit : FsmStateAction
	{
		[UIHint(UIHint.FsmBool)]
		[Tooltip("If TRUE all new tweens will be set for recycling, meaning that when killed they won't be destroyed but instead will be put in a pool and reused rather than creating new tweens. This option allows you to avoid GC allocations by reusing tweens, but you will have to take care of tween references, since they might result active even if they were killed (since they might have been respawned and might now be in use as other completely different tweens)")]
		public FsmBool recycleAllByDefault;

		[UIHint(UIHint.FsmBool)]
		[Tooltip("If set to TRUE tweens will be slightly slower but safer, allowing DOTween to automatically take care of things like targets being destroyed while a tween is running. WARNING: on iOS safeMode works only if stripping level is set to 'Strip Assemblies' or Script Call Optimization is set to 'Slow and Safe'.")]
		public FsmBool useSafeMode;

		[Tooltip("Depending on the chosen mode DOTween will log only errors, errors and warnings, or everything plus additional informations.")]
		public LogBehaviour logBehaviour = LogBehaviour.ErrorsOnly;

		[ActionSection("Set Capacity")]
		[UIHint(UIHint.FsmInt)]
		[Tooltip("Directly sets the current max capacity of Tweeners (meaning how many Tweeners can be running at the same time) so that DOTween doesn't need to automatically increase them in case the max is reached (which might lead to hiccups when that happens).")]
		public FsmInt tweenersCapacity = 200;

		[UIHint(UIHint.FsmInt)]
		[Tooltip("Directly sets the current max capacity of Sequences (meaning how many Sequences can be running at the same time) so that DOTween doesn't need to automatically increase them in case the max is reached (which might lead to hiccups when that happens). Sequences capacity must be less or equal to Tweeners capacity (if you pass a low Tweener capacity it will be automatically increased to match the Sequence's). Beware: use this method only when there are no tweens running.")]
		public FsmInt sequencesCapacity = 10;

		[ActionSection("Debug Options")]
		[UIHint(UIHint.FsmBool)]
		public FsmBool debugThis;

		public override void Reset()
		{
			base.Reset();
			recycleAllByDefault = new FsmBool
			{
				Value = false
			};
			useSafeMode = new FsmBool
			{
				Value = true
			};
			logBehaviour = LogBehaviour.ErrorsOnly;
			tweenersCapacity = new FsmInt
			{
				UseVariable = false,
				Value = 200
			};
			sequencesCapacity = new FsmInt
			{
				UseVariable = false,
				Value = 10
			};
			debugThis = new FsmBool
			{
				Value = false
			};
		}

		public override void OnEnter()
		{
			DOTween.Init(recycleAllByDefault.Value, useSafeMode.Value, logBehaviour).SetCapacity(tweenersCapacity.Value, sequencesCapacity.Value);
			if (debugThis.Value)
			{
				base.State.Debug("DOTween Init");
			}
			Finish();
		}
	}
}
