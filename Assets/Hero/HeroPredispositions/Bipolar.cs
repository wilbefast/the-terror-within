using UnityEngine;
using System.Collections;

public class Bipolar : HeroPredisposition
{
	bool manic;
	private int mania = 15;
	void Start()
	{
		manic = (Random.value >= 0.5);
	}
	public override int ModifyFear(int fear)
	{
		manic = !manic;
		return manic ? fear + mania : fear - mania;
	}
}
