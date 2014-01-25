using UnityEngine;
using System.Collections;

public class Optimistic : HeroPredisposition 
{
	public override int ModifyFear (int fear)
	{
		return fear - 25;
	}
}