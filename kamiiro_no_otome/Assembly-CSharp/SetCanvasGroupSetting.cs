using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetCanvasGroupSetting : StateBehaviour
{
	public bool isSelf;

	public string findGoName;

	public bool blocksRayCasts;

	public bool interactable;

	public float alpha;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		if (isSelf)
		{
			GetComponent<CanvasGroup>().interactable = interactable;
			GetComponent<CanvasGroup>().blocksRaycasts = blocksRayCasts;
			GetComponent<CanvasGroup>().alpha = alpha;
		}
		else if (GameObject.Find(findGoName) != null)
		{
			GameObject.Find(findGoName).GetComponent<CanvasGroup>().interactable = interactable;
			GameObject.Find(findGoName).GetComponent<CanvasGroup>().blocksRaycasts = blocksRayCasts;
			GameObject.Find(findGoName).GetComponent<CanvasGroup>().alpha = alpha;
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
