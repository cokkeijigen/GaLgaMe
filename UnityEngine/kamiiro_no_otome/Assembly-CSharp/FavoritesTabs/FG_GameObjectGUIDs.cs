using System.Collections.Generic;
using UnityEngine;

namespace FavoritesTabs
{
	[ExecuteAlways]
	public class FG_GameObjectGUIDs : MonoBehaviour
	{
		public static bool _dirty = true;

		public static HashSet<FG_GameObjectGUIDs> allInstances = new HashSet<FG_GameObjectGUIDs>();

		[SerializeField]
		[HideInInspector]
		public List<string> guids = new List<string>();

		[SerializeField]
		[HideInInspector]
		public List<Object> objects = new List<Object>();

		public static void Test()
		{
		}

		protected FG_GameObjectGUIDs()
		{
			_dirty = true;
		}

		protected void Awake()
		{
			_dirty = allInstances.Add(this) || _dirty;
		}

		protected void OnEnable()
		{
			_dirty = allInstances.Add(this) || _dirty;
		}

		protected void OnDisable()
		{
			_dirty = true;
		}

		protected void OnDestroy()
		{
			_dirty = allInstances.Remove(this) || _dirty;
		}
	}
}
