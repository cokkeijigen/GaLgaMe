using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class StartInDoorRest : StateBehaviour
{
	private HeaderStatusManager headerStatusManager;

	private InDoorTalkManager inDoorTalkManager;

	private InDoorCommandManager inDoorCommandManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
		inDoorCommandManager = GameObject.Find("InDoor Command Manager").GetComponent<InDoorCommandManager>();
	}

	public override void OnStateBegin()
	{
		inDoorCommandManager.restDialogGroupGo.SetActive(value: false);
		MasterAudio.FadeAllPlaylistsToVolume(0f, 0.3f);
		MasterAudio.PlaySound("SeRest", 1f, null, 0f, null, null);
		PlayerNonSaveDataManager.oldTimeZone = PlayerDataManager.totalTimeZoneCount;
		PlayerNonSaveDataManager.oldDayCount = PlayerDataManager.currentTotalDay;
		PlayerNonSaveDataManager.addTimeZoneNum = inDoorTalkManager.restTimeZoneNum;
		PlayerNonSaveDataManager.isRequiredCalcCarriageStore = true;
		GameObject.Find("AddTime Manager").GetComponent<PlayMakerFSM>().SendEvent("AddTimeZone");
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		ParameterContainer component = headerStatusManager.partyGroupGoList[0].GetComponent<ParameterContainer>();
		int index = PlayerStatusDataManager.characterLv[0] - 1;
		PlayerStatusDataManager.characterHp[0] = PlayerStatusDataManager.characterMaxHp[0];
		SliderAndTmpText variable = component.GetVariable<SliderAndTmpText>("hpGroup");
		int num = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[0].characterHP[index];
		variable.slider.maxValue = num;
		variable.slider.value = num;
		variable.textMeshProUGUI.text = num.ToString();
		PlayerStatusDataManager.characterHp[0] = PlayerStatusDataManager.characterMaxHp[0];
		SliderAndTmpText variable2 = component.GetVariable<SliderAndTmpText>("mpGroup");
		int num2 = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[0].characterMP[index];
		variable2.slider.maxValue = num2;
		variable2.slider.value = num2;
		variable2.textMeshProUGUI.text = num2.ToString();
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			int dungeonHeroineFollowNum = PlayerDataManager.DungeonHeroineFollowNum;
			ParameterContainer component2 = headerStatusManager.partyGroupGoList[1].GetComponent<ParameterContainer>();
			index = PlayerStatusDataManager.characterLv[dungeonHeroineFollowNum] - 1;
			PlayerStatusDataManager.characterHp[dungeonHeroineFollowNum] = PlayerStatusDataManager.characterMaxHp[dungeonHeroineFollowNum];
			SliderAndTmpText variable3 = component2.GetVariable<SliderAndTmpText>("hpGroup");
			num = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[dungeonHeroineFollowNum].characterHP[index];
			variable3.slider.maxValue = num;
			variable3.slider.value = num;
			variable3.textMeshProUGUI.text = num.ToString();
			PlayerStatusDataManager.characterHp[dungeonHeroineFollowNum] = PlayerStatusDataManager.characterMaxHp[dungeonHeroineFollowNum];
			SliderAndTmpText variable4 = component2.GetVariable<SliderAndTmpText>("mpGroup");
			num2 = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[dungeonHeroineFollowNum].characterMP[index];
			variable4.slider.maxValue = num2;
			variable4.slider.value = num2;
			variable4.textMeshProUGUI.text = num2.ToString();
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
