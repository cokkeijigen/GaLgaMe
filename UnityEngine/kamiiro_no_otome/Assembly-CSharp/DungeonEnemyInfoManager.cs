using Arbor;
using I2.Loc;
using UnityEngine;

public class DungeonEnemyInfoManager : MonoBehaviour
{
	public GameObject enemyInfoWindow;

	public Localize enemyInfoLocText;

	private void Awake()
	{
		enemyInfoWindow.SetActive(value: false);
	}

	public void PointerEnterDungeonEnemyIcon(GameObject enterGo)
	{
		int @int = enterGo.transform.parent.GetComponent<ParameterContainer>().GetInt("enemyID");
		ParameterContainer component = enterGo.GetComponent<ParameterContainer>();
		component.GetVariable<I2LocalizeComponent>("enemyInfoTextLoc").localize.Term = "enemy" + @int;
		component.GetGameObject("enemyInfoWindowGo").SetActive(value: true);
	}

	public void PointerExitFromEnemyIcon()
	{
		enemyInfoWindow.SetActive(value: false);
	}
}
