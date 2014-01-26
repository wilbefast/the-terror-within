using UnityEngine;
using System.Collections;

public abstract class HeroNote : MonoBehaviour 
{
	private bool __hovering;
	public bool hovering
	{
		get { return __hovering; }
	}
	
	void OnMouseEnter()
	{
		__hovering = true;
	}
	
	void OnMouseExit()
	{
		__hovering = false;
	}
}
