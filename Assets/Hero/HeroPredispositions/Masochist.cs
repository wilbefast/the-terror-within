using UnityEngine;
using System.Collections;

public class Masochist : HeroPredisposition
{
	public override int ModifyFear(int fear)
	{
		return -fear;
	}
}
