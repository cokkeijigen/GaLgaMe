using I2.Loc;
using UnityEngine;

public class CraftInfoManager : MonoBehaviour
{
	private CraftCanvasManager craftCanvasManager;

	public GameObject infoWindowGo;

	public Localize infoTextLoc;

	private void Awake()
	{
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
	}

	public void OpenCraftLvInfoWindow(string typeName, GameObject enterGo)
	{
		infoTextLoc.Term = "infoCraft_" + typeName;
		Vector3 centerPosition = GetCenterPosition(enterGo.GetComponent<RectTransform>());
		infoWindowGo.GetComponent<RectTransform>().position = new Vector2(centerPosition.x, centerPosition.y + 50f);
		infoWindowGo.SetActive(value: true);
	}

	public void OpenCraftAlertInfoWindow(string typeName, GameObject enterGo)
	{
		if (!craftCanvasManager.isWorkShopLvEnough)
		{
			infoTextLoc.Term = "infoCraftAlert_" + typeName;
			Vector3 centerPosition = GetCenterPosition(enterGo.GetComponent<RectTransform>());
			infoWindowGo.GetComponent<RectTransform>().position = new Vector2(centerPosition.x, centerPosition.y - 60f);
			infoWindowGo.SetActive(value: true);
		}
	}

	private Vector3 GetCenterPosition(RectTransform targetRect)
	{
		Vector3 position = targetRect.position;
		Vector3 vector = new Vector3(Mathf.Lerp((0f - targetRect.rect.size.x) / 2f, targetRect.rect.size.x / 2f, targetRect.pivot.x) * targetRect.transform.lossyScale.x, Mathf.Lerp((0f - targetRect.rect.size.y) / 2f, targetRect.rect.size.y / 2f, targetRect.pivot.y) * targetRect.transform.lossyScale.y);
		return position - vector;
	}
}
