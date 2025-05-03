using Arbor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class RefreshExpFrameData : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private DungeonMapStatusManager dungeonMapStatusManager;

	private ParameterContainer parameterContainer;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		dungeonMapStatusManager = GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapStatusManager>();
		parameterContainer = GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		int @int = parameterContainer.GetInt("characterID");
		int int2 = parameterContainer.GetInt("partyMemberNum");
		TextMeshProUGUI textMeshProUGUI = parameterContainer.GetVariable<SliderAndTmpText>("expFrameValue").textMeshProUGUI;
		Slider slider = parameterContainer.GetVariable<SliderAndTmpText>("expFrameValue").slider;
		textMeshProUGUI.text = PlayerStatusDataManager.characterLv[@int].ToString();
		slider.maxValue = PlayerStatusDataManager.characterNextLvExp[@int];
		slider.minValue = PlayerStatusDataManager.characterCurrentLvExp[@int];
		slider.value = PlayerStatusDataManager.characterExp[@int];
		parameterContainer.GetVariable<UguiImage>("characterImage").image.sprite = dungeonMapManager.characterThumnailSpriteArray[@int];
		dungeonMapStatusManager.isExpFrameSetUp[int2] = true;
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
