using System;
using Arbor;

[Serializable]
public class FlexibleUguiImage : FlexibleField<UguiImage>
{
	public FlexibleUguiImage(UguiImage value)
		: base(value)
	{
	}

	public FlexibleUguiImage(AnyParameterReference parameter)
		: base(parameter)
	{
	}

	public FlexibleUguiImage(InputSlotAny slot)
		: base(slot)
	{
	}

	public static explicit operator UguiImage(FlexibleUguiImage flexible)
	{
		return flexible.value;
	}

	public static explicit operator FlexibleUguiImage(UguiImage value)
	{
		return new FlexibleUguiImage(value);
	}
}
