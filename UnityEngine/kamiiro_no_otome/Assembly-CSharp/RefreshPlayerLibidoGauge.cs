using Arbor;
using DarkTonic.MasterAudio;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class RefreshPlayerLibidoGauge : StateBehaviour
{
	private SexTouchManager sexTouchManager;

	private SexTouchStatusManager sexTouchStatusManager;

	public float waitTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexTouchManager = GameObject.Find("Sex Touch Manager").GetComponent<SexTouchManager>();
		sexTouchStatusManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchStatusManager>();
	}

	public override void OnStateBegin()
	{
		sexTouchStatusManager.playerLibidoPoint = Mathf.Clamp(sexTouchStatusManager.playerLibidoPoint + sexTouchStatusManager.playerLibidoAddPoint, 0, 80);
		float num = (float)sexTouchStatusManager.playerLibidoPoint / 100f + 0.2f;
		sexTouchStatusManager.playerLibidoHeartRect.localScale = new Vector2(num, num);
		sexTouchStatusManager.playerLibidoAnimator.SetTrigger("addLibido");
		Transform transform = PoolManager.Pools["sexTouchPool"].Spawn(sexTouchManager.heroineClickHeartPrefabGoArray[3], sexTouchManager.touchAreaPointGoArray[0].transform);
		transform.localScale = new Vector3(1f, 1f, 1f);
		transform.localPosition = new Vector3(0f, 0f, 0f);
		PoolManager.Pools["sexTouchPool"].Despawn(transform, 1f, sexTouchManager.prefabParentTransform);
		string text = "Voice_Fellatio_" + PlayerNonSaveDataManager.selectSexBattleHeroineId;
		if (!MasterAudio.IsSoundGroupPlaying(text))
		{
			Debug.Log("フェラ音声再生なし：" + text);
			MasterAudio.PlaySound(text, 1f, null, 0f, null, null);
		}
		else
		{
			Debug.Log("フェラ音声再生中：" + text);
		}
		if (sexTouchStatusManager.playerLibidoPoint >= 80)
		{
			Transition(stateLink);
			return;
		}
		sexTouchStatusManager.isFellatioClick = false;
		Invoke("InvokeMethod", waitTime);
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

	private void InvokeMethod()
	{
		Transition(stateLink);
	}
}
