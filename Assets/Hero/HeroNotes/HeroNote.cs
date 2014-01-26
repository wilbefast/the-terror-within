using UnityEngine;
using System.Collections;

public class HeroNote : MonoBehaviour 
{
	private static readonly string[] qualifiers =
	{
		"Unknown", "Fish", "Hairy", "Horned", "Lion", "Lizard", "Octopus", "Spider", "Swan"
	};
	
	private int currentIndex = 0;
	
	void OnMouseDown()
	{
		currentIndex = (currentIndex + 1) % qualifiers.Length;
		
		renderer.material.mainTexture = (Texture)Resources.Load("Note" + qualifiers[currentIndex]);
	}
}
