using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class AdoptMagicMaterialItemIDfromTMP : StateBehaviour
{
	private CraftAddOnManager craftAddOnManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftAddOnManager = GameObject.Find("Craft AddOn Manager").GetComponent<CraftAddOnManager>();
	}

	public override void OnStateBegin()
	{
		string selectAddOnType = craftAddOnManager.selectAddOnType;
		if (!(selectAddOnType == "addOn_TYPE"))
		{
			if (selectAddOnType == "addOn_POWER")
			{
				craftAddOnManager.selectedMagicMatrialID[1] = craftAddOnManager.selectedMagicMaterialID_Temp;
			}
		}
		else
		{
			craftAddOnManager.selectedMagicMatrialID[0] = craftAddOnManager.selectedMagicMaterialID_Temp;
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
