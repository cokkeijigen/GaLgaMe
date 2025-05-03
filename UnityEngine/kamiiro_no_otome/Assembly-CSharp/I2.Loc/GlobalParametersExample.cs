namespace I2.Loc
{
	public class GlobalParametersExample : RegisterGlobalParameters
	{
		public override string GetParameterValue(string ParamName)
		{
			if (ParamName == "WINNER")
			{
				return "Javier";
			}
			if (ParamName == "NUM PLAYERS")
			{
				return 5.ToString();
			}
			return null;
		}
	}
}
