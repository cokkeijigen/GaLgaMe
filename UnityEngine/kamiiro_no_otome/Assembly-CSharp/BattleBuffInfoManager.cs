using I2.Loc;
using Sirenix.OdinInspector;
using UnityEngine;

public class BattleBuffInfoManager : SerializedMonoBehaviour
{
	public GameObject battleInfoCanvasGo;

	public GameObject buffInfoWindow;

	public Localize buffInfoLocText;

	public GameObject enemyInfoWindow;

	public Localize enemyInfoLocText;

	private void Awake()
	{
		battleInfoCanvasGo.SetActive(value: true);
		buffInfoWindow.SetActive(value: false);
		enemyInfoWindow.SetActive(value: false);
	}

	public void CloseBuffInfoWindow()
	{
		buffInfoWindow.SetActive(value: false);
		enemyInfoWindow.SetActive(value: false);
	}

	public void OpenBuffInfoWIndow(GameObject enterGo, string type, bool isDungeon)
	{
		Camera camera = null;
		camera = ((!isDungeon) ? GameObject.Find("Battle Camera").GetComponent<Camera>() : GameObject.Find("Dungeon Camera").GetComponent<Camera>());
		Vector2 vector = RectTransformUtility.WorldToScreenPoint(camera, enterGo.GetComponent<RectTransform>().position);
		buffInfoWindow.GetComponent<RectTransform>().position = new Vector2(vector.x, vector.y - 20f);
		buffInfoLocText.Term = "infoBuff_" + type;
		buffInfoWindow.SetActive(value: true);
	}

	public void OpenSubTypeInfoWIndow(GameObject enterGo, string type, bool isDungeon)
	{
		Camera camera = null;
		camera = ((!isDungeon) ? GameObject.Find("Battle Camera").GetComponent<Camera>() : GameObject.Find("Dungeon Camera").GetComponent<Camera>());
		Vector2 vector = RectTransformUtility.WorldToScreenPoint(camera, enterGo.GetComponent<RectTransform>().position);
		buffInfoWindow.GetComponent<RectTransform>().position = new Vector2(vector.x, vector.y - 20f);
		buffInfoLocText.Term = "infoSubType_" + type;
		buffInfoWindow.SetActive(value: true);
	}

	public void OpenDungeonBuffInfoWIndow(GameObject enterGo, string type)
	{
		Vector2 vector = RectTransformUtility.WorldToScreenPoint(GameObject.Find("Dungeon Camera").GetComponent<Camera>(), enterGo.GetComponent<RectTransform>().position);
		buffInfoWindow.GetComponent<RectTransform>().position = new Vector2(vector.x, vector.y - 30f);
		buffInfoLocText.Term = "infoDungeonBuff_" + type;
		buffInfoWindow.SetActive(value: true);
	}
}
