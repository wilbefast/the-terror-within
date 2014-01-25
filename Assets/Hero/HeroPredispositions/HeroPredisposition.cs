using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class HeroPredisposition : MonoBehaviour
{
	public abstract int ModifyFear(int fear);
	
	#region generate random predisposition 
	
	public static List<System.Type> types = new List<System.Type>();
	
	static HeroPredisposition()
	{
		Func<System.Type, bool> isHeroPredisposition =
			t => (t != typeof(HeroPredisposition) && typeof(HeroPredisposition).IsAssignableFrom(t));
		
		foreach(var t in typeof(HeroPredisposition).Assembly.GetTypes().Where(isHeroPredisposition))
			types.Add(t);
	}
	
	public static System.Type random()
	{
		return types[UnityEngine.Random.Range(0, types.Count)];
	}
	
	#endregion generate random predisposition 
}
