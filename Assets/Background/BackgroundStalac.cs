using UnityEngine;
using System.Collections;

public class BackgroundStalac : MonoBehaviour 
{
	void Update () 
	{
		transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back,
            Camera.main.transform.rotation * Vector3.up); 
            
        transform.RotateAround(Vector2.right, -90);
		
	}
	
	void OnDrawGizmos()
	{
		Gizmos.DrawCube(transform.position, new Vector3(0.1f, 1.0f, 0.1f));
	}
}
