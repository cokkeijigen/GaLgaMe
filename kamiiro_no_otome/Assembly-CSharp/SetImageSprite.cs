using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class SetImageSprite : StateBehaviour
{
	public Sprite sprite;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		GetComponent<Image>().sprite = sprite;
		GetComponent<Image>().SetNativeSize();
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
