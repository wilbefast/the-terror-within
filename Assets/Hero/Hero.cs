using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour 
{
	
	[Range(1, 5)]
	public int combatAbility = 1;
	
	#region phobias and predispositions 
	
	
	[Range(0, 10)]
	public int numberOfPredispositions = 1;
	
	void Start()
	{
		// generate combat strength
		combatAbility = Random.Range(1, 3);
		
		// generate phobia(s)
		var phobias = new GameObject("Phobias");
		phobias.transform.parent = transform;
		
		foreach(System.Type qualifier in MonsterQualifier.types)
		{
			if(UnityEngine.Random.Range(0,1.0f) < 0.2f)
				phobias.AddComponent(qualifier);
		}
		
		// generate predisposition(s)
		var predispositions = new GameObject("Predispositions");
		predispositions.transform.parent = transform;
		for(int i = 0; i < numberOfPredispositions; i++)
			predispositions.AddComponent(HeroPredisposition.random());
	}
	
	
	public IEnumerable phobias
	{
		get
		{
			return gameObject.GetComponentsInChildren<MonsterQualifier>();
		}
	}
	
	public IEnumerable predispositions
	{
		get
		{
			return gameObject.GetComponentsInChildren<HeroPredisposition>();
		}
	}
	
	public int fear
	{
		get
		{
			Monster monster = (Monster)GameObject.FindObjectOfType(typeof(Monster));
			
			// base fear's initial value on the monster's actual strength
			int total = monster.strength;
			foreach(var hero in GameObject.FindSceneObjectsOfType(typeof(Hero)))
				total -= ((Hero)hero).combatAbility;

			
			// take phobias into account
			foreach(MonsterQualifier qualifier in monster.qualifiers)
				foreach(MonsterQualifier phobia in phobias)
					if(phobia.GetType() == qualifier.GetType())
						total += 10;
			
			// take predisposition into account
			foreach(HeroPredisposition predisposition in predispositions)
				total = predisposition.ModifyFear(total);
			
			// done
			return total;
		}
	}
	
	#endregion phobias and predispositions 
	
	
#if UNITY_EDITOR
	
	public bool showAttributes = false;
	
	void OnGUI()
	{
		Vector2 overhead = Camera.main.WorldToScreenPoint(transform.position - Vector3.up);
		
		GUI.Box(new Rect(overhead.x - 25, overhead.y, 50, 50), fear.ToString());
	
		
		if(!showAttributes)
			return;
		
		int y = 125;
		
		y += 75;
		foreach(var phobia in phobias)
		{
			GUI.Box(new Rect(50, y, 150, 50), phobia.ToString());
			y += 75;
		}
		
		foreach(var predisposition in predispositions)
		{
			GUI.Box(new Rect(50, y, 250, 50), predisposition.ToString());
			y += 75;
		}
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(transform.position, 0.5f);
	}
	
#endif // UNITY_EDITOR
}
