using UnityEngine;

namespace SoftMasking.Samples
{
	public class ItemsGenerator : MonoBehaviour
	{
		public RectTransform target;

		public Sprite image;

		public int count;

		public string baseName;

		public Item itemPrefab;

		private static readonly Color[] colors = new Color[7]
		{
			Color.red,
			Color.green,
			Color.blue,
			Color.cyan,
			Color.yellow,
			Color.magenta,
			Color.gray
		};

		public void Generate()
		{
			DestroyChildren();
			int num = Random.Range(0, colors.Length - 1);
			for (int i = 0; i < count; i++)
			{
				Item item = Object.Instantiate(itemPrefab);
				item.transform.SetParent(target, worldPositionStays: false);
				item.Set($"{baseName} {i + 1:D2}", image, colors[(num + i) % colors.Length], Random.Range(0.4f, 1f), Random.Range(0.4f, 1f));
			}
		}

		private void DestroyChildren()
		{
			while (target.childCount > 0)
			{
				Object.DestroyImmediate(target.GetChild(0).gameObject);
			}
		}
	}
}
