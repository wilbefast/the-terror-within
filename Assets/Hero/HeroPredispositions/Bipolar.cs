using UnityEngine;
using System.Collections;
using System;

public class Bipolar : HeroPredisposition
{
	int manic;
	private int mania = 25;
	void Start()
	{
		manic = UnityEngine.Random.Range(0,1);
	}
	public override int ModifyFear(int fear)
	{
		return fear + ((int)Math.Pow(-1,(Dungeon.instance.currentRoomNumber + manic)) * mania);
	}
}
