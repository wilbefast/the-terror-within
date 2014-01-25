using UnityEngine;
using System.Collections;

public class MonsterAnimation : MonoBehaviour
{
	public bool monsterIsRunning;
	public float baseLine, verticalSpeed;
	private float maxVerticalSpeed = -0.1f;
	
	void Start()
	{
		baseLine = this.transform.position.y;
		verticalSpeed = maxVerticalSpeed;
		monsterIsRunning = false;
	}
	
	void Update ()
	{
		monsterIsRunning = (Dungeon.instance.state == Dungeon.State.ADVANCING);
		
		if(monsterIsRunning || verticalSpeed > maxVerticalSpeed)
		{
			if(verticalSpeed <= maxVerticalSpeed)
			{
				verticalSpeed = -maxVerticalSpeed;
			}
			else
			{
				verticalSpeed += maxVerticalSpeed / 40f;
			}
			this.transform.Translate (new Vector3(0, verticalSpeed, 0));
		}
	}
}