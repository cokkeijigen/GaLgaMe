using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class EnemyAgilityInitialize : StateBehaviour
{
	public Slider agilitySlider;

	private ParameterContainer parameterContainer;

	private DungeonBattleManager dungeonBattleManager;

	public StateLink initializeLink;

	public StateLink resetLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		parameterContainer = GetComponent<ParameterContainer>();
		dungeonBattleManager = GameObject.Find("Dungeon Battle Manager").GetComponent<DungeonBattleManager>();
	}

	public override void OnStateBegin()
	{
		int @int = parameterContainer.GetInt("enemyPartyNum");
		bool @bool = parameterContainer.GetBool("isInitialize");
		agilitySlider.value = 0f;
		if (!@bool)
		{
			parameterContainer.SetBool("isInitialize", value: true);
			GetComponent<DungeonCharacterAgility>().isCoroutineStop = false;
			dungeonBattleManager.isEnemyAgilitySetUp[@int] = true;
			Debug.Log("敵アジリティ初期化完了");
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
