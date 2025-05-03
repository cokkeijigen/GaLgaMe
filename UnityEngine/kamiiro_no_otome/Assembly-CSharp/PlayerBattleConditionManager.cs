using System.Collections.Generic;
using Sirenix.OdinInspector;

public class PlayerBattleConditionManager : SerializedBehaviour
{
	public class MemberIsDead
	{
		public int memberNum;

		public int memberID;

		public int memberAgility;

		public bool isDead;
	}

	public class MemberBuffCondition
	{
		public string type;

		public int power;

		public int continutyTurn;
	}

	public class MemberBadState
	{
		public string type;

		public int continutyTurn;
	}

	public class MemberSkillReChargeTurn
	{
		public int skillID;

		public int needRechargeTurn;

		public int maxRechargeTurn;
	}

	public static List<MemberIsDead> playerIsDead = new List<MemberIsDead>();

	public static List<List<MemberBuffCondition>> playerBuffCondition = new List<List<MemberBuffCondition>>();

	public static List<List<MemberBadState>> playerBadState = new List<List<MemberBadState>>();

	public static List<List<MemberSkillReChargeTurn>> playerSkillRechargeTurn = new List<List<MemberSkillReChargeTurn>>();

	public static List<MemberIsDead> enemyIsDead = new List<MemberIsDead>();

	public static List<List<MemberBuffCondition>> enemyBuffCondition = new List<List<MemberBuffCondition>>();

	public static List<List<MemberBadState>> enemyBadState = new List<List<MemberBadState>>();
}
