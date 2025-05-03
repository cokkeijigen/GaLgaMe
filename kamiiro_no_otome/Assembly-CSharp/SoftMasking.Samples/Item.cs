using UnityEngine;
using UnityEngine.UI;

namespace SoftMasking.Samples
{
	public class Item : MonoBehaviour
	{
		public Image image;

		public Text title;

		public Text description;

		public RectTransform healthBar;

		public RectTransform damageBar;

		public void Set(string name, Sprite sprite, Color color, float health, float damage)
		{
			if ((bool)image)
			{
				image.sprite = sprite;
				image.color = color;
			}
			if ((bool)title)
			{
				title.text = name;
			}
			if ((bool)description)
			{
				description.text = "The short description of " + name;
			}
			if ((bool)healthBar)
			{
				healthBar.anchorMax = new Vector2(health, 1f);
			}
			if ((bool)damageBar)
			{
				damageBar.anchorMax = new Vector2(damage, 1f);
			}
		}
	}
}
