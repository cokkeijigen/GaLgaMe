using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class CharacterAgilityInitialize : StateBehaviour
{
	public Slider agilitySlider;

	private ParameterContainer parameterContainer;

	private DungeonBattleManager dungeonBattleManager;

	private DungeonMapManager dungeonMapManager;

	public StateLink initializeLink;

	public StateLink resetLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		parameterContainer = GetComponent<ParameterContainer>();
		dungeonBattleManager = GameObject.Find("Dungeon Battle Manager").GetComponent<DungeonBattleManager>();
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
	}

	public override void OnStateBegin()
	{
		int @int = parameterContainer.GetInt("characterID");
		int int2 = parameterContainer.GetInt("partyMemberNum");
		bool @bool = parameterContainer.GetBool("isInitialize");
		parameterContainer.GetVariable<UguiImage>("characterImage").image.sprite = dungeonMapManager.characterBattleThumnailSpriteArray[@int];
		agilitySlider.value = 0f;
		if (!@bool)
		{
			GetComponent<DungeonCharacterAgility>().isCoroutineStop = false;
			parameterContainer.SetBool("isInitialize", value: true);
			dungeonBattleManager.isCharacterAgilitySetUp[int2] = true;
			Debug.Log("アジリティ初期化完了");
			Transition(initializeLink);
		}
		else
		{
			Transition(resetLink);
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
}
