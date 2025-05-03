using DarkTonic.MasterAudio;
using UnityEngine;
using UnityEngine.UI;

public class FactorDisposalButtonClickAction : MonoBehaviour
{
	private FactorDisposalManager factorDisposalManager;

	public int factorID;

	public int uniqueID;

	public Toggle toggle;

	public bool isInitialized;

	private void Awake()
	{
		factorDisposalManager = GameObject.Find("Factor Disposal Manager").GetComponent<FactorDisposalManager>();
		toggle.group = factorDisposalManager.GetComponent<ToggleGroup>();
	}

	public void PushDisposalToggle()
	{
		if (isInitialized)
		{
			MasterAudio.PlaySound("SeTabSwitch", 1f, null, 0f, null, null);
			if (toggle.isOn)
			{
				factorDisposalManager.tempSelectFactorID = factorID;
				factorDisposalManager.tempSelectUniqueID = uniqueID;
			}
			else
			{
				factorDisposalManager.tempSelectFactorID = 999;
				factorDisposalManager.tempSelectUniqueID = 0;
			}
			factorDisposalManager.SetDisposalApplyButtonInteractable();
		}
	}
}
