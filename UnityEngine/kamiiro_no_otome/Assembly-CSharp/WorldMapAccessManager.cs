using System.Collections.Generic;
using Arbor;
using Coffee.UIExtensions;
using DarkTonic.MasterAudio;
using DG.Tweening;
using I2.Loc;
using PathologicalGames;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorldMapAccessManager : SerializedMonoBehaviour
{
	public TotalMapAccessManager totalMapAccessManager;

	public GameObject worldMapCameraGo;

	public CanvasGroup worldCanvasGroup;

	public Camera worldMapCamera;

	public NeedWorldMapDateData needWorldMapDateData;

	public WorldMapUnlockDataBase worldMapUnlockDataBase;

	public float moveLimitX;

	public float moveLimitY;

	private Vector3 mouseStartPos;

	public Texture2D mapHandCursor;

	public Image[] worldMapBgImageArray;

	public Sprite[] worldMapTimeZoneSpriteArray;

	public Transform totalMapPoolParentGo;

	public GameObject newMapPointEffectPrefabGo;

	public GameObject dungeonGetItemNoticeCanvas;

	public Image eventItemIconImage;

	public GameObject getItemWithRecipeTextGo;

	public Localize getItemNoticeTextLoc;

	public Localize getItemSummaryTextLoc;

	public GameObject poolParentGo;

	public UIParticle uIParticle;

	public Transform getItemEffectPrefabGo;

	private Transform effectSpawnGo;

	public List<bool> isWorldMapAccessPointInitialized = new List<bool>();

	public Transform spawnEffectGo;

	public Vector3 worldPlayerPosV3;

	public void SetWorldMapCanvasInteractable(bool value)
	{
		if (value)
		{
			worldCanvasGroup.alpha = 1f;
		}
		worldCanvasGroup.interactable = value;
	}

	public void AddWorldMapAccessPointInitialize()
	{
		isWorldMapAccessPointInitialized.Add(item: true);
	}

	public void OpenWorldMapDialog(UnityAction unityAction)
	{
		Vector3 position = PlayerNonSaveDataManager.selectAccessPointGO.transform.position;
		position.x = Mathf.Clamp(position.x, moveLimitX * -1f, moveLimitX);
		position.y = Mathf.Clamp(position.y, moveLimitY * -1f, moveLimitY);
		position.z = -100f;
		worldMapCameraGo.transform.DOMove(position, 0.1f).OnComplete(delegate
		{
			unityAction();
		});
	}

	public int GetNeedAccessDay(string currentPointName, string selectPointName)
	{
		List<string> list = new List<string>();
		for (int i = 0; i < needWorldMapDateData.needAccessDayList.Count; i++)
		{
			list.Add(worldMapUnlockDataBase.worldMapUnlockDataList[i].currentPointName);
		}
		int index = list.FindIndex((string data) => data == currentPointName);
		int num = list.FindIndex((string data) => data == selectPointName);
		return int.Parse(needWorldMapDateData.needAccessDayList[index][num]);
	}

	public int GetNeedAccessTime(string currentPointName, string selectPointName)
	{
		List<string> list = new List<string>();
		for (int i = 0; i < needWorldMapDateData.needAccessDayList.Count; i++)
		{
			list.Add(worldMapUnlockDataBase.worldMapUnlockDataList[i].currentPointName);
		}
		int index = list.FindIndex((string data) => data == currentPointName);
		int num = list.FindIndex((string data) => data == selectPointName);
		return int.Parse(needWorldMapDateData.needAccessTimeZoneList[index][num]);
	}

	public bool IsPointerOnUGUI()
	{
		mouseStartPos = worldMapCamera.ScreenToWorldPoint(Input.mousePosition);
		bool flag = false;
		PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
		List<RaycastResult> list = new List<RaycastResult>();
		pointerEventData.position = Input.mousePosition;
		EventSystem.current.RaycastAll(pointerEventData, list);
		foreach (RaycastResult item in list)
		{
			if (item.gameObject.CompareTag("UiButton") || item.gameObject.CompareTag("AreaPoint"))
			{
				flag = true;
			}
			if (flag)
			{
				break;
			}
		}
		return flag;
	}

	public void ChangeMapCursor(bool isHandCursol)
	{
		if (isHandCursol)
		{
			if (PlayerDataManager.mapPlaceStatusNum == 0)
			{
				Cursor.SetCursor(mapHandCursor, Vector2.zero, CursorMode.Auto);
			}
		}
		else
		{
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		}
	}

	public void SetWolrdMapCameraDrag()
	{
		Vector3 vector = worldMapCamera.ScreenToWorldPoint(Input.mousePosition);
		if (mouseStartPos != vector)
		{
			Vector3 vector2 = mouseStartPos - vector;
			worldMapCamera.transform.localPosition += vector2;
			Vector3 localPosition = worldMapCamera.transform.localPosition;
			if (worldMapCamera.transform.localPosition.x > moveLimitX)
			{
				localPosition.x = moveLimitX;
			}
			else if (worldMapCamera.transform.localPosition.x < moveLimitX * -1f)
			{
				localPosition.x = moveLimitX * -1f;
			}
			if (worldMapCamera.transform.localPosition.y > moveLimitY)
			{
				localPosition.y = moveLimitY;
			}
			else if (worldMapCamera.transform.localPosition.y < moveLimitY * -1f)
			{
				localPosition.y = moveLimitY * -1f;
			}
			worldMapCamera.transform.localPosition = localPosition;
		}
	}

	public void SetWorldMapCameraDragEnable(bool value)
	{
		worldCanvasGroup.interactable = value;
		worldCanvasGroup.blocksRaycasts = value;
		PlayerDataManager.worldMapInputBlock = !value;
	}

	public void SetWorldMapTimeZoneImage()
	{
		worldMapBgImageArray[1].sprite = worldMapBgImageArray[0].sprite;
		worldMapBgImageArray[1].color = Color.white;
		worldMapBgImageArray[0].sprite = worldMapTimeZoneSpriteArray[PlayerDataManager.currentTimeZone];
		worldMapBgImageArray[1].DOFade(0f, 0.2f);
	}

	public void StartDungeonDeepClearEffect()
	{
		worldPlayerPosV3 = totalMapAccessManager.worldPlayerGo.transform.position;
		DOTween.Sequence().Append(totalMapAccessManager.worldPlayerGo.transform.DOMoveY(worldPlayerPosV3.y + 1f, 0.2f)).Append(totalMapAccessManager.worldPlayerGo.GetComponent<CanvasGroup>().DOFade(0f, 0.2f));
		Invoke("InvokeAfterWorldPlayerGoFade", 1f);
	}

	private void InvokeAfterWorldPlayerGoFade()
	{
		GameObject gameObject = worldCanvasGroup.transform.Find("World Area/" + PlayerDataManager.currentDungeonName).GetComponent<ParameterContainer>().GetGameObject("flagImageGo");
		gameObject.SetActive(value: true);
		Debug.Log("旗を表示：" + PlayerDataManager.currentDungeonName);
		spawnEffectGo = PoolManager.Pools["totalMapPool"].Spawn(newMapPointEffectPrefabGo, gameObject.transform);
		spawnEffectGo.localScale = new Vector3(1f, 1f, 1f);
		spawnEffectGo.localPosition = new Vector3(0f, 0f, 0f);
		MasterAudio.PlaySound("SeEffectNewWorldPoint", 1f, null, 0f, null, null);
		Invoke("InvokeAfterParticleSpawn", 1.5f);
	}

	private void InvokeAfterParticleSpawn()
	{
		DOTween.Sequence().Append(totalMapAccessManager.worldPlayerGo.GetComponent<CanvasGroup>().DOFade(1f, 0.2f)).Append(totalMapAccessManager.worldPlayerGo.transform.DOMoveY(worldPlayerPosV3.y, 0.2f));
		PoolManager.Pools["totalMapPool"].Despawn(spawnEffectGo, 0f, totalMapPoolParentGo);
		PlayerNonSaveDataManager.isDungeonDeepClear = false;
		if (PlayerNonSaveDataManager.isDungeonGetRareItem)
		{
			int itemId = PlayerNonSaveDataManager.dungeonGetRareItemId;
			getItemWithRecipeTextGo.SetActive(value: false);
			if (itemId < 950)
			{
				getItemNoticeTextLoc.Term = "eventItem" + itemId;
				getItemSummaryTextLoc.Term = "eventItem" + itemId + "_summary";
				Sprite itemSprite = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData data) => data.itemID == itemId).itemSprite;
				eventItemIconImage.sprite = itemSprite;
				if (!string.IsNullOrEmpty(GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData data) => data.itemID == itemId).rewardRecipeName))
				{
					getItemWithRecipeTextGo.SetActive(value: true);
				}
			}
			else
			{
				getItemNoticeTextLoc.Term = "jewel" + itemId;
				getItemSummaryTextLoc.Term = "jewel" + itemId + "_summary";
				Sprite itemSprite2 = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData data) => data.itemID == itemId).itemSprite;
				eventItemIconImage.sprite = itemSprite2;
			}
			MasterAudio.PlaySound("SeNotice", 1f, null, 0f, null, null);
			dungeonGetItemNoticeCanvas.SetActive(value: true);
			effectSpawnGo = PoolManager.Pools["totalMapPool"].Spawn(getItemEffectPrefabGo, uIParticle.transform);
			effectSpawnGo.localScale = new Vector3(1f, 1f, 1f);
			effectSpawnGo.localPosition = new Vector3(0f, 0f, 0f);
			Debug.Log("アイテム獲得エフェクトをスポーン");
			uIParticle.RefreshParticles();
		}
		else
		{
			CloseDungeonGetItemNotice();
		}
	}

	public void CloseDungeonGetItemNotice()
	{
		PlayerNonSaveDataManager.isDungeonGetRareItem = false;
		PlayerNonSaveDataManager.isDungeonGetRareItemWithRecipe = false;
		if (PoolManager.Pools["totalMapPool"].IsSpawned(effectSpawnGo))
		{
			PoolManager.Pools["totalMapPool"].Despawn(effectSpawnGo, poolParentGo.transform);
		}
		uIParticle.RefreshParticles();
		foreach (Transform item in uIParticle.gameObject.transform)
		{
			Object.Destroy(item.gameObject);
		}
		dungeonGetItemNoticeCanvas.SetActive(value: false);
		Debug.Log("踏破ダンジョンの演出終了");
		GameObject.Find("Transition Manager").GetComponent<PlayMakerFSM>().SendEvent("PushNoticeOkButton");
	}
}
