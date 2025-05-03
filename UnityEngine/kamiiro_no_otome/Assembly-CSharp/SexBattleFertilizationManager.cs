using Arbor;
using Coffee.UIExtensions;
using DarkTonic.MasterAudio;
using DG.Tweening;
using I2.Loc;
using PathologicalGames;
using TMPro;
using UnityEngine;

public class SexBattleFertilizationManager : MonoBehaviour
{
	private SexBattleTurnManager sexBattleTurnManager;

	private SexBattleMessageTextManager sexBattleMessageTextManager;

	public bool isInSideCumShot;

	public CanvasGroup[] cumShotCanvasGroupArray;

	public RectTransform cumShotToggleRectTransform;

	public Localize inSideCumShotTextLoc;

	public int fertilizationProbability;

	public GameObject fertilizationProbabilityGroup;

	public TextMeshProUGUI fertilizationProbabilityText;

	public GameObject fertilizationAnimationGroup;

	public Animator fertilizationAnimator;

	public GameObject fertilizationResultGroup;

	public Localize fertilizationResultNameLoc;

	public Localize fertilizationResultSummaryLoc;

	public GameObject[] fertilizationEffectArray;

	public UIParticle[] fertilizationEffectParentArray;

	public bool isFertilizationSuccess;

	private void Awake()
	{
		sexBattleTurnManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleTurnManager>();
		sexBattleMessageTextManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleMessageTextManager>();
	}

	public void InitializeFertilization()
	{
		fertilizationResultGroup.SetActive(value: false);
		fertilizationAnimationGroup.SetActive(value: false);
		isInSideCumShot = true;
		cumShotToggleRectTransform.localScale = new Vector3(1f, 1f, 1f);
		CanvasGroup[] array = cumShotCanvasGroupArray;
		foreach (CanvasGroup obj in array)
		{
			obj.interactable = true;
			obj.alpha = 1f;
		}
		if (PlayerNonSaveDataManager.isSexHeroineMenstrualDay)
		{
			fertilizationProbabilityGroup.SetActive(value: true);
			fertilizationProbabilityText.text = "0";
			if (PlayerNonSaveDataManager.isMenstrualDaySexUseCondom)
			{
				inSideCumShotTextLoc.Term = "buttonCumShotCondom";
				return;
			}
			inSideCumShotTextLoc.Term = "buttonCumShotInSIde";
			if (!PlayerNonSaveDataManager.isSexHeroineEnableFertilization)
			{
				isInSideCumShot = false;
				cumShotToggleRectTransform.localScale = new Vector3(-1f, 1f, 1f);
				array = cumShotCanvasGroupArray;
				foreach (CanvasGroup obj2 in array)
				{
					obj2.interactable = false;
					obj2.alpha = 0.5f;
				}
			}
		}
		else
		{
			fertilizationProbabilityGroup.SetActive(value: false);
			inSideCumShotTextLoc.Term = "buttonCumShotInSIde";
		}
	}

	public void ChangeInsideCumShotToggle(int num)
	{
		switch (num)
		{
		case 0:
			isInSideCumShot = true;
			cumShotToggleRectTransform.localScale = new Vector3(1f, 1f, 1f);
			break;
		case 1:
			if (isInSideCumShot)
			{
				isInSideCumShot = false;
				cumShotToggleRectTransform.localScale = new Vector3(-1f, 1f, 1f);
			}
			else
			{
				isInSideCumShot = true;
				cumShotToggleRectTransform.localScale = new Vector3(1f, 1f, 1f);
			}
			break;
		case 2:
			isInSideCumShot = false;
			cumShotToggleRectTransform.localScale = new Vector3(-1f, 1f, 1f);
			break;
		}
	}

	public void CalcHeroineEcstasyProcess()
	{
		if (isInSideCumShot && PlayerNonSaveDataManager.isSexHeroineMenstrualDay && !PlayerNonSaveDataManager.isMenstrualDaySexUseCondom && PlayerNonSaveDataManager.isSexHeroineEnableFertilization && sexBattleTurnManager.sexBattlePlayerInShotCount > 0)
		{
			int num = Random.Range(10, 20);
			int fromValue = fertilizationProbability;
			fertilizationProbability = Mathf.Clamp(fertilizationProbability + num, 0, 99);
			fertilizationProbabilityText.DOCounter(fromValue, fertilizationProbability, 1f);
		}
	}

	public void CalcCumShotProcess()
	{
		if (isInSideCumShot)
		{
			if (PlayerNonSaveDataManager.isSexHeroineMenstrualDay)
			{
				if (PlayerNonSaveDataManager.isMenstrualDaySexUseCondom)
				{
					sexBattleTurnManager.sexBattlePlayerCondomShotCount++;
					return;
				}
				sexBattleTurnManager.sexBattlePlayerInShotCount++;
				if (PlayerNonSaveDataManager.isSexHeroineEnableFertilization)
				{
					int num = Random.Range(25, 30);
					int fromValue = fertilizationProbability;
					fertilizationProbability = Mathf.Clamp(fertilizationProbability + num, 0, 99);
					fertilizationProbabilityText.DOCounter(fromValue, fertilizationProbability, 1f);
					fertilizationAnimationGroup.SetActive(value: true);
					switch (sexBattleTurnManager.sexBattlePlayerInShotCount)
					{
					case 1:
						fertilizationAnimator.SetTrigger("CumShot0to1");
						break;
					case 2:
						fertilizationAnimator.SetTrigger("CumShot1to2");
						break;
					case 3:
						fertilizationAnimator.SetTrigger("CumShot2to3");
						break;
					}
				}
			}
			else
			{
				sexBattleTurnManager.sexBattlePlayerInShotCount++;
			}
		}
		else
		{
			sexBattleTurnManager.sexBattlePlayerOutShotCount++;
		}
	}

	public void CalcVictoryCumShotProcess()
	{
		int num = 1;
		if (PlayerNonSaveDataManager.isSexHeroineMenstrualDay)
		{
			if (!isInSideCumShot)
			{
				sexBattleTurnManager.sexBattlePlayerOutShotCount++;
			}
			else if (PlayerNonSaveDataManager.isMenstrualDaySexUseCondom)
			{
				sexBattleTurnManager.sexBattlePlayerCondomShotCount++;
			}
			else
			{
				sexBattleTurnManager.sexBattlePlayerInShotCount++;
				if (PlayerNonSaveDataManager.isSexHeroineEnableFertilization)
				{
					isFertilizationSuccess = false;
					fertilizationAnimationGroup.SetActive(value: true);
					if (sexBattleTurnManager.sexBattleRemainCumShotCount >= 2)
					{
						switch (sexBattleTurnManager.sexBattlePlayerInShotCount)
						{
						case 1:
							if (sexBattleTurnManager.sexBattleRemainCumShotCount >= 3)
							{
								num = 3;
								isFertilizationSuccess = true;
								fertilizationAnimator.SetTrigger("CumShot0to3");
								Debug.Log("CumShot0to3");
							}
							else
							{
								num = 2;
								fertilizationAnimator.SetTrigger("CumShot0to2");
								Debug.Log("CumShot0to2");
							}
							break;
						case 2:
							num = 2;
							isFertilizationSuccess = true;
							fertilizationAnimator.SetTrigger("CumShot1to3");
							Debug.Log("CumShot1to3");
							break;
						case 3:
							isFertilizationSuccess = true;
							fertilizationAnimator.SetTrigger("CumShot2to3");
							Debug.Log("CumShot2to3");
							break;
						}
					}
					else
					{
						switch (sexBattleTurnManager.sexBattlePlayerInShotCount)
						{
						case 1:
							fertilizationAnimator.SetTrigger("CumShot0to1");
							Debug.Log("CumShot0to1");
							break;
						case 2:
							fertilizationAnimator.SetTrigger("CumShot1to2");
							Debug.Log("CumShot1to2");
							break;
						case 3:
							isFertilizationSuccess = true;
							fertilizationAnimator.SetTrigger("CumShot2to3");
							Debug.Log("CumShot2to3");
							break;
						}
					}
					int num2 = Random.Range(40, 60) * num;
					int fromValue = fertilizationProbability;
					if (isFertilizationSuccess)
					{
						fertilizationProbability = Mathf.Clamp(fertilizationProbability + num2, 100, 150);
					}
					else
					{
						fertilizationProbability = Mathf.Clamp(fertilizationProbability + num2, 0, 99);
					}
					fertilizationProbabilityText.DOCounter(fromValue, fertilizationProbability, 1f);
					Debug.Log("妊娠したか：" + isFertilizationSuccess + "／妊娠確率：" + fertilizationProbability + "／マルチボーナス：" + num);
				}
			}
		}
		else if (isInSideCumShot)
		{
			sexBattleTurnManager.sexBattlePlayerOutShotCount++;
		}
		else
		{
			sexBattleTurnManager.sexBattlePlayerInShotCount++;
		}
		PlayerSexStatusDataManager.playerSexExtasyLimit[0] = 0;
	}

	public void CloseFertilizationAnimation()
	{
		fertilizationAnimationGroup.SetActive(value: false);
	}

	public void SpawnFertilizationEffect()
	{
		Transform obj = PoolManager.Pools["sexSkillPool"].Spawn(fertilizationEffectArray[0], fertilizationEffectParentArray[0].transform);
		obj.localScale = new Vector3(1f, 1f, 1f);
		obj.localPosition = new Vector3(0f, 0f, 0f);
		Debug.Log("受精エフェクトをスポーン");
		fertilizationEffectParentArray[0].RefreshParticles();
		MasterAudio.PlaySound("SeFertilize_Start3", 1f, null, 0f, null, null);
	}

	public void EndFertilizationAnimation()
	{
		fertilizationResultNameLoc.Term = "sexFertilization_resultCharacter" + PlayerNonSaveDataManager.selectSexBattleHeroineId;
		fertilizationResultSummaryLoc.Term = "sexFertilization_resultSummary" + PlayerNonSaveDataManager.selectSexBattleHeroineId;
		fertilizationResultGroup.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleTextGroupGo.SetActive(value: false);
		Transform obj = PoolManager.Pools["sexSkillPool"].Spawn(fertilizationEffectArray[1], fertilizationEffectParentArray[1].transform);
		obj.localScale = new Vector3(1f, 1f, 1f);
		obj.localPosition = new Vector3(0f, 0f, 0f);
		Debug.Log("リザルトエフェクトをスポーン");
		fertilizationEffectParentArray[1].RefreshParticles();
		MasterAudio.PlaySound("SeFanfare", 1f, null, 0f, null, null);
	}

	public void CloseFertilizationResult()
	{
		fertilizationResultGroup.SetActive(value: false);
		GameObject.Find("SexBattle Result Manager").GetComponent<ArborFSM>().SendTrigger("OpenSexBattleResult");
	}
}
