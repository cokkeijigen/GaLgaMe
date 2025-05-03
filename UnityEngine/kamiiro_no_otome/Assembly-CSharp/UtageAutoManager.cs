using UnityEngine;
using UnityEngine.UI;
using Utage;

public class UtageAutoManager : MonoBehaviour
{
	public AdvConfig advConfig;

	public Sprite[] autoButtonSpriteArray;

	public bool isUtageAutoPlayWait;

	public GameObject pushButtonGo;

	public void PushUtageAutoButton(GameObject buttonGo)
	{
		buttonGo.GetComponent<Button>().interactable = false;
		Debug.Log("宴のオート状態：" + advConfig.IsAutoBrPage);
		if (advConfig.IsAutoBrPage || isUtageAutoPlayWait)
		{
			advConfig.IsAutoBrPage = false;
			isUtageAutoPlayWait = false;
			CancelInvoke("StartUtageAuto");
			buttonGo.GetComponent<Image>().sprite = autoButtonSpriteArray[0];
			buttonGo.GetComponent<Image>().SetNativeSize();
			Debug.Log("オート無効：" + advConfig.IsAutoBrPage);
		}
		else
		{
			isUtageAutoPlayWait = true;
			buttonGo.GetComponent<Image>().sprite = autoButtonSpriteArray[1];
			buttonGo.GetComponent<Image>().SetNativeSize();
			float time = 5f * (1f - PlayerOptionsDataManager.optionsAutoTextSpeed * 2f);
			Debug.Log("オート有効／待機時間：" + time + "／オプション速度" + PlayerOptionsDataManager.optionsAutoTextSpeed * 2f);
			Invoke("StartUtageAuto", time);
		}
		buttonGo.GetComponent<Button>().interactable = true;
	}

	private void StartUtageAuto()
	{
		advConfig.IsAutoBrPage = true;
		isUtageAutoPlayWait = false;
		Debug.Log("Invoke／オート有効設定完了");
	}
}
