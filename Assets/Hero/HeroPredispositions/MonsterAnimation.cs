using UnityEngine;
using System.Collections;

public class MonsterAnimation : MonoBehaviour
{
	public bool monsterIsRunning;
	public float baseLine, verticalSpeed;
	private float maxVerticalSpeed = -0.05f;
	private float horizontalSpeed = -0.025f;
	
	void Start()
	{
		baseLine = this.transform.position.y;
		verticalSpeed = maxVerticalSpeed;
		monsterIsRunning = false;
	}
	
	void Update ()
	{
	
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
			this.transform.Translate (new Vector3(0,verticalSpeed,0));
		}
		
		if(this.transform.position.x > 5.0f)
		{
			monsterIsRunning = true;
			((RunningAnimation)GameObject.FindObjectOfType(typeof(RunningAnimation))).partyIsRunning = true;
			this.transform.Translate ( new Vector3(horizontalSpeed,0,0));
		}
		else
		{
			monsterIsRunning = false;
			((RunningAnimation)GameObject.FindObjectOfType(typeof(RunningAnimation))).partyIsRunning = false;
		}
	}
}