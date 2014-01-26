using UnityEngine;
using System.Collections;
using System;

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
			if(value == State.DEFEAT)
				Debug.Log ("Defeat?!");
			__state = value;
		}
	}
	
	void Update()
	{
		if(Input.GetKey(KeyCode.Escape))
			Application.Quit();
		
		switch(state)
		{
			case State.ADVANCING:
				// move the monster
				if(currentRoomNumber <= numberOfRooms)
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
	private static readonly int startingStamina = 20;
		
	public int currentRoomNumber;
	private int currentStamina;
	private float roomOffset = 0.0f;
	
	public float progress
	{
		get
		{
			return ((currentRoomNumber + roomOffset) / (float)numberOfRooms);
		}
	}
	
	private int irrationalFearBonus;
	
	void Start()
	{
		reset();
	}
	
	public void reset()
	{	
		// reset the party's stamina
		currentStamina = startingStamina;
		
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
	public Texture heart, halfHeart, emptyHeart;
	public Texture fightButton, fightButtonHover, fightButtonPressed, fleeButton, fleeButtonHover, fleeButtonPressed;
	private GUIStyle buttonStyle = new GUIStyle();
	private Rect fightButtonCollider = new Rect(320, 300, 200, 100);
	private Rect fleeButtonCollider = new Rect(120, 300, 200, 100);
	
	void OnGUI()
	{
		// show decision buttons
		switch(__state)
		{
			case State.DECISION:
				// choose to run away
				if(fleeButtonCollider.Contains(Event.current.mousePosition) && Input.GetMouseButton(0))
				{
					if(GUI.Button(fleeButtonCollider, fleeButtonPressed, buttonStyle))
					{
						StartCoroutine(__flight());
						break;
					}
				}
  				else if(fleeButtonCollider.Contains(Event.current.mousePosition))
				{
					if(GUI.Button(fleeButtonCollider, fleeButtonHover, buttonStyle))
					{
						StartCoroutine(__flight());
						break;
					}
				}
				else
				{
					if(GUI.Button(fleeButtonCollider, fleeButton, buttonStyle))
					{
						StartCoroutine(__flight());
						break;
					}
				}
			
				// choose to fight
				if(fightButtonCollider.Contains(Event.current.mousePosition) && Input.GetMouseButton(0))
				{
					if(GUI.Button(fightButtonCollider, fightButtonPressed, buttonStyle))
					{
						StartCoroutine(__combat());
						break;
					}
				}
  				else if(fightButtonCollider.Contains(Event.current.mousePosition))
				{
					if(GUI.Button(fightButtonCollider, fightButtonHover, buttonStyle))
					{
						StartCoroutine(__combat());
						break;
					}
				}
				else
				{
					if(GUI.Button(fightButtonCollider, fightButton, buttonStyle))
					{
						StartCoroutine(__combat());
						break;
					}
				}
				break;
			
			case State.CELEBRATING:
				if(irrationalFearBonus > 0)
					GUI.Box(new Rect(300, 500, 300, 50),  "Immersion therapy bonus: +" + irrationalFearBonus);
				break;
			
			case State.FLEEING:
			case State.COMBAT:
				// show the strength of the party relative to the monster. +ve means the *party* is stronger.
				GUI.Box(new Rect(370, 10, 300, 50),  (-Monster.instance.strength).ToString("+#;-#;0"));
				break;
			
			case State.DEFEAT:
				if(GUI.Button(new Rect(550, 200, 100, 50), "LOSER! Try again?"))
					reset();
				break;
			
			case State.VICTORY:
				foreach(Transform child in Monster.instance.transform)
					Destroy (child.gameObject);
				if(GUI.Button(new Rect(550, 200, 100, 50), "WINNER! Try again?"))
					reset();
				break;
		}
		
		
		
		//GUI.Box(new Rect(20, 100, 300, 50), "room: " + currentRoomNumber);

		// show player health as hearts
		for(int i = 1; i <= 20; i++)
		{
			if(i % 2 == 0 && i <= currentStamina)
				GUI.Label (new Rect(20+(i*15),20,30,30),heart);
			if(i % 2 == 1 && i == currentStamina)
				GUI.Label (new Rect(20+((i+1)*15),20,30,30),halfHeart);
			if(i % 2 == 0 && i>currentStamina && (i-1) != currentStamina)
				GUI.Label (new Rect(20+(i*15),20,30,30),emptyHeart);
		}
		
		
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
	}
	
	#endregion advance  
	
	#region escape 

	private static readonly float fleeDuration = 1.5f;
	private static readonly float regroupDuration = 1.0f;
	
	private IEnumerator __flight()
	{
		// increase stamina if the party would have lost the fight, decrease it otherwise
		if(Monster.instance.strength > 0)
			currentStamina = Math.Min (currentStamina + 1,startingStamina);
		else
			currentStamina--;	
		
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
	
	private IEnumerator __forcedFlight()
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
				total += Mathf.Clamp(((Hero)hero).fear,-25,25);
			
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
		if(Monster.instance.strength <= 0)
		{
			// start celebrating this battle being won
			state = State.CELEBRATING;
		
			// Face your fears bonus
			if(totalFear > 0)
			{
				irrationalFearBonus = (int)(Mathf.Log(totalFear,3));
				currentStamina += irrationalFearBonus;
				currentStamina = Math.Min (currentStamina,startingStamina);
			}
			
			else
				irrationalFearBonus = 0;
			
			// celebrate
			yield return new WaitForSeconds(celebrateDuration);
			
			currentRoomNumber++;
			
			// ultimate victory ?
			if(currentRoomNumber > numberOfRooms)
			{
				state = State.VICTORY;
			}
			else
			{
				// advance toward the next monster
				state = State.ADVANCING;
				Monster.instance.reset();
				
				// progress to next stage
				StartCoroutine(__descendIntoDarkness());
			}
		}
		else
		{
			currentStamina -= Monster.instance.strength;
			if(currentStamina <=0)
			{
				// disaster !
				state = State.DEFEAT;
			}
			else
			{
				StartCoroutine(__forcedFlight());
			}
		}
	}
		
	#endregion combat 
}