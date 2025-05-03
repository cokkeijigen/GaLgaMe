using TMPro;
using UnityEngine;

public class SexBattleMessageTextManager : MonoBehaviour
{
	public SexBattleManager sexBattleManager;

	public SexBattleTurnManager sexBattleTurnManager;

	public SexBattleEffectManager sexBattleEffectManager;

	public GameObject sexBattleTextGroupGo;

	public GameObject sexBattleMessageGroupGo_Top;

	public GameObject sexBattleMessageGroupGo_SelfFertilize;

	public GameObject sexBattleMessageGroupGo_Fertilize;

	public GameObject sexBattleMessageGroupGo_Self;

	public GameObject sexBattleMessageGroupGo_Bottom;

	public GameObject sexBattleMessageGroupGo_Victory;

	public GameObject sexBattleMessageGroupGo_Ecstasy;

	public GameObject sexBattleMessageGroupGo_AfterEcstasy;

	public GameObject sexBattleMessageGroupGo_Slip;

	public GameObject[] sexBattleMessageGroupGo_DamageRaw;

	public GameObject[] sexBattleMessageGroupGo_SubPowerRaw;

	public GameObject sexBattleMessageGroupGo_AfterHealRaw;

	public TextMeshProUGUI[] sexBattleMessageGroup_Top;

	public TextMeshProUGUI[] sexBattleMessageGroup_SelfFertilize;

	public TextMeshProUGUI[] sexBattleMessageGroup_Fertilize;

	public TextMeshProUGUI[] sexBattleMessageGroup_Self;

	public TextMeshProUGUI[] sexBattleMessageGroup_Bottom;

	public TextMeshProUGUI[] sexBattleMessageGroup_Victory;

	public TextMeshProUGUI[] sexBattleMessageGroup_Ecstasy;

	public TextMeshProUGUI[] sexBattleMessageGroup_AfterEcstasy;

	public TextMeshProUGUI[] sexBattleMessageGroup_Slip;

	public void ResetBattleTextMessage()
	{
		sexBattleMessageGroupGo_SelfFertilize.SetActive(value: false);
		sexBattleMessageGroupGo_Fertilize.SetActive(value: false);
		sexBattleMessageGroupGo_Self.SetActive(value: false);
		sexBattleMessageGroupGo_Bottom.SetActive(value: false);
		sexBattleMessageGroupGo_Victory.SetActive(value: false);
		sexBattleMessageGroupGo_Ecstasy.SetActive(value: false);
		sexBattleMessageGroupGo_AfterEcstasy.SetActive(value: false);
		sexBattleMessageGroupGo_Slip.SetActive(value: false);
		TextMeshProUGUI[] array = sexBattleMessageGroup_Top;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].gameObject.SetActive(value: false);
		}
		array = sexBattleMessageGroup_SelfFertilize;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].gameObject.SetActive(value: false);
		}
		array = sexBattleMessageGroup_Fertilize;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].gameObject.SetActive(value: false);
		}
		array = sexBattleMessageGroup_Self;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].gameObject.SetActive(value: false);
		}
		array = sexBattleMessageGroup_Victory;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].gameObject.SetActive(value: false);
		}
		array = sexBattleMessageGroup_Ecstasy;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].gameObject.SetActive(value: false);
		}
		array = sexBattleMessageGroup_AfterEcstasy;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].gameObject.SetActive(value: false);
		}
		array = sexBattleMessageGroup_Slip;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].gameObject.SetActive(value: false);
		}
		array = sexBattleMessageGroup_Bottom;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].gameObject.SetActive(value: false);
		}
		GameObject[] array2 = sexBattleMessageGroupGo_DamageRaw;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].SetActive(value: false);
		}
		array2 = sexBattleMessageGroupGo_SubPowerRaw;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].SetActive(value: false);
		}
	}
}
