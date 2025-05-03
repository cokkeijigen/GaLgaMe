using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class OpenCommandClickSummaryWindow : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	private ScenarioBattleUiManager scenarioBattleUiManager;

	private bool isRevive;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
		scenarioBattleUiManager = GameObject.Find("Battle Ui Manager").GetComponent<ScenarioBattleUiManager>();
	}

	public override void OnStateBegin()
	{
		scenarioBattleSkillManager.blackImageGoArray[0].SetActive(value: false);
		scenarioBattleSkillManager.blackImageGoArray[1].SetActive(value: true);
		if (scenarioBattleSkillManager.isUseSkill)
		{
			scenarioBattleSkillManager.skillWindow.SetActive(value: false);
			if (scenarioBattleTurnManager.playerSkillData.skillTarget == BattleSkillData.SkillTarget.all)
			{
				scenarioBattleSkillManager.commandClickSummaryTextLocArray[1].Term = "battleSelectUseSkillAllTarget";
			}
			else
			{
				scenarioBattleSkillManager.commandClickSummaryTextLocArray[1].Term = "battleSelectUseSkillSoloTarget";
			}
			BattleSkillData playerSkillData = scenarioBattleTurnManager.playerSkillData;
			if (playerSkillData != null)
			{
				if (playerSkillData.skillType == BattleSkillData.SkillType.attack || playerSkillData.skillType == BattleSkillData.SkillType.magicAttack || playerSkillData.skillType == BattleSkillData.SkillType.mixAttack || playerSkillData.skillType == BattleSkillData.SkillType.deBuff)
				{
					SetTargetEnemy();
				}
				else if (playerSkillData.skillTarget == BattleSkillData.SkillTarget.solo || playerSkillData.skillTarget == BattleSkillData.SkillTarget.all)
				{
					if (playerSkillData.skillType == BattleSkillData.SkillType.revive)
					{
						isRevive = true;
					}
					else
					{
						isRevive = false;
					}
					SetTargetPlayer();
				}
				else
				{
					SetTargetSelf();
				}
			}
		}
		else
		{
			scenarioBattleSkillManager.itemWindow.SetActive(value: false);
			ItemData itemData = GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData data) => data.itemID == scenarioBattleTurnManager.battleUseItemID);
			if (itemData.target == ItemData.Target.all)
			{
				scenarioBattleSkillManager.commandClickSummaryTextLocArray[1].Term = "battleSelectUseItemAllTarget";
			}
			else
			{
				scenarioBattleSkillManager.commandClickSummaryTextLocArray[1].Term = "battleSelectUseItemSoloTarget";
			}
			if (itemData.category == ItemData.Category.revive)
			{
				isRevive = true;
			}
			else
			{
				isRevive = false;
			}
			SetTargetPlayer();
		}
		scenarioBattleSkillManager.commandClickSummaryWindow.SetActive(value: true);
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

	private void SetTargetPlayer()
	{
		scenarioBattleSkillManager.isSelectToEnemy = false;
		scenarioBattleSkillManager.commandClickKeyDownGroupGo.SetActive(value: false);
		scenarioBattleUiManager.SetMaterialEffect("player");
		if (isRevive)
		{
			scenarioBattleTurnManager.setFrameTypeName = "dead";
			scenarioBattleUiManager.uiFSM.SendTrigger("SetCharacterFrameType");
		}
		else
		{
			scenarioBattleTurnManager.setFrameTypeName = "alive";
			scenarioBattleUiManager.uiFSM.SendTrigger("SetCharacterFrameType");
		}
		Debug.Log("味方を対象にする");
	}

	private void SetTargetSelf()
	{
		scenarioBattleSkillManager.isSelectToEnemy = false;
		scenarioBattleSkillManager.commandClickKeyDownGroupGo.SetActive(value: false);
		scenarioBattleUiManager.SetMaterialEffect("player");
		scenarioBattleTurnManager.setFrameTypeName = "self";
		scenarioBattleUiManager.uiFSM.SendTrigger("SetCharacterFrameType");
		Debug.Log("自分を対象にする");
	}

	private void SetTargetEnemy()
	{
		scenarioBattleSkillManager.isSelectToEnemy = true;
		scenarioBattleSkillManager.commandClickKeyDownGroupGo.SetActive(value: true);
		scenarioBattleUiManager.SetMaterialEffect("enemy");
		Debug.Log("敵を対象にする");
	}
}
