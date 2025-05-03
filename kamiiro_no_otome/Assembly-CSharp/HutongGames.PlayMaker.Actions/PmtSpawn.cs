using PathologicalGames;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Path-o-logical/Pool Manager 2")]
	[Tooltip("Use Spawn() instead of Unity's Instantiate(). The function signature is the same but the return type is different and Spawn() will use an instance from the pool if one is available.")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W849")]
	public class PmtSpawn : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.FsmString)]
		[Tooltip("Pool name")]
		public FsmString poolName;

		[RequiredField]
		[UIHint(UIHint.FsmGameObject)]
		[Tooltip("GameObject or prefab to spawn")]
		public FsmGameObject gameObject;

		[UIHint(UIHint.FsmGameObject)]
		[Tooltip("Use this to define the transform of the spawned GameObject when created")]
		public FsmGameObject spawnTransform;

		[Tooltip("OR a world position, If spawnTransform is declared, OrSpawnPosition will be added to the transform position for offset")]
		public FsmVector3 OrSpawnPosition;

		[UIHint(UIHint.FsmGameObject)]
		[Tooltip("Store the returned the spawned GameObject.")]
		public FsmGameObject spawnedGameObject;

		[Tooltip("Event triggered if spawn was successful")]
		public FsmEvent successEvent;

		[Tooltip("Event triggered if spawn was not successful. It fails if the 'limit' option was used and the limit was reached.")]
		public FsmEvent failEvent;

		public override void Reset()
		{
			poolName = null;
			gameObject = null;
			spawnTransform = null;
			OrSpawnPosition = null;
			spawnedGameObject = null;
			successEvent = null;
			failEvent = null;
		}

		public override void OnEnter()
		{
			DoSpawn();
			Finish();
		}

		private void DoSpawn()
		{
			if (!(poolName.Value == "") && !(gameObject.Value == null))
			{
				Vector3 pos = Vector3.zero;
				Quaternion rot = Quaternion.identity;
				GameObject value = spawnTransform.Value;
				if (value != null)
				{
					pos = value.transform.position;
					rot = value.transform.rotation;
				}
				if (OrSpawnPosition != null)
				{
					pos += OrSpawnPosition.Value;
				}
				Transform transform = PoolManager.Pools[poolName.Value].Spawn(gameObject.Value.transform, pos, rot);
				if (transform != null)
				{
					spawnedGameObject.Value = transform.gameObject;
					base.Fsm.Event(successEvent);
				}
				else
				{
					base.Fsm.Event(failEvent);
				}
			}
		}
	}
}
