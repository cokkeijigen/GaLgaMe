using System;
using Arbor;

[Serializable]
public class FlexibleSliderAndTmpText : FlexibleField<SliderAndTmpText>
{
	public FlexibleSliderAndTmpText(SliderAndTmpText value)
		: base(value)
	{
	}

	public FlexibleSliderAndTmpText(AnyParameterReference parameter)
		: base(parameter)
	{
	}

	public FlexibleSliderAndTmpText(InputSlotAny slot)
		: base(slot)
	{
	}

	public static explicit operator SliderAndTmpText(FlexibleSliderAndTmpText flexible)
	{
		return flexible.value;
	}

	public static explicit operator FlexibleSliderAndTmpText(SliderAndTmpText value)
	{
		return new FlexibleSliderAndTmpText(value);
	}
}
