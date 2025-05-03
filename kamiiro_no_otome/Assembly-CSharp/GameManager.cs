using UnityEngine;

public class GameManager : MonoBehaviour
{
	public TextMesh text_fx_name;

	public GameObject[] fx_prefabs;

	public int index_fx;

	private Ray ray;

	private RaycastHit2D ray_cast_hit;

	private void Start()
	{
		text_fx_name.text = "[" + (index_fx + 1) + "] " + fx_prefabs[index_fx].name;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			ray_cast_hit = Physics2D.Raycast(new Vector2(ray.origin.x, ray.origin.y), new Vector2(0f, 0f));
			if ((bool)ray_cast_hit)
			{
				switch (ray_cast_hit.transform.name)
				{
				case "BG":
					Object.Instantiate(fx_prefabs[index_fx], new Vector3(ray.origin.x, ray.origin.y, 0f), Quaternion.identity);
					break;
				case "UI-arrow-right":
					ray_cast_hit.transform.SendMessage("Go");
					index_fx++;
					if (index_fx >= fx_prefabs.Length)
					{
						index_fx = 0;
					}
					text_fx_name.text = "[" + (index_fx + 1) + "] " + fx_prefabs[index_fx].name;
					break;
				case "UI-arrow-left":
					ray_cast_hit.transform.SendMessage("Go");
					index_fx--;
					if (index_fx <= -1)
					{
						index_fx = fx_prefabs.Length - 1;
					}
					text_fx_name.text = "[" + (index_fx + 1) + "] " + fx_prefabs[index_fx].name;
					break;
				case "Instructions":
					Object.Destroy(ray_cast_hit.transform.gameObject);
					break;
				}
			}
		}
		if (Input.GetKeyDown("z") || Input.GetKeyDown("left"))
		{
			GameObject.Find("UI-arrow-left").SendMessage("Go");
			index_fx--;
			if (index_fx <= -1)
			{
				index_fx = fx_prefabs.Length - 1;
			}
			text_fx_name.text = "[" + (index_fx + 1) + "] " + fx_prefabs[index_fx].name;
		}
		if (Input.GetKeyDown("x") || Input.GetKeyDown("right"))
		{
			GameObject.Find("UI-arrow-right").SendMessage("Go");
			index_fx++;
			if (index_fx >= fx_prefabs.Length)
			{
				index_fx = 0;
			}
			text_fx_name.text = "[" + (index_fx + 1) + "] " + fx_prefabs[index_fx].name;
		}
		if (Input.GetKeyDown("space"))
		{
			Object.Instantiate(fx_prefabs[index_fx], new Vector3(0f, 0f, 0f), Quaternion.identity);
		}
	}
}
