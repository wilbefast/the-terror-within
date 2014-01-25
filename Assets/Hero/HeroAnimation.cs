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
		var moving = false;
		
		if(Dungeon.instance.state == Dungeon.State.ADVANCING) 
			moving = true;
		else if (Dungeon.instance.state == Dungeon.State.FLEEING) 
			moving = true;
			
		if(moving || verticalSpeed < maxVerticalSpeed)
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