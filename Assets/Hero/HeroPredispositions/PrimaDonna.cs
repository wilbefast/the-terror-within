using UnityEngine;
using System.Collections;

public class PrimaDonna : HeroPredisposition
{
	public override int ModifyFear(int fear)
	{
		if(fear>0)
			return 25;
		else if(fear<0)
			return -25;
		else return 0;
	}
}
