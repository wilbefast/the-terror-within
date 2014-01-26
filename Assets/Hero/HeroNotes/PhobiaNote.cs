using UnityEngine;
using System.Collections;

public class PhobiaNote : HeroNote 
{
	private static readonly string[] qualifiers =
	{
		"UnknownPhobia", "Fish", "Hairy", "Horned", "Lion", "Lizard", "Octopus", "Spider", "Swan"
	};
	
	private int currentIndex = 0;
	
	void Start()
	{
		renderer.material.mainTexture = (Texture)Resources.Load("Note" + qualifiers[currentIndex]);
	}
	
	void OnMouseDown()
	{
		currentIndex = (currentIndex + 1) % qualifiers.Length;
		renderer.material.mainTexture = (Texture)Resources.Load("Note" + qualifiers[currentIndex]);
	}
}
