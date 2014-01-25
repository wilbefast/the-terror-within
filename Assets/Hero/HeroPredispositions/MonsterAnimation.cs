using UnityEngine;
using System.Collections;

public class MonsterAnimation : MonoBehaviour
{
	public bool monsterIsRunning;
	public float baseLine, verticalSpeed;
	private float maxVerticalSpeed = -0.05f;
	
	void Start()
	{
		baseLine = this.transform.position.y;
		verticalSpeed = maxVerticalSpeed;
		monsterIsRunning = false;
	}
	
	void Update ()
	{
	
		if(monsterIsRunning)
		{
			if(verticalSpeed <= maxVerticalSpeed)
			{
				verticalSpeed = -maxVerticalSpeed;
			}
			else
			{
				verticalSpeed += maxVerticalSpeed / 44f;
			}
			this.transform.Translate (new Vector3(0,verticalSpeed,0));
		}
	}
}
