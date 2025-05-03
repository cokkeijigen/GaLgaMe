using DarkTonic.MasterAudio;
using TMPro;
using UnityEngine;

public class CalcResultCharacterExpForPM : MonoBehaviour
{
	public PlayMakerFSM playMakerFSM;

	public TextMeshProUGUI lvText;

	public Vector2 GetNewLvExpSliderMinMax(int characterID, int upCount, int oldLv)
	{
		int num = 0;
		int num2 = 0;
		int num3 = oldLv + upCount;
		int num4 = Mathf.Clamp(num3, 0, 50);
		if (num3 <= 50)
		{
			num = GameDataManager.instance.needExpDataBase.needCharacterLvExpList[num4 - 1];
			num2 = GameDataManager.instance.needExpDataBase.needCharacterLvExpList[num4];
		}
		else
		{
			num = GameDataManager.instance.needExpDataBase.needCharacterLvExpList[50];
			num2 = 999999;
		}
		return new Vector2(num, num2);
	}

	public void SetNewCharacterLv(int characterID, int upCount, int oldLv)
	{
		int num = oldLv + upCount;
		int num2 = Mathf.Clamp(num, 0, 50);
		PlayerStatusDataManager.characterLv[characterID] = num2;
		lvText.text = num2.ToString();
		if (num <= 50)
		{
			Debug.Log("LVアップしたキャラID：" + characterID + "／新たなLV：" + num2 + "／アップ回数：" + upCount);
			PlayerStatusDataManager.LvUpPlayerStatus(characterID, CallBackMethod);
		}
		else
		{
			Debug.Log("LV上限のキャラID：" + characterID);
		}
	}

	private void CallBackMethod()
	{
		MasterAudio.PlaySound("SeLevelUp", 1f, null, 0f, null, null);
		Debug.Log("LVアップ完了");
		playMakerFSM.SendEvent("AfterSetNewCharacterLv");
	}

	public int GetChatacterLv(int characterID)
	{
		return PlayerStatusDataManager.characterLv[characterID];
	}
}
