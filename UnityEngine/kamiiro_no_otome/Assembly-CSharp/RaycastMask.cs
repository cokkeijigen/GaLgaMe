using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class RaycastMask : MonoBehaviour, ICanvasRaycastFilter
{
	private Image _image;

	private Sprite _sprite;

	private void Start()
	{
		_image = GetComponent<Image>();
	}

	public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
	{
		_sprite = _image.sprite;
		RectTransform rectTransform = (RectTransform)base.transform;
		RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)base.transform, sp, eventCamera, out var localPoint);
		Vector2 vector = new Vector2(localPoint.x + rectTransform.pivot.x * rectTransform.rect.width, localPoint.y + rectTransform.pivot.y * rectTransform.rect.height);
		Rect textureRect = _sprite.textureRect;
		Rect rect = rectTransform.rect;
		int num = 0;
		int num2 = 0;
		Image.Type type = _image.type;
		if (type != 0 && type == Image.Type.Sliced)
		{
			Vector4 border = _sprite.border;
			num = ((vector.x < border.x) ? Mathf.FloorToInt(textureRect.x + vector.x) : ((!(vector.x > rect.width - border.z)) ? Mathf.FloorToInt(textureRect.x + border.x + (vector.x - border.x) / (rect.width - border.x - border.z) * (textureRect.width - border.x - border.z)) : Mathf.FloorToInt(textureRect.x + textureRect.width - (rect.width - vector.x))));
			num2 = ((vector.y < border.y) ? Mathf.FloorToInt(textureRect.y + vector.y) : ((!(vector.y > rect.height - border.w)) ? Mathf.FloorToInt(textureRect.y + border.y + (vector.y - border.y) / (rect.height - border.y - border.w) * (textureRect.height - border.y - border.w)) : Mathf.FloorToInt(textureRect.y + textureRect.height - (rect.height - vector.y))));
		}
		else
		{
			num = Mathf.FloorToInt(textureRect.x + textureRect.width * vector.x / rect.width);
			num2 = Mathf.FloorToInt(textureRect.y + textureRect.height * vector.y / rect.height);
		}
		try
		{
			return _sprite.texture.GetPixel(num, num2).a > 0f;
		}
		catch (UnityException)
		{
			Debug.LogError("Mask texture not readable, set your sprite to Texture Type 'Advanced' and check 'Read/Write Enabled'");
			Object.Destroy(this);
			return false;
		}
	}
}
