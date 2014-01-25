using UnityEngine;
using System.Collections;

public class PrimaDonna : HeroPredisposition
{
	public override int ModifyFear(int fear)
	{
		return fear * 10;
	}
}
