using Arbor;
using DarkTonic.MasterAudio;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class CheckPriceSettingNotice : StateBehaviour
{
	private CarriageTalkManager carriageTalkManager;

	public float invokeTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		carriageTalkManager = GameObject.Find("Carriage Talk Manager").GetComponent<CarriageTalkManager>();
	}

	public override void OnStateBegin()
	{
		if (PlayerDataManager.currentShopRankSecondNum > 1)
		{
			string text = "shopRank_second" + PlayerDataManager.currentShopRankSecondNum;
			Debug.Log("チェックする販売倍率通知の名前：" + text);
			if (!PlayerFlagDataManager.priceSettingNoticeFlagDictionary[text])
			{
				Invoke("OpenPriceSettingNoticeDialog", invokeTime);
			}
			else
			{
				Transition(stateLink);
			}
		}
		else
		{
			Transition(stateLink);
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

	private void OpenPriceSettingNoticeDialog()
	{
		if (PlayerDataManager.currentShopRankSecondNum == 2)
		{
			carriageTalkManager.priceSettingNoticeTextLoc.Term = "noticePriceSetting_bottom1";
		}
		else
		{
			carriageTalkManager.priceSettingNoticeTextLoc.Term = "noticePriceSetting_bottom2";
		}
		carriageTalkManager.priceSettingNoticeDialogGo.SetActive(value: true);
		carriageTalkManager.effectSpawnGo = null;
		carriageTalkManager.effectSpawnGo = PoolManager.Pools["Carriage Item Pool"].Spawn(carriageTalkManager.priceSettingNoticePrefabGo, carriageTalkManager.uIParticle.transform);
		MasterAudio.PlaySound("SePriceSettingNotice", 1f, null, 0f, null, null);
		carriageTalkManager.effectSpawnGo.localScale = new Vector3(1f, 1f, 1f);
		carriageTalkManager.effectSpawnGo.localPosition = new Vector3(0f, 0f, 0f);
		Debug.Log("販売倍率エフェクトをスポーン");
		carriageTalkManager.uIParticle.RefreshParticles();
	}
}
