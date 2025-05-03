using System.Collections.Generic;
using System.Linq;
using Arbor;
using HutongGames.PlayMaker;
using UnityEngine;

[AddComponentMenu("")]
public class CheckLocalMapInDoorEvent : StateBehaviour
{
	private PlayMakerFSM playMakerFSM;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		PlayMakerFSM[] components = GetComponents<PlayMakerFSM>();
		foreach (PlayMakerFSM playMakerFSM in components)
		{
			if (playMakerFSM.FsmName == "状態遷移")
			{
				this.playMakerFSM = playMakerFSM;
				break;
			}
		}
	}

	public override void OnStateBegin()
	{
		InDoorCharacterLocationData inDoorCharacterLocationData = null;
		new List<InDoorCharacterLocationData>();
		List<InDoorLocationData> list = new List<InDoorLocationData>();
		List<string> list2 = new List<string>();
		string placeName = base.transform.parent.gameObject.name;
		List<InDoorCharacterLocationData> list3 = (from data in GameDataManager.instance.inDoorLocationDataBase.inDoorCharacterLocationDataList.Where((InDoorCharacterLocationData data) => data.accessPointName == PlayerDataManager.currentAccessPointName && data.placeName == placeName).ToList()
			orderby data.sortID descending
			select data).ToList();
		for (int i = 0; i < list3.Count; i++)
		{
			Debug.Log("場所名：" + placeName + "／リスト数：" + list3.Count + "／現在のカウント：" + i + "／確認するクリアフラグ：" + list3[i].sectionFlagName);
			if (PlayerFlagDataManager.scenarioFlagDictionary[list3[i].sectionFlagName])
			{
				inDoorCharacterLocationData = list3[i];
				break;
			}
		}
		switch (PlayerDataManager.currentTimeZone)
		{
		case 0:
			list = inDoorCharacterLocationData.talkCharacterList0;
			break;
		case 1:
			list = inDoorCharacterLocationData.talkCharacterList1;
			break;
		case 2:
			list = inDoorCharacterLocationData.talkCharacterList2;
			break;
		case 3:
			list = inDoorCharacterLocationData.talkCharacterList3;
			break;
		}
		for (int j = 0; j < list.Count; j++)
		{
			list2.Add(list[j].talkCharacterName);
		}
		FsmArray fsmArray = playMakerFSM.FsmVariables.GetFsmArray("checkNameArray");
		FsmArray fsmArray2 = playMakerFSM.FsmVariables.GetFsmArray("checkBoolArray");
		for (int k = 0; k < 3; k++)
		{
			fsmArray.Values[k] = "";
			fsmArray2.Values[k] = false;
		}
		for (int l = 0; l < list2.Count; l++)
		{
			fsmArray.Values[l] = list2[l];
			Debug.Log("場所名：" + placeName + "／PM配列バリュー：" + fsmArray.Values[l]?.ToString() + "／カウント：" + l + " / " + list2.Count);
		}
		playMakerFSM.SendEvent("CheckInDoorEvent");
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
