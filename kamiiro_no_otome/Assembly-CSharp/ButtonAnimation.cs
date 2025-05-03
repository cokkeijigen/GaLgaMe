using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
	private float initial_size_x;

	private float initial_size_y;

	public float factor = 0.5f;

	public float speed = 1.15f;

	private bool GO;

	private void Awake()
	{
		initial_size_x = base.transform.localScale.x;
		initial_size_y = base.transform.localScale.y;
	}

	private void FixedUpdate()
	{
		if (GO)
		{
			float x = base.transform.localScale.x;
			float y = base.transform.localScale.y;
			if (base.transform.localScale.y < initial_size_y)
			{
				x *= speed;
				y *= speed;
				base.transform.localScale = new Vector3(x, y, 1f);
			}
			else
			{
				x = initial_size_x;
				y = initial_size_y;
				base.transform.localScale = new Vector3(x, y, 1f);
				GO = false;
			}
		}
	}

	private void Go()
	{
		GO = true;
		base.transform.localScale = new Vector3(initial_size_x * factor, initial_size_y * factor, 1f);
	}
}
