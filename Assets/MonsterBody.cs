using UnityEngine;
using System.Collections;

public class MonsterBody : MonoBehaviour 
{
	public static MonsterBody random()
	{
		var gameObject = new GameObject("Body", typeof(MonsterHead));
		
		return gameObject.GetComponent<MonsterBody>();
	}
}
