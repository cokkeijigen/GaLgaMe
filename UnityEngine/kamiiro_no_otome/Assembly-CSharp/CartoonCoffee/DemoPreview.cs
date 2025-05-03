using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CartoonCoffee
{
	public class DemoPreview : MonoBehaviour
	{
		public static DemoPreview c;

		private Dictionary<string, Button> buttons;

		private Transform currentCategory;

		private Transform categoryParent;

		private Text currentParticle;

		private Text currentIndex;

		private int index;

		private Transform particleParent;

		private void Awake()
		{
			c = this;
			buttons = new Dictionary<string, Button>();
			currentParticle = base.transform.Find("Banner/CurrentText").GetComponent<Text>();
			currentIndex = base.transform.Find("Banner/Count").GetComponent<Text>();
			categoryParent = GameObject.Find("ParticleCategories").transform;
			particleParent = base.transform.parent.Find("Particles");
			GameObject gameObject = base.transform.Find("Banner/CategoryButton").gameObject;
			for (int i = 0; i < categoryParent.childCount; i++)
			{
				Transform category = categoryParent.GetChild(i);
				GameObject obj = Object.Instantiate(gameObject);
				obj.GetComponent<Text>().text = category.name;
				obj.transform.SetParent(gameObject.transform.parent, worldPositionStays: false);
				obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(25f, -80 - 27 * i);
				Button component = obj.GetComponent<Button>();
				component.onClick.AddListener(delegate
				{
					SelectCategory(category.name);
				});
				buttons.Add(category.name, component);
				obj.SetActive(value: true);
				category.gameObject.SetActive(value: false);
			}
			SelectCategory(categoryParent.GetChild(0).name);
		}

		private void Update()
		{
			if (!currentCategory.name.StartsWith("Projectile"))
			{
				if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.mouseScrollDelta.y < 0f)
				{
					NextProjectile();
				}
				if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.mouseScrollDelta.y > 0f)
				{
					PreviousProjectile();
				}
				if (((Input.GetMouseButtonDown(0) && Input.mousePosition.x / (float)Screen.width > 0.175f) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter)) && currentCategory.name.StartsWith("Burst"))
				{
					GameObject obj = Object.Instantiate(currentCategory.GetChild(index).GetChild(0).gameObject);
					obj.transform.SetParent(particleParent, worldPositionStays: false);
					Object.Destroy(obj, 10f);
				}
			}
		}

		public void SelectCategory(string category)
		{
			if (currentCategory != null)
			{
				buttons[currentCategory.name].interactable = true;
				buttons[currentCategory.name].transform.Find("Selected").gameObject.SetActive(value: false);
				currentCategory.gameObject.SetActive(value: false);
			}
			buttons[category].interactable = false;
			buttons[category].transform.Find("Selected").gameObject.SetActive(value: true);
			currentCategory = categoryParent.Find(category);
			currentCategory.gameObject.SetActive(value: true);
			SelectIndex(0);
			RemoveButtonHighlight();
		}

		public void NextProjectile()
		{
			if (currentCategory.name.StartsWith("Projectile"))
			{
				DemoHandler.c.Next();
				return;
			}
			index++;
			if (index >= currentCategory.childCount)
			{
				index = 0;
			}
			SelectIndex(index);
			RemoveButtonHighlight();
		}

		public void PreviousProjectile()
		{
			if (currentCategory.name.StartsWith("Projectile"))
			{
				DemoHandler.c.Previous();
				return;
			}
			index--;
			if (index < 0)
			{
				index = currentCategory.childCount - 1;
			}
			SelectIndex(index);
			RemoveButtonHighlight();
		}

		private void DisableOtherParticles()
		{
			for (int i = 0; i < currentCategory.childCount; i++)
			{
				currentCategory.GetChild(i).gameObject.SetActive(value: false);
			}
			for (int j = 0; j < particleParent.childCount; j++)
			{
				particleParent.GetChild(j).gameObject.SetActive(value: false);
			}
		}

		private void SelectIndex(int newIndex)
		{
			DisableOtherParticles();
			index = newIndex;
			currentCategory.GetChild(index).gameObject.SetActive(value: true);
			UpdateText();
		}

		public void UpdateText()
		{
			if (currentCategory.name.StartsWith("Projectile"))
			{
				currentIndex.text = DemoHandler.c.GetIndexString();
				currentParticle.text = DemoHandler.c.GetProjectile();
			}
			else
			{
				currentIndex.text = index + 1 + "/" + currentCategory.childCount;
				currentParticle.text = currentCategory.GetChild(index).name;
			}
		}

		private void RemoveButtonHighlight()
		{
			if (EventSystem.current != null)
			{
				EventSystem.current.SetSelectedGameObject(null);
			}
		}
	}
}
