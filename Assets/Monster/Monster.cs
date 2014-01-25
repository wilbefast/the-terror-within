using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
	void Start()
	{
		// add body-parts root
		var bodyParts = new GameObject("BodyParts");
		bodyParts.transform.parent = transform;
		
		// add head
		var head = (GameObject)Instantiate(Resources.Load("Head"));
		head.transform.position += transform.position;
		head.transform.parent = bodyParts.transform;
		head.AddComponent(MonsterQualifier.randomExcluding(qualifiers));
		
		// add torso
		var torso = (GameObject)Instantiate(Resources.Load("Torso"));
		torso.transform.position += transform.position;
		torso.transform.parent = bodyParts.transform;
		torso.AddComponent(MonsterQualifier.randomExcluding(qualifiers));
		
		// add arms
		var arms = (GameObject)Instantiate(Resources.Load("Arms"));
		arms.transform.position += transform.position;
		arms.transform.parent = bodyParts.transform;
		arms.AddComponent(MonsterQualifier.randomExcluding(qualifiers));
		
		// add legs
		var legs = (GameObject)Instantiate(Resources.Load("Legs"));
		legs.transform.position += transform.position;
		legs.transform.parent = bodyParts.transform;
		legs.AddComponent(MonsterQualifier.randomExcluding(qualifiers));
		
		// add tail
		var tail = (GameObject)Instantiate(Resources.Load("Tail"));
		tail.transform.position += transform.position;
		tail.transform.parent = bodyParts.transform;
		tail.AddComponent(MonsterQualifier.randomExcluding(qualifiers));
		
	}
	
	public int strength
	{
		get
		{
			return 0; 
		}
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
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, 0.5f);
	}
	
#endif // UNITY_EDITOR
}
