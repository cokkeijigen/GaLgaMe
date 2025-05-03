using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetStatusWindowVisible : StateBehaviour
{
	private GameObject commonCanvasGO;

	private GameObject itemCanvasGO;

	private GameObject skillCanvasGO;

	private GameObject statusCanvasGO;

	private GameObject equipCanvasGO;

	public bool commonActive;

	public bool itemActive;

	public bool statusActive;

	public bool equipActive;

	public bool skillActive;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		commonCanvasGO = GameObject.Find("Status Canvas Group").transform.Find("Common Canvas").gameObject;
		itemCanvasGO = GameObject.Find("Status Canvas Group").transform.Find("Item Canvas").gameObject;
		skillCanvasGO = GameObject.Find("Status Canvas Group").transform.Find("Skill Canvas").gameObject;
		statusCanvasGO = GameObject.Find("Status Canvas Group").transform.Find("Status Canvas").gameObject;
		equipCanvasGO = GameObject.Find("Status Canvas Group").transform.Find("Equip Canvas").gameObject;
	}

	public override void OnStateBegin()
	{
		commonCanvasGO.SetActive(commonActive);
		itemCanvasGO.SetActive(itemActive);
		skillCanvasGO.SetActive(skillActive);
		statusCanvasGO.SetActive(statusActive);
		equipCanvasGO.SetActive(equipActive);
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
