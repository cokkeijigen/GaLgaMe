using System;
using Arbor;

[Serializable]
public class FlexibleUguiTextVariable : FlexibleField<UguiTextVariable>
{
	public FlexibleUguiTextVariable(UguiTextVariable value)
		: base(value)
	{
	}

	public FlexibleUguiTextVariable(AnyParameterReference parameter)
		: base(parameter)
	{
	}

	public FlexibleUguiTextVariable(InputSlotAny slot)
		: base(slot)
	{
	}

	public static explicit operator UguiTextVariable(FlexibleUguiTextVariable flexible)
	{
		return flexible.value;
	}

	public static explicit operator FlexibleUguiTextVariable(UguiTextVariable value)
	{
		return new FlexibleUguiTextVariable(value);
	}
}
