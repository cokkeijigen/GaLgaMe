using System.Collections.Generic;
using Arbor;
using DarkTonic.MasterAudio;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class SexTouchStatusManager : SerializedMonoBehaviour
{
	public enum HeroineSexLvStage
	{
		A,
		B
	}

	public SexTouchManager sexTouchManager;

	public ArborFSM touchCumShotFSM;

	public RectTransform heroineLibidoHeartRect;

	public float heroineLibidoPoint;

	public float heroineLibidoAddPoint;

	public Animator heroineLibidoAnimator;

	public bool isAddLibidoAnimation;

	public Dictionary<string, float> heroineClickPointRepeatCountDictionary = new Dictionary<string, float>();

	public GameObject playerLibidoGroupGo;

	public RectTransform playerLibidoHeartRect;

	public int playerLibidoPoint;

	public int playerLibidoAddPoint;

	public Animator playerLibidoAnimator;

	public bool isAddPlayerLibidoAnimation;

	public int beforePlayerLibido;

	public bool isCumShotFace;

	public bool isFellatioClick;

	public float touchVoiceThresholdTime;

	public float touchVoiceCurrentTime;

	public HeroineSexLvStage heroineSexLvStage;

	private void Update()
	{
		if (sexTouchManager.touchCanvas.activeInHierarchy)
		{
			touchVoiceCurrentTime += Time.deltaTime;
		}
	}

	public void PlayTouchVoice()
	{
		if (touchVoiceCurrentTime >= touchVoiceThresholdTime)
		{
			string text = "";
			text = ((heroineLibidoAddPoint < 2f) ? "_Low" : ((!(heroineLibidoAddPoint < 4f)) ? "_High" : "_Middle"));
			string clickSelectAreaPointName = sexTouchManager.clickSelectAreaPointName;
			if (clickSelectAreaPointName == "mouth" || clickSelectAreaPointName == "womb")
			{
				text = "_Low";
			}
			MasterAudio.PlaySound("Voice_Touch" + PlayerNonSaveDataManager.selectSexBattleHeroineId + text, 1f, null, 0f, null, null);
			touchVoiceCurrentTime = 0f;
		}
	}

	public bool GetFellatioIsClick()
	{
		return isFellatioClick;
	}

	public void SetCumShotIsFace(bool value)
	{
		isCumShotFace = value;
		touchCumShotFSM.SendTrigger("StartCumShot");
	}

	public bool CheckPointerOverUi()
	{
		return EventSystem.current.IsPointerOverGameObject();
	}

	public void SetHeroineSexLvStage()
	{
		if (PlayerSexStatusDataManager.heroineSexLv[PlayerNonSaveDataManager.selectSexBattleHeroineId - 1] < 4)
		{
			heroineSexLvStage = HeroineSexLvStage.A;
			Debug.Log("えっちLV：" + PlayerSexStatusDataManager.heroineSexLv[PlayerNonSaveDataManager.selectSexBattleHeroineId - 1] + "えっちLVステージはA");
		}
		else
		{
			heroineSexLvStage = HeroineSexLvStage.A;
			Debug.Log("えっちLV：" + PlayerSexStatusDataManager.heroineSexLv[PlayerNonSaveDataManager.selectSexBattleHeroineId - 1] + "えっちLVステージはBだがA");
		}
	}
}
