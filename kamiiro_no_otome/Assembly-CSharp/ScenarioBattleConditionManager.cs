using I2.Loc;
using UnityEngine;

public class ScenarioBattleConditionManager : MonoBehaviour
{
	public Canvas buffInfoWindowCanvas;

	public GameObject buffInfoWindow;

	public Localize buffInfoLocText;

	private void Awake()
	{
		buffInfoWindow.SetActive(value: false);
		buffInfoWindowCanvas.sortingLayerName = "ScenarioBattle";
	}

	public void PointerEnterBuffIcon(GameObject enterGo, string type, bool isBuff)
	{
		Vector3 position = enterGo.GetComponent<RectTransform>().position;
		buffInfoWindow.GetComponent<RectTransform>().position = new Vector2(position.x, position.y - 0.25f);
		if (isBuff)
		{
			buffInfoLocText.Term = "infoBuff_" + type;
		}
		else
		{
			buffInfoLocText.Term = "infoDebuff_" + type;
		}
		buffInfoWindow.SetActive(value: true);
	}

	public void PointerEnterBadStatusIcon(GameObject enterGo, string type)
	{
		Vector3 position = enterGo.GetComponent<RectTransform>().position;
		buffInfoWindow.GetComponent<RectTransform>().position = new Vector2(position.x, position.y - 0.25f);
		buffInfoLocText.Term = "infoBadStatus_" + type;
		buffInfoWindow.SetActive(value: true);
	}

	public void PointerExitFromIcon()
	{
		buffInfoWindow.SetActive(value: false);
	}
}
