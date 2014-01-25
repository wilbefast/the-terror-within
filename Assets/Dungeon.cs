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
	
	#region input: keyboard controls 
	
	public KeyCode fightKey;
	public KeyCode runKey;
	
	enum State
	{
		IDLE,
		FIGHTING,
		RUNNING
	}
	
	private State __state;
	
	void Update () 
	{
		if(Input.GetKey(fightKey))
		{
			__state = State.FIGHTING;
			if(GetPartyCombatStrength() >= GameObject.Find("The Hydra").GetComponent<Monster>().strength)
			{
				// defeat the monster; progress to next stage
				currentRoomNumber ++;
				Monster.instance.reset();
			}
			else
			{
				// death, defeat, dishonour
			}
		}
		
		if(Input.GetKey(runKey))
			__state = State.RUNNING;
	}
	
	#endregion input: keyboard controls 

	#region dungeon progression 
	
	static int numberOfRooms = 15;
	
	public int currentRoomNumber = 1;
	
	public int GetPartyCombatStrength()
	{
		int totalCombatAbility = 0;
		foreach(var hero in GameObject.FindSceneObjectsOfType(typeof(Hero)))
			totalCombatAbility += ((Hero)hero).combatAbility;
		return totalCombatAbility;
	}
	#endregion dungeon progression 
	
	#region user interface 
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(400, 50, 100, 50), "Run"))
			Monster.instance.reset();
		
		if(GUI.Button(new Rect(550, 50, 100, 50), "Fight"))
			Monster.instance.reset();
		
		GUI.Box(new Rect(200, 400, 300, 50), "party strength: " + GetPartyCombatStrength());
	}
	
	#endregion user interface 
}