using Coffee.UIExtensions;
using DG.Tweening;
using PathologicalGames;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ExtensionEffectManager : MonoBehaviour
{
	private InDoorCommandManager inDoorCommandManager;

	private ExtensionDialogManager extensionDialogManager;

	public Image[] extensionPartsImageArray;

	public Material[] extensionPartsMaterialArray;

	public PlayableDirector playableDirector;

	public UIParticle uiParticle;

	public AudioSource audioSource;

	public Transform poolParent;

	public GameObject spawnPrefabGo;

	private Transform spawnGo;

	private void Awake()
	{
		Material[] array = extensionPartsMaterialArray;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetFloat("_Brightness_Fade_1", 0f);
		}
	}

	public void StartExtensionTimeline(int index)
	{
		extensionDialogManager = GameObject.Find("Extension Dialog Manager").GetComponent<ExtensionDialogManager>();
		uiParticle.transform.SetParent(extensionPartsImageArray[index].transform);
		uiParticle.transform.localPosition = new Vector3(0f, 0f, 0f);
		playableDirector.time = 0.0;
		playableDirector.Play();
	}

	public void SpawnExtensionEffect()
	{
		audioSource.volume = PlayerOptionsDataManager.optionsSeVolume;
		extensionPartsImageArray[extensionDialogManager.selectExtensionIndex].material.DOFloat(1f, "_Brightness_Fade_1", 0.5f);
		spawnGo = PoolManager.Pools["inDoorPool"].Spawn(spawnPrefabGo, uiParticle.transform);
		spawnGo.localPosition = new Vector3(0f, 0f, 0f);
		spawnGo.localScale = new Vector3(1f, 1f, 1f);
		uiParticle.gameObject.SetActive(value: true);
		uiParticle.RefreshParticles();
	}

	public void ChangeExtensionPartsImage()
	{
		inDoorCommandManager = GameObject.Find("InDoor Command Manager").GetComponent<InDoorCommandManager>();
		extensionPartsImageArray[extensionDialogManager.selectExtensionIndex].material.DOFloat(0f, "_Brightness_Fade_1", 0.5f);
		CraftWorkShopData craftWorkShopData = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"];
		switch (extensionDialogManager.selectExtensionIndex)
		{
		case 0:
			inDoorCommandManager.carrageWorkShopImage.sprite = inDoorCommandManager.bgCarriageWorkShopSpriteList[craftWorkShopData.workShopLv];
			break;
		case 1:
			inDoorCommandManager.carrageToolImage.sprite = inDoorCommandManager.bgCarriageToolSpriteList[craftWorkShopData.workShopToolLv];
			break;
		case 2:
			inDoorCommandManager.carrageFurnaceImage.sprite = inDoorCommandManager.bgCarriageFurnaceSpriteList[craftWorkShopData.furnaceLv];
			break;
		case 3:
			inDoorCommandManager.carrageAddOnImage.sprite = inDoorCommandManager.bgCarriageAddOnSpriteList[craftWorkShopData.enableAddOnLv + 1];
			break;
		}
	}

	public void AfterExtensionEffect()
	{
		if (PoolManager.Pools["inDoorPool"].IsSpawned(spawnGo))
		{
			PoolManager.Pools["inDoorPool"].Despawn(spawnGo, poolParent);
		}
		uiParticle.gameObject.SetActive(value: false);
		extensionDialogManager.OpenAfterExtensionDialog();
	}
}
