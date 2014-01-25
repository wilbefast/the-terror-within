using UnityEngine;
using System.Collections;

public class Dungeon : MonoBehaviour
{
	#region singleton 
	
	public static Dungeon instance
	{
		get
		{
			return (Dungeon)(GameObject.FindSceneObjectsOfType(typeof(Dungeon))[0]);
		}
	}
	
	#endregion singleton 
	

	#region dungeon progression 
	
	static int numberOfRooms = 15;
	
	public int currentRoomNumber = 1;
	
	public int GetPartyCombatStrength()
	{
		int totalCombatAbility = 0;
		foreach(var hero in GameObject.FindSceneObjectsOfType(typeof(Hero)))
			totalCombatAbility += ((Hero)hero).combatAbility;
		return totalCombatAbility;
	}
	#endregion dungeon progression 
	
	#region user interface 
	
	public GUIText resultText;
	
	[RangeAttribute(1.0f, 5.0f)]
	public float textDuration = 3.0f;
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(400, 50, 100, 50), "Run"))
		{
			StopAllCoroutines();
			StartCoroutine(__showTextForDuration(
				"FLED from a strength " + Monster.instance.strength + " monster.", textDuration));
			
			Monster.instance.reset();
		}
		
		if(GUI.Button(new Rect(550, 50, 100, 50), "Fight"))
		{
			if(GetPartyCombatStrength() >= Monster.instance.strength)
			{
				// defeat the monster; progress to next stage
				currentRoomNumber++;
				
				StopAllCoroutines();
				StartCoroutine(__showTextForDuration(
					"WON against a strength " + Monster.instance.strength + " monster!", textDuration));
				
				Monster.instance.reset();
				
				foreach(var hero in GameObject.FindSceneObjectsOfType(typeof(Hero)))
					((Hero)hero).combatAbility ++;
			}
			else
			{
				StopAllCoroutines();
				StartCoroutine(__showTextForDuration(
					"KILLED by a strength " + Monster.instance.strength + " monster!", textDuration));
			}
		}
	
		//GUI.Box(new Rect(200, 400, 300, 50), "party strength: " + GetPartyCombatStrength());
	}
	
	private IEnumerator __showTextForDuration(string text, float duration)
	{
		
		
		guiText.text = text;
		
		guiText.enabled = true;
		
		yield return new WaitForSeconds(duration);
		
		guiText.enabled = false;
	}
	
	#endregion user interface 
}