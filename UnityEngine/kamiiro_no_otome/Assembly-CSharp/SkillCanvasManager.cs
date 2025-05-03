using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCanvasManager : MonoBehaviour
{
	public GameObject haveSkillScrollContentGO;

	public TextMeshProUGUI[] haveSkillGroupText;

	public GameObject equipSkillScrollContentGO;

	public TextMeshProUGUI[] equipSkillGroupText;

	public Text[] itemRepairMaterialText;

	public TextMeshProUGUI[] itemMpText;

	public GameObject itemRepairApplyButton;

	public Localize[] skillTextLocGroup;

	public Image skillImage;

	public GameObject[] skillTypeTextArray;

	public GameObject[] skillPowerTextArray;

	public Localize[] itemTextLocGroup;

	public Image itemImage;

	public Toggle viewChangeToggle;

	public GameObject[] itemTypeTextArray;

	public GameObject[] itemPowerTextArray;
}
