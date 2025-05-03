using System;
using Arbor;

[Serializable]
public class FlexibleContainerSkillData : FlexibleField<ContainerSkillData>
{
	public FlexibleContainerSkillData(ContainerSkillData value)
		: base(value)
	{
	}

	public FlexibleContainerSkillData(AnyParameterReference parameter)
		: base(parameter)
	{
	}

	public FlexibleContainerSkillData(InputSlotAny slot)
		: base(slot)
	{
	}

	public static explicit operator ContainerSkillData(FlexibleContainerSkillData flexible)
	{
		return flexible.value;
	}

	public static explicit operator FlexibleContainerSkillData(ContainerSkillData value)
	{
		return new FlexibleContainerSkillData(value);
	}
}
