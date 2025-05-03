using System.Linq;
using Arbor;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioBattleUiManager : MonoBehaviour
{
	public ArborFSM uiFSM;

	public UtageBattleSceneManager utageBattleSceneManager;

	public ScenarioBattleTurnManager scenarioBattleTurnManager;

	public ScenarioBattleSkillManager scenarioBattleSkillManager;

	public void SetMaterialEffect(string type)
	{
		switch (type)
		{
		case "none":
			SetBattleUiMaterial(isBlack: false);
			SetPlayerMaterial(isBlack: false);
			SetEnemyMaterial(isBlack: false);
			foreach (GameObject enemyImageGo in utageBattleSceneManager.enemyImageGoList)
			{
				enemyImageGo.GetComponent<ParameterContainer>().GetGameObject("selectNameGroup").SetActive(value: false);
			}
			utageBattleSceneManager.commandButtonGroup[0].GetComponent<CanvasGroup>().interactable = true;
			utageBattleSceneManager.commandButtonGroup[0].GetComponent<CanvasGroup>().blocksRaycasts = true;
			utageBattleSceneManager.commandButtonGroup[1].GetComponent<CanvasGroup>().interactable = true;
			utageBattleSceneManager.commandButtonGroup[1].GetComponent<CanvasGroup>().blocksRaycasts = true;
			utageBattleSceneManager.playerFrameParent.GetComponent<CanvasGroup>().interactable = true;
			utageBattleSceneManager.playerFrameParent.GetComponent<CanvasGroup>().blocksRaycasts = true;
			utageBattleSceneManager.enemyGroupPanel.GetComponent<CanvasGroup>().interactable = true;
			utageBattleSceneManager.enemyImagePanel.GetComponent<CanvasGroup>().interactable = true;
			Debug.Log("戦闘UIのマテリアル：none");
			break;
		case "player":
			SetBattleUiMaterial(isBlack: true);
			SetPlayerMaterial(isBlack: false);
			SetEnemyMaterial(isBlack: true);
			foreach (GameObject enemyImageGo2 in utageBattleSceneManager.enemyImageGoList)
			{
				enemyImageGo2.GetComponent<ParameterContainer>().GetGameObject("selectNameGroup").SetActive(value: false);
			}
			utageBattleSceneManager.commandButtonGroup[0].GetComponent<CanvasGroup>().interactable = false;
			utageBattleSceneManager.commandButtonGroup[0].GetComponent<CanvasGroup>().blocksRaycasts = false;
			utageBattleSceneManager.commandButtonGroup[1].GetComponent<CanvasGroup>().interactable = false;
			utageBattleSceneManager.commandButtonGroup[1].GetComponent<CanvasGroup>().blocksRaycasts = false;
			utageBattleSceneManager.playerFrameParent.GetComponent<CanvasGroup>().interactable = true;
			utageBattleSceneManager.playerFrameParent.GetComponent<CanvasGroup>().blocksRaycasts = true;
			utageBattleSceneManager.enemyGroupPanel.GetComponent<CanvasGroup>().interactable = false;
			utageBattleSceneManager.enemyImagePanel.GetComponent<CanvasGroup>().interactable = false;
			Debug.Log("戦闘UIのマテリアル：player");
			break;
		case "enemy":
		{
			SetBattleUiMaterial(isBlack: true);
			SetPlayerMaterial(isBlack: true);
			SetEnemyMaterial(isBlack: false);
			foreach (GameObject enemyImageGo3 in utageBattleSceneManager.enemyImageGoList)
			{
				enemyImageGo3.GetComponent<ParameterContainer>().GetGameObject("selectNameGroup").SetActive(value: true);
			}
			utageBattleSceneManager.commandButtonGroup[0].GetComponent<CanvasGroup>().interactable = false;
			utageBattleSceneManager.commandButtonGroup[0].GetComponent<CanvasGroup>().blocksRaycasts = false;
			utageBattleSceneManager.commandButtonGroup[1].GetComponent<CanvasGroup>().interactable = false;
			utageBattleSceneManager.commandButtonGroup[1].GetComponent<CanvasGroup>().blocksRaycasts = false;
			utageBattleSceneManager.playerFrameParent.GetComponent<CanvasGroup>().interactable = false;
			utageBattleSceneManager.playerFrameParent.GetComponent<CanvasGroup>().blocksRaycasts = false;
			for (int i = 0; i < PlayerStatusDataManager.playerPartyMember.Length; i++)
			{
				ParameterContainer component = utageBattleSceneManager.playerFrameGoList[i].GetComponent<ParameterContainer>();
				component.GetGameObject("characterImage").GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f);
				component.GetGameObject("frameImage").GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f);
			}
			utageBattleSceneManager.enemyGroupPanel.GetComponent<CanvasGroup>().interactable = true;
			utageBattleSceneManager.enemyImagePanel.GetComponent<CanvasGroup>().interactable = true;
			Debug.Log("戦闘UIのマテリアル：enemy");
			break;
		}
		}
	}

	private void SetPlayerMaterial(bool isBlack)
	{
		ResetBattlePlayerMaterial();
		if (isBlack)
		{
			scenarioBattleTurnManager.scenarioBattleUI_Player.SetFloat("_ColorHSV_Brightness_1", 0.4f);
			for (int i = 0; i < utageBattleSceneManager.playerFrameGoList.Count; i++)
			{
				GameObject gameObject = utageBattleSceneManager.playerFrameGoList[i];
				foreach (TmpText item in gameObject.GetComponent<ParameterContainer>().GetVariableList<TmpText>("tmpNumList").ToList())
				{
					item.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmpBlackAndOutlineMaterial;
				}
				foreach (TmpText item2 in gameObject.GetComponent<ParameterContainer>().GetVariableList<TmpText>("tmp1mList").ToList())
				{
					item2.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmp1mBlackAndOutlineMaterial;
				}
				foreach (TmpText item3 in gameObject.GetComponent<ParameterContainer>().GetVariableList<TmpText>("tmpBoldList").ToList())
				{
					item3.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmpBoldBlackAndOutlineMaterial;
				}
			}
			return;
		}
		if (scenarioBattleTurnManager.playerSkillData != null && scenarioBattleSkillManager.blackImageGoArray[1].activeInHierarchy && scenarioBattleSkillManager.isUseSkill)
		{
			if (scenarioBattleTurnManager.playerSkillData.skillTarget == BattleSkillData.SkillTarget.self)
			{
				scenarioBattleTurnManager.scenarioBattleUI_Player.SetFloat("_ColorHSV_Brightness_1", 0.4f);
				int memberNum = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == scenarioBattleTurnManager.useSkillPartyMemberID).memberNum;
				GameObject gameObject2 = utageBattleSceneManager.playerFrameGoList[memberNum];
				CopyBattlePlayerMaterial(gameObject2, 1f);
				CopyBattlePlayerSkillButtonMaterial(gameObject2, 1f);
				foreach (GameObject playerFrameGo in utageBattleSceneManager.playerFrameGoList)
				{
					foreach (TmpText item4 in playerFrameGo.GetComponent<ParameterContainer>().GetVariableList<TmpText>("tmpNumList").ToList())
					{
						item4.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmpBlackAndOutlineMaterial;
					}
					foreach (TmpText item5 in playerFrameGo.GetComponent<ParameterContainer>().GetVariableList<TmpText>("tmp1mList").ToList())
					{
						item5.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmp1mBlackAndOutlineMaterial;
					}
					foreach (TmpText item6 in playerFrameGo.GetComponent<ParameterContainer>().GetVariableList<TmpText>("tmpBoldList").ToList())
					{
						item6.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmpBoldBlackAndOutlineMaterial;
					}
				}
				ParameterContainer component = utageBattleSceneManager.playerFrameGoList[memberNum].GetComponent<ParameterContainer>();
				foreach (TmpText item7 in component.GetVariableList<TmpText>("tmpNumList").ToList())
				{
					item7.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmpNormalOutlineMaterial;
				}
				foreach (TmpText item8 in component.GetVariableList<TmpText>("tmp1mList").ToList())
				{
					item8.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmp1mMaterial;
				}
				foreach (TmpText item9 in component.GetComponent<ParameterContainer>().GetVariableList<TmpText>("tmpBoldList").ToList())
				{
					item9.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmpBoldMaterial;
				}
				Debug.Log("自分のみ通常表示");
				return;
			}
			scenarioBattleTurnManager.scenarioBattleUI_Player.SetFloat("_ColorHSV_Brightness_1", 1f);
			for (int j = 0; j < utageBattleSceneManager.playerFrameGoList.Count; j++)
			{
				GameObject gameObject3 = utageBattleSceneManager.playerFrameGoList[j];
				foreach (TmpText item10 in gameObject3.GetComponent<ParameterContainer>().GetVariableList<TmpText>("tmpNumList").ToList())
				{
					item10.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmpNormalOutlineMaterial;
				}
				foreach (TmpText item11 in gameObject3.GetComponent<ParameterContainer>().GetVariableList<TmpText>("tmp1mList").ToList())
				{
					item11.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmp1mMaterial;
				}
				foreach (TmpText item12 in gameObject3.GetComponent<ParameterContainer>().GetVariableList<TmpText>("tmpBoldList").ToList())
				{
					item12.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmpBoldMaterial;
				}
			}
			Debug.Log("味方は全員通常表示");
			return;
		}
		scenarioBattleTurnManager.scenarioBattleUI_Player.SetFloat("_ColorHSV_Brightness_1", 1f);
		int k;
		for (k = 0; k < PlayerStatusDataManager.playerPartyMember.Length; k++)
		{
			if (PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == k).isDead)
			{
				GameObject targetFrameGo = utageBattleSceneManager.playerFrameGoList[k];
				CopyBattlePlayerMaterial(targetFrameGo, 0.4f);
				Debug.Log("死亡者はスライダを暗転表示／num：" + k);
			}
		}
		for (int l = 0; l < utageBattleSceneManager.playerFrameGoList.Count; l++)
		{
			GameObject gameObject4 = utageBattleSceneManager.playerFrameGoList[l];
			foreach (TmpText item13 in gameObject4.GetComponent<ParameterContainer>().GetVariableList<TmpText>("tmpNumList").ToList())
			{
				item13.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmpNormalOutlineMaterial;
			}
			foreach (TmpText item14 in gameObject4.GetComponent<ParameterContainer>().GetVariableList<TmpText>("tmp1mList").ToList())
			{
				item14.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmp1mMaterial;
			}
			foreach (TmpText item15 in gameObject4.GetComponent<ParameterContainer>().GetVariableList<TmpText>("tmpBoldList").ToList())
			{
				item15.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmpBoldMaterial;
			}
		}
		Debug.Log("味方は死亡者以外、通常表示");
	}

	private void SetEnemyMaterial(bool isBlack)
	{
		if (isBlack)
		{
			scenarioBattleTurnManager.scenarioBattleUI_Enemy.SetFloat("_ColorHSV_Brightness_1", 0.4f);
			{
				foreach (GameObject enemyButtonGo in utageBattleSceneManager.enemyButtonGoList)
				{
					foreach (TmpText item in enemyButtonGo.GetComponent<ParameterContainer>().GetVariableList<TmpText>("tmpList").ToList())
					{
						item.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmpBlackAndOutlineMaterial;
					}
					enemyButtonGo.GetComponent<ParameterContainer>().GetVariable<TmpText>("selectNumberText").textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmpBlackMaterial;
				}
				return;
			}
		}
		scenarioBattleTurnManager.scenarioBattleUI_Enemy.SetFloat("_ColorHSV_Brightness_1", 1f);
		foreach (GameObject enemyButtonGo2 in utageBattleSceneManager.enemyButtonGoList)
		{
			foreach (TmpText item2 in enemyButtonGo2.GetComponent<ParameterContainer>().GetVariableList<TmpText>("tmpList").ToList())
			{
				item2.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmpNormalOutlineMaterial;
			}
			enemyButtonGo2.GetComponent<ParameterContainer>().GetVariable<TmpText>("selectNumberText").textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmpNormalMaterial;
		}
	}

	private void SetBattleUiMaterial(bool isBlack)
	{
		ParameterContainer component = scenarioBattleTurnManager.GetComponent<ParameterContainer>();
		if (isBlack)
		{
			scenarioBattleTurnManager.scenarioBattleUI.SetFloat("_ColorHSV_Brightness_1", 0.4f);
			foreach (TmpText item in component.GetVariableList<TmpText>("tmpList").ToList())
			{
				item.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmpBlackAndOutlineMaterial;
			}
			foreach (TmpText item2 in component.GetVariableList<TmpText>("tmpItemList").ToList())
			{
				item2.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmpBlackMaterial;
			}
			{
				foreach (TmpText item3 in component.GetVariableList<TmpText>("tmpBoldList").ToList())
				{
					item3.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmpBoldBlackAndOutlineMaterial;
				}
				return;
			}
		}
		scenarioBattleTurnManager.scenarioBattleUI.SetFloat("_ColorHSV_Brightness_1", 1f);
		foreach (TmpText item4 in component.GetVariableList<TmpText>("tmpList").ToList())
		{
			item4.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmpNormalOutlineMaterial;
		}
		foreach (TmpText item5 in component.GetVariableList<TmpText>("tmpItemList").ToList())
		{
			item5.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmpNormalMaterial;
		}
		foreach (TmpText item6 in component.GetVariableList<TmpText>("tmpBoldList").ToList())
		{
			item6.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmpBoldMaterial;
		}
	}

	public void CopyBattlePlayerMaterial(GameObject targetFrameGo, float brightness)
	{
		ParameterContainer component = targetFrameGo.GetComponent<ParameterContainer>();
		Material material = new Material(component.GetGameObject("skillButton").GetComponent<Image>().material);
		foreach (GameObject gameObject in component.GetGameObjectList("miscUiGoList"))
		{
			gameObject.GetComponent<Image>().material = material;
		}
		foreach (GameObject gameObject2 in component.GetGameObjectList("buffImageGoList"))
		{
			gameObject2.GetComponent<Image>().material = material;
		}
		foreach (GameObject gameObject3 in component.GetGameObjectList("badStateImageGoList"))
		{
			gameObject3.GetComponent<Image>().material = material;
		}
		material.SetFloat("_ColorHSV_Brightness_1", brightness);
		Debug.Log("指定したミニフレームのマテリアルを差し替える：" + brightness);
	}

	public void CopyBattlePlayerSkillButtonMaterial(GameObject targetGo, float brightness)
	{
		ParameterContainer component = targetGo.GetComponent<ParameterContainer>();
		Material material = new Material(component.GetGameObject("skillButton").GetComponent<Image>().material);
		if (!component.GetBool("isSupportCharacter"))
		{
			component.GetVariable<UguiImage>("skillButtonImage").image.material = material;
			component.GetVariable<UguiImage>("skillIconImage").image.material = material;
			component.GetGameObject("chargeCircleGo").GetComponent<Image>().material = material;
			component.GetGameObject("talismanButton").GetComponent<Image>().material = material;
			component.GetVariable<UguiImage>("talismanIconImage").image.material = material;
			component.GetGameObject("talismanCircleGo").GetComponent<Image>().material = material;
			component.GetGameObject("halfSkillButton").GetComponent<Image>().material = material;
			component.GetVariable<UguiImage>("halfSkillIconImage").image.material = material;
			component.GetGameObject("halfSkillCircleGo").GetComponent<Image>().material = material;
			foreach (GameObject gameObject in component.GetGameObjectList("halfSkillButtonGoList"))
			{
				gameObject.GetComponent<Image>().material = material;
			}
			foreach (UguiImage variable in component.GetVariableList<UguiImage>("halfSkillIconImageList"))
			{
				variable.image.material = material;
			}
			foreach (GameObject gameObject2 in component.GetGameObjectList("halfSkillCircleGoList"))
			{
				gameObject2.GetComponent<Image>().material = material;
			}
			foreach (GameObject gameObject3 in component.GetGameObjectList("partyHalfSkillButtonGoList"))
			{
				gameObject3.GetComponent<Image>().material = material;
			}
			foreach (UguiImage variable2 in component.GetVariableList<UguiImage>("partyHalfSkillIconImageList"))
			{
				variable2.image.material = material;
			}
			foreach (GameObject gameObject4 in component.GetGameObjectList("partyHalfSkillCircleGoList"))
			{
				gameObject4.GetComponent<Image>().material = material;
			}
		}
		material.SetFloat("_ColorHSV_Brightness_1", brightness);
		if (brightness == 1f)
		{
			component.GetVariable<TmpText>("tmpBold").textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmpBoldMaterial;
		}
		else
		{
			component.GetVariable<TmpText>("tmpBold").textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmpBoldBlackAndOutlineMaterial;
		}
		Debug.Log("指定したスキルボタンのマテリアルを差し替える／明るさ：" + brightness);
	}

	private void ResetBattlePlayerMaterial()
	{
		for (int i = 0; i < utageBattleSceneManager.playerFrameGoList.Count; i++)
		{
			ParameterContainer component = utageBattleSceneManager.playerFrameGoList[i].GetComponent<ParameterContainer>();
			if (component.GetGameObjectList("miscUiGoList")[0].GetComponent<Image>().material != scenarioBattleTurnManager.scenarioBattleUI_Player)
			{
				if (!component.GetBool("isSupportCharacter"))
				{
					component.GetGameObject("skillButton").GetComponent<Image>().material = scenarioBattleTurnManager.scenarioBattleUI_Player;
					component.GetVariable<UguiImage>("skillIconImage").image.material = scenarioBattleTurnManager.scenarioBattleUI_Player;
					component.GetGameObject("chargeCircleGo").GetComponent<Image>().material = scenarioBattleTurnManager.scenarioBattleUI_Player;
					component.GetGameObject("talismanButton").GetComponent<Image>().material = scenarioBattleTurnManager.scenarioBattleUI_Player;
					component.GetVariable<UguiImage>("talismanIconImage").image.material = scenarioBattleTurnManager.scenarioBattleUI_Player;
					component.GetGameObject("talismanCircleGo").GetComponent<Image>().material = scenarioBattleTurnManager.scenarioBattleUI_Player;
					component.GetGameObject("halfSkillButton").GetComponent<Image>().material = scenarioBattleTurnManager.scenarioBattleUI_Player;
					component.GetVariable<UguiImage>("halfSkillIconImage").image.material = scenarioBattleTurnManager.scenarioBattleUI_Player;
					component.GetGameObject("halfSkillCircleGo").GetComponent<Image>().material = scenarioBattleTurnManager.scenarioBattleUI_Player;
					foreach (GameObject gameObject in component.GetGameObjectList("halfSkillButtonGoList"))
					{
						gameObject.GetComponent<Image>().material = scenarioBattleTurnManager.scenarioBattleUI_Player;
					}
					foreach (UguiImage variable in component.GetVariableList<UguiImage>("halfSkillIconImageList"))
					{
						variable.image.material = scenarioBattleTurnManager.scenarioBattleUI_Player;
					}
					foreach (GameObject gameObject2 in component.GetGameObjectList("halfSkillCircleGoList"))
					{
						gameObject2.GetComponent<Image>().material = scenarioBattleTurnManager.scenarioBattleUI_Player;
					}
					foreach (GameObject gameObject3 in component.GetGameObjectList("partyHalfSkillButtonGoList"))
					{
						gameObject3.GetComponent<Image>().material = scenarioBattleTurnManager.scenarioBattleUI_Player;
					}
					foreach (UguiImage variable2 in component.GetVariableList<UguiImage>("partyHalfSkillIconImageList"))
					{
						variable2.image.material = scenarioBattleTurnManager.scenarioBattleUI_Player;
					}
					foreach (GameObject gameObject4 in component.GetGameObjectList("partyHalfSkillCircleGoList"))
					{
						gameObject4.GetComponent<Image>().material = scenarioBattleTurnManager.scenarioBattleUI_Player;
					}
				}
				foreach (GameObject gameObject5 in component.GetGameObjectList("miscUiGoList"))
				{
					gameObject5.GetComponent<Image>().material = scenarioBattleTurnManager.scenarioBattleUI_Player;
				}
				foreach (GameObject gameObject6 in component.GetGameObjectList("buffImageGoList"))
				{
					gameObject6.GetComponent<Image>().material = scenarioBattleTurnManager.scenarioBattleUI_Player;
				}
				foreach (GameObject gameObject7 in component.GetGameObjectList("badStateImageGoList"))
				{
					gameObject7.GetComponent<Image>().material = scenarioBattleTurnManager.scenarioBattleUI_Player;
				}
			}
			else
			{
				Debug.Log("コピーではないので差し替え不要：" + i);
			}
		}
		Debug.Log("味方の全マテリアルを全部元に戻す");
	}

	public void SetFrameSkillButtonInteractable(ParameterContainer param, bool value)
	{
		if (param.GetBool("isSupportCharacter"))
		{
			return;
		}
		BattleSkillButtonManagerForPM component = param.GetGameObject("statusGroupGo").GetComponent<BattleSkillButtonManagerForPM>();
		foreach (GameObject gameObject in param.GetGameObjectList("halfSkillButtonGoList"))
		{
			gameObject.GetComponent<CanvasGroup>().blocksRaycasts = value;
			gameObject.GetComponent<CanvasGroup>().interactable = value;
		}
		foreach (UguiImage variable in param.GetVariableList<UguiImage>("halfSkillIconImageList"))
		{
			variable.image.raycastTarget = value;
		}
		foreach (GameObject gameObject2 in param.GetGameObjectList("partyHalfSkillButtonGoList"))
		{
			gameObject2.GetComponent<CanvasGroup>().blocksRaycasts = value;
			gameObject2.GetComponent<CanvasGroup>().interactable = value;
		}
		foreach (UguiImage variable2 in param.GetVariableList<UguiImage>("partyHalfSkillIconImageList"))
		{
			variable2.image.raycastTarget = value;
		}
		if (param.GetInt("characterID") == 0)
		{
			component.halfChangeCicleAnimator.SetTrigger("disable");
		}
		else
		{
			component.partyHalfChangeCicleAnimator.SetTrigger("disable");
		}
	}
}
