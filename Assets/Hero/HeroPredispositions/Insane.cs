using UnityEngine;
using System.Collections;

public class Insane : HeroPredisposition
{
	public override int ModifyFear(int fear)
	{
		return -fear;
	}
}
