using UnityEngine;
using System.Collections;

public class Dungeon : MonoBehaviour
{
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
				Destroy (GameObject.Find("The Hydra"));
				currentRoomNumber ++;
				var newMonster = new GameObject("The Hydra");
				newMonster.AddComponent<Monster>();
				newMonster.transform.parent = GameObject.Find ("Monstrous Horde").transform;
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
		int output = 0;
		Transform heroicParty = GameObject.Find("Heroic Party").transform;
		for(int i = 0; i < heroicParty.childCount; i++)
		{
			output += heroicParty.GetChild(i).GetComponent<Hero>().combatAbility;
		}
		return output;
	}
	
	#endregion dungeon progression 
}