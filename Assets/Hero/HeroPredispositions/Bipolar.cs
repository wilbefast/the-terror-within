using UnityEngine;
using System.Collections;

public class Bipolar : HeroPredisposition
{
	bool manic;
	void Start()
	{
		manic = (Random.value >= 0.5);
	}
	public override int ModifyFear(int fear)
	{
		manic = !manic;
		return manic ? fear + 10 : fear - 10;
	}
}
