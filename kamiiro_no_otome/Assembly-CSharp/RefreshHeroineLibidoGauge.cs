using Arbor;
using DarkTonic.MasterAudio;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class RefreshHeroineLibidoGauge : StateBehaviour
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
		sexTouchStatusManager.heroineLibidoPoint = Mathf.Clamp(sexTouchStatusManager.heroineLibidoPoint + sexTouchStatusManager.heroineLibidoAddPoint, 0f, 80f);
		float num = sexTouchStatusManager.heroineLibidoPoint / 100f + 0.2f;
		sexTouchStatusManager.heroineLibidoHeartRect.localScale = new Vector2(num, num);
		sexTouchStatusManager.heroineLibidoAnimator.SetTrigger("addLibido");
		int num2 = 0;
		num2 = ((!(sexTouchStatusManager.heroineLibidoAddPoint < 2f)) ? ((sexTouchStatusManager.heroineLibidoAddPoint < 4f) ? 1 : 2) : 0);
		Transform transform = PoolManager.Pools["sexTouchPool"].Spawn(sexTouchManager.heroineClickHeartPrefabGoArray[num2], sexTouchManager.touchAreaPointGoArray[sexTouchManager.clickSelectAreaPointIndex - 1].transform);
		transform.localScale = new Vector3(1f, 1f, 1f);
		transform.localPosition = new Vector3(0f, 0f, 0f);
		PoolManager.Pools["sexTouchPool"].Despawn(transform, 1f, sexTouchManager.prefabParentTransform);
		sexTouchStatusManager.PlayTouchVoice();
		if (sexTouchManager.isTouchSePlaying)
		{
			MasterAudio.PlaySound("SexBattle_Caress", 1f, null, 0f, null, null);
		}
		if (sexTouchStatusManager.heroineLibidoPoint >= 80f)
		{
			Transition(stateLink);
		}
		else
		{
			Invoke("InvokeMethod", waitTime);
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

	private void InvokeMethod()
	{
		Transition(stateLink);
	}
}
