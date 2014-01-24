using UnityEngine;
using System.Collections;

public class Stoic : HeroPredisposition
{
	public override int ModifyFear (int fear)
	{
		return (int)((float)fear / 5.0f);
	}	
}
