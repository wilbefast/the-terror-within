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
		
		// move back to parent position
		transform.localPosition = Vector3.zero;
		
		// add body-parts root
		var bodyParts = new GameObject("BodyParts");
		bodyParts.transform.parent = transform;
		
		// add head
		var head = (GameObject)Instantiate(Resources.Load("Head"));
		head.transform.position += transform.position;
		head.transform.parent = bodyParts.transform;
		head.AddComponent(MonsterQualifier.random());
		
		// add torso
		var torso = (GameObject)Instantiate(Resources.Load("Torso"));
		torso.transform.position += transform.position;
		torso.transform.parent = bodyParts.transform;
		
		// add arms
		var arms = (GameObject)Instantiate(Resources.Load("Arms"));
		arms.transform.position += transform.position;
		arms.transform.parent = bodyParts.transform;
		arms.AddComponent(MonsterQualifier.random());
		
		// add legs
		var legs = (GameObject)Instantiate(Resources.Load("Legs"));
		legs.transform.position += transform.position;
		legs.transform.parent = bodyParts.transform;
		legs.AddComponent(MonsterQualifier.random());
		
		// add tail
		var tail = (GameObject)Instantiate(Resources.Load("Tail"));
		tail.transform.position += transform.position;
		tail.transform.parent = bodyParts.transform;
		tail.AddComponent(MonsterQualifier.random());
		
		// set relative strength between monster and player. A -ve number means the monster is stronger.
		strength =  UnityEngine.Random.Range(-(Dungeon.numberOfRooms-Dungeon.instance.currentRoomNumber + 3), (Dungeon.numberOfRooms-Dungeon.instance.currentRoomNumber + 3));
	}
	
	public IEnumerable qualifiers
	{
		get
		{
			return gameObject.GetComponentsInChildren<MonsterQualifier>();
		}
	}
	
#if UNITY_EDITOR
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, 0.5f);
	}
	
#endif // UNITY_EDITOR
}
