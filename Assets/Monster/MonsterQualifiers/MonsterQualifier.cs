using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public abstract class MonsterQualifier : MonoBehaviour 
{
	#region abstract 
	
	public abstract string qualifierName
	{
		get;
	}
	
	#endregion abstract 
	
	
	#region generate random predisposition 
	
	public static List<System.Type> types = new List<System.Type>();
	
	static MonsterQualifier()
	{
		Func<System.Type, bool> isMonsterQualifier =
			t => (t != typeof(MonsterQualifier) && typeof(MonsterQualifier).IsAssignableFrom(t));
		
		foreach(var t in typeof(MonsterQualifier).Assembly.GetTypes().Where(isMonsterQualifier))
			types.Add(t);
	}
	
	public static System.Type random()
	{
		return types[UnityEngine.Random.Range(0, types.Count)];
	}
	
	public static System.Type randomExcluding(IEnumerable enumerable)
	{
		var excluding = types.Where (qualifierClass =>
		{
			foreach(var qualifierInstance in enumerable)
				if (qualifierInstance.GetType() == qualifierClass) 
					return false;
			return true;
		});
		
		if(!excluding.Any())
		{
			// should never happen lolz
			Debug.LogError("No MonsterQualifier types left to choose from");
			return null;
		}
			
		int randomIndex = UnityEngine.Random.Range(0, excluding.Count()), index = 0;
		foreach(var randomType in excluding)
		{
			if(index == randomIndex)
				return randomType;
			index++;
		}
		
		// should NEVER happen roflz
		Debug.LogError("Selected index " + randomIndex + " is larger than type list of size " + excluding.Count());
		return null;
	}
	
	#endregion generate random predisposition 
}
