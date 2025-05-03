using System;
using Arbor;

[Serializable]
public class FlexibleI2LocalizeComponent : FlexibleField<I2LocalizeComponent>
{
	public FlexibleI2LocalizeComponent(I2LocalizeComponent value)
		: base(value)
	{
	}

	public FlexibleI2LocalizeComponent(AnyParameterReference parameter)
		: base(parameter)
	{
	}

	public FlexibleI2LocalizeComponent(InputSlotAny slot)
		: base(slot)
	{
	}

	public static explicit operator I2LocalizeComponent(FlexibleI2LocalizeComponent flexible)
	{
		return flexible.value;
	}

	public static explicit operator FlexibleI2LocalizeComponent(I2LocalizeComponent value)
	{
		return new FlexibleI2LocalizeComponent(value);
	}
}
