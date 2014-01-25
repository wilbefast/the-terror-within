using UnityEngine;
using System.Collections;

public class Bloodthirsty : HeroPredisposition
{
	public override int ModifyFear(int fear)
	{
		return fear - GameObject.Find ("Dungeon").GetComponent<Dungeon>().currentRoomNumber;
	}
}
