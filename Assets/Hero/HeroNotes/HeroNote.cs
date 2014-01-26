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
	
	void Update()
	{
		if(!hovering)
			return;
		
		if(Input.GetMouseButtonDown(0))
			switchNote();
			
		else if(Input.GetMouseButtonDown(1))
			resetNote();
	}
	
	private Rect rect = new Rect(0, 0, 200, 200);
	
	void OnGUI()
	{
		if(hovering && Dungeon.instance.state != Dungeon.State.ADVANCING && Dungeon.instance.state != Dungeon.State.FLEEING)
		{
			var p = Camera.main.WorldToScreenPoint(transform.position);
			rect.x = p.x - rect.width/2;
			rect.y = Screen.height - p.y - rect.height - 50;
			GUI.Box(rect, getFlavourText());
		}
	}
	
	protected abstract string getFlavourText();
	protected abstract void switchNote();
	protected abstract void resetNote();
}
