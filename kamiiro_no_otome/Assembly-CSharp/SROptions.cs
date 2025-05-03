using System;
using System.ComponentModel;
using Arbor;
using SRDebugger;
using SRDebugger.Internal;
using SRF.Service;
using UnityEngine;
using UnityEngine.Scripting;
using Utage;

[Preserve]
public class SROptions : INotifyPropertyChanged
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
	public sealed class DisplayNameAttribute : System.ComponentModel.DisplayNameAttribute
	{
		public DisplayNameAttribute(string displayName)
			: base(displayName)
		{
		}
	}

	[AttributeUsage(AttributeTargets.Property)]
	public sealed class IncrementAttribute : SRDebugger.IncrementAttribute
	{
		public IncrementAttribute(double increment)
			: base(increment)
		{
		}
	}

	[AttributeUsage(AttributeTargets.Property)]
	public sealed class NumberRangeAttribute : SRDebugger.NumberRangeAttribute
	{
		public NumberRangeAttribute(double min, double max)
			: base(min, max)
		{
		}
	}

	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
	public sealed class SortAttribute : SRDebugger.SortAttribute
	{
		public SortAttribute(int priority)
			: base(priority)
		{
		}
	}

	private static SROptions _current;

	public static SROptions Current => _current;

	public event SROptionsPropertyChanged PropertyChanged;

	private event PropertyChangedEventHandler InterfacePropertyChangedEventHandler;

	event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
	{
		add
		{
			InterfacePropertyChangedEventHandler += value;
		}
		remove
		{
			InterfacePropertyChangedEventHandler -= value;
		}
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
	public static void OnStartup()
	{
		_current = new SROptions();
		SRServiceManager.GetService<InternalOptionsRegistry>().AddOptionContainer(Current);
	}

	public void OnPropertyChanged(string propertyName)
	{
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, propertyName);
		}
		if (this.InterfacePropertyChangedEventHandler != null)
		{
			this.InterfacePropertyChangedEventHandler(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	[DisplayName("コマンド戦闘を勝利で終わらせる")]
	[Category("通常バトル")]
	public void ScenarioBattleDebugWin()
	{
		GameObject gameObject = GameObject.Find("Battle Turn Manager");
		if (gameObject != null)
		{
			gameObject.GetComponent<ArborFSM>().SendTrigger("StartBattleWin");
			Debug.Log("戦闘を勝利で終わらせる");
		}
		else
		{
			Debug.Log("戦闘を勝利で終わらせる／ゲームオブジェクトが存在しない");
		}
	}

	[DisplayName("味方MPをゼロにする")]
	[Category("通常バトル")]
	public void CharacterMpZero()
	{
		for (int i = 0; i < 5; i++)
		{
			PlayerStatusDataManager.characterMp[i] = 0;
		}
		Debug.Log("味方MPをゼロにする");
	}

	[DisplayName("味方全員のHPを1にする")]
	[Category("通常バトル")]
	public void CharacterHpOne()
	{
		for (int i = 0; i < 5; i++)
		{
			PlayerStatusDataManager.characterHp[i] = 1;
		}
		Debug.Log("味方HPを1にする");
	}

	[DisplayName("エデンのHPを1にする")]
	[Category("通常バトル")]
	public void CharacterEdenHpOne()
	{
		PlayerStatusDataManager.characterHp[0] = 1;
		Debug.Log("エデンのHPを1にする");
	}

	[DisplayName("リィナのHPを1にする")]
	[Category("通常バトル")]
	public void Character2HpOne()
	{
		PlayerStatusDataManager.characterHp[2] = 1;
		Debug.Log("リィナのHPを1にする");
	}

	[DisplayName("シアのHPを1にする")]
	[Category("通常バトル")]
	public void Character3HpOne()
	{
		PlayerStatusDataManager.characterHp[3] = 1;
		Debug.Log("シアのHPを1にする");
	}

	[DisplayName("エデンのTPを1にする")]
	[Category("通常バトル")]
	public void CharacterEdenTpOne()
	{
		int weaponID = PlayerEquipDataManager.playerEquipWeaponID[0];
		PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == weaponID && data.equipCharacter == 0).weaponIncludeMp = 1;
		Debug.Log("エデンのTPを1にする");
	}

	[DisplayName("エデンのTPを5にする")]
	[Category("通常バトル")]
	public void CharacterEdenTpFive()
	{
		int weaponID = PlayerEquipDataManager.playerEquipWeaponID[0];
		PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == weaponID && data.equipCharacter == 0).weaponIncludeMp = 5;
		Debug.Log("エデンのTPを5にする");
	}

	[DisplayName("味方SPを99にする")]
	[Category("通常バトル")]
	public void CharacterSpAlmostMax()
	{
		for (int i = 1; i < PlayerStatusDataManager.partyMemberCount; i++)
		{
			PlayerStatusDataManager.characterSp[i] = 99;
		}
		Debug.Log("味方SPを99にする");
	}

	[DisplayName("リィナのSPを99にする")]
	[Category("通常バトル")]
	public void Character2SpAlmostMax()
	{
		PlayerStatusDataManager.characterSp[2] = 99;
		Debug.Log("リィナのSPを99にする");
	}

	[DisplayName("シアのSPを99にする")]
	[Category("通常バトル")]
	public void Character3SpAlmostMax()
	{
		PlayerStatusDataManager.characterSp[3] = 99;
		Debug.Log("シアのSPを99にする");
	}

	[DisplayName("レヴィのSPを99にする")]
	[Category("通常バトル")]
	public void Character4SpAlmostMax()
	{
		PlayerStatusDataManager.characterSp[4] = 99;
		Debug.Log("レヴィのSPを99にする");
	}

	[DisplayName("味方クリティカル率を90にする")]
	[Category("通常バトル")]
	public void CharacterCriticalUp()
	{
		for (int i = 0; i < 5; i++)
		{
			PlayerStatusDataManager.characterCritical[i] = 90;
		}
		Debug.Log("味方クリティカル率を90にする");
	}

	[DisplayName("エデンのクリティカルバフを30にする")]
	[Category("通常バトル")]
	public void CharacterCriticalBuffUp()
	{
		PlayerBattleConditionManager.MemberBuffCondition item = new PlayerBattleConditionManager.MemberBuffCondition
		{
			type = "critical",
			power = 30,
			continutyTurn = 30
		};
		PlayerBattleConditionManager.playerBuffCondition[0].Add(item);
		Debug.Log("エデンのクリティカルバフを30にする");
	}

	[DisplayName("エデンの受け流しを90にする")]
	[Category("通常バトル")]
	public void CharacterParryUp()
	{
		PlayerEquipDataManager.equipFactorParry[0] = 90;
		Debug.Log("エデンの受け流しを90にする");
	}

	[DisplayName("エデンのダメージ吸収を30にする")]
	[Category("通常バトル")]
	public void CharacterVampireUp()
	{
		PlayerEquipDataManager.equipFactorVampire[0] = 30;
		Debug.Log("エデンのダメージ吸収を30にする");
	}

	[DisplayName("エデンの毒ファクターを90にする")]
	[Category("通常バトル")]
	public void CharacterPoisonFactorUp()
	{
		PlayerEquipDataManager.equipFactorPoison[0] = 90;
		Debug.Log("エデンの毒ファクターを90にする");
	}

	[DisplayName("エデンの麻痺ファクターを90にする")]
	[Category("通常バトル")]
	public void CharacterParalyzeFactorUp()
	{
		PlayerEquipDataManager.equipFactorParalyze[0] = 90;
		Debug.Log("エデンの麻痺ファクターを90にする");
	}

	[DisplayName("エデンの崩しファクターを90にする")]
	[Category("通常バトル")]
	public void CharacterStaggerFactorUp()
	{
		PlayerEquipDataManager.equipFactorStagger[0] = 90;
		Debug.Log("エデンの崩しファクターを90にする");
	}

	[DisplayName("エデンの即死ファクターを90にする")]
	[Category("通常バトル")]
	public void CharacterDeathFactorUp()
	{
		PlayerEquipDataManager.equipFactorDeath[0] = 90;
		Debug.Log("エデンの即死ファクターを90にする");
	}

	[DisplayName("エデンをリジェネ状態にする")]
	[Category("通常バトル")]
	public void CharacterRegeneration()
	{
		PlayerBattleConditionManager.MemberBuffCondition item = new PlayerBattleConditionManager.MemberBuffCondition
		{
			type = "regeneration",
			power = 30,
			continutyTurn = 30
		};
		PlayerBattleConditionManager.playerBuffCondition[0].Add(item);
		Debug.Log("エデンをリジェネ状態にする");
	}

	[DisplayName("ドロップ金額アップにする")]
	[Category("通常バトル")]
	public void GetMoneyUp()
	{
		PlayerEquipDataManager.accessoryDropMoneyUp = 150;
		Debug.Log("ドロップ金額アップにする");
	}

	[DisplayName("獲得EXPアップにする")]
	[Category("通常バトル")]
	public void GetExpUp()
	{
		PlayerEquipDataManager.accessoryExpUp = 150;
		Debug.Log("獲得EXPアップにする");
	}

	[DisplayName("アイテムドロップ率アップにする")]
	[Category("通常バトル")]
	public void GetItemDiscoverUp()
	{
		PlayerEquipDataManager.accessoryItemDiscover = 60;
		Debug.Log("アイテムドロップ率アップにする");
	}

	[DisplayName("敵のHPを1にする")]
	[Category("通常バトル")]
	public void EnemyHpOne()
	{
		for (int i = 0; i < PlayerStatusDataManager.enemyMember.Length; i++)
		{
			PlayerStatusDataManager.enemyHp[i] = 1;
		}
		Debug.Log("敵のHPを1にする");
	}

	[DisplayName("真ん中の敵のHPを1にする")]
	[Category("通常バトル")]
	public void EnemyTwoHpOne()
	{
		PlayerStatusDataManager.enemyHp[1] = 1;
		Debug.Log("真ん中の敵のHPを1にする");
	}

	[DisplayName("敵の状態異常発生率を-90にする")]
	[Category("通常バトル")]
	public void EnemyResistDown()
	{
		for (int i = 0; i < PlayerStatusDataManager.enemyMember.Length; i++)
		{
			PlayerStatusDataManager.enemyResist[i] = -90;
		}
		Debug.Log("敵の状態異常発生率を-90にする");
	}

	[DisplayName("次の味方のダメージで敵のHPを0ぴったりにする")]
	[Category("通常バトル")]
	public void EnemyHpZero()
	{
		PlayerNonSaveDataManager.isEnemyHpZeroAttack = true;
		Debug.Log("次の味方のダメージで敵のHPを0ぴったりにする");
	}

	[DisplayName("敵の攻撃力を1にする")]
	[Category("通常バトル")]
	public void EnemyAttackMin()
	{
		for (int i = 0; i < PlayerStatusDataManager.enemyMember.Length; i++)
		{
			PlayerStatusDataManager.enemyAttack[i] = 1;
		}
		Debug.Log("敵の攻撃力を1にする");
	}

	[DisplayName("敵の防御力を9999にする")]
	[Category("通常バトル")]
	public void EnemyDefenseMax()
	{
		for (int i = 0; i < PlayerStatusDataManager.enemyMember.Length; i++)
		{
			PlayerStatusDataManager.enemyDefense[i] = 9999;
		}
		Debug.Log("敵の防御力を9999にする");
	}

	[DisplayName("味方全体に毒を付与する")]
	[Category("通常バトル")]
	public void AllCharacterPoisonAdd()
	{
		for (int i = 0; i < PlayerBattleConditionManager.playerIsDead.Count; i++)
		{
			PlayerBattleConditionManager.MemberBadState item = new PlayerBattleConditionManager.MemberBadState
			{
				type = "poison",
				continutyTurn = 3
			};
			PlayerBattleConditionManager.playerBadState[i].Add(item);
			GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>().SetBadStateIcon("poison", i, targetForce: true, setVisible: true);
		}
		Debug.Log("味方全体に毒を付与する");
	}

	[DisplayName("味方の誰かに毒を付与する")]
	[Category("通常バトル")]
	public void AnyCharacterPoisonAdd()
	{
		int partyNum = UnityEngine.Random.Range(0, PlayerStatusDataManager.playerPartyMember.Length);
		int num = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == partyNum);
		PlayerBattleConditionManager.MemberBadState item = new PlayerBattleConditionManager.MemberBadState
		{
			type = "poison",
			continutyTurn = 3
		};
		PlayerBattleConditionManager.playerBadState[num].Add(item);
		GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>().SetBadStateIcon("poison", partyNum, targetForce: true, setVisible: true);
		Debug.Log($"味方の誰かに毒を付与する／num：{partyNum}／index：{num}");
	}

	[DisplayName("エデンに毒を付与する")]
	[Category("通常バトル")]
	public void CharacterPoisonAdd()
	{
		int index = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == 0);
		int memberNum = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == 0).memberNum;
		PlayerBattleConditionManager.MemberBadState item = new PlayerBattleConditionManager.MemberBadState
		{
			type = "poison",
			continutyTurn = 30
		};
		PlayerBattleConditionManager.playerBadState[index].Add(item);
		GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>().SetBadStateIcon("poison", memberNum, targetForce: true, setVisible: true);
		Debug.Log("エデンに毒を付与する／インデックス：" + index);
	}

	[DisplayName("味方の誰かに麻痺を付与する")]
	[Category("通常バトル")]
	public void AnyCharacterParalyzeAdd()
	{
		int partyNum = UnityEngine.Random.Range(0, PlayerStatusDataManager.playerPartyMember.Length);
		int num = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == partyNum);
		PlayerBattleConditionManager.MemberBadState item = new PlayerBattleConditionManager.MemberBadState
		{
			type = "paralyze",
			continutyTurn = 3
		};
		PlayerBattleConditionManager.playerBadState[num].Add(item);
		GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>().SetBadStateIcon("paralyze", partyNum, targetForce: true, setVisible: true);
		Debug.Log($"味方の誰かに麻痺を付与する／num：{partyNum}／index：{num}");
	}

	[DisplayName("エデンの防御力デバフを30にする")]
	[Category("通常バトル")]
	public void EdenDefenseDeBuff()
	{
		int index = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == 0);
		PlayerBattleConditionManager.MemberBuffCondition item = new PlayerBattleConditionManager.MemberBuffCondition
		{
			type = "defense",
			power = -30,
			continutyTurn = 30
		};
		PlayerBattleConditionManager.playerBuffCondition[index].Add(item);
		Debug.Log("エデンの防御力デバフを30にする");
	}

	[DisplayName("リィナの防御力デバフを30にする")]
	[Category("通常バトル")]
	public void RinaDefenseDeBuff()
	{
		int index = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == 2);
		PlayerBattleConditionManager.MemberBuffCondition item = new PlayerBattleConditionManager.MemberBuffCondition
		{
			type = "defense",
			power = -30,
			continutyTurn = 30
		};
		PlayerBattleConditionManager.playerBuffCondition[index].Add(item);
		Debug.Log("リィナの防御力デバフを30にする");
	}

	[DisplayName("敵0の攻撃力デバフを30にする")]
	[Category("通常バトル")]
	public void Enemy0AttackDeBuff()
	{
		PlayerBattleConditionManager.MemberBuffCondition item = new PlayerBattleConditionManager.MemberBuffCondition
		{
			type = "attack",
			power = -30,
			continutyTurn = 30
		};
		PlayerBattleConditionManager.enemyBuffCondition[0].Add(item);
		Debug.Log("敵0の攻撃力デバフを30にする");
	}

	[DisplayName("敵のスキルを満タンにする")]
	[Category("通常バトル")]
	public void EnemySkillAllCharge()
	{
		for (int i = 0; i < PlayerStatusDataManager.enemyMember.Length; i++)
		{
			PlayerStatusDataManager.enemyChargeTurnList[i] = PlayerStatusDataManager.enemyMaxChargeTurnList[i];
		}
		Debug.Log("敵のスキルを満タンにする");
	}

	[DisplayName("攻撃バフを+10にする")]
	[Category("ダンジョンバトル")]
	public void DungeonAttackBuffAdd()
	{
		GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapStatusManager>().dungeonBuffAttack = 10;
		Debug.Log("攻撃バフを+10にする");
	}

	[DisplayName("防御バフを-10にする")]
	[Category("ダンジョンバトル")]
	public void DungeonDefenseBuffAdd()
	{
		GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapStatusManager>().dungeonBuffDefense = -10;
		Debug.Log("防御バフを-10にする");
	}

	[DisplayName("素早さバフを-1にする")]
	[Category("ダンジョンバトル")]
	public void DungeonAgilityBuffAdd()
	{
		GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapStatusManager>().dungeonDeBuffAgility = -1;
		Debug.Log("素早さバフを-1にする");
	}

	[DisplayName("撤退バフを-30にする")]
	[Category("ダンジョンバトル")]
	public void DungeonRetreatBuffAdd()
	{
		GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapStatusManager>().dungeonBuffRetreat = -30;
		Debug.Log("撤退バフを-30にする");
	}

	[DisplayName("アイテム獲得バフを+100にする")]
	[Category("ダンジョンバトル")]
	public void DungeonItemDropBuffAdd()
	{
		PlayerEquipDataManager.accessoryItemDiscover = 100;
		Debug.Log("アイテム獲得バフを+100にする");
	}

	[DisplayName("味方クリティカル率を50にする")]
	[Category("ダンジョンバトル")]
	public void DungeonCharacterCriticalUp()
	{
		PlayerStatusDataManager.playerAllCritical = 50;
		Debug.Log("味方クリティカル率を50にする");
	}

	[DisplayName("味方全体のHPを1にする")]
	[Category("ダンジョンバトル")]
	public void DungeonPlayerHpOne()
	{
		PlayerStatusDataManager.playerAllHp = 1;
		Debug.Log("味方全体のHPを1にする");
	}

	[DisplayName("敵全体のHPを1にする")]
	[Category("ダンジョンバトル")]
	public void DungeonEnemyHpOne()
	{
		PlayerStatusDataManager.enemyAllHp = 1;
		Debug.Log("敵全体のHPを1にする");
	}

	[DisplayName("味方のチャージを100にする")]
	[Category("ダンジョンバトル")]
	public void DungeonPlayerChargeMax()
	{
		for (int i = 1; i < 5; i++)
		{
			PlayerStatusDataManager.characterSp[i] = 100;
		}
		Debug.Log("味方SPを100にする");
	}

	[DisplayName("味方の連携攻撃率を50にする")]
	[Category("ダンジョンバトル")]
	public void DungeonPlayerComboUp()
	{
		for (int i = 1; i < 5; i++)
		{
			PlayerStatusDataManager.characterComboProbability[i] = 50;
		}
		Debug.Log("味方の連携攻撃率を50にする");
	}

	[DisplayName("ダンジョンで性欲上昇アップにする")]
	[Category("ダンジョンバトル")]
	public void EquipLibidoUp()
	{
		PlayerEquipDataManager.accessoryLibidoUpRate = 50;
		Debug.Log("ダンジョンで性欲上昇アップにする");
	}

	[DisplayName("ダンジョンで性欲上昇ダウンにする")]
	[Category("ダンジョンバトル")]
	public void EquipLibidoDown()
	{
		PlayerEquipDataManager.accessoryLibidoUpRate = -50;
		Debug.Log("ダンジョンで性欲上昇ダウンにする");
	}

	[DisplayName("ダンジョンで性欲上昇なしにする")]
	[Category("ダンジョンバトル")]
	public void EquipLibidoZero()
	{
		PlayerEquipDataManager.accessoryLibidoUpRate = 0;
		Debug.Log("ダンジョンで性欲上昇なしにする");
	}

	[DisplayName("敵のチャージを50にする")]
	[Category("ダンジョンバトル")]
	public void DungeonEnemyChargeAdd()
	{
		GameObject.Find("Dungeon Battle Manager").GetComponent<DungeonBattleManager>().enemyCharge = 50;
		Debug.Log("敵のチャージを50にする");
	}

	[DisplayName("味方に毒を付与する")]
	[Category("ダンジョンバトル")]
	public void DungeonPlayerPoisonAdd()
	{
		PlayerBattleConditionManager.MemberBadState item = new PlayerBattleConditionManager.MemberBadState
		{
			type = "poison",
			continutyTurn = 3
		};
		PlayerBattleConditionManager.playerBadState[0].Add(item);
		Debug.Log("味方に毒を付与する");
	}

	[DisplayName("階層を+3する")]
	[Category("ダンジョンマップ")]
	public void DungeonCurrentFloorAdd()
	{
		int value = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>().dungeonCurrentFloorNum + 3;
		int dungeonMaxFloorNum = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>().dungeonMaxFloorNum;
		GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>().dungeonCurrentFloorNum = Mathf.Clamp(value, 0, dungeonMaxFloorNum - 1);
		Debug.Log("階層を+3する");
	}

	[DisplayName("性欲を+20する")]
	[Category("ダンジョンマップ")]
	public void PlayerLibidoAdd()
	{
		PlayerDataManager.playerLibido += 20;
		Debug.Log("性欲を+20する");
	}

	[DisplayName("性欲を-20する")]
	[Category("ダンジョンマップ")]
	public void PlayerLibidoSubtract()
	{
		PlayerDataManager.playerLibido -= 20;
		Debug.Log("性欲を-20する");
	}

	[DisplayName("敵のHPを1にする")]
	[Category("えっちバトル")]
	public void EnemySexHpOne()
	{
		PlayerSexStatusDataManager.playerSexHp[PlayerNonSaveDataManager.selectSexBattleHeroineId] = 1;
		Debug.Log("ヒロインのHPを1にする");
	}

	[DisplayName("敵の残り絶頂数を1にする")]
	[Category("えっちバトル")]
	public void EnemySexLimitOne()
	{
		PlayerSexStatusDataManager.playerSexExtasyLimit[1] = 1;
		Debug.Log("ヒロインの残り絶頂数を1にする");
	}

	[DisplayName("自分のHPを1にする")]
	[Category("えっちバトル")]
	public void PlayerSexHpOne()
	{
		PlayerSexStatusDataManager.playerSexHp[0] = 1;
		Debug.Log("エデンのHPを1にする");
	}

	[DisplayName("自分の残り射精数を1にする")]
	[Category("えっちバトル")]
	public void PlayerSexLimitOne()
	{
		PlayerSexStatusDataManager.playerSexExtasyLimit[0] = 1;
		Debug.Log("エデンの残り射精数を1にする");
	}

	[DisplayName("エデンのチャージを最大にする")]
	[Category("えっちバトル")]
	public void PlayerSexChargeMax()
	{
		PlayerSexStatusDataManager.playerSexTrance[0] = 100;
		Debug.Log("エデンのチャージを最大にする");
	}

	[DisplayName("えっちLVを19にする")]
	[Category("えっちバトル")]
	public void SexLvPreMax()
	{
		PlayerSexStatusDataManager.playerSexLv = 19;
		PlayerSexStatusDataManager.heroineSexLv[PlayerNonSaveDataManager.selectSexBattleHeroineId - 1] = 19;
		Debug.Log("エデンのえっちLVを19にする");
	}

	[DisplayName("合計日数を11進める")]
	[Category("マップ")]
	public void AddDayCount()
	{
		PlayerNonSaveDataManager.addTimeZoneNum = 44;
		PlayerNonSaveDataManager.isRequiredCalcCarriageStore = false;
		GameObject.Find("AddTime Manager").GetComponent<PlayMakerFSM>().SendEvent("AddTimeZone");
		Debug.Log("合計日数を11進める");
	}

	[DisplayName("時間帯を1進める")]
	[Category("マップ")]
	public void AddTimeZone()
	{
		PlayerNonSaveDataManager.addTimeZoneNum = 1;
		PlayerNonSaveDataManager.isRequiredCalcCarriageStore = false;
		GameObject.Find("AddTime Manager").GetComponent<PlayMakerFSM>().SendEvent("AddTimeZone");
		Debug.Log("時間帯を進める");
	}

	[DisplayName("時間帯をランダムに進める")]
	[Category("マップ")]
	public void AddTimeZoneRandom()
	{
		int num = (PlayerNonSaveDataManager.addTimeZoneNum = UnityEngine.Random.Range(1, 10));
		PlayerNonSaveDataManager.isRequiredCalcCarriageStore = false;
		GameObject.Find("AddTime Manager").GetComponent<PlayMakerFSM>().SendEvent("AddTimeZone");
		Debug.Log("時間帯を進める：" + num);
	}

	[DisplayName("工房ツールLVを1上げる")]
	[Category("工房拡張")]
	public void AddWorkShopToolLv()
	{
		CraftWorkShopData craftWorkShopData = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"];
		craftWorkShopData.workShopToolLv++;
		craftWorkShopData.workShopToolLv = Mathf.Clamp(craftWorkShopData.workShopToolLv, 0, 5);
		Debug.Log("工房ツールLVを1上げる");
	}

	[DisplayName("プロローグを終盤までスキップする")]
	[Category("シナリオ")]
	public void JumpPrologueEnd()
	{
		GameObject.Find("AdvEngine").GetComponent<AdvEngine>().JumpScenario("M_Main_001-3");
	}

	[DisplayName("デバッグ画面を最前面にする")]
	[Category("デバッグ")]
	public void SetSrDebuggerMoveForward()
	{
		GameObject gameObject = GameObject.Find("SR_Canvas");
		gameObject.GetComponent<Canvas>().sortingOrder = 3000;
		Transform transform = gameObject.transform.Find("SR_Content/SR_Main/SR_Tab/Console(Clone)");
		if (transform != null)
		{
			transform.GetComponent<Canvas>().sortingOrder = 3001;
		}
		else
		{
			Debug.Log("コンソールはnull");
		}
		GameObject gameObject2 = GameObject.Find("Options(Clone)");
		if (gameObject2 != null)
		{
			gameObject2.GetComponent<Canvas>().sortingOrder = 3001;
		}
		else
		{
			Debug.Log("オプションはnull");
		}
	}
}
