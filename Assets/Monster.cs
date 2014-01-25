using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
	public int strength;
	
	void Start()
	{
		// add body-parts root
		var bodyParts = new GameObject("BodyParts");
		bodyParts.transform.parent = transform;
		
		
		// add head
		var head = new GameObject("Head");
		head.transform.parent = bodyParts.transform;
		head.AddComponent(MonsterQualifier.randomExcluding(qualifiers));
		
		// add torso
		var torso = new GameObject("Torso");
		torso.transform.parent = bodyParts.transform;
		torso.AddComponent(MonsterQualifier.randomExcluding(qualifiers));
		
		// add arms
		var arms = new GameObject("Arms");
		arms.transform.parent = bodyParts.transform;
		arms.AddComponent(MonsterQualifier.randomExcluding(qualifiers));
		
		// add legs
		var legs = new GameObject("Legs");
		legs.transform.parent = bodyParts.transform;
		legs.AddComponent(MonsterQualifier.randomExcluding(qualifiers));
		
		// add tail
		var tail = new GameObject("Tail");
		tail.transform.parent = bodyParts.transform;
		tail.AddComponent(MonsterQualifier.randomExcluding(qualifiers));
		
		// set monster strength
		strength =  UnityEngine.Random.Range(5,25);
	}
	
	public IEnumerable qualifiers
	{
		get
		{
			return gameObject.GetComponentsInChildren<MonsterQualifier>();
		}
	}
	
#if UNITY_EDITOR
	
	void OnGUI()
	{
		int y = 50;
		foreach(var qualifier in qualifiers)
		{
			GUI.Box(new Rect(900, y, 150, 50), qualifier.ToString());
			y += 75;
		}
	}
	
#endif // UNITY_EDITOR
}
