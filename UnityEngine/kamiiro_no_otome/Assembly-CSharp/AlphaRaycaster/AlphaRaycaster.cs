using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AlphaRaycaster
{
	[AddComponentMenu("Event/Alpha Raycaster")]
	[ExecuteInEditMode]
	public class AlphaRaycaster : GraphicRaycaster
	{
		private struct CharacterRect
		{
			private Rect rect;

			private Text text;

			private UIVertex upperLeftVertex;

			private UIVertex bottomRightVertex;

			private UIVertex bottomLeftVertex;

			public CharacterRect(Text text, UIVertex ulv, UIVertex urv, UIVertex brv, UIVertex blv)
			{
				upperLeftVertex = ulv;
				bottomRightVertex = brv;
				bottomLeftVertex = blv;
				this.text = text;
				rect = new Rect(blv.position.x, blv.position.y, Mathf.Abs(ulv.position.x - urv.position.x), Mathf.Abs(ulv.position.y - blv.position.y));
			}

			public bool Contains(Vector2 position)
			{
				return GetScaledRect().Contains(position);
			}

			public float GetTextureAlphaFromPosition(Vector2 position)
			{
				Vector2 vector = Rect.PointToNormalized(GetScaledRect(), position);
				Texture2D texture2D = text.mainTexture as Texture2D;
				if (!texture2D)
				{
					return 0f;
				}
				float num = Mathf.Lerp(bottomLeftVertex.uv0.x, bottomRightVertex.uv0.x, vector.x) * (float)texture2D.width;
				float num2 = Mathf.Lerp(bottomLeftVertex.uv0.y, upperLeftVertex.uv0.y, vector.y) * (float)texture2D.height;
				return texture2D.GetPixel((int)num, (int)num2).a;
			}

			private Rect GetScaledRect()
			{
				float num = (float)text.fontSize / (float)text.font.fontSize;
				return new Rect(rect.xMin * num, rect.yMin * num, rect.width * num, rect.height * num);
			}
		}

		[Header("Alpha test properties")]
		[Range(0f, 1f)]
		[Tooltip("Texture regions of the UI objects with opacity (alpha) lower than alpha threshold won't react to input events.")]
		public float AlphaThreshold = 0.9f;

		[Tooltip("Whether material tint color of the UI objects should affect alpha threshold.")]
		public bool IncludeMaterialAlpha;

		[Tooltip("When selective mode is active the alpha testing will only execute for UI objects with AlphaCheck component.")]
		public bool SelectiveMode;

		[Tooltip("Show warnings in the console when attempting to alpha test objects with a not-readable texture.")]
		public bool ShowTextureWarnings;

		protected override void OnEnable()
		{
			base.OnEnable();
			GraphicRaycaster component = GetComponent<GraphicRaycaster>();
			if ((bool)component && component != this)
			{
				UnityEngine.Object.DestroyImmediate(component);
			}
		}

		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			base.Raycast(eventData, resultAppendList);
			if (SelectiveMode)
			{
				return;
			}
			for (int num = resultAppendList.Count - 1; num >= 0; num--)
			{
				if (!resultAppendList[num].gameObject.GetComponent<AlphaCheck>())
				{
					try
					{
						Image component = resultAppendList[num].gameObject.GetComponent<Image>();
						if ((bool)component)
						{
							if ((bool)component.sprite && AlphaCheckImage(resultAppendList[num].gameObject, component, eventData.position, eventCamera, AlphaThreshold, IncludeMaterialAlpha))
							{
								resultAppendList.RemoveAt(num);
							}
						}
						else
						{
							RawImage component2 = resultAppendList[num].gameObject.GetComponent<RawImage>();
							if ((bool)component2)
							{
								if (AlphaCheckRawImage(resultAppendList[num].gameObject, component2, eventData.position, eventCamera, AlphaThreshold, IncludeMaterialAlpha))
								{
									resultAppendList.RemoveAt(num);
								}
							}
							else
							{
								Text component3 = resultAppendList[num].gameObject.GetComponent<Text>();
								if ((bool)component3 && AlphaCheckText(resultAppendList[num].gameObject, component3, eventData.position, eventCamera, AlphaThreshold, IncludeMaterialAlpha))
								{
									resultAppendList.RemoveAt(num);
								}
							}
						}
					}
					catch (UnityException ex)
					{
						if (Application.isEditor && ShowTextureWarnings)
						{
							Debug.LogWarning($"Alpha test failed: {ex.Message}");
						}
					}
				}
			}
		}

		public static bool AlphaCheckImage(GameObject obj, Image objImage, Vector2 pointerPos, Camera eventCamera, float alphaThreshold, bool includeMaterialAlpha)
		{
			RectTransform rectTransform = obj.transform as RectTransform;
			if (!rectTransform)
			{
				return false;
			}
			Vector3 vector = ScreenToLocalObjectPosition(pointerPos, rectTransform, eventCamera);
			Texture2D texture2D = objImage.mainTexture as Texture2D;
			if (!texture2D)
			{
				return false;
			}
			Rect imageTextureRect = GetImageTextureRect(objImage);
			Vector2 size = rectTransform.rect.size;
			if (objImage.preserveAspect && (objImage.type == Image.Type.Simple || objImage.type == Image.Type.Filled))
			{
				if (size.x < size.y)
				{
					size.y = size.x * (imageTextureRect.height / imageTextureRect.width);
				}
				else
				{
					size.x = size.y * (imageTextureRect.width / imageTextureRect.height);
				}
				Vector2 vector2 = new Vector2((!Mathf.Approximately(Mathf.Abs(rectTransform.pivot.x), 0.5f)) ? 1 : 2, (!Mathf.Approximately(Mathf.Abs(rectTransform.pivot.y), 0.5f)) ? 1 : 2);
				if (Mathf.Abs(vector.x * vector2.x) > size.x || Mathf.Abs(vector.y * vector2.y) > size.y)
				{
					return true;
				}
			}
			float num = vector.x + size.x * rectTransform.pivot.x;
			float num2 = vector.y + size.y * rectTransform.pivot.y;
			Vector4 border = objImage.sprite.border;
			Vector2 vector3 = new Vector2(border.x + border.z, border.y + border.w);
			Vector2 vector4 = new Vector2(imageTextureRect.width - vector3.x, imageTextureRect.height - vector3.y);
			Vector4 vector5 = border / objImage.pixelsPerUnit;
			Vector2 vector6 = new Vector2(vector5.x + vector5.z, vector5.y + vector5.w);
			Rect rect = new Rect(vector5.x, vector5.y, Mathf.Clamp(size.x - vector6.x, 0f, float.PositiveInfinity), Mathf.Clamp(size.y - vector6.y, 0f, float.PositiveInfinity));
			bool flag = objImage.hasBorder && rect.Contains(new Vector2(num, num2));
			if (objImage.type == Image.Type.Tiled)
			{
				if (flag)
				{
					if (!objImage.fillCenter)
					{
						return true;
					}
					num = border.x + (num - vector5.x) * objImage.pixelsPerUnit % vector4.x;
					num2 = border.y + (num2 - vector5.y) * objImage.pixelsPerUnit % vector4.y;
				}
				else if (objImage.hasBorder)
				{
					num *= Mathf.Clamp(vector6.x / size.x, 1f, float.PositiveInfinity);
					num2 *= Mathf.Clamp(vector6.y / size.y, 1f, float.PositiveInfinity);
					if (num >= vector5.x && num <= vector5.x + rect.width)
					{
						num = border.x + (num - vector5.x) * objImage.pixelsPerUnit % vector4.x;
					}
					else if (num > vector5.x + rect.width)
					{
						num = imageTextureRect.width - border.z + (num - rect.width - vector5.x) * border.x / vector5.x;
					}
					else if (num < vector5.x)
					{
						num = num * border.x / vector5.x;
					}
					if (num2 >= vector5.y && num2 <= vector5.y + rect.height)
					{
						num2 = border.y + (num2 - vector5.y) * objImage.pixelsPerUnit % vector4.y;
					}
					else if (num2 > vector5.y + rect.height)
					{
						num2 = imageTextureRect.height - border.w + (num2 - rect.height - vector5.y) * border.y / vector5.y;
					}
					else if (num2 < vector5.y)
					{
						num2 = num2 * border.y / vector5.y;
					}
				}
				else
				{
					if (num > imageTextureRect.width)
					{
						num %= imageTextureRect.width;
					}
					if (num2 > imageTextureRect.height)
					{
						num2 %= imageTextureRect.height;
					}
				}
			}
			else if (objImage.type == Image.Type.Sliced)
			{
				if (flag)
				{
					if (!objImage.fillCenter)
					{
						return true;
					}
					num = border.x + (num - vector5.x) * (vector4.x / rect.width);
					num2 = border.y + (num2 - vector5.y) * (vector4.y / rect.height);
				}
				else
				{
					num *= Mathf.Clamp(vector6.x / size.x, 1f, float.PositiveInfinity);
					num2 *= Mathf.Clamp(vector6.y / size.y, 1f, float.PositiveInfinity);
					if (num >= vector5.x && num <= vector5.x + rect.width)
					{
						num = border.x + (num - vector5.x) * (vector4.x / rect.width);
					}
					else if (num > vector5.x + rect.width)
					{
						num = imageTextureRect.width - border.z + (num - rect.width - vector5.x) * border.x / vector5.x;
					}
					else if (num < vector5.x)
					{
						num = num * border.x / vector5.x;
					}
					if (num2 >= vector5.y && num2 <= vector5.y + rect.height)
					{
						num2 = border.y + (num2 - vector5.y) * (vector4.y / rect.height);
					}
					else if (num2 > vector5.y + rect.height)
					{
						num2 = imageTextureRect.height - border.w + (num2 - rect.height - vector5.y) * border.y / vector5.y;
					}
					else if (num2 < vector5.y)
					{
						num2 = num2 * border.y / vector5.y;
					}
				}
			}
			else
			{
				num *= imageTextureRect.width / size.x;
				num2 *= imageTextureRect.height / size.y;
			}
			if (objImage.type == Image.Type.Filled)
			{
				float num3 = ((imageTextureRect.height > imageTextureRect.width) ? (num * (imageTextureRect.height / imageTextureRect.width)) : num);
				float num4 = ((imageTextureRect.width > imageTextureRect.height) ? (num2 * (imageTextureRect.width / imageTextureRect.height)) : num2);
				float num5 = ((imageTextureRect.height > imageTextureRect.width) ? imageTextureRect.height : imageTextureRect.width);
				float num6 = ((imageTextureRect.width > imageTextureRect.height) ? imageTextureRect.width : imageTextureRect.height);
				if (objImage.fillMethod == Image.FillMethod.Horizontal)
				{
					if (objImage.fillOrigin == 0 && num / imageTextureRect.width > objImage.fillAmount)
					{
						return true;
					}
					if (objImage.fillOrigin == 1 && num / imageTextureRect.width < 1f - objImage.fillAmount)
					{
						return true;
					}
				}
				if (objImage.fillMethod == Image.FillMethod.Vertical)
				{
					if (objImage.fillOrigin == 0 && num2 / imageTextureRect.height > objImage.fillAmount)
					{
						return true;
					}
					if (objImage.fillOrigin == 1 && num2 / imageTextureRect.height < 1f - objImage.fillAmount)
					{
						return true;
					}
				}
				if (objImage.fillMethod == Image.FillMethod.Radial90)
				{
					if (objImage.fillOrigin == 0)
					{
						if (objImage.fillClockwise && Mathf.Atan(num4 / num3) / (MathF.PI / 2f) < 1f - objImage.fillAmount)
						{
							return true;
						}
						if (!objImage.fillClockwise && Mathf.Atan(num4 / num3) / (MathF.PI / 2f) > objImage.fillAmount)
						{
							return true;
						}
					}
					if (objImage.fillOrigin == 1)
					{
						if (objImage.fillClockwise && num4 < (0f - 1f / Mathf.Tan((1f - objImage.fillAmount) * MathF.PI / 2f)) * num3 + num6)
						{
							return true;
						}
						if (!objImage.fillClockwise && num4 > (0f - 1f / Mathf.Tan(objImage.fillAmount * MathF.PI / 2f)) * num3 + num6)
						{
							return true;
						}
					}
					if (objImage.fillOrigin == 2)
					{
						if (objImage.fillClockwise && num4 > Mathf.Tan((1f - objImage.fillAmount) * MathF.PI / 2f) * (num3 - num5) + num6)
						{
							return true;
						}
						if (!objImage.fillClockwise && num4 < Mathf.Tan(objImage.fillAmount * MathF.PI / 2f) * (num3 - num5) + num6)
						{
							return true;
						}
					}
					if (objImage.fillOrigin == 3)
					{
						if (objImage.fillClockwise && num4 > 1f / Mathf.Tan((1f - objImage.fillAmount) * MathF.PI / 2f) * (num5 - num3))
						{
							return true;
						}
						if (!objImage.fillClockwise && num4 < 1f / Mathf.Tan(objImage.fillAmount * MathF.PI / 2f) * (num5 - num3))
						{
							return true;
						}
					}
				}
				if (objImage.fillMethod == Image.FillMethod.Radial180)
				{
					if (objImage.fillOrigin == 0)
					{
						if (objImage.fillClockwise && Mathf.Atan2(num4, 2f * (num3 - num5 / 2f)) < (1f - objImage.fillAmount) * MathF.PI)
						{
							return true;
						}
						if (!objImage.fillClockwise && Mathf.Atan2(num2, 2f * (num3 - num5 / 2f)) > objImage.fillAmount * MathF.PI)
						{
							return true;
						}
					}
					if (objImage.fillOrigin == 1)
					{
						if (objImage.fillClockwise && Mathf.Atan2(num3, -2f * (num4 - num6 / 2f)) < (1f - objImage.fillAmount) * MathF.PI)
						{
							return true;
						}
						if (!objImage.fillClockwise && Mathf.Atan2(num3, -2f * (num4 - num6 / 2f)) > objImage.fillAmount * MathF.PI)
						{
							return true;
						}
					}
					if (objImage.fillOrigin == 2)
					{
						if (objImage.fillClockwise && Mathf.Atan2(num6 - num4, -2f * (num3 - num5 / 2f)) < (1f - objImage.fillAmount) * MathF.PI)
						{
							return true;
						}
						if (!objImage.fillClockwise && Mathf.Atan2(num6 - num4, -2f * (num3 - num5 / 2f)) > objImage.fillAmount * MathF.PI)
						{
							return true;
						}
					}
					if (objImage.fillOrigin == 3)
					{
						if (objImage.fillClockwise && Mathf.Atan2(num5 - num3, 2f * (num4 - num6 / 2f)) < (1f - objImage.fillAmount) * MathF.PI)
						{
							return true;
						}
						if (!objImage.fillClockwise && Mathf.Atan2(num5 - num3, 2f * (num4 - num6 / 2f)) > objImage.fillAmount * MathF.PI)
						{
							return true;
						}
					}
				}
				if (objImage.fillMethod == Image.FillMethod.Radial360)
				{
					if (objImage.fillOrigin == 0)
					{
						if (objImage.fillClockwise)
						{
							float num7 = Mathf.Atan2(num4 - num6 / 2f, num3 - num5 / 2f) + MathF.PI / 2f;
							float num8 = MathF.PI * 2f * (1f - objImage.fillAmount);
							num7 = ((num7 < 0f) ? (MathF.PI * 2f + num7) : num7);
							if (num7 < num8)
							{
								return true;
							}
						}
						if (!objImage.fillClockwise)
						{
							float num9 = Mathf.Atan2(num4 - num6 / 2f, num3 - num5 / 2f) + MathF.PI / 2f;
							float num10 = MathF.PI * 2f * objImage.fillAmount;
							num9 = ((num9 < 0f) ? (MathF.PI * 2f + num9) : num9);
							if (num9 > num10)
							{
								return true;
							}
						}
					}
					if (objImage.fillOrigin == 1)
					{
						if (objImage.fillClockwise)
						{
							float num11 = Mathf.Atan2(num4 - num6 / 2f, num3 - num5 / 2f);
							float num12 = MathF.PI * 2f * (1f - objImage.fillAmount);
							num11 = ((num11 < 0f) ? (MathF.PI * 2f + num11) : num11);
							if (num11 < num12)
							{
								return true;
							}
						}
						if (!objImage.fillClockwise)
						{
							float num13 = Mathf.Atan2(num4 - num6 / 2f, num3 - num5 / 2f);
							float num14 = MathF.PI * 2f * objImage.fillAmount;
							num13 = ((num13 < 0f) ? (MathF.PI * 2f + num13) : num13);
							if (num13 > num14)
							{
								return true;
							}
						}
					}
					if (objImage.fillOrigin == 2)
					{
						if (objImage.fillClockwise)
						{
							float num15 = Mathf.Atan2(num4 - num6 / 2f, num3 - num5 / 2f) - MathF.PI / 2f;
							float num16 = MathF.PI * 2f * (1f - objImage.fillAmount);
							num15 = ((num15 < 0f) ? (MathF.PI * 2f + num15) : num15);
							if (num15 < num16)
							{
								return true;
							}
						}
						if (!objImage.fillClockwise)
						{
							float num17 = Mathf.Atan2(num4 - num6 / 2f, num3 - num5 / 2f) - MathF.PI / 2f;
							float num18 = MathF.PI * 2f * objImage.fillAmount;
							num17 = ((num17 < 0f) ? (MathF.PI * 2f + num17) : num17);
							if (num17 > num18)
							{
								return true;
							}
						}
					}
					if (objImage.fillOrigin == 3)
					{
						if (objImage.fillClockwise)
						{
							float num19 = Mathf.Atan2(num4 - num6 / 2f, num3 - num5 / 2f) - MathF.PI;
							float num20 = MathF.PI * 2f * (1f - objImage.fillAmount);
							num19 = ((num19 < 0f) ? (MathF.PI * 2f + num19) : num19);
							if (num19 < num20)
							{
								return true;
							}
						}
						if (!objImage.fillClockwise)
						{
							float num21 = Mathf.Atan2(num4 - num6 / 2f, num3 - num5 / 2f) - MathF.PI;
							float num22 = MathF.PI * 2f * objImage.fillAmount;
							num21 = ((num21 < 0f) ? (MathF.PI * 2f + num21) : num21);
							if (num21 > num22)
							{
								return true;
							}
						}
					}
				}
			}
			float num23 = texture2D.GetPixel((int)(num + imageTextureRect.x), (int)(num2 + imageTextureRect.y)).a;
			if (includeMaterialAlpha)
			{
				num23 *= objImage.color.a;
			}
			if (num23 < alphaThreshold)
			{
				return true;
			}
			return false;
		}

		public static bool AlphaCheckText(GameObject obj, Text objText, Vector2 pointerPos, Camera eventCamera, float alphaThreshold, bool includeMaterialAlpha)
		{
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(obj.transform as RectTransform, pointerPos, eventCamera, out var localPoint))
			{
				return true;
			}
			List<CharacterRect> list = new List<CharacterRect>();
			for (int i = 0; i < objText.cachedTextGenerator.verts.Count; i += 4)
			{
				list.Add(new CharacterRect(objText, objText.cachedTextGenerator.verts[i], objText.cachedTextGenerator.verts[i + 1], objText.cachedTextGenerator.verts[i + 2], objText.cachedTextGenerator.verts[i + 3]));
			}
			float num = -1f;
			foreach (CharacterRect item in list)
			{
				if (item.Contains(localPoint))
				{
					num = item.GetTextureAlphaFromPosition(localPoint);
					break;
				}
			}
			if (Mathf.Approximately(num, -1f))
			{
				return true;
			}
			if (includeMaterialAlpha)
			{
				num *= objText.color.a;
			}
			if (num < alphaThreshold)
			{
				return true;
			}
			return false;
		}

		public static bool AlphaCheckRawImage(GameObject obj, RawImage objRawImage, Vector2 pointerPos, Camera eventCamera, float alphaThreshold, bool includeMaterialAlpha)
		{
			RectTransform rectTransform = obj.transform as RectTransform;
			if (!rectTransform)
			{
				return false;
			}
			Vector3 vector = ScreenToLocalObjectPosition(pointerPos, rectTransform, eventCamera);
			Texture2D texture2D = objRawImage.mainTexture as Texture2D;
			if (!texture2D)
			{
				return false;
			}
			Rect uvRect = objRawImage.uvRect;
			Vector2 size = rectTransform.rect.size;
			float num = vector.x + size.x * rectTransform.pivot.x;
			float num2 = vector.y + size.y * rectTransform.pivot.y;
			num = (num + uvRect.x) * uvRect.width;
			num2 = (num2 + uvRect.y) * uvRect.height;
			float num3 = texture2D.GetPixel((int)(num + uvRect.x), (int)(num2 + uvRect.y)).a;
			if (includeMaterialAlpha)
			{
				num3 *= objRawImage.color.a;
			}
			if (num3 < alphaThreshold)
			{
				return true;
			}
			return false;
		}

		private static Vector3 ScreenToLocalObjectPosition(Vector2 screenPosition, RectTransform objTrs, Camera eventCamera)
		{
			Vector3 position;
			if ((bool)eventCamera)
			{
				Plane plane = new Plane(objTrs.forward, objTrs.position);
				Ray ray = eventCamera.ScreenPointToRay(screenPosition);
				plane.Raycast(ray, out var enter);
				position = ray.GetPoint(enter);
			}
			else
			{
				position = screenPosition;
				float num = ((0f - objTrs.forward.x) * (position.x - objTrs.position.x) - objTrs.forward.y * (position.y - objTrs.position.y)) / objTrs.forward.z;
				position += new Vector3(0f, 0f, objTrs.position.z + num);
			}
			return objTrs.InverseTransformPoint(position);
		}

		private static Rect GetImageTextureRect(Image objImage)
		{
			if (objImage.sprite.textureRectOffset.sqrMagnitude > 0f)
			{
				return objImage.sprite.packed ? new Rect(objImage.sprite.textureRect.xMin - objImage.sprite.textureRectOffset.x, objImage.sprite.textureRect.yMin - objImage.sprite.textureRectOffset.y, objImage.sprite.textureRect.width + objImage.sprite.textureRectOffset.x * 2f, objImage.sprite.textureRect.height + objImage.sprite.textureRectOffset.y * 2f) : objImage.sprite.rect;
			}
			return objImage.sprite.textureRect;
		}
	}
}
