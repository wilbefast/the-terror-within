using UnityEngine;
using System.Collections;

public class HeroAnimation : MonoBehaviour
{
	public float baseLine, verticalSpeed;
	
	private float angle;
	
	void Start()
	{
		angle = Random.Range(0, Mathf.PI*2);
		baseLine = this.transform.position.y;
	}
		
	void Update ()
	{
		var p = transform.localPosition;
		
		if((Dungeon.instance.state == Dungeon.State.ADVANCING)
		|| (Dungeon.instance.state == Dungeon.State.FLEEING))
		{
			angle += Time.deltaTime*10;
			if(angle > Mathf.PI*2)
				angle -= Mathf.PI*2;
			
			transform.localPosition = new Vector3(p.x, baseLine + Mathf.Abs (Mathf.Sin(angle)), p.z);
		}
		else
		{
			angle = 0.0f;
			transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(p.x, baseLine, p.z), Time.deltaTime*10);
		}
	}
}