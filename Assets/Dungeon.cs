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
			switch(value)
			{
			case State.FLEEING:
				score -= 100;
				if(score < 0)
					score = 0;
				break;
			}
			
			__state = value;
		}
	}
	
	void Update()
	{
		switch(state)
		{
			case State.ADVANCING:
				// move the monster
				if(currentRoomNumber < numberOfRooms)
					Monster.instance.transform.Translate(-4*Time.deltaTime, 0, 0);
				// move the background
				parallax.RotateAround(Vector3.up, 0.1f*Time.deltaTime);
				if(Monster.instance.transform.position.x <= 5.0f)
					state = State.DECISION;
				break;
			
			case State.FLEEING:
				// move the monster
				if(Monster.instance.transform.localPosition.x < 0)
					Monster.instance.transform.Translate(8*Time.deltaTime, 0, 0);
				// move the background
				parallax.RotateAround(Vector3.up, -0.2f*Time.deltaTime);
				break;
		}
	}
	
	#endregion states 
	
	#region dungeon progression 
	
	public static readonly int numberOfRooms = 10;
	
	public int currentRoomNumber;
	private float roomOffset = 0.0f;
	
	public float progress
	{
		get
		{
			return ((currentRoomNumber + roomOffset) / (float)numberOfRooms);
		}
	}
	
	public int score;
	private int irrationalFearBonus;
	
	void Start()
	{
		reset();
	}
	
	public void reset()
	{	
		// reset the score
		score = 0;
		
		// reset the state
		__state = State.ADVANCING;
		
		// reset progression
		RenderSettings.ambientLight = Color.white;
		currentRoomNumber = 1;
		roomOffset = 0.0f;
		
		// reset heroes
		foreach(var hero in GameObject.FindSceneObjectsOfType(typeof(Hero)))
				((Hero)hero).reset();
		
		// reset the monster
		Monster.instance.reset();
		

	}
	
	#endregion dungeon progression 
	
	#region user interface 
	
	void OnGUI()
	{
		// show decision buttons
		switch(__state)
		{
			case State.DECISION:
				if(GUI.Button(new Rect(400, 50, 100, 50), "Flee"))
				{
					// choose to run away
					StartCoroutine(__flight());
					break;
				}
				
				if(GUI.Button(new Rect(550, 50, 100, 50), "Fight"))
				{
					// choose to fight
					StartCoroutine(__combat());
					break;
				}
				break;
			
			case State.CELEBRATING:
				if(irrationalFearBonus > 0)
					GUI.Box(new Rect(300, 500, 300, 50),  "Immersion therapy bonus: +" + irrationalFearBonus);
				break;
			
			case State.FLEEING:
			case State.COMBAT:
				// show the strength of the party relative to the monster. +ve means the *party* is stronger.
				GUI.Box(new Rect(300, 500, 300, 50),  (-Monster.instance.strength).ToString("+#;-#;0"));
				break;
			
			case State.DEFEAT:
				if(GUI.Button(new Rect(550, 200, 100, 50), "LOSER! Try again?"))
					reset();
				break;
			
			case State.VICTORY:
				if(GUI.Button(new Rect(550, 200, 100, 50), "WINNER! Try again?"))
					reset();
				break;
		}
		
		GUI.Box(new Rect(20, 470, 300, 50), "room: " + currentRoomNumber);
		
		GUI.Box(new Rect(20, 20, 300, 50), "score: " + score);
	
		
	}
	
	#endregion user interface 
	
	#region advance 
	
	public Transform parallax;
	
	private static readonly float descendDuration = 3.0f; 
	
	private IEnumerator __descendIntoDarkness()
	{
		float remainingTime = descendDuration;
		
		while(remainingTime > 0)
		{
			remainingTime -= Time.deltaTime;
			
			roomOffset = 1 - remainingTime/descendDuration;
			RenderSettings.ambientLight = Color.Lerp(
				RenderSettings.ambientLight, 
				Color.Lerp(
					Color.white, 
					Color.black, 
					Dungeon.instance.progress*0.9f), 
				Time.deltaTime);
			
			yield return null;
		}
		
		// next level
		roomOffset = 0.0f;
		currentRoomNumber++;
		
		// ultimate victory ?
		if(currentRoomNumber > numberOfRooms)
			state = State.VICTORY;
	}
	
	#endregion advance  
	
	#region escape 

	private static readonly float fleeDuration = 1.5f;
	private static readonly float regroupDuration = 1.0f;
	
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
	
	private static readonly  float combatDuration = 2.0f;

	private static readonly  float celebrateDuration = 2.0f;
	
	public int totalFear
	{
		get
		{
			int total = 0;
			foreach(var hero in GameObject.FindSceneObjectsOfType(typeof(Hero)))
				total += ((Hero)hero).fear;
			
			return total;
		}
	}
	
	private IEnumerator __combat()
	{
		// start fighting
		state = State.COMBAT;
	
		// fight
		yield return new WaitForSeconds(combatDuration);
	
		// stop fighting
		if(Monster.instance.strength < 0)
		{
			// start celebrating this battle being won
			state = State.CELEBRATING;
		
			// add score
			score += 500;
			irrationalFearBonus = (int)Mathf.Floor((1.0f - (totalFear / (float)Monster.instance.strength)))*100;
			if(irrationalFearBonus > 0)
				score += irrationalFearBonus;
			else
				irrationalFearBonus = 0;
			
			// celebrate
			yield return new WaitForSeconds(combatDuration);

			// advance toward the next monster
			state = State.ADVANCING;
			Monster.instance.reset();
			
			// progress to next stage
			StartCoroutine(__descendIntoDarkness());
		}
		else if(Monster.instance.strength - numberOfRooms + currentRoomNumber < 0)
		{
			StartCoroutine(__flight());
		}
		else
		{
			// disaster !
			state = State.DEFEAT;
		}
	}
		
	#endregion combat 
}