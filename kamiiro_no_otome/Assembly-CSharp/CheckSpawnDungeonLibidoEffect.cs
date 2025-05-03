using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckSpawnDungeonLibidoEffect : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private DungeonMapEffectManager dungeonMapEffectManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		dungeonMapEffectManager = GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapEffectManager>();
	}

	public override void OnStateBegin()
	{
		string text = "";
		text = ((PlayerDataManager.playerLibido >= 70) ? "Max" : ((PlayerDataManager.playerLibido < 40) ? "Normal" : "High"));
		List<DungeonLibidoData> dungeonLibidoDataList = dungeonMapManager.dungeonLibidoDataBase.dungeonLibidoDataList;
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			CharacterStatusData characterStatusData = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[PlayerDataManager.DungeonHeroineFollowNum];
			if (PlayerFlagDataManager.scenarioFlagDictionary[characterStatusData.characterDungeonSexUnLockFlag])
			{
				switch (text)
				{
				case "High":
				{
					Vector2 libidoMaxEffectV2 = dungeonLibidoDataList.Find((DungeonLibidoData data) => data.characterID == PlayerDataManager.DungeonHeroineFollowNum).libidoMaxEffectV2;
					dungeonMapEffectManager.SpawnDungeonLibidoEffect(libidoMaxEffectV2);
					break;
				}
				case "Max":
				{
					Vector2 libidoMaxEffectV = dungeonLibidoDataList.Find((DungeonLibidoData data) => data.characterID == PlayerDataManager.DungeonHeroineFollowNum).libidoMaxEffectV2;
					dungeonMapEffectManager.SpawnDungeonLibidoEffect(libidoMaxEffectV);
					break;
				}
				}
			}
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
