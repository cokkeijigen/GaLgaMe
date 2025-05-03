using System.Collections.Generic;
using System.Linq;
using I2.Loc;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class BattleSkillButtonManagerForPM : SerializedMonoBehaviour
{
	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public PlayMakerFSM skillButtonFSM;

	public Animator halfChangeCicleAnimator;

	public Animator partyHalfChangeCicleAnimator;

	public GameObject[] frameSkillButtonGroupArray;

	public GameObject[] frameChargeCircleGroupArray;

	public GameObject[] chargeCircleGoArray;

	public Image[] skillButtonImageArray;

	public GameObject[] partyChargeCircleGoArray;

	public Image[] partySkillButtonImageArray;

	public Image[] skillIconImageArray;

	public Localize[] chargeTextLocArray;

	public Image[] partySkillIconImageArray;

	public Localize[] partyChargeTextLocArray;

	public Sprite[] skillButtonSpriteArray;

	public Sprite[] skillButtonHalfSpriteArray;

	public Sprite[] skillIconSpriteArray;

	public Sprite[] skillIconHalfSpriteArray;

	private void Awake()
	{
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
	}

	public void SetSkillButtonVisible(int characterId)
	{
		if (characterId == 0)
		{
			frameSkillButtonGroupArray[0].SetActive(value: true);
			frameSkillButtonGroupArray[1].SetActive(value: false);
			frameChargeCircleGroupArray[0].SetActive(value: true);
			frameChargeCircleGroupArray[1].SetActive(value: false);
			if (CheckIsEdenContract())
			{
				skillButtonImageArray[0].gameObject.SetActive(value: true);
				skillButtonImageArray[1].gameObject.SetActive(value: true);
				skillButtonImageArray[2].gameObject.SetActive(value: true);
				Debug.Log("TPMPスキルボタンを表示");
			}
			else
			{
				skillButtonImageArray[0].gameObject.SetActive(value: true);
				skillButtonImageArray[1].gameObject.SetActive(value: false);
				skillButtonImageArray[2].gameObject.SetActive(value: false);
				Debug.Log("TPスキルボタンを表示");
			}
		}
		else
		{
			frameSkillButtonGroupArray[0].SetActive(value: false);
			frameSkillButtonGroupArray[1].SetActive(value: true);
			frameChargeCircleGroupArray[0].SetActive(value: false);
			frameChargeCircleGroupArray[1].SetActive(value: true);
			skillButtonImageArray[0].gameObject.SetActive(value: false);
			skillButtonImageArray[1].gameObject.SetActive(value: true);
			skillButtonImageArray[2].gameObject.SetActive(value: true);
			chargeCircleGoArray[0].SetActive(value: false);
			Debug.Log("MPスキルボタンを表示");
		}
		skillButtonFSM.SendEvent("SkillButtonRefresh");
	}

	public bool CheckIsEdenContract()
	{
		bool result = false;
		if (!(PlayerNonSaveDataManager.resultScenarioName == "M_Main_001-1"))
		{
			result = true;
		}
		return result;
	}

	public bool CheckCharacterDeath(int characterId)
	{
		bool flag = false;
		flag = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == characterId).isDead;
		Debug.Log("キャラの死亡を確認する／ID：" + characterId + "／死亡：" + flag);
		return flag;
	}

	public bool CheckSkillUse(int characterId)
	{
		bool flag = false;
		flag = scenarioBattleTurnManager.isUseSkillCheckArray[characterId];
		Debug.Log("スキルの使用を確認する／ID：" + characterId + "／使用：" + flag);
		return flag;
	}

	public bool CheckEnableUseMpSkill(int characterId, bool isAttack)
	{
		bool result = false;
		int num = 0;
		List<PlayerBattleConditionManager.MemberSkillReChargeTurn> list = null;
		num = ((characterId != 0) ? characterId : PlayerStatusDataManager.partyMemberCount);
		IEnumerable<PlayerBattleConditionManager.MemberSkillReChargeTurn> source = PlayerBattleConditionManager.playerSkillRechargeTurn[num].Where((PlayerBattleConditionManager.MemberSkillReChargeTurn data) => data.needRechargeTurn == 0);
		list = ((!isAttack) ? source.Where((PlayerBattleConditionManager.MemberSkillReChargeTurn data) => data.skillID >= 200).ToList() : source.Where((PlayerBattleConditionManager.MemberSkillReChargeTurn data) => data.skillID < 200).ToList());
		if (list.Any())
		{
			result = true;
		}
		return result;
	}

	public bool CheckEnableUseTpSkill()
	{
		bool result = false;
		if (PlayerBattleConditionManager.playerSkillRechargeTurn[0].Where((PlayerBattleConditionManager.MemberSkillReChargeTurn data) => data.needRechargeTurn == 0).ToList().Any())
		{
			result = true;
		}
		return result;
	}

	public bool CheckEnoughMpSkill(int characterId, bool isAttack)
	{
		bool result = false;
		int num = 0;
		List<PlayerBattleConditionManager.MemberSkillReChargeTurn> rechargeList = null;
		num = ((characterId != 0) ? characterId : PlayerStatusDataManager.partyMemberCount);
		IEnumerable<PlayerBattleConditionManager.MemberSkillReChargeTurn> source = PlayerBattleConditionManager.playerSkillRechargeTurn[num].Where((PlayerBattleConditionManager.MemberSkillReChargeTurn data) => data.needRechargeTurn == 0);
		if (isAttack)
		{
			rechargeList = source.Where((PlayerBattleConditionManager.MemberSkillReChargeTurn data) => data.skillID < 200).ToList();
		}
		else
		{
			rechargeList = source.Where((PlayerBattleConditionManager.MemberSkillReChargeTurn data) => data.skillID >= 200).ToList();
		}
		int num2 = 0;
		num2 = PlayerStatusDataManager.characterMp[characterId];
		int i;
		for (i = 0; i < rechargeList.Count; i++)
		{
			int useMP = GameDataManager.instance.playerSkillDataBase.skillDataList.Find((BattleSkillData data) => data.skillID == rechargeList[i].skillID).useMP;
			if (num2 >= useMP)
			{
				Debug.Log("キャラID：" + characterId + "／スキルID：" + rechargeList[i].skillID + "／コストMP：" + useMP + "／現在のMP：" + num2);
				result = true;
				break;
			}
		}
		return result;
	}

	public bool CheckEnoughTpSkill()
	{
		bool result = false;
		List<PlayerBattleConditionManager.MemberSkillReChargeTurn> list = PlayerBattleConditionManager.playerSkillRechargeTurn[0].Where((PlayerBattleConditionManager.MemberSkillReChargeTurn data) => data.needRechargeTurn == 0).ToList();
		int weaponID = PlayerEquipDataManager.playerEquipWeaponID[0];
		int weaponIncludeMp = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == weaponID && data.equipCharacter == 0).weaponIncludeMp;
		int i;
		for (i = 0; i < list.Count; i++)
		{
			int useMP = GameDataManager.instance.playerSkillDataBase.skillDataList.Find((BattleSkillData data) => data.skillID == list[i].skillID).useMP;
			if (weaponIncludeMp >= useMP)
			{
				Debug.Log("スキルID：" + list[i].skillID + "／コストTP：" + useMP + "／現在のTP：" + weaponIncludeMp);
				result = true;
				break;
			}
		}
		return result;
	}

	public void SetHalfSkillButtonGraphic(int characterId, string type, int index, bool isMpButton)
	{
		switch (type)
		{
		case "death":
			skillButtonImageArray[index].sprite = skillButtonHalfSpriteArray[1];
			skillButtonImageArray[index].color = new Color(0.6f, 0.6f, 0.6f);
			skillIconImageArray[index].sprite = skillIconHalfSpriteArray[index];
			skillIconImageArray[index].color = new Color(0.3f, 0.3f, 0.3f);
			chargeTextLocArray[index].Term = GetSkillButtonTextLoc(index, isMpButton);
			chargeCircleGoArray[index].SetActive(value: false);
			Debug.Log("半分スキルボタン死亡中");
			break;
		case "normal":
			skillButtonImageArray[index].sprite = skillButtonHalfSpriteArray[0];
			skillButtonImageArray[index].color = Color.white;
			skillIconImageArray[index].sprite = skillIconHalfSpriteArray[index];
			skillIconImageArray[index].color = Color.white;
			chargeTextLocArray[index].Term = GetSkillButtonTextLoc(index, isMpButton);
			Debug.Log("半分使用可能なスキルあり");
			break;
		case "skillUsed":
			skillButtonImageArray[index].sprite = skillButtonHalfSpriteArray[1];
			skillButtonImageArray[index].color = Color.white;
			skillIconImageArray[index].sprite = skillIconHalfSpriteArray[index];
			skillIconImageArray[index].color = new Color(0.85f, 0.85f, 0.85f);
			chargeTextLocArray[index].Term = GetSkillButtonTextLoc(index, isMpButton);
			chargeCircleGoArray[index].SetActive(value: false);
			Debug.Log("半分スキル使用済みorMP足りない");
			break;
		case "recharge":
			skillButtonImageArray[index].sprite = skillButtonHalfSpriteArray[2];
			skillButtonImageArray[index].color = Color.white;
			skillIconImageArray[index].sprite = skillIconHalfSpriteArray[3];
			skillIconImageArray[index].color = new Color(0.6f, 0.6f, 0.6f);
			chargeTextLocArray[index].Term = "skillButton_allReCharge";
			chargeCircleGoArray[index].SetActive(value: false);
			Debug.Log("半分スキル全チャージ中");
			break;
		}
	}

	public void SetPartyHalfSkillButtonGraphic(int characterId, string type, int index, bool isMpButton)
	{
		switch (type)
		{
		case "death":
			partySkillButtonImageArray[index].sprite = skillButtonHalfSpriteArray[1];
			partySkillButtonImageArray[index].color = new Color(0.6f, 0.6f, 0.6f);
			partySkillIconImageArray[index].sprite = skillIconHalfSpriteArray[index + 1];
			partySkillIconImageArray[index].color = new Color(0.3f, 0.3f, 0.3f);
			partyChargeTextLocArray[index].Term = GetPartySkillButtonTextLoc(index, isMpButton);
			partyChargeCircleGoArray[index].SetActive(value: false);
			Debug.Log("半分スキルボタン死亡中");
			break;
		case "normal":
			partySkillButtonImageArray[index].sprite = skillButtonHalfSpriteArray[0];
			partySkillButtonImageArray[index].color = Color.white;
			partySkillIconImageArray[index].sprite = skillIconHalfSpriteArray[index + 1];
			partySkillIconImageArray[index].color = Color.white;
			partyChargeTextLocArray[index].Term = GetPartySkillButtonTextLoc(index, isMpButton);
			Debug.Log("半分使用可能なスキルあり");
			break;
		case "skillUsed":
			partySkillButtonImageArray[index].sprite = skillButtonHalfSpriteArray[1];
			partySkillButtonImageArray[index].color = Color.white;
			partySkillIconImageArray[index].sprite = skillIconHalfSpriteArray[index + 1];
			partySkillIconImageArray[index].color = new Color(0.85f, 0.85f, 0.85f);
			partyChargeTextLocArray[index].Term = GetPartySkillButtonTextLoc(index, isMpButton);
			partyChargeCircleGoArray[index].SetActive(value: false);
			Debug.Log("半分スキル使用済みorMP足りない");
			break;
		case "recharge":
			partySkillButtonImageArray[index].sprite = skillButtonHalfSpriteArray[2];
			partySkillButtonImageArray[index].color = Color.white;
			partySkillIconImageArray[index].sprite = skillIconHalfSpriteArray[3];
			partySkillIconImageArray[index].color = new Color(0.6f, 0.6f, 0.6f);
			partyChargeTextLocArray[index].Term = "skillButton_allReCharge";
			partyChargeCircleGoArray[index].SetActive(value: false);
			Debug.Log("半分スキル全チャージ中");
			break;
		}
	}

	public void SetSkillButtonGraphic(string type, int index, bool isMpButton)
	{
		switch (type)
		{
		case "death":
			skillButtonImageArray[2].sprite = skillButtonSpriteArray[1];
			skillButtonImageArray[2].color = new Color(0.6f, 0.6f, 0.6f);
			skillIconImageArray[2].sprite = skillIconSpriteArray[index];
			skillIconImageArray[2].color = new Color(0.3f, 0.3f, 0.3f);
			chargeTextLocArray[2].Term = GetSkillButtonTextLoc(index, isMpButton);
			chargeCircleGoArray[2].SetActive(value: false);
			Debug.Log("仲間スキルボタン死亡中");
			break;
		case "normal":
			skillButtonImageArray[2].sprite = skillButtonSpriteArray[0];
			skillButtonImageArray[2].color = Color.white;
			skillIconImageArray[2].sprite = skillIconSpriteArray[index];
			skillIconImageArray[2].color = Color.white;
			chargeTextLocArray[2].Term = GetSkillButtonTextLoc(index, isMpButton);
			chargeCircleGoArray[2].SetActive(value: true);
			Debug.Log("仲間使用可能なスキルあり");
			break;
		case "skillUsed":
			skillButtonImageArray[2].sprite = skillButtonSpriteArray[1];
			skillButtonImageArray[2].color = Color.white;
			skillIconImageArray[2].sprite = skillIconSpriteArray[index];
			skillIconImageArray[2].color = new Color(0.85f, 0.85f, 0.85f);
			chargeTextLocArray[2].Term = GetSkillButtonTextLoc(index, isMpButton);
			chargeCircleGoArray[2].SetActive(value: false);
			Debug.Log("仲間スキル使用済み");
			break;
		case "recharge":
			skillButtonImageArray[2].sprite = skillButtonSpriteArray[2];
			skillButtonImageArray[2].color = Color.white;
			skillIconImageArray[2].sprite = skillIconSpriteArray[3];
			skillIconImageArray[2].color = new Color(0.6f, 0.6f, 0.6f);
			chargeTextLocArray[2].Term = "skillButton_allReCharge";
			chargeCircleGoArray[2].SetActive(value: false);
			Debug.Log("仲間スキル全チャージ中");
			break;
		}
	}

	private string GetSkillButtonTextLoc(int index, bool isMpButton)
	{
		string text = "";
		if (isMpButton)
		{
			if (index == 1)
			{
				return "skillButton_attack";
			}
			return "skillButton_support";
		}
		return "summaryTalisman";
	}

	private string GetPartySkillButtonTextLoc(int index, bool isMpButton)
	{
		string result = "";
		if (isMpButton)
		{
			result = ((index != 0) ? "skillButton_support" : "skillButton_attack");
		}
		return result;
	}

	public void SetFirstBattleHalfChangeCircleActive(bool isEnableTalisman)
	{
		int num = 0;
		if (isEnableTalisman)
		{
			num++;
		}
		switch (num)
		{
		case 1:
			halfChangeCicleAnimator.SetTrigger("talismanOnly");
			break;
		case 0:
			halfChangeCicleAnimator.SetTrigger("disable");
			break;
		}
		Debug.Log("最初の戦闘の半分スキルボタンのステート：" + num);
	}

	public void SetHalfChangeCircleActive(bool isEnableTalisman, bool isEnableAttack, bool isEnableSupport)
	{
		int num = 0;
		if (isEnableTalisman)
		{
			num++;
		}
		if (isEnableAttack)
		{
			num += 4;
		}
		if (isEnableSupport)
		{
			num += 7;
		}
		switch (num)
		{
		case 12:
			halfChangeCicleAnimator.SetTrigger("triple");
			break;
		case 11:
			halfChangeCicleAnimator.SetTrigger("mpDouble");
			break;
		case 8:
			halfChangeCicleAnimator.SetTrigger("tpAndSupport");
			break;
		case 7:
			halfChangeCicleAnimator.SetTrigger("support");
			break;
		case 5:
			halfChangeCicleAnimator.SetTrigger("tpAndAttack");
			break;
		case 4:
			halfChangeCicleAnimator.SetTrigger("attack");
			break;
		case 1:
			halfChangeCicleAnimator.SetTrigger("talisman");
			break;
		case 0:
			halfChangeCicleAnimator.SetTrigger("disable");
			break;
		}
		Debug.Log("半分スキルボタンのステート：" + num);
	}

	public void SetPartyHalfChangeCircleActive(bool isEnableAttack, bool isEnableSupport)
	{
		int num = 0;
		if (isEnableAttack)
		{
			num += 4;
		}
		if (isEnableSupport)
		{
			num += 7;
		}
		switch (num)
		{
		case 11:
			partyHalfChangeCicleAnimator.SetTrigger("mpDouble");
			break;
		case 7:
			partyHalfChangeCicleAnimator.SetTrigger("support");
			break;
		case 4:
			partyHalfChangeCicleAnimator.SetTrigger("attack");
			break;
		case 0:
			partyHalfChangeCicleAnimator.SetTrigger("disable");
			break;
		}
		Debug.Log("仲間半分スキルボタンのステート：" + num);
	}
}
