using UnityEngine;

public class DontDestroyObject : MonoBehaviour
{
	private static DontDestroyObject instanse;

	private void Awake()
	{
		if (instanse == null)
		{
			instanse = this;
			Object.DontDestroyOnLoad(this);
		}
		else
		{
			Object.Destroy(base.gameObject);
		}
	}
}
