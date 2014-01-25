using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour 
{
	
	public int combatAbility = 3;
	
	#region phobias and predispositions 
	
	
	[Range(0, 10)]
	public int numberOfPredispositions = 1;
	
	void Start()
	{
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
			
			// take phobias into account
			foreach(MonsterQualifier qualifier in monster.qualifiers)
				foreach(MonsterQualifier phobia in phobias)
					if(phobia.GetType() == qualifier.GetType())
						total += 1;
			
			// take predisposition into account
			foreach(HeroPredisposition predisposition in predispositions)
				total = predisposition.ModifyFear(total);
			
			// done
			return total;
		}
	}
	
	#endregion phobias and predispositions 
	
	
#if UNITY_EDITOR
	
	void OnGUI()
	{
		int y = 50;
		GUI.Box(new Rect(50, 50, 150, 50), "fear = " + fear);
		
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
	
#endif // UNITY_EDITOR
}
