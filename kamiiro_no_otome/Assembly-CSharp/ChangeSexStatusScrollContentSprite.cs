using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class ChangeSexStatusScrollContentSprite : StateBehaviour
{
	private StatusManager statusManager;

	private SexStatusManager sexStatusManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
		sexStatusManager = GameObject.Find("Sex Status Manager").GetComponent<SexStatusManager>();
	}

	public override void OnStateBegin()
	{
		if (!sexStatusManager.isSelectTypePassvie || statusManager.selectCharacterNum != 0)
		{
			foreach (Transform item in sexStatusManager.sexStatusContentGO.transform)
			{
				item.GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage").image.sprite = statusManager.selectScrollContentSpriteArray[0];
			}
			sexStatusManager.sexStatusContentGO.transform.GetChild(sexStatusManager.selectSexSkillScrollContentIndex).GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage")
				.image.sprite = statusManager.selectScrollContentSpriteArray[1];
		}
		if (!sexStatusManager.isSelectTypePassvie)
		{
			sexStatusManager.sexSkillTypeButtonGoArray[0].GetComponent<Image>().sprite = sexStatusManager.sexSkillTypeSpriteArray[0];
			sexStatusManager.sexSkillTypeButtonGoArray[1].GetComponent<Image>().sprite = sexStatusManager.sexSkillTypeSpriteArray[1];
		}
		else
		{
			sexStatusManager.sexSkillTypeButtonGoArray[0].GetComponent<Image>().sprite = sexStatusManager.sexSkillTypeSpriteArray[1];
			sexStatusManager.sexSkillTypeButtonGoArray[1].GetComponent<Image>().sprite = sexStatusManager.sexSkillTypeSpriteArray[0];
		}
		GameObject[] sexSkillTabButtonGoArray = sexStatusManager.sexSkillTabButtonGoArray;
		for (int i = 0; i < sexSkillTabButtonGoArray.Length; i++)
		{
			sexSkillTabButtonGoArray[i].GetComponent<Image>().sprite = statusManager.categoryTabSpriteArray[0];
		}
		sexStatusManager.sexSkillTabButtonGoArray[sexStatusManager.selectSexSkillCharacterTabIndex].GetComponent<Image>().sprite = statusManager.categoryTabSpriteArray[1];
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
