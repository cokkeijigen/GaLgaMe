using System;
using UnityEngine;

namespace I2.Loc
{
	public class RegisterBundlesManager : MonoBehaviour, IResourceManager_Bundles
	{
		public void OnEnable()
		{
			if (!ResourceManager.pInstance.mBundleManagers.Contains(this))
			{
				ResourceManager.pInstance.mBundleManagers.Add(this);
			}
		}

		public void OnDisable()
		{
			ResourceManager.pInstance.mBundleManagers.Remove(this);
		}

		public virtual UnityEngine.Object LoadFromBundle(string path, Type assetType)
		{
			return null;
		}
	}
}
