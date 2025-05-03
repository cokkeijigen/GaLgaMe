using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class CheckNeedEffectNewWorldMapPoint : StateBehaviour
{
	private WorldMapAccessManager worldMapAccessManager;

	private Transform spawnGo;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		worldMapAccessManager = GameObject.Find("WorldMap Access Manager").GetComponent<WorldMapAccessManager>();
	}

	public override void OnStateBegin()
	{
		if (PlayerDataManager.isNeedEffectNewWorldMapPoint)
		{
			Vector3 position = GameObject.Find(PlayerDataManager.needEffectNewWorldMapPointName).transform.position;
			float x = Mathf.Clamp(position.x, -8f, 8f);
			float y = Mathf.Clamp(position.y, -4f, 4f);
			ShortcutExtensions.DOMove(endValue: new Vector3(x, y, -100f), target: worldMapAccessManager.worldMapCameraGo.transform, duration: 0.5f).OnComplete(ProcessingAfterWorldMapCameraMove);
		}
		else
		{
			Transition(stateLink);
		}
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
	}

	public override void OnStateLateUpdate()
	{
	}

	private void ProcessingAfterWorldMapCameraMove()
	{
		CanvasGroup component = GameObject.Find(PlayerDataManager.needEffectNewWorldMapPointName).GetComponent<CanvasGroup>();
		component.interactable = true;
		component.blocksRaycasts = true;
		component.DOFade(1f, 0.5f);
		spawnGo = PoolManager.Pools["totalMapPool"].Spawn(worldMapAccessManager.newMapPointEffectPrefabGo, component.transform);
		spawnGo.localPosition = new Vector3(0f, 0f, 0f);
		spawnGo.localScale = new Vector3(1f, 1f, 1f);
		MasterAudio.PlaySound("SeEffectNewWorldPoint", 1f, null, 0f, null, null);
		Invoke("InvokeMethod", 1f);
	}

	private void InvokeMethod()
	{
		PoolManager.Pools["totalMapPool"].Despawn(spawnGo, 0f, worldMapAccessManager.totalMapPoolParentGo);
		PlayerDataManager.isNeedEffectNewWorldMapPoint = false;
		PlayerDataManager.needEffectNewWorldMapPointName = "";
		Transition(stateLink);
	}
}
