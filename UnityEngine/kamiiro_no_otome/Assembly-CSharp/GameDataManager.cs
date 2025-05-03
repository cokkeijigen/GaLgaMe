using UnityEngine;

public class GameDataManager : MonoBehaviour
{
	public static GameDataManager instance;

	public CharacterStatusDataBase characterStatusDataBase;

	public NeedExpDataBase needExpDataBase;

	public BattleEnemyDataBase battleEnemyDataBase;

	public ScenarioBattleDataBase scenarioBattleDataBase;

	public DungeonMapDataBase dungeonMapDataBase;

	public DungeonMapCardDataBase dungeonMapCardDataBase;

	public DungeonItemInfoDataBase dungeonItemInfoDataBase;

	public HeroineLocationDataBase heroineLocationDataBase;

	public InDoorLocationDataBase inDoorLocationDataBase;

	public QuestDataBase questDataBase;

	public QuestSexScenarioDataBase questSexScenarioDataBase;

	public QuestDungeonClearDataBase questDungeonClearDataBase;

	public QuestItemDataBase questItemDataBase;

	public ShopRankDataBase shopRankDataBase;

	public BattleSkillDataBase playerSkillDataBase;

	public BattleSkillDataBase enemySkillDataBase;

	public SkillEffectDataBase skillEffectDataBase;

	public ItemWeaponDataBase itemWeaponDataBase;

	public ItemArmorDataBase itemArmorDataBase;

	public ItemPartyWeaponDataBase itemPartyWeaponDataBase;

	public ItemPartyArmorDataBase itemPartyArmorDataBase;

	public ItemAccessoryDataBase itemAccessoryDataBase;

	public ItemMaterialDataBase itemMaterialDataBase;

	public ItemCanMakeMaterialDataBase itemCanMakeMaterialDataBase;

	public ItemCampItemDataBase itemCampItemDataBase;

	public ItemMagicMaterialDataBase itemMagicMaterialDataBase;

	public ItemCashableItemDataBase itemCashableItemDataBase;

	public ItemDataBase itemDataBase;

	public ItemEventItemDataBase itemEventItemDataBase;

	public ItemCategoryDataBase itemCategoryDataBase;

	public FactorDataBase factorDataBaseWeapon;

	public FactorDataBase factorDataBaseArmor;

	public SexTouchDataBase sexTouchDataBase;

	public SexTouchClickDataBase sexTouchClickDataBase;

	public SexSkillDataBase sexSkillDataBase;

	public HeroineSexPassiveDataBase heroineSexPassiveDataBase;

	public SceneGarellyDataBase sceneGarellyDataBase;

	public HelpDataBase helpDataBase;

	public Sprite[] playerResultFrameSprite;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Object.Destroy(base.gameObject);
		}
	}
}
