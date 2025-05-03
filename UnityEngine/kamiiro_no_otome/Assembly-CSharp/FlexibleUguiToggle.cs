using System;
using Arbor;

[Serializable]
public class FlexibleUguiToggle : FlexibleField<UguiToggle>
{
	public FlexibleUguiToggle(UguiToggle value)
		: base(value)
	{
	}

	public FlexibleUguiToggle(AnyParameterReference parameter)
		: base(parameter)
	{
	}

	public FlexibleUguiToggle(InputSlotAny slot)
		: base(slot)
	{
	}

	public static explicit operator UguiToggle(FlexibleUguiToggle flexible)
	{
		return flexible.value;
	}

	public static explicit operator FlexibleUguiToggle(UguiToggle value)
	{
		return new FlexibleUguiToggle(value);
	}
}
