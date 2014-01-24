using UnityEngine;
using System.Collections;

public class MonsterLegs : MonoBehaviour 
{
	public static MonsterLegs random()
	{
		var gameObject = new GameObject("Legs", typeof(MonsterLegs));
		
		return gameObject.GetComponent<MonsterLegs>();
	}
}
