using UnityEngine;
using System.Collections;

public class CaptainHindsight : HeroPredisposition
{
	private int lastFear = 0;
	public override int ModifyFear(int fear)
	{
		int tempFear = lastFear;
		lastFear = fear;
		return tempFear;
	}
}
