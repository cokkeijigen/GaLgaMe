using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace I2.Loc
{
	public class ResourceManager : MonoBehaviour
	{
		private static ResourceManager mInstance;

		public List<IResourceManager_Bundles> mBundleManagers = new List<IResourceManager_Bundles>();

		public UnityEngine.Object[] Assets;

		private readonly Dictionary<string, UnityEngine.Object> mResourcesCache = new Dictionary<string, UnityEngine.Object>(StringComparer.Ordinal);

		public static ResourceManager pInstance
		{
			get
			{
				bool flag = mInstance == null;
				if (mInstance == null)
				{
					mInstance = (ResourceManager)UnityEngine.Object.FindObjectOfType(typeof(ResourceManager));
				}
				if (mInstance == null)
				{
					GameObject obj = new GameObject("I2ResourceManager", typeof(ResourceManager));
					obj.hideFlags |= HideFlags.HideAndDontSave;
					mInstance = obj.GetComponent<ResourceManager>();
					SceneManager.sceneLoaded += MyOnLevelWasLoaded;
				}
				if (flag && Application.isPlaying)
				{
					UnityEngine.Object.DontDestroyOnLoad(mInstance.gameObject);
				}
				return mInstance;
			}
		}

		public static void MyOnLevelWasLoaded(Scene scene, LoadSceneMode mode)
		{
			pInstance.CleanResourceCache();
			LocalizationManager.UpdateSources();
		}

		public T GetAsset<T>(string Name) where T : UnityEngine.Object
		{
			T val = FindAsset(Name) as T;
			if (val != null)
			{
				return val;
			}
			return LoadFromResources<T>(Name);
		}

		private UnityEngine.Object FindAsset(string Name)
		{
			if (Assets != null)
			{
				int i = 0;
				for (int num = Assets.Length; i < num; i++)
				{
					if (Assets[i] != null && Assets[i].name == Name)
					{
						return Assets[i];
					}
				}
			}
			return null;
		}

		public bool HasAsset(UnityEngine.Object Obj)
		{
			if (Assets == null)
			{
				return false;
			}
			return Array.IndexOf(Assets, Obj) >= 0;
		}

		public T LoadFromResources<T>(string Path) where T : UnityEngine.Object
		{
			try
			{
				if (string.IsNullOrEmpty(Path))
				{
					return null;
				}
				if (mResourcesCache.TryGetValue(Path, out var value) && value != null)
				{
					return value as T;
				}
				T val = null;
				if (Path.EndsWith("]", StringComparison.OrdinalIgnoreCase))
				{
					int num = Path.LastIndexOf("[", StringComparison.OrdinalIgnoreCase);
					int length = Path.Length - num - 2;
					string value2 = Path.Substring(num + 1, length);
					Path = Path.Substring(0, num);
					T[] array = Resources.LoadAll<T>(Path);
					int i = 0;
					for (int num2 = array.Length; i < num2; i++)
					{
						if (array[i].name.Equals(value2))
						{
							val = array[i];
							break;
						}
					}
				}
				else
				{
					val = Resources.Load(Path, typeof(T)) as T;
				}
				if (val == null)
				{
					val = LoadFromBundle<T>(Path);
				}
				if (val != null)
				{
					mResourcesCache[Path] = val;
				}
				return val;
			}
			catch (Exception ex)
			{
				Debug.LogErrorFormat("Unable to load {0} '{1}'\nERROR: {2}", typeof(T), Path, ex.ToString());
				return null;
			}
		}

		public T LoadFromBundle<T>(string path) where T : UnityEngine.Object
		{
			int i = 0;
			for (int count = mBundleManagers.Count; i < count; i++)
			{
				if (mBundleManagers[i] != null)
				{
					T val = mBundleManagers[i].LoadFromBundle(path, typeof(T)) as T;
					if (val != null)
					{
						return val;
					}
				}
			}
			return null;
		}

		public void CleanResourceCache()
		{
			mResourcesCache.Clear();
			Resources.UnloadUnusedAssets();
			CancelInvoke();
		}
	}
}
