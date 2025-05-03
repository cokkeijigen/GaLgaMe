using HutongGames.PlayMaker;
using UnityEngine;

namespace Doozy.PlayMaker.Actions
{
	public static class DOTweenActionsUtils
	{
		public static void Debug(this FsmState state, string message)
		{
			UnityEngine.Debug.Log("GameObject [" + state.Fsm.GameObjectName + "] -> FSM [" + state.Fsm.Name + "] -> State [" + state.Name + "]: " + message);
		}
	}
}
