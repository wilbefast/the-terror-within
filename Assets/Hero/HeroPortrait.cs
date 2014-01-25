using UnityEngine;
using System.Collections;

public class HeroPortrait : MonoBehaviour 
{
	public Hero hero;

	private static readonly float minFear = -25.0f;
	private static readonly float maxFear = 25.0f;
	private static readonly int numberOfPortraits = 13;
	
	void Update()
	{
		// choose which portrait to draw
		switch(Dungeon.instance.state)
		{
			case Dungeon.State.DECISION:
			case Dungeon.State.COMBAT:
				float normalisedFear = (Mathf.Clamp(hero.fear, minFear, maxFear) - minFear) / (maxFear - minFear);
				normalisedFear *= normalisedFear;
				renderer.material.mainTexture = (Texture)Resources.Load("Portrait" + ((int)(normalisedFear * (numberOfPortraits-1))).ToString("D2"));
				break;
			
			case Dungeon.State.ADVANCING:
				renderer.material.mainTexture = (Texture)Resources.Load("PortraitAdvancing");
				break;
			
			case Dungeon.State.DEFEAT:
				renderer.material.mainTexture = (Texture)Resources.Load("PortraitDead");
				break;
			
			case Dungeon.State.CELEBRATING:
				renderer.material.mainTexture = (Texture)Resources.Load("PortraitCelebrate");
				break;
			
			case Dungeon.State.FLEEING:
				renderer.material.mainTexture = (Texture)Resources.Load("PortraitFlee");
				break;
			
			case Dungeon.State.VICTORY:
				renderer.material.mainTexture = (Texture)Resources.Load("PortraitVictory");
				break;
			
			default:
				renderer.material.mainTexture = (Texture)Resources.Load("Portrait02");
				break;	
		}
	}
}
