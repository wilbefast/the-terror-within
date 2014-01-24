using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterHead : MonoBehaviour 
{
	public static MonsterHead random()
	{
		var gameObject = new GameObject("Head", typeof(MonsterHead));
		
		return gameObject.GetComponent<MonsterHead>();
	}

}
