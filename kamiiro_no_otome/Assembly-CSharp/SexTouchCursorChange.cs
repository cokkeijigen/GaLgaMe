using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SexTouchCursorChange : StateBehaviour
{
	public enum Type
	{
		enter,
		exit
	}

	private SexTouchManager sexTouchManager;

	private SexTouchHeroineDataManager sexTouchHeroineDataManager;

	public Type type;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexTouchManager = GameObject.Find("Sex Touch Manager").GetComponent<SexTouchManager>();
		sexTouchHeroineDataManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchHeroineDataManager>();
	}

	public override void OnStateBegin()
	{
		switch (type)
		{
		case Type.enter:
			Cursor.SetCursor(sexTouchManager.touchAreaHandTexture, Vector2.zero, CursorMode.Auto);
			break;
		case Type.exit:
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
			break;
		}
		Transition(stateLink);
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
}
