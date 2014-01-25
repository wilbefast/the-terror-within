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
			
		}
		
		if(Input.GetKey(runKey))
			__state = State.RUNNING;
	}
	
	#endregion input: keyboard controls 

	#region dungeon progression 
	static int numberOfRooms = 15;
	public int currentRoomNumber = 1;
	
	#endregion dungeon progression 
}