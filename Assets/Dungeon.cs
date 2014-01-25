using UnityEngine;
using System.Collections;

public class Dungeon : MonoBehaviour
{
	#region singleton 
	
	public static Dungeon instance
	{
		get
		{
			return (Dungeon)(GameObject.FindSceneObjectsOfType(typeof(Dungeon))[0]);
		}
	}
	
	#endregion singleton 
	
	#region states 
	
	public enum State
	{
		ADVANCING,
		DECISION,
		COMBAT,
		FLEEING,
		REGROUPING,
		CELEBRATING,
		DEFEAT,
		VICTORY
	}
	
	public State __state = State.ADVANCING;
	
	public State state
	{
		get
		{
			return __state;
		}
		
		private set
		{
			__state = value;
		}
	}
	
	void Update()
	{
		switch(state)
		{
			case State.ADVANCING:
				// move the monster
				Monster.instance.transform.Translate(-4*Time.deltaTime, 0, 0);
				if(Monster.instance.transform.position.x <= 5.0f)
					state = State.DECISION;
				break;
			
			case State.FLEEING:
				// move the monster
				if(Monster.instance.transform.localPosition.x < 0)
					Monster.instance.transform.Translate(8*Time.deltaTime, 0, 0);
				break;
		}
	}
	
	#endregion states 
	
	#region dungeon progression 
	
	static int numberOfRooms = 15;
	
	public int currentRoomNumber = 1;
	
	public int combinedHeroStrength
	{
		get
		{
			int totalCombatAbility = 0;
			foreach(var hero in GameObject.FindSceneObjectsOfType(typeof(Hero)))
				totalCombatAbility += ((Hero)hero).combatAbility;
			return totalCombatAbility;
		}
	}
	
	public void reset()
	{	
		// reset the state
		__state = State.ADVANCING;
		
		// reset progression
		currentRoomNumber = 1;
		
		// reset heroes
		foreach(var hero in GameObject.FindSceneObjectsOfType(typeof(Hero)))
				((Hero)hero).reset();
		
		// reset the monster
		Monster.instance.reset();
		

	}
	
	#endregion dungeon progression 
	
	#region user interface 
	
	public GUIText resultText;
	
	[RangeAttribute(1.0f, 5.0f)]
	public float textDuration = 3.0f;
	
	void OnGUI()
	{
		// show decision buttons
		switch(__state)
		{
			case State.DECISION:
				if(GUI.Button(new Rect(400, 50, 100, 50), "Flee"))
					// choose to run away
					StartCoroutine(__flight());
				
				if(GUI.Button(new Rect(550, 50, 100, 50), "Fight"))
					// choose to fight
					StartCoroutine(__combat());
				break;
			
			case State.DEFEAT:
				if(GUI.Button(new Rect(550, 100, 100, 50), "LOSER! Try again?"))
					reset();
				break;
			
			case State.VICTORY:
				if(GUI.Button(new Rect(550, 100, 100, 50), "WINNER! Try again?"))
					reset();
				break;
		}
		
	
		//GUI.Box(new Rect(200, 400, 300, 50), "party strength: " + GetPartyCombatStrength());
	}
	
	#endregion user interface 
	
	#region escape 
	
	[Range(0.0f, 5.0f)]
	public float fleeDuration = 4.0f;
	
	[Range(0.0f, 5.0f)]
	public float regroupDuration = 1.0f;
	
	private IEnumerator __flight()
	{
		// start to flee
		state = State.FLEEING;
		
		// flee
		yield return new WaitForSeconds(fleeDuration);
		
		// start to regroup
		state = State.REGROUPING;
		
		// regroup
		yield return new WaitForSeconds(regroupDuration);
		
		// move out towards a new monster
		Monster.instance.reset();
		state = State.ADVANCING;
	}
	
	#endregion escape 
	
	#region combat 
	
	[Range(0.0f, 5.0f)]
	public float combatDuration = 2.0f;

	[Range(0.0f, 5.0f)]
	public float celebrateDuration = 2.0f;
	
	private IEnumerator __combat()
	{
		// start fighting
		state = State.COMBAT;
	
		// fight
		yield return new WaitForSeconds(combatDuration);
	
		// stop fighting
		if(combinedHeroStrength >= Monster.instance.strength)
		{
			// defeat the monster: progress to next stage
			currentRoomNumber++;
			
			// gain experience 			
			foreach(var hero in GameObject.FindSceneObjectsOfType(typeof(Hero)))
				((Hero)hero).combatAbility ++;
			
			
			// ultimate victory ?
			if(currentRoomNumber > numberOfRooms)
				// start celebrating the war being won!
				state = State.VICTORY;
			else
			{
				// start celebrating this battle being won
				state = State.CELEBRATING;
			
				// celebrate
				yield return new WaitForSeconds(combatDuration);
	
				// advance toward the next monster
				state = State.ADVANCING;
				Monster.instance.reset();
			}
		}
		else
		{
			// disaster !
			state = State.DEFEAT;
		}
	}
		
	#endregion combat 
}