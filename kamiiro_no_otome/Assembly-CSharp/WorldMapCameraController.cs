using System.Collections.Generic;
using Arbor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[AddComponentMenu("")]
public class WorldMapCameraController : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	public float moveLimitX;

	public float moveLimitY;

	public bool isRayHit;

	public bool isButtonClick;

	private int resultCount;

	private Camera worldMapCamera;

	private Vector3 mouseStartPos;

	private Vector3 dragSetPos;

	private List<RaycastResult> raycastResults = new List<RaycastResult>();

	public ArborFSM playerFSM;

	public StateLink stateLink;

	public float resetTime;

	private void Start()
	{
		worldMapCamera = GetComponent<Camera>();
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
	}

	public override void OnStateBegin()
	{
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		if (Input.GetMouseButtonDown(0))
		{
			mouseStartPos = worldMapCamera.ScreenToWorldPoint(Input.mousePosition);
			isRayHit = IsPointerOnUGUI(Input.mousePosition);
		}
		if (Input.GetMouseButton(0))
		{
			if (mouseStartPos != Input.mousePosition && !isRayHit && !PlayerDataManager.worldMapInputBlock)
			{
				Vector3 vector = worldMapCamera.ScreenToWorldPoint(Input.mousePosition);
				Vector3 vector2 = mouseStartPos - vector;
				base.transform.position += vector2;
				dragSetPos = base.transform.position;
				if (base.transform.position.x > moveLimitX)
				{
					dragSetPos.x = moveLimitX;
				}
				else if (base.transform.position.x < moveLimitX * -1f)
				{
					dragSetPos.x = moveLimitX * -1f;
				}
				if (base.transform.position.y > moveLimitY)
				{
					dragSetPos.y = moveLimitY;
				}
				else if (base.transform.position.y < moveLimitY * -1f)
				{
					dragSetPos.y = moveLimitY * -1f;
				}
				base.transform.position = dragSetPos;
			}
			if (isRayHit && !isButtonClick)
			{
				foreach (RaycastResult raycastResult in raycastResults)
				{
					if (raycastResult.gameObject.CompareTag("AreaPoint"))
					{
						raycastResult.gameObject.GetComponent<Button>().onClick.Invoke();
						isButtonClick = true;
					}
				}
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			isButtonClick = false;
		}
	}

	public override void OnStateLateUpdate()
	{
	}

	private bool IsPointerOnUGUI(Vector2 screenPosition)
	{
		resultCount = 0;
		PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
		pointerEventData.position = screenPosition;
		EventSystem.current.RaycastAll(pointerEventData, raycastResults);
		foreach (RaycastResult raycastResult in raycastResults)
		{
			if (raycastResult.gameObject.CompareTag("AreaPoint"))
			{
				resultCount++;
			}
			if (raycastResult.gameObject.CompareTag("MenuButton"))
			{
				resultCount++;
			}
		}
		return resultCount > 0;
	}
}
