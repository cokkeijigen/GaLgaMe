using System.Collections.Generic;
using Coffee.UIExtensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class DungeonRouteAnimationManager : SerializedMonoBehaviour
{
	public GameObject edenAnimationImageGo;

	public GameObject heroineAnimationImageGo;

	public Image dungeonRouteAnimationBgImage;

	public Image dungeonRouteAnimationRouteImage;

	public Image dungeonRouteAnimationEnemyImage;

	public AudioSource dungeonRouteAudioSource;

	public Sprite[] edenTalkSpriteArray;

	public Sprite[] heroineWalkSpriteArray;

	public Sprite[] heroineTalk1SpriteArray;

	public Sprite[] heroineTalk2SpriteArray;

	public Sprite[] heroineWaitSpriteArray;

	public Sprite[] heroineBattleSpriteArray;

	public Sprite[] enemyBattleSpriteArray;

	public Dictionary<string, Sprite> dungeonRouteBgDictionary = new Dictionary<string, Sprite>();

	public Dictionary<string, TimelineAsset> dungeonRouteTimelineDictionary = new Dictionary<string, TimelineAsset>();

	public GameObject[] treasurePrefabGoArray;

	public Transform treasureSpawnGo;

	public UIParticle uIParticle;
}
