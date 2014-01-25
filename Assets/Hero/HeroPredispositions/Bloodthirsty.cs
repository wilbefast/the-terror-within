using UnityEngine;
using System.Collections;

public class Bloodthirsty : HeroPredisposition
{
	public override int ModifyFear(int fear)
	{
		return (fear - (int)(50 * Dungeon.instance.progress));
	}
}
