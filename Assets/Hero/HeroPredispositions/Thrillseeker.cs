using UnityEngine;
using System.Collections;
using System;

public class Thrillseeker : HeroPredisposition
{
	public override int ModifyFear(int fear)
	{
		return -Math.Abs(fear);
	}
}
