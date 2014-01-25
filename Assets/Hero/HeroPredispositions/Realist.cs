using UnityEngine;
using System.Collections;

public class Realist : HeroPredisposition
{
	public override int ModifyFear (int fear)
	{
		return fear;
	}	
}
