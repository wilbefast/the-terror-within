using UnityEngine;
using System;
using System.Collections;

public class Hero : MonoBehaviour 
{	
	#region phobias and predispositions 
	
	public void reset()
	{
		// remove all sub-objects
		foreach(Transform child in transform)
			Destroy(child.gameObject);

		// generate phobia(s)
		var phobias = new GameObject("Phobias");
		phobias.transform.parent = transform;
		phobias.AddComponent(MonsterQualifier.random());
		
		// generate predisposition(s)
		var predispositions = new GameObject("Predispositions");
		predispositions.transform.parent = transform;
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
			foreach(MonsterQualifier phobia in phobias)
			{
				int exponent = 0;
				foreach(MonsterQualifier qualifier in monster.qualifiers)
				{
					if(phobia.GetType() == qualifier.GetType())
						exponent ++;
				}
				if(exponent > 0)
					total += (6 + (int)Math.Pow(4, exponent));
			}
			
			
			// take predisposition into account
			foreach(HeroPredisposition predisposition in predispositions)
				total = predisposition.ModifyFear(total);
			
			// done
			return total;
		}
	}
	
	#endregion phobias and predispositions 
	
	
	private static readonly float minFear = -25.0f;
	private static readonly float maxFear = 25.0f;
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
				normalisedFear *= normalisedFear;
				portraitTexture = (Texture)Resources.Load("Portrait" + ((int)(normalisedFear * (numberOfPortraits-1))).ToString("D2"));
				break;
			
			case Dungeon.State.ADVANCING:
				portraitTexture = (Texture)Resources.Load("PortraitAdvancing");
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
			foreach(var phobia in phobias)
				GUI.Box(new Rect(portraitPosition.x, portraitPosition.y + 100, 150, 50), phobia.ToString());
			
			foreach(var predisposition in predispositions)
				GUI.Box(new Rect(portraitPosition.x, portraitPosition.y + 100, 250, 50), predisposition.ToString());
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
