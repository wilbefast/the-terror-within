using UnityEngine;
using System;
using System.Collections;

public class Hero : MonoBehaviour 
{
	public int combatAbility = 1;
		
	void Start()
	{
		reset();
	}
	
	#region phobias and predispositions 
	
	[Range(0, 10)]
	public int numberOfPredispositions = 1;
	
	public void reset()
	{
		// generate combat strength
		combatAbility = UnityEngine.Random.Range(1, 3);
		
		// generate phobia(s)
		var phobias = new GameObject("Phobias");
		phobias.transform.parent = transform;
		
		foreach(System.Type qualifier in MonsterQualifier.types)
		{
			if(UnityEngine.Random.Range(0, 1.0f) < 0.2f)
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
	
	
	private static readonly float minFear = -20.0f;
	private static readonly float maxFear = 100.0f;
	private static readonly int numberOfPortraits = 13;
	
#if UNITY_EDITOR	
	public bool showAttributes = false;
#endif // UNITY_EDITOR
	
	void OnGUI()
	{
		// choose where to draw portrait
		Vector2 portraitPosition = Camera.main.WorldToScreenPoint(transform.position);
		
		// choose which portrait to draw
		Texture portraitTexture;
		switch(Dungeon.instance.state)
		{
			case Dungeon.State.DECISION:
			case Dungeon.State.COMBAT:
				float normalisedFear = (Mathf.Clamp(fear, minFear, maxFear) - minFear) / (maxFear - minFear);
				portraitTexture = (Texture)Resources.Load("Portrait" + ((int)(normalisedFear * (numberOfPortraits-1))).ToString("D2"));
				break;
			
			case Dungeon.State.DEFEAT:
				portraitTexture = (Texture)Resources.Load("PortraitDead");
				break;
			
			case Dungeon.State.CELEBRATING:
				portraitTexture = (Texture)Resources.Load("PortraitCelebrate");
				break;
			
			case Dungeon.State.FLEEING:
				portraitTexture = (Texture)Resources.Load("PortraitFlee");
				break;
			
			case Dungeon.State.VICTORY:
				portraitTexture = (Texture)Resources.Load("PortraitVictory");
				break;
			
			default:
				portraitTexture = (Texture)Resources.Load("Portrait02");
				break;	
		}
		
		// draw it
		GUI.DrawTexture(new Rect(portraitPosition.x - 25, portraitPosition.y, 100, 100), portraitTexture);
	
		/*
		 * Debug stuff
		 */
		
#if UNITY_EDITOR
		if(showAttributes)
		{
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
#endif // UNITY_EDITOR
	}
	
#if UNITY_EDITOR
	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(transform.position, 0.5f);
	}
#endif // UNITY_EDITOR

}
