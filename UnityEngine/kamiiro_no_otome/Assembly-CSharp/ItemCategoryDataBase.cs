using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/Item Category DataBase")]
public class ItemCategoryDataBase : SerializedScriptableObject
{
	public Dictionary<string, Sprite> itemCategoryIconDictionary = new Dictionary<string, Sprite>();

	public Dictionary<string, Sprite> skillCategoryIconDictionary = new Dictionary<string, Sprite>();

	public Dictionary<string, Sprite> bodyCategoryIconDictionary = new Dictionary<string, Sprite>();

	public Dictionary<string, Sprite> craftIconDictionary = new Dictionary<string, Sprite>();

	public Dictionary<string, Sprite> questIconDictionary = new Dictionary<string, Sprite>();

	public Dictionary<string, Sprite> dungeonCardIconDictionary = new Dictionary<string, Sprite>();

	public Dictionary<string, Sprite> dungeonCardTypeIconDictionary = new Dictionary<string, Sprite>();

	public Dictionary<string, Sprite> buffIconDictionary = new Dictionary<string, Sprite>();

	public Dictionary<string, Sprite> deBuffIconDictionary = new Dictionary<string, Sprite>();

	public Dictionary<string, Sprite> badStateIconDictionary = new Dictionary<string, Sprite>();

	public Dictionary<string, Sprite> eventIconDictionary = new Dictionary<string, Sprite>();

	public List<Sprite> weekDayIconList = new List<Sprite>();
}
