using UnityEngine;
using System;
using System.Collections;

public class Hero : MonoBehaviour 
{
	public int combatAbility = 1;
	
	#region phobias and predispositions 
	
	[Range(0, 10)]
	public int numberOfPredispositions = 1;
	
	void Start()
	{
		// generate combat strength
		combatAbility = UnityEngine.Random.Range(1, 3);
		
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
			
			// take the party's strength into account
			foreach(var hero in GameObject.FindSceneObjectsOfType(typeof(Hero)))
				total -= ((Hero)hero).combatAbility;

			
			// take phobias into account
			foreach(MonsterQualifier phobia in phobias)
			{
				int exponent = 0;
				foreach(MonsterQualifier qualifier in monster.qualifiers)
				{
					if(phobia.GetType() == qualifier.GetType())
						exponent ++;
				}
				if(exponent > 0)
					total += (int)Math.Pow(10,exponent);
			}
			
			// take predisposition into account
			foreach(HeroPredisposition predisposition in predispositions)
				total = predisposition.ModifyFear(total);
			
			// done
			return total;
		}
	}
	
	#endregion phobias and predispositions 
	
	
#if UNITY_EDITOR
	
	private static readonly float minFear = -20.0f;
	private static readonly float maxFear = 100.0f;
	private static readonly int numberOfPortraits = 13;
	
	public bool showAttributes = false;
	
	void OnGUI()
	{
		Vector2 overhead = Camera.main.WorldToScreenPoint(transform.position);
		
		float normalisedFear = (Mathf.Clamp(fear, minFear, maxFear) - minFear) / (maxFear - minFear);
		
		var portraitTexture = (Texture)Resources.Load("Portrait" + ((int)(normalisedFear * (numberOfPortraits-1))).ToString("D2"));
		GUI.DrawTexture(new Rect(overhead.x - 25, overhead.y, 100, 100), portraitTexture);
		
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
