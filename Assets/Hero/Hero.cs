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
		
		// create portrait
		var portrait = (GameObject)Instantiate(Resources.Load("PortraitPrefab"));
		portrait.GetComponent<HeroPortrait>().hero = this;
		portrait.transform.parent = transform;
		portrait.transform.localPosition = Vector3.zero;

		// generate phobia(s)
		var phobias = new GameObject("Phobias");
		phobias.transform.parent = transform;
		phobias.AddComponent(MonsterQualifier.random());
		phobias.transform.localPosition = Vector3.zero;
		
		// generate predisposition(s)
		var predispositions = new GameObject("Predispositions");
		predispositions.transform.parent = transform;
		predispositions.AddComponent(HeroPredisposition.random());
		predispositions.transform.localPosition = Vector3.zero;
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
	
	
#if UNITY_EDITOR
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(transform.position, 0.5f);
	}
	
#endif // UNITY_EDITOR
}
