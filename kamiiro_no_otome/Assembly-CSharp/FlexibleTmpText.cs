using System;
using Arbor;

[Serializable]
public class FlexibleTmpText : FlexibleField<TmpText>
{
	public FlexibleTmpText(TmpText value)
		: base(value)
	{
	}

	public FlexibleTmpText(AnyParameterReference parameter)
		: base(parameter)
	{
	}

	public FlexibleTmpText(InputSlotAny slot)
		: base(slot)
	{
	}

	public static explicit operator TmpText(FlexibleTmpText flexible)
	{
		return flexible.value;
	}

	public static explicit operator FlexibleTmpText(TmpText value)
	{
		return new FlexibleTmpText(value);
	}
}
