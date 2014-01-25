using UnityEngine;
using System.Collections;

public class Pessimistic : HeroPredisposition
{
	public override int ModifyFear (int fear)
	{
		 return fear + 5;
	}
}
