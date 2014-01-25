using UnityEngine;
using System.Collections;

public class HeroAnimation : MonoBehaviour
{
	public float baseLine, verticalSpeed;
	private float maxVerticalSpeed = 0.25f;
	
	void Start()
	{
		baseLine = this.transform.position.y;
		verticalSpeed = maxVerticalSpeed;
	}
	
	void Update ()
	{
		var partyIsRunning = (Dungeon.instance.state == Dungeon.State.ADVANCING);
		
		if(partyIsRunning || verticalSpeed < maxVerticalSpeed)
		{
			if(verticalSpeed >= maxVerticalSpeed)
			{
				verticalSpeed = -maxVerticalSpeed;
			}
			else
			{
				verticalSpeed += maxVerticalSpeed / 10f;
			}
			this.transform.Translate (new Vector3(0,verticalSpeed,0));
		}
	}
}