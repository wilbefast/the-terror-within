using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
	#region singleton 
	
	public static Monster instance
	{
		get
		{
			return (Monster)(GameObject.FindSceneObjectsOfType(typeof(Monster))[0]);
		}
	}
	
	#endregion singleton 
	
	
	public int strength;
	
	public void reset()
	{
		// remove all sub-objects
		foreach(Transform child in transform)
			DestroyImmediate(child.gameObject);
		
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
		
		// set monster strength
		strength =  UnityEngine.Random.Range(5, 25);
		strength +=  5 * Dungeon.instance.currentRoomNumber;
	}
	
	void Start()
	{
		reset ();
	}
	
	public IEnumerable qualifiers
	{
		get
		{
			return gameObject.GetComponentsInChildren<MonsterQualifier>();
		}
	}
	
#if UNITY_EDITOR
	
	public bool showAttributes = false;
	
	void OnGUI()
	{
		int y = 50;
		
		if(showAttributes)
		{
			GUI.Box (new Rect(900,y,150,50), "Strength = " + strength);
			y+=75;
		}
		
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
