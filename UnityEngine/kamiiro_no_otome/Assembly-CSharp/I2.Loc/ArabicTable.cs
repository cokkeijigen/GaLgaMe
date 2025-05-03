using System.Collections.Generic;

namespace I2.Loc
{
	internal class ArabicTable
	{
		private static List<ArabicMapping> mapList;

		private static ArabicTable arabicMapper;

		internal static ArabicTable ArabicMapper
		{
			get
			{
				if (arabicMapper == null)
				{
					arabicMapper = new ArabicTable();
				}
				return arabicMapper;
			}
		}

		private ArabicTable()
		{
			mapList = new List<ArabicMapping>();
			mapList.Add(new ArabicMapping(1569, 65152));
			mapList.Add(new ArabicMapping(1575, 65165));
			mapList.Add(new ArabicMapping(1571, 65155));
			mapList.Add(new ArabicMapping(1572, 65157));
			mapList.Add(new ArabicMapping(1573, 65159));
			mapList.Add(new ArabicMapping(1609, 64508));
			mapList.Add(new ArabicMapping(1574, 65161));
			mapList.Add(new ArabicMapping(1576, 65167));
			mapList.Add(new ArabicMapping(1578, 65173));
			mapList.Add(new ArabicMapping(1579, 65177));
			mapList.Add(new ArabicMapping(1580, 65181));
			mapList.Add(new ArabicMapping(1581, 65185));
			mapList.Add(new ArabicMapping(1582, 65189));
			mapList.Add(new ArabicMapping(1583, 65193));
			mapList.Add(new ArabicMapping(1584, 65195));
			mapList.Add(new ArabicMapping(1585, 65197));
			mapList.Add(new ArabicMapping(1586, 65199));
			mapList.Add(new ArabicMapping(1587, 65201));
			mapList.Add(new ArabicMapping(1588, 65205));
			mapList.Add(new ArabicMapping(1589, 65209));
			mapList.Add(new ArabicMapping(1590, 65213));
			mapList.Add(new ArabicMapping(1591, 65217));
			mapList.Add(new ArabicMapping(1592, 65221));
			mapList.Add(new ArabicMapping(1593, 65225));
			mapList.Add(new ArabicMapping(1594, 65229));
			mapList.Add(new ArabicMapping(1601, 65233));
			mapList.Add(new ArabicMapping(1602, 65237));
			mapList.Add(new ArabicMapping(1603, 65241));
			mapList.Add(new ArabicMapping(1604, 65245));
			mapList.Add(new ArabicMapping(1605, 65249));
			mapList.Add(new ArabicMapping(1606, 65253));
			mapList.Add(new ArabicMapping(1607, 65257));
			mapList.Add(new ArabicMapping(1608, 65261));
			mapList.Add(new ArabicMapping(1610, 65265));
			mapList.Add(new ArabicMapping(1570, 65153));
			mapList.Add(new ArabicMapping(1577, 65171));
			mapList.Add(new ArabicMapping(1662, 64342));
			mapList.Add(new ArabicMapping(1670, 64378));
			mapList.Add(new ArabicMapping(1688, 64394));
			mapList.Add(new ArabicMapping(1711, 64402));
			mapList.Add(new ArabicMapping(1705, 64398));
		}

		internal int Convert(int toBeConverted)
		{
			foreach (ArabicMapping map in mapList)
			{
				if (map.from == toBeConverted)
				{
					return map.to;
				}
			}
			return toBeConverted;
		}
	}
}
