using System;
using UnityEngine;

namespace I2.Loc
{
	public interface IResourceManager_Bundles
	{
		UnityEngine.Object LoadFromBundle(string path, Type assetType);
	}
}
