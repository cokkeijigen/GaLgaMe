using I2.Loc;
using UnityEngine;

public class DungeonConditionManager : MonoBehaviour
{
	public GameObject mapBuffInfoWindow;

	public Localize mapBuffInfoLocText;

	public GameObject dungeonBuffInfoWindow;

	public Localize dungeonBuffInfoLocText;

	public GameObject battlePlayerBuffInfoWindow;

	public Localize battlePlayerBuffInfoLocText;

	public GameObject battleEnemyBuffInfoWindow;

	public Localize battleEnemyBuffInfoLocText;

	private void Awake()
	{
		mapBuffInfoWindow.SetActive(value: false);
		dungeonBuffInfoWindow.SetActive(value: false);
		battlePlayerBuffInfoWindow.SetActive(value: false);
		battleEnemyBuffInfoWindow.SetActive(value: false);
	}

	public void PointerEnterBuffIcon(GameObject enterGo, string type, bool isPlayer, bool isBuff)
	{
		Vector3 position = enterGo.GetComponent<RectTransform>().position;
		if (isPlayer)
		{
			battlePlayerBuffInfoWindow.GetComponent<RectTransform>().position = new Vector2(position.x, position.y - 0.25f);
			if (isBuff)
			{
				battlePlayerBuffInfoLocText.Term = "infoBuff_" + type;
			}
			else
			{
				battlePlayerBuffInfoLocText.Term = "infoDebuff_" + type;
			}
			battlePlayerBuffInfoWindow.SetActive(value: true);
		}
		else
		{
			battleEnemyBuffInfoWindow.GetComponent<RectTransform>().position = new Vector2(position.x, position.y - 0.25f);
			if (isBuff)
			{
				battleEnemyBuffInfoLocText.Term = "infoBuff_" + type;
			}
			else
			{
				battleEnemyBuffInfoLocText.Term = "infoDebuff_" + type;
			}
			battleEnemyBuffInfoWindow.SetActive(value: true);
		}
	}

	public void PointerEnterBadStatusIcon(GameObject enterGo, string type, bool isPlayer)
	{
		if (isPlayer)
		{
			Vector3 position = enterGo.GetComponent<RectTransform>().position;
			battlePlayerBuffInfoWindow.GetComponent<RectTransform>().position = new Vector2(position.x, position.y - 0.25f);
			battlePlayerBuffInfoLocText.Term = "infoBadStatus_" + type;
			battlePlayerBuffInfoWindow.SetActive(value: true);
		}
		else
		{
			Vector3 position2 = enterGo.GetComponent<RectTransform>().position;
			battleEnemyBuffInfoWindow.GetComponent<RectTransform>().position = new Vector2(position2.x, position2.y - 0.25f);
			battleEnemyBuffInfoLocText.Term = "infoBadStatus_" + type;
			battleEnemyBuffInfoWindow.SetActive(value: true);
		}
	}

	public void PointerExitFromMapIcon()
	{
		mapBuffInfoWindow.SetActive(value: false);
	}

	public void PointerExitFromBattleIcon()
	{
		dungeonBuffInfoWindow.SetActive(value: false);
		battlePlayerBuffInfoWindow.SetActive(value: false);
		battleEnemyBuffInfoWindow.SetActive(value: false);
	}

	public void PointerEnterDungeonMapBuffIcon(GameObject enterGo, string type)
	{
		Vector3 position = enterGo.GetComponent<RectTransform>().position;
		mapBuffInfoWindow.GetComponent<RectTransform>().position = new Vector2(position.x, position.y - 0.4f);
		mapBuffInfoLocText.Term = "infoDungeonBuff_" + type;
		mapBuffInfoWindow.SetActive(value: true);
	}

	public void PointerEnterDungeonBattleBuffIcon(GameObject enterGo, string type)
	{
		Vector3 position = enterGo.GetComponent<RectTransform>().position;
		dungeonBuffInfoWindow.GetComponent<RectTransform>().position = new Vector2(position.x, position.y - 0.4f);
		dungeonBuffInfoLocText.Term = "infoDungeonBuff_" + type;
		dungeonBuffInfoWindow.SetActive(value: true);
	}
}
