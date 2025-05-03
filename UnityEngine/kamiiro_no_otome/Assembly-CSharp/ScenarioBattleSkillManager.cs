using System.Collections.Generic;
using System.Linq;
using Arbor;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioBattleSkillManager : MonoBehaviour
{
	public ArborFSM itemFSM;

	public ArborFSM skillFSM;

	public ArborFSM chargeFSM;

	public GameObject itemWindow;

	public GameObject[] blackImageGoArray;

	public GameObject[] scrollContentPrefabGoArray;

	public Sprite[] scrollContentSpriteArray;

	public GameObject itemContentGo;

	public Transform battleItemSpawnParent;

	public CanvasGroup itemApplyButton;

	public GameObject skillWindow;

	public GameObject skillContentGo;

	public Transform battleSkillSpawnParent;

	public CanvasGroup skillApplyButton;

	public Localize scrollSummaryNeedMpTextLoc;

	public GameObject skillWindowMpCategoryGo;

	public RectTransform skillWindowMpFrame;

	public Button[] skillWindowMpCategoryButton;

	public Sprite[] skillWindowMpCategoryButtonSprite;

	public Localize skillWindowMpCategoryLoc;

	public Sprite[] skillButtonSpriteArray;

	public Sprite[] skillButtonIconSpriteArray;

	public int selectSkillMpType;

	public int selectSkillTypeNum;

	public GameObject commandClickSummaryWindow;

	public Localize[] commandClickSummaryTextLocArray;

	public GameObject commandClickKeyDownGroupGo;

	public bool isSelectToEnemy;

	public Sprite[] enemyChargeSpriteArray;

	public Sprite noItemImageSprite;

	public int scrollContentClickNum;

	public bool isScrollContentClick;

	public bool isUseSkill;

	public bool isOpenItemOrSkillWindow;

	public bool isUseChargeSkill;

	public List<int> chargeSkillCharacterId;

	public bool isEnemyChargeMax;

	public bool isSkillAverageDamage;

	public string playingSKillVoiceGroupName;

	public bool GetSelectToEnemy()
	{
		return isSelectToEnemy;
	}

	public string GetAllTargetTerm()
	{
		string text = "";
		if (PlayerStatusDataManager.playerPartyMember.Length > 1)
		{
			isSkillAverageDamage = true;
			if (PlayerStatusDataManager.playerPartyMember.Contains(0))
			{
				return "battleTextPlayerAllTarget";
			}
			return "battleTextPlayerAllTarget2";
		}
		isSkillAverageDamage = false;
		return "character" + PlayerStatusDataManager.playerPartyMember[0];
	}
}
