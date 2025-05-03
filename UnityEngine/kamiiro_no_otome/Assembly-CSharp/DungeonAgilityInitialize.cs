using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class DungeonAgilityInitialize : StateBehaviour
{
	public Slider agilitySlider;

	private ParameterContainer parameterContainer;

	private DungeonBattleManager dungeonBattleManager;

	private DungeonMapManager dungeonMapManager;

	public bool isPlayerAgility;

	public StateLink stateLink;

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
		float num = 0f;
		float num2 = 0f;
		if (isPlayerAgility)
		{
			int @int = parameterContainer.GetInt("characterID");
			int int2 = parameterContainer.GetInt("partyMemberNum");
			parameterContainer.GetVariable<UguiImage>("characterImage").image.sprite = dungeonMapManager.characterBattleThumnailSpriteArray[@int];
			agilitySlider.value = 0f;
			GetComponent<DungeonCharacterAgility>().isCoroutineStop = false;
			parameterContainer.SetBool("isInitialize", value: true);
			dungeonBattleManager.isCharacterAgilitySetUp[int2] = true;
		}
		else
		{
			if (PlayerDataManager.playerLibido < 25)
			{
				agilitySlider.value = 0f;
			}
			else if (PlayerDataManager.playerLibido < 50)
			{
				num = 25f;
				num2 = 50f;
				agilitySlider.value = Random.Range(num, num2);
			}
			else if (PlayerDataManager.playerLibido < 75)
			{
				num = 50f;
				num2 = 75f;
				agilitySlider.value = Random.Range(num, num2);
				Debug.Log("アジリティ幅：" + num + "／" + num2);
			}
			else
			{
				num = 75f;
				num2 = 100f;
				agilitySlider.value = Random.Range(num, num2);
			}
			parameterContainer.SetBool("isInitialize", value: true);
			int int3 = parameterContainer.GetInt("enemyPartyNum");
			dungeonBattleManager.isEnemyAgilitySetUp[int3] = true;
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
