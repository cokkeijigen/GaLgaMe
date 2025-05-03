using System;
using Arbor;

[Serializable]
public class FlexibleSliderAndTmpMaxTextVariable : FlexibleField<SliderAndTmpMaxTextVariable>
{
	public FlexibleSliderAndTmpMaxTextVariable(SliderAndTmpMaxTextVariable value)
		: base(value)
	{
	}

	public FlexibleSliderAndTmpMaxTextVariable(AnyParameterReference parameter)
		: base(parameter)
	{
	}

	public FlexibleSliderAndTmpMaxTextVariable(InputSlotAny slot)
		: base(slot)
	{
	}

	public static explicit operator SliderAndTmpMaxTextVariable(FlexibleSliderAndTmpMaxTextVariable flexible)
	{
		return flexible.value;
	}

	public static explicit operator FlexibleSliderAndTmpMaxTextVariable(SliderAndTmpMaxTextVariable value)
	{
		return new FlexibleSliderAndTmpMaxTextVariable(value);
	}
}
