using Arbor;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Arbor")]
	public class SendArborTrigger : FsmStateAction
	{
		[ObjectType(typeof(ArborFSM))]
		public FsmObject arborFSM;

		public FsmString findGoName;

		[RequiredField]
		public FsmString setString;

		public override void Reset()
		{
			arborFSM = null;
			setString = "";
		}

		public override void OnEnter()
		{
			ArborFSM arborFSM = null;
			if (this.arborFSM.Value != null)
			{
				arborFSM = this.arborFSM.Value as ArborFSM;
				Debug.Log("トリガー送信先：" + this.arborFSM.Value.name);
			}
			else
			{
				arborFSM = GameObject.Find(findGoName.Value).GetComponent<ArborFSM>();
				Debug.Log("トリガー送信先Find：" + findGoName.Value);
			}
			arborFSM.SendTrigger(setString.Value);
			Debug.Log("Arborにトリガーを送信：" + setString.Value);
			Finish();
		}
	}
}
