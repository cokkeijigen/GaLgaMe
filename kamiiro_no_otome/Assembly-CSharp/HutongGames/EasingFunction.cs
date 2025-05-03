using System;
using UnityEngine;

namespace HutongGames
{
	public class EasingFunction
	{
		public enum Ease
		{
			EaseInQuad,
			EaseOutQuad,
			EaseInOutQuad,
			EaseInCubic,
			EaseOutCubic,
			EaseInOutCubic,
			EaseInQuart,
			EaseOutQuart,
			EaseInOutQuart,
			EaseInQuint,
			EaseOutQuint,
			EaseInOutQuint,
			EaseInSine,
			EaseOutSine,
			EaseInOutSine,
			EaseInExpo,
			EaseOutExpo,
			EaseInOutExpo,
			EaseInCirc,
			EaseOutCirc,
			EaseInOutCirc,
			Linear,
			Spring,
			EaseInBounce,
			EaseOutBounce,
			EaseInOutBounce,
			EaseInBack,
			EaseOutBack,
			EaseInOutBack,
			EaseInElastic,
			EaseOutElastic,
			EaseInOutElastic,
			CustomCurve,
			Punch
		}

		public delegate float Function(float s, float e, float v);

		private const float NATURAL_LOG_OF_2 = 0.6931472f;

		public static AnimationCurve AnimationCurve;

		public static float Linear(float start, float end, float value)
		{
			return Mathf.Lerp(start, end, value);
		}

		public static float Spring(float start, float end, float value)
		{
			value = Mathf.Clamp01(value);
			value = (Mathf.Sin(value * MathF.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + 1.2f * (1f - value));
			return start + (end - start) * value;
		}

		public static float EaseInQuad(float start, float end, float value)
		{
			end -= start;
			return end * value * value + start;
		}

		public static float EaseOutQuad(float start, float end, float value)
		{
			end -= start;
			return (0f - end) * value * (value - 2f) + start;
		}

		public static float EaseInOutQuad(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end * 0.5f * value * value + start;
			}
			value -= 1f;
			return (0f - end) * 0.5f * (value * (value - 2f) - 1f) + start;
		}

		public static float EaseInCubic(float start, float end, float value)
		{
			end -= start;
			return end * value * value * value + start;
		}

		public static float EaseOutCubic(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return end * (value * value * value + 1f) + start;
		}

		public static float EaseInOutCubic(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end * 0.5f * value * value * value + start;
			}
			value -= 2f;
			return end * 0.5f * (value * value * value + 2f) + start;
		}

		public static float EaseInQuart(float start, float end, float value)
		{
			end -= start;
			return end * value * value * value * value + start;
		}

		public static float EaseOutQuart(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return (0f - end) * (value * value * value * value - 1f) + start;
		}

		public static float EaseInOutQuart(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end * 0.5f * value * value * value * value + start;
			}
			value -= 2f;
			return (0f - end) * 0.5f * (value * value * value * value - 2f) + start;
		}

		public static float EaseInQuint(float start, float end, float value)
		{
			end -= start;
			return end * value * value * value * value * value + start;
		}

		public static float EaseOutQuint(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return end * (value * value * value * value * value + 1f) + start;
		}

		public static float EaseInOutQuint(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end * 0.5f * value * value * value * value * value + start;
			}
			value -= 2f;
			return end * 0.5f * (value * value * value * value * value + 2f) + start;
		}

		public static float EaseInSine(float start, float end, float value)
		{
			end -= start;
			return (0f - end) * Mathf.Cos(value * (MathF.PI / 2f)) + end + start;
		}

		public static float EaseOutSine(float start, float end, float value)
		{
			end -= start;
			return end * Mathf.Sin(value * (MathF.PI / 2f)) + start;
		}

		public static float EaseInOutSine(float start, float end, float value)
		{
			end -= start;
			return (0f - end) * 0.5f * (Mathf.Cos(MathF.PI * value) - 1f) + start;
		}

		public static float EaseInExpo(float start, float end, float value)
		{
			end -= start;
			return end * Mathf.Pow(2f, 10f * (value - 1f)) + start;
		}

		public static float EaseOutExpo(float start, float end, float value)
		{
			end -= start;
			return end * (0f - Mathf.Pow(2f, -10f * value) + 1f) + start;
		}

		public static float EaseInOutExpo(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end * 0.5f * Mathf.Pow(2f, 10f * (value - 1f)) + start;
			}
			value -= 1f;
			return end * 0.5f * (0f - Mathf.Pow(2f, -10f * value) + 2f) + start;
		}

		public static float EaseInCirc(float start, float end, float value)
		{
			end -= start;
			return (0f - end) * (Mathf.Sqrt(1f - value * value) - 1f) + start;
		}

		public static float EaseOutCirc(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return end * Mathf.Sqrt(1f - value * value) + start;
		}

		public static float EaseInOutCirc(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return (0f - end) * 0.5f * (Mathf.Sqrt(1f - value * value) - 1f) + start;
			}
			value -= 2f;
			return end * 0.5f * (Mathf.Sqrt(1f - value * value) + 1f) + start;
		}

		public static float EaseInBounce(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			return end - EaseOutBounce(0f, end, num - value) + start;
		}

		public static float EaseOutBounce(float start, float end, float value)
		{
			value /= 1f;
			end -= start;
			if (value < 0.36363637f)
			{
				return end * (7.5625f * value * value) + start;
			}
			if (value < 0.72727275f)
			{
				value -= 0.54545456f;
				return end * (7.5625f * value * value + 0.75f) + start;
			}
			if ((double)value < 0.9090909090909091)
			{
				value -= 0.8181818f;
				return end * (7.5625f * value * value + 0.9375f) + start;
			}
			value -= 21f / 22f;
			return end * (7.5625f * value * value + 63f / 64f) + start;
		}

		public static float EaseInOutBounce(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			if (value < num * 0.5f)
			{
				return EaseInBounce(0f, end, value * 2f) * 0.5f + start;
			}
			return EaseOutBounce(0f, end, value * 2f - num) * 0.5f + end * 0.5f + start;
		}

		public static float EaseInBack(float start, float end, float value)
		{
			end -= start;
			value /= 1f;
			float num = 1.70158f;
			return end * value * value * ((num + 1f) * value - num) + start;
		}

		public static float EaseOutBack(float start, float end, float value)
		{
			float num = 1.70158f;
			end -= start;
			value -= 1f;
			return end * (value * value * ((num + 1f) * value + num) + 1f) + start;
		}

		public static float EaseInOutBack(float start, float end, float value)
		{
			float num = 1.70158f;
			end -= start;
			value /= 0.5f;
			if (value < 1f)
			{
				num *= 1.525f;
				return end * 0.5f * (value * value * ((num + 1f) * value - num)) + start;
			}
			value -= 2f;
			num *= 1.525f;
			return end * 0.5f * (value * value * ((num + 1f) * value + num) + 2f) + start;
		}

		public static float EaseInElastic(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			float num2 = num * 0.3f;
			float num3 = 0f;
			if (value == 0f)
			{
				return start;
			}
			if ((value /= num) == 1f)
			{
				return start + end;
			}
			float num4;
			if (num3 == 0f || num3 < Mathf.Abs(end))
			{
				num3 = end;
				num4 = num2 / 4f;
			}
			else
			{
				num4 = num2 / (MathF.PI * 2f) * Mathf.Asin(end / num3);
			}
			return 0f - num3 * Mathf.Pow(2f, 10f * (value -= 1f)) * Mathf.Sin((value * num - num4) * (MathF.PI * 2f) / num2) + start;
		}

		public static float EaseOutElastic(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			float num2 = num * 0.3f;
			float num3 = 0f;
			if (value == 0f)
			{
				return start;
			}
			if ((value /= num) == 1f)
			{
				return start + end;
			}
			float num4;
			if (num3 == 0f || num3 < Mathf.Abs(end))
			{
				num3 = end;
				num4 = num2 * 0.25f;
			}
			else
			{
				num4 = num2 / (MathF.PI * 2f) * Mathf.Asin(end / num3);
			}
			return num3 * Mathf.Pow(2f, -10f * value) * Mathf.Sin((value * num - num4) * (MathF.PI * 2f) / num2) + end + start;
		}

		public static float EaseInOutElastic(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			float num2 = num * 0.3f;
			float num3 = 0f;
			if (value == 0f)
			{
				return start;
			}
			if ((value /= num * 0.5f) == 2f)
			{
				return start + end;
			}
			float num4;
			if (num3 == 0f || num3 < Mathf.Abs(end))
			{
				num3 = end;
				num4 = num2 / 4f;
			}
			else
			{
				num4 = num2 / (MathF.PI * 2f) * Mathf.Asin(end / num3);
			}
			if (value < 1f)
			{
				return -0.5f * (num3 * Mathf.Pow(2f, 10f * (value -= 1f)) * Mathf.Sin((value * num - num4) * (MathF.PI * 2f) / num2)) + start;
			}
			return num3 * Mathf.Pow(2f, -10f * (value -= 1f)) * Mathf.Sin((value * num - num4) * (MathF.PI * 2f) / num2) * 0.5f + end + start;
		}

		public static float LinearD(float start, float end, float value)
		{
			return end - start;
		}

		public static float EaseInQuadD(float start, float end, float value)
		{
			return 2f * (end - start) * value;
		}

		public static float EaseOutQuadD(float start, float end, float value)
		{
			end -= start;
			return (0f - end) * value - end * (value - 2f);
		}

		public static float EaseInOutQuadD(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end * value;
			}
			value -= 1f;
			return end * (1f - value);
		}

		public static float EaseInCubicD(float start, float end, float value)
		{
			return 3f * (end - start) * value * value;
		}

		public static float EaseOutCubicD(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return 3f * end * value * value;
		}

		public static float EaseInOutCubicD(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return 1.5f * end * value * value;
			}
			value -= 2f;
			return 1.5f * end * value * value;
		}

		public static float EaseInQuartD(float start, float end, float value)
		{
			return 4f * (end - start) * value * value * value;
		}

		public static float EaseOutQuartD(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return -4f * end * value * value * value;
		}

		public static float EaseInOutQuartD(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return 2f * end * value * value * value;
			}
			value -= 2f;
			return -2f * end * value * value * value;
		}

		public static float EaseInQuintD(float start, float end, float value)
		{
			return 5f * (end - start) * value * value * value * value;
		}

		public static float EaseOutQuintD(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return 5f * end * value * value * value * value;
		}

		public static float EaseInOutQuintD(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return 2.5f * end * value * value * value * value;
			}
			value -= 2f;
			return 2.5f * end * value * value * value * value;
		}

		public static float EaseInSineD(float start, float end, float value)
		{
			return (end - start) * 0.5f * MathF.PI * Mathf.Sin(MathF.PI / 2f * value);
		}

		public static float EaseOutSineD(float start, float end, float value)
		{
			end -= start;
			return MathF.PI / 2f * end * Mathf.Cos(value * (MathF.PI / 2f));
		}

		public static float EaseInOutSineD(float start, float end, float value)
		{
			end -= start;
			return end * 0.5f * MathF.PI * Mathf.Cos(MathF.PI * value);
		}

		public static float EaseInExpoD(float start, float end, float value)
		{
			return 6.931472f * (end - start) * Mathf.Pow(2f, 10f * (value - 1f));
		}

		public static float EaseOutExpoD(float start, float end, float value)
		{
			end -= start;
			return 3.465736f * end * Mathf.Pow(2f, 1f - 10f * value);
		}

		public static float EaseInOutExpoD(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return 3.465736f * end * Mathf.Pow(2f, 10f * (value - 1f));
			}
			value -= 1f;
			return 3.465736f * end / Mathf.Pow(2f, 10f * value);
		}

		public static float EaseInCircD(float start, float end, float value)
		{
			return (end - start) * value / Mathf.Sqrt(1f - value * value);
		}

		public static float EaseOutCircD(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return (0f - end) * value / Mathf.Sqrt(1f - value * value);
		}

		public static float EaseInOutCircD(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end * value / (2f * Mathf.Sqrt(1f - value * value));
			}
			value -= 2f;
			return (0f - end) * value / (2f * Mathf.Sqrt(1f - value * value));
		}

		public static float EaseInBounceD(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			return EaseOutBounceD(0f, end, num - value);
		}

		public static float EaseOutBounceD(float start, float end, float value)
		{
			value /= 1f;
			end -= start;
			if (value < 0.36363637f)
			{
				return 2f * end * 7.5625f * value;
			}
			if (value < 0.72727275f)
			{
				value -= 0.54545456f;
				return 2f * end * 7.5625f * value;
			}
			if ((double)value < 0.9090909090909091)
			{
				value -= 0.8181818f;
				return 2f * end * 7.5625f * value;
			}
			value -= 21f / 22f;
			return 2f * end * 7.5625f * value;
		}

		public static float EaseInOutBounceD(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			if (value < num * 0.5f)
			{
				return EaseInBounceD(0f, end, value * 2f) * 0.5f;
			}
			return EaseOutBounceD(0f, end, value * 2f - num) * 0.5f;
		}

		public static float EaseInBackD(float start, float end, float value)
		{
			float num = 1.70158f;
			return 3f * (num + 1f) * (end - start) * value * value - 2f * num * (end - start) * value;
		}

		public static float EaseOutBackD(float start, float end, float value)
		{
			float num = 1.70158f;
			end -= start;
			value -= 1f;
			return end * ((num + 1f) * value * value + 2f * value * ((num + 1f) * value + num));
		}

		public static float EaseInOutBackD(float start, float end, float value)
		{
			float num = 1.70158f;
			end -= start;
			value /= 0.5f;
			if (value < 1f)
			{
				num *= 1.525f;
				return 0.5f * end * (num + 1f) * value * value + end * value * ((num + 1f) * value - num);
			}
			value -= 2f;
			num *= 1.525f;
			return 0.5f * end * ((num + 1f) * value * value + 2f * value * ((num + 1f) * value + num));
		}

		public static float EaseInElasticD(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			float num2 = num * 0.3f;
			float num3 = 0f;
			float num4;
			if (num3 == 0f || num3 < Mathf.Abs(end))
			{
				num3 = end;
				num4 = num2 / 4f;
			}
			else
			{
				num4 = num2 / (MathF.PI * 2f) * Mathf.Asin(end / num3);
			}
			float num5 = MathF.PI * 2f;
			return (0f - num3) * num * num5 * Mathf.Cos(num5 * (num * (value - 1f) - num4) / num2) / num2 - 3.465736f * num3 * Mathf.Sin(num5 * (num * (value - 1f) - num4) / num2) * Mathf.Pow(2f, 10f * (value - 1f) + 1f);
		}

		public static float EaseOutElasticD(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			float num2 = num * 0.3f;
			float num3 = 0f;
			float num4;
			if (num3 == 0f || num3 < Mathf.Abs(end))
			{
				num3 = end;
				num4 = num2 * 0.25f;
			}
			else
			{
				num4 = num2 / (MathF.PI * 2f) * Mathf.Asin(end / num3);
			}
			return num3 * MathF.PI * num * Mathf.Pow(2f, 1f - 10f * value) * Mathf.Cos(MathF.PI * 2f * (num * value - num4) / num2) / num2 - 3.465736f * num3 * Mathf.Pow(2f, 1f - 10f * value) * Mathf.Sin(MathF.PI * 2f * (num * value - num4) / num2);
		}

		public static float EaseInOutElasticD(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			float num2 = num * 0.3f;
			float num3 = 0f;
			float num4;
			if (num3 == 0f || num3 < Mathf.Abs(end))
			{
				num3 = end;
				num4 = num2 / 4f;
			}
			else
			{
				num4 = num2 / (MathF.PI * 2f) * Mathf.Asin(end / num3);
			}
			if (value < 1f)
			{
				value -= 1f;
				return -3.465736f * num3 * Mathf.Pow(2f, 10f * value) * Mathf.Sin(MathF.PI * 2f * (num * value - 2f) / num2) - num3 * MathF.PI * num * Mathf.Pow(2f, 10f * value) * Mathf.Cos(MathF.PI * 2f * (num * value - num4) / num2) / num2;
			}
			value -= 1f;
			return num3 * MathF.PI * num * Mathf.Cos(MathF.PI * 2f * (num * value - num4) / num2) / (num2 * Mathf.Pow(2f, 10f * value)) - 3.465736f * num3 * Mathf.Sin(MathF.PI * 2f * (num * value - num4) / num2) / Mathf.Pow(2f, 10f * value);
		}

		public static float SpringD(float start, float end, float value)
		{
			value = Mathf.Clamp01(value);
			end -= start;
			return end * (6f * (1f - value) / 5f + 1f) * (-2.2f * Mathf.Pow(1f - value, 1.2f) * Mathf.Sin(MathF.PI * value * (2.5f * value * value * value + 0.2f)) + Mathf.Pow(1f - value, 2.2f) * (MathF.PI * (2.5f * value * value * value + 0.2f) + 23.561945f * value * value * value) * Mathf.Cos(MathF.PI * value * (2.5f * value * value * value + 0.2f)) + 1f) - 6f * end * (Mathf.Pow(1f - value, 2.2f) * Mathf.Sin(MathF.PI * value * (2.5f * value * value * value + 0.2f)) + value / 5f);
		}

		public static float CustomCurve(float start, float end, float value)
		{
			if (AnimationCurve == null)
			{
				return Mathf.Lerp(start, end, value);
			}
			return Mathf.Lerp(start, end, AnimationCurve.Evaluate(value));
		}

		public static float Punch(float start, float end, float value)
		{
			if (value <= 0f)
			{
				return start;
			}
			if (value >= 1f)
			{
				return start;
			}
			return Mathf.Pow(2f, -10f * value) * Mathf.Sin(value * (MathF.PI * 2f) / 0.15f);
		}

		public static float PunchD(float start, float end, float value)
		{
			return (0f - (10f * Mathf.Log(2f) * 0.15f * Mathf.Sin(MathF.PI * 2f * value / 0.15f) - MathF.PI * 2f * Mathf.Cos(MathF.PI * 2f * value / 0.15f))) / Mathf.Pow(0.3f, 10f * value);
		}

		public static Function GetEasingFunction(Ease easingFunction)
		{
			switch (easingFunction)
			{
			case Ease.CustomCurve:
				return CustomCurve;
			case Ease.EaseInQuad:
				return EaseInQuad;
			case Ease.EaseOutQuad:
				return EaseOutQuad;
			case Ease.EaseInOutQuad:
				return EaseInOutQuad;
			case Ease.EaseInCubic:
				return EaseInCubic;
			case Ease.EaseOutCubic:
				return EaseOutCubic;
			case Ease.EaseInOutCubic:
				return EaseInOutCubic;
			case Ease.EaseInQuart:
				return EaseInQuart;
			case Ease.EaseOutQuart:
				return EaseOutQuart;
			case Ease.EaseInOutQuart:
				return EaseInOutQuart;
			case Ease.EaseInQuint:
				return EaseInQuint;
			case Ease.EaseOutQuint:
				return EaseOutQuint;
			case Ease.EaseInOutQuint:
				return EaseInOutQuint;
			case Ease.EaseInSine:
				return EaseInSine;
			case Ease.EaseOutSine:
				return EaseOutSine;
			case Ease.EaseInOutSine:
				return EaseInOutSine;
			case Ease.EaseInExpo:
				return EaseInExpo;
			case Ease.EaseOutExpo:
				return EaseOutExpo;
			case Ease.EaseInOutExpo:
				return EaseInOutExpo;
			case Ease.EaseInCirc:
				return EaseInCirc;
			case Ease.EaseOutCirc:
				return EaseOutCirc;
			case Ease.EaseInOutCirc:
				return EaseInOutCirc;
			case Ease.Linear:
				return Linear;
			case Ease.Spring:
				return Spring;
			case Ease.EaseInBounce:
				return EaseInBounce;
			case Ease.EaseOutBounce:
				return EaseOutBounce;
			case Ease.EaseInOutBounce:
				return EaseInOutBounce;
			case Ease.EaseInBack:
				return EaseInBack;
			case Ease.EaseOutBack:
				return EaseOutBack;
			case Ease.EaseInOutBack:
				return EaseInOutBack;
			case Ease.EaseInElastic:
				return EaseInElastic;
			case Ease.EaseOutElastic:
				return EaseOutElastic;
			case Ease.EaseInOutElastic:
				return EaseInOutElastic;
			case Ease.Punch:
				return Punch;
			default:
				return null;
			}
		}

		public static Function GetEasingFunctionDerivative(Ease easingFunction)
		{
			switch (easingFunction)
			{
			case Ease.EaseInQuad:
				return EaseInQuadD;
			case Ease.EaseOutQuad:
				return EaseOutQuadD;
			case Ease.EaseInOutQuad:
				return EaseInOutQuadD;
			case Ease.EaseInCubic:
				return EaseInCubicD;
			case Ease.EaseOutCubic:
				return EaseOutCubicD;
			case Ease.EaseInOutCubic:
				return EaseInOutCubicD;
			case Ease.EaseInQuart:
				return EaseInQuartD;
			case Ease.EaseOutQuart:
				return EaseOutQuartD;
			case Ease.EaseInOutQuart:
				return EaseInOutQuartD;
			case Ease.EaseInQuint:
				return EaseInQuintD;
			case Ease.EaseOutQuint:
				return EaseOutQuintD;
			case Ease.EaseInOutQuint:
				return EaseInOutQuintD;
			case Ease.EaseInSine:
				return EaseInSineD;
			case Ease.EaseOutSine:
				return EaseOutSineD;
			case Ease.EaseInOutSine:
				return EaseInOutSineD;
			case Ease.EaseInExpo:
				return EaseInExpoD;
			case Ease.EaseOutExpo:
				return EaseOutExpoD;
			case Ease.EaseInOutExpo:
				return EaseInOutExpoD;
			case Ease.EaseInCirc:
				return EaseInCircD;
			case Ease.EaseOutCirc:
				return EaseOutCircD;
			case Ease.EaseInOutCirc:
				return EaseInOutCircD;
			case Ease.Linear:
				return LinearD;
			case Ease.Spring:
				return SpringD;
			case Ease.EaseInBounce:
				return EaseInBounceD;
			case Ease.EaseOutBounce:
				return EaseOutBounceD;
			case Ease.EaseInOutBounce:
				return EaseInOutBounceD;
			case Ease.EaseInBack:
				return EaseInBackD;
			case Ease.EaseOutBack:
				return EaseOutBackD;
			case Ease.EaseInOutBack:
				return EaseInOutBackD;
			case Ease.EaseInElastic:
				return EaseInElasticD;
			case Ease.EaseOutElastic:
				return EaseOutElasticD;
			case Ease.EaseInOutElastic:
				return EaseInOutElasticD;
			case Ease.Punch:
				return PunchD;
			default:
				return null;
			}
		}
	}
}
